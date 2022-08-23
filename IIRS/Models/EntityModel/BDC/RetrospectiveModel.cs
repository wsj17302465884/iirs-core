using IIRS.Utilities.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.BDC
{
    [SugarTable("V_RETROSPECTIVE_QUERY", SysConst.DB_CON_BDC)]
    public class RetrospectiveModel
    {
        public RetrospectiveModel()
        {

        }
        /// <summary>
        /// 不动产类型
        /// </summary>
        public string bdclx { get; set; }
        /// <summary>
        /// 不动产证号
        /// </summary>
        public string bdczh { get; set; }
        /// <summary>
        /// 权利人名称
        /// </summary>
        public string qlrmc { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        public string zjhm { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? jzmj { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 共有土地面积
        /// </summary>
        public decimal? gytdmj { get; set; }
        /// <summary>
        /// 独用土地面积
        /// </summary>
        public decimal? dytdmj { get; set; }

    }
}
