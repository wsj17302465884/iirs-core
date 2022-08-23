using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 房屋相关信息
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    public class BankAuthorizeController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        private readonly ILogger<BankAuthorizeController> _logger;

        private readonly IBankAuthorizeRepository _bankAuthorizeRepository;

        private readonly IOrderHouseAssociationRepository _orderHouseAssociationRepository;

        private readonly IMortgageServices _mortgageServices;

        private readonly IIflowActionRepository _iflowActionRepository;




        public BankAuthorizeController(IDBTransManagement dbTransManagement, ILogger<BankAuthorizeController> logger, IBankAuthorizeRepository bankAuthorizeRepository,IOrderHouseAssociationRepository orderHouseAssociationRepository, IMortgageServices mortgageServices, IIflowActionRepository iflowActionRepository)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _bankAuthorizeRepository = bankAuthorizeRepository;
            _logger = logger;
            _orderHouseAssociationRepository = orderHouseAssociationRepository;
            _iflowActionRepository = iflowActionRepository;
            _mortgageServices = mortgageServices;
        }

        /// <summary>
        /// 根据身份证查询所有的订单业务
        /// </summary>
        /// <param name="documentnumber">身份证号</param>
        /// <param name="status">状态</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<BankAuthorize>>> GetList(string documentnumber , int status)
        {
            try
            {
                var data = await _bankAuthorizeRepository.GetBankAuthorizeList(documentnumber,status);
                return new MessageModel<List<BankAuthorize>>()
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
                return new MessageModel<List<BankAuthorize>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 查询授权授权人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<BankAuthorize>>> GetBankAuthorizes()
        {
            
            try
            {
                var data = await _bankAuthorizeRepository.GetBankAuthorizeList();
                return new MessageModel<List<BankAuthorize>>()
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
                return new MessageModel<List<BankAuthorize>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 根据业务编码获得授权的房屋
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<OrderHouseAssociation>>> GetOrderHouseAssociationList(string bid)
        {
            try
            {
                var data = await _orderHouseAssociationRepository.GetOrderHouseAssociationList(bid);
                return new MessageModel<List<OrderHouseAssociation>>()
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
                return new MessageModel<List<OrderHouseAssociation>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        
        /// <summary>
        /// 查询授权订单
        /// </summary>
        /// <param name="documentnumber"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<BankAuthorize>>> GetBankAuthorizeList(string documentnumber, int status)
        {
            try
            {
                var data = await _mortgageServices.GetBankAuthorizes(documentnumber, status);
                return new MessageModel<List<BankAuthorize>>()
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
                return new MessageModel<List<BankAuthorize>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 获取所有流程节点
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<IFLOW_ACTION>>> GetFlowActionList(short groupId)
        {
            try
            {
                var data = await _mortgageServices.GetFlowActionList(groupId);
                return new MessageModel<List<IFLOW_ACTION>>()
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
                return new MessageModel<List<IFLOW_ACTION>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        #region 内网使用
        /// <summary>
        /// 
        /// </summary>
        /// <param name="zjhm">证件号码</param>
        /// <param name="bdczh">不动产证号</param>
        /// <param name="zjlb">证件类别</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<DJ_TSGL>>> GetTstybmListByZjhm(string zjhm,string bdczh,string zjlb)
        {
            try
            {
                var data = await _mortgageServices.GetTstybmListByZjhm(zjhm, bdczh,zjlb);
                if(data.Count > 0)
                {
                    return new MessageModel<List<DJ_TSGL>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }else
                {
                    return new MessageModel<List<DJ_TSGL>>()
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
                return new MessageModel<List<DJ_TSGL>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 查询企业下的房屋
        /// </summary>
        /// <param name="zjhm"></param>
        /// <param name="bdczh"></param>
        /// <param name="queryval"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<DJ_TSGL>>> GetTstybmListByEnterpriseZjhm(string zjhm, string bdczh, int queryval)
        {
            try
            {
                var data = await _mortgageServices.GetTstybmListByEnterpriseZjhm(zjhm, bdczh, queryval);
                if (data.Count > 0)
                {
                    return new MessageModel<List<DJ_TSGL>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<DJ_TSGL>>()
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
                return new MessageModel<List<DJ_TSGL>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        #endregion
    }
}
