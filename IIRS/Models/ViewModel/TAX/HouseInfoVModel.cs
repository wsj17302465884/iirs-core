using System;
using System.Collections.Generic;

namespace IIRS.Models.ViewModel.TAX

{
    public class HouseInfoVModel
    {

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
        /// Desc:不动产单元号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCDYH { get; set; }
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
        public string postdata { get; set; }
    }
}
