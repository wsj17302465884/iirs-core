using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.print
{
    /// <summary>
    /// 不动产抵押预告登记申请表
    /// </summary>
    public class DyYgSqbPrintVModel
    {
        public DyYgSqbPrintVModel()
        {

        }
        /// <summary>
        /// 原受理编号
        /// </summary>
        public string slbh { get; set; } = "";
        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime? sjsj { get; set; }
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
        public string qlr_zjzl { get; set; }
        /// <summary>
        /// 权利人证件号码
        /// </summary>
        public string qlr_zjhm { get; set; }
        /// <summary>
        /// 义务人名称
        /// </summary>
        public string ywrmc { get; set; }
        /// <summary>
        /// 义务人证件类别
        /// </summary>
        public string ywr_zjzl { get; set; }
        /// <summary>
        /// 义务人证件号码
        /// </summary>
        public string ywr_zjhm { get; set; }
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
        /// 不动产证号
        /// </summary>
        public string xgzh { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public string mj { get; set; }
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
        /// 担保范围
        /// </summary>
        public string dbfw { get; set; }
        /// <summary>
        /// 是约定
        /// </summary>
        public string sfyd1 { get; set; }
        /// <summary>
        /// 否约定
        /// </summary>
        public string sfyd2 { get; set; }
        /// <summary>
        /// 权利起始时间
        /// </summary>
        public DateTime? qlqssj { get; set; }
        /// <summary>
        /// 权利结束时间
        /// </summary>
        public DateTime? qljssj { get; set; }
        /// <summary>
        /// 登记原因
        /// </summary>
        public string djyy { get; set; }
        /// <summary>
        /// 登记原因 证明文件
        /// </summary>
        public string zmwj { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
       

        public string PDFFile { get; set; }
    }
}
