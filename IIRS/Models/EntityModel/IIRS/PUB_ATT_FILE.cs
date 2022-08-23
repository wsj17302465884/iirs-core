using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("PUB_ATT_FILE")]
    public partial class PUB_ATT_FILE
    {
        public PUB_ATT_FILE()
        {

        }
        /// <summary>
        /// Desc:文件主键编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string FILE_ID { get; set; }

        /// <summary>
        /// Desc:字典分类编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GROUP_ID { get;set;}

        /// <summary>
        /// Desc:字典分类名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GROUP_NAME { get; set; }

        /// <summary>
        /// Desc:附件名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FILE_NAME { get; set; }
        
        /// <summary>
        /// Desc:附件显示名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DISPLAY_NAME {get;set;}

        /// <summary>
        /// Desc:附件文件类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MEDIA_TYPE { get; set; }

        /// <summary>
        /// Desc:关联业务系统主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string BUS_PK { get; set; }

        /// <summary>
        /// 备注编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string BAK_PK { get; set; }

        /// <summary>
        /// 排序
        /// Nullable:False
        /// </summary>           
        public decimal ODR { get; set; }

        /// <summary>
        /// Desc:路径
        /// Default:
        /// Nullable:False
        /// </summary>
        [SugarColumn(ColumnName = "FILE_PATH")]
        public string PATH { get; set; }

        /// <summary>
        /// Desc:创建日期
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime CDATE { get; set; }

        [SugarColumn(IsIgnore = true)]
        public int a { get; set; }

        [SugarColumn(IsIgnore = true)]
        public int xh { get; set; }

        public string XID { get; set; }
    }
}
