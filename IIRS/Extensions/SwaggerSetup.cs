using System;
using System.IO;
using System.Linq;
using IIRS.Utilities;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using NLog;
using Swashbuckle.AspNetCore.Filters;

namespace IIRS.Extensions
{
    /// <summary>
    /// Swagger 设置类
    /// </summary>
    public static class SwaggerSetup
    {
        /// <summary>
        /// 日志
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// 依赖注入 Swagger 服务
        /// </summary>
        /// <param name="services">ServiceCollection对象</param>
        public static void AddSwaggerSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            var basePath = AppContext.BaseDirectory;
            var ApiName = AppsettingsUtility.SiteConfig.ApiName;

            services.AddSwaggerGen(c =>
            {
                //遍历出全部的版本，做文档信息展示
                typeof(ApiVersions).GetEnumNames().ToList().ForEach(version =>
                {
                    c.SwaggerDoc(version, new OpenApiInfo
                    {
                        Version = version,
                        Title = $"{ApiName}接口文档——Netcore 3.1",
                        Description = $"{ApiName} HTTP API " + version,
                        Contact = new OpenApiContact {
                            Name = ApiName,
                            Email = "IIRS@xxx.com",
                            Url = new Uri("https://www.baidu.com")
                        },
                        License = new OpenApiLicense {
                            Name = ApiName+" 官方文档",
                            Url = new Uri("http://www.baidu.com")
                        }
                    });
                    c.OrderActionsBy(o => o.RelativePath);
                });


                try
                {
                    var xmlPath = Path.Combine(basePath, AppsettingsUtility.SiteConfig.XMLDoc);//这个就是刚刚配置的xml文件名
                    c.IncludeXmlComments(xmlPath, true);
                }
                catch (Exception ex)
                {
                    logger.Error($"{AppsettingsUtility.SiteConfig.XMLDoc} 丢失，请检查配置文件设置，并将其拷贝到程序运行目录。\n" + ex.Message);
                }

                // 开启加权小锁
                c.OperationFilter<AddResponseHeadersFilter>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();

                // 在header中添加token，传递到后台
                c.OperationFilter<SecurityRequirementsOperationFilter>();


                // 必须是 oauth2
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "JWT授权(数据将在请求头中进行传输) 直接在下框中输入Bearer {token}（注意两者之间是一个空格）\"",
                    Name = "Authorization",//jwt默认的参数名称
                    In = ParameterLocation.Header,//jwt默认存放Authorization信息的位置(请求头中)
                    Type = SecuritySchemeType.ApiKey
                });
            });
        }
    }
}
