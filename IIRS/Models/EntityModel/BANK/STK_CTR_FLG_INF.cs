using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///存量房合同备案信息
    ///</summary>
    [SugarTable("STK_CTR_FLG_INF", SysConst.DB_CON_BANK)]
    public partial class STK_CTR_FLG_INF
    {
        /// <summary>
        /// 存量房合同备案信息
        /// </summary>
        public STK_CTR_FLG_INF()
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
        /// Desc:存量房合同备案ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CLFHTBAXX_ID { get; set; }

        /// <summary>
        /// Desc:存量房买卖合同编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string STK_BUYSELL_CTR_ID { get; set; }

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
        /// Desc:买受人信息ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MSRXX_ID { get; set; }

        /// <summary>
        /// Desc:出卖人信息ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CSRXX_ID { get; set; }

    }
}
