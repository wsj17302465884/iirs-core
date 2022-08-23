using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.BANK
{
    /// <summary>
    /// 权利登记信息
    /// </summary>
    public class Rght_Rgs_Inf
    {
        /// <summary>
        /// 权利登记信息
        /// </summary>
        public Rght_Rgs_Inf()
        {

        }
        /// <summary>
        /// 不动产权关联证号
        /// </summary>
        public string RealEst_Wght_Rltv_CrtfNo { get; set; }
        /// <summary>
        /// 权利登记日期时间
        /// </summary>
        public string Rght_RgDt_Tm { get; set; }

        public List<Rght_Rgs_Inf_Rght_Psn_Inf> Rght_Rgs_Inf_Rght_Psn_Inf { get; set; } = new List<Rght_Rgs_Inf_Rght_Psn_Inf>();
    }
}
