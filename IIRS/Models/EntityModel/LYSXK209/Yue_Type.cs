using System;
using SqlSugar;

namespace IIRS.Models.EntityModel.LYSXK209
{
    /// <summary>
    /// 预约类型表 - 存储预约类型
    /// </summary>
    [SugarTable("YUE_TYPE", Utilities.Common.SysConst.DB_CON_LYSXK209)]
    public class Yue_Type
    {
        public Yue_Type()
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
        /// 预约类型名称
        /// </summary>
        public string NAME { get; set; }

        /// <summary>
        /// 预约类型提示 - 如办理需要的要件等，用于提示用户
        /// </summary>
        public string TIPS { get; set; }

        /// <summary>
        /// 是否不需要协办人
        /// 目前预约类型中，除 单独交税 类型外，均需要协办人
        /// </summary>
        public int NONEEDASSIST { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int ORDERSORT { get; set; }
    }
}
