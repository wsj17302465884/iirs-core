using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DYZXSQ", SysConst.DB_CON_BANK)]
    public partial class DYZXSQ
    {
        public DYZXSQ()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string DYZX_ID { get; set; }

        /// <summary>
        /// Desc:来源系统
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SRCSYS { get; set; }

        /// <summary>
        /// Desc:系统参考号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TXN_STM_REF_NO { get; set; }

        /// <summary>
        /// Desc:多法人标识
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SVRLLGPSN_IDR { get; set; }

        /// <summary>
        /// Desc:地区码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RGON_CD { get; set; }

        /// <summary>
        /// Desc:受理站点
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ACPT_AREA { get; set; }

        /// <summary>
        /// Desc:贷款主办机构
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LN_HOST_INST { get; set; }

        /// <summary>
        /// Desc:操作用户编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MNPLT_USR_ECD { get; set; }

        /// <summary>
        /// Desc:操作用户名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MNPLT_USR_NM { get; set; }

        /// <summary>
        /// Desc:操作员所在机构名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MNPLT_USR_WBT_INST_NM { get; set; }

        /// <summary>
        /// Desc:操作员所在机构代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MNPLT_USR_WBT_INST_CD { get; set; }

        /// <summary>
        /// Desc:申请时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string APLY_TM { get; set; }

        /// <summary>
        /// Desc:申请类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string APLY_TP { get; set; }

        /// <summary>
        /// Desc:业务件号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BSN_PTS_NO { get; set; }

        /// <summary>
        /// Desc:登记类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string RGS_TP { get; set; }

        /// <summary>
        /// Desc:抵押类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_TP { get; set; }

        /// <summary>
        /// Desc:注销原因
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LOUT_RSN { get; set; }

        /// <summary>
        /// Desc:结清状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CLSG_ST { get; set; }

        /// <summary>
        /// Desc:预告登记证号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FRCST_RGSCTF_NO { get; set; }

        /// <summary>
        /// Desc:不动产登记证明号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REALEST_ECB_NO { get; set; }

        /// <summary>
        /// Desc:抵押合同号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_CTR_NO { get; set; }

        /// <summary>
        /// Desc:主债权担保金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double PRIM_CLM_WRNT_AMT { get; set; }

        /// <summary>
        /// Desc:最高债权数额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal HGST_CLM_NUM_AMT { get; set; }

        /// <summary>
        /// Desc:商业贷款
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double CMRC_LN { get; set; }

        /// <summary>
        /// Desc:公积金贷款
        /// Default:
        /// Nullable:True
        /// </summary>           
        public double PRFDLN { get; set; }

        /// <summary>
        /// Desc:债务履行期限（债权确定期间、抵押期限）开始
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LN_STTM { get; set; }

        /// <summary>
        /// Desc:债务履行期限（债权确定期间、抵押期限）截止
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string LN_EDTM { get; set; }

        /// <summary>
        /// Desc:业务申请号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BAPL_NO { get; set; }

        /// <summary>
        /// Desc:抵押权人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_WGHT_PSN_NM { get; set; }

        /// <summary>
        /// Desc:抵押权人证件类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_WGHT_PSN_CRDT_TP { get; set; }

        /// <summary>
        /// Desc:抵押权人证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_WGHT_PSN_CRDT_NO { get; set; }

        /// <summary>
        /// Desc:抵押权人联系电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_WGHT_PSN_CTC_TEL { get; set; }

        /// <summary>
        /// Desc:抵押权人类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_WGHT_PSN_TP { get; set; }

        /// <summary>
        /// Desc:抵押权人法定代表或负责人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_WGHT_PSN_LGL_TBL_OR_PNP { get; set; }

        /// <summary>
        /// Desc:抵押权人地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_WGHT_PSN_ADR { get; set; }

        /// <summary>
        /// Desc:抵押代理人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_AGNC_PSN_NM { get; set; }

        /// <summary>
        /// Desc:抵押代理人证件类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_AGNC_PSN_CRDT_TP { get; set; }

        /// <summary>
        /// Desc:抵押代理人证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_AGNC_PSN_CRDT_NO { get; set; }

        /// <summary>
        /// Desc:抵押代理人联系电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_AGNC_PSN_CTC_TEL { get; set; }

        /// <summary>
        /// Desc:最高额抵押担保的债权确定情形
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HIGH_MRTG_WRNT_CLM_DETR { get; set; }

        /// <summary>
        /// Desc:是否真实表示
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TRUE_EXPS { get; set; }

        /// <summary>
        /// Desc:是否共有房屋
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string COM_HS { get; set; }

        /// <summary>
        /// Desc:抵押不动产单元信息ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYBDCDYXX_ID { get; set; }

        /// <summary>
        /// Desc:借款人信息ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JKRXX_ID { get; set; }

        /// <summary>
        /// Desc:附件ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FJ_ID { get; set; }
        /// <summary>
        /// 银行经办代理人信息ID
        /// </summary>
        public string YHJBDLRXX_ID { get; set; }

    }
}
