using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("CTR_AND_CLM_STTN", SysConst.DB_CON_BANK)]
    public partial class CTR_AND_CLM_STTN
    {
           public CTR_AND_CLM_STTN(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string HID {get;set;}

           /// <summary>
           /// Desc:合同及债权情况ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HTJZQQK_ID {get;set;}

           /// <summary>
           /// Desc:抵押合同号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_CTR_NO {get;set;}

           /// <summary>
           /// Desc:抵押不动产类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_REALEST_TP {get;set;}

           /// <summary>
           /// Desc:抵押范围
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_SCOP {get;set;}

           /// <summary>
           /// Desc:权利范围
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGHT_SCOP {get;set;}

           /// <summary>
           /// Desc:抵押顺序号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_SEQ_NO {get;set;}

           /// <summary>
           /// Desc:抵押附记
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_INFO {get;set;}

           /// <summary>
           /// Desc:抵押方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_MOD {get;set;}

           /// <summary>
           /// Desc:抵押率
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? MRTG_RATE {get;set;}

           /// <summary>
           /// Desc:主债权金额
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? PRIM_CLM_AMT {get;set;}

           /// <summary>
           /// Desc:被担保主债权数额（总额）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? PRIM_CLM_WRNT_TOTAL_AMT {get;set;}

           /// <summary>
           /// Desc:主债权担保金额
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? PRIM_CLM_WRNT_AMT {get;set;}

           /// <summary>
           /// Desc:被担保主债权币种
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PRIM_CLM_WRNT_AMT_CCY {get;set;}

           /// <summary>
           /// Desc:被担保主债权汇率
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PRIM_CLM_WRNT_AMT_EXRT {get;set;}

           /// <summary>
           /// Desc:最高债权确定事实
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HGST_CLM_DETR_FCT {get;set;}

           /// <summary>
           /// Desc:土地使用权取得方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LAND_US_WGHT_OBTN_MOD {get;set;}

           /// <summary>
           /// Desc:房产价值确定方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HS_VAL_DETRMOD {get;set;}

           /// <summary>
           /// Desc:债务人及对应被担保合同情况
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OBLG_AND_WRNT_CTR_STTN {get;set;}

           /// <summary>
           /// Desc:贷款用途
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LN_USE {get;set;}

           /// <summary>
           /// Desc:贷款类别
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LN_CGY {get;set;}

           /// <summary>
           /// Desc:最高债权数额
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? HGST_CLM_NUM_AMT {get;set;}

           /// <summary>
           /// Desc:贷款开始时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LN_STTM {get;set;}

           /// <summary>
           /// Desc:贷款结束时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LN_EDTM {get;set;}

           /// <summary>
           /// Desc:抵押起始时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_BEG_TM {get;set;}

           /// <summary>
           /// Desc:抵押结束时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_ED_TM {get;set;}

           /// <summary>
           /// Desc:债务履行期限
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DBT_PRFMN_DDLN {get;set;}

           /// <summary>
           /// Desc:商业贷款
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? CMRC_LN {get;set;}

           /// <summary>
           /// Desc:公积金贷款
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? PRFDLN {get;set;}

           /// <summary>
           /// Desc:抵押权人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_WGHT_PSN_NM {get;set;}

           /// <summary>
           /// Desc:抵押权人代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_WGHT_PSN_CD {get;set;}

           /// <summary>
           /// Desc:抵押权人证件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_WGHT_PSN_CRDT_TP {get;set;}

           /// <summary>
           /// Desc:抵押权人证件号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_WGHT_PSN_CRDT_NO {get;set;}

           /// <summary>
           /// Desc:抵押权人联系电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_WGHT_PSN_CTC_TEL {get;set;}

           /// <summary>
           /// Desc:抵押权人类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_WGHT_PSN_TP {get;set;}

           /// <summary>
           /// Desc:抵押权人法定代表或负责人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_WGHT_PSN_LGL_TBL_OR_PNP {get;set;}

           /// <summary>
           /// Desc:抵押权人地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_WGHT_PSN_ADR {get;set;}

           /// <summary>
           /// Desc:抵押权人法定代表人电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_WGHT_PSN_LGL_RPRS_TEL {get;set;}

           /// <summary>
           /// Desc:抵押权人法定代表人证件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_WGHT_PSN_LGL_RPRS_CRDT_TP {get;set;}

           /// <summary>
           /// Desc:抵押权人法定代表人证件号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MRTG_WGHT_PSN_LGL_RPRS_CRDT_NO {get;set;}

           /// <summary>
           /// Desc:抵押不动产单元信息ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DYBDCDY_ID {get;set;}

    }
}
