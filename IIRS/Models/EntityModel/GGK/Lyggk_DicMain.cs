using IIRS.Utilities.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.GGK
{
    /// <summary>
    /// 收费信息：缴费类型
    /// </summary>
    [SugarTable("DIC_MAIN", SysConst.DB_CON_LYGGK)]
    public class Lyggk_DicMain
    {
        public Lyggk_DicMain()
        {

        }
        public string dicCode { get; set; }
        public string dicName { get; set; }
        public string tableName { get; set; }
        public string dicType { get; set; }
        public string dicNote { get; set; }
        public string pid { get; set; }
    }
}
