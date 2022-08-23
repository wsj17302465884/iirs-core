using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("FRCST_RGS_RGHT_PSN_INF", SysConst.DB_CON_BANK)]
    public partial class FRCST_RGS_RGHT_PSN_INF
    {
           public FRCST_RGS_RGHT_PSN_INF(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string YID {get;set;}

           /// <summary>
           /// Desc:预告登记权利人信息ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YGDJQLRXX_ID {get;set;}

           /// <summary>
           /// Desc:预告不动产登记证明号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FRCST_REALEST_RGSCTF_NO {get;set;}

           /// <summary>
           /// Desc:权利类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGHT_TP {get;set;}

           /// <summary>
           /// Desc:权利人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGHT_PSN_NM {get;set;}

           /// <summary>
           /// Desc:权利人证件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGHT_PSN_CRDT_TP {get;set;}

           /// <summary>
           /// Desc:权利人证件号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGHT_PSN_CRDT_NO {get;set;}

    }
}
