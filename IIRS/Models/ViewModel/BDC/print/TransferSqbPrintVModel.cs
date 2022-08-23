using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    /// <summary>
    /// 转移登记 - 不动产登记申请书打印
    /// </summary>
    public class TransferSqbPrintVModel
    {
        public TransferSqbPrintVModel()
        {

        }
        /// <summary>
        /// 编号
        /// </summary>
        public string slbh { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public DateTime? djrq { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string sjr { get; set; }
        /// <summary>
        /// 登记使用权
        /// </summary>
        public string djsyq { get; set; }
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
        /// 义务人证件号码
        /// </summary>
        public string ywrmc { get; set; }
        /// <summary>
        /// 义务人证件号码
        /// </summary>
        public string ywr_zjlb { get; set; }
        /// <summary>
        /// 义务人证件号码
        /// </summary>
        public string ywr_zjhm { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string bdcdyh { get; set; }
        /// <summary>
        /// 不动产类型
        /// </summary>
        public string bdclx { get; set; }
        public string bdclx1 { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public decimal? jzmj { get; set; }
        public decimal? tdmj { get; set; }
        public decimal? gytdmj { get; set; }
        public string mj { get; set; }
        /// <summary>
        /// 规划用途
        /// </summary>
        public string ghyt { get; set; }
        public string tdyt { get; set; }
        public string tdlx { get; set; }
        /// <summary>
        /// 原不动产证号
        /// </summary>
        public string old_bdczh { get; set; }
        /// <summary>
        /// 被担保债权数额
        /// </summary>
        public decimal? bdbzqse { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime? qlqssj { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? qljssj { get; set; }
        /// <summary>
        /// 履行期限
        /// </summary>
        public string lxqx { get; set; }
        /// <summary>
        /// 登记原因
        /// </summary>
        public string djyy { get; set; }
        public string fj { get; set; }
        /// <summary>
        /// 登记原因证明文件
        /// </summary>
        public string djyy_zmwj { get; set; }
        public string tstybm { get; set; }
        public string PDFFile { get; set; }
    }
}
