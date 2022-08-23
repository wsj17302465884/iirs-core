using System;
using System.Threading;
using System.Threading.Tasks;
using IIRS.Utilities.ConsoleHelper;
using Microsoft.Extensions.Hosting;

namespace IIRS.Tasks
{
    public class TestTaskService : IHostedService, IDisposable
    {
        private Timer _timer;
 
        /// <summary>
        /// 这里可以通过依赖注入做很多很多事情
        /// </summary>
        public TestTaskService()
        {
        }

        /// <summary>
        /// 开始异步启动任务时做的工作，比如读取设置以设定计时器开始时间等等等等
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Console.WriteLine("测试任务正在启动......");

            _timer = new Timer(DoWork, null, TimeSpan.Zero,
                TimeSpan.FromSeconds(60));              //一分钟执行一次

            return Task.CompletedTask;
        }

        /// <summary>
        /// 实际做的各种工作，比如同步数据库、清理日志、后台下载点小电影啥的
        /// </summary>
        /// <param name="state"></param>
        private void DoWork(object state)
        {
            // 做了个简单的终端显示不同颜色的小工具
            //ConsoleHelper.WriteSuccessLine($"测试任务执行： {DateTime.Now}");
        }

        /// <summary>
        /// 异步任务停止时做的工作
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task StopAsync(CancellationToken cancellationToken)
        {
            //Console.WriteLine("测试任务正在停止......");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 销毁对象
        /// </summary>
        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
