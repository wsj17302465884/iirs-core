using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    public class MrgeReleaseSqbVModel
    {
        public MrgeReleaseSqbVModel() { }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string slbh { get; set; }
        /// <summary>
        /// 收件人
        /// </summary>
        public string sjr { get; set; }
        /// <summary>
        /// 收件日期
        /// </summary>
        public DateTime? sjrq { get; set; }
/*        /// <summary>
        /// 登记类型
        /// </summary>
        public string djlx { get; set; }*/
        /// <summary>
        /// 登记事由
        /// </summary>
        public string djsy { get; set; }
        /// <summary>
        /// 登记类别
        /// </summary>
        public string djlb { get; set; }
        /// <summary>
        /// 抵押权人名称
        /// </summary>
        public string dyqrmc { get; set; }
        /// <summary>
        /// 抵押权人证件类别
        /// </summary>
        public string dyqr_zjlb { get; set; }
        /// <summary>
        /// 抵押权人证件号码
        /// </summary>
        public string dyqr_zjhm { get; set; }
        /// <summary>
        /// 抵押人名称
        /// </summary>
        public string dyrmc { get; set; }
        /// <summary>
        /// 抵押人证件类别
        /// </summary>
        public string dyr_zjlb { get; set; }
        /// <summary>
        /// 抵押人证件号码
        /// </summary>
        public string dyr_zjhm { get; set; }
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string bdcdyh { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 权利类型
        /// </summary>
        public string qllx { get; set; }
        /// <summary>
        /// 权利性质
        /// </summary>
        public string qlxz { get; set; }
        /// <summary>
        /// 不动产证书号
        /// </summary>
        public string bdczsh { get; set; }
        /// <summary>
        /// 不动产证明号
        /// </summary>
        public string bdczmh { get; set; }
        /// <summary>
        /// 面积
        /// </summary>
        public decimal? mj { get; set; }
        /// <summary>
        /// 被担保债权数额
        /// </summary>
        public decimal? bdbzqse { get; set; }

        /// <summary>
        /// 债务履行期限
        /// </summary>
        public string zwlxqx { get; set; }
        /// <summary>
        /// 担保范围
        /// </summary>
        public string dbfw { get; set; }
        /// <summary>
        /// 是否存在约定
        /// </summary>
        public string sfyd { get; set; }

        /// <summary>
        /// 登记原因
        /// </summary>
        public string djyy { get; set; }
        /// <summary>
        /// 登记原因证明文件
        /// </summary>
        public string djyyzmwj { get; set; }
        /// <summary>
        /// 登记备注
        /// </summary>
        public string bz { get; set; }

        public string PDFFile { get; set; }
    }
 
}
