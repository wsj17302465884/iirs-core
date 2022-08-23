namespace IIRS.Models.ViewModel.IIRS
{
    /// <summary>
    /// 电子缴款书参数实体类
    /// </summary>
    public class PayParamVModel
    {
        /// <summary>
        /// 区划
        /// </summary>
        public string region { get; set; } = "210000";
        /// <summary>
        /// 单位标识
        /// </summary>
        public string deptcode { get; set; } = "275005";
        /// <summary>
        /// 应用帐号
        /// </summary>
        public string appid { get; set; } = "LYSBDCDJZX6585092";
        /// <summary>
        /// 请求业务参数
        /// </summary>
        public string data { get; set; }
        /// <summary>
        /// 请求随机标识
        /// </summary>
        public string noise { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string version { get; set; } = "1.0";
        /// <summary>
        /// 签名
        /// </summary>
        public string sign { get; set; }
    }
}
