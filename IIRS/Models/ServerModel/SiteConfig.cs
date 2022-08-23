using System.Collections.Generic;

namespace IIRS.Models.ServerModel
{
    /// <summary>
    /// 系统配置类
    /// </summary>
    public class SiteConfig
    {
        /// <summary>
        /// 程序名称
        /// </summary>
        public string ApiName { get; set; }

        /// <summary>
        /// Web API 描述文档名称
        /// 在项目 -> 属性 -> 生成XML文档中指定的文件名
        /// </summary>
        public string XMLDoc { get; set; }

        /// <summary>
        /// Web API 前缀
        /// /{RoutePrefix}/controller/action
        /// </summary>
        public string RoutePrefix { get; set; }

        /// <summary>
        /// 允许访问系统的IP白名单列表，以";"分割，留空为不限制
        /// </summary>
        public string AllowIP { get; set; }

        /// <summary>
        /// 系统主数据库
        /// </summary>
        public string MainDB { get; set; }

        /// <summary>
        /// 数据库连接集合
        /// </summary>
        public List<MutiDBOperate> DBS { get; set; }

        /// <summary>
        /// SignalR服务地址
        /// </summary>
        public string SignalRPath { get; set; }

        /// <summary>
        /// 法院WebAPI地址
        /// </summary>
        public string LawApiPath { get; set; }

        /// <summary>
        /// AccessToken加密信息
        /// </summary>
        public Encrypt Encrypt { get; set; }
    }

    /// <summary>
    /// 加密信息类
    /// </summary>
    public class Encrypt
    {
        /// <summary>
        /// 密钥字符串
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// 发行人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 读者
        /// </summary>
        public string Audience { get; set; }
    }
}
