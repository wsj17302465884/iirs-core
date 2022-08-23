using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///财务收费单续表
    ///</summary>
    [SugarTable("SFD_FB_INFO", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class SFD_FB_INFO
    {
        ///<summary>
        ///财务收费单续表
        ///</summary>
        public SFD_FB_INFO()
        {


        }
        /// <summary>
        /// Desc:财务收费单续表编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        [JsonProperty("CWSFDXBBH")]
        public string CWSFDXBBH { get; set; }

        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SLBH")] 
        public string SLBH { get; set; }

        /// <summary>
        /// Desc:清单序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("QDXH")] 
        public decimal? QDXH { get; set; }

        /// <summary>
        /// Desc:收费项目
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SFXM")] 
        public string SFXM { get; set; }

        /// <summary>
        /// Desc:计量单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("JLDW")] 
        public string JLDW { get; set; }

        /// <summary>
        /// Desc:数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SL")] 
        public decimal? SL { get; set; }

        /// <summary>
        /// Desc:收费标准
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("SFBZ")] 
        public decimal? SFBZ { get; set; }

        /// <summary>
        /// Desc:核收金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("HSJE")] 
        public decimal? HSJE { get; set; }

        /// <summary>
        /// Desc:减免金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("JMJE")] 
        public decimal? JMJE { get; set; }

        /// <summary>
        /// Desc:减免原因
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("JMYY")] 
        public string JMYY { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("BZ")] 
        public string BZ { get; set; }

        /// <summary>
        /// Desc:清单类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        [JsonProperty("QDLX")] 
        public string QDLX { get; set; }
        /// <summary>
        /// 登记信息主键
        /// </summary>
        [JsonProperty("XID")]
        public string XID { get; set; }
        

    }
}
