using System;
using SqlSugar;

namespace IIRS.Models.EntityModel.LYSXK209
{
    /// <summary>
    /// 时间段类型计划表 - 存储每个时间段每个类型的默认放号计划
    /// </summary>
    [SugarTable("YUE_PERIODTYPEAMOUNT", Utilities.Common.SysConst.DB_CON_LYSXK209)]
    public class Yue_PTAmount
    {
        public Yue_PTAmount()
        {
        }

        /// <summary>
        /// 唯一编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid ID { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        public bool ISDELETED { get; set; }

        /// <summary>
        /// 时间段编号
        /// </summary>
        public Guid PERIOD_ID { get; set; }

        /// <summary>
        /// 类型编号
        /// </summary>
        public Guid TYPE_ID { get; set; }

        /// <summary>
        /// 计划放号数量
        /// </summary>
        public int SCHEDULEAMOUNT { get; set; }
    }
}
