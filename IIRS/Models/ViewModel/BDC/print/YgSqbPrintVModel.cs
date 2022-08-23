using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    /// <summary>
    /// 不动产预告登记申请表
    /// </summary>
    public class YgSqbPrintVModel
    {
        public YgSqbPrintVModel()
        {

        }
        /// <summary>
        /// 原受理编号
        /// </summary>
        public string slbh { get; set; } = "";
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime? sjrq { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string sjr { get; set; }
        /// <summary>
        /// 登记事由：一般抵押 最高额抵押 在建工程抵押 其它
        /// </summary>
        public string djsy { get; set; }
        /// <summary>
        /// 登记类型：首次登记 转移登记 变更登记 注销登记 预告登记 其它
        /// </summary>
        public string djlx { get; set; }
        /// <summary>
        /// 权利人名称
        /// </summary>
        public string qlrxm { get; set; }
        /// <summary>
        /// 权利人证件类别
        /// </summary>
        public string qlrzjzl { get; set; }
        /// <summary>
        /// 权利人证件号码
        /// </summary>
        public string qlrzjhm { get; set; }
        /// <summary>
        /// 义务人名称
        /// </summary>
        public string ywrmc { get; set; }
        /// <summary>
        /// 义务人证件类别
        /// </summary>
        public string ywrzjzl { get; set; }
        /// <summary>
        /// 义务人证件号码
        /// </summary>
        public string ywrzjhm { get; set; }
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string bdcdyh { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 不动产类型
        /// </summary>
        public string bdclx { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public string mj { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string yt { get; set; }
        /// <summary>
        /// 原不动产证书号
        /// </summary>
        public string ybdczsh { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? jzmj { get; set; }
        /// <summary>
        /// 土地面积
        /// </summary>
        public decimal? tdmj { get; set; }
        /// <summary>
        /// 土地独用面积
        /// </summary>
        public decimal? tddymj { get; set; }
        /// <summary>
        /// 共有土地面积
        /// </summary>
        public decimal? gytdmj { get; set; }
        /// <summary>
        /// 土地类型
        /// </summary>
        public string tdlx { get; set; }
        
        /// <summary>
        /// 不动产权利设定期限
        /// </summary>
        public string bdcqlsdqx { get; set; }
        /// <summary>
        /// 被担保债权数额
        /// </summary>
        public decimal? bdbzqse { get; set; }
        /// <summary>
        /// 债务履行期限（债权确定期限）
        /// </summary>
        public string zwlxqx { get; set; }
        /// <summary>
        /// 在建建筑物抵押范围
        /// </summary>
        public string zjjzwdyfw { get; set; }
        /// <summary>
        /// 需役地范围
        /// </summary>
        public string xydzl { get; set; }
        /// <summary>
        /// 需役地不动产单元号
        /// </summary>
        public string xydbdcdyh { get; set; }
        /// <summary>
        /// 登记原因
        /// </summary>
        public string djyy { get; set; }
        /// <summary>
        /// 权利起始时间
        /// </summary>
        public DateTime? qlqssj { get; set; }
        /// <summary>
        /// 权利结束时间
        /// </summary>
        public DateTime? qljssj { get; set; }

        /// <summary>
        /// 登记原因 证明文件
        /// </summary>
        public string djyyzmwj { get; set; }
        /// <summary>
        /// 附记
        /// </summary>
        public string fj { get; set; }
       

        public string PDFFile { get; set; }
    }
}
