using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    /// <summary>
    /// 转移登记 - 不动产登记收件收据
    /// </summary>
    public class TransferSjsjPrintVModel
    {
        public TransferSjsjPrintVModel()
        {

        }
        public string slbh { get; set; }
        public string xgzh { get; set; }
        public string sqr { get; set; }
        /// <summary>
        /// 交件人
        /// </summary>
        public string jjr { get; set; }
        public string ywlx { get; set; }
        public string tel { get; set; }
        public string zl { get; set; }
        public string fj { get; set; }
        public string sjr { get; set; }
        public DateTime? djrq { get; set; }
        public DateTime? cnrq { get; set; }
        public string PDFFile { get; set; }
    }
}
