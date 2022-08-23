using System;
using System.Runtime.InteropServices;
using Autofac.Extensions.DependencyInjection;
using IIRS.Utilities.ConsoleHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;

namespace IIRS
{
    public class Program
    {
        /// <summary>
        /// 程序入口
        /// </summary>
        /// <param name="args">命令行参数</param>
        public static void Main(string[] args)
        {
            // 日志初始化并启动
            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
            
            try
            {
                //Console.Clear();
                //Console.Title = "服务器控制台";
                //logger.Debug("系统初始化");
                //Console.WriteLine($"服务器正在启动......");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception exception)
            {
                //ConsoleHelper.WriteErrorLine("服务器启动异常");
                logger.Error(exception, "抛出异常，结束程序运行。" + exception.Message);
                throw;
            }
            finally
            {
                NLog.LogManager.Shutdown();
                //Console.WriteLine($"服务器关闭");
            }
        }
            

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //<-- Autofac
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    // 移除原有所有日志提供服务
                    logging.ClearProviders();
                    // 设置日志最小记录级别
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    ;
                })
                .UseWindowsService()
                // 使用NLog
                .UseNLog();
            }
            else
            {
                return Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory()) //<-- Autofac
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
                .ConfigureLogging(logging =>
                {
                    // 移除原有所有日志提供服务
                    logging.ClearProviders();
                    // 设置日志最小记录级别
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                    ;
                })
                .UseSystemd()
                // 使用NLog
                .UseNLog();
            }
        }
    }
}
