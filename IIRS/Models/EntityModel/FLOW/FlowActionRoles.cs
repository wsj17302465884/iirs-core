using SqlSugar;
using System;
namespace IIRS.Models.EntityModel
{
    [SugarTable("FLOW_ACTION_ROLES")]
    public class FlowActionRoles
    {
        public int ACTION_ID { get; set; }

        public Guid ROLE_ID { get; set; }
    }
}
