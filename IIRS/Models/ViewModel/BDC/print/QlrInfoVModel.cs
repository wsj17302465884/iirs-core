using IIRS.Models.EntityModel.IIRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    public class QlrInfoVModel
    {
        public PersonInfo qlrList { get; set; } = new PersonInfo();
        public PersonInfo ywrList { get; set; } = new PersonInfo();
        public PersonInfo dyrList { get; set; } = new PersonInfo();
        public PersonInfo dyqrList { get; set; } = new PersonInfo();
    }

    public class PersonInfo
    {
        public string qlrmc { get; set; }
        public string zjhm { get; set; }
        public string zjlb_zwm { get; set; }
    }
}
