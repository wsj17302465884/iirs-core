using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DJ_SFD_FB", SysConst.DB_CON_BDC)]
    public partial class DJ_SFD_FB
    {
        public DJ_SFD_FB()
        {


        }
        /// <summary>
        /// Desc:财务收费单续表编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string CWSFDXBBH { get; set; }

        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SLBH { get; set; }

        /// <summary>
        /// Desc:清单序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? QDXH { get; set; }

        /// <summary>
        /// Desc:收费项目
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SFXM { get; set; }

        /// <summary>
        /// Desc:计量单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JLDW { get; set; }

        /// <summary>
        /// Desc:数量
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? SL { get; set; }

        /// <summary>
        /// Desc:收费标准
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? SFBZ { get; set; }

        /// <summary>
        /// Desc:核收金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? HSJE { get; set; }

        /// <summary>
        /// Desc:减免金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? JMJE { get; set; }

        /// <summary>
        /// Desc:减免原因
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JMYY { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BZ { get; set; }

        /// <summary>
        /// Desc:清单类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QDLX { get; set; }

    }
}
