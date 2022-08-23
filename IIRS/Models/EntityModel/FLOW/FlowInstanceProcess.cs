using SqlSugar;
using System;
namespace IIRS.Models.EntityModel
{
    [SugarTable("FLOW_INSTANCE_PROCESS")]
    public class FlowInstanceProcess
    {
        public Guid PK { get; set; }

        public Guid INSTANCE_ID { get; set; }

        public int FLOW_ID { get; set; }

        public string BUS_JSON { get; set; }

        public DateTime ACTION_TIME { get; set; }
    }
}
