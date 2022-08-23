using IIRS.Models.EntityModel.IIRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.IIRS
{
    public class PdfVModel
    {
        public List<OrderHouseAssociation> houseList { get; set; } = new List<OrderHouseAssociation>();

        public List<PUB_ATT_FILE> fileList { get; set; } = new List<PUB_ATT_FILE>();

        public string slbh { get; set; }
        public DateTime? sqrq { get; set; }

        public string Strsqrq { get; set; }
        public string dyfs { get; set; }
        public decimal? bdbzzqse { get; set; }
        public DateTime? qlqssj { get; set; }
        public DateTime? qljssj { get; set; }

        public string Strqlqssj { get; set; }
        public string Strqljssj { get; set; }
        public string bid { get; set; }
        public string dylx { get; set; }
        public string dyqr { get; set; }
        public string dyqrzjlb { get; set; }
        public string dyqrzjhm { get; set; }
        public string dyr { get; set; }
        public string dyrzjlb { get; set; }
        public string dyrzjhm { get; set; }
        public string sjr { get; set; }

    }
}
