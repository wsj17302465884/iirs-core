using SqlSugar;
using System;

namespace IIRS.Models.ViewModel
{
    public class LSTFileVModel
    {
        /// <summary>
        /// 操作员姓名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string USERNAME { get; set; }

        /// <summary>
        /// Desc:文档名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string TITLE { get; set; }

        /// <summary>
        /// Desc:所属类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SSLB { get; set; }
        /// <summary>
        /// Desc:路径
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FILEPATH { get; set; }
    }
}
