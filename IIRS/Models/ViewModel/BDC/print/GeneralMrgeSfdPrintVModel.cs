using IIRS.Models.EntityModel.IIRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    /// <summary>
    /// 转移登记 - 收费单打印
    /// </summary>
    public class GeneralMrgeSfdPrintVModel
    {
        public GeneralMrgeSfdPrintVModel()
        {

        }
        public string slbh { get; set; }
        public string ywlx { get; set; }
        public string xmmc { get; set; }
        public string jfbh { get; set; }
        public string jfdw { get; set; }
        public string dh { get; set; }
        public string zl { get; set; }
        public decimal? ysje { get; set; }
        public string strYsje { get; set; }
        public decimal? ssje { get; set; }
        public string strSsje { get; set; }
        public string jbr { get; set; }
        public DateTime? jbrq { get; set; }
        public string skrq { get; set; }

        public string sfxm1 { get; set; }
        public string jldw1 { get; set; }
        public decimal? sl1 { get; set; }
        public decimal? sfbz1 { get; set; }
        public decimal? jmje1 { get; set; }
        public string jmyy1 { get; set; }
        public decimal? hsje1 { get; set; }
        public string bz1 { get; set; }
        public string sfxm2 { get; set; }
        public string jldw2 { get; set; }
        public decimal? sl2 { get; set; }
        public decimal? sfbz2 { get; set; }
        public decimal? jmje2 { get; set; }
        public string jmyy2 { get; set; }
        public decimal? hsje2 { get; set; }
        public string bz2 { get; set; }
        public string sfxm3 { get; set; }
        public string jldw3 { get; set; }
        public decimal? sl3 { get; set; }
        public decimal? sfbz3 { get; set; }
        public decimal? jmje3 { get; set; }
        public string jmyy3 { get; set; }
        public decimal? hsje3 { get; set; }
        public string bz3 { get; set; }
        public string sfxm4 { get; set; }
        public string jldw4 { get; set; }
        public decimal? sl4 { get; set; }
        public decimal? sfbz4 { get; set; }
        public decimal? jmje4 { get; set; }
        public string jmyy4 { get; set; }
        public decimal? hsje4 { get; set; }
        public string bz4 { get; set; }
        public string PDFFile { get; set; }
        public List<SFD_FB_INFO> sfdfbList { get; set; } = new List<SFD_FB_INFO>();
    }
}
