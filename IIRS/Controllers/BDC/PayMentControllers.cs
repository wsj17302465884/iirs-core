using IIRS.IServices.BDC;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace IIRS.Controllers.BDC
{
    /// <summary>
    /// 线下缴费控制器
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    public class PayMentControllers : ControllerBase
    {
        private readonly ILogger<PayMentControllers> _logger;
        private readonly IPayMentServices _payMentServices;
        public PayMentControllers(ILogger<PayMentControllers> logger, IPayMentServices payMentServices)
        {
            this._logger = logger;
            this._payMentServices = payMentServices;
        }
        /// <summary>
        /// 1.线下电子缴款书，生成缴款码
        /// </summary>
        /// <param name="slbh">缴费受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<string>> GenerateTollCode(string slbh)
        {
            try
            {
                var data = await this._payMentServices.GenerateTollCode(slbh);
                if (data != null)
                {
                    
                    return new MessageModel<string>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "获取失败",
                        success = false
                    };
                }
            }
            catch (Exception ex)
            {

                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<string>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 2.生成缴款书支付二维码URL
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<string>> GetPayQrCode(string slbh)
        {
            try
            {
                var data = await this._payMentServices.GetPayQrCode(slbh);
                if (!string.IsNullOrEmpty(data))
                {
                    if(data.Contains("http://debug.epayservice.cn/thirdpay/qrcode"))
                    {
                        return new MessageModel<string>()
                        {
                            msg = "获取成功！",
                            success = true,
                            response = data
                        };
                    }
                    else if (data == "400")
                    {
                        return new MessageModel<string>()
                        {
                            msg = "获取支付URL失败,请联系管理员",
                            success = false,
                            response = data
                        };
                    }
                    else if (data == "500")
                    {
                        return new MessageModel<string>()
                        {
                            msg = "未输入受理编号",
                            success = false,
                            response = data
                        };
                    }
                    else
                    {
                        return new MessageModel<string>()
                        {
                            msg = "获取失败！",
                            success = false,
                            response = data
                        };
                    }
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "获取失败",
                        success = false
                    };
                }
            }
            catch (Exception ex)
            {

                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<string>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 3.根据缴款码获取缴款情况
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<string>> GetPayResult(string slbh,string userName)
        {
            try
            {
                var data = await this._payMentServices.GetPayResult(slbh, userName);
                if (!string.IsNullOrEmpty(data))
                {
                    if(data == "200")
                    {
                        return new MessageModel<string>()
                        {
                            msg = "已缴款",
                            success = true,
                            response = data
                        };
                    }
                    else
                    {
                        return new MessageModel<string>()
                        {
                            msg = "未缴款",
                            success = false,
                            response = data
                        };
                    }
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "获取失败",
                        success = false
                    };
                }
            }
            catch (Exception ex)
            {

                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<string>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 4.根据缴款码获取电子票据信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<string>> GetGenerateTicket(string slbh)
        {
            try
            {
                var data = await this._payMentServices.GetGenerateTicket(slbh);
                if (!string.IsNullOrEmpty(data))
                {
                    if (data == "200")
                    {
                        return new MessageModel<string>()
                        {
                            msg = "开具电子票据成功",
                            success = true,
                            response = data
                        };
                    }
                    else
                    {
                        return new MessageModel<string>()
                        {
                            msg = "开具电子票据失败",
                            success = false,
                            response = data
                        };
                    }
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "获取失败",
                        success = false
                    };
                }
            }
            catch (Exception ex)
            {

                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<string>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 何雨檬调取财政接口
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public void SubmitPayHome()
        {
            try
            {
                _logger.LogDebug("何雨檬财政税收测试:" + DateTime.Now);
                this._payMentServices.SubmitPayHome();
                //return "调用成功";
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }
    }
}
