using IIRS.Utilities.Common;
using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BDC
{
    /// <summary>
    /// 在建工程抵押变更查询实体类
    /// </summary>
    [SugarTable("V_CONSTRUCTION_CHANGE", SysConst.DB_CON_BDC)]
    public partial class ConstructionChangeVModel
    {
        public ConstructionChangeVModel()
        {


        }

        /// <summary>
        /// 图属统一编码
        /// </summary>
        public string tstybm { get; set; }

        /// <summary>
        /// 宗地统一编码
        /// </summary>
        public string zdtybm { get; set; }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string slbh { get; set; }
        /// <summary>
        /// 不动产证号
        /// </summary>
        public string xgzh { get; set; }
        /// <summary>
        /// 不动产证明号
        /// </summary>
        public string bdczmh { get; set; }

        /// <summary>
        /// 抵押人名称
        /// </summary>
        public string dyr { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 抵押面积
        /// </summary>
        public decimal? dymj { get; set; }

        public string djzl { get; set; }

        public string tdqllx { get; set; }

        public string tdqlxz { get; set; }

        public string tdyt { get; set; }

        public string tdsyqx { get; set; }

        public string qt { get; set; }

        public DateTime djrq { get; set; }

        public string fj { get; set; }

        //[SugarColumn(IsIgnore = true)]
        //public string NewSlbh { get; set; }

        //public string bdcdyh { get; set; }



    }
}
