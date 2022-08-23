using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.IIRS
{
    [SugarTable("BANKRELATEDIIRS", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class BANKRELATEDIIRS
    {
        public BANKRELATEDIIRS()
        {

        }
        [SugarColumn(IsPrimaryKey = true)]
        public Guid RelatedId { get; set; }
        /// <summary>
        /// 生成银行请求编码
        /// </summary>
        public Guid BankId { get; set; }
        /// <summary>
        /// 银行系统参考号
        /// </summary>
        public string Srcsys { get; set; }
        /// <summary>
        /// iirs库主键BID
        /// </summary>
        public string IIRSBid { get; set; }
        /// <summary>
        /// 抵押受理编号
        /// </summary>
        public string Slbh { get; set; }

        /// <summary>
        /// 请求时间
        /// </summary>
        public DateTime QueryDate { get; set; }

    }
}
