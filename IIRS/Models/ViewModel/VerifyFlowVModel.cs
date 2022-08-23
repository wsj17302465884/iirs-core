using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel
{
    public class VerifyFlowVModel
    {
        /// <summary>
        /// 当前流程状态机状态ID
        /// </summary>
        public string AUZ_ID { get; set; }


        /// <summary>
        /// 历史XID
        /// </summary>
        public string NEXT_XID { get; set; }

        /// <summary>
        /// 历史流程状态下一节点状态
        /// </summary>
        public string NEXT_AUZ_ID { get; set; }

        /// <summary>
        /// 当前主流程状态节点
        /// </summary>
        public decimal? CURRENT_STATUS { get; set; }

        /// <summary>
        /// 当前流程节点前一节点ID
        /// </summary>
        public decimal? PRE_STATUS { get; set; }

        /// <summary>
        /// 受理编号
        /// </summary>
        public string SLBH { get; set; }

        /// <summary>
        /// 是否已经提交,1:已提交 2:未提交
        /// </summary>
        public int IS_ACTION_OK { get; set; }
    }
}
