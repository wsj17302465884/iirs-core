using IIRS.Models.EntityModel.IIRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.TAX
{
    public class InsertTaxVModel
    {        
        public string newSlbh { get; set; }
        public string bdczh { get; set; }
        public string bdcdyh { get; set; }
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
        public string xzqh_code { get; set; }
        /// <summary>
        /// 乡镇街道
        /// </summary>
        public string xzjd { get; set; }
        /// <summary>
        /// 行政区域
        /// </summary>
        public string xzqy { get; set; }
        /// <summary>
        /// 所属税务机关
        /// </summary>
        public string ssswjg { get; set; }
        /// <summary>
        /// 房屋类别
        /// </summary>
        public string fwlb { get; set; }
        public string fwlb_id { get; set; }
        /// <summary>
        /// 权属转移对象
        /// </summary>
        public string qszydx { get; set; }
        public string qszydx_fclb { get; set; }
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
        public string fwcx { get; set; }
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
        /// <summary>
        /// 交易价格
        /// </summary>
        public decimal? jyje { get; set; }

        public decimal? pgje { get; set; }

        public List<qlrInfo> sw_qlrList { get; set; } = new List<qlrInfo>();
        public List<qlrInfo> sw_ywrList { get; set; } = new List<qlrInfo>();


    }
    public class qlrInfo
    {

        public string glbm { get; set; }

        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string slbh { get; set; }

        /// <summary>
        /// Desc:业务编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ywbm { get; set; }

        /// <summary>
        /// Desc:权利人ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string qlrid { get; set; }

        /// <summary>
        /// 是否权属人
        /// </summary>
        public int is_owner { get; set; }

        /// <summary>
        /// Desc:共有方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string gyfs { get; set; }

        /// <summary>
        /// Desc:共有份额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string gyfe { get; set; }

        /// <summary>
        /// Desc:顺序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? sxh { get; set; }

        /// <summary>
        /// Desc:权利人类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string qlrlx { get; set; }

        /// <summary>
        /// Desc:权利人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string qlrmc { get; set; }

        /// <summary>
        /// Desc:证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string zjhm { get; set; }

        /// <summary>
        /// Desc:证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string zjlb { get; set; }

        /// <summary>
        /// Desc:证件名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string zjlb_zwm { get; set; }

        /// <summary>
        /// Desc:电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string dh { get; set; }
        /// <summary>
        /// 登记信息主键
        /// </summary>
        public string xid { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public string iscertified { get; set; }
    }
}
