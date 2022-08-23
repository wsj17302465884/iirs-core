using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Services.Base;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RT.Comb;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace IIRS.Controllers
{
    /// <summary>
    /// 房屋相关信息
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    public class HouseStatusController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        readonly IBdcxgxxRepository _bdcxgxxRepository;

        private readonly ILogger<HouseStatusController> _logger;

        readonly IMortgageServices _mortgageServices;

        readonly IHouseStatusRepository _houseStatusRepository;

        readonly IOrderHouseAssociationRepository _orderHouseAssociationRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbTransManagement"></param>
        /// <param name="bdcxgxxRepository"></param>
        /// <param name="logger"></param>
        /// <param name="mortgageServices"></param>
        /// <param name="houseStatusRepository"></param>
        /// <param name="orderHouseAssociationRepository"></param>
        public HouseStatusController(IDBTransManagement dbTransManagement, IBdcxgxxRepository bdcxgxxRepository, ILogger<HouseStatusController> logger, IMortgageServices mortgageServices,IHouseStatusRepository houseStatusRepository, IOrderHouseAssociationRepository orderHouseAssociationRepository)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _bdcxgxxRepository = bdcxgxxRepository;
            _logger = logger;
            _mortgageServices = mortgageServices;
            _houseStatusRepository = houseStatusRepository;
            _orderHouseAssociationRepository = orderHouseAssociationRepository;
        }



        /// <summary>
        /// 根据图属统一编码获取房屋的基本信息
        /// </summary>
        /// <param name="tstybm">图属统一编码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<HouseStatusViewModel>>> GetHouseSatausList(string tstybm)
        {
            return await _houseStatusRepository.GetHouseSatausList(tstybm);
        }
        /// <summary>
        /// 根据身份证号获取图属统一编码
        /// </summary>
        /// <param name="zjhm">证件号码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<MortgageViewModel>>> GetTstybmByZjhm(string zjhm)
        {
            //200.200.0.131
            //(_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[2].ConnId);

            //虚拟机
            (_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[5].ConnId);

            return await _mortgageServices.GetTstybmByZjhm(zjhm);
        }
        /// <summary>
        /// 根据证件号码查询有多少房子
        /// </summary>
        /// <param name="zjhm">证件号码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<MortgageViewModel>>> GetTstybmCountByZjhm(string zjhm)
        {
            //200.200.0.131
            //(_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[2].ConnId);

            //虚拟机
            (_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[5].ConnId);

            return await _mortgageServices.GetTstybmCountByZjhm(zjhm);
        }
        /// <summary>
        /// 根据企业名称获得该企业名下的所有TSTYBM（企业权利人类型为：权利人）
        /// </summary>
        /// <param name="qlrmc">企业名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<MortgageViewModel>>> GetTstybmCountByQlrmc(string qlrmc)
        {
            //200.200.0.131
            //(_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[2].ConnId);

            //虚拟机
            (_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[5].ConnId);
            return await _mortgageServices.GetTstybmCountByQlrmc(qlrmc);
        }

        /// <summary>
        /// 新增房屋相关信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<List<Guid>>> AddHouseStatusList([FromForm] string model)
        {
            var data = new MessageModel<List<Guid>>();
            data.response = new List<Guid>();
            if (string.IsNullOrEmpty(model))
            {
                data.success = false;
                data.msg = "提交数据非法";
                return data;
            }
            model = HttpUtility.UrlDecode(model);
            List<BdcxgxxModel> bdcxgxxModels = JsonConvert.DeserializeObject<List<BdcxgxxModel>>(model);
            _dbTransManagement.BeginTran();
            try
            {
                foreach (var item in bdcxgxxModels)
                {
                    //item.XID = Provider.Sql.Create();
                    //item.FLOW_TIME = DateTime.Now;

                    var id = await _bdcxgxxRepository.AddBdcxgxxModel(item);
                    //data.success = id > 0;
                    //if (data.success)
                    //{
                    //    //data.response = id.ObjToString(); 
                    //    data.response.Add(item.XID);
                    //    data.msg = "添加成功";
                    //}
                }
                _dbTransManagement.CommitTran();
            }
            catch (Exception ex)
            {
                _dbTransManagement.RollbackTran();
                _logger.LogError(ex.Message);
                data.success = false;
                data.msg = ex.Message;
            }
            return data;

        }
        
        [HttpGet]
        public async Task<MessageModel<List<HouseAuthorizeVModel>>> GetHouseMessages(string Documentnumber)
        {
            try
            {
                var data = await _mortgageServices.GetHouseMessages(Documentnumber);
                return new MessageModel<List<HouseAuthorizeVModel>>()
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
                return new MessageModel<List<HouseAuthorizeVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 获取分页不动产数据
        /// </summary>
        /// <param name="tstybm"></param>
        /// <param name="intPageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<HouseStatusViewModel>>> GetHouseSatausListToPage(string tstybm, int intPageIndex)
        {
            try
            {
                var data = await _houseStatusRepository.GetHouseSatausListToPage(tstybm,intPageIndex);
                if (data.data.Count > 0)
                {
                    return new MessageModel<PageModel<HouseStatusViewModel>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<PageModel<HouseStatusViewModel>>()
                    {
                        msg = "未查询到数据",
                        success = false,
                        response = data
                    };
                }
                    
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PageModel<HouseStatusViewModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        [HttpGet]
        public async Task<MessageModel<PageModel<HouseStatusViewModel>>> GetEnterpriseHouseSatausListToPage(string tstybm, int intPageIndex)
        {
            try
            {
                var data = await _houseStatusRepository.GetEnterpriseHouseSatausListToPage(tstybm, intPageIndex);
                if (data.data.Count > 0)
                {
                    return new MessageModel<PageModel<HouseStatusViewModel>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<PageModel<HouseStatusViewModel>>()
                    {
                        msg = "未查询到数据",
                        success = false,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PageModel<HouseStatusViewModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strDYVModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<int>> Post([FromForm] string strDYVModel)
        {
            try
            {
                int count = 0;
                DateTime saveTime = DateTime.Now;
                AuthorizedHouseVModel model = JsonConvert.DeserializeObject<AuthorizedHouseVModel>(HttpUtility.UrlDecode(strDYVModel));
                BankAuthorize bankAuthorize = new BankAuthorize()
                {
                    BID = Provider.Sql.Create().ToString(),
                    DOCUMENTTYPE = model.zjlb,
                    DOCUMENTNUMBER = model.dyr_zjhm,
                    AUTHORIZATIONDATE = saveTime,
                    STATUS = model.STATUS,
                    rightname = model.dyr,
                    BankCode = model.YHYTSHXYDM,
                    BankName = model.DYQRMC
                };
                List<OrderHouseAssociation> orderHouseAssociationsList = new List<OrderHouseAssociation>();

                foreach (var item in model.selectHouseInfo)
                {
                    orderHouseAssociationsList.Add(new OrderHouseAssociation
                    { 
                        OID = Provider.Sql.Create().ToString(),
                        BID = bankAuthorize.BID,
                        CERTIFICATENUMBER = item.bdczh,
                        ACCEPTANCENUMBER = item.slbh,
                        NUMBERID = item.tstybm,
                        ADDRESS = item.zl,
                        rightname = item.qlrmc,
                        AUTHORIZATIONDATE = saveTime,
                        qllx = item.qllx,
                        qlxz = item.qlxz,
                        jzmj = item.jzmj,
                        tdqllx = item.tdqllx,
                        tdqlxz = item.tdqlxz,
                        tdmj = item.dytdmj,
                        bdclx = item.bdclx
                    }
                    );;
                        
                }

                count = await _mortgageServices.SaveBankauthorize(bankAuthorize, orderHouseAssociationsList);
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
        /// 
        /// </summary>
        /// <param name="strDYVModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> PostBid([FromForm] string strDYVModel)
        {
            try
            {
                string Bid = "";
                DateTime saveTime = DateTime.Now;
                AuthorizedHouseVModel model = JsonConvert.DeserializeObject<AuthorizedHouseVModel>(HttpUtility.UrlDecode(strDYVModel));
                BankAuthorize bankAuthorize = new BankAuthorize()
                {
                    BID = Provider.Sql.Create().ToString(),
                    DOCUMENTTYPE = model.zjlb,
                    DOCUMENTNUMBER = model.dyr_zjhm,
                    AUTHORIZATIONDATE = saveTime,
                    STATUS = model.STATUS,
                    rightname = model.dyr,
                    BankCode = model.YHYTSHXYDM,
                    BankName = model.DYQRMC
                };
                List<OrderHouseAssociation> orderHouseAssociationsList = new List<OrderHouseAssociation>();

                foreach (var item in model.selectHouseInfo)
                {
                    orderHouseAssociationsList.Add(new OrderHouseAssociation
                    {
                        OID = Provider.Sql.Create().ToString(),
                        BID = bankAuthorize.BID,
                        CERTIFICATENUMBER = item.bdczh,
                        ACCEPTANCENUMBER = item.slbh,
                        NUMBERID = item.tstybm,
                        ADDRESS = item.zl,
                        rightname = item.qlrmc,
                        AUTHORIZATIONDATE = saveTime,
                        qllx = item.qllx,
                        qlxz = item.qlxz,
                        jzmj = item.jzmj,
                        tdqllx = item.tdqllx,
                        tdqlxz = item.tdqlxz,
                        tdmj = item.dytdmj,
                        bdclx = item.bdclx
                    }
                    );

                }

                IFLOW_DO_ACTION IflowDoAction = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = Convert.ToInt32(model.STATUS),
                    AUZ_ID = bankAuthorize.BID,
                    CDATE = DateTime.Now,
                    USER_NAME = model.SJR
                };

                Bid = await _mortgageServices.SaveReturnBid(bankAuthorize, orderHouseAssociationsList, IflowDoAction);
                return new MessageModel<string>()
                {
                    msg = "获取成功",
                    success = true,
                    response = Bid
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<string>()
                {
                    msg = "保存失败" + ex.Message,
                    success = false,
                    response = ""
                };
            }
        }

        /// <summary>
        /// 是否可以提交选中数据
        /// </summary>
        /// <param name="tstybm"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<OrderHouseAssociation>>> GetDoSubmit(string tstybm)
        {
            try
            {
                var data = await _mortgageServices.GetDoSubmit(tstybm);
                if(data.Count > 0)
                {
                    return new MessageModel<List<OrderHouseAssociation>>()
                    {
                        msg = "办理过业务",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<OrderHouseAssociation>>()
                    {
                        msg = "获取成功",
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

        
    }
}
