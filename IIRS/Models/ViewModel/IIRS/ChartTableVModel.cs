using SqlSugar;
using System;

namespace IIRS.Models.ViewModel
{
    public class ChartTableVModel
    {
        /// <summary>
        /// 操作员姓名
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string REALNAME { get; set; }

        /// <summary>
        /// Desc:操作员ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string USERID { get; set; }

        /// <summary>
        /// Desc:登记种类
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int DJZL { get; set; }

        /// <summary>
        /// Desc:件数
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int COUNT { get; set; }
    }
}
