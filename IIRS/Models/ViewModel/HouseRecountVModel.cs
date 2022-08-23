using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel
{
    /// <summary>
    /// 房屋追述视图信息
    /// </summary>
    public class HouseRecountListVModel
    {
        /// <summary>
        /// 当前受理编号
        /// </summary>
        public HouseRecountVModel currentList { get; set; } = new HouseRecountVModel();

        /// <summary>
        /// 历史
        /// </summary>
        public List<HouseRecountVModel> historyList { get; set; } = new List<HouseRecountVModel>();

        /// <summary>
        /// 未来
        /// </summary>
        public List<HouseRecountVModel> futureList { get; set; } = new List<HouseRecountVModel>();
    }


    /// <summary>
    /// 房屋追述视图信息
    /// </summary>
    public class HouseRecountVModel
    {
        /// <summary>
        /// 图属统一编码
        /// </summary>
        public string TSTYBM { get; set; }

        /// <summary>
        /// 受理编号
        /// </summary>
        public string SLBH { get; set; }

        /// <summary>
        /// 登记大类--收件单
        /// </summary>
        public string DJDL { get; set; }

        /// <summary>
        /// 不动产单元号--当前登记簿
        /// </summary>
        public string BDCDYH { get; set; }

        /// <summary>
        /// 不动产证号--当前登记簿
        /// </summary>
        public string BDCZH { get; set; }

        /// <summary>
        /// 登记日期 --当前登记簿
        /// LyfeCycle==null 或者-1,并且DJRQ=null
        /// </summary>
        public DateTime? DJRQ { get; set; }


        public string State { get; set; }

        public List<HouseRecountVModel> NextRecount { get; set; } = new List<HouseRecountVModel>();

    }
}
