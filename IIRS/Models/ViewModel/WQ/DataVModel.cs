using System;

namespace IIRS.Models.ViewModel.WQ

{

    public class DataVModel
    {        
        public string code { get; set; }
        public DataVModelR data { get; set; }
        public string message { get; set; }
        
    }

    public class DataVModelR
    {
        /// <summary>
        /// Desc:日期
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string cdate { get; set; }
        /// <summary>
        /// Desc:日期
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string scheduledate { get; set; }
        
        /// <summary>
        /// Desc星期
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string week { get; set; }
        /// <summary>
        /// Desc:是否休息  0是 1 否
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int isRest { get; set; }
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

    public class scheduleVModel
    {
        /// <summary>
        /// Desc:日期
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string scheduledate { get; set; }

        /// <summary>
        /// Desc类型名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string name { get; set; }
        /// <summary>
        /// Desc:放号量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int scheduleamount { get; set; }
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

    public class typeVModel
    {
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Guid ID { get; set; }
        /// <summary>
        /// Desc:ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public Guid AMOUNTID { get; set; }
        
        /// <summary>
        /// Desc:名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string TYPENAME { get; set; }

        /// <summary>
        /// Desc:时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string STARTTIME { get; set; }
        /// <summary>
        /// Desc:数量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int COUNT { get; set; }
        /// <summary>
        /// Desc:剩余数量
        /// Default:
        /// Nullable:False
        /// </summary>           
        public int USERDAMOUNT { get; set; }
    }
}
