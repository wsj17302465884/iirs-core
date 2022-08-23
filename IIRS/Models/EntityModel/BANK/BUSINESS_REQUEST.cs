using IIRS.Utilities.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.BANK
{
    [SugarTable("BUSINESS_REQUEST", SysConst.DB_CON_BANK)]
    public class BUSINESS_REQUEST
    {
        public BUSINESS_REQUEST()
        {

        }

        /// <summary>
        /// 序号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid SID { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string SERIALNUMBER { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int ZT { get; set; }
        /// <summary>
        /// 请求日期
        /// </summary>
        public DateTime REQUESTDATE { get; set; }
        /// <summary>
        /// 业务类型
        /// </summary>
        public string BUSINESS_TYPE { get; set; }
        /// <summary>
        /// IIRS库主表业务号
        /// </summary>
        public string BID { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string RESULT { get; set; }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string SLBH { get; set; }
    }
}
