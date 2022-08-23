using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    //辽阳市不动产抵押登记专网申报主债权合同及抵押合同结构化表（首次）
    public class MrgePrintVModel
    {
        public MrgePrintVModel()
        {

        }
        /// <summary>
        /// 抵押合同
        /// </summary>
        public string dyht { get; set; }
        /// <summary>
        /// 抵押权人
        /// </summary>
        public string dyqr { get; set; }
        /// <summary>
        /// 抵押权人证件类别
        /// </summary>
        public string dyqr_zjlb { get; set; }
        /// <summary>
        /// 抵押权人证件号码
        /// </summary>
        public string dyqr_zjhm { get; set; }
        /// <summary>
        /// 抵押人
        /// </summary>
        public string dyr { get; set; }
        /// <summary>
        /// 抵押人证件类别
        /// </summary>
        public string dyr_zjlb { get; set; }
        /// <summary>
        /// 抵押人证件号码
        /// </summary>
        public string dyr_zjhm { get; set; }
        /// <summary>
        /// 债务人
        /// </summary>
        public string zwr { get; set; }
        /// <summary>
        /// 债务人证件类别
        /// </summary>
        public string zwr_zjlb { get; set; }
        /// <summary>
        /// 债务人证件号码
        /// </summary>
        public string zwr_zjhm { get; set; }
        /// <summary>
        /// 被担保主债权数额
        /// </summary>
        public string bdbzzqse { get; set; }
        /// <summary>
        /// 抵押类型
        /// </summary>
        public string dylx { get; set; }
        /// <summary>
        /// 债务期限
        /// </summary>
        public string zwqx { get; set; }
        /// <summary>
        /// 权利起始日期
        /// </summary>
        public string qsrq { get; set; }
        /// <summary>
        /// 权利终止日期
        /// </summary>
        public string zzrq { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string bdcdyh { get; set; }
        /// <summary>
        /// 不动产证号
        /// </summary>
        public string bdczh { get; set; }
        /// <summary>
        /// 抵押面积
        /// </summary>
        public string jzmj { get; set; }
        /// <summary>
        /// 附记
        /// </summary>
        public string fj { get; set; }
        public string PDFFile { get; set; }
    }
}
