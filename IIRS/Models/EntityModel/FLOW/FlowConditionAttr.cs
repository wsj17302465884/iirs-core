using SqlSugar;
using System;
namespace IIRS.Models.EntityModel
{
    [SugarTable("FLOW_CONDITION_ATTR")]
    public class FlowConditionAttr
    {
        public Guid PK { get; set; }

        public int CONDITION_ID { get; set; }

        public string ATTR_NAME { get; set; }

        public int ATTR_TYPE { get; set; }
    }
}
