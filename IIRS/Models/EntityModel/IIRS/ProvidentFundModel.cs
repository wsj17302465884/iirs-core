using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.IIRS
{
    [SugarTable("PROVIDENTFUND", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class ProvidentFundModel
    {
        public ProvidentFundModel()
        {

        }
        /// <summary>
        /// 主键
        /// </summary>
        public Guid pid { get; set; }
        /// <summary>
        /// 权利人名称
        /// </summary>
        public string qlrmc { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string zjhm { get; set; }

        /// <summary>
        /// 权利人名称1
        /// </summary>
        public string qlrmc1 { get; set; }
        /// <summary>
        /// 证件号码1
        /// </summary>
        public string zjhm1 { get; set; }
        /// <summary>
        /// 查询结果
        /// </summary>
        public string result { get; set; }

        public string org_name { get; set; }
        public string user_name { get; set; }

        public int housecount { get; set; }
        /// <summary>
        /// 查询时间
        /// </summary>
        public DateTime queryDate { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string conditionQuery { get; set; }

        /// <summary>
        /// PDF文件
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string PDFFile { get; set; }

        ///// <summary>
        ///// 不动产证号
        ///// </summary>
        //[SugarColumn(IsIgnore = true)]
        //public string bdczh { get; set; }
    }
}
