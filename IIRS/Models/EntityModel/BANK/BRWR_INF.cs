using IIRS.Utilities.Common;
using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("BRWR_INF", SysConst.DB_CON_BANK)]
    public partial class BRWR_INF
    {
           public BRWR_INF(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public Guid JID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string JKRXX_ID {get;set;}

           /// <summary>
           /// Desc:借款人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BRWR_NM {get;set;}

           /// <summary>
           /// Desc:借款人类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BRWR_TP {get;set;}

           /// <summary>
           /// Desc:借款人证件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BRWR_CRDT_TP {get;set;}

           /// <summary>
           /// Desc:借款人证件号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BRWR_CRDT_NO {get;set;}

           /// <summary>
           /// Desc:联系电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CTC_TEL {get;set;}

    }
}
