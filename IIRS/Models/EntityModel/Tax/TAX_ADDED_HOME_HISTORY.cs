using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.Tax
{
    ///<summary>
    ///增量房交易历史表
    ///</summary>
    [SugarTable("TAX_ADDED_HOME_HIST", SysConst.DB_CON_TAX)]
    public partial class TAX_ADDED_HOME_HISTORY
    {
        

        public TAX_ADDED_HOME_HISTORY()
        {


        }
        /// <summary>
        /// Desc:交易流水号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public Guid TAX_PK { get; set; }

        /// <summary>
        /// Desc:业务合同编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HTBH { get; set; }

        /// <summary>
        /// Desc:地址
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DZ { get; set; }

        /// <summary>
        /// Desc:行政区域代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string XQ_SWJG_DM { get; set; }

        /// <summary>
        /// Desc:行政区划代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string XZQHSZDM { get; set; }

        /// <summary>
        /// Desc:街道乡镇代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string JDXZDM { get; set; }

        /// <summary>
        /// Desc:所属税务机关代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SS_SWJG_DM { get; set; }

        /// <summary>
        /// Desc:房屋小区代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string ZZXQ_DM { get; set; }

        /// <summary>
        /// Desc:房屋产权证书号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CQCJH { get; set; }

        /// <summary>
        /// Desc:楼号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FDC_LH { get; set; }

        /// <summary>
        /// Desc:单元
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DY { get; set; }

        /// <summary>
        /// Desc:楼层
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LC { get; set; }

        /// <summary>
        /// Desc:房屋类别
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FWLB_DM { get; set; }

        /// <summary>
        /// Desc:建筑结构
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string JZJG_DM { get; set; }

        /// <summary>
        /// Desc:建筑地上层数
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FC_JZWDSCS { get; set; }

        /// <summary>
        /// Desc:建筑地下层数
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FC_JZWDXCS { get; set; }

        /// <summary>
        /// Desc:房屋起始层
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FC_FWQSC { get; set; }

        /// <summary>
        /// Desc:房屋截止层
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FC_FWJZC { get; set; }

        /// <summary>
        /// Desc:房屋总层数
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FWZCS { get; set; }

        /// <summary>
        /// Desc:房屋所在层数
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FWSZCS { get; set; }

        /// <summary>
        /// Desc:套内面积
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TNMJ { get; set; }

        /// <summary>
        /// Desc:建筑面积
        /// Default:
        /// Nullable:False
        /// </summary>           
        public decimal FWJZMJ { get; set; }

        /// <summary>
        /// Desc:契税权属转移对象代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string QSQSZYDX_DM { get; set; }

        /// <summary>
        /// Desc:房屋朝向代码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FWCX_DM { get; set; }

        /// <summary>
        /// Desc:交易类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JYLX_DM { get; set; }

        /// <summary>
        /// Desc:交易日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime JYHTRQ { get; set; }

        /// <summary>
        /// Desc:合同成交价格
        /// Default:
        /// Nullable:False
        /// </summary>           
        public decimal HTCJJG { get; set; }

        /// <summary>
        /// Desc:不动产单元号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCDYH { get; set; }

        /// <summary>
        /// Desc:创建日期
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime CDATE { get; set; }

        /// <summary>
        /// Desc:发送报税时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? TAX_TIME { get; set; }

        /// <summary>
        /// Desc:是否已报税,0,：未报税，1：已报税
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int IS_TAX { get; set; }

        /// <summary>
        /// Desc:状态,0,：未发送，1：待发送，2：待确认
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int STATE { get; set; }

        /// <summary>
        /// 受理编号
        /// </summary>
        public string SLBH { get; set; }

        /// <summary>
        /// 税务合同编号
        /// </summary>
        public string TAX_HTBH { get; set; }

        /// <summary>
        /// 权属转移用途
        /// </summary>
        public string QSQSZYYT_DM { get; set; }
        /// <summary>
        /// 权属转移方式
        /// </summary>
        public string QSQSZYLB_DM { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public string FH { get; set; }
        /// <summary>
        /// 单价
        /// </summary>
        public decimal DJ { get; set; }
        /// <summary>
        /// 交易价格
        /// </summary>
        public decimal JYJG { get; set; }
        /// <summary>
        /// 合同金额
        /// </summary>
        public decimal HTJE { get; set; }
        /// <summary>
        /// 当期应收款金额
        /// </summary>
        public decimal DQYSKJE { get; set; }
        /// <summary>
        /// 合同签订时间
        /// </summary>
        public DateTime? HTQDSJ { get; set; }
        /// <summary>
        /// 当期应收税款所属月
        /// </summary>
        public string DQYSSKSSYF { get; set; }
        /// <summary>
        /// 购买方信息
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<TAX_ADDED_HOME_BUYER_HISTORY> GMFXX { get; set; } = new List<TAX_ADDED_HOME_BUYER_HISTORY>();

      
        public string MSG { get; internal set; }
        public string EX_MSG { get; internal set; }
        public int SEND_TIMES { get;  set; }
        
        public string POST_DATA { get; internal set; }
        /// <summary>
        /// 判断房屋类型 0 住宅 1 商业
        /// </summary>
        public int IS_BUS { get; internal set; }
    }
}
