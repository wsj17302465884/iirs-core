using System;
namespace IIRS.Models.ViewModel
{
    /// <summary>
    /// Access Token类
    /// </summary>
    public class TokenModel
    {
        /// <summary>
        /// Access Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public double ExpiresIn { get; set; }

        /// <summary>
        /// Token类型
        /// </summary>
        public string TokenType { get; set; }
    }
}
