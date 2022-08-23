using System;
using IIRS.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace IIRS.Extensions
{
    /// <summary>
    /// 配置任务辅助类
    /// </summary>
    public static class JobSetup
    {
        /// <summary>
        /// 添加任务服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddJobSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddHostedService<TestTaskService>();
        }
    }
}
