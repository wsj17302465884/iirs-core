using IIRS.Models.EntityModel.BDC;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.Condition
{
    public class ConditionQueryResultVModel
    {
        /// <summary>
        /// 图属统一编码
        /// </summary>
        public string tstybm { get; set; }
        /// <summary>
        /// 不动产类型
        /// </summary>
        public string bdclx { get; set; }
        /// <summary>
        /// 证书类型
        /// </summary>
        public string zslx { get; set; }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string slbh { get; set; }
        /// <summary>
        /// 不动产证号
        /// </summary>
        public string bdczh { get; set; }
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string bdcdyh { get; set; }
        /// <summary>
        /// 权利人名称
        /// </summary>
        public string qlrmc { get; set; }
        /// <summary>
        /// 证件类别
        /// </summary>
        public string zjlb { get; set; }
        /// <summary>
        /// 证件类别中文名
        /// </summary>
        public string zjlb_zwm { get; set; }
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
        /// 规划用途
        /// </summary>
        public string ghyt { get; set; }
        /// <summary>
        /// 土地权利类型
        /// </summary>
        public string tdqllx { get; set; }
        /// <summary>
        /// 土地权利性质
        /// </summary>
        public string tdqlxz { get; set; }
        /// <summary>
        /// 土地规划用途
        /// </summary>
        public string tdghyt { get; set; }
        /// <summary>
        /// 房屋建筑面积
        /// </summary>
        public decimal? jzmj { get; set; }
        /// <summary>
        /// 土地面积
        /// </summary>
        public decimal? tdmj { get; set; }
        /// <summary>
        /// 权利类型中文名
        /// </summary>
        public string qllx_zwm { get; set; }
        /// <summary>
        /// 权利性质中文名
        /// </summary>
        public string qlxz_zwm { get; set; }
        /// <summary>
        /// 规划用途中文名
        /// </summary>
        public string ghyt_zwm { get; set; }
        /// <summary>
        /// 土地权利类型中文名
        /// </summary>
        public string tdqllx_zwm { get; set; }
        /// <summary>
        /// 土地权利性质中文名
        /// </summary>
        public string tdqlxz_zwm { get; set; }
        /// <summary>
        /// 土地规划用途中文名
        /// </summary>
        public string tdghyt_zwm { get; set; }
        /// <summary>
        /// 房屋状态
        /// </summary>
        public string zt { get; set; }
        /// <summary>
        /// 是否选中
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool isOk { get; set; }

        public List<DJ_QLRGL> qlrList = new List<DJ_QLRGL>();
    }
}
