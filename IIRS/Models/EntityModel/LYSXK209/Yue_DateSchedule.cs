using System;
using SqlSugar;

namespace IIRS.Models.EntityModel.LYSXK209
{
    /// <summary>
    /// 日期计划表 - 存储每天每个时间段每个类型的放号计划
    /// 每天定时任务，插入14天（两周）后的数据
    /// </summary>
    [SugarTable("YUE_DATESCHEDULE", Utilities.Common.SysConst.DB_CON_LYSXK209)]
    public class Yue_DateSchedule
    {
        public Yue_DateSchedule()
        {
        }

        /// <summary>
        /// 唯一编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid ID { get; set; }

        /// <summary>
        /// 计划日期 - 只有日期部分有意义
        /// </summary>
        public string SCHEDULEDATE { get; set; }

        /// <summary>
        /// 时间段类型计划表关联编号
        /// </summary>
        public Guid AMOUNT_ID { get; set; }

        /// <summary>
        /// 实际放号数量
        /// </summary>
        public int SCHEDULEAMOUNT { get; set; }

        /// <summary>
        /// 剩余放号数量
        /// </summary>
        public int USEDAMOUNT { get; set; }
    }
}
