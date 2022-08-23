using System.Collections.Generic;
using IIRS.Models.ServerModel;
using Microsoft.Extensions.Configuration;

namespace IIRS.Utilities
{
    /// <summary>
    /// 系统设置工具类
    /// </summary>
    public class AppsettingsUtility
    {
        public static SiteConfig SiteConfig { get; private set; }

        /// <summary>
        /// 将配置项的值赋值给属性
        /// </summary>
        /// <param name="configuration"></param>
        public void Initial(IConfiguration configuration)
        {
            SiteConfig siteConfig = new SiteConfig
            {
                //注意：可以使用冒号来获取内层的配置项
                ApiName = configuration["SiteConfig:ApiName"],
                XMLDoc = configuration["SiteConfig:XMLDoc"],
                RoutePrefix = configuration["SiteConfig:RoutePrefix"] == "" ? "api" : configuration["SiteConfig:RoutePrefix"],
                MainDB = configuration["SiteConfig:MainDB"],
                DBS = configuration.GetSection("SiteConfig:DBS").Get<List<MutiDBOperate>>(),
                SignalRPath = configuration["SiteConfig:SignalRPath"],
                Encrypt = new Encrypt
                {
                    Secret = configuration["SiteConfig:Encrypt:Secret"],
                    Issuer = configuration["SiteConfig:Encrypt:Issuer"],
                    Audience = configuration["SiteConfig:Encrypt:Audience"]
                }
            };
            SiteConfig = siteConfig;
        }
    }
}
