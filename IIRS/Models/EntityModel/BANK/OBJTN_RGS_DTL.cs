using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("OBJTN_RGS_DTL", SysConst.DB_CON_BANK)]
    public partial class OBJTN_RGS_DTL
    {
           public OBJTN_RGS_DTL(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string YID {get;set;}

           /// <summary>
           /// Desc:异议登记明细ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YYDJMX_ID {get;set;}

           /// <summary>
           /// Desc:异议登记申请日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OBJTN_RGS_APLY_DT {get;set;}

           /// <summary>
           /// Desc:异议登记注销日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OBJTN_RGS_RLV_DT {get;set;}

           /// <summary>
           /// Desc:异议登记证明号信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OBJTN_RGSCTFNO_INF {get;set;}

    }
}
