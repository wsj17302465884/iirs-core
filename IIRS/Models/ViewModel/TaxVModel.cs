using IIRS.Models.EntityModel.IIRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel
{
    public class TaxVModel
    {
        /// <summary>
        /// 房产类型：存量、增量
        /// </summary>
        public string fclx { get; set; }
        /// <summary>
        /// 房产类别：住宅、非住宅
        /// </summary>
        public string fclb { get; set; }
        /// <summary>
        /// 坐落地址
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 行政区划
        /// </summary>
        public string xzqh { get; set; }
        /// <summary>
        /// 乡镇街道
        /// </summary>
        public List<SYS_DIC> xzjd { get; set; }
        /// <summary>
        /// 行政区域
        /// </summary>
        public List<SYS_DIC> xzqy { get; set; }
        /// <summary>
        /// 所属税务机关
        /// </summary>
        public List<SYS_DIC> ssswjg { get; set; }
        /// <summary>
        /// 房屋类别
        /// </summary>
        public string fwlb { get; set; }
        public string fwlb_id { get; set; }
        /// <summary>
        /// 权属转移对象
        /// </summary>
        public string qszydx { get; set; }
        public string qszydx_dCode { get; set; }
        /// <summary>
        /// 房屋小区
        /// </summary>
        public string fwxq { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public string jylx { get; set; }
        public string jylx_id { get; set; }
        /// <summary>
        /// 幢号
        /// </summary>
        public string zh { get; set; }
        /// <summary>
        /// 单元号
        /// </summary>
        public string dyh { get; set; }
        /// <summary>
        /// 门牌号
        /// </summary>
        public string mph { get; set; }
        /// <summary>
        /// 总层数
        /// </summary>
        public string zcs { get; set; }
        /// <summary>
        /// 不动产房屋结构
        /// </summary>
        public string bdcfwjg { get; set; }
        /// <summary>
        /// 税务房屋结构
        /// </summary>
        public string swfwjg { get; set; }
        public string swfwjg_id { get; set; }
        /// <summary>
        /// 所在基础层
        /// </summary>
        public string szjcc { get; set; }
        /// <summary>
        /// 房屋朝向
        /// </summary>
        public List<SYS_DIC> fwcx { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? jzmj { get; set; }
        /// <summary>
        /// 分摊面积
        /// </summary>
        public decimal? ftmj { get; set; }
        /// <summary>
        /// 套内面积
        /// </summary>
        public decimal? tnmj { get; set; }
        /// <summary>
        /// 收件时间
        /// </summary>
        public string sjsj { get; set; }
        
    }
}
