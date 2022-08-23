using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.IIRS
{
    public class AuthorizedHouseVModel
    {
        #region
        [SugarColumn(IsPrimaryKey = true)]
        public string BID { get; set; }

        /// <summary>
        /// Desc:证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string zjlb { get; set; }

        /// <summary>
        /// Desc:证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string dyr_zjhm { get; set; }

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
        /// 权利人名称
        /// </summary>
        public string dyr { get; set; }

        /// <summary>
        /// 银行编码
        /// </summary>
        public string BankCode { get; set; }

        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string YHYTSHXYDM { get; set; }
        
        /// <summary>
        /// 抵押权人
        /// </summary>
        public string DYQRMC { get; set; }

        public string SJR { get; set; }
        #endregion
        //抵押物
        public class PawmVModel
        {
            /// <summary>
            /// Desc:主键
            /// Default:
            /// Nullable:False
            /// </summary>           
            [SugarColumn(IsPrimaryKey = true)]
            public string OID { get; set; }

            /// <summary>
            /// Desc:关联bid
            /// Default:
            /// Nullable:True
            /// </summary>           
            public string BID { get; set; }

            /// <summary>
            /// Desc:不动产证号
            /// Default:
            /// Nullable:True
            /// </summary>           
            public string bdczh { get; set; }

            /// <summary>
            /// Desc:受理编号
            /// Default:
            /// Nullable:True
            /// </summary>           
            public string slbh { get; set; }

            

            /// <summary>
            /// Desc:房屋ID或宗地ID
            /// Default:
            /// Nullable:True
            /// </summary>           
            public string tstybm { get; set; }

            /// <summary>
            /// Desc:地址
            /// Default:
            /// Nullable:True
            /// </summary>           
            public string zl { get; set; }

            /// <summary>
            /// Desc:授权日期
            /// Default:
            /// Nullable:True
            /// </summary>           
            public DateTime? AUTHORIZATIONDATE { get; set; }

            /// <summary>
            /// 权利人名称
            /// </summary>
            public string qlrmc { get; set; }

            public string qllx { get; set; }
            public string qlxz { get; set; }
            public decimal? jzmj { get; set; }
            public string bdclx { get; set; }
            public string tdqllx { get; set; }
            public string tdqlxz { get; set; }
            public decimal? dytdmj { get; set; }
            public decimal? gytdmj { get; set; }
            public string bdcdyh { get; set; }
        }

        public List<PawmVModel> selectHouseInfo { get; set; }
    }
}
