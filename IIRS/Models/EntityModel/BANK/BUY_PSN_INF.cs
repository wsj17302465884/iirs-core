using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("BUY_PSN_INF", SysConst.DB_CON_BANK)]
    public partial class BUY_PSN_INF
    {
           public BUY_PSN_INF(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string MID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MSRXX_ID {get;set;}

           /// <summary>
           /// Desc:权利类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGHT_TP {get;set;}

           /// <summary>
           /// Desc:买受人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BUY_PSN_NM {get;set;}

           /// <summary>
           /// Desc:买受人证件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BUY_PSN_CRDT_TP {get;set;}

           /// <summary>
           /// Desc:买受人证件号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BUY_PSN_CRDT_NO {get;set;}

    }
}
