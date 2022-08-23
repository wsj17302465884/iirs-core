using IIRS.IRepository;
using IIRS.IRepository.IIRS;
using IIRS.IServices;
using IIRS.IServices.Bank;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Utilities.Common;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RT.Comb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IIRS.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    public class BankMrgeReleaseController : ControllerBase
    {
        private readonly ILogger<BdcMrgeReleaseController> _logger;
        readonly IWebHostEnvironment _webHostEnvironment;
        readonly IBdcMrgeReleaseServices _bdcMrgeReleaseServices;
        readonly IBankMrgeReleaseServices _bankMrgeReleaseServices;
        private readonly ICommonServices _CommonServices;
        private readonly IChangeMrgeServices _changeMrgeServices;
        private readonly IBus_visit_logRepository _Ibus_visit_logRepository;

        public BankMrgeReleaseController(ICommonServices commonServices, ILogger<BdcMrgeReleaseController> logger, IBankMrgeReleaseServices bankMrgeReleaseServices, IBdcMrgeReleaseServices bdcMrgeReleaseServices, IWebHostEnvironment webHostEnvironment, IChangeMrgeServices changeMrgeServices, IBus_visit_logRepository bus_visit_logRepository)
        {
            this._CommonServices = commonServices;
            this._logger = logger;
            this._bdcMrgeReleaseServices = bdcMrgeReleaseServices;
            this._bankMrgeReleaseServices = bankMrgeReleaseServices;
            this._webHostEnvironment = webHostEnvironment;
            this._changeMrgeServices = changeMrgeServices;
            this._Ibus_visit_logRepository = bus_visit_logRepository;
        }

        /// <summary>
        /// 查询要抵押不动产信息
        /// </summary>
        /// <param name="BDCZMH">不动产证明号</param>
        /// <param name="DYRMC">抵押人名称</param>
        /// <param name="USER_ID">操作员ID</param>
        /// <param name="USER_NAME">操作员名称</param>
        /// <param name="Bank_ID">抵押权人(银行)编码</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        [HttpGet]
        public MessageModel<PageModel<MrgeCertInfoVModel>> GetMrgeCertInfo(string BDCZMH, string DYRMC, string USER_ID, string USER_NAME, string Bank_ID, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                BUS_VISIT_LOG Lmodel = new BUS_VISIT_LOG();
                Lmodel.PK = Guid.NewGuid().ToString();
                Lmodel.GROUP_ID = 1;
                Lmodel.PARAMS = $"不动产证明号：{ BDCZMH}, 抵押人名称：{ DYRMC}";
                if (Lmodel.PARAMS.Length >= 500)
                {
                    Lmodel.PARAMS = Lmodel.PARAMS.Substring(0, 498)+"...";
                }
                Lmodel.ORGANIZATION_ID =Bank_ID;
                Lmodel.USER_ID = USER_ID;
                Lmodel.USER_NAME = USER_NAME;
                Lmodel.VDATE = DateTime.Now;
                var Logdata = this._Ibus_visit_logRepository.Add(Lmodel);
                var data = this._bankMrgeReleaseServices.GetMrgeCertInfo(BDCZMH, DYRMC, Bank_ID, pageIndex, pageSize);
                return new MessageModel<PageModel<MrgeCertInfoVModel>>()
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
                return new MessageModel<PageModel<MrgeCertInfoVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false
                };
            }
        }

        /// <summary>
        /// 银行提交不动产抵押注销审核
        /// </summary>
        /// <param name="strMrgeReleaseVModel">审核内容</param>
        /// <param name="strFileTree">提交附件</param>
        /// <returns></returns>
        [HttpPost]
        public MessageModel<string> Post([FromForm] string strMrgeReleaseVModel, [FromForm] string strFileTree)
        {
            try
            {
                int count = 0;
                DateTime saveTime = DateTime.Now;
                string saveDataJson = HttpUtility.UrlDecode(strMrgeReleaseVModel);
                BdcMrgeReleaseVModel releaseInfo = null;
                try
                {
                    releaseInfo = JsonConvert.DeserializeObject<BdcMrgeReleaseVModel>(saveDataJson);
                }
                catch (Exception ex)
                {
                    return new MessageModel<string>()
                    {
                        msg = "数据保存格式错误，原因：" + ex.Message,
                        success = false
                    };
                }
                string newSLBH = releaseInfo.SLBH;//服务器端获取受理编号
                List<Base64FilesVModel> fileTree = null;
                List<PUB_ATT_FILE> attFileModel = null;
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
                releaseInfo.SJSJ = saveTime;
                string OldXID = string.Empty;
                bool isInsert = string.IsNullOrEmpty(releaseInfo.XID);
                decimal? preFlowID = null;
                if (isInsert)
                {
                    releaseInfo.XID = Provider.Sql.Create().ToString();//流程实力现实手主键
                    releaseInfo.AUZ_ID = Provider.Sql.Create().ToString();//流程实例主键
                    releaseInfo.JSON_PK = Provider.Sql.Create().ToString();//POST表json原始数据主键
                }
                else//否则为修改，就存在退回修改或直接修改情况
                {
                    var flowResult = this._CommonServices.FlowInfoQuery(releaseInfo.XID).Result;
                    preFlowID = flowResult.CURRENT_STATUS;
                    if (flowResult == null || flowResult.SLBH != newSLBH || flowResult.CURRENT_STATUS != SysBdcFlowConst.FLOW_DYZX_BANK)
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
                        OldXID = releaseInfo.XID;//记录作废XID，并更细历史手信息
                        releaseInfo.XID = Provider.Sql.Create().ToString();//流程实力现实手主键
                        releaseInfo.JSON_PK = Provider.Sql.Create().ToString();//POST表json原始数据主键
                        releaseInfo.AUZ_ID = flowResult.AUZ_ID;
                    }
                    else//否则,正常修改
                    {

                    }
                }
                #region 提取附件数据，提出树形结构数据
                int imgCount = 0;
                if (fileTree != null)
                {
                    imgCount = fileTree.Where(S => S.children.Count > 0).Count();
                    if (imgCount > 0)
                    {
                        attFileModel = SysUtility.UploadSysBase64File(this._webHostEnvironment.WebRootPath, newSLBH, fileTree);
                        if (attFileModel != null && attFileModel.Count == 0 && releaseInfo.CommandType == 1)
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
                                file.XID = releaseInfo.XID;
                            }
                        }
                    }
                }
                #endregion
                BankAuthorize AuzInfo = null;
                IFLOW_DO_ACTION flowInfo = null;
                if (releaseInfo.CommandType == 0)//如果是暂存
                {
                    if (isInsert)//并且是新办件
                    {
                        AuzInfo = new BankAuthorize()
                        {
                            BID = releaseInfo.AUZ_ID,
                            AUTHORIZATIONDATE = saveTime,
                            STATUS = SysBdcFlowConst.FLOW_DYZX_BANK,
                            PRE_STATUS = null
                        };

                        flowInfo = new IFLOW_DO_ACTION()
                        {
                            PK = Provider.Sql.Create().ToString(),
                            FLOW_ID = SysBdcFlowConst.FLOW_DYZX_BANK,
                            PRE_FLOW_ID = null,
                            AUZ_ID = releaseInfo.AUZ_ID,
                            CDATE = saveTime,
                            USER_NAME = releaseInfo.SJR
                        };
                    }
                }
                else
                {
                    AuzInfo = new BankAuthorize()
                    {
                        BID = releaseInfo.AUZ_ID,
                        AUTHORIZATIONDATE = saveTime,
                        STATUS = SysBdcFlowConst.FLOW_DYZX_SL_BANK,
                        PRE_STATUS = preFlowID
                    };

                    flowInfo = new IFLOW_DO_ACTION()
                    {
                        PK = Provider.Sql.Create().ToString(),
                        FLOW_ID = SysBdcFlowConst.FLOW_DYZX_SL_BANK,
                        PRE_FLOW_ID = preFlowID,
                        AUZ_ID = releaseInfo.AUZ_ID,
                        CDATE = saveTime,
                        USER_NAME = releaseInfo.SJR
                    };
                }

                //BankAuthorize AuzInfo = new BankAuthorize()
                //{
                //    BID = releaseInfo.AUZ_ID,
                //    STATUS = SysBdcFlowConst.FLOW_DYZX_SL_BANK,
                //    PRE_STATUS = preFlowID
                //};

                List<TSGL_INFO> tsglET = new List<TSGL_INFO>();
                List<XGDJGL_INFO> xgdjglET = new List<XGDJGL_INFO>();
                List<QLRGL_INFO> qlrglET = new List<QLRGL_INFO>();

                //IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                //{
                //    PK = Provider.Sql.Create().ToString(),
                //    FLOW_ID = SysBdcFlowConst.FLOW_DYZX_SL_BANK,
                //    PRE_FLOW_ID = null,
                //    AUZ_ID = releaseInfo.AUZ_ID,
                //    CDATE = saveTime,
                //    USER_NAME = releaseInfo.SJR
                //};
                string bdczmh = string.Empty;//不动产证明号

                bdczmh = releaseInfo.selectHouse.BDCZH;
                xgdjglET.Add(new XGDJGL_INFO()
                {
                    BGBM = Provider.Sql.Create().ToString(),
                    ZSLBH = newSLBH,
                    FSLBH = releaseInfo.selectHouse.SLBH,
                    BGRQ = saveTime,
                    BGLX = "抵押注销",
                    XGZLX = releaseInfo.selectHouse.ZSLX,
                    XGZH = releaseInfo.selectHouse.BDCZH,
                    PID = "",
                    XID = releaseInfo.XID
                });
                QL_XG_INFO qlxgInfo = null;
                foreach (var sun in releaseInfo.selectHouse.children)
                {
                    if (qlxgInfo == null && !string.IsNullOrEmpty(sun.BDCZH))
                    {
                        qlxgInfo = this._changeMrgeServices.GetLandHouseRightInfo(sun.BDCZH).Result;
                        if (qlxgInfo != null)
                        {
                            qlxgInfo.XTBH = Provider.Sql.Create().ToString();//权力信息表插入主键
                            qlxgInfo.XID = releaseInfo.XID;
                        }
                    }
                    tsglET.Add(new TSGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        BDCLX = sun.BDCLX,//房屋或土地
                        TSTYBM = sun.TSTYBM,
                        BDCDYH = sun.BDCDYH,
                        DJZL = "抵押注销",
                        CSSJ = saveTime,
                        XID = releaseInfo.XID
                    });
                    xgdjglET.Add(new XGDJGL_INFO()
                    {
                        BGBM = Provider.Sql.Create().ToString(),
                        ZSLBH = newSLBH,
                        //FSLBH = sun.SLBH,
                        FSLBH = sun.SLBH,
                        BGRQ = saveTime,
                        BGLX = "抵押注销",
                        XGZLX = sun.ZSLX,
                        XGZH = sun.BDCZH,
                        PID = releaseInfo.selectHouse.SLBH,
                        XID = releaseInfo.XID
                    });
                }

                XGDJZX_INFO zxInfo = new XGDJZX_INFO()
                {
                    SLBH = newSLBH,
                    XGZH = bdczmh,
                    BDCDYH = releaseInfo.selectHouse.BDCDYH,
                    DJLX = "注销登记",
                    DJYY = releaseInfo.ZXYY,
                    DLRXM = releaseInfo.DYZXLXR,
                    DLJGMC = releaseInfo.DYQRMC,
                    SQBZ = releaseInfo.BZ,
                    //SPBZ = releaseInfo.SPBZ,
                    SQRQ = saveTime,
                    SJR = releaseInfo.SJR,
                    XID = releaseInfo.XID
                };
                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    XID = releaseInfo.XID,
                    YWSLBH = newSLBH,
                    SLBH = newSLBH,
                    DJZL = SysBdcFlowConst.FLOW_DYZX_GROUP,
                    ZL = releaseInfo.ZL,
                    ORG_ID = releaseInfo.BankDeptID,
                    USER_ID = releaseInfo.DYQRMC_ID,
                    TEL = releaseInfo.DYZXLXRDH,
                    HTH = releaseInfo.HTH,
                    BDCZH = bdczmh,
                    QLRMC = string.Join(",", releaseInfo.selectPerson.Cast<DyPersonVModel>().OrderBy(S => S.SXH).Select(S => S.QLRMC).ToArray()),
                    REMARK1 = "抵押注销",
                    AUZ_ID = releaseInfo.AUZ_ID,
                    PDH = releaseInfo.PDH,
                    IS_ACTION_OK = releaseInfo.CommandType
                    //SaveDataJson = saveDataJson
                };
                SJD_INFO sjdInfo = new SJD_INFO()
                {
                    SLBH = newSLBH,
                    DJDL = "400",
                    LCLX = releaseInfo.LCLX,
                    LCMC = releaseInfo.LCMC,
                    ZL = releaseInfo.ZL,
                    SJR = releaseInfo.SJR,
                    SJSJ = saveTime,
                    CNSJ = releaseInfo.CNSJ,
                    QXDM = releaseInfo.SZQY,
                    TZRXM = regInfo.QLRMC,
                    TZRDH = regInfo.TEL,
                    XID = regInfo.XID
                };

                foreach (var person in releaseInfo.selectPerson)
                {
                    qlrglET.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        YWBM = newSLBH,
                        ZJHM= person.ZJHM,
                        QLRID = person.QLRID?? Provider.Sql.Create().ToString("N"),
                        QLRMC= person.QLRMC,
                        ZJLB = person.ZJLB,
                        ZJLB_ZWM = person.ZJLB_ZWM,
                        DH = person.DH,
                        QLRLX = "抵押人",
                        SXH= person.SXH,
                        XID = releaseInfo.XID
                    });
                }
                qlrglET.Add(new QLRGL_INFO()
                {
                    GLBM = Provider.Sql.Create().ToString(),
                    SLBH = newSLBH,
                    YWBM = newSLBH,
                    ZJHM = releaseInfo.YHYTSHXYDM,
                    QLRID = Provider.Sql.Create().ToString("N"),
                    QLRMC = releaseInfo.DYQRMC,
                    ZJLB = "8",
                    ZJLB_ZWM = "统一社会信用代码",
                    DH = releaseInfo.DYZXLXRDH,
                    QLRLX = "抵押权人",
                    XID = releaseInfo.XID
                });

                SysDataRecorderModel saveJson = new SysDataRecorderModel()
                {
                    PK = releaseInfo.JSON_PK,
                    BUS_PK = releaseInfo.XID,
                    CDATE = saveTime,
                    USER_ID = releaseInfo.DYQRMC_ID,
                    USER_NAME = releaseInfo.DYQRMC,
                    IS_STOP = releaseInfo.CommandType,
                    SAVEDATAJSON = JsonConvert.SerializeObject(releaseInfo),
                    REMARKS1 = releaseInfo.selectHouse.BDCZH,//不动产证明号
                    REMARKS2 = string.Join(",", releaseInfo.selectPerson.Cast<DyPersonVModel>().Select(S => S.QLRMC).Distinct().ToArray()),
                    REMARKS3 = string.Join(",", releaseInfo.selectRightPerson.Cast<DyPersonVModel>().Select(S => S.QLRMC).Distinct().ToArray()),
                    REMARKS4 = string.Empty,
                    REMARKS5 = string.Empty
                };
                count = _bdcMrgeReleaseServices.MortgageRelease(isInsert, AuzInfo, regInfo, saveJson, zxInfo, null, flowInfo, tsglET, xgdjglET, qlrglET, sjdInfo, qlxgInfo, attFileModel, OldXID);
                return new MessageModel<string>()
                {
                    msg = "获取成功",
                    success = true,
                    response = @$"{{""XID"":""{releaseInfo.XID}"",""AUZ_ID"":""{releaseInfo.AUZ_ID}"",""JSON_PK"":""{releaseInfo.JSON_PK}""}}"
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
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<string>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = "{}"
                };
            }
        }

        /// <summary>
        /// 不动产审核银行抵押注销申请审批
        /// </summary>
        /// <param name="strMrgeReleaseVModel">审批内容信息</param>
        /// <param name="isAauditingPass">是否审核通过,1:通过,0：退回</param>
        /// <returns></returns>
        [HttpPost]
        public MessageModel<string> Auditing([FromForm] string strMrgeReleaseVModel, [FromForm] int isAauditingPass)
        {
            try
            {
                int count = 0;
                DateTime saveTime = DateTime.Now;
                string saveDataJson = HttpUtility.UrlDecode(strMrgeReleaseVModel);
                BdcMrgeReleaseVModel releaseInfo = null;
                
                try
                {
                    releaseInfo = JsonConvert.DeserializeObject<BdcMrgeReleaseVModel>(saveDataJson);
                }
                catch (Exception ex)
                {
                    return new MessageModel<string>()
                    {
                        msg = "数据保存格式错误，请与管理员联系",
                        success = false
                    };
                }
                
                var flowResult = this._CommonServices.FlowInfoQuery(releaseInfo.XID).Result;
                var preFlowID = flowResult.CURRENT_STATUS;
                
                IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = isAauditingPass == 1 ? SysBdcFlowConst.FLOW_DYZX_SH : SysBdcFlowConst.FLOW_DYZX_BANK,
                    PRE_FLOW_ID = Convert.ToInt32(preFlowID),
                    AUZ_ID = releaseInfo.AUZ_ID,
                    CDATE = saveTime,
                    USER_NAME = releaseInfo.SJR
                };

                BankAuthorize AuzInfo = new BankAuthorize()
                {
                    BID = releaseInfo.AUZ_ID,
                    AUTHORIZATIONDATE = saveTime,
                    STATUS = flowInfo.FLOW_ID,
                    PRE_STATUS = Convert.ToInt32(preFlowID)
                };
                
                if (isAauditingPass == 0)
                {
                    releaseInfo.CommandType = 2;
                }
                else
                {
                    releaseInfo.CommandType = 1;
                }
                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    IS_ACTION_OK = releaseInfo.CommandType,
                    XID = releaseInfo.XID
                };
                SysDataRecorderModel saveJson = new SysDataRecorderModel()
                {
                    BUS_PK = releaseInfo.XID,
                    SAVEDATAJSON = JsonConvert.SerializeObject(releaseInfo),
                };

                SPB_INFO spInfo = new SPB_INFO()
                {
                    SPBH = Provider.Sql.Create().ToString(),
                    SLBH = releaseInfo.SLBH,
                    SPDX = "初审意见",
                    SPYJ = releaseInfo.SPYJ,
                    SPR = releaseInfo.SPR,
                    SPRQ = saveTime,
                    SPTXR = releaseInfo.SPR,
                    XID = releaseInfo.XID,
                    SPBZ= releaseInfo.SPBZ
                };

                XGDJZX_INFO zxInfo = new XGDJZX_INFO()
                {
                    XID = releaseInfo.XID,
                    SPRQ = DateTime.Now,
                    SPBZ = releaseInfo.SPBZ
                };

                count = _bankMrgeReleaseServices.Auditing(AuzInfo, regInfo, saveJson, spInfo, flowInfo, zxInfo);
                return new MessageModel<string>()
                {
                    msg = "获取成功",
                    success = true,
                    response = @$"{{""XID"":""{releaseInfo.XID}"",""AUZ_ID"":""{releaseInfo.AUZ_ID}"",""JSON_PK"":""{releaseInfo.JSON_PK}""}}"
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
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
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
