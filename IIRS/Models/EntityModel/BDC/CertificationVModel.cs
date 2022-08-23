using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    /// <summary>
    /// 不动产证明号查询实体类
    /// </summary>
    //[SugarTable("V_CERTIFICATION_QUERY", "GL")]
    [SugarTable("V_CERTIFICATION_QUERY", SysConst.DB_CON_BDC)]
    public partial class CertificationVModel
    {
        public CertificationVModel()
        {


        }
        [SugarColumn(IsIgnore = true)]
        public int xh { get; set; }
        /// <summary>
        /// 不动产证明号
        /// </summary>
        public string bdczmh { get; set; }

        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string bdcdyh { get; set; }


        /// <summary>
        /// 相关证号
        /// </summary>
        public string bdczh { get; set; }

        public string dyfs { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }

        public decimal dymj { get; set; }

        /// <summary>
        /// 抵押人
        /// </summary>
        public string Dyr { get; set; }

        /// <summary>
        /// 抵押人证件类别
        /// </summary>
        public string Dyr_Zjlb { get; set; }

        /// <summary>
        /// 抵押人证件号码
        /// </summary>
        public string Dyr_Zjhm { get; set; }

        /// <summary>
        /// 抵押权人
        /// </summary>
        public string Dyqr { get; set; }

        /// <summary>
        /// 抵押权人证件类别
        /// </summary>
        public string Dyqr_Zjlb { get; set; }

        /// <summary>
        /// 抵押权人证件号码
        /// </summary>
        public string Dyqr_Zjhm { get; set; }

        /// <summary>
        /// 权利结束日期
        /// </summary>
        public string qljssj { get; set; }

        [SugarColumn(IsIgnore = true)]
        public bool hasChildren { get; set; } = true;

        /// <summary>
        /// 图属统一编码
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string tstybm { get; set; }

        /// <summary>
        /// 受理编号
        /// </summary>
        public string slbh { get; set; }

        /// <summary>
        /// 不动产证消息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string bdczhMessage { get; set; }

        /// <summary>
        /// 查封的不动产证号消息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string CfBdczhMessage { get; set; }

        /// <summary>
        /// 按钮是否可用
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string btnable { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string qllx { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string qlxz { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string tdqllx { get; set; }
        [SugarColumn(IsIgnore = true)]
        public string tdqlxz { get; set; }
        [SugarColumn(IsIgnore = true)]
        public decimal? jzmj { get; set; }
        [SugarColumn(IsIgnore = true)]
        public decimal? gytdmj { get; set; }
        [SugarColumn(IsIgnore = true)]
        public decimal? dytdmj { get; set; }
    }
}
