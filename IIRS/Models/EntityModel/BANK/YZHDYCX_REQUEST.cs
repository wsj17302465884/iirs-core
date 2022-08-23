using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///已在行抵押查询请求
    ///</summary>
    [SugarTable("YZHDYCX_REQUEST", SysConst.DB_CON_BANK)]
    public partial class YZHDYCX_REQUEST
    {
        /// <summary>
        /// 已在行抵押查询请求
        /// </summary>
        public YZHDYCX_REQUEST()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string YID { get; set; }

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
        /// Nullable:False
        /// </summary>           
        public string RGON_CD { get; set; }

        /// <summary>
        /// Desc:不动产所属省份代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PROVINCE_CD { get; set; }

        /// <summary>
        /// Desc:不动产所属城市代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CITY_CD { get; set; }

        /// <summary>
        /// Desc:不动产所属区县代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DISTRICT_CD { get; set; }

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
        /// Desc:操作用户岗位名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MNPLT_USR_PST { get; set; }

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
        /// Desc:不动产单元号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REALEST_UNIT_NO { get; set; }

        /// <summary>
        /// Desc:不动产权证号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REALEST_WRNT_NO { get; set; }

        /// <summary>
        /// Desc:不动产登记证明号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REALEST_RGSCTF_NO { get; set; }

        /// <summary>
        /// Desc:抵押预告证明号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_FRCST_RGSCTF_NO { get; set; }

        /// <summary>
        /// Desc:抵押权人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_WGHT_PSN_NM { get; set; }

        /// <summary>
        /// Desc:不动产权人姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REALEST_WGHT_PSN_NM { get; set; }

        /// <summary>
        /// Desc:不动产权人类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REALEST_WGHT_PSN_TP { get; set; }

        /// <summary>
        /// Desc:不动产权人证件类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REALEST_WGHT_PSN_CRDT_TP { get; set; }

        /// <summary>
        /// Desc:不动产权人证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REALEST_WGHT_PSN_CRDT_NO { get; set; }

        /// <summary>
        /// Desc:业务件号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BSN_PTS_NO { get; set; }

        /// <summary>
        /// Desc:业务标识
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BSN_IDR { get; set; }

        /// <summary>
        /// Desc:商品房买卖合同编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CMRCLHS_BUYSELL_CTR_ID { get; set; }

        /// <summary>
        /// Desc:买受人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BUY_PSN_NM { get; set; }

        /// <summary>
        /// Desc:是否新房
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IF_NEWHS { get; set; }

        /// <summary>
        /// Desc:不动产坐落
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HSLOCATION { get; set; }

        /// <summary>
        /// Desc:审核人编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CHK_PSN_ECD { get; set; }

        /// <summary>
        /// Desc:查询目的
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ENQR_PPS { get; set; }

        /// <summary>
        /// Desc:是否产权人授权
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string IF_OWN_PSN_AHN { get; set; }

    }
}
