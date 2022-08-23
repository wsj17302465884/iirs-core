using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("IFLOW_DO_ACTION", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class IFLOW_DO_ACTION
    {
        public IFLOW_DO_ACTION()
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
        /// Desc:流程编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public int FLOW_ID { get; set; }

        /// <summary>
        /// Desc:前流程编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PRE_FLOW_ID { get; set; }

        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string AUZ_ID { get; set; }

        /// <summary>
        /// Desc:创建时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CDATE { get; set; }

        /// <summary>
        /// Desc:办理人姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string USER_NAME { get; set; }
        /// <summary>
        /// 前一流程处理状态
        /// </summary>
        public int? PRE_STATUS { get; set; }
    }
}
