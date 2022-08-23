using IIRS.Utilities.Common;
using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("BNK_HDL_AGNC_PSN_INF", SysConst.DB_CON_BANK)]
    public partial class BNK_HDL_AGNC_PSN_INF
    {
           public BNK_HDL_AGNC_PSN_INF(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public Guid BID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YHJBDLRXX_ID {get;set;}

           /// <summary>
           /// Desc:代理人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AGNC_PSN_NM {get;set;}

           /// <summary>
           /// Desc:代理人类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AGNC_PSN_TP {get;set;}

           /// <summary>
           /// Desc:代理人证件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AGNC_PSN_CRDT_TP {get;set;}

           /// <summary>
           /// Desc:代理人证件号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AGNC_PSN_CRDT_NO {get;set;}

           /// <summary>
           /// Desc:业务经办人员联系电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BSN_RSPBPSN_CTC_TEL {get;set;}

           /// <summary>
           /// Desc:业务经办人员联系地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BSN_RSPBPSN_CTC_ADR {get;set;}

    }
}
