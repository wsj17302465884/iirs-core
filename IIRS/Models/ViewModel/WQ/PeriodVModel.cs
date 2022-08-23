using System;

namespace IIRS.Models.ViewModel.WQ

{


    public class PeriodVModel
    {
        /// <summary>
        /// Desc:id
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Guid ID { get; set; }
        /// <summary>
        /// 时间段开始时间 - 只有时间部分有意义
        /// </summary>
        public string STARTTIME { get; set; }
        /// <summary>
        ///  是否为上午时段 0是 1 否
        /// </summary>
        public int ISAM { get; set; }


    }

    
}
