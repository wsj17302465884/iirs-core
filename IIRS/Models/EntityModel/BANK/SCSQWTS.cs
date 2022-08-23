using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("SCSQWTS", SysConst.DB_CON_BANK)]
    public partial class SCSQWTS
    {
           public SCSQWTS(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string WTS_ID {get;set;}

           /// <summary>
           /// Desc:来源系统
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string SRCSYS {get;set;}

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
           /// Desc:不动产所属省份代码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string PROVINCE_CD {get;set;}

           /// <summary>
           /// Desc:不动产所属城市代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CITY_CD {get;set;}

           /// <summary>
           /// Desc:不动产所属区县代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DISTRICT_CD {get;set;}

           /// <summary>
           /// Desc:贷款主办机构
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LN_HOST_INST {get;set;}

           /// <summary>
           /// Desc:系统参考号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TXN_STM_REF_NO {get;set;}

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
           /// Desc:不动产登记证明号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REALEST_RGSCTF_NO {get;set;}

           /// <summary>
           /// Desc:不动产不动产权证号单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REALEST_WRNT_NO {get;set;}

           /// <summary>
           /// Desc:不动产单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REALEST_UNIT_NO {get;set;}

           /// <summary>
           /// Desc:委托人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TRSTR_NM {get;set;}

           /// <summary>
           /// Desc:委托人类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TRSTR_TP {get;set;}

           /// <summary>
           /// Desc:委托人证件号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TRSTR_CRDT_NM {get;set;}

           /// <summary>
           /// Desc:委托书类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MANDDOC_TP {get;set;}

           /// <summary>
           /// Desc:委托书编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MANDDOC_ID {get;set;}

           /// <summary>
           /// Desc:经办人签字日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RSPBPSN_SGN_DT {get;set;}

           /// <summary>
           /// Desc:代理人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AGNC_PSN_NM {get;set;}

           /// <summary>
           /// Desc:代理人联系电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AGNC_PSN_CTC_TEL {get;set;}

           /// <summary>
           /// Desc:代理人证件号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AGNC_PSN_CRDT_NO {get;set;}

           /// <summary>
           /// Desc:代理人签字日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AGNC_PSN_SGN_DT {get;set;}

           /// <summary>
           /// Desc:有效期开始时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AVLDT_STTM {get;set;}

           /// <summary>
           /// Desc:有效期结束时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AVLDT_EDTM {get;set;}

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

    }
}
