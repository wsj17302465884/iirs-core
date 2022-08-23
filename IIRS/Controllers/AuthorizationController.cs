using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RT.Comb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static IIRS.Models.ViewModel.IIRS.ParcelVModel;

namespace IIRS.Controllers
{
    /// <summary>
    /// 暂存与退回信息
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    public class AuthorizationController : ControllerBase
    {
        private readonly IDBTransManagement _dbTransManagement;
        private readonly ILogger<AuthorizationController> _logger;
        private readonly IIflowActionRepository _iflowActionRepository;
        private readonly IBankAuthorizeRepository _bankAuthorizeRepository;
        private readonly IConstructionServices _constructionServices;
        private readonly IAuthorizationServices _authorizationServices;

        public AuthorizationController(IDBTransManagement dbTransManagement, ILogger<AuthorizationController> logger, IIflowActionRepository iflowActionRepository, IBankAuthorizeRepository bankAuthorizeRepository, IConstructionServices constructionServices, IAuthorizationServices authorizationServices)
        {
            _dbTransManagement = dbTransManagement;
            _logger = logger;
            _iflowActionRepository = iflowActionRepository;
            _bankAuthorizeRepository = bankAuthorizeRepository;
            _constructionServices = constructionServices;
            _authorizationServices = authorizationServices;
        }
        /// <summary>
        /// 获取流程名称
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<IFLOW_ACTION>>> GetFlowName()
        {
            try
            {
                var data = await _iflowActionRepository.GetFlowName();
                if(data.Count>0)
                {
                    return new MessageModel<List<IFLOW_ACTION>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<IFLOW_ACTION>>()
                    {
                        msg = "未获取到数据",
                        success = false,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<IFLOW_ACTION>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 获取授权订单信息
        /// </summary>
        /// <param name="intPageIndex"></param>
        /// <param name="zjhm"></param>
        /// <param name="userId"></param>
        /// <param name="flowId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<BankAuthorize>>> GetAuthorizationListToPage(int intPageIndex, string zjhm, string userId, int flowId)
        {
            string fid = flowId.ToString();
            
            try
            {
                //var data = await _bankAuthorizeRepository.GetAuthorizationListToPage(intPageIndex, zjhm, jbr,flowId);
                var data = await _authorizationServices.GetAuthorizationListToPage(intPageIndex, zjhm, userId, flowId);
                BankAuthorize model = new BankAuthorize();
                List<BankAuthorize> models = new List<BankAuthorize>();
                
                for (int i = 0; i < data.data.Count; i++)
                {
                    model = new BankAuthorize();
                    model.BID = data.data[i].BID;
                    model.DOCUMENTTYPE = data.data[i].DOCUMENTTYPE;
                    model.DOCUMENTNUMBER = data.data[i].DOCUMENTNUMBER;
                    model.AUTHORIZATIONDATE = data.data[i].AUTHORIZATIONDATE;
                    model.STATUS = data.data[i].STATUS;
                    model.rightname = data.data[i].rightname;
                    model.BankCode = data.data[i].BankCode;
                    model.BankName = data.data[i].BankName;
                    model.FlowName = data.data[i].FlowName;
                    models.Add(model);
                }
                data.data.Clear();
                foreach (var item in models)
                {
                    data.data.Add(item);
                }
                data.page = data.page;
                data.pageCount = data.pageCount;
                data.PageSize = data.PageSize;
                data.dataCount = data.dataCount;

                if (data.data.Count > 0)
                {
                    return new MessageModel<PageModel<BankAuthorize>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<PageModel<BankAuthorize>>()
                    {
                        msg = "未获取到数据",
                        success = false,
                        response = data
                    };
                }                
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PageModel<BankAuthorize>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 抵押变更提交数据
        /// </summary>
        /// <param name="strDYVModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<int>> Post([FromForm] string strDYVModel)
        {
            try
            {
                int count = 0;
                string xgzlx = "";
                DateTime saveTime = DateTime.Now;
                ParcelVModel ParcelInfo = JsonConvert.DeserializeObject<ParcelVModel>(HttpUtility.UrlDecode(strDYVModel));
                string newSLBH = _constructionServices.GetSLBH();//服务器端获取受理编号
                List<TSGL_INFO> tsglET = new List<TSGL_INFO>();
                List<OrderHouseAssociation> ationList = new List<OrderHouseAssociation>();
                List<XGDJGL_INFO> xgdjglET = new List<XGDJGL_INFO>();
                List<QLRGL_INFO> qlrglET = new List<QLRGL_INFO>();
                ParcelInfo.CNSJ = saveTime.AddDays(3);
                if (ParcelInfo.Bdclx)
                {
                    xgzlx = "房屋抵押证明";
                }
                else
                {
                    xgzlx = "土地抵押证明";
                }
                BankAuthorize bankAuthorize = new BankAuthorize()
                {
                    BID = ParcelInfo.AUZ_ID,
                    STATUS = 17     //抵押变更暂存状态为17
                };

                DY_INFO dyET = new DY_INFO()
                {
                    SLBH = newSLBH,
                    YWSLBH = newSLBH,
                    DJLX = "抵押登记",      //抵押登记
                    DJYY = ParcelInfo.DJYY,
                    XGZH = ParcelInfo.selectHouseInfo[0].BDCZH,
                    ZLXX = ParcelInfo.ZL,   //户坐落.....等                    
                    SQRQ = saveTime,
                    DYLX = "抵押变更",     //抵押变更
                    DYSW = ParcelInfo.DYSW,
                    DYFS = ParcelInfo.DYFS,
                    DYMJ = ParcelInfo.dyMJ,   //多个户的面积和   
                    BDBZZQSE = ParcelInfo.BDBZQSE,
                    PGJE = ParcelInfo.PGJE,
                    HTH = ParcelInfo.HTH,
                    LXR = ParcelInfo.DYLXR,
                    LXRDH = ParcelInfo.DYLXRDH,
                    BDCDYH = ParcelInfo.bdcdyh,

                    CNSJ = ParcelInfo.CNSJ,
                    FJ = ParcelInfo.BZ,
                    ZWR = string.Concat(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(p => p.QLRMC).ToArray()),
                    ZWRZJH = string.Concat(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(p => p.ZJHM).ToArray()),
                    ZWRZJLX = string.Concat(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(p => p.ZJLB).Distinct().ToArray()),//改成中文类型
                    DLJGMC = ParcelInfo.DYQRMC,
                    QLQSSJ = ParcelInfo.ZWLXQXQSRQ.Date,
                    QLJSSJ = ParcelInfo.ZWLXQXJZRQ.Date,
                    DYQX = ParcelInfo.LXQX.ToString()
                };
                
                IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = 17,
                    AUZ_ID = ParcelInfo.AUZ_ID,
                    CDATE = saveTime,
                    USER_NAME = ParcelInfo.DYLXR
                };

                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    XID = Provider.Sql.Create().ToString(),
                    YWSLBH = newSLBH,
                    DJZL = 3,   //登记类型（抵押变更）
                    BDCZH = ParcelInfo.selectHouseInfo[0].BDCZH,
                    SLBH = ParcelInfo.selectHouseInfo[0].SLBH,
                    QLRMC = string.Join(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(s => s.QLRMC).ToArray()),
                    ZL = dyET.ZLXX,         //户坐落加等
                    ORG_ID = ParcelInfo.BankDeptID,
                    USER_ID = ParcelInfo.DYQRMC_ID,
                    TEL = ParcelInfo.DYLXRDH,
                    AUZ_ID = ParcelInfo.AUZ_ID,
                    HTH = ParcelInfo.HTH,
                    REMARK2 = ParcelInfo.DYLX

                };

                IFLOW_DO_ACTION flow = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    AUZ_ID = ParcelInfo.AUZ_ID,
                    FLOW_ID = 17                    
                };

                foreach (var hourse in ParcelInfo.selectHouseInfo)
                {
                    //需要插入房子的相关信息
                    tsglET.Add(new TSGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        BDCLX = "房屋",
                        TSTYBM = hourse.TSTYBM,
                        BDCDYH = hourse.BDCDYH,
                        DJZL = "抵押",
                        CSSJ = saveTime
                    });

                    //插入授权订单相关联的房屋信息
                    ationList.Add(new OrderHouseAssociation()
                    {
                        OID = Provider.Sql.Create().ToString(),
                        BID = ParcelInfo.AUZ_ID,
                        CERTIFICATENUMBER = hourse.BDCZH,
                        ACCEPTANCENUMBER = hourse.SLBH,
                        NUMBERID = hourse.TSTYBM,
                        ADDRESS = hourse.ZL,
                        rightname = hourse.QLRMC,
                        AUTHORIZATIONDATE = saveTime
                    });
                }

                xgdjglET.Add(new XGDJGL_INFO()
                {
                    BGBM = Provider.Sql.Create().ToString(),
                    ZSLBH = newSLBH,
                    FSLBH = ParcelInfo.DYSLBH,
                    BGRQ = saveTime,
                    BGLX = "抵押变更",
                    XGZLX = xgzlx,
                    XGZH = ParcelInfo.Bdczmh
                });

                foreach (var item in ParcelInfo.selectHouseInfo)
                {
                    xgdjglET.Add(new XGDJGL_INFO()
                    {
                        BGBM = Provider.Sql.Create().ToString(),
                        ZSLBH = newSLBH,
                        FSLBH = item.SLBH,    //土地受理编号
                        BGRQ = saveTime,
                        BGLX = "抵押",            //在建工程抵押
                        XGZLX = item.ZSLX,
                        XGZH = item.BDCZH         //土地的相关证号
                    });
                }

                foreach (var person in ParcelInfo.selectDyPerson)
                {
                    qlrglET.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        YWBM = newSLBH,
                        ZJHM = person.ZJHM,
                        QLRID = person.QLRID,
                        QLRMC = person.QLRMC,
                        ZJLB = person.ZJLB_ZWM,
                        DH = person.DH,
                        QLRLX = "抵押人",
                        //SXH = person.SXH
                    });
                }
                qlrglET.Add(new QLRGL_INFO()
                {
                    GLBM = Provider.Sql.Create().ToString(),
                    SLBH = newSLBH,
                    YWBM = newSLBH,
                    ZJHM = ParcelInfo.YHYTSHXYDM,
                    QLRID = ParcelInfo.YHYTSHXYDM2,
                    QLRMC = ParcelInfo.DYQRMC,
                    ZJLB = "统一社会信用代码",//"统一社会信用代码",
                    DH = ParcelInfo.DYLXRDH,
                    QLRLX = "抵押权人"
                });
                count = await _authorizationServices.Authorization(bankAuthorize,regInfo, flowInfo, tsglET, ationList, dyET, xgdjglET, qlrglET);
                return new MessageModel<int>()
                {
                    msg = "获取成功",
                    success = true,
                    response = count
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<int>()
                {
                    msg = "保存失败" + ex.Message,
                    success = false,
                    response = 0
                };
            }

        }

        /// <summary>
        /// 获取抵押人信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<QLRGL_INFO>>> GetAuthorizationDyrList(string slbh)
        {
            try
            {
                var data = await _authorizationServices.GetAuthorizationDyrList(slbh);
                if (data.Count > 0)
                {
                    return new MessageModel<List<QLRGL_INFO>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<QLRGL_INFO>>()
                    {
                        msg = "未获取到数据",
                        success = false,
                        response = data
                    };
                }
            }
            catch (Exception ex )
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<QLRGL_INFO>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
            
        }

        /// <summary>
        /// 获取房屋信息
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="flowId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<OrderHouseAssociation>>> GetAuthorizationHouseList(string bid,int flowId)
        {
            try
            {
                var data = await _authorizationServices.GetAuthorizationHouseList(bid,flowId);
                if (data.Count > 0)
                {
                    return new MessageModel<List<OrderHouseAssociation>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<OrderHouseAssociation>>()
                    {
                        msg = "未获取到数据",
                        success = false,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<OrderHouseAssociation>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 获取抵押信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<DY_INFO>>> GetDyInfoModel(string slbh)
        {
            try
            {
                var data = await _authorizationServices.GetDyInfoModel(slbh);
                if (data.Count > 0)
                {
                    return new MessageModel<List<DY_INFO>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<DY_INFO>>()
                    {
                        msg = "未获取到数据",
                        success = false,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<DY_INFO>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 删除订单相关数据
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="slbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<int>> delete(string bid,string slbh)
        {
            int count = await _authorizationServices.DeleteAuthorizationInfo(bid,slbh);
            if (count > 0)
            {
                return new MessageModel<int>()
                {
                    msg = "删除成功",
                    success = true,
                    response = count
                };
            }
            else
            {
                return new MessageModel<int>()
                {
                    msg = "删除失败",
                    success = false,
                    response = count
                };
            }
        }

        /// <summary>
        /// 抵押提交数据
        /// </summary>
        /// <param name="strDYVModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<int>> SavePost([FromForm] string strDYVModel)
        {
            try
            {
                int count = 0;
                DateTime saveTime = DateTime.Now;
                ParcelVModel ParcelInfo = JsonConvert.DeserializeObject<ParcelVModel>(HttpUtility.UrlDecode(strDYVModel));
                string newSLBH = _constructionServices.GetSLBH();//服务器端获取受理编号
                List<TSGL_INFO> tsglET = new List<TSGL_INFO>();
                List<OrderHouseAssociation> ationList = new List<OrderHouseAssociation>();
                List<XGDJGL_INFO> xgdjglET = new List<XGDJGL_INFO>();
                List<QLRGL_INFO> qlrglET = new List<QLRGL_INFO>();
                ParcelInfo.CNSJ = saveTime.AddDays(3);
                
                DY_INFO dyET = new DY_INFO()
                {
                    SLBH = newSLBH,
                    YWSLBH = newSLBH,
                    DJLX = "抵押登记",      //抵押登记
                    DJYY = ParcelInfo.DJYY,
                    XGZH = ParcelInfo.selectHouseInfo[0].BDCZH,
                    ZLXX = ParcelInfo.ZL,   //户坐落.....等                    
                    SQRQ = saveTime,
                    DYLX = "抵押变更",     //抵押变更
                    DYSW = ParcelInfo.DYSW,
                    DYFS = ParcelInfo.DYFS,
                    DYMJ = ParcelInfo.dyMJ,   //多个户的面积和   
                    BDBZZQSE = ParcelInfo.BDBZQSE,
                    PGJE = ParcelInfo.PGJE,
                    HTH = ParcelInfo.HTH,
                    LXR = ParcelInfo.DYLXR,
                    LXRDH = ParcelInfo.DYLXRDH,
                    BDCDYH = ParcelInfo.bdcdyh,

                    CNSJ = ParcelInfo.CNSJ,
                    FJ = ParcelInfo.BZ,
                    ZWR = string.Concat(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(p => p.QLRMC).ToArray()),
                    ZWRZJH = string.Concat(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(p => p.ZJHM).ToArray()),
                    ZWRZJLX = string.Concat(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(p => p.ZJLB).Distinct().ToArray()),//改成中文类型
                    DLJGMC = ParcelInfo.DYQRMC,
                    QLQSSJ = ParcelInfo.ZWLXQXQSRQ.Date,
                    QLJSSJ = ParcelInfo.ZWLXQXJZRQ.Date,
                    DYQX = ParcelInfo.LXQX.ToString()
                };
                IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = 11,   //抵押变更（待不动产中心抵押审批）
                    AUZ_ID = ParcelInfo.AUZ_ID,
                    CDATE = saveTime,
                    USER_NAME = ParcelInfo.DYLXR
                };
                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    XID = Provider.Sql.Create().ToString(),
                    YWSLBH = newSLBH,
                    DJZL = 3,   //登记类型（抵押变更）
                    BDCZH = ParcelInfo.selectHouseInfo[0].BDCZH,
                    SLBH = ParcelInfo.selectHouseInfo[0].SLBH,
                    QLRMC = string.Join(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(s => s.QLRMC).ToArray()),
                    ZL = dyET.ZLXX,         //户坐落加等
                    ORG_ID = ParcelInfo.BankDeptID,
                    USER_ID = ParcelInfo.DYQRMC_ID,
                    TEL = ParcelInfo.DYLXRDH,
                    AUZ_ID = ParcelInfo.AUZ_ID,
                    HTH = ParcelInfo.HTH,
                    REMARK2 = ParcelInfo.DYLX

                };

                foreach (var hourse in ParcelInfo.selectHouseInfo)
                {
                    //需要插入房子的相关信息
                    tsglET.Add(new TSGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        BDCLX = "房屋",
                        TSTYBM = hourse.numberid,
                        BDCDYH = hourse.BDCDYH,
                        DJZL = "抵押",
                        CSSJ = saveTime
                    });

                    //插入授权订单相关联的房屋信息
                    ationList.Add(new OrderHouseAssociation()
                    {
                        OID = Provider.Sql.Create().ToString(),
                        BID = ParcelInfo.AUZ_ID,
                        CERTIFICATENUMBER = hourse.certificatenumber,
                        ACCEPTANCENUMBER = hourse.acceptancenumber,
                        NUMBERID = hourse.numberid,
                        ADDRESS = hourse.address,
                        rightname = hourse.rightname,
                        AUTHORIZATIONDATE = saveTime
                    });
                }

                foreach (var item in ParcelInfo.selectHouseInfo)
                {
                    xgdjglET.Add(new XGDJGL_INFO()
                    {
                        BGBM = Provider.Sql.Create().ToString(),
                        ZSLBH = newSLBH,
                        FSLBH = item.acceptancenumber,    //土地受理编号
                        BGRQ = saveTime,
                        BGLX = "抵押",   
                        XGZLX = item.ZSLX,
                        XGZH = item.certificatenumber  //相关证号
                    });
                }

                foreach (var person in ParcelInfo.selectDyPerson)
                {
                    qlrglET.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        YWBM = newSLBH,
                        ZJHM = person.ZJHM,
                        QLRID = person.QLRID,
                        QLRMC = person.QLRMC,
                        ZJLB = person.ZJLB_ZWM,
                        DH = person.DH,
                        QLRLX = "抵押人",
                        //SXH = person.SXH
                    });
                }
                count = await _authorizationServices.Authorization(regInfo, flowInfo, tsglET, ationList, dyET, xgdjglET, qlrglET);
                return new MessageModel<int>()
                {
                    msg = "获取成功",
                    success = true,
                    response = count
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<int>()
                {
                    msg = "保存失败" + ex.Message,
                    success = false,
                    response = 0
                };
            }

        }
        /// <summary>
        /// 获取流程节点分组表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<IFLOW_ACTION_GROUP>>> GetIflowGroupList()
        {
            try
            {
                var data = await _authorizationServices.GetIflowGroupList();
                if (data.Count > 0)
                {
                    return new MessageModel<List<IFLOW_ACTION_GROUP>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<IFLOW_ACTION_GROUP>>()
                    {
                        msg = "未获取到数据",
                        success = false,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<IFLOW_ACTION_GROUP>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 获取节点下的流程名称
        /// </summary>
        /// <param name="gid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<IFLOW_ACTION>>> GetFlowNameByGid(int gid)
        {
            try
            {
                var data = await _authorizationServices.GetFlowNameByGid(gid);
                if (data.Count > 0)
                {
                    return new MessageModel<List<IFLOW_ACTION>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<IFLOW_ACTION>>()
                    {
                        msg = "未获取到数据",
                        success = false,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<IFLOW_ACTION>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
    }
}
