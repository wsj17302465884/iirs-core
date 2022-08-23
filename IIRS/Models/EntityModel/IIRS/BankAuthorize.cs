using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///银行授权订单查询
    ///</summary>
    [SugarTable("BANKAUTHORIZE", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class BankAuthorize
    {
        public BankAuthorize()
        {


        }
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string BID { get; set; }

        /// <summary>
        /// Desc:证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DOCUMENTTYPE { get; set; }

        /// <summary>
        /// Desc:证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DOCUMENTNUMBER { get; set; }

        /// <summary>
        /// Desc:授权日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? AUTHORIZATIONDATE { get; set; }

        /// <summary>
        /// Desc:授权截至日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? AUTHORIZATIONDEADLINE { get; set; }

        /// <summary>
        /// 当前流程状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? STATUS { get; set; }

        /// <summary>
        /// 前一流程状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PRE_STATUS { get; set; }

        /// <summary>
        /// 权利人名称
        /// </summary>
        public string rightname { get; set; }

        /// <summary>
        /// 银行编码
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }


        [SugarColumn(IsIgnore = true)]
        public string FlowName { get; set; }


        public string Mortgagetype { get; set; }

        /// <summary>
        /// 被授权人
        /// </summary>
        public string AUTHORIZERPERSON { get; set; }
    }
}
