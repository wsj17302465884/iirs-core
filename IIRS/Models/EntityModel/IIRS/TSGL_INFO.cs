using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///图属关联表
    ///</summary>
    [SugarTable("TSGL_INFO", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class TSGL_INFO
    {
        public TSGL_INFO()
        {


        }
        /// <summary>
        /// Desc:关联编码
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string GLBM { get; set; }

        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SLBH { get; set; }

        /// <summary>
        /// Desc:不动产类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCLX { get; set; }

        /// <summary>
        /// Desc:不动产图属统一编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TSTYBM { get; set; }

        /// <summary>
        /// Desc:不动产单元号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCDYH { get; set; }

        /// <summary>
        /// Desc:登记种类(审批通过后赋值)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DJZL { get; set; }

        /// <summary>
        /// Desc:产生时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CSSJ { get; set; }

        /// <summary>
        /// Desc:现实\历史状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? LIFECYCLE { get; set; }

        /// <summary>
        /// 登记信息主键
        /// </summary>
        public string XID { get; set; }
    }
}
