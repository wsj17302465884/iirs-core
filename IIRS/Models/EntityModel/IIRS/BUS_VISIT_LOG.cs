using IIRS.Utilities.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.IIRS
{
    [SugarTable("BUS_VISIT_LOG", SysConst.DB_CON_IIRS)]
    public class BUS_VISIT_LOG
    {
        public BUS_VISIT_LOG()
        {


        }
        /// <summary>
        /// Desc:主键流水
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PK { get; set; }
        /// <summary>
        /// Desc:分组编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? GROUP_ID { get; set; }
        /// <summary>
        /// Desc:组织机构ID(顶级)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ORGANIZATION_ID { get; set; }
        /// <summary>
        /// Desc:操作人编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string USER_ID { get; set; }
        /// <summary>
        /// Desc:操作人姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string USER_NAME { get; set; }
        /// <summary>
        /// Desc:访问时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? VDATE { get; set; }
        /// <summary>
        /// Desc:访问参数列表
        /// Default:
        /// Nullable:True
        /// </summary>
        public string PARAMS { get; set; }
        /// <summary>
        /// Desc:显示名称
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string DISPLAY_NAME { get; set; }
        /// <summary>
        /// Desc:访问参数列表全
        /// Default:
        /// Nullable:True
        /// </summary>
         [SugarColumn(IsIgnore = true)]
        public string PARAMSWHOLE { get; set; }

    }
}
