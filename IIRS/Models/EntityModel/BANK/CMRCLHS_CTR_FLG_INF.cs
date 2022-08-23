using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///商品房合同备案信息
    ///</summary>
    [SugarTable("CMRCLHS_CTR_FLG_INF", SysConst.DB_CON_BANK)]
    public partial class CMRCLHS_CTR_FLG_INF
    {
        /// <summary>
        /// 商品房合同备案信息
        /// </summary>
        public CMRCLHS_CTR_FLG_INF()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string SID { get; set; }

        /// <summary>
        /// Desc:商品房合同备案信息ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPFHTBAXX_ID { get; set; }

        /// <summary>
        /// Desc:商品房买卖合同编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CMRCLHS_BUYSELL_CTR_ID { get; set; }

        /// <summary>
        /// Desc:预售人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PRSL_PSN_NM { get; set; }

        /// <summary>
        /// Desc:成交额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? MDL_AMT { get; set; }

        /// <summary>
        /// Desc:登记备案日期时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RGS_FLG_DT_TM { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPFHTBAMSRXX_ID { get; set; }

    }
}
