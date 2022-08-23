using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///订单房屋关联
    ///</summary>
    [SugarTable("ORDERHOUSEASSOCIATION", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class OrderHouseAssociation
    {
        public OrderHouseAssociation()
        {


        }
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

        /// <summary>
        /// Desc:授权日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? AUTHORIZATIONDATE { get; set; }

        /// <summary>
        /// 权利人名称
        /// </summary>
        public string rightname { get; set; }
        /// <summary>
        /// 权利类型
        /// </summary>
        public string qllx { get; set; }
        /// <summary>
        /// 权利性质
        /// </summary>
        public string qlxz { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? jzmj { get; set; }
        /// <summary>
        /// 
        /// </summary>不动产类型
        public string bdclx { get; set; }
        /// <summary>
        /// 土地权利类型
        /// </summary>
        public string tdqllx { get; set; }
        /// <summary>
        /// 土地权利性质
        /// </summary>
        public string tdqlxz { get; set; }
        /// <summary>
        /// 土地面积
        /// </summary>
        public decimal? tdmj { get; set; }
        public string bdcdyh { get; set; }

        /// <summary>
        /// 受理状态
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string flow_name { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string gname { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string xgzlx { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string newSlbh { get; set; }
        

    }
}
