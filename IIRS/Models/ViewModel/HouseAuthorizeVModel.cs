using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel
{
    public class HouseAuthorizeVModel
    {
        public HouseAuthorizeVModel()
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
        /// Desc:状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? STATUS { get; set; }

        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>  
        public string OID { get; set; }

        /// <summary>
        /// Desc:不动产证号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CERTIFICATENUMBER { get; set; }

        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ACCEPTANCENUMBER { get; set; }

        /// <summary>
        /// Desc:状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? houseStatus { get; set; }

        /// <summary>
        /// Desc:房屋ID或宗地ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string NUMBERID { get; set; }

        /// <summary>
        /// Desc:地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ADDRESS { get; set; }

        public string rightname { get; set; }
    }
}
