using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("SYS_DIC", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class SYS_DIC
    {
           public SYS_DIC(){


           }
        /// <summary>
        /// 字典编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey=true)]
        public string DIC_ID { get;set;}

        /// <summary>
        /// 分组编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int GID { get;set;}

        /// <summary>
        /// 字典项名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DNAME { get; set; }

        /// <summary>
        /// 是否启用
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int IS_USED { get;set;}

        /// <summary>
        /// 自定义分组编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DEFINED_CODE { get;set;}

        /// <summary>
        /// 排序
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal ODR { get;set;}

        /// <summary>
        /// 状态标识1
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int MARK { get;set;}

        /// <summary>
        /// 备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REAMRK { get;set;}

        public string itemNote { get; set; }

        /// <summary>
        /// 父节点编号
        /// </summary>
        public string FDIC_ID { get; set; }

        /// <summary>
        /// tree子集菜单名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<SYS_DIC> children { get; set; }// = new List<SYS_DIC>();
    }
}
