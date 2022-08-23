using SqlSugar;
using System;
namespace IIRS.Models.EntityModel.IIRS
{
    [SugarTable("IFlowAction", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class IFlowAction
    {
        public int FLOW_ID { get; set; }

        public int GROUP_ID { get; set; }

        public string FLOW_NAME { get; set; }
    }
}
