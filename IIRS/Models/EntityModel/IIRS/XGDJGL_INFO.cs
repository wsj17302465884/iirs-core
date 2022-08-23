using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("XGDJGL_INFO", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class XGDJGL_INFO
    {
        public XGDJGL_INFO()
        {


        }
        /// <summary>
        /// Desc:变更编码
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string BGBM { get; set; }

        /// <summary>
        /// Desc:子受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZSLBH { get; set; }

        /// <summary>
        /// Desc:父受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FSLBH { get; set; }

        /// <summary>
        /// Desc:变更日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? BGRQ { get; set; }

        /// <summary>
        /// Desc:变更类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BGLX { get; set; }

        /// <summary>
        /// Desc:相关证号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XGZH { get; set; }

        /// <summary>
        /// Desc:相关证类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XGZLX { get; set; }


        public string PID { get; set; }
        /// <summary>
        /// 登记信息主键
        /// </summary>
        public string XID { get; set; }
    }
}
