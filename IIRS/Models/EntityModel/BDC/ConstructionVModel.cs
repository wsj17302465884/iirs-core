using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    /// <summary>
    /// 在建工程不动产证明号查询实体类
    /// </summary>
    [SugarTable("V_CONSTRUCTION_QUERY", SysConst.DB_CON_BDC)]
    public partial class ConstructionVModel
    {
        public ConstructionVModel()
        {


        }

        public string tstybm { get; set; }

        /// <summary>
        /// 宗地图属统一编码
        /// </summary>
        public string zdtybm { get; set; }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string slbh { get; set; }
        /// <summary>
        /// 不动产证号
        /// </summary>
        public string bdczh { get; set; }

        /// <summary>
        /// 权利人名称
        /// </summary>
        public string qlrmc { get; set; }

        public string tdzl { get; set; }

        public decimal? fzmj { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string NewSlbh { get; set; }

        public string bdcdyh { get; set; }



    }
}
