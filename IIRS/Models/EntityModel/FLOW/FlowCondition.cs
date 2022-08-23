using SqlSugar;
using System;
namespace IIRS.Models.EntityModel
{
    [SugarTable("FLOW_CONDITION")]
    public class FlowCondition
    {
        public int CONDITION_ID { get; set; }

        public int CAID { get; set; }

        public int TAR_ACTION_ID { get; set; }

        public string EXPR { get; set; }
    }
}
