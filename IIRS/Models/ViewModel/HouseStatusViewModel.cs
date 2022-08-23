using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel
{
    public class HouseStatusViewModel
    {
        public HouseStatusViewModel()
        {

        }

        /// <summary>
        /// 图属统一编码
        /// </summary>
        public string Tstybm { get; set; }

        /// <summary>
        /// 不动产证号
        /// </summary>
        public string Bdczh { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? Jzmj { get; set; }

        /// <summary>
        /// FWXG建筑面积
        /// </summary>
        public decimal? Fwxg_Jzmj { get; set; }

        /// <summary>
        /// 坐落
        /// </summary>
        public string Zl { get; set; }

        /// <summary>
        /// 共有方式
        /// </summary>
        public string Gyfs { get; set; }

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
        /// 土地权利类型
        /// </summary>
        public string TdQllx { get; set; }
        /// <summary>
        /// 土地权利性质
        /// </summary>
        public string TdQlxz { get; set; }
        /// <summary>
        /// 土地规划用途
        /// </summary>
        public string Tdyt { get; set; }

        /// <summary>
        /// 土地使用期限
        /// </summary>
        public string Tdsyqx { get; set; }

        /// <summary>
        /// 土地起始日期
        /// </summary>
        public DateTime? TdQsrq { get; set; }

        /// <summary>
        /// 土地终止日期
        /// </summary>
        public DateTime? TdZzrq { get; set; }

        /// <summary>
        /// 建筑宗地面积
        /// </summary>
        public decimal? Jzzdmj { get; set; }

        /// <summary>
        /// 登记簿其他
        /// </summary>
        public string DjbQt { get; set; }

        /// <summary>
        /// 登记簿登记日期
        /// </summary>
        public DateTime? DjbDjrq { get; set; }

        /// <summary>
        /// 登记簿发证日期
        /// </summary>
        public DateTime? DjbFzrq { get; set; }

        /// <summary>
        /// 登记簿证书序列号
        /// </summary>
        public string DjbZsxlh { get; set; }

        /// <summary>
        /// 登记簿附记
        /// </summary>
        public string DjbFj { get; set; }

        /// <summary>
        /// 登记簿发证机关
        /// </summary>
        public string DjbFzjg { get; set; }

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
        public string DyQx { get; set; }

        /// <summary>
        /// 抵押其他
        /// </summary>
        public string DyQt { get; set; }

        /// <summary>
        /// 抵押登记日期
        /// </summary>
        public DateTime? DyDjrq { get; set; }

        /// <summary>
        /// 抵押起始日期
        /// </summary>
        public DateTime? Dyqssj { get; set; }

        /// <summary>
        /// 抵押结束日期
        /// </summary>
        public DateTime? Dyjssj { get; set; }

        /// <summary>
        /// 抵押发证日期
        /// </summary>
        public DateTime? DyFzrq { get; set; }

        /// <summary>
        /// 抵押证书序列号
        /// </summary>
        public string DyZsxlh { get; set; }

        /// <summary>
        /// 抵押发证机关
        /// </summary>
        public string DyFzjg { get; set; }

        /// <summary>
        /// 抵押合同号
        /// </summary>
        public string DyHth{ get; set; }

        /// <summary>
        /// 抵押附记
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
        /// 查封期限
        /// </summary>
        public string Cfqx{ get; set; }

        /// <summary>
        /// 查封日期
        /// </summary>
        public DateTime? Cfrq { get; set; }

        /// <summary>
        /// 查封起始时间
        /// </summary>
        public DateTime? CfQssj { get; set; }

        /// <summary>
        /// 查封结束时间
        /// </summary>
        public DateTime? CfJssj { get; set; }

        /// <summary>
        /// 查封付记
        /// </summary>
        public string Cffj { get; set; }

        /// <summary>
        /// 预告申请日期
        /// </summary>
        public DateTime? YgSqrq { get; set; }

        /// <summary>
        /// 预告审批单位
        /// </summary>
        public string YgSpdw{ get; set; }

        /// <summary>
        /// 预告审批日期
        /// </summary>
        public DateTime? YgSprq { get; set; }

        /// <summary>
        /// 预告不动产证明号
        /// </summary>
        public string YgBdczmh { get; set; }

        /// <summary>
        /// 预告登记日期
        /// </summary>
        public DateTime? YgDjrq { get; set; }

        /// <summary>
        /// 预告发证机构
        /// </summary>
        public string YgFzjg { get; set; }

        /// <summary>
        /// 预告发证日期
        /// </summary>
        public DateTime? YgFzrq { get; set; }

        /// <summary>
        /// 预告证书序列号
        /// </summary>
        public string YgZsxlh { get; set; }

        /// <summary>
        /// 预告其他
        /// </summary>
        public string YgQt{ get; set; }

        /// <summary>
        /// 登记种类
        /// </summary>
        public string Djzl { get; set; }

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
        /// 预告权利人名称
        /// </summary>
        public string Yg_Qlrmc { get; set; }
        /// <summary>
        /// 预告证件类别
        /// </summary>
        public string Yg_Zjlb { get; set; }
        /// <summary>
        /// 预告证件号码
        /// </summary>
        public string Yg_Zjhm { get; set; }

        public string Lifecycle { get; set; }
    }
}
