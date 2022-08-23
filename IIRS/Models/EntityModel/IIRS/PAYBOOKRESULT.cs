using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.IIRS
{
    /// <summary>
    /// 缴款书返回结果
    /// </summary>
    [SugarTable("PAYBOOKRESULT", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class PAYBOOKRESULT
    {
        public PAYBOOKRESULT()
        {

        }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string BUSNO { get; set; }
        /// <summary>
        /// 缴款码
        /// </summary>
        public string PAYCODE { get; set; }
        /// <summary>
        /// 缴款书号
        /// </summary>
        public string PAYNO { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CREATETIME { get; set; }
        /// <summary>
        /// 财政是否已缴费
        /// </summary>
        public string ISPAY { get; set; }
        /// <summary>
        /// 银行是否已缴费
        /// </summary>
        public string BANKISPAY { get; set; }
        /// <summary>
        /// 缴费时间
        /// </summary>
        public DateTime? PAYDATE { get; set; }
        /// <summary>
        /// 电子票据Url
        /// </summary>
        public string PICTUREURL { get; set; }
        /// <summary>
        /// 缴费地址Url
        /// </summary>
        public string PAYURL { get; set; }

    }
}
