using System;
using IIRS.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace IIRS.Utilities.SwaggerHelper
{
    /// <summary>
    /// 自定义路由 /{RoutePrefix}/{version}/[controler]/[action]
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class CustomRouteAttribute : RouteAttribute, IApiDescriptionGroupNameProvider
    {

        /// <summary>
        /// 分组名称，实现接口 IApiDescriptionGroupNameProvider
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 无参数构造函数，继承基类路由
        /// </summary>
        public CustomRouteAttribute() : base($"/{AppsettingsUtility.SiteConfig.RoutePrefix}/[controller]/[action]")
        {
        }

        /// <summary>
        /// 自定义路由前缀构造函数，继承基类路由
        /// </summary>
        /// <param name="actionName"></param>
        public CustomRouteAttribute(string actionName = "") : base($"/{AppsettingsUtility.SiteConfig.RoutePrefix}/[controller]/{actionName}")
        {
        }

        /// <summary>
        /// 自定义版本构造函数，继承基类路由
        /// </summary>
        /// <param name="version"></param>
        public CustomRouteAttribute(ApiVersions version) : base($"/{AppsettingsUtility.SiteConfig.RoutePrefix}/{version.ToString()}/[controller]/[action]")
        {
            GroupName = version.ToString();
        }

        /// <summary>
        /// 自定义版本+自定义路由前缀构造函数，继承基类路由
        /// </summary>
        /// <param name="version"></param>
        /// <param name="actionName"></param>
        public CustomRouteAttribute(ApiVersions version, string actionName = "") : base($"/{AppsettingsUtility.SiteConfig.RoutePrefix}/{version.ToString()}/[controller]/{actionName}")
        {
            GroupName = version.ToString();
        }
    }
}
