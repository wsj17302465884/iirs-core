using IIRS.IRepository;
using IIRS.IServices;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Utilities.Common;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RT.Comb;
using Spire.Pdf;
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
    public class BdcMrgeReleaseController : ControllerBase
    {
        private readonly ILogger<BdcMrgeReleaseController> _logger;
        readonly IWebHostEnvironment _webHostEnvironment;
        readonly IBdcMrgeReleaseServices _bdcMrgeReleaseServices;
        private readonly ICommonServices _CommonServices;
        private readonly IChangeMrgeServices _changeMrgeServices;
        public BdcMrgeReleaseController(ICommonServices commonServices, ILogger<BdcMrgeReleaseController> logger, IBdcMrgeReleaseServices bdcMrgeReleaseServices, IWebHostEnvironment webHostEnvironment, IChangeMrgeServices changeMrgeServices)
        {
            this._CommonServices = commonServices;
            this._logger = logger;
            this._bdcMrgeReleaseServices = bdcMrgeReleaseServices;
            this._webHostEnvironment = webHostEnvironment;
            this._changeMrgeServices = changeMrgeServices;
        }

        /// <summary>
        /// 生成《不动产抵押登记申请表》
        /// </summary>
        /// <param name="xid">业务编号</param>
        /// <returns>base64格式PDF文件</returns>
        [HttpGet]
        public MessageModel<string> PrintApplyData(string xid)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            try
            {
                // 生成PDF
                var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "不动产抵押登记申请表.pdf");

                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(TemplateFile);
                this._bdcMrgeReleaseServices.SetPrintApplyData(doc, xid);
                Stream stream = new MemoryStream();
                doc.SaveToStream(stream);

                byte[] arr = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(arr, 0, (int)stream.Length);
                stream.Close();
                string pdfBase64 = "data:application/pdf;base64," + Convert.ToBase64String(arr);

                return new MessageModel<string>()
                {
                    msg = "打印成功",
                    success = true,
                    response = pdfBase64
                };
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<string>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false
                };
            }
        }

        /// <summary>
        /// 生成《不动产抵押登记审批表》
        /// </summary>
        /// <param name="xid">业务编号</param>
        /// <returns>base64格式PDF文件</returns>
        [HttpGet]
        public MessageModel<string> PrintApproveData(string xid)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            try
            {
                // 生成PDF
                var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "抵押注销审批表.pdf");

                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(TemplateFile);
                this._bdcMrgeReleaseServices.SetPrintApproveData(doc, xid);
                Stream stream = new MemoryStream();
                doc.SaveToStream(stream);

                byte[] arr = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(arr, 0, (int)stream.Length);
                stream.Close();
                string pdfBase64 = "data:application/pdf;base64," + Convert.ToBase64String(arr);

                return new MessageModel<string>()
                {
                    msg = "打印成功",
                    success = true,
                    response = pdfBase64
                };
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<string>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false
                };
            }
        }

        /// <summary>
        /// 查询抵押项目登记信息
        /// </summary>
        /// <param name="DY_SLBH">抵押受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<MrgeReleaseVModel>> GetMrgeCertHouseInfo(string DY_SLBH)
        {
            try
            {
                var data = await this._bdcMrgeReleaseServices.GetMrgeCertHouseInfo(DY_SLBH);
                return new MessageModel<MrgeReleaseVModel>()
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
                return new MessageModel<MrgeReleaseVModel>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false
                };
            }
        }

        /// <summary>
        /// 查询抵押证明信息
        /// </summary>
        /// <param name="BDCDYH">不动产单元号</param>
        /// <param name="BDCZMH">不动产证明号</param>
        /// <param name="QLRMC">权利人名称</param>
        /// <param name="ZL">坐落</param>
        /// <param name="DY_SLBH">抵押受理编号</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        [HttpGet]
        public async Task<MessageModel<PageStringModel>> GetMrgeCertInfo(string BDCDYH, string BDCZMH, string QLRMC, string ZL, string DY_SLBH, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var data = await this._bdcMrgeReleaseServices.GetMrgeCertInfo(BDCDYH, BDCZMH, QLRMC, ZL, DY_SLBH ,pageIndex, pageSize);
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
                        msg = "数据保存格式错误，请与管理员联系",
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
                catch
                {
                    return new MessageModel<string>()
                    {
                        msg = "附件数据保存格式错误，请与管理员联系",
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
                    releaseInfo.SJSJ = saveTime;
                }
                else//否则为修改，就存在退回修改或直接修改情况
                {
                    var flowResult = this._CommonServices.FlowInfoQuery(releaseInfo.XID).Result;
                    preFlowID = flowResult.CURRENT_STATUS;
                    if (flowResult == null || flowResult.SLBH != newSLBH || flowResult.CURRENT_STATUS != SysBdcFlowConst.FLOW_DYZX_SL)
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
                //{
                //    BID = releaseInfo.AUZ_ID,
                //    AUTHORIZATIONDATE = saveTime,
                //    STATUS = SysBdcFlowConst.FLOW_DYZX_SH,
                //    PRE_STATUS = preFlowID.HasValue ? preFlowID : SysBdcFlowConst.FLOW_DYZX_SL
                //};
                IFLOW_DO_ACTION flowInfo = null;
                //{
                //    PK = Provider.Sql.Create().ToString(),
                //    FLOW_ID = SysBdcFlowConst.FLOW_DYZX_SH,
                //    PRE_FLOW_ID = AuzInfo.PRE_STATUS,
                //    AUZ_ID = releaseInfo.AUZ_ID,
                //    CDATE = saveTime,
                //    USER_NAME = releaseInfo.SJR
                //};

                if (releaseInfo.CommandType == 0)//如果是暂存
                {
                    if (isInsert)//并且是新办件,
                    {
                        AuzInfo = new BankAuthorize()
                        {
                            BID = releaseInfo.AUZ_ID,
                            AUTHORIZATIONDATE = saveTime,
                            STATUS = SysBdcFlowConst.FLOW_DYZX_SL,
                            PRE_STATUS = null
                        };

                        flowInfo = new IFLOW_DO_ACTION()
                        {
                            PK = Provider.Sql.Create().ToString(),
                            FLOW_ID = SysBdcFlowConst.FLOW_DYZX_SL,
                            PRE_FLOW_ID = AuzInfo.PRE_STATUS,
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
                        STATUS = SysBdcFlowConst.FLOW_DYZX_SH,
                        PRE_STATUS = SysBdcFlowConst.FLOW_DYZX_SL
                    };

                    flowInfo = new IFLOW_DO_ACTION()
                    {
                        PK = Provider.Sql.Create().ToString(),
                        FLOW_ID = SysBdcFlowConst.FLOW_DYZX_SH,
                        PRE_FLOW_ID = AuzInfo.PRE_STATUS,
                        AUZ_ID = releaseInfo.AUZ_ID,
                        CDATE = saveTime,
                        USER_NAME = releaseInfo.SJR
                    };
                }

                List<TSGL_INFO> tsglET = new List<TSGL_INFO>();
                List<XGDJGL_INFO> xgdjglET = new List<XGDJGL_INFO>();
                List<QLRGL_INFO> qlrglET = new List<QLRGL_INFO>();
                
                
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
                    SQBZ= releaseInfo.BZ,
                    SPBZ = releaseInfo.SPBZ,
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
                    SAVEDATE = saveTime,
                    IS_ACTION_OK = releaseInfo.CommandType

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

                SPB_INFO spInfo = new SPB_INFO()
                {
                    SPBH = Provider.Sql.Create().ToString(),
                    SLBH = newSLBH,
                    SPDX = "初审意见",
                    SPYJ = releaseInfo.SPYJ,
                    SPR = releaseInfo.SPR,
                    SPRQ = saveTime,
                    SPTXR = releaseInfo.SPR,
                    XID = releaseInfo.XID,
                    SPBZ = releaseInfo.SPBZ
                };

                count = _bdcMrgeReleaseServices.MortgageRelease(isInsert, AuzInfo, regInfo, saveJson, zxInfo, spInfo, flowInfo, tsglET, xgdjglET, qlrglET, sjdInfo, qlxgInfo, attFileModel, OldXID);
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
    }
}
