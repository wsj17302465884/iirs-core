using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("YZHDYCX_RESULT", SysConst.DB_CON_BANK)]
    public partial class YZHDYCX_RESULT
    {
           public YZHDYCX_RESULT(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string RID {get;set;}

           /// <summary>
           /// Desc:返回码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RET_CODE {get;set;}

           /// <summary>
           /// Desc:返回说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RET_DATA {get;set;}

           /// <summary>
           /// Desc:业务件号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string BSN_PTS_NO {get;set;}

           /// <summary>
           /// Desc:原抵押业务ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ORI_MRTG_BSNID {get;set;}

           /// <summary>
           /// Desc:是否可合并办理注销+抵押
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IF_CAN_MRG_LOUT_AND_MRTG {get;set;}

           /// <summary>
           /// Desc:不可合并办理的原因
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string NOT_CAN_MRTG_RSN {get;set;}

           /// <summary>
           /// Desc:不动产登记证明号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string REALEST_RGSCTF_NO {get;set;}

           /// <summary>
           /// Desc:抵押权人信息ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DYQRXX_ID {get;set;}

           /// <summary>
           /// Desc:抵押方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_MOD {get;set;}

           /// <summary>
           /// Desc:不动产信息ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BDCXX_ID {get;set;}

           /// <summary>
           /// Desc:预告登记信息ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YGDJXX_ID {get;set;}

           /// <summary>
           /// Desc:商品房合同备案信息ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPFHTBAXX_ID {get;set;}

           /// <summary>
           /// Desc:存量房合同备案信息ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CLFHTBAXX_ID {get;set;}

    }
}
