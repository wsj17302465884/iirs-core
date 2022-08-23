using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.IIRS
{
    public class PrintHouseVModel
    {
        /// <summary>
        /// 不动产证号
        /// </summary>
        public string Bdczh { get; set; }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string Slbh { get; set; }
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string Bdcdyh { get; set; }
        /// <summary>
        /// 权利类型
        /// </summary>
        public string Qllx { get; set; }
        /// <summary>
        /// 权利性质
        /// </summary>
        public string Qlxz { get; set; }
        /// <summary>
        /// 
        /// </summary>土地权利类型
        public string Tdqllx { get; set; }
        /// <summary>
        /// 土地权利性质
        /// </summary>
        public string Tdqlxz { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal? Jzmj { get; set; }
        /// <summary>
        /// 独用土地面积
        /// </summary>
        public decimal? Gytdmj { get; set; }

        public decimal? Dytdmj { get; set; }
    }
}
