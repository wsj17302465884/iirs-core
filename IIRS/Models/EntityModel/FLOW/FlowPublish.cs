using SqlSugar;
using System;
namespace IIRS.Models.EntityModel
{
    [SugarTable("FLOW_PUBLISH")]
    public class FlowPublish
    {
        public int PUBLISH_ID { get; set; }

        public int FLOW_ID { get; set; }

        public int IS_PUBLISH { get; set; }

        public DateTime CDATE { get; set; }
    }
}
