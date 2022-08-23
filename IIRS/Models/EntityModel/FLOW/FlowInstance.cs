using RT.Comb;
using SqlSugar;
using System;
namespace IIRS.Models.EntityModel
{
    [SugarTable("FLOW_INSTANCE")]
    public class FlowInstance
    {
        public FlowInstance()
        {
            this.INSTANCE_ID = Provider.Sql.Create();
            this.CDATE = DateTime.Now;
        }

        [SugarColumn(IsPrimaryKey = true)]
        public Guid INSTANCE_ID { get; set; }

        public int PUBLISH_ID { get; set; }

        public int CURRENT_ACTION { get; set; }

        public string BUS_JSON { get; set; }

        public DateTime CDATE { get; set; }

        public int END_MARK { get; set; }
    }
}
