using IIRS.Utilities.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.GGK
{
    /// <summary>
    /// 收费标准明细表
    /// </summary>
    [SugarTable("DIC_ITEM", SysConst.DB_CON_LYGGK)]
    public class Lyggk_DicItem
    {
        /// <summary>
        /// 收费标准明细表
        /// </summary>
        public Lyggk_DicItem()
        {

        }
        public string itemId { get; set; }
        public string dicCode { get; set; }
        public string itemName { get; set; }
        public string itemVal { get; set; }
        public string itemNote { get; set; }
    }
}
