using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DJ_TSGL", Utilities.Common.SysConst.DB_CON_BDC)]
    public partial class DJ_TSGL
    {
        public DJ_TSGL()
        {


        }
        /// <summary>
        /// Desc:关联编码
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string GLBM { get; set; }

        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SLBH { get; set; }

        /// <summary>
        /// Desc:不动产类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCLX { get; set; }

        /// <summary>
        /// Desc:不动产图属统一编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TSTYBM { get; set; }

        /// <summary>
        /// Desc:不动产单元号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCDYH { get; set; }

        /// <summary>
        /// Desc:登记种类(审批通过后赋值)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DJZL { get; set; }

        /// <summary>
        /// Desc:关联模式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GLMS { get; set; }

        /// <summary>
        /// Desc:产生时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CSSJ { get; set; }

        /// <summary>
        /// Desc:现实\历史状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? LIFECYCLE { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJZH { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZDSLBH { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCDYH1 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZDZL { get; set; }

        /// <summary>
        /// Desc:限售起始日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? XSQSRQ { get; set; }

        /// <summary>
        /// Desc:限售截止日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? XSJZRQ { get; set; }

        /// <summary>
        /// Desc:原不动产单元号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YBDCDYH { get; set; }

        /// <summary>
        /// Desc:自建挂接编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GJBH_ZHH { get; set; }

        /// <summary>
        /// Desc:房地挂接类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FDGJLX { get; set; }

        /// <summary>
        /// Desc:自建
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJFJ { get; set; }

        /// <summary>
        /// Desc:中间值
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? FTJZMJ { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string dyr { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string zjlb_zwm { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool isOk { get; set; }

    }
}
