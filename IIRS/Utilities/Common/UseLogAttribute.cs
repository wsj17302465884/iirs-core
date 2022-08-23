using System;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using StackExchange.Profiling;

namespace IIRS.Utilities.Common
{
    /// <summary>
    /// 是否使用日志记录标签
    /// 使用方法：在 Controller 或 Function 上加入[ServiceFilter(typeof(UseLogAttribute))] 标签
    /// 注意，此方法必须用在需要登录的 Controller 或 Function 上
    /// </summary>
    public class UseLogAttribute : Attribute, IResourceFilter
    {
        private readonly ILogger<UseLogAttribute> _logger;
        private string _dataIntercept;
        private long _beginTime, _endTime;
        private bool _isError;

        public UseLogAttribute(ILogger<UseLogAttribute> logger)
        {
            _logger = logger;
            _dataIntercept = "";
            _beginTime = DateTime.Now.Ticks;
            _isError = false;
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            string UserName = context.HttpContext.User.Identity.Name;
            _dataIntercept =
                $"[Time]:         { DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss ffff")} \r\n" +
                $"[User]:         { UserName} \r\n" +
                $"[Method]:       { context.ActionDescriptor.RouteValues["controller"]} : {context.ActionDescriptor.RouteValues["action"]} \r\n";
            if (context.ActionDescriptor.Parameters.Count > 0)
            {
                _dataIntercept += $"[Parameters]:   ";
                for (int i = 0; i < context.ActionDescriptor.Parameters.Count; i++)
                {
                    if (i == 0)
                    {
                        _dataIntercept += $"{context.ActionDescriptor.Parameters[i].Name}: {context.ActionDescriptor.Parameters[i].ParameterType.Name}\r\n";
                    }
                    else
                    {
                        _dataIntercept += $"                {context.ActionDescriptor.Parameters[i].Name}: {context.ActionDescriptor.Parameters[i].ParameterType.Name}\r\n";
                    };
                }
            }

            try
            {
                MiniProfiler.Current.Step($"执行Service方法：{context.ActionDescriptor.RouteValues["controller"]}() -> ");
            }
            catch (Exception ex)// 同步2
            {
                _isError = true;
                LogEx(ex, ref _dataIntercept);
            }
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            if (!_isError)
            {
                _endTime = DateTime.Now.Ticks;
                _dataIntercept += $"[Duration]:     { (_endTime - _beginTime) / 10_000.0 }毫秒\r\n";
                //_dataIntercept += $"【执行完成结果】：{JsonConvert.SerializeObject(context.Result)}";
                _dataIntercept += "-----------------------------------------------------------\r\n";
                _logger.LogInformation(_dataIntercept);
            }
            else
            {
                _logger.LogError(_dataIntercept);
            }
        }

        private void LogEx(Exception ex, ref string dataIntercept)
        {
            if (ex != null)
            {
                //执行的 service 中，收录异常
                MiniProfiler.Current.CustomTiming("Errors：", ex.Message);
                //执行的 service 中，捕获异常
                dataIntercept += ($"[Exception]:    {ex.Message + ex.InnerException}\r\n");
                dataIntercept += "-----------------------------------------------------------\r\n";
            }
        }
    }
}