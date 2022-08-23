using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    public class GeneralMrgeSpbPrintVModel
    {
        public GeneralMrgeSpbPrintVModel()
        {

        }
        /// <summary>
        /// 原受理编号
        /// </summary>
        public string slbh { get; set; }
        public string oldSlbh { get; set; }
        /// <summary>
        /// 登记原因
        /// </summary>
        public string djyy { get; set; }
        /// <summary>
        /// 权利人名称
        /// </summary>
        public string qlrmc { get; set; }
        public string qlr_zjhm { get; set; }
        public string qlr_zjlb { get; set; }
        /// <summary>
        /// 义务人名称
        /// </summary>
        public string ywrmc { get; set; }
        public string ywr_zjhm { get; set; }
        public string ywr_zjlb { get; set; }
        public string tstybm { get; set; }
        /// <summary>
        /// 不动产证号
        /// </summary>
        public string bdczmh { get; set; }
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string bdcdyh { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 权利设定方式
        /// </summary>
        public string qlsdfs { get; set; }
        /// <summary>
        /// 权利类型
        /// </summary>
        public string qllx { get; set; }
        /// <summary>
        /// 权利性质
        /// </summary>
        public string qlxz { get; set; }
        /// <summary>
        /// 不动产类型
        /// </summary>
        public string bdclx { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? jzmj { get; set; }
        /// <summary>
        /// 原不动产证号
        /// </summary>
        public string old_bdczh { get; set; }
        /// <summary>
        /// 规划用途
        /// </summary>
        public string ghyt { get; set; }
        /// <summary>
        /// 共有方式
        /// </summary>
        public string gyfs { get; set; }
        /// <summary>
        /// 共有份额
        /// </summary>
        public string gyfe { get; set; }
        /// <summary>
        /// 使用期限
        /// </summary>
        public string syqx { get; set; }

        /// <summary>
        /// 被担保债权数额
        /// </summary>
        public decimal? bdbzqse { get; set; }
        /// <summary>
        /// 债务履行期限
        /// </summary>
        public string lxqx { get; set; }
        /// <summary>
        /// 其他
        /// </summary>
        public string qt { get; set; }
        public string spbz { get; set; }
        public string fj { get; set; }

        public string xid { get; set; }
        public string ywslbh { get; set; }
        public string userId { get; set; }
        public string djzl { get; set; }
        public string sjr { get; set; }
        public int IsActionOk { get; set; }
        public DateTime? djrq { get; set; }
        public string xgzh { get; set; }
        public string ghyt_zwm { get; set; }
    }
}
