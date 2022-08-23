using System;
using System.Linq;
using System.Text;
using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DJ_QLRGL", SysConst.DB_CON_BDC)]
    public partial class DJ_QLRGL
    {
        public DJ_QLRGL()
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
        /// Desc:持证方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CZFS { get; set; }

        /// <summary>
        /// Desc:持证编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CZBH { get; set; }

        /// <summary>
        /// Desc:不动产证号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCZH { get; set; }

        /// <summary>
        /// Desc:证书序列号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZSXLH { get; set; }

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
        /// Desc:关系
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GX { get; set; }

        /// <summary>
        /// Desc:现实\历史状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? LIFECYCLE { get; set; }

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

        /// Desc:证件类别中文名
        /// Default:
        /// Nullable:True
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string zjlb_zwm { get; set; }

        /// <summary>
        /// Desc:电子证书序列号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DZZSXLH { get; set; }

        /// <summary>
        /// Desc:电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DH { get; set; }

    }
}
