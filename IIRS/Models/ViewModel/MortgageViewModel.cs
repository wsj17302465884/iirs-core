using System;

namespace IIRS.Models.ViewModel
{
    public class MortgageViewModel
    {
        public MortgageViewModel()
        {

        }
        /// <summary>
        /// 不动产证号
        /// </summary>
        public string Bdczh { get; set; }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string Slbh { get; set; }
        /// <summary>
        /// 登记种类
        /// </summary>
        public string Djzl { get; set; }
        /// <summary>
        /// 权利人类型
        /// </summary>
        public string Qlrlx { get; set; }
        /// <summary>
        /// 权利人名称
        /// </summary>
        public string Qlrmc { get; set; }
        /// <summary>
        /// 证件类别
        /// </summary>
        public string Zjlb { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string Zjhm { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string Zl { get; set; }
        /// <summary>
        /// 权利类型
        /// </summary>
        public string Qllx { get; set; }
        /// <summary>
        /// 权利性质
        /// </summary>
        public string Qlxz { get; set; }
        /// <summary>
        /// 规划用途
        /// </summary>
        public string Ghyt { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? Jzmj { get; set; }
        /// <summary>
        /// 土地使用期限
        /// </summary>
        public string Tdsyqx { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        public string Qt { get; set; }
        /// <summary>
        /// 登记簿登记日期
        /// </summary>
        public DateTime? DjbDjrq { get; set; }
        /// <summary>
        /// 登记簿付记
        /// </summary>
        public string DjbFj { get; set; }
        /// <summary>
        /// 不动产证明号
        /// </summary>
        public string Bdczmh { get; set; }
        /// <summary>
        /// 抵押面积
        /// </summary>
        public decimal? Dymj { get; set; }
        /// <summary>
        /// 抵押面积2
        /// </summary>
        public decimal? Dymj2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal? Bdbzzqse { get; set; }
        /// <summary>
        /// 抵押方式
        /// </summary>
        public string Dyfs { get; set; }
        /// <summary>
        /// 抵押其他
        /// </summary>
        public string DyQt { get; set; }
        /// <summary>
        /// 抵押登记日期
        /// </summary>
        public DateTime? DyDjrq { get; set; }

        /// <summary>
        /// 抵押期限
        /// </summary>
        public string Dyqx { get; set; }
        /// <summary>
        /// 抵押付记
        /// </summary>
        public string Dyfj { get; set; }
        /// <summary>
        /// 查封文号
        /// </summary>
        public string Cfwh { get; set; }
        /// <summary>
        /// 查封机构
        /// </summary>
        public string Cfjg { get; set; }
        /// <summary>
        /// 代办人
        /// </summary>
        public string Dbr { get; set; }
        /// <summary>
        /// 查封期限
        /// </summary>
        public string Cfqx { get; set; }
        /// <summary>
        /// 查封日期
        /// </summary>
        public DateTime? Cfrq { get; set; }
        /// <summary>
        /// 查封文件
        /// </summary>
        public string Cfwj { get; set; }

        /// <summary>
        /// 查封付记
        /// </summary>
        public string Cffj { get; set; }
        /// <summary>
        /// 抵押权人
        /// </summary>
        public string Dyqr_Name { get; set; }

        /// <summary>
        /// 抵押权人证件类别
        /// </summary>
        public string Dyqr_Zjlb { get; set; }

        /// <summary>
        /// 抵押权人证件号码
        /// </summary>
        public string Dyqr_Zjhm { get; set; }

        /// <summary>
        /// 抵押人
        /// </summary>
        public string Dyr_Name { get; set; }

        /// <summary>
        /// 抵押人证件类别
        /// </summary>
        public string Dyr_Zjlb { get; set; }

        /// <summary>
        /// 抵押人证件号码
        /// </summary>
        public string Dyr_Zjhm { get; set; }

        /// <summary>
        /// 抵押状态
        /// </summary>
        public string Dyzt { get; set; }

        /// <summary>
        /// 查封状态
        /// </summary>
        public string Cfzt { get; set; }

        /// <summary>
        /// 共有方式
        /// </summary>
        public string Gyfs { get; set; }

        /// <summary>
        /// 图属统一编码
        /// </summary>
        public string Tstybm { get; set; }
    }
}
