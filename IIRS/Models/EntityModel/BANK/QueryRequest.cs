using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.BANK
{
    /// <summary>
    /// 查询请求
    /// </summary>
    public class QueryRequest
    {
        /// <summary>
        /// 查询请求
        /// </summary>
        public QueryRequest()
        {

        }
        /// <summary>
        /// 序号
        /// </summary>
        public string REQUESTID { get; set; }
        /// <summary>
        /// IIRS生成有效流水号
        /// </summary>
        public string SERIALNUMBER { get; set; }
        /// <summary>
        /// 来源系统
        /// </summary>
        public string SRCSYS { get; set; }
        /// <summary>
        /// 不动产所属区县代码
        /// </summary>
        public string RGON_CD { get; set; }
        /// <summary>
        /// 不动产权证号
        /// </summary>
        public string REALEST_WRNT_NO { get; set; }
        /// <summary>
        /// 不动产登记证明号
        /// </summary>
        public string BDCZMH { get; set; }
        /// <summary>
        /// 不动产权人姓名
        /// </summary>
        public string REALEST_WGHT_PSN_NM { get; set; }
        /// <summary>
        /// 不动产权人类型
        /// </summary>
        public string REALEST_WGHT_PSN_TP { get; set; }
        /// <summary>
        /// 不动产权人证件类型
        /// </summary>
        public string REALEST_WGHT_PSN_CRDT_TP { get; set; }
        /// <summary>
        /// 不动产权人证件号码
        /// </summary>
        public string REALEST_WGHT_PSN_CRDT_NO { get; set; }
        /// <summary>
        /// 0:已在行抵押 1：未在行抵押
        /// </summary>
        public int ALREADY_IN { get; set; }
        /// <summary>
        /// 委托书ID
        /// </summary>
        public string WTSID { get; set; }
    }
}
