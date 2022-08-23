using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC
{
    /// <summary>
    /// 
    /// </summary>
    public class MrgeCertInfoVModel
    {
        public int RN { get; set; }
        
        public string SLBH { get; set; }
        public string BDCZMH { get; set; }

        public string DBFW { get; set; }

        public string DYLX { get; set; }

        public string ZL { get; set; }

        public string BDCLX { get; set; }

        public string QLRMC { get; set; }
        public string ZT { get; set; }

        public string BDCDYH { get; set; }

        public decimal? BDBZZQSE { get; set; }

        public decimal? ZGZQSE { get; set; }

        /// <summary>
        /// 是否进行中,1:进行中,0:未开始
        /// </summary>
        public int IS_DOING { get; set; } = 0;

        public List<MrgeCertInfoVModel> children { get; set; } = new List<MrgeCertInfoVModel>();

        public bool hasChildren { get; set; } = false;
    }
}
