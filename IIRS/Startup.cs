using System.Linq;
using System.Reflection;
using System.Text;
using Autofac;
using IIRS.Extensions;
using IIRS.Utilities;
using IIRS.Utilities.Common;
using IIRS.Utilities.ConsoleHelper;
using IIRS.Utilities.ContractResolver;
using IIRS.Utilities.Filter;
using IIRS.Utilities.HttpContextUser;
using IIRS.Utilities.SignalRHelper;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace IIRS
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Env { get; }
        /// <summary>
        /// 启动类初始化
        /// </summary>
        /// <param name="configuration">系统配置属性</param>
        /// <param name="env">环境变量</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            Env = env;
            // 系统参数读取。因为只在系统启动时读取一次，没必要依赖注入，简单点写
            new AppsettingsUtility().Initial(configuration);
        }

        /// <summary>
        /// 运行时将调用此方法。 使用此方法将服务添加到容器。
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            // 加入简单的缓存
            services.AddMemoryCacheSetup();
            //ConsoleHelper.WriteSuccessLine($"加载缓存完成");
            // 加入数据库连接
            services.AddSqlsugarSetup();
            //ConsoleHelper.WriteSuccessLine($"连接数据库完成");
            // APIController
            services.AddControllers()
                //.AddControllersAsServices()
                ;
            //ConsoleHelper.WriteSuccessLine($"加载控制器完成");
            // 加入 MiniProfiler
            services.AddMiniProfilerSetup();
            // 添加Swagger服务
            services.AddSwaggerSetup();
            //// 添加任务
            //services.AddJobSetup();
            //启用压缩传输
            services.AddResponseCompression();
            // 因为需要在系统中访问 HttpContext 对象，因此在系统服务中依赖注入
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // 依赖注入自定义用户对象
            services.AddScoped<IUser, AspNetUser>();
            // 添加自定义授权
            services.AddAuthorizationSetup();
            //ConsoleHelper.WriteSuccessLine($"加载认证模块完成");
            // 为 SignalR 配置跨域
            services.AddCors(c =>
            {
                // 添加一个策略
                c.AddPolicy("SignalRCors", policy =>
                {
                    policy
                    .AllowAnyHeader()                           // 允许任何请求标头
                    .AllowAnyMethod()                           // 允许任何方法
                    .AllowCredentials()                         // 允许跨域凭据
                    .SetIsOriginAllowed(hostName => true);      // 为基础策略设置指定的值
                });
            });
            // 添加SignalR服务
            services.AddSignalR()
                //.AddMessagePackProtocol()                     // 添加MessagePack传输协议支持
                ;
            //ConsoleHelper.WriteSuccessLine($"加载消息服务完成");
            // 全局路由前缀
            services.AddControllers(o =>
            {
                // 统一修改路由
                o.Conventions.Insert(0, new GlobalRoutePrefixFilter(new RouteAttribute(RoutePrefix.Name)));
            })
            // 全局配置Json序列化处理
            .AddNewtonsoftJson(options =>
            {
                // 忽略循环引用
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                //// 不使用驼峰样式的key
                //options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                // 全部key转换为小写，自制
                options.SerializerSettings.ContractResolver = new LowerCasePropertyNames();
                // 置时间格式
                //options.SerializerSettings.DateFormatString = "yyyy-MM-dd";
            });

            services.Configure<FormOptions>(x =>
            {
                //x.ValueLengthLimit = int.MaxValue;
                x.MultipartBodyLengthLimit = 1073741824L; // 1024MB int.MaxValue
            });

            // 全局注册日志标签类
            services.AddScoped<UseLogAttribute>();
            //ConsoleHelper.WriteSuccessLine($"加载日志记录模块完成");
        }

        /// <summary>
        /// Autofac 自动化依赖注入，注意需要在 Program.cs 中添加
        /// UseServiceProviderFactory(new AutofacServiceProviderFactory())
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {

            #region 带有接口层的服务注入

            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(a => a.Name.EndsWith("Repository"))
                .AsImplementedInterfaces()
                .InstancePerDependency()
                //.EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
                //.InterceptedBy(typeof(LogAOP))
                ;

            builder.RegisterAssemblyTypes(typeof(Program).Assembly)
                .Where(a => a.Name.EndsWith("Services"))
                .AsImplementedInterfaces()
                .InstancePerDependency()
                //.EnableInterfaceInterceptors()//引用Autofac.Extras.DynamicProxy;
                //.InterceptedBy(typeof(LogAOP))
                ;

            #endregion


            #region 没有接口的服务，只能注入该类中的虚方法

            ////发现和登录存在冲突，删除，后续研究解决
            //builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(Love)))
            //    .EnableClassInterceptors();

            #endregion
        }

        /// <summary>
        /// 运行时将调用此方法。 使用此方法来配置HTTP请求管道。
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // 如果当前为开发环境
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                
            }

            #region Swagger服务
            // 使用Swagger服务
            app.UseSwagger();
            // 使用SwaggerUI界面
            app.UseSwaggerUI(c =>
            {
                //根据版本名称倒序 遍历展示
                var ApiName = AppsettingsUtility.SiteConfig.ApiName;
                typeof(ApiVersions).GetEnumNames().OrderByDescending(e => e).ToList().ForEach(version =>
                {
                    c.SwaggerEndpoint($"/swagger/{version}/swagger.json", $"{ApiName} {version}");
                });
                // 将swagger首页，设置成我们自定义的页面，记得这个字符串的写法：解决方案名.index.html，并且该文件属性为嵌入的资源
                c.IndexStream = () => GetType().GetTypeInfo().Assembly.GetManifestResourceStream("IIRS.index.html");
                // 路径配置，设置为空，表示直接在根域名访问该文件
                c.RoutePrefix = "";
            });
                #endregion

            app.UseResponseCompression();

            // 跳转https
            //app.UseHttpsRedirection();
            // 使用静态文件
            app.UseStaticFiles();
            // 使用cookie
            app.UseCookiePolicy();
            // 返回错误码
            app.UseStatusCodePages();//把错误码返回前台，比如是404
            // Routing
            app.UseRouting();
            // 跨域
            app.UseCors("SignalRCors");

            // 先开启认证
            app.UseAuthentication();
            // 然后是授权中间件
            app.UseAuthorization();
            // 使用 MiniProfiler
            app.UseMiniProfiler();
            // 添加终结点
            app.UseEndpoints(endpoints =>
            {
                // 控制器路由终结点
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                // SignalR 终结点
                endpoints.MapHub<MessageHub>(AppsettingsUtility.SiteConfig.SignalRPath);
            });
            //ConsoleHelper.WriteSuccessLine($"服务器启动完成，开始监听服务。");
            //ConsoleHelper.WriteInfoLine($"按 Ctrl + C 结束服务器运行");
        }
    }
}
