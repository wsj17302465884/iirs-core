using System;
using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DJ_SFD", SysConst.DB_CON_BDC)]
    public partial class DJ_SFD
    {
        public DJ_SFD()
        {


        }
        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string SLBH { get; set; }

        /// <summary>
        /// Desc:交费编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JFBH { get; set; }

        /// <summary>
        /// Desc:项目名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XMMC { get; set; }

        /// <summary>
        /// Desc:交费单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JFDW { get; set; }

        /// <summary>
        /// Desc:通讯地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TXDZ { get; set; }

        /// <summary>
        /// Desc:电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DH { get; set; }

        /// <summary>
        /// Desc:交费类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JFLX { get; set; }

        /// <summary>
        /// Desc:经办人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JBR { get; set; }

        /// <summary>
        /// Desc:经办日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? JBRQ { get; set; }

        /// <summary>
        /// Desc:经办意见
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JBYJ { get; set; }

        /// <summary>
        /// Desc:审核人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SHR { get; set; }

        /// <summary>
        /// Desc:审核意见
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SHYJ { get; set; }

        /// <summary>
        /// Desc:审核日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? SHRQ { get; set; }

        /// <summary>
        /// Desc:应收金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? YSJE { get; set; }

        /// <summary>
        /// Desc:实收金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? SSJE { get; set; }

        /// <summary>
        /// Desc:收款人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SKR { get; set; }

        /// <summary>
        /// Desc:收款日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? SKRQ { get; set; }

        /// <summary>
        /// Desc:收款意见
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SKYJ { get; set; }

        /// <summary>
        /// Desc:受理人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SLR { get; set; }

        /// <summary>
        /// Desc:打印状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYZT { get; set; }

        /// <summary>
        /// Desc:收费状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SFZT { get; set; }

        /// <summary>
        /// Desc:合并单号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HBDH { get; set; }

        /// <summary>
        /// Desc:打印人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYR { get; set; }

        /// <summary>
        /// Desc:打印时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? DYSJ { get; set; }

        /// <summary>
        /// Desc:合并人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HBR { get; set; }

        /// <summary>
        /// Desc:合并时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? HBSJ { get; set; }

        /// <summary>
        /// Desc:收费备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SFBZ { get; set; }

        /// <summary>
        /// Desc:营业税
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? YYS { get; set; }

        /// <summary>
        /// Desc:个人所得税
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? GRSDS { get; set; }

        /// <summary>
        /// Desc:契税
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? QS { get; set; }

        /// <summary>
        /// Desc:土地增值税
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TDZZS { get; set; }

        /// <summary>
        /// Desc:发票号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FPH { get; set; }

        /// <summary>
        /// Desc:核收金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HSJE { get; set; }

        /// <summary>
        /// Desc:上次打印人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYR1 { get; set; }

        /// <summary>
        /// Desc:上次打印时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? DYSJ1 { get; set; }

        /// <summary>
        /// Desc:上次核收金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HSJE1 { get; set; }

        /// <summary>
        /// Desc:集中缴费情况
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JZJF { get; set; }
        /// <summary>
        /// 非税发票号（用于对照非税缴费号）
        /// </summary>
        public string FSFPH { get; set; }
        /// <summary>
        /// 非税缴费状态（用于判断是否存在或正在网上缴费）
        /// </summary>
        public string FSJFZT { get; set; }
        /// <summary>
        /// 非税缴费二维码占位
        /// </summary>
        public string FSJFEWM { get; set; }

    }
}
