using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    /// <summary>
    /// 不动产抵押登记申请表
    /// </summary>
    public class DySqbPrintVModel
    {
        public DySqbPrintVModel()
        {

        }
        /// <summary>
        /// 原受理编号
        /// </summary>
        public string slbh { get; set; } = "";
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime? djrq { get; set; }
        public DateTime? qlqsrq { get; set; }
        public DateTime? qljsrq { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string sjr { get; set; }
        /// <summary>
        /// 抵押类型：一般抵押 最高额抵押 在建工程抵押 其它
        /// </summary>
        public string dylx { get; set; }
        /// <summary>
        /// 登记类型：首次登记 转移登记 变更登记 注销登记 预告登记 其它
        /// </summary>
        public string djlx { get; set; }
        /// <summary>
        /// 权利人名称
        /// </summary>
        public string qlrmc { get; set; }
        /// <summary>
        /// 权利人证件类别
        /// </summary>
        public string qlr_zjlb { get; set; }
        /// <summary>
        /// 权利人证件号码
        /// </summary>
        public string qlr_zjhm { get; set; }
        /// <summary>
        /// 法定代表人或负责人
        /// </summary>
        public string fddbr { get; set; }
        /// <summary>
        /// 法定代表人或负责人联系电话
        /// </summary>
        public string fddbr_tel { get; set; }
        /// <summary>
        /// 代理人名称
        /// </summary>
        public string dlrmc { get; set; }
        /// <summary>
        /// 代理人电话
        /// </summary>
        public string dlr_tel { get; set; }
        /// <summary>
        /// 义务人名称
        /// </summary>
        public string ywrmc { get; set; }
        /// <summary>
        /// 义务人证件类别
        /// </summary>
        public string ywr_zjlb { get; set; }
        /// <summary>
        /// 义务人证件号码
        /// </summary>
        public string ywr_zjhm { get; set; }
        /// <summary>
        /// 义务人-法定代表人或负责人
        /// </summary>
        public string ywr_fddbr { get; set; }
        /// <summary>
        /// 义务人-法定代表人或负责人电话
        /// </summary>
        public string ywr_fddbr_tel { get; set; }
        /// <summary>
        /// 义务人-代理人名称
        /// </summary>
        public string ywr_dlrmc { get; set; }
        /// <summary>
        /// 义务人-代理人电话
        /// </summary>
        public string ywr_dlr_tel { get; set; }
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string bdcdyh { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 权利类型
        /// </summary>
        public string qllx { get; set; }
        /// <summary>
        /// 权利性质
        /// </summary>
        public string qlxz { get; set; }
        /// <summary>
        /// 不动产证号
        /// </summary>
        public string bdczh { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? jzmj { get; set; }
        public decimal? tdmj { get; set; }
        public decimal? gytdmj { get; set; }
        /// <summary>
        /// 被担保债权数额
        /// </summary>
        public decimal? bdbzqse { get; set; }
        /// <summary>
        /// 债务履行期限（债权确定期限）
        /// </summary>
        public string lxqx { get; set; }
        /// <summary>
        /// 在建建筑物抵押范围
        /// </summary>
        public string dyfw { get; set; }
        /// <summary>
        /// 需役地坐落
        /// </summary>
        public string xydzl { get; set; }
        /// <summary>
        /// 需役地不动产单元号
        /// </summary>
        public string xyd_bdcdyh { get; set; }
        /// <summary>
        /// 登记原因
        /// </summary>
        public string djyy { get; set; }
        /// <summary>
        /// 登记原因 证明文件
        /// </summary>
        public string zmwj { get; set; }
        /// <summary>
        /// 系统主键
        /// </summary>
        public string xid { get; set; }
        /// <summary>
        /// 新受理编号
        /// </summary>
        public string ywslbh { get; set; }
        /// <summary>
        /// 操作员ID
        /// </summary>
        public string userId { get; set; }
        /// <summary>
        /// 流程名称
        /// </summary>
        public string gname { get; set; }
        /// <summary>
        /// 保存节点
        /// </summary>
        public string IsActionOk { get; set; }
        /// <summary>
        /// 收件时间
        /// </summary>
        public DateTime? savedate { get; set; }
        /// <summary>
        /// 原不动产证号
        /// </summary>
        public string xgzh { get; set; }
        /// <summary>
        /// 规划用途中文名
        /// </summary>
        public string ghyt_zwm { get; set; }

        public string bdclx { get; set; }
        public string tdlx { get; set; }
        public string ghyt { get; set; }
        public string tdyt { get; set; }
        public string fj { get; set; }
        public string tstybm { get; set; }
        public string qt { get; set; }
        public decimal? tddymj { get; set; }
        public string mj { get; set; }
        public string djyy_zmwj { get; set; }
        /// <summary>
        /// 担保范围
        /// </summary>
        public string dbfw { get; set; }
        /// <summary>
        /// 是否约定
        /// </summary>
        public string sfyd1 { get; set; }
        public string sfyd2 { get; set; }
        public string PDFFile { get; set; }
    }
}
