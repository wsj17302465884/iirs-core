using System;
using System.Collections.Generic;

namespace IIRS.Models.ViewModel
{
    /// <summary>
    /// 不动产相关证号信息查询
    /// </summary>
    public class ImmovablesRelationVModel
    {
        /// <summary>
        /// 图属统一编码
        /// </summary>
        public string Tstybm { get; set; }

        /// <summary>
        /// 不动产证号
        /// </summary>
        public string Bdczh { get; set; }

        /// <summary>
        /// 不动产证号
        /// </summary>
        public DateTime? DJRQ { get; set; }

        /// <summary>
        /// 坐落
        /// </summary>
        public string Zl { get; set; }

        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? Jzmj { get; set; }
       
        /// <summary>
        /// 登记受理编号
        /// </summary>
        public string Slbh { get; set; }

        

        /// <summary>
        /// 权利人名称
        /// </summary>
        public string QLR { get; set; }

        /// <summary>
        /// 义务人名称
        /// </summary>
        public string YWR { get; set; }

        /// <summary>
        /// 登记种类
        /// </summary>
        public string DJZL { get; set; }

        /// <summary>
        /// 房产隶属土地（或土地地上房产）抵押提示信息
        /// </summary>
        public string TDDYXX { get; set; }

        /// <summary>
        /// 登记类型
        /// </summary>
        public string DJLX { get; set; }
    }
}
