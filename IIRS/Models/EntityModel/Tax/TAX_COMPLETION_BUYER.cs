using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.Tax
{
    [SugarTable("TAX_COMPLETION_BUYER", SysConst.DB_CON_TAX)]
    public class TAX_COMPLETION_BUYER
    {
        /// <summary>
        /// 流水号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string PK { get; set; }

        /// <summary>
        /// 完易流水号
        /// </summary>           
        public string TAX_CPK { get; set; }

        /// <summary>
        /// 系统税票号码
        /// </summary>           
        public string XTSPHM { get; set; }

        /// <summary>
        /// 纳税人识别号
        /// </summary>           
        public string NSRSBH { get; set; }

        /// <summary>
        /// 纳税人名称
        /// </summary>           
        public string NSRMC { get; set; }

        /// <summary>
        /// 征收项目名称
        /// </summary>           
        public string ZSXM { get; set; }

        /// <summary>
        /// 征收品目名称
        /// </summary>           
        public string ZSPM { get; set; }

        /// <summary>
        /// 税额
        /// </summary>           
        public decimal SE { get; set; }
    }
}
