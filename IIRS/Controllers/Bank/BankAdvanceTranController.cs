using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.IServices;
using IIRS.IServices.Bank;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
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

namespace IIRS.Controllers.Bank
{
    /// <summary>
    /// 银行版 -- 预告抵押登记
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    public class BankAdvanceTranController : ControllerBase
    {
        private readonly IBdcGeneralMrgeServices _BdcGeneralMrgeServices;
        private readonly IChangeMrgeServices _changeMrgeServices;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICommonServices _CommonServices;
        private readonly IDBTransManagement _dbTransManagement;
        private readonly ILogger<BankAdvanceTranController> _logger;
        private readonly IBankQueryServices _queryServices;
        private readonly IBankGeneralMrgeServices _BankGeneralMrgeServices;
        private readonly IBus_visit_logRepository _Ibus_visit_logRepository;
        private readonly IBankAdvanceTranServices _IbankAdvanceTranServices;
        public BankAdvanceTranController(IDBTransManagement dbTransManagement, ILogger<BankAdvanceTranController> logger, IBdcGeneralMrgeServices bdcGeneralMrgeServices1, IChangeMrgeServices changeMrgeServices, IWebHostEnvironment webHostEnvironment, 
            ICommonServices commonServices, IBankQueryServices queryServices,
            IBankGeneralMrgeServices bdcGeneralMrgeServices,
            IBus_visit_logRepository bus_visit_logRepository, IBankAdvanceTranServices ibankAdvanceTranServices)
        {
            _BdcGeneralMrgeServices = bdcGeneralMrgeServices1;
            _changeMrgeServices = changeMrgeServices;
            _webHostEnvironment = webHostEnvironment;
            _CommonServices = commonServices;
            _dbTransManagement = dbTransManagement;
            _logger = logger;
            _queryServices = queryServices;
            _BankGeneralMrgeServices = bdcGeneralMrgeServices;
            _Ibus_visit_logRepository = bus_visit_logRepository;
            _IbankAdvanceTranServices = ibankAdvanceTranServices;
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
            //XM = "朱晓琪";
            //SFZH = "211004199102137822";
            //BDCZH = "CS辽(2022)辽阳市不动产证明第0001877号";
            //BDCLX = "房屋";
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
                        msg = "不动产证明号不能为空",
                        success = false
                    };
                }
               /* if (string.IsNullOrEmpty(BDCLX) || !((new string[] { "房屋", "土地" }).Contains(BDCLX)))
                {
                    return new MessageModel<PageStringModel>()
                    {
                        msg = "不动产类型只能是:房屋或者土地",
                        success = false
                    };
                }*/
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
                Lmodel.PARAMS = $"不动产证号：{BDCZH} 姓名{XM} 不动产类型{BDCLX}身份证号：{SFZH}";
                if (Lmodel.PARAMS.Length >= 500)
                {
                    Lmodel.PARAMS = Lmodel.PARAMS.Substring(0, 498) + "...";
                }
                Lmodel.ORGANIZATION_ID = ORGANIZATION_ID;
                Lmodel.USER_ID = USER_ID;
                Lmodel.USER_NAME = USER_NAME;
                Lmodel.VDATE = DateTime.Now;
                var Logdata = this._Ibus_visit_logRepository.Add(Lmodel);
                var data = await this._BankGeneralMrgeServices.GetAdvanceHourseInfo(XM, SFZH, BDCZH, BDCLX, pageIndex, pageSize);
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
        /// 预告抵押懒加载数据
        /// </summary>
        /// <param name="XM">姓名</param>
        /// <param name="SFZH">身份证号</param>
        /// <param name="BDCZH">不动产证明号</param>
        /// <param name="BDCLX">不动产类型</param>
        /// <param name="ORGANIZATION_ID">组织机构编码</param>
        /// <param name="USER_ID">用户编码</param>
        /// <param name="USER_NAME">用户名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<AdvanceVModel>>> GetBdcHourseInfoList(string XM, string SFZH, string BDCZH, string BDCLX, string ORGANIZATION_ID, string USER_ID, string USER_NAME)
        {
            //XM = "朱晓琪";
            //SFZH = "211004199102137822";
            //BDCZH = "CS辽(2022)辽阳市不动产证明第0001877号";
            //BDCLX = "房屋";
            try
            {
                if (string.IsNullOrEmpty(XM))
                {
                    return new MessageModel<List<AdvanceVModel>>()
                    {
                        msg = "姓名不能为空",
                        success = false
                    };
                }
                if (string.IsNullOrEmpty(BDCZH))
                {
                    return new MessageModel<List<AdvanceVModel>>()
                    {
                        msg = "不动产证明号不能为空",
                        success = false
                    };
                }
                /* if (string.IsNullOrEmpty(BDCLX) || !((new string[] { "房屋", "土地" }).Contains(BDCLX)))
                 {
                     return new MessageModel<PageStringModel>()
                     {
                         msg = "不动产类型只能是:房屋或者土地",
                         success = false
                     };
                 }*/
                if (SFZH.Length != 18)
                {
                    return new MessageModel<List<AdvanceVModel>>()
                    {
                        msg = "错误的身份证号",
                        success = false
                    };
                }
                BUS_VISIT_LOG Lmodel = new BUS_VISIT_LOG();
                Lmodel.PK = Guid.Empty.ToString("N");
                Lmodel.GROUP_ID = 2;
                Lmodel.PARAMS = $"不动产证号：{BDCZH} 姓名{XM} 不动产类型{BDCLX}身份证号：{SFZH}";
                if (Lmodel.PARAMS.Length >= 500)
                {
                    Lmodel.PARAMS = Lmodel.PARAMS.Substring(0, 498) + "...";
                }
                Lmodel.ORGANIZATION_ID = ORGANIZATION_ID;
                Lmodel.USER_ID = USER_ID;
                Lmodel.USER_NAME = USER_NAME;
                Lmodel.VDATE = DateTime.Now;
                var Logdata = this._Ibus_visit_logRepository.Add(Lmodel);
                var data = await this._IbankAdvanceTranServices.GetBdcHourseInfo(XM, SFZH, BDCZH, BDCLX);
                return new MessageModel<List<AdvanceVModel>>()
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
                return new MessageModel<List<AdvanceVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false
                };
            }
        }
        /// <summary>
        /// 查询不动产权利人
        /// </summary>
        /// <param name="SLBH">受理编号(逗号分隔)</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<QLR_VModel>>> GetYGQlrInfo(string SLBH)
        {
            try
            {
                string[] slbh = SLBH.Split(",", StringSplitOptions.RemoveEmptyEntries);
                if (slbh.Length > 0)
                {
                    var data = await this._BankGeneralMrgeServices.GetBankQlrInfo(slbh);
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



        /// <summary>
        /// 保存不动产预告抵押信息
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
                string fslbh = "";
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
                    if (flowResult == null || flowResult.CURRENT_STATUS != SysBdcFlowConst.FLOW_YGDY_BANK)
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
                            STATUS = SysBdcFlowConst.FLOW_YGDY_BANK,
                            PRE_STATUS = null
                        };

                        flowInfo = new IFLOW_DO_ACTION()
                        {
                            PK = Provider.Sql.Create().ToString(),
                            FLOW_ID = SysBdcFlowConst.FLOW_YGDY_BANK,
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
                        STATUS = SysBdcFlowConst.FLOW_YGDY_SL_BANK,
                        PRE_STATUS = preFlowID
                    };

                    flowInfo = new IFLOW_DO_ACTION()
                    {
                        PK = Provider.Sql.Create().ToString(),
                        FLOW_ID = SysBdcFlowConst.FLOW_YGDY_SL_BANK,
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
                    DJLX = "预告登记",
                    DJYY = dyInfo.DJYY,
                    XGZH = xgzh,
                    ZLXX = dyInfo.ZL,
                    //selectDyHouse[0].ZL + (dyInfo.selectDyHouse.Count > 1 ? "等" + dyInfo.selectDyHouse.Count + "个" : string.Empty),
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
                    //QRZYQK = dyInfo.QRZYQK
                    SFCZYD = dyInfo.QRZYQK,   //1.是  0.否
                    DYRLX = dyInfo.DYRLX
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
                    DJZL = SysBdcFlowConst.FLOW_YGDY_GROUP,
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
                    SAVEDATE = saveTime
                };

                SJD_INFO sjdInfo = new SJD_INFO()
                {
                    SLBH = newSLBH,
                    DJDL = "700",
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
                            BGLX = "抵押预告",
                            XGZLX = "房屋预告证明",
                            XGZH = hourse.BDCZH,
                            XID = dyInfo.XID
                        });
                        fslbh = hourse.SLBH;
                    }
                }
                if(dyInfo.ygXgdjglList.Count > 0)
                {
                    foreach (var item in dyInfo.ygXgdjglList)
                    {
                        xgdjglET.Add(new XGDJGL_INFO()
                        {
                            BGBM = Provider.Sql.Create().ToString(),
                            ZSLBH = item.ZSLBH,
                            FSLBH = item.FSLBH,
                            BGRQ = item.BGRQ,
                            BGLX = dyInfo.DYLX,
                            XGZLX = item.XGZLX,
                            XGZH = item.XGZH,
                            PID = item.PID,
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
        /// 查询预告下是否有房产
        /// </summary>
        /// <param name="slbh">预告受理编码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<DJ_XGDJGL>>> GetYgBdczhInfo(string slbh)
        {
            try
            {
                var data = await _IbankAdvanceTranServices.GetYgBdczhInfo(slbh);

                if (data.Count > 0)
                {
                    return new MessageModel<List<DJ_XGDJGL>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<DJ_XGDJGL>>()
                    {
                        msg = "未获取到数据",
                        success = false,
                        response = null
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<DJ_XGDJGL>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
    }


}
