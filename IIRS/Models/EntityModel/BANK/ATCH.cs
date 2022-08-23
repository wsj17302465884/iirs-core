using IIRS.Utilities.Common;
using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///附件表
    ///</summary>
    [SugarTable("ATCH", SysConst.DB_CON_BANK)]
    public partial class ATCH
    {
           public ATCH(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public Guid FID {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FJ_ID {get;set;}

           /// <summary>
           /// Desc:附件ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ATCH_ID {get;set;}

           /// <summary>
           /// Desc:附件名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ATCH_NM {get;set;}

           /// <summary>
           /// Desc:附件大小
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ATCH_SZ {get;set;}

           /// <summary>
           /// Desc:附件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ATCH_TP {get;set;}

           /// <summary>
           /// Desc:附件附记
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ATCH_INFO {get;set;}

           /// <summary>
           /// Desc:附件目录序号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ATCH_CTLG_SN {get;set;}

    }
}
