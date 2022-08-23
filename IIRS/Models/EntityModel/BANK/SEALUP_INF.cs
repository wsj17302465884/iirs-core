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
    [SugarTable("SEALUP_INF", SysConst.DB_CON_BANK)]
    public partial class SEALUP_INF
    {
           public SEALUP_INF(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string CID {get;set;}

           /// <summary>
           /// Desc:查封序号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SEALUP_SN {get;set;}

           /// <summary>
           /// Desc:查封单位名称
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SEALUP_UNIT_NM {get;set;}

           /// <summary>
           /// Desc:查封开始时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SEALUP_STTM {get;set;}

           /// <summary>
           /// Desc:查封结束时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SEALUP_EDTM {get;set;}

           /// <summary>
           /// Desc:查封文号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SEALUP_FLNO {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SERIALNUMBER {get;set;}

    }
}
