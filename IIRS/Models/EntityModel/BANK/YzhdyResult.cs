using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.BANK
{
    /// <summary>
    /// 已在行抵押请求结果
    /// </summary>
    public class YzhdyResult
    {
        /// <summary>
        /// 已在行抵押请求结果
        /// </summary>
        public YzhdyResult()
        {
        }
        [SugarColumn(IsPrimaryKey = true)]
        public string RID { get; set; }

        /// <summary>
        /// Desc:不动产登记证明号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string REALEST_RGSCTF_NO { get; set; }
        /// <summary>
        /// Desc:不动产权证号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string REALEST_WRNT_NO { get; set; }
    }
}
