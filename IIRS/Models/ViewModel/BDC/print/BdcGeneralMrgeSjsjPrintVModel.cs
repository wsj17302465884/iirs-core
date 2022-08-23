using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    /// <summary>
    /// 一般抵押 - 不动产登记收件收据
    /// </summary>
    public class BdcGeneralMrgeSjsjPrintVModel
    {
        public BdcGeneralMrgeSjsjPrintVModel()
        {

        }
        public string slbh { get; set; }
        public string xgzh { get; set; }
        public string sqr { get; set; }
        public string dyr { get; set; }
        /// <summary>
        /// 交件人
        /// </summary>
        public string jjr { get; set; }
        public string ywlx { get; set; }
        public string djlx { get; set; }
        public string dyfs { get; set; }
        public string tel { get; set; }
        public string zl { get; set; }
        public string fj { get; set; }
        public string sjr { get; set; }
        public DateTime? sjsj { get; set; }
        public DateTime? cnrq { get; set; }
        public string PDFFile { get; set; }
    }
}
