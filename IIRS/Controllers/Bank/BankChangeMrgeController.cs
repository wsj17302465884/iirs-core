using IIRS.IServices;
using IIRS.IServices.Bank;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Utilities.Common;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RT.Comb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IIRS.Controllers
{
    /// <summary>
    /// 接口API控制器
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class BankChangeMrgeController : ControllerBase
    {
        private readonly ILogger<BankChangeMrgeController> _logger;
        private readonly IChangeMrgeServices _IChangeMrgeServices;
        private readonly IBankChangeMrgeServices _IBankChangeMrgeServices;
        readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICommonServices _CommonServices;

        public BankChangeMrgeController(ICommonServices commonServices, ILogger<BankChangeMrgeController> logger, IChangeMrgeServices _IChangeMrgeServices, IBankChangeMrgeServices bankChangeMrgeServices, IWebHostEnvironment webHostEnvironment)
        {
            this._logger = logger;
            this._CommonServices = commonServices;
            this._IChangeMrgeServices = _IChangeMrgeServices;
            this._IBankChangeMrgeServices = bankChangeMrgeServices;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        public MessageModel<string> Post([FromForm] string strVModel, [FromForm] string strFileTree)
        {
            if (string.IsNullOrEmpty(strVModel))
            {
                return new MessageModel<string>()
                {
                    msg = "数据保存格式错误，原因：未获取到业务数据！",
                    success = false
                };
            }
            ChangeMrgeVModel pageDataInfo = null;
            string saveDataJson = HttpUtility.UrlDecode(strVModel);
            List<Base64FilesVModel> fileTree = null;
            try
            {
                pageDataInfo = JsonConvert.DeserializeObject<ChangeMrgeVModel>(saveDataJson);
                if (!string.IsNullOrEmpty(strFileTree))
                {
                    fileTree = JsonConvert.DeserializeObject<List<Base64FilesVModel>>(strFileTree);
                }
            }
            catch (Exception ex)
            {
                return new MessageModel<string>()
                {
                    msg = "数据保存格式错误，原因：" + ex.Message,
                    success = false
                };
            }
            DateTime saveTime = DateTime.Now;
            try
            {
                List<PUB_ATT_FILE> attFileModel = null;
                string OldXID = string.Empty;
                bool isInsert = string.IsNullOrEmpty(pageDataInfo.XID);
                decimal? preFlowID = null;
                if (isInsert)
                {
                    pageDataInfo.SJSJ = saveTime;
                    pageDataInfo.XID = Provider.Sql.Create().ToString();//流程实力现实手主键
                    pageDataInfo.AUZ_ID = Provider.Sql.Create().ToString();//流程实例主键
                    pageDataInfo.JSON_PK = Provider.Sql.Create().ToString();//POST表json原始数据主键

                    pageDataInfo.qlxgInfo.XTBH = Provider.Sql.Create().ToString();//权力信息表插入主键
                    pageDataInfo.qlxgInfo.XID = pageDataInfo.XID;
                }
                else//否则为修改,就存在退回修改或直接修改情况
                {
                    var flowResult = this._CommonServices.FlowInfoQuery(pageDataInfo.XID).Result;
                    //string[] slbhs = string.Concat(flowResult.SLBH).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (flowResult == null || flowResult.CURRENT_STATUS != SysBdcFlowConst.FLOW_ZYDYHBBL_BANK)
                    {
                        preFlowID = flowResult.CURRENT_STATUS;
                        return new MessageModel<string>()
                        {
                            msg = "错误的流程编号!",
                            success = false
                        };
                    }
                    else if (flowResult.IS_ACTION_OK == 1)//如果已提交,则不能编辑
                    {
                        return new MessageModel<string>()
                        {
                            msg = "数据已提交后续审批,修改被拒绝!",
                            success = false
                        };
                    }
                    else if (flowResult.IS_ACTION_OK == 2)//如果是退回流程
                    {
                        OldXID = pageDataInfo.XID;//记录作废XID，并更细历史手信息
                        pageDataInfo.XID = Provider.Sql.Create().ToString();//流程实力现实手主键
                        pageDataInfo.JSON_PK = Provider.Sql.Create().ToString();//POST表json原始数据主键

                        pageDataInfo.qlxgInfo.XID = pageDataInfo.XID;//权力信息表
                    }
                    if (flowResult != null)
                    {
                        preFlowID = flowResult.CURRENT_STATUS;
                        pageDataInfo.AUZ_ID = flowResult.AUZ_ID;
                    }
                }
                int imgCount = 0;//附件数量
                if (fileTree != null)
                {
                    imgCount = fileTree.Where(S => S.children.Count > 0).Count();
                    if (imgCount > 0)
                    {
                        attFileModel = SysUtility.UploadSysBase64File(this._webHostEnvironment.WebRootPath, pageDataInfo.SLBH, fileTree);
                        if (attFileModel != null && attFileModel.Count == 0 && pageDataInfo.CommandType == 1)
                        {
                            return new MessageModel<string>()
                            {
                                msg = "无有效的上传附件！",
                                success = false
                            };
                        }
                        else
                        {
                            foreach (var file in attFileModel)
                            {
                                file.XID = pageDataInfo.XID;
                            }
                        }
                    }
                }
                if (pageDataInfo.CommandType == 1 && imgCount < 1)//提交保存附件不能为空
                {
                    return new MessageModel<string>()
                    {
                        msg = "请上传附件信息!",
                        success = false
                    };
                }
                string newSlbh = pageDataInfo.SLBH;
                string newSlbh_1 = pageDataInfo.SLBH + "-1";
                //pageDataInfo.AUZ_ID = xid;
                if (pageDataInfo.qlxgInfo != null)
                {
                    pageDataInfo.qlxgInfo.SLBH = newSlbh;
                }
                List<SFD_INFO> sfdList = new List<SFD_INFO>();
                List<SFD_FB_INFO> sfdDetailList = new List<SFD_FB_INFO>();
                if (pageDataInfo.djSfd != null)
                {
                    pageDataInfo.djSfd.XID = pageDataInfo.XID;
                    pageDataInfo.djSfd.SLBH = newSlbh;
                    sfdList.Add(pageDataInfo.djSfd);
                }
                if (pageDataInfo.djSfdList != null && pageDataInfo.djSfdList.Count > 0)
                {
                    foreach (var sfdDetail in pageDataInfo.djSfdList)
                    {
                        sfdDetail.CWSFDXBBH = Provider.Sql.Create().ToString();
                        sfdDetail.XID = pageDataInfo.XID;
                        sfdDetail.SL = sfdDetail.SL;
                        sfdDetail.SLBH = newSlbh;
                        sfdDetailList.Add(sfdDetail);
                    }
                }
                if (pageDataInfo.dySfd != null)
                {
                    pageDataInfo.dySfd.XID = pageDataInfo.XID;
                    pageDataInfo.dySfd.SLBH = newSlbh_1;
                    sfdList.Add(pageDataInfo.dySfd);
                }
                if (pageDataInfo.dySfdList != null && pageDataInfo.dySfdList.Count > 0)
                {
                    foreach (var sfdDetail in pageDataInfo.dySfdList)
                    {
                        sfdDetail.CWSFDXBBH = Provider.Sql.Create().ToString();
                        sfdDetail.XID = pageDataInfo.XID;
                        sfdDetail.SLBH = newSlbh_1;
                        sfdDetail.SL = sfdDetail.SL;
                        sfdDetailList.Add(sfdDetail);
                    }
                }
                BankAuthorize AuzInfo = null;
                IFLOW_DO_ACTION flowInfo = null;
                if (pageDataInfo.CommandType == 0)//如果是暂存
                {
                    if (isInsert)//并且是新办件
                    {
                        AuzInfo = new BankAuthorize()
                        {
                            BID = pageDataInfo.AUZ_ID,
                            AUTHORIZATIONDATE = saveTime,
                            STATUS = SysBdcFlowConst.FLOW_ZYDYHBBL_BANK,
                            PRE_STATUS = null
                        };

                        flowInfo = new IFLOW_DO_ACTION()
                        {
                            PK = Provider.Sql.Create().ToString(),
                            FLOW_ID = SysBdcFlowConst.FLOW_ZYDYHBBL_BANK,
                            PRE_FLOW_ID = null,
                            AUZ_ID = pageDataInfo.AUZ_ID,
                            CDATE = saveTime,
                            USER_NAME = pageDataInfo.SJR
                        };
                    }
                }
                else
                {
                    AuzInfo = new BankAuthorize()
                    {
                        BID = pageDataInfo.AUZ_ID,
                        AUTHORIZATIONDATE = saveTime,
                        STATUS = SysBdcFlowConst.FLOW_ZYDYHBBL_SL_BANK,
                        PRE_STATUS = preFlowID
                    };

                    flowInfo = new IFLOW_DO_ACTION()
                    {
                        PK = Provider.Sql.Create().ToString(),
                        FLOW_ID = SysBdcFlowConst.FLOW_ZYDYHBBL_SL_BANK,
                        PRE_FLOW_ID = preFlowID,
                        AUZ_ID = pageDataInfo.AUZ_ID,
                        CDATE = saveTime,
                        USER_NAME = pageDataInfo.SJR
                    };
                }
                //BankAuthorize AuzInfo = new BankAuthorize()
                //{
                //    BID = pageDataInfo.AUZ_ID,
                //    STATUS = SysBdcFlowConst.FLOW_ZYDYHBBL_SL_BANK,
                //    PRE_STATUS = preFlowID
                //};
                //IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                //{
                //    PK = Provider.Sql.Create().ToString(),
                //    FLOW_ID = SysBdcFlowConst.FLOW_ZYDYHBBL_SL_BANK,
                //    PRE_FLOW_ID = null,
                //    AUZ_ID = pageDataInfo.AUZ_ID,
                //    CDATE = saveTime,
                //    USER_NAME = pageDataInfo.SJR
                //};

                DJB_INFO djbInfo = new DJB_INFO()
                {
                    SLBH = newSlbh,
                    DJLX = "转移登记",
                    DJYY = pageDataInfo.DJ_DJYY,
                    SQRQ = saveTime,
                    BDCZH = pageDataInfo.BDCQZH_NEW,
                    GYFS = pageDataInfo.GYFS,
                    ZSLX = "房屋不动产证",
                    QT = pageDataInfo.QT,
                    FJ = pageDataInfo.FJ,
                    XGZH = pageDataInfo.selectHouse.BDCZH,
                    SSJC = "辽",
                    JGJC = "辽阳市",
                    FZND = DateTime.Now.Year.ToString(),
                    ZSH = string.Concat(pageDataInfo.BDCQZH_NEW_NUM),
                    xid= pageDataInfo.XID,
                    SZQY = pageDataInfo.SZQY
                };

                DY_INFO dyInfo = new DY_INFO()
                {
                    SLBH = newSlbh_1,
                    DYLX = "一般抵押权",
                    DJYY = pageDataInfo.DY_DJYY,
                    XGZH = pageDataInfo.BDCQZH_NEW,
                    SQRQ = saveTime,
                    QT = pageDataInfo.QT_DY,
                    DYSW = pageDataInfo.DYSW,
                    DYFS = pageDataInfo.DYFS,
                    DYMJ = pageDataInfo.dyMJ,
                    BDBZZQSE = pageDataInfo.BDBZQSE,
                    PGJE = pageDataInfo.PGJE,
                    HTH = pageDataInfo.HTH,
                    LXR = pageDataInfo.DYLXR,
                    LXRDH = pageDataInfo.DYLXRDH,
                    CNSJ = pageDataInfo.CNSJ ?? DateTime.Now.AddMonths(1),
                    FJ = pageDataInfo.FJ,
                    ZWR = string.Join(",", pageDataInfo.selectDyPerson.Cast<ChangeMrgePersonVModel>().Select(p => p.QLRMC).ToArray()),
                    ZWRZJH = string.Join(",", pageDataInfo.selectDyPerson.Cast<ChangeMrgePersonVModel>().Select(p => p.ZJHM).ToArray()),
                    ZWRZJLX = string.Join(",", pageDataInfo.selectDyPerson.Cast<ChangeMrgePersonVModel>().Select(p => p.ZJLB).Distinct().ToArray()),//改成中文类型
                    DLJGMC = pageDataInfo.DYQRMC,
                    QLQSSJ = pageDataInfo.ZWLXQXQSRQ,
                    QLJSSJ = pageDataInfo.ZWLXQXJZRQ,
                    DYQX = pageDataInfo.LXQX.ToString(),
                    XID = pageDataInfo.XID,
                    SZQY = pageDataInfo.SZQY,
                    ZGZQSE= pageDataInfo.ZGZQSE,
                    DBFW = pageDataInfo.DBFW,
                    QRZYQK = pageDataInfo.QRZYQK,
                    SFCZYD = pageDataInfo.QRZYQK,
                    DYRLX = pageDataInfo.DYRLX,
                };

                List<TSGL_INFO> tsgl = new List<TSGL_INFO>();
                tsgl.Add(new TSGL_INFO()
                {
                    GLBM = Provider.Sql.Create().ToString(),
                    SLBH = newSlbh,
                    BDCLX = pageDataInfo.selectHouse.BDCLX,
                    TSTYBM = pageDataInfo.selectHouse.TSTYBM,
                    BDCDYH = pageDataInfo.selectHouse.BDCDYH,
                    DJZL = "权属",
                    XID= pageDataInfo.XID,
                    CSSJ = saveTime
                });
                tsgl.Add(new TSGL_INFO()
                {
                    GLBM = Provider.Sql.Create().ToString(),
                    SLBH = newSlbh_1,
                    BDCLX = pageDataInfo.selectHouse.BDCLX,
                    TSTYBM = pageDataInfo.selectHouse.TSTYBM,
                    BDCDYH = pageDataInfo.selectHouse.BDCDYH,
                    DJZL = "抵押",
                    XID = pageDataInfo.XID,
                    CSSJ = saveTime
                });

                List<QLRGL_INFO> qlrglList = new List<QLRGL_INFO>();
                //第一笔业务:转移
                foreach (var djywr in pageDataInfo.selectYwPerson)//原房主（或开发商）,义务人
                {
                    qlrglList.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSlbh,
                        YWBM = newSlbh,
                        ZJHM = djywr.ZJHM,
                        QLRID = "义务人_" + djywr.ZJHM, //djQlr.QLRID,
                        QLRMC = djywr.QLRMC,
                        GYFE = djywr.GYFE,
                        GYFS = djywr.GYFS,
                        ZJLB = djywr.ZJLB,
                        ZJLB_ZWM = djywr.ZJLB_ZWM,
                        DH = djywr.DH,
                        QLRLX = "义务人",
                        SXH = djywr.SXH,
                        IS_OWNER = djywr.IS_OWNER,
                        XID = pageDataInfo.XID
                    });
                }
                foreach (var qlr in pageDataInfo.selectDyPerson)//权利人买家
                {
                    qlrglList.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSlbh,
                        YWBM = newSlbh,
                        ZJHM = qlr.ZJHM,
                        QLRID = "权利人_" + qlr.ZJHM, //djQlr.QLRID,
                        QLRMC = qlr.QLRMC,
                        GYFE = qlr.GYFE,
                        GYFS = qlr.GYFS,
                        ZJLB = qlr.ZJLB,
                        ZJLB_ZWM = qlr.ZJLB_ZWM,
                        DH = qlr.DH,
                        QLRLX = "权利人",
                        SXH = qlr.SXH,
                        IS_OWNER = qlr.IS_OWNER,
                        XID = pageDataInfo.XID
                    });
                }
                //第一笔业务:抵押
                foreach (var dyr in pageDataInfo.selectDyPerson)//保存抵押人
                {
                    qlrglList.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSlbh_1,
                        YWBM = newSlbh_1,
                        ZJHM = dyr.ZJHM,
                        QLRID = "抵押人_" + dyr.ZJHM,//djYwr.QLRID,
                        QLRMC = dyr.QLRMC,
                        GYFE = dyr.GYFE,
                        GYFS = dyr.GYFS,
                        ZJLB = dyr.ZJLB,
                        ZJLB_ZWM = dyr.ZJLB_ZWM,
                        DH = dyr.DH,
                        QLRLX = "抵押人",
                        SXH = dyr.SXH,
                        IS_OWNER = dyr.IS_OWNER,
                        XID = pageDataInfo.XID
                    });
                }
                foreach (var dyqr in pageDataInfo.selectDyqPerson)//抵押权人
                {
                    qlrglList.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSlbh_1,
                        YWBM = newSlbh_1,
                        ZJHM = dyqr.ZJHM,
                        QLRID = "抵押权人_" + dyqr.ZJHM,  //dyr.QLRID,
                        QLRMC = dyqr.QLRMC,
                        GYFE = dyqr.GYFE,
                        GYFS = dyqr.GYFS,
                        ZJLB = dyqr.ZJLB,
                        ZJLB_ZWM = dyqr.ZJLB_ZWM,
                        DH = dyqr.DH,
                        QLRLX = "抵押权人",
                        SXH = dyqr.SXH,
                        IS_OWNER = dyqr.IS_OWNER,
                        XID = pageDataInfo.XID
                    });
                }

                List<XGDJGL_INFO> xgdjgllList = new List<XGDJGL_INFO>();
                xgdjgllList.Add(new XGDJGL_INFO()
                {
                    BGBM = Provider.Sql.Create().ToString(),
                    ZSLBH = newSlbh,
                    FSLBH = pageDataInfo.selectHouse.SLBH,
                    BGRQ = saveTime,
                    BGLX = "权属变更",
                    XGZLX = "房屋不动产证",
                    XGZH = pageDataInfo.selectHouse.BDCZH,
                    XID = pageDataInfo.XID,
                });
                xgdjgllList.Add(new XGDJGL_INFO()
                {
                    BGBM = Provider.Sql.Create().ToString(),
                    ZSLBH = newSlbh_1,
                    FSLBH = newSlbh,
                    BGRQ = saveTime,
                    BGLX = "抵押",
                    XGZLX = "房屋不动产证",
                    XGZH = pageDataInfo.BDCQZH_NEW,
                    XID = pageDataInfo.XID
                });

                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    XID = pageDataInfo.XID,
                    YWSLBH = newSlbh,
                    DJZL = SysBdcFlowConst.FLOW_ZYDYHBBL_GROUP,
                    BDCZH = pageDataInfo.selectHouse.BDCZH,
                    SLBH = newSlbh,
                    QLRMC = string.Join(",", pageDataInfo.selectYwPerson.Cast<ChangeMrgePersonVModel>().Select(s => s.QLRMC).ToArray()),
                    //dyInfo.selectDyPerson[0].QLRMC
                    ZL = pageDataInfo.selectHouse.ZL,
                    ORG_ID = pageDataInfo.BankDeptID,
                    USER_ID = pageDataInfo.DYQRMC_ID,
                    TEL = pageDataInfo.DYLXRDH,
                    AUZ_ID = pageDataInfo.AUZ_ID,
                    HTH = dyInfo.HTH,
                    REMARK1 = "转移及抵押",
                    SJR = dyInfo.SJR,
                    IS_ACTION_OK = pageDataInfo.CommandType,
                    PDH = pageDataInfo.PDH,
                    SAVEDATE = saveTime
                };
                List<SJD_INFO> sjdList = new List<SJD_INFO>();
                sjdList.Add(new SJD_INFO()
                {
                    SLBH = newSlbh,
                    DJDL = "200",
                    LCLX = pageDataInfo.LCLX,
                    LCMC = pageDataInfo.LCMC,
                    ZL = pageDataInfo.ZL,
                    SJR = pageDataInfo.SJR,
                    SJSJ = saveTime,
                    CNSJ = pageDataInfo.CNSJ,
                    QXDM = pageDataInfo.SZQY,
                    TZRXM = regInfo.QLRMC,
                    TZRDH = regInfo.TEL,
                    XID = regInfo.XID
                });

                sjdList.Add(new SJD_INFO()
                {
                    SLBH = newSlbh_1,
                    DJDL = "910",
                    LCLX = pageDataInfo.LCLX,
                    LCMC = pageDataInfo.LCMC,
                    ZL = pageDataInfo.ZL,
                    SJR = pageDataInfo.SJR,
                    SJSJ = saveTime,
                    CNSJ = pageDataInfo.CNSJ,
                    QXDM = pageDataInfo.SZQY,
                    TZRXM = regInfo.QLRMC,
                    TZRDH = regInfo.TEL,
                    XID = regInfo.XID
                });

                SysDataRecorderModel saveJson = new SysDataRecorderModel()
                {
                    PK = pageDataInfo.JSON_PK,
                    BUS_PK = pageDataInfo.XID,
                    CDATE = saveTime,
                    USER_ID = pageDataInfo.DYQRMC_ID,
                    USER_NAME = pageDataInfo.DYLXR,
                    IS_STOP = pageDataInfo.CommandType,
                    SAVEDATAJSON = JsonConvert.SerializeObject(pageDataInfo),
                    REMARKS1 = pageDataInfo.selectHouse.BDCZH,//不动产证明号
                    REMARKS2 = string.Join(",", pageDataInfo.selectDyPerson.Cast<ChangeMrgePersonVModel>().Select(S => S.QLRMC).Distinct().ToArray()),
                    REMARKS3 = string.Join(",", pageDataInfo.selectYwPerson.Cast<ChangeMrgePersonVModel>().Select(S => S.QLRMC).Distinct().ToArray()),
                    REMARKS4 = string.Empty,
                    REMARKS5 = string.Empty
                };
                if (pageDataInfo.qlxgInfo != null)
                {
                    pageDataInfo.qlxgInfo.XID = pageDataInfo.XID;
                }

                int count = this._IChangeMrgeServices.ChangeMrgesSave(isInsert, regInfo, AuzInfo, saveJson, flowInfo, null, djbInfo, tsgl, dyInfo, xgdjgllList, qlrglList, pageDataInfo.qlxgInfo, attFileModel, sfdList, sfdDetailList, sjdList, OldXID);
                return new MessageModel<string>()
                {
                    msg = "数据保存成功",
                    success = true,
                    response = @$"{{""XID"":""{pageDataInfo.XID}"",""AUZ_ID"":""{pageDataInfo.AUZ_ID}"",""JSON_PK"":""{pageDataInfo.JSON_PK}""}}"
                };
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<string>()
                {
                    msg = ex.Message,
                    success = false
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<string>()
                {
                    msg = "数据保存失败，原因：" + ex.Message,
                    success = false
                };
            }
            
            
        }

        /// <summary>
        /// 不动产审核银行转移抵押申请
        /// </summary>
        /// <param name="strVModel">审批内容信息</param>
        /// <param name="isAauditingPass">是否审核通过【1:同意，0：不同意】</param>
        /// <returns></returns>
        [HttpPost]
        public MessageModel<string> Auditing([FromForm] string strVModel, [FromForm] int isAauditingPass)
        {
            ChangeMrgeVModel pageDataInfo = null;
            string saveDataJson = HttpUtility.UrlDecode(strVModel);
            try
            {
                pageDataInfo = JsonConvert.DeserializeObject<ChangeMrgeVModel>(saveDataJson);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("Cannot deserialize the current JSON"))
                {
                    return new MessageModel<string>()
                    {
                        msg = "数据保存格式错误，原因:" + ex.Message,
                        success = false
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "数据保存格式错误，请与管理员联系",
                        success = false
                    };
                }
            }
            DateTime saveTime = DateTime.Now;
            try
            {
                IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = isAauditingPass == 1 ? SysBdcFlowConst.FLOW_ZYDYHBBL_SH : SysBdcFlowConst.FLOW_ZYDYHBBL_BANK,
                    PRE_FLOW_ID = SysBdcFlowConst.FLOW_ZYDYHBBL_SL_BANK,
                    AUZ_ID = pageDataInfo.AUZ_ID,
                    CDATE = saveTime,
                    USER_NAME = pageDataInfo.SJR
                };

                BankAuthorize AuzInfo = new BankAuthorize()
                {
                    BID = pageDataInfo.AUZ_ID,
                    AUTHORIZATIONDATE = saveTime,
                    STATUS = flowInfo.FLOW_ID,
                    PRE_STATUS = SysBdcFlowConst.FLOW_ZYDYHBBL_SL_BANK
                };

                SysDataRecorderModel saveJson = new SysDataRecorderModel()
                {
                    BUS_PK = pageDataInfo.XID,
                    SAVEDATAJSON = JsonConvert.SerializeObject(pageDataInfo),
                };

                SPB_INFO spInfo = new SPB_INFO()
                {
                    SPBH = Provider.Sql.Create().ToString(),
                    SLBH = pageDataInfo.SLBH,
                    SPDX = "初审意见",
                    SPYJ = pageDataInfo.SPYJ,
                    SPR = pageDataInfo.SPR,
                    SPRQ = saveTime,
                    SPTXR = pageDataInfo.SPR,
                    XID = pageDataInfo.XID,
                    SPBZ = pageDataInfo.SPBZ
                };

                DY_INFO zxInfo = new DY_INFO()
                {
                    XID = pageDataInfo.XID,
                    SPBZ = pageDataInfo.SPBZ
                };

                DJB_INFO djInfo = new DJB_INFO()
                {
                    xid = pageDataInfo.XID,
                    SPBZ = pageDataInfo.SPBZ,
                    SPRQ = saveTime
                };
                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    IS_ACTION_OK = pageDataInfo.CommandType,
                    XID = pageDataInfo.XID
                };
                int count = this._IBankChangeMrgeServices.Auditing(AuzInfo, regInfo, saveJson, spInfo, flowInfo, zxInfo, djInfo);
                return new MessageModel<string>()
                {
                    msg = "数据保存成功",
                    success = true,
                    response = @$"{{""XID"":""{pageDataInfo.XID}"",""AUZ_ID"":""{pageDataInfo.AUZ_ID}"",""JSON_PK"":""{pageDataInfo.JSON_PK}""}}"
                };
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<string>()
                {
                    msg = ex.Message,
                    success = false
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<string>()
                {
                    msg = "数据保存失败，原因：" + ex.Message,
                    success = false
                };
            }
        }
    }
}
