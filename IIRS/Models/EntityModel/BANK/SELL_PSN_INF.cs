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
    [SugarTable("SELL_PSN_INF", SysConst.DB_CON_BANK)]
    public partial class SELL_PSN_INF
    {
           public SELL_PSN_INF(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string SID {get;set;}

           /// <summary>
           /// Desc:出卖人信息ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CMRXX_ID {get;set;}

           /// <summary>
           /// Desc:不动产权证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGHT_TP {get;set;}

           /// <summary>
           /// Desc:权利类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SELL_PSN_NM {get;set;}

           /// <summary>
           /// Desc:出卖人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SELL_PSN_CRDT_TP {get;set;}

           /// <summary>
           /// Desc:出卖人证件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SELL_PSN_CRDT_NO {get;set;}

           /// <summary>
           /// Desc:出卖人证件号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REALEST_WRNT_NO {get;set;}

    }
}
