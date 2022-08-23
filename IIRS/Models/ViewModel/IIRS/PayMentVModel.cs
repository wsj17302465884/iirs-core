using System.Collections.Generic;

namespace IIRS.Models.ViewModel.IIRS
{
    /// <summary>
    /// 缴费实体类
    /// </summary>
    public class PayMentVModel
    {
        public PayMentVModel()
        {

        }
        /// <summary>
        /// 业务流水号
        /// </summary>
        public string busNo { get; set; }
        /// <summary>
        /// 开票点编码
        /// </summary>
        public string placeCode { get; set; } = "001";
        /// <summary>
        /// 缴款书种类编码
        /// </summary>
        public string billCode { get; set; } = "0300";
        /// <summary>
        /// 有效日期
        /// </summary>
        public string effectiveDate { get; set; }
        /// <summary>
        /// 交款人类型
        /// </summary>
        public string payerType { get; set; }
        /// <summary>
        /// 缴款人
        /// </summary>
        public string payer { get; set; }
        /// <summary>
        /// 经办人
        /// </summary>
        public string author { get; set; }
        /// <summary>
        /// 缴款人证件号
        /// </summary>
        public string idCardNo { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        public string tel { get; set; }
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string email { get; set; } = "";
        /// <summary>
        /// 总金额
        /// </summary>
        public decimal? totalAmt { get; set; }
        
        /// <summary>
        /// 收费项目明细
        /// </summary>
        public List<chargeDetail> chargeDetail { get; set; } = new List<chargeDetail>();
    }

    public class chargeDetail
    {
        /// <summary>
        /// 收费项目编码
        /// </summary>
        public string chargeCode { get; set; } = "079904";
        /// <summary>
        /// 收费项目名称
        /// </summary>
        public string chargeName { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        public string unit { get; set; }
        /// <summary>
        /// 收费标准
        /// </summary>
        public decimal? std { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public decimal? number { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public decimal? amt { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
    }
}
