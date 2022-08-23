using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("QLRGL_INFO", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class QLRGL_INFO
    {
        public QLRGL_INFO()
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
        /// Desc:业务编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YWBM { get; set; }

        /// <summary>
        /// Desc:权利人ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string QLRID { get; set; }

        /// <summary>
        /// 是否权属人
        /// </summary>
        public int IS_OWNER { get; set; }

        /// <summary>
        /// Desc:共有方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GYFS { get; set; }

        /// <summary>
        /// Desc:共有份额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GYFE { get; set; }

        /// <summary>
        /// Desc:顺序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? SXH { get; set; }

        /// <summary>
        /// Desc:权利人类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QLRLX { get; set; }

        /// <summary>
        /// Desc:权利人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QLRMC { get; set; }

        /// <summary>
        /// Desc:证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJHM { get; set; }

        /// <summary>
        /// Desc:证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJLB { get; set; }

        /// <summary>
        /// Desc:证件名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJLB_ZWM { get; set; }

        /// <summary>
        /// Desc:电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DH { get; set; }
        /// <summary>
        /// 登记信息主键
        /// </summary>
        public string XID { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string iscertified { get; set; }
        
    }
}
