using SqlSugar;
using System;
namespace IIRS.Models.EntityModel.IIRS
{
    [SugarTable("TestTable", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class TestTable
    {
        public Guid id { get; set; }

        public string Name { get; set; }

        public bool IsEnable { get; set; }

        public DateTime EditDate { get; set; }
    }
}
