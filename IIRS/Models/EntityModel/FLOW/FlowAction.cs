using SqlSugar;
using System;
namespace IIRS.Models.EntityModel
{
    [SugarTable("FLOW_ACTION")]
    public class FlowAction
    {
        public int ACTION_ID { get; set; }

        public int PUBLISH_ID { get; set; }

        public string ACTION { get; set; }

        public string NAV_URL { get; set; }

        public string REMARK { get; set; }

        public int TAR_ACTION_ID { get; set; }

        public int TAR_IS_CONDITION { get; set; }

        public int IS_FLOW_CTL { get; set; }

        public int ACTION_MARK { get; set; }
    }
}
