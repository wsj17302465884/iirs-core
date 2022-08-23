using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.IIRS
{
    /// <summary>
    /// 业务判断条件实体类
    /// </summary>
    public class BusinessJudgmentVModel1
    {
        /// <summary>
        /// 权证受理编号
        /// </summary>
        public string qz_slbh { get; set; }
        /// <summary>
        /// 当前办理业务受理编号
        /// </summary>
        public string yw_slbh { get; set; }
        /// <summary>
        /// 当前抵押受理编号
        /// </summary>
        public string dy_slbh { get; set; }
        /// <summary>
        /// 图属统一编码
        /// </summary>
        public string tstybm { get; set; }
        /// <summary>
        /// 不动产类型：房屋或者宗地
        /// </summary>
        public string bdclx { get; set; }
        /// <summary>
        /// 业务类型：抵押或抵押变更
        /// </summary>
        public string ywlx { get; set; }
    }
}
