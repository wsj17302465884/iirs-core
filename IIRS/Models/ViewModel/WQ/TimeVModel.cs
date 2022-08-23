using System;

namespace IIRS.Models.ViewModel.WQ

{


    public class TimeVModel
    {
        /// <summary>
        /// Desc:时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string starttime { get; set; }
        /// <summary>
        /// Desc:日期
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string cdate { get; set; }
        /// <summary>
        /// Desc:抵押状态 0开通 1关闭
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int mortState { get; set; }

        /// <summary>
        /// Desc:交易状态 0开通 1关闭
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int dealState { get; set; }
        /// <summary>
        /// Desc:公积金状态 0开通 1关闭
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int pubFundsState { get; set; }
        /// <summary>
        /// Desc:抵押预约时段人数
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int mortCount { get; set; }
        /// <summary>
        /// Desc:一体化预约时段人数
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int appointmentCount { get; set; }
        /// <summary>
        /// Desc:公积金预约时段人数
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int pubFundsCount { get; set; }
        /// <summary>
        /// Desc:单独交税时段人数
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int taxPayCount { get; set; }
       
    }

    
}
