using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    [SugarTable("T_ENUM_FC_FWJG", SysConst.DB_CON_BDC)]
    public class T_ENUM_FC_FWJG_EModel
    {
        /// <summary>
        /// 房屋结构编号
        /// </summary>
        public string FWJGID { get; set; }

        /// <summary>
        /// 房屋结构名称
        /// </summary>
        public string FWJGMC { get; set; }

        /// <summary>
        /// 房屋结构名称
        /// </summary>
        public string FWJGQC { get; set; }
    }
}
