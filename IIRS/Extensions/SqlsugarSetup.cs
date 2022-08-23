using System;
using System.Collections.Generic;
using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Repository;
using IIRS.Repository.Base;
using IIRS.Utilities;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;

namespace IIRS.Extensions
{
    /// <summary>
    /// SqlSuger 安装类
    /// </summary>
    public static class SqlsugarSetup
    {
        /// <summary>
        /// 安装 SqlSuger 方法
        /// </summary>
        /// <param name="services"></param>
        public static void AddSqlsugarSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddScoped<ISqlSugarClient>(o =>
            {
                var listConfig = new List<ConnectionConfig>();

                AppsettingsUtility.SiteConfig.DBS.ForEach(m =>
                {
                    if (m.Enabled)
                    {
                        listConfig.Add(new ConnectionConfig()
                        {
                            ConfigId = m.ConnId.ToLower(),      //连接命，全部小写，避免混乱
                            ConnectionString = m.Connection,    //连接串
                            DbType = (DbType)m.DbType,          //数据库类型
                            IsAutoCloseConnection = true,       //是否自动关闭连接
                            IsShardSameThread = true,           //相同线程是否使用同一个SqlConnection
                            AopEvents = new AopEvents
                            {
                                OnLogExecuting = (sql, p) =>
                                {
                                    // 多库操作的话，此处暂时无效果
                                }
                            },
                            MoreSettings = new ConnMoreSettings()
                            {
                                IsAutoRemoveDataCache = true
                            }
                        });
                    };
                });
                return new SqlSugarClient(listConfig);
            });

            services.AddScoped<IDBTransManagement, DBTransManagement>();
        }
    }
}
