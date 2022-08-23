using System;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace IIRS.Utilities.Filter
{
    /// <summary>
    /// IP地址检测
    /// </summary>
    public class ClientIdCheckFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 日志
        /// </summary>
        private readonly ILogger<ClientIdCheckFilter> _logger;

        private IConfiguration _configuration;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="configuration"></param>
        public ClientIdCheckFilter(ILogger<ClientIdCheckFilter> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var remoteIp = context.HttpContext.Connection.RemoteIpAddress;
            string[] ip = { };
            var _safelist = _configuration["SiteConfig:AllowIP"].ToString();
            if (_safelist.Trim() != "")
                ip = _safelist.Split(';');

            var bytes = remoteIp.GetAddressBytes();
            var badIp = true;
            if (ip.Count() > 0)
            {
                foreach (var address in ip)
                {
                    var testIp = IPAddress.Parse(address);
                    if (testIp.GetAddressBytes().SequenceEqual(bytes))
                    {
                        badIp = false;
                        break;
                    }
                }

                if (badIp)
                {
                    _logger.LogInformation($"已拒绝远程地址 {remoteIp} 访问");
                    context.Result = new StatusCodeResult(401);
                    return;
                }
            }
            base.OnActionExecuting(context);
        }
    }
}
