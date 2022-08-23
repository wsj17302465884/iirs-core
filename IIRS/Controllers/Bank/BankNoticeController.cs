using IIRS.IRepository.Base;
using IIRS.IServices.BDC;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Controllers.Bank
{
    /// <summary>
    /// 预告登记
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    public class BankNoticeController : ControllerBase
    {
        private readonly IDBTransManagement _dbTransManagement;
        private readonly ILogger<BankNoticeController> _logger;
        private readonly INoticeRegistrationServices _noticeRegistrationServices;

        public BankNoticeController(IDBTransManagement dbTransManagement, ILogger<BankNoticeController> logger,
            INoticeRegistrationServices noticeRegistrationServices)
        {
            _dbTransManagement = dbTransManagement;
            _logger = logger;
            _noticeRegistrationServices = noticeRegistrationServices;
        }

        /// <summary>
        /// 获取该合同编号下的网签数据
        /// </summary>
        /// <param name="HTBH">合同编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<V_BDCZJK_WQ_Vmodel>>> GetAdvanceByInternetSignInfo(string HTBH)
        {
            try
            {
                var data = await _noticeRegistrationServices.GetAdvanceByInternetSignInfo(HTBH);
                if (data.Count > 0)
                {
                    return new MessageModel<List<V_BDCZJK_WQ_Vmodel>>()
                    {
                        msg = "调用接口成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<V_BDCZJK_WQ_Vmodel>>()
                    {
                        msg = "调用接口成功",
                        success = true,
                        response = data
                    };
                }

            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<V_BDCZJK_WQ_Vmodel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 获取房产户权籍调查信息
        /// </summary>
        /// <param name="bdcdyh">当前业务不动产单元号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<fc_h_qsdcVmodel>>> GetAdvanceByHouseInfo(string bdcdyh)
        {
            try
            {
                var data = await _noticeRegistrationServices.GetAdvanceByHouseInfo(bdcdyh);
                if (data.Count > 0)
                {
                    return new MessageModel<List<fc_h_qsdcVmodel>>()
                    {
                        msg = "调用接口成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<fc_h_qsdcVmodel>>()
                    {
                        msg = "调用接口成功",
                        success = true,
                        response = data
                    };
                }

            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<fc_h_qsdcVmodel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
    }
}
