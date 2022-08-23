using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.Tax
{
    ///<summary>
    ///存量房交易购方历史表
    ///</summary>
    [SugarTable("TAX_EXISTING_HOME_BUYER_H", SysConst.DB_CON_TAX)]
    public partial class TAX_EXISTING_HOME_BUYER_HISTORY
    {
           public TAX_EXISTING_HOME_BUYER_HISTORY(){


           }
           /// <summary>
           /// Desc:主键编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string PK {get;set;}

           /// <summary>
           /// Desc:交易流水号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string TAX_PK {get;set;}

           /// <summary>
           /// Desc:纳税人识别号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string NSRSBH {get;set;}

           /// <summary>
           /// Desc:姓名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string NSRMC {get;set;}

           /// <summary>
           /// Desc:国籍
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string GJ_DM {get;set;}

           /// <summary>
           /// Desc:证件类型
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SFZJZL_DM {get;set;}

           /// <summary>
           /// Desc:证件号码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SFZJHM {get;set;}

           /// <summary>
           /// Desc:所在份额
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal SZFE {get;set;}

           /// <summary>
           /// Desc:房屋套次
           /// Default:
           /// Nullable:False
           /// </summary>           
           //public string FWTC_DM {get;set;}

           /// <summary>
           /// Desc:地址
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string DZ {get;set;}

           /// <summary>
           /// Desc:联系电话（卖方）
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string LXDH {get;set;}
        public string SLBH { get;  set; }
        public decimal JYBL { get;  set; }
    }
}
