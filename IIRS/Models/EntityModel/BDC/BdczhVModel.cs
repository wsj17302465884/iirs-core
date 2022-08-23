using IIRS.Utilities.Common;
using SqlSugar;
using System.ComponentModel;

namespace IIRS.Models.EntityModel.BDC
{
    /// <summary>
    /// 不动产证明号查询实体类
    /// </summary>
    [SugarTable("V_HOUSE_XGZH_QUERY", SysConst.DB_CON_BDC)]
    public partial class BdczhVModel
    {
        public BdczhVModel()
        {


        }
        [SugarColumn(IsIgnore = true)]
        public int xh { get; set; }

        /// <summary>
        /// 不动产证明号
        /// </summary>
        public string bdczmh { get; set; }
        /// <summary>
        /// 相关证号
        /// </summary>
        public string xgzh { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }

        /// <summary>
        /// 抵押人
        /// </summary>
        public string Dyr { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? dymj { get; set; }

        
    }
}
