using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DJ_XGDJGL", Utilities.Common.SysConst.DB_CON_BDC)]
    public partial class DJ_XGDJGL
    {
        public DJ_XGDJGL()
        {


        }
        /// <summary>
        /// Desc:变更编码
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string BGBM { get; set; }

        /// <summary>
        /// Desc:子受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZSLBH { get; set; }

        /// <summary>
        /// Desc:父受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FSLBH { get; set; }

        /// <summary>
        /// Desc:变更日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? BGRQ { get; set; }

        /// <summary>
        /// Desc:变更类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BGLX { get; set; }

        /// <summary>
        /// Desc:相关证号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XGZH { get; set; }

        /// <summary>
        /// Desc:相关证类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XGZLX { get; set; }

        /// <summary>
        /// Desc:记录树形关系
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PID { get; set; }

        /// <summary>
        /// Desc:父受理编号备份
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FSLBH1 { get; set; }

        /// <summary>
        /// Desc:房地挂接类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FDGJLX { get; set; }

        /// <summary>
        /// Desc:用于数据处理（自建）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TS { get; set; }

        /// <summary>
        /// Desc:用于数据处理（自建）
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZHH { get; set; }

        /// <summary>
        /// Desc:子受理编号备份
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZSLBH1 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string 说明 { get; set; }

        /// <summary>
        /// Desc:顺序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SXH { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string XID { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string BDCZH { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string ZSLX { get; set; }

    }
}
