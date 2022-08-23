using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    public class BdcGeneralMrgeQdPrintVModel
    {
        public BdcGeneralMrgeQdPrintVModel()
        {

        }
        public List<BdcGeneralMrgeQd> modelList { get; set; } = new List<BdcGeneralMrgeQd>();
        public string PDFFile { get; set; }
    }

    public class BdcGeneralMrgeQd
    {
        public string slbh { get; set; }
        public string bdczh { get; set; }
        public string bdcdyh { get; set; }
        public string zl { get; set; }
        public decimal? mj { get; set; }
        public decimal? ftmj { get; set; }
        public string qlrmc { get; set; }
        public string zjhm { get; set; }
        public string ghyt { get; set; }
        public string fwqlxz { get; set; }
        public string tdqlxz { get; set; }
        public string tdqlxz_zwm { get; set; }
        public string fj { get; set; }
        public string xgzlx { get; set; }
        public string fwSlbh { get; set; }
        public string tdSlbh { get; set; }
        public string gytdmj { get; set; }
        public string sjtdyt { get; set; }
        public string tdyt { get; set; }
        public string syqmj { get; set; }
        public string yt { get; set; }
        public string yt_zwm { get; set; }
    }
}
