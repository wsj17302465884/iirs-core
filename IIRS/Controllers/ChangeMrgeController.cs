using IIRS.IServices;
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
    public class ChangeMrgeController : ControllerBase
    {
        private readonly ILogger<ChangeMrgeController> _logger;
        private readonly IChangeMrgeServices _IChangeMrgeServices;
        readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICommonServices _CommonServices;

        public ChangeMrgeController(ICommonServices commonServices, ILogger<ChangeMrgeController> logger, IChangeMrgeServices _IChangeMrgeServices, IWebHostEnvironment webHostEnvironment)
        {
            this._logger = logger;
            this._CommonServices = commonServices;
            this._IChangeMrgeServices = _IChangeMrgeServices;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<MessageModel<string[]>> GetBDCZH_NUM()
        {
            try
            {
                return new MessageModel<string[]>()
                {
                    msg = "获取成功",
                    success = true,
                    response = await this._IChangeMrgeServices.GetBDCZH_NUM()
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"ChangeMrgeController.GetBDCZH_NUM:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<string[]>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null
                };
            }
        }

        [HttpGet]
        public async Task<MessageModel<ChangeMrgeHouseVModel>> GetHouseInfo(string bdzcz)
        {
            try
            {
                ChangeMrgeHouseVModel model = await this._IChangeMrgeServices.GetHouseInfo(bdzcz);
                return new MessageModel<ChangeMrgeHouseVModel>()
                {
                    msg = "获取成功",
                    success = true,
                    response = model
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"ChangeMrgeController.GetHouseInfo:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<ChangeMrgeHouseVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null
                };
            }
        }

        [HttpGet]
        public async Task<MessageModel<List<ChangeMrgePersonVModel>>> GetPersonInfo(string bdzcz)
        {
            try
            {
                var model = await this._IChangeMrgeServices.GetPersonInfo(bdzcz);
                return new MessageModel<List<ChangeMrgePersonVModel>>()
                {
                    msg = "获取成功",
                    success = true,
                    response = model
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"ChangeMrgeController.GetPersonInfo:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<List<ChangeMrgePersonVModel>>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null
                };
            }
        }

        [HttpGet]
        public async Task<MessageModel<QL_XG_INFO>> GetLandHouseRightInfo(string bdzcz)
        {
            try
            {
                QL_XG_INFO model = await this._IChangeMrgeServices.GetLandHouseRightInfo(bdzcz);
                return new MessageModel<QL_XG_INFO>()
                {
                    msg = "获取成功",
                    success = true,
                    response = model
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"ChangeMrgeController.GetLandHouseRightInfo:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<QL_XG_INFO>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null
                };
            }
        }

        [HttpPost]
        public MessageModel<string> Post([FromForm] string strDYVModel, [FromForm] string strFileTree)
        {
            //Dictionary<string, string> meteData = new Dictionary<string, string>();
            //meteData.Add("AA", "1");
            //meteData.Add("BB", "2");
            //string EEE = JsonConvert.SerializeObject(meteData);
            //var EFS = JsonConvert.DeserializeObject<Dictionary<string, string>>(EEE);
            decimal? preFlowID = null;
            ChangeMrgeVModel pageDataInfo = null;
            string saveDataJson = HttpUtility.UrlDecode(strDYVModel);
            List<Base64FilesVModel> fileTree = null;
            try
            {
                pageDataInfo = JsonConvert.DeserializeObject<ChangeMrgeVModel>(saveDataJson);
                fileTree = JsonConvert.DeserializeObject<List<Base64FilesVModel>>(strFileTree);
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
                List<PUB_ATT_FILE> attFileModel = null;
                string OldXID = string.Empty;
                bool isInsert = string.IsNullOrEmpty(pageDataInfo.XID);
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
                    preFlowID = flowResult.CURRENT_STATUS;
                    //string[] slbhs = string.Concat(flowResult.SLBH).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                    if (flowResult == null || flowResult.CURRENT_STATUS != SysBdcFlowConst.FLOW_ZYDYHBBL_SL)
                    {
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
                        pageDataInfo.AUZ_ID = flowResult.AUZ_ID;

                        pageDataInfo.qlxgInfo.XID = pageDataInfo.XID;//权力信息表
                    }
                }
                //string xid = Provider.Sql.Create().ToString();//主键
                //string IIRS_DJBH = Provider.Sql.Create().ToString();//登记编号,用于保存附件主键
                int imgCount = 0;//附件数量
                if (fileTree != null)
                {
                    imgCount = fileTree.Where(S => S.children.Count > 0).Count();
                    if (imgCount > 0)
                    {
                        attFileModel = SysUtility.UploadSysBase64File(this._webHostEnvironment.WebRootPath, pageDataInfo.SLBH, fileTree);
                        foreach (var file in attFileModel)
                        {
                            file.XID = pageDataInfo.XID;
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
                            STATUS = SysBdcFlowConst.FLOW_ZYDYHBBL_SL,
                            PRE_STATUS = null
                        };

                        flowInfo = new IFLOW_DO_ACTION()
                        {
                            PK = Provider.Sql.Create().ToString(),
                            FLOW_ID = SysBdcFlowConst.FLOW_ZYDYHBBL_SL,
                            PRE_FLOW_ID = null,
                            AUZ_ID = pageDataInfo.AUZ_ID,
                            CDATE = saveTime,
                            USER_NAME = pageDataInfo.DYLXR
                        };
                    }
                }
                else
                {
                    AuzInfo = new BankAuthorize()
                    {
                        BID = pageDataInfo.AUZ_ID,
                        AUTHORIZATIONDATE = saveTime,
                        STATUS = SysBdcFlowConst.FLOW_ZYDYHBBL_SH,
                        PRE_STATUS = preFlowID
                    };

                    flowInfo = new IFLOW_DO_ACTION()
                    {
                        PK = Provider.Sql.Create().ToString(),
                        FLOW_ID = SysBdcFlowConst.FLOW_ZYDYHBBL_SH,
                        PRE_FLOW_ID = SysBdcFlowConst.FLOW_ZYDYHBBL_SL,
                        AUZ_ID = pageDataInfo.AUZ_ID,
                        CDATE = saveTime,
                        USER_NAME = pageDataInfo.DYLXR
                    };
                }
                

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
                    ZGZQSE = pageDataInfo.ZGZQSE,
                    QRZYQK = pageDataInfo.QRZYQK
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
                        QLRID = "权力人_" + qlr.ZJHM, //djQlr.QLRID,
                        QLRMC = qlr.QLRMC,
                        GYFE = qlr.GYFE,
                        GYFS = qlr.GYFS,
                        ZJLB = qlr.ZJLB,
                        ZJLB_ZWM = qlr.ZJLB_ZWM,
                        DH = qlr.DH,
                        QLRLX = "权力人",
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
                    DJZL = 200,
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
                    LCLX = "国有建设用地使用权及房屋所有权登记",
                    LCMC = pageDataInfo.LCMC,
                    ZL = pageDataInfo.ZL,
                    SJR = pageDataInfo.SJR,
                    SJSJ = saveTime,
                    CNSJ = pageDataInfo.CNSJ,
                    QXDM = pageDataInfo.SZQY,
                    TZRXM = regInfo.QLRMC,
                    TZRDH = regInfo.TEL
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

                SPB_INFO spInfo = new SPB_INFO()
                {
                    SPBH = Provider.Sql.Create().ToString(),
                    SLBH = newSlbh,
                    SPDX = "初审意见",
                    SPYJ = pageDataInfo.SPYJ,
                    SPR = pageDataInfo.SPR,
                    SPRQ = saveTime,
                    SPTXR = pageDataInfo.SPR,
                    XID = pageDataInfo.XID
                };

                int count = this._IChangeMrgeServices.ChangeMrgesSave(isInsert, regInfo, AuzInfo, saveJson, flowInfo, spInfo, djbInfo, tsgl, dyInfo, xgdjgllList, qlrglList, pageDataInfo.qlxgInfo, attFileModel, sfdList, sfdDetailList, sjdList, OldXID);
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
