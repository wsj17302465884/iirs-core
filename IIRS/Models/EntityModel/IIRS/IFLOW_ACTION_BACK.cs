using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("IFLOW_ACTION_BACK", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class IFLOW_ACTION_BACK
    {
        public IFLOW_ACTION_BACK()
        {


        }
        /// <summary>
        /// Desc:流水号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string PK { get; set; }

        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string XID { get; set; }

        /// <summary>
        /// Desc:流程节点编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int FLOW_ID { get; set; }

        /// <summary>
        /// Desc:处理人ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string USER_ID { get; set; }

        /// <summary>
        /// Desc:处理人
        /// </summary>           
        public string USER_NAME { get; set; }

        /// <summary>
        /// Desc:处理时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime BTIIME { get; set; }


        [SugarColumn(IsIgnore = true)]
        public string BackTiemStr {
            get
            {
                return this.BTIIME.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// Desc:退回原因
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BACK_REMARK { get; set; }

    }
}
