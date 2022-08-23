using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.BANK
{
    public class Admn_Rst
    {
        /// <summary>
        /// 行政限制
        /// </summary>
        public Admn_Rst()
        {

        }
        /// <summary>
        /// 序号
        /// </summary>
        public Guid XZid { get; set; }
        /// <summary>
        /// 行政限制ID
        /// </summary>
        public string XZXZ_ID { get; set; }
        /// <summary>
        /// 行政限制开始日期
        /// </summary>
        public string Admn_Rst_StDt { get; set; }
        /// <summary>
        /// 行政限制解除日期
        /// </summary>
        public string Admn_Rst_Rlv_Dt { get; set; }
        /// <summary>
        /// 行政限制内容
        /// </summary>
        public string Admn_Rst_Cntnt { get; set; }
    }
}
