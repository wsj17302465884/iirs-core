using IIRS.IRepository;
using IIRS.IRepository.IIRS;
using IIRS.IServices;
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
    /// 登录控制器
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    public class BdcGeneralMrgeController : ControllerBase
    {
        private readonly ILogger<BdcGeneralMrgeController> _logger;
        private readonly IBdcGeneralMrgeServices _BdcGeneralMrgeServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IRegistration_infoRepository _registration;
        private readonly ICommonServices _CommonServices;
        private readonly IChangeMrgeServices _changeMrgeServices;
        public BdcGeneralMrgeController(ICommonServices commonServices, ILogger<BdcGeneralMrgeController> logger, IRegistration_infoRepository registration, IWebHostEnvironment webHostEnvironment, IBdcGeneralMrgeServices bdcGeneralMrgeServices, IChangeMrgeServices changeMrgeServices)
        {
            this._CommonServices = commonServices;
            this._logger = logger;
            this._BdcGeneralMrgeServices = bdcGeneralMrgeServices;
            this._webHostEnvironment = webHostEnvironment;
            this._registration = registration;
            this._changeMrgeServices = changeMrgeServices;
        }
    
        /// <summary>
        /// 查询房屋信息
        /// </summary>
        /// <param name="BDCZH">不动产证号</param>
        /// <param name="BDCLX">不动产类型(宗地、房屋,否则去除该条件)</param>
        /// <param name="QLRMC">权利人名称</param>
        /// <param name="ZL">坐落</param>
        /// <param name="DJB_SLBH">(登记簿)受理编号</param>
        /// <param name="BDCDYH">不动产单元号</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        [HttpGet]
        public async Task<MessageModel<PageStringModel>> GetBdcHourseInfo(string BDCZH, string BDCLX, string QLRMC, string ZL, string DJB_SLBH, string BDCDYH, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var data = await this._BdcGeneralMrgeServices.GetBdcHourseInfo(BDCZH, BDCLX, QLRMC, ZL, DJB_SLBH, BDCDYH, pageIndex, pageSize);
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
        /// 抵押勘误查询
        /// </summary>
        /// <param name="BDCDYH">不动产单元号</param>
        /// <param name="BDCZMH">不动产证号</param>
        /// <param name="SLBH">(登记簿)受理编号</param>
        /// <param name="BDCLX">不动产类型(宗地、房屋,否则去除该条件)</param>
        /// <param name="DYRMC">抵押人名称</param>
        /// <param name="ZL">坐落</param>
        /// <param name="YWRMC">义务人名称</param>
        /// <param name="ZSLX">证书类型</param>
        /// <param name ="LIFE"> 数据状态 </param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        [HttpGet]
        public async Task<MessageModel<PageStringModel>> GetBdcCorrigendumInfo(string BDCDYH,string BDCZMH,string SLBH,string BDCLX,string DYRMC,string ZL,string YWRMC,string ZSLX,string LIFE ,int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var data = await this._BdcGeneralMrgeServices.GetBdcCorrigendumInfo(BDCDYH, BDCZMH, SLBH, BDCLX, DYRMC, ZL, YWRMC, ZSLX, LIFE, pageIndex, pageSize);
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
        /// 查询抵押项目勘误登记信息
        /// </summary>
        /// <param name="DY_SLBH">抵押受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<KwHouseVModel>> GetMrgeCertHouseInfo(string DY_SLBH)
        {
            try
            {
                var data = await this._BdcGeneralMrgeServices.GetMrgeCertHouseInfo(DY_SLBH);
                return new MessageModel<KwHouseVModel>()
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
                return new MessageModel<KwHouseVModel>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false
                };
            }
        }
        /// <summary>
        /// 查询不动产权利人
        /// </summary>
        /// <param name="djbSLBH">登记簿受理编号(逗号分隔)</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<QLR_VModel>>> GetBdcQlrInfo(string djbSLBH)
        {
            try
            {
                string[] slbh = djbSLBH.Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (slbh.Length>0)
                {
                    var data = await this._BdcGeneralMrgeServices.GetBdcQlrInfo(slbh);
                    return new MessageModel<List<QLR_VModel>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<QLR_VModel>>()
                    {
                        msg = "查询无记录",
                        success = true
                    };
                }
            }
            catch (Exception ex)
            {

                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<QLR_VModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        [HttpGet]
        public async Task<MessageModel<REGISTRATION_INFO>> GetRegistrationByXid(string XID)
        {
            try
            {
                var data = await this._registration.QueryById(XID);
                return new MessageModel<REGISTRATION_INFO>()
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
                return new MessageModel<REGISTRATION_INFO>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
            
        }

        /// <summary>
        /// 查询附件
        /// </summary>
        /// <param name="XID">业务信息表XID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<El_CascaderTree>>> MediasQueryByPK(string XID)
        {
            List<El_CascaderTree> data = null;
            if (!string.IsNullOrEmpty(XID))
            {
                try
                {
                    var fileList = await _BdcGeneralMrgeServices.UploadFileQueryByXID(XID);
                    if (fileList.Count > 0)
                    {
                        data = new List<El_CascaderTree>();
                        var groups = fileList.Cast<PUB_ATT_FILE>().Select(s => new { GROUP_ID = s.GROUP_ID, GROUP_NAME = s.GROUP_NAME }).Distinct();
                        El_CascaderTree treeData = null;
                        foreach (var group in groups)
                        {
                            treeData = new El_CascaderTree()
                            {
                                label = group.GROUP_NAME,
                                value = group.GROUP_ID
                            };
                            var result = fileList.Where(s => s.GROUP_ID == treeData.value).OrderBy(e => e.ODR);
                            if (result.Count() > 0)
                            {
                                treeData.children = new List<El_CascaderTree>();
                                foreach (var value in result)
                                {

                                    treeData.children.Add(new El_CascaderTree()
                                    {
                                        label = value.DISPLAY_NAME,
                                        value = this.HttpContext.Request.Host.Value + value.PATH + value.FILE_NAME
                                    });
                                }
                            }
                            data.Add(treeData);
                        }

                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return new MessageModel<List<El_CascaderTree>>()
            {
                msg = "查询成功",
                success = true,
                response = data
            };
        }

        /// <summary>
        /// 上传附件信息
        /// </summary>
        /// <param name="xid"></param>
        /// <param name="slbh"></param>
        /// <param name="strFileTree"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<int>> UpdateFile([FromForm] string xid, [FromForm] string slbh, [FromForm] string strFileTree)
        {
            try
            {
                List<Base64FilesVModel> fileTree = JsonConvert.DeserializeObject<List<Base64FilesVModel>>(strFileTree);
                int imgCount = fileTree.Where(S => S.children.Count > 0).Count();
                if (imgCount > 0)
                {
                    var attFileModel = SysUtility.UploadSysBase64File(this._webHostEnvironment.WebRootPath, slbh, fileTree);
                    int count = await _BdcGeneralMrgeServices.UpdateFile(attFileModel, xid);
                    return new MessageModel<int>()
                    {
                        msg = "数据保存成功！",
                        success = true,
                        response = count
                    };
                }
                else
                {
                    //if (slbh.Length == 12)
                    //{
                    //    string strSaveDay = slbh.Substring(0, 8);
                    //}
                    //202009228002
                    return new MessageModel<int>()
                    {
                        msg = "数据保存成功！",
                        success = true,
                        response = 0
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<int>()
                {
                    msg = $"错误，原因：{ex.Message}！",
                    success = false,
                    response = 0
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"BdcGeneralMrgeController.UpdateFile:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<int>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = 0
                };
            }

        }


        /// <summary>
        /// 保存不动产一般抵押信息
        /// </summary>
        /// <param name="IsSave"></param>
        /// <param name="strDYVModel">保存抵押信息</param>
        /// <param name="strFileTree">附件数据</param>
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
                        msg = "数据保存格式错误，请于管理员联系",
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
                catch
                {
                    return new MessageModel<string>()
                    {
                        msg = "附件数据保存格式错误，请与管理员联系",
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
                    dyInfo.SJSJ = saveTime;
                }
                else//否则为修改 存在退回修改或直接修改情况
                {
                    var flowResult = this._CommonServices.FlowInfoQuery(dyInfo.XID).Result;
                    preFlowID = flowResult.CURRENT_STATUS;
                    if (flowResult == null || flowResult.CURRENT_STATUS != SysBdcFlowConst.FLOW_DYDJ_SL)
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
                }
                #region 提取附件数据，提出树形结构数据
                List<PUB_ATT_FILE> attFileModel = null;
                if (dyInfo.CommandType == 1 && fileTree == null)
                {
                    return new MessageModel<string>()
                    {
                        msg = "请上传附件信息!",
                        success = false
                    };
                }
                if (fileTree != null)
                {
                    int imgCount = fileTree.Where(S => S.children.Count > 0).Count();
                    if (imgCount > 0)
                    {
                        attFileModel = SysUtility.UploadSysBase64File(this._webHostEnvironment.WebRootPath, newSLBH, fileTree);
                        foreach (var file in attFileModel)
                        {
                            file.XID = dyInfo.XID;
                        }
                    }
                    else if (dyInfo.CommandType == 1)//提交保存附件不能为空
                    {
                        return new MessageModel<string>()
                        {
                            msg = "请上传附件信息!",
                            success = false
                        };
                    }
                }
                #endregion
                //strDYVModel = HttpUtility.UrlDecode(strDYVModel);
                //DYVModel model = JsonConvert.DeserializeObject<DYVModel>(strDYVModel);
                BankAuthorize AuzInfo = new BankAuthorize()
                {
                    BID = dyInfo.AUZ_ID,
                    AUTHORIZATIONDATE = saveTime,
                    STATUS = SysBdcFlowConst.FLOW_DYDJ_SH,
                    PRE_STATUS = preFlowID
                };
                List<TSGL_INFO> tsglET = new List<TSGL_INFO>();
                List<XGDJGL_INFO> xgdjglET = new List<XGDJGL_INFO>();
                List<QLRGL_INFO> qlrglET = new List<QLRGL_INFO>();
                //提交返回值
                IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = SysBdcFlowConst.FLOW_DYDJ_SH,//根据是否为暂存操作，判断当前流程节点提交信息
                    PRE_FLOW_ID = SysBdcFlowConst.FLOW_DYZX_SL,
                    AUZ_ID = dyInfo.AUZ_ID,
                    CDATE = saveTime,
                    USER_NAME = dyInfo.SJR
                };

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
                /*dyInfo.CNSJ = saveTime.AddDays(3);*/
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
                    DBFW = dyInfo.DBFW,
                    QRZYQK = dyInfo.QRZYQK
                };

                SPB_INFO spInfo = new SPB_INFO()
                {
                    SPBH = Provider.Sql.Create().ToString(),
                    SLBH = newSLBH,
                    SPDX = "初审意见",
                    SPYJ = dyInfo.SPYJ,
                    SPR = dyInfo.SPR,
                    SPRQ = saveTime,
                    SPTXR = dyInfo.SPR,
                    XID = dyInfo.XID,
                    SPBZ = dyInfo.SPBZ
                };

                string slbhStr = string.Empty;
                if (dyInfo.selectDyHouse.Count > 0)
                {
                    slbhStr = dyInfo.selectDyHouse[0].SLBH + (dyInfo.selectDyHouse.Count > 1 ? "等" + dyInfo.selectDyHouse.Count + "个" : string.Empty);
                }
                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    XID = dyInfo.XID,
                    YWSLBH = newSLBH,
                    DJZL = 910,
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
                    SJR = dyInfo.SJR,
                    IS_ACTION_OK = dyInfo.CommandType,
                    PDH = dyInfo.PDH,
                    SaveDataJson = saveDataJson
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
                        //var EE = this._changeMrgeServices.GetLandHouseRightInfo(hourse.BDCZH).Result;
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
                    SysDataRecorderModel saveJson = new SysDataRecorderModel()
                    {
                        PK = dyInfo.JSON_PK,
                        BUS_PK = dyInfo.XID,
                        CDATE = saveTime,
                        USER_ID = dyInfo.DYQRMC_ID,
                        USER_NAME = dyInfo.DYQRMC,
                        IS_STOP = isSubmitWorkFlow ? 1 : 0,
                        SAVEDATAJSON = JsonConvert.SerializeObject(dyInfo),
                        REMARKS1 = string.Join(",", dyInfo.selectDyHouse.Cast<DyHouseVModel>().Select(S => S.BDCZH).ToArray().Take(20)),//要抵押的不动产证号
                        REMARKS2 = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(S => S.QLRMC).Distinct().ToArray()),
                        REMARKS3 = string.Join(",", dyInfo.selectRightPerson.Cast<DyPersonVModel>().Select(S => S.QLRMC).Distinct().ToArray()),
                        REMARKS4 = string.Empty,
                        REMARKS5 = string.Empty
                    };
                    count = _BdcGeneralMrgeServices.Mortgage(AuzInfo, regInfo, flowInfo, saveJson, tsglET, dyET, spInfo, xgdjglET, qlrglET, attFileModel, dyInfo.sfd, dyInfo.sfdList, sjdInfo, qlxgInfo, isInsert, IsSave != 1, OldXID);
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
       
    }
}
