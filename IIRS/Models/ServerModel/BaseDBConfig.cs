namespace IIRS.Models.ServerModel
{
    /// <summary>
    /// 数据库类型枚举
    /// </summary>
    public enum DataBaseType
    {
        MySql = 0,
        SqlServer = 1,
        Sqlite = 2,
        Oracle = 3,
        PostgreSQL = 4
    }

    /// <summary>
    /// 数据库连接信息
    /// </summary>
    public class MutiDBOperate
    {
        public string ConnId { get; set; }
        public DataBaseType DbType { get; set; }
        public bool Enabled { get; set; }
        public string Connection { get; set; }
    }
}
