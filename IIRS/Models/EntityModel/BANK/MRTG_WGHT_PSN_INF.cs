using System;
using System.Linq;
using System.Text;
using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("MRTG_WGHT_PSN_INF", SysConst.DB_CON_BANK)]
    public partial class MRTG_WGHT_PSN_INF
    {
           public MRTG_WGHT_PSN_INF(){


           }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string MID { get; set; }
        /// <summary>
        /// Desc:抵押权人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_WGHT_PSN_NM {get;set;}

           /// <summary>
           /// Desc:抵押权人代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_WGHT_PSN_CD {get;set;}

           /// <summary>
           /// Desc:债权金额
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? CLM_NUM_AMT {get;set;}

           /// <summary>
           /// Desc:债务履行期限（债权确定期间、抵押期限）开始
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DBT_PRFMN_STDT {get;set;}

           /// <summary>
           /// Desc:债务履行期限（债权确定期间、抵押期限）截止
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DBT_PRFMN_EDDT {get;set;}

           

    }
}
