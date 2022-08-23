using IIRS.Models.EntityModel.BANK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BANK
{
    public class YzhdyResultVModel
    {
        /// <summary>
        /// 已在行抵押查询返回结果
        /// </summary>
        public YzhdyResultVModel()
        {

        }
        
        /// <summary>
        /// 不动产登记证明号
        /// </summary>
        public string RealEst_RgsCtf_No { get; set; }
        /// <summary>
        /// 抵押权人信息
        /// </summary>
        public List<MRTG_WGHT_PSN_INF> Mrtg_Wght_Psn_Inf { get; set; } = new List<MRTG_WGHT_PSN_INF>();
        /// <summary>
        /// 抵押方式
        /// </summary>
        public string Mrtg_Mod { get; set; }
        /// <summary>
        /// 不动产信息
        /// </summary>
        public List<REALEST_INF> RealEst_Inf { get; set; } = new List<REALEST_INF>();
        
    }
}
