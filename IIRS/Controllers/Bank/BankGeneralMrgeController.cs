using IIRS.IRepository.IIRS;
using IIRS.IServices;
using IIRS.IServices.Bank;
using IIRS.IServices.BDC;
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
    /// 抵押登记
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    public class BankGeneralMrgeController : ControllerBase
    {
        private readonly ILogger<BdcGeneralMrgeController> _logger;
        private readonly IBankGeneralMrgeServices _BankGeneralMrgeServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IBdcGeneralMrgeServices _BdcGeneralMrgeServices;
        private readonly ICommonServices _CommonServices;
        private readonly IChangeMrgeServices _changeMrgeServices;
        private readonly IBus_visit_logRepository _Ibus_visit_logRepository;

        public BankGeneralMrgeController(ICommonServices commonServices, ILogger<BdcGeneralMrgeController> logger, IWebHostEnvironment webHostEnvironment, IBankGeneralMrgeServices bankGeneralMrgeServices, IBdcGeneralMrgeServices bdcGeneralMrgeServices, IChangeMrgeServices changeMrgeServices, IBus_visit_logRepository bus_visit_logRepository)
        {
            this._logger = logger;
            this._BankGeneralMrgeServices = bankGeneralMrgeServices;
            this._webHostEnvironment = webHostEnvironment;
            this._BdcGeneralMrgeServices = bdcGeneralMrgeServices;
            this._CommonServices = commonServices;
            this._changeMrgeServices = changeMrgeServices;
            this._Ibus_visit_logRepository = bus_visit_logRepository;
        }

        /// <summary>
        /// 查询房屋信息
        /// </summary>
        /// <param name="XM">姓名</param>
        /// <param name="SFZH">身份证号</param>
        /// <param name="BDCZH">不动产证号</param>
        /// <param name="BDCLX">不动产类型</param>
        /// <param name="ORGANIZATION_ID">组织机构编号</param>
        /// <param name="USER_ID">操作员ID</param>
        /// <param name="USER_NAME">操作员名称</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        [HttpGet]
        public async Task<MessageModel<PageStringModel>> GetBdcHourseInfo(string XM, string SFZH, string BDCZH, string BDCLX, string ORGANIZATION_ID, string USER_ID, string USER_NAME, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                if (string.IsNullOrEmpty(XM))
                {
                    return new MessageModel<PageStringModel>()
                    {
                        msg = "姓名不能为空",
                        success = false
                    };
                }
                if (string.IsNullOrEmpty(BDCZH))
                {
                    return new MessageModel<PageStringModel>()
                    {
                        msg = "不动产证号不能为空",
                        success = false
                    };
                }
                if (string.IsNullOrEmpty(BDCLX) || !((new string[] { "房屋","土地"}).Contains(BDCLX)))
                {
                    return new MessageModel<PageStringModel>()
                    {
                        msg = "不动产类型只能是:房屋或者土地",
                        success = false
                    };
                }
                if (SFZH.Length != 18)
                {
                    return new MessageModel<PageStringModel>()
                    {
                        msg = "错误的身份证号",
                        success = false
                    };
                }
                BUS_VISIT_LOG Lmodel = new BUS_VISIT_LOG();
                Lmodel.PK = Guid.Empty.ToString("N");
                Lmodel.GROUP_ID = 2;
                Lmodel.PARAMS = $"不动产证号：{BDCZH} 不动产类型：{BDCLX} 姓名{XM}身份证号：{SFZH}";
                if (Lmodel.PARAMS.Length >= 500)
                {
                    Lmodel.PARAMS = Lmodel.PARAMS.Substring(0, 498) + "...";
                }
                Lmodel.ORGANIZATION_ID = ORGANIZATION_ID;
                Lmodel.USER_ID = USER_ID;
                Lmodel.USER_NAME = USER_NAME;
                Lmodel.VDATE = DateTime.Now;
                var Logdata = this._Ibus_visit_logRepository.Add(Lmodel);
                var data = await this._BankGeneralMrgeServices.GetBdcHourseInfo(XM, SFZH, BDCZH, BDCLX, pageIndex, pageSize);
                return new MessageModel<PageStringModel>()
                {
                    msg = "获取成功",
                    success = true,
                    response = data
                };
            }
            catch (Exception ex)
            {

                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PageStringModel>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false
                };
            }
        }

        /// <summary>
        /// 保存不动产一般抵押信息
        /// </summary>
        /// <param name="IsSave">是否为临时保存(1：暂存,不等于1：提交流程)</param>
        /// <param name="strDYVModel"></param>
        /// <param name="strFileTree"></param>
        /// <returns></returns>
        [HttpPost]
        public MessageModel<string> Post([FromForm] int IsSave, [FromForm] string strDYVModel, [FromForm] string strFileTree)
        {
            string newSLBH = string.Empty;
            try
            {
                //bool isInsert = string.IsNullOrEmpty(xid);
                //string AUZ_ID = Provider.Sql.Create().ToString();
                //if (isInsert)
                //{
                //    xid = Provider.Sql.Create().ToString();//登记编号,用于保存附件主键
                //}
                int count = 0;
                DateTime saveTime = DateTime.Now;
                string saveDataJson = HttpUtility.UrlDecode(strDYVModel);
                HouseVModel dyInfo = null;
                decimal? preFlowID = null;
                //string newSLBH = "20200813095210";
                try
                {
                    dyInfo = JsonConvert.DeserializeObject<HouseVModel>(saveDataJson);
                }
                catch (Exception ex)
                {
                    return new MessageModel<string>()
                    {
                        msg = "数据保存格式错误，原因：" + ex.Message,
                        success = false
                    };
                }
                newSLBH = dyInfo.SLBH;//服务器端获取受理编号
                List<Base64FilesVModel> fileTree = null;
                try
                {
                    if (!string.IsNullOrEmpty(strFileTree))
                    {
                        fileTree = JsonConvert.DeserializeObject<List<Base64FilesVModel>>(strFileTree);
                    }
                }
                catch (Exception ex)
                {
                    return new MessageModel<string>()
                    {
                        msg = "附件数据保存格式错误，原因：" + ex.Message,
                        success = false
                    };
                }
                int imgCount = 0;
                if (fileTree != null)
                {
                    imgCount = fileTree.Where(S => S.children.Count > 0).Count();
                }
                if (dyInfo.CommandType == 1 && imgCount == 0)//提交保存附件不能为空
                {
                    return new MessageModel<string>()
                    {
                        msg = "请上传附件信息!",
                        success = false
                    };
                }
                bool isSubmitWorkFlow = dyInfo.CommandType == 1;
                string OldXID = string.Empty;
                bool isInsert = string.IsNullOrEmpty(dyInfo.XID);
                if (isInsert)
                {
                    dyInfo.XID = Provider.Sql.Create().ToString();//流程实力现实手主键
                    dyInfo.AUZ_ID = Provider.Sql.Create().ToString();//流程实例主键
                    dyInfo.JSON_PK = Provider.Sql.Create().ToString();//POST表json原始数据主键
                }
                else//否则为修改 存在退回修改或直接修改情况
                {
                    var flowResult = this._CommonServices.FlowInfoQuery(dyInfo.XID).Result;
                    preFlowID = flowResult.CURRENT_STATUS;
                    if (flowResult == null || flowResult.CURRENT_STATUS != SysBdcFlowConst.FLOW_DYDJ_BANK)
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
                    if (flowResult.IS_ACTION_OK == 2)//如果是退回流程
                    {
                        OldXID = dyInfo.XID;//记录作废XID，并更细历史手信息
                        dyInfo.XID = Provider.Sql.Create().ToString();//流程实力现实手主键
                        dyInfo.JSON_PK = Provider.Sql.Create().ToString();//POST表json原始数据主键
                        dyInfo.AUZ_ID = flowResult.AUZ_ID;
                    }
                    else//否则,正常修改
                    {

                    }
                }
                #region 提取附件数据，提出树形结构数据
                List<PUB_ATT_FILE> attFileModel = null;
                
                if (imgCount > 0)
                {
                    attFileModel = SysUtility.UploadSysBase64File(this._webHostEnvironment.WebRootPath, newSLBH, fileTree);
                    if (attFileModel != null && attFileModel.Count == 0 && dyInfo.CommandType == 1)
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
                            file.XID = dyInfo.XID;
                        }
                    }
                }
                #endregion
                BankAuthorize AuzInfo = null;
                IFLOW_DO_ACTION flowInfo = null;
                if (dyInfo.CommandType == 0)//如果是暂存
                {
                    if (isInsert)//并且是新办件
                    {
                        AuzInfo = new BankAuthorize()
                        {
                            BID = dyInfo.AUZ_ID,
                            AUTHORIZATIONDATE = saveTime,
                            STATUS = SysBdcFlowConst.FLOW_DYDJ_BANK,
                            PRE_STATUS = null
                        };
                        flowInfo = new IFLOW_DO_ACTION()
                        {
                            PK = Provider.Sql.Create().ToString(),
                            FLOW_ID = SysBdcFlowConst.FLOW_DYDJ_BANK,
                            PRE_FLOW_ID = null,
                            AUZ_ID = dyInfo.AUZ_ID,
                            CDATE = saveTime,
                            USER_NAME = dyInfo.SJR
                        };
                    }
                }
                else
                {
                    AuzInfo = new BankAuthorize()
                    {
                        BID = dyInfo.AUZ_ID,
                        AUTHORIZATIONDATE = saveTime,
                        STATUS = SysBdcFlowConst.FLOW_DYDJ_SL_BANK,
                        PRE_STATUS = preFlowID
                    };

                    flowInfo = new IFLOW_DO_ACTION()
                    {
                        PK = Provider.Sql.Create().ToString(),
                        FLOW_ID = SysBdcFlowConst.FLOW_DYDJ_SL_BANK,
                        PRE_FLOW_ID = preFlowID,
                        AUZ_ID = dyInfo.AUZ_ID,
                        CDATE = saveTime,
                        USER_NAME = dyInfo.SJR
                    };
                }
                //BankAuthorize AuzInfo = new BankAuthorize()
                //{
                //    BID = dyInfo.AUZ_ID,
                //    STATUS = SysBdcFlowConst.FLOW_DYDJ_SL_BANK,
                //    PRE_STATUS = preFlowID
                //};
                List<TSGL_INFO> tsglET = new List<TSGL_INFO>();
                List<XGDJGL_INFO> xgdjglET = new List<XGDJGL_INFO>();
                List<QLRGL_INFO> qlrglET = new List<QLRGL_INFO>();
               
                //提交返回值
                //IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                //{
                //    PK = Provider.Sql.Create().ToString(),
                //    FLOW_ID = SysBdcFlowConst.FLOW_DYDJ_SL_BANK,//根据是否为暂存操作，判断当前流程节点提交信息
                //    PRE_FLOW_ID = null,
                //    AUZ_ID = dyInfo.AUZ_ID,
                //    CDATE = saveTime,
                //    USER_NAME = dyInfo.SJR
                //};

                string xgzh = string.Empty;//相关证号
                if (dyInfo.selectDyHouse.Count > 0)
                {
                    xgzh = dyInfo.selectDyHouse[0].BDCZH + (dyInfo.selectDyHouse.Count > 1 ? "等" + dyInfo.selectDyHouse.Count + "个" : string.Empty);
                }
                DY_INFO dyET = new DY_INFO()
                {
                    SLBH = newSLBH,
                    YWSLBH = newSLBH,
                    DJLX = "抵押登记",
                    DJYY = dyInfo.DJYY,
                    XGZH = xgzh,
                    ZLXX = dyInfo.ZL,
                    //.selectDyHouse[0].ZL + (dyInfo.selectDyHouse.Count > 1 ? "等" + dyInfo.selectDyHouse.Count + "个" : string.Empty),
                    SQRQ = saveTime,
                    DYLX = dyInfo.DYLX,
                    DYSW = dyInfo.DYSW,
                    DYFS = dyInfo.DYFS,
                    DYMJ = dyInfo.dyMJ,
                    BDBZZQSE = dyInfo.BDBZQSE,
                    PGJE = dyInfo.PGJE,
                    HTH = dyInfo.HTH,
                    LXR = dyInfo.DYLXR,
                    LXRDH = dyInfo.DYLXRDH,
                    CNSJ = dyInfo.CNSJ,
                    //SJR = dyInfo.SJR,
                    FJ = dyInfo.BZ,
                    ZWR = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(p => p.QLRMC).ToArray()),
                    ZWRZJH = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(p => p.ZJHM).ToArray()),
                    ZWRZJLX = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(p => p.ZJLB).Distinct().ToArray()),//改成中文类型
                    DLJGMC = dyInfo.DYQRMC,
                    QLQSSJ = dyInfo.ZWLXQXQSRQ.Date,
                    QLJSSJ = dyInfo.ZWLXQXJZRQ.Date,
                    DYQX = dyInfo.LXQX.ToString(),
                    XID = dyInfo.XID,
                    QT = dyInfo.QT_DY,
                    ZGZQSE = dyInfo.ZGZQSE,
                    DBFW= dyInfo.DBFW,
                    //QRZYQK = dyInfo.QRZYQK
                    SFCZYD = dyInfo.QRZYQK,   //1.是  0.否
                    DYRLX = dyInfo.DYRLX
                };

                string slbhStr = string.Empty;
                if (dyInfo.selectDyHouse.Count > 0)
                {
                    slbhStr = dyInfo.selectDyHouse[0].SLBH + (dyInfo.selectDyHouse.Count > 1 ? "等" + dyInfo.selectDyHouse.Count + "个" : string.Empty);
                }
                //var dyqr = string.Empty;
               REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    XID = dyInfo.XID,
                    YWSLBH = newSLBH,
                    DJZL = SysBdcFlowConst.FLOW_DYDJ_GROUP,
                    BDCZH = dyET.XGZH,
                    SLBH = newSLBH,
                    QLRMC = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(s => s.QLRMC).ToArray()),
                    //dyInfo.selectDyPerson[0].QLRMC
                    ZL = dyET.ZLXX,
                    ORG_ID = dyInfo.BankDeptID,
                    USER_ID = dyInfo.DYQRMC_ID,
                    TEL = dyInfo.DYLXRDH,
                    AUZ_ID = dyInfo.AUZ_ID,
                    HTH = dyInfo.HTH,
                    REMARK1 = "个人抵押",
                    REMARK2 = dyInfo.selectRightPerson[0].QLRMC,
                    SJR = dyInfo.SJR,
                    IS_ACTION_OK = dyInfo.CommandType,
                    PDH = dyInfo.PDH,
                    SAVEDATE = saveTime
                };

                SJD_INFO sjdInfo = new SJD_INFO()
                {
                    SLBH = newSLBH,
                    DJDL = "910",
                    LCLX = dyInfo.LCLX,
                    LCMC = dyInfo.LCMC,
                    ZL = dyInfo.ZL,
                    SJR = dyInfo.SJR,
                    SJSJ = saveTime,
                    CNSJ = dyInfo.CNSJ,
                    QXDM = dyInfo.SZQY,
                    TZRXM = regInfo.QLRMC,
                    TZRDH = regInfo.TEL,
                    XID = regInfo.XID
                };

                QL_XG_INFO qlxgInfo = null;
                if (dyInfo.selectDyHouse != null && dyInfo.selectDyHouse.Count > 0)
                {
                    var maxHouse = dyInfo.selectDyHouse.OrderByDescending(S => S.FWMJ).FirstOrDefault();
                    if (maxHouse != null)
                    {
                        qlxgInfo = this._changeMrgeServices.GetLandHouseRightInfo(maxHouse.BDCZH).Result;
                        if (qlxgInfo != null)
                        {
                            qlxgInfo.XTBH = Provider.Sql.Create().ToString();//权力信息表插入主键
                            qlxgInfo.XID = dyInfo.XID;
                        }
                    }
                    foreach (var hourse in dyInfo.selectDyHouse)
                    {
                        tsglET.Add(new TSGL_INFO()
                        {
                            GLBM = Provider.Sql.Create().ToString(),
                            SLBH = newSLBH,
                            BDCLX = hourse.BDCLX,//房屋或土地
                            TSTYBM = hourse.TSTYBM,
                            BDCDYH = hourse.BDCDYH,
                            DJZL = "抵押",
                            CSSJ = saveTime,
                            XID = dyInfo.XID
                        });
                        xgdjglET.Add(new XGDJGL_INFO()
                        {
                            BGBM = Provider.Sql.Create().ToString(),
                            ZSLBH = newSLBH,
                            FSLBH = hourse.SLBH,
                            BGRQ = saveTime,
                            BGLX = "抵押",
                            XGZLX = hourse.ZSLX,
                            XGZH = hourse.BDCZH,
                            XID = dyInfo.XID
                        });
                    }
                }
                foreach (var person in dyInfo.selectDyPerson)
                {
                    qlrglET.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        YWBM = newSLBH,
                        ZJHM = person.ZJHM,
                        QLRID = person.QLRID,
                        QLRMC = person.QLRMC,
                        //ZJLB = person.ZJLB,
                        ZJLB = person.ZJLB,
                        ZJLB_ZWM = person.ZJLB_ZWM,
                        DH = person.DH,
                        QLRLX = "抵押人",
                        SXH = person.SXH,
                        IS_OWNER = person.IS_OWNER,
                        XID = dyInfo.XID
                    });
                }

                foreach (var rightperson in dyInfo.selectRightPerson)
                {

                    qlrglET.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        YWBM = newSLBH,
                        ZJHM = rightperson.ZJHM,
                        QLRID = Provider.Sql.Create().ToString("N"),
                        QLRMC = rightperson.QLRMC,
                        ZJLB = rightperson.ZJLB,
                        ZJLB_ZWM = rightperson.ZJLB_ZWM,
                        DH = rightperson.DH,
                        QLRLX = "抵押权人",
                        XID = dyInfo.XID
                    });

                    dyInfo.sfd.XID = dyInfo.XID;
                    dyInfo.sfd.SLBH = dyInfo.SLBH;
                    if (dyInfo.sfdList != null && dyInfo.sfdList.Count > 0)
                    {
                        foreach (var sfdDetail in dyInfo.sfdList)
                        {
                            sfdDetail.CWSFDXBBH = Provider.Sql.Create().ToString();
                            sfdDetail.XID = dyInfo.XID;
                            sfdDetail.SLBH = dyInfo.SLBH;
                        }
                    }

                    SysDataRecorderModel saveJson = new SysDataRecorderModel()
                    {
                        PK = dyInfo.JSON_PK,
                        BUS_PK = dyInfo.XID,
                        CDATE = saveTime,
                        USER_ID = dyInfo.DYQRMC_ID,
                        USER_NAME = dyInfo.DYQRMC,
                        IS_STOP = isSubmitWorkFlow ? 1 : 0,
                        SAVEDATAJSON = JsonConvert.SerializeObject(dyInfo),
                        REMARKS1 = string.Join(",", dyInfo.selectDyHouse.Cast<DyHouseVModel>().Select(S => S.BDCZH).ToArray()),//要抵押的不动产证号
                        REMARKS2 = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(S => S.QLRMC).Distinct().ToArray()),
                        REMARKS3 = string.Join(",", dyInfo.selectRightPerson.Cast<DyPersonVModel>().Select(S => S.QLRMC).Distinct().ToArray()),
                        REMARKS4 = string.Empty,
                        REMARKS5 = string.Empty
                    };
                    count = _BdcGeneralMrgeServices.Mortgage(AuzInfo, regInfo, flowInfo, saveJson, tsglET, dyET, null, xgdjglET, qlrglET, attFileModel, dyInfo.sfd, dyInfo.sfdList, sjdInfo, qlxgInfo, isInsert, IsSave != 1, OldXID);
                    return new MessageModel<string>()
                    {
                        msg = "保存成功",
                        success = true,
                        response = @$"{{""XID"":""{dyInfo.XID}"",""AUZ_ID"":""{dyInfo.AUZ_ID}"",""JSON_PK"":""{dyInfo.JSON_PK}""}}"
                    };
                }
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
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<string>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = "{}"
                };
            }
            if (!string.IsNullOrEmpty(newSLBH))
            {
                SysUtility.Delete(this._webHostEnvironment.WebRootPath, newSLBH);
            }
            return new MessageModel<string>()
            {
                msg = "保存失败",
                success = false
            };
        }

        /// <summary>
        /// 不动产审核银行抵押申请审批
        /// </summary>
        /// <param name="strVModel">审批内容信息</param>
        /// <param name="isAauditingPass">是否审核通过,1:通过,0：退回</param>
        /// <returns></returns>
        [HttpPost]
        public MessageModel<string> Auditing([FromForm] string strVModel, [FromForm] int isAauditingPass)
        {
            try
            {
                int count = 0;
                DateTime saveTime = DateTime.Now;
                string saveDataJson = HttpUtility.UrlDecode(strVModel);
                HouseVModel dyVModel = null;
                try
                {
                    dyVModel = JsonConvert.DeserializeObject<HouseVModel>(saveDataJson);
                }
                catch (Exception ex)
                {
                    return new MessageModel<string>()
                    {
                        msg = "数据保存格式错误，请与管理员联系",
                        success = false
                    };
                }

                IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = isAauditingPass == 1 ? SysBdcFlowConst.FLOW_DYDJ_SH : SysBdcFlowConst.FLOW_DYDJ_BANK,
                    PRE_FLOW_ID = SysBdcFlowConst.FLOW_DYDJ_SL_BANK,
                    AUZ_ID = dyVModel.AUZ_ID,
                    CDATE = saveTime,
                    USER_NAME = dyVModel.SJR
                };

                BankAuthorize AuzInfo = new BankAuthorize()
                {
                    BID = dyVModel.AUZ_ID,
                    AUTHORIZATIONDATE = saveTime,
                    STATUS = flowInfo.FLOW_ID,
                    PRE_STATUS = SysBdcFlowConst.FLOW_DYDJ_SL_BANK
                };

                SysDataRecorderModel saveJson = new SysDataRecorderModel()
                {
                    BUS_PK = dyVModel.XID,
                    SAVEDATAJSON = JsonConvert.SerializeObject(dyVModel),
                };

                SPB_INFO spInfo = new SPB_INFO()
                {
                    SPBH = Provider.Sql.Create().ToString(),
                    SLBH = dyVModel.SLBH,
                    SPDX = "初审意见",
                    SPYJ = dyVModel.SPYJ,
                    SPR = dyVModel.SPR,
                    SPRQ = saveTime,
                    SPTXR = dyVModel.SPR,
                    XID = dyVModel.XID,
                    SPBZ= dyVModel.SPBZ
                };

                DY_INFO dyInfo = new DY_INFO()
                {
                    XID = dyVModel.XID,
                    SPBZ = dyVModel.SPBZ
                };
                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    IS_ACTION_OK = dyVModel.CommandType,
                    XID = dyVModel.XID
                };
                count = _BankGeneralMrgeServices.Auditing(AuzInfo, regInfo, saveJson, spInfo, flowInfo, dyInfo);
                return new MessageModel<string>()
                {
                    msg = "获取成功",
                    success = true,
                    response = @$"{{""XID"":""{dyVModel.XID}"",""AUZ_ID"":""{dyVModel.AUZ_ID}"",""JSON_PK"":""{dyVModel.JSON_PK}""}}"
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
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"BankGeneralMrgeController.Auditing:【错误代码：{logErrorCode},原因:{ex.Message}】";
                //this._logger.LogDebug(errorLog);
                return new MessageModel<string>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = "{}"
                };
            }
        }
    }
}
