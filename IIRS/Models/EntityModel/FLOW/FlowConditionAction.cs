using SqlSugar;
using System;
namespace IIRS.Models.EntityModel
{
    [SugarTable("FLOW_CONDITION_ACTION")]
    public class FlowConditionAction
    {
        public int CAID { get; set; }

        public string CNAME { get; set; }
    }
}
