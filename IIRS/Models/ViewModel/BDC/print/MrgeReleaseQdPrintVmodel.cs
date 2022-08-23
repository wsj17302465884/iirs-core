using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    public class MrgeReleaseQdPrintVmodel
    {
        public MrgeReleaseQdPrintVmodel()
        {

        }
        public List<MrgeReleaseQdPrint> modelList { get; set; } = new List<MrgeReleaseQdPrint>();

        public string PDFFile { get; set; }

    }

    public class MrgeReleaseQdPrint
    {
        public string slbh { get; set; }
        public string bdczmh { get; set; }
        public string bdczh { get; set; }
        public string zl { get; set; }
        public decimal? mj { get; set; }
        public string qlrmc { get; set; }
        public string yt { get; set; }
        public string zslx { get; set; }


    }


}
