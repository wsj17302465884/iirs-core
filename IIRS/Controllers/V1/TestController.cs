using System.Collections.Generic;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;

namespace IIRS.Controllers.V1
{
    /// <summary>
    /// 第一版测试控制器
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V1)]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// Get
        /// </summary>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "第一版的 Test" };
        }

        /// <summary>
        /// 测试个路由
        /// </summary>
        /// <returns></returns>
        [HttpGet, HttpPost]
        public string TestAction()
        {
            return "Hello, World!";
        }
    }
}
