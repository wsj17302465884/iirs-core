using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("LST_FILE")]
    public partial class LST_FILE
    {
        public LST_FILE()
        {

        }

        /// <summary>
        /// Desc:业务主键编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string FID { get; set; }
        /// <summary>
        /// Desc:操作员姓名
        /// Default:
        /// Nullable:False
        /// </summary>
        public string USERNAME { get; set; }
        /// <summary>
        /// Desc:目录名称
        /// Default:
        /// Nullable:False
        /// </summary>
        public string TITLE { get; set; }


        /// <summary>
        /// Desc:创建日期
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime? CDATE { get; set; }
        /// <summary>
        /// Desc:路径
        /// Default:
        /// Nullable:False
        /// </summary>
        public string FILEPATH { get; set; }
        /// <summary>
        /// Desc:所属类别
        /// Default:
        /// Nullable:False
        /// </summary>
        public string SSLB { get; set; }
        /// <summary>
        /// Desc:base64
        /// Default:
        /// Nullable:False
        /// </summary>
        public string base64 { get; set; }
    }
}
