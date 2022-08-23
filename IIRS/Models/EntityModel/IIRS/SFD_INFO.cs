using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///财务收费单
    ///</summary>
    [SugarTable("SFD_INFO", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class SFD_INFO
    {
        ///<summary>
        ///财务收费单
        ///</summary>
        public SFD_INFO()
        {


        }
        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        [JsonProperty("SLBH")]
        public string SLBH { get; set; }

        /// <summary>
        /// Desc:交费编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("JFBH")]
        public string JFBH { get; set; }

        /// <summary>
        /// Desc:项目名称
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("XMMC")]
        public string XMMC { get; set; }

        /// <summary>
        /// Desc:交费单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("JFDW")]
        public string JFDW { get; set; }

        /// <summary>
        /// Desc:通讯地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("TXDZ")]
        public string TXDZ { get; set; }

        /// <summary>
        /// Desc:电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("DH")]
        public string DH { get; set; }

        /// <summary>
        /// Desc:交费类型
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("JFLX")]
        public string JFLX { get; set; }

        /// <summary>
        /// Desc:经办人
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("JBR")]
        public string JBR { get; set; }

        /// <summary>
        /// Desc:经办日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("JBRQ")]
        public DateTime? JBRQ { get; set; }

        /// <summary>
        /// Desc:经办意见
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("JBYJ")]
        public string JBYJ { get; set; }

        /// <summary>
        /// Desc:审核人
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SHR")]
        public string SHR { get; set; }

        /// <summary>
        /// Desc:审核意见
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SHYJ")]
        public string SHYJ { get; set; }

        /// <summary>
        /// Desc:审核日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SHRQ")]
        public DateTime? SHRQ { get; set; }

        /// <summary>
        /// Desc:应收金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("YSJE")]
        public decimal? YSJE { get; set; }

        /// <summary>
        /// Desc:实收金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SSJE")]
        public decimal? SSJE { get; set; }

        /// <summary>
        /// Desc:收款人
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SKR")]
        public string SKR { get; set; }

        /// <summary>
        /// Desc:收款日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SKRQ")]
        public DateTime? SKRQ { get; set; }

        /// <summary>
        /// Desc:收款意见
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SKYJ")]
        public string SKYJ { get; set; }

        /// <summary>
        /// Desc:受理人
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SLR")]
        public string SLR { get; set; }

        /// <summary>
        /// Desc:打印状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("DYZT")]
        public string DYZT { get; set; }

        /// <summary>
        /// Desc:收费状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SFZT")]
        public string SFZT { get; set; }

        /// <summary>
        /// Desc:合并单号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("HBDH")]
        public string HBDH { get; set; }

        /// <summary>
        /// Desc:打印人
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("DYR")]
        public string DYR { get; set; }

        /// <summary>
        /// Desc:打印时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("DYSJ")]
        public DateTime? DYSJ { get; set; }

        /// <summary>
        /// Desc:合并人
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("HBR")]
        public string HBR { get; set; }

        /// <summary>
        /// Desc:合并时间
        /// Default:
        /// Nullable:True
        /// </summary>           

        [JsonProperty("HBSJ")] 
        public DateTime? HBSJ { get; set; }

        /// <summary>
        /// Desc:收费备注
        /// Default:
        /// Nullable:True
        /// </summary>           

        [JsonProperty("SFBZ")] 
        public string SFBZ { get; set; }

        /// <summary>
        /// Desc:营业税
        /// Default:
        /// Nullable:True
        /// </summary>           

        [JsonProperty("YYS")] 
        public decimal? YYS { get; set; }

        /// <summary>
        /// Desc:个人所得税
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("GRSDS")] 
        public decimal? GRSDS { get; set; }

        /// <summary>
        /// Desc:契税
        /// Default:
        /// Nullable:True
        /// </summary>           

        [JsonProperty("QS")] 
        public decimal? QS { get; set; }

        /// <summary>
        /// Desc:土地增值税
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("TDZZS")] 
        public decimal? TDZZS { get; set; }

        /// <summary>
        /// Desc:发票号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("FPH")] 
        public string FPH { get; set; }

        /// <summary>
        /// Desc:核收金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("HSJE")] 
        public decimal? HSJE { get; set; }

        /// <summary>
        /// Desc:上次打印人
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("DYR1")] 
        public string DYR1 { get; set; }

        /// <summary>
        /// Desc:上次打印时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("DYSJ1")] 
        public DateTime? DYSJ1 { get; set; }

        /// <summary>
        /// Desc:上次核收金额
        /// Default:
        /// Nullable:True
        /// </summary>           

        [JsonProperty("HSJE1")] 
        public decimal? HSJE1 { get; set; }

        /// <summary>
        /// Desc:集中缴费情况
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("JZJF")] 
        public string JZJF { get; set; }

        /// <summary>
        /// 登记信息主键
        /// </summary>

        [JsonProperty("XID")] 
        public string XID { get; set; }

    }
}
