using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("YY_INFO", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class YY_INFO
    {
        public YY_INFO()
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
        /// Desc:相关不动产证
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XGZH { get; set; }

        /// <summary>
        /// Desc:不动产单元号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCDYH { get; set; }

        /// <summary>
        /// Desc:异议事项
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YYSX { get; set; }

        /// <summary>
        /// Desc:申请人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SQR { get; set; }

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
        /// Desc:异议不动产证明号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCZMH { get; set; }

        /// <summary>
        /// Desc:省市简称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SSJC { get; set; }

        /// <summary>
        /// Desc:机构简称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JGJC { get; set; }

        /// <summary>
        /// Desc:发证年度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FZND { get; set; }

        /// <summary>
        /// Desc:证书号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZSH { get; set; }

        /// <summary>
        /// Desc:发证机关
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FZJG { get; set; }

        /// <summary>
        /// Desc:发证日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? FZRQ { get; set; }

        /// <summary>
        /// Desc:证书序列号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZSXLH { get; set; }

        /// <summary>
        /// Desc:打印次数
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? DYCS { get; set; }

        /// <summary>
        /// Desc:归档号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GDH { get; set; }

        /// <summary>
        /// Desc:档案密集
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DAMJ { get; set; }

        /// <summary>
        /// Desc:其他
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QT { get; set; }

        /// <summary>
        /// Desc:附记
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FJ { get; set; }

        /// <summary>
        /// Desc:核定批次号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HDPCH { get; set; }

        /// <summary>
        /// Desc:缮证批次号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SZPCH { get; set; }

        /// <summary>
        /// Desc:现实\历史状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? LIFECYCLE { get; set; }

        /// <summary>
        /// Desc:缮证人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SZR { get; set; }

        /// <summary>
        /// Desc:登记日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? DJRQ { get; set; }

        /// <summary>
        /// Desc:登簿人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DBR { get; set; }

        /// <summary>
        /// Desc:审批备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPBZ { get; set; }

        /// <summary>
        /// Desc:位置
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string WZ { get; set; }

        /// <summary>
        /// Desc:电子证书序列号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DZZSXLH { get; set; }

        /// <summary>
        /// Desc:区分区域
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QFQY { get; set; }
        /// <summary>
        /// 登记信息主键
        /// </summary>
        public string XID { get; set; }

    }
}
