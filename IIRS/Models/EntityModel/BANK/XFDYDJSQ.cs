using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("XFDYDJSQ", SysConst.DB_CON_BANK)]
    public partial class XFDYDJSQ
    {
           public XFDYDJSQ(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string XID {get;set;}

           /// <summary>
           /// Desc:来源系统
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SRCSYS {get;set;}

           /// <summary>
           /// Desc:系统参考号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TXN_STM_REF_NO {get;set;}

           /// <summary>
           /// Desc:多法人标识
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SVRLLGPSN_IDR {get;set;}

           /// <summary>
           /// Desc:地区码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGON_CD {get;set;}

           /// <summary>
           /// Desc:原交易系统参考号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ORI_TXN_STM_REF_NO {get;set;}

           /// <summary>
           /// Desc:补正标志
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPLMCRT_IND {get;set;}

           /// <summary>
           /// Desc:业务申请号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BAPL_NO {get;set;}

           /// <summary>
           /// Desc:贷款主办机构
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LN_HOST_INST {get;set;}

           /// <summary>
           /// Desc:受理人编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ACPT_PSN_ECD {get;set;}

           /// <summary>
           /// Desc:受理人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ACPT_PSN_NM {get;set;}

           /// <summary>
           /// Desc:受理时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ACPT_TM {get;set;}

           /// <summary>
           /// Desc:受理人联系电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ACPT_PSN_CTC_TEL {get;set;}

           /// <summary>
           /// Desc:受理人证件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ACPT_PSN_CRDT_TP {get;set;}

           /// <summary>
           /// Desc:受理人证件号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ACPT_PSN_CRDT_NO {get;set;}

           /// <summary>
           /// Desc:短信联系人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SMS_CTCPSN_NM {get;set;}

           /// <summary>
           /// Desc:短信联系人联系电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SMS_CTCPSN_CTC_TEL {get;set;}

           /// <summary>
           /// Desc:操作用户编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MNPLT_USR_ECD {get;set;}

           /// <summary>
           /// Desc:操作用户名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MNPLT_USR_NM {get;set;}

           /// <summary>
           /// Desc:操作员所在机构名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MNPLT_USR_WBT_INST_NM {get;set;}

           /// <summary>
           /// Desc:操作员所在机构代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MNPLT_USR_WBT_INST_CD {get;set;}

           /// <summary>
           /// Desc:操作用户意见
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MNPLT_USR_OPIN {get;set;}

           /// <summary>
           /// Desc:登记类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGS_TP {get;set;}

           /// <summary>
           /// Desc:登记原因
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGS_RSN {get;set;}

           /// <summary>
           /// Desc:申报状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DCL_ST {get;set;}

           /// <summary>
           /// Desc:申请时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string APLY_TM {get;set;}

           /// <summary>
           /// Desc:申请类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string APLY_TP {get;set;}

           /// <summary>
           /// Desc:上报组织
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RPT_ORG {get;set;}

           /// <summary>
           /// Desc:产别
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HSPTY_CGY {get;set;}

           /// <summary>
           /// Desc:预约类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RSRVTN_TP {get;set;}

           /// <summary>
           /// Desc:关联类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RLTV_TP {get;set;}

           /// <summary>
           /// Desc:关联数据
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RLTV_DATA {get;set;}

           /// <summary>
           /// Desc:不动产所属区域
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REALEST_BLNG_RGON {get;set;}

           /// <summary>
           /// Desc:最高额抵押担保的债权确定情形
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HIGH_MRTG_WRNT_CLM_DETR {get;set;}

           /// <summary>
           /// Desc:银行业务受理号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BNK_BSN_ACPT_NO {get;set;}

           /// <summary>
           /// Desc:合同及债权情况ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HTJZQQK_ID {get;set;}

           /// <summary>
           /// Desc:银行经办代理人信息ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YHJBDLRXX_ID {get;set;}

           /// <summary>
           /// Desc:出卖人信息ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CMRXX_ID {get;set;}

           /// <summary>
           /// Desc:附件ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FJ_ID {get;set;}

    }
}
