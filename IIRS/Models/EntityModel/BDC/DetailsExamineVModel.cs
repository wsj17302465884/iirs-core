using IIRS.Utilities.Common;
using SqlSugar;
using System.Collections.Generic;

namespace IIRS.Models.EntityModel.BDC
{
    /// <summary>
    /// 不动产证明号查询实体类
    /// </summary>
    [SugarTable("V_DETAILSEXAMINE", SysConst.DB_CON_BDC)]
    public partial class DetailsExamineVModel
    {
        public DetailsExamineVModel()
        {


        }

        /// <summary>
        /// 图属统一编码
        /// </summary>
        public string tstybm { get; set; }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string slbh { get; set; }
        /// <summary>
        /// 登记种类
        /// </summary>
        public string djzl { get; set; }
                

        /// <summary>
        /// 不动产证号
        /// </summary>
        public string bdczh { get; set; }

        /// <summary>
        /// 抵押证明号
        /// </summary>
        public string bdczmh { get; set; }

        /// <summary>
        /// 查封文号
        /// </summary>
        public string cfwh { get; set; }

        /// <summary>
        /// 异议不动产证明号
        /// </summary>
        public string yybdczmh { get; set; }

        /// <summary>
        /// 预告不动产证明号
        /// </summary>
        public string ygbdczmh { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<DetailsExamineVModel> Children { get; set; }
    }
}
