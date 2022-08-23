using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC
{
    /// <summary>
    /// 待办任务
    /// </summary>
    public class AgencyTaskVModel
    {
        public string xid { get; set; }
        public string slbh { get; set; }
        public string qlrmc { get; set; }
        public string lczl { get; set; }
        public decimal? statusId { get; set; }
        public string status { get; set; }
        public DateTime? dateTime { get; set; }
        public string next_xid { get; set; }
        public int is_action_ok { get; set; }
        public string vue_url { get; set; }
        public string vue_name { get; set; }
        public string bank_vue_url { get; set; }
    }
}
