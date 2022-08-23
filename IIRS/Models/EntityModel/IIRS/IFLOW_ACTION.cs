using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("IFLOW_ACTION", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class IFLOW_ACTION
    {
        public IFLOW_ACTION()
        {


        }
        /// <summary>
        /// Desc:流程编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public short FLOW_ID { get; set; }

        /// <summary>
        /// Desc:流程名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FLOW_NAME { get; set; }

        /// <summary>
        /// Desc:流程分组编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int GROUP_ID { get; set; }
        /// <summary>
        /// 跳转VUE名称
        /// </summary>
        public string VUE_URL { get; set; }

        /// <summary>
        /// 跳转VUE名称
        /// </summary>
        public string BANK_VUE_URL { get; set; }


        /// <summary>
        /// VUE名称
        /// </summary>
        public string VUE_NAME { get; set; }

        /// <summary>
        /// 不动产流程对照名称
        /// </summary>
        public string BDC_FLOW_NAME { get; set; }

        /// <summary>
        /// 是否为流程终止节点：1:终止节点,0:非终止节点
        /// </summary>
        public string TERMINATION_NODE { get; set; }
    }
}
