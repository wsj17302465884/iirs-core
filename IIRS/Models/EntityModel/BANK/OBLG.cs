using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("OBLG", SysConst.DB_CON_BANK)]
    public partial class OBLG
    {
           public OBLG(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string OID {get;set;}

           /// <summary>
           /// Desc:债务人信息ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZWRXX_ID {get;set;}

           /// <summary>
           /// Desc:债务人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OBLG_NM {get;set;}

    }
}
