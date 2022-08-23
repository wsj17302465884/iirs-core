using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.Print
{
    public class MrgeReleaseSpbVModel
    {
        public MrgeReleaseSpbVModel() { }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string slbh { get; set; }
        /// <summary>
        /// 权利人姓名(名称)
        /// </summary>
        public string qlrmc { get; set; }
        /// <summary>
        /// 不动产权证明
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
        /// 面积
        /// </summary>
        public string jzmj { get; set; }
        /// <summary>
        /// 原不动产权证书号
        /// </summary>
        public string old_bdcqzh { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string ghyt { get; set; }
        /// <summary>
        /// 使用期限
        /// </summary>
        public string syqx { get; set; }
        /// <summary>
        /// 义务人姓名(名称)
        /// </summary>
        public string ywrmc { get; set; }
        /// <summary>
        /// 被担保债权数额 （最高债权额）
        /// </summary>
        public decimal? bdbzqse { get; set; }
        /// <summary>
        /// 债务履行期限 （债权确定期间）
        /// </summary>
        public string? lxqx { get; set; }
        /// <summary>
        /// 担保范围
        /// </summary>
        public string dbfw { get; set; }
        /// <summary>
        /// 是否存在禁止或限制 转让抵押不动产的约定是
        /// </summary>
        public string yd1 { get; set; }
        /// <summary>
        ///         /// 是否存在禁止或限制 转让抵押不动产的约定否
        /// </summary>
        public string yd2 { get; set; }
        /// <summary>
        /// 其他 权利 情况
        /// </summary>
        public string qt { get; set; }
        /// <summary>
        /// 初审意见
        /// </summary>
        public string csyj { get; set; }
        /// <summary>
        /// 审查人初审人
        /// </summary>
        public string sjr { get; set; }
        /// <summary>
        /// 审查初审时间
        /// </summary>
        public DateTime? cssj { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string bz { get; set; }
        public decimal? fwmj { get; set; }
        public decimal? tdmj { get; set; }
        public string qlrlx { get; set; }
        public string td_qllx_zwm { get; set; }
        public string PDFFile { get; set; }




    }

}
