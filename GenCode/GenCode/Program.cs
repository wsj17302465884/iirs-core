using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using IIRS.Models.ServerModel;
using IIRS.Utilities;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using Umfrage;
using Umfrage.Abstractions;
using Umfrage.Builders;
using Umfrage.Builders.Abstractions;
using Umfrage.Implementations;

namespace GenCode
{
    class Program
    {
        /// <summary>
        /// 模板
        /// </summary>
        private static string[] _entityClassTemplateFile = new string[]
        {
            "Entity",
            "IRepository",
            "Repository",
        };

        /// <summary>
        /// 程序入口
        /// </summary>
        /// <param name="args"></param>
        public static async System.Threading.Tasks.Task Main(string[] args)
        {
            // 加载 appsettings.json 配置文件
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
            // 从配置文件中读取设置
            new AppsettingsUtility().Initial(config);

            // 建立一个终端
            var terminal = new UserTerminal();

            // 为终端建立一个问题集
            IQuestionnaire questionnaire = new Questionnaire(terminal);

            // 设置系统欢迎信息
            questionnaire.Settings.WelcomeMessage = $"欢迎使用 {AppsettingsUtility.SiteConfig.ApiName} 代码生成器";

            // 校验规则方法 -> 长度必须大于0
            bool Validator(IQuestion x) => x.Answer.Length > 0;

            // 问题构造器
            IQuestionBuilder builder = new QuestionBuilder();

            // 命名空间
            var askNamespace = builder.Simple().Text("1. Please Provide Program Namespace")
                .AddValidation(Validator, "Namespace Must Be Provided.")
                .Build();
            questionnaire.Add(askNamespace);

            // 数据库选择
            IList<IOption> ts = new List<IOption>();
            AppsettingsUtility.SiteConfig.DBS.ForEach(m =>
            {
                var questionOption = new QuestionOption(m.ConnId);
                ts.Add(questionOption);
            });
            builder.List()
                .Text("2. Please Select Database(s) to Generate Code.")
                .AddOptions(ts)
                .AsCheckList()
                .AddValidation(x => {
                    var q = (CheckList)x;
                    return q.Answer.Trim() != String.Empty;
                })
                .WithErrorMessage("You Must Select At Least One Database.")
                .AddToQuestionnaire(questionnaire);

            // 确认选择
            var confirm = builder.Simple()
                .Text("3. Is Your Choice Correct?")
                .AsConfirm()
                .WithHint("Y / n")
                .AddValidation(x =>
                {
                    var q = (Confirm)x;
                    return q.PossibleAnswers.Contains(x.Answer);
                }, "Your Answer Can Only Be 'y' Or 'n'")
                .Build();
            questionnaire.Add(confirm);

            // 确认执行
            var process = builder.Simple()
                .Text("4. Will Be Generate Code File, Continue?")
                .AsConfirm()
                .WithHint("Y / n")
                .AddValidation(x =>
                {
                    var q = (Confirm)x;
                    return q.PossibleAnswers.Contains(x.Answer);
                }, "Your Answer Can Only Be 'y' Or 'n'")
                .Build();
            questionnaire.Add(process);

            // 开始执行
            questionnaire.Start();

            // 循环取问题
            while (questionnaire.CanProceed)
            {
                if (questionnaire.CurrentQuestion.Text == confirm.Text || questionnaire.CurrentQuestion.Text == process.Text)
                {
                    if (questionnaire.CurrentQuestion.Answer == "n")
                    {
                        System.Environment.Exit(0);
                    }
                }
                questionnaire.Next();
            }
            
            // 打印结果
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>();
            foreach (var q in questionnaire.ProcessedQuestions)
            {
                keyValuePairs.Add(q.Text, q.Answer);
            }

            var namespaceName = keyValuePairs["1. Please Provide Program Namespace"].ToString();

            var dbName = keyValuePairs["2. Please Select Database(s) to Generate Code."].ToString().Split(", ");

            terminal.Printer.WriteLine();
            terminal.Printer.Write("Starting Generat Files, Please Wait......");
            terminal.Printer.WriteLine();

            // 根据所选数据库
            foreach (var item in dbName)
            {

                // 建立数据库连接
                var dbs = CreateDBConnection(item);

                terminal.Printer.WriteLine();
                terminal.Printer.Write($"Starting Reading Tables From Database {item}, Please Wait......");
                terminal.Printer.WriteLine();
                terminal.Printer.WriteLine();

                // 数据库类型
                var dbType = (DbType)AppsettingsUtility.SiteConfig.DBS.Find(x => x.ConnId == item).DbType;
                // 查询库中所有表SQL语句
                var tblsql = GetQueryTablesSQL(item, dbType);
                // 数据库中所有表相关信息
                var tblrst = await dbs.Ado.SqlQueryAsync<TableInfo>(tblsql);

                // 循环获取表中所有字段信息
                for (int i = 0; i < tblrst.Count; i++)
                {
                    terminal.Printer.WriteLine($"Generating Entity Class of Table {tblrst[i].TableName}, Table's Description: {tblrst[i].TableDesc}");
                    terminal.Printer.WriteLine();

                    await Task.Run(async () =>
                    {
                        // 查询表中所有字段备注的SQL语句
                        var colsql = GetQueryColumnSQL(item, tblrst[i].TableName, dbType);
                        // 所有字段的备注信息
                        var colrst = await dbs.Ado.SqlQueryAsync<ColumnInfo>(colsql);
                        // 表中所有主键信息列表
                        var colpk = await GetPrimaryKeyByTableNamesAsync(dbs, tblrst[i].TableName, dbType);
                        // 表中的所有字段其他信息
                        List<DbColumnInfo> result = new List<DbColumnInfo>();
                        using (var colinfo = (DbDataReader)dbs.Ado.GetDataReader($"select * from {tblrst[i].TableName} where 1=2"))
                        {
                            var schemaTable = colinfo.GetSchemaTable();
                            foreach (System.Data.DataRow row in schemaTable.Rows)
                            {
                                DbColumnInfo column = new DbColumnInfo()
                                {
                                    TableName = tblrst[i].TableName,
                                    DataType = row["DataType"].ToString().Replace("System.", "").Trim(),
                                    IsNullable = row["AllowDBNull"].ObjToBool(),
                                    ColumnDescription = colrst.Find(x => x.ColumnName == row["ColumnName"].ToString()).ColumnDesc,
                                    DbColumnName = row["ColumnName"].ToString(),
                                    Length = row["ColumnSize"].ObjToInt(),
                                    Scale = row["numericscale"].ObjToInt()
                                };

                                // 如果数据库为 Oracle
                                if (dbType == DbType.Oracle)
                                {
                                    column.IsPrimarykey = colpk.Contains(row["ColumnName"].ToString());
                                }
                                result.Add(column);
                            }
                        }

                        for (int j = 0; j < result.Count; j++)
                        {
                            terminal.Printer.WriteLine("--------------------");
                            terminal.Printer.WriteLine($"Column Name: {result[j].DbColumnName}");
                            terminal.Printer.WriteLine($"Description: { result[j].ColumnDescription}");
                            terminal.Printer.WriteLine($"DataType: { result[j].DataType}");
                            terminal.Printer.WriteLine($"IsNullable: { result[j].IsNullable}");
                            terminal.Printer.WriteLine($"DbColumnName: { result[j].DbColumnName}");
                            terminal.Printer.WriteLine($"IsPrimarykey: { result[j].IsPrimarykey}");
                            terminal.Printer.WriteLine($"Length: { result[j].Length}");
                            terminal.Printer.WriteLine($"Scale: { result[j].Scale}");
                            terminal.Printer.WriteLine("--------------------");
                            terminal.Printer.WriteLine();
                        }
                    });
                    terminal.Printer.WriteLine($"Entity Class of Table {tblrst[i].TableName} Generated.");
                }

                // 生成实体类

            }



            // 保持终端不自动退出
            Console.ReadLine();
        }

        /// <summary>
        /// 获取查询库中所有表信息的SQL语句 参考了 SQLSugar.Tool
        /// </summary>
        /// <param name="DBName">数据库名</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>SQL字符串</returns>
        private static string GetQueryTablesSQL(string DBName, DbType dbType)
        {
            return dbType switch
            {
                DbType.MySql =>         $"SELECT TABLE_NAME as TableName, Table_Comment as TableDesc FROM INFORMATION_SCHEMA.TABLES where TABLE_SCHEMA = '{DBName}' order by TableName asc",
                DbType.Oracle =>        "select table_name as TableName,comments as TableDesc from user_tab_comments order by table_name asc",
                DbType.PostgreSQL =>    @"SELECT t2.tablename AS TableName, CAST(obj_description(relfilenode, 'pg_class') AS VARCHAR) AS TableDesc
                                        FROM pg_class t1
                                        LEFT JOIN pg_tables t2 ON t1.relname = t2.tablename
                                        WHERE t2.schemaname = 'public'
                                        ORDER BY t1.relname ASC",
                DbType.Sqlite =>        "SELECT name AS TableName, '' AS TableDesc FROM sqlite_master order by name asc",
                DbType.SqlServer =>     @"SELECT tbs.name TableName,ds.value TableDesc
                                        FROM sys.extended_properties ds
                                        LEFT JOIN sysobjects tbs ON ds.major_id=tbs.id
                                        WHERE ds.minor_id=0 and
                                        tbs.name='ScheduleRecords'",
                _ =>                    "",
            };
        }

        /// <summary>
        /// 获取查询表中所有字段信息的SQL语句 参考了 SQLSugar.Tool
        /// </summary>
        /// <param name="DBName">数据库ing</param>
        /// <param name="tableName">表名</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns></returns>
        private static string GetQueryColumnSQL(string DBName, string tableName, DbType dbType)
        {
            return dbType switch
            {
                DbType.MySql => $"select COLUMN_NAME as ColumnName,column_comment as ColumnDesc from INFORMATION_SCHEMA.Columns where table_name='{tableName}' and table_schema='{DBName}'",
                DbType.Oracle => $"select column_name as ColumnName,comments as ColumnDesc from user_col_comments where table_name = '{tableName}'",
                DbType.PostgreSQL => $@"SELECT col_description(A.attrelid, A.attnum) AS ColumnDesc, A.attname AS ColumnName
                                            FROM pg_class AS C, pg_attribute AS A 
                                            WHERE C.relname = '{tableName}' AND A.attrelid = C.oid AND A.attnum >0",
                DbType.Sqlite => $"PRAGMA table_info('{tableName}')",
                DbType.SqlServer => $"SELECT objname ColumnName,value ColumnDesc FROM ::fn_listextendedproperty (NULL, 'user', 'dbo', 'table', '{tableName}', 'column', DEFAULT)",
                _ => "",
            };
        }

        /// <summary>
        /// 获取查询表中的主键列表
        /// TODO 目前只做了Oracle部分
        /// </summary>
        /// <param name="db"></param>
        /// <param name="tableName"></param>
        /// <param name="dbType"></param>
        /// <returns></returns>
        private static async Task<List<string>> GetPrimaryKeyByTableNamesAsync(SqlSugarClient db, string tableName, DbType dbType)
        {
            return dbType switch
            {
                DbType.MySql => new List<string>(),
                DbType.Oracle => await db.Ado.SqlQueryAsync<string>(@$"select distinct cu.COLUMN_name KEYNAME
                            from user_cons_columns cu, user_constraints au
                            where cu.constraint_name = au.constraint_name
                            and au.constraint_type = 'P' and au.table_name = '{tableName}'"),
                DbType.PostgreSQL => new List<string>(),
                DbType.Sqlite =>new List<string>(),
                DbType.SqlServer => new List<string>(),
                _ => new List<string>()
            };
        }

        /// <summary>
        /// 建立数据库连接
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <returns></returns>
        private static SqlSugarClient CreateDBConnection(string dbName)
        {
            MutiDBOperate db = AppsettingsUtility.SiteConfig.DBS.Find(x => x.ConnId == dbName);

            var listConfig = new ConnectionConfig()
            {
                ConfigId = db.ConnId.ToLower(),      //连接命，全部小写，避免混乱
                ConnectionString = db.Connection,    //连接串
                DbType = (DbType)db.DbType,          //数据库类型
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
            };
            return new SqlSugarClient(listConfig);
        }

        internal class TableInfo
        {
            public string TableName { get; set; }
            public string TableDesc { get; set; }
        }

        internal class ColumnInfo
        {
            public string ColumnName { get; set; }
            public string ColumnDesc { get; set; }
        }
    }
}
