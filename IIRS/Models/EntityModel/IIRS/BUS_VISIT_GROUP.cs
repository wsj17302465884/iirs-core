using IIRS.Utilities.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.IIRS
{
    [SugarTable("BUS_VISIT_GROUP", SysConst.DB_CON_IIRS)]
    public class BUS_VISIT_GROUP
    {
        public BUS_VISIT_GROUP()
        {


        }
        /// <summary>
        /// Desc:分组编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? GROUP_ID { get; set; }
        /// <summary>
        /// Desc:分组名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GROUP_NAME { get; set; }
        /// <summary>
        /// Desc:显示名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DISPLAY_NAME { get; set; }

    }
}
