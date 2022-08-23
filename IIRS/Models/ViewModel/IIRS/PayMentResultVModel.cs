namespace IIRS.Models.ViewModel.IIRS
{
    /// <summary>
    /// 缴款书返回结果
    /// </summary>
    public class PayMentResultVModel
    {
        public string result { get; set; }
        public message message { get; set; } = new message();
    }

    public class message
    {
        /// <summary>
        /// 业务流水号
        /// </summary>
        public string busNo { get; set; }
        /// <summary>
        /// 缴款码
        /// </summary>
        public string payCode { get; set; }
        /// <summary>
        /// 缴款书号
        /// </summary>
        public string payNo { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string createTime { get; set; }
        /// <summary>
        /// 收款人全称
        /// </summary>
        public string recName { get; set; }
        /// <summary>
        /// 收款人账号
        /// </summary>
        public string recNo { get; set; }
        /// <summary>
        /// 收款人开户银行
        /// </summary>
        public string recBank { get; set; }
    }
}
