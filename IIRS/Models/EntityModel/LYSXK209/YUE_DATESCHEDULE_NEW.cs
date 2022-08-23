using System;
using SqlSugar;

namespace IIRS.Models.EntityModel.LYSXK209
{
    [SugarTable("YUE_DATESCHEDULE_NEW", Utilities.Common.SysConst.DB_CON_LYSXK209)]
    public class YUE_DATESCHEDULE_NEW
    {
        public YUE_DATESCHEDULE_NEW()
        {

        }
        /// <summary>
        /// 主键编号
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 日期
        /// </summary>
        public string cdate { get; set; }

        /// <summary>
        /// 星期
        /// </summary>
        public string week { get; set; }
        /// <summary>
        ///  是否休息  0是 1 否
        /// </summary>
        public int isrest { get; set; }
        /// <summary>
        /// 抵押状态 0开通 1关闭
        /// </summary>
        public int mortstate { get; set; }

        /// <summary>
        ///      交易状态 0开通 1关闭
        /// </summary>
        public int dealstate { get; set; }

        /// <summary>
        ///   公积金状态 0开通 1关闭
        /// </summary>
        public int pubfundsstate { get; set; }

        /// <summary>
        ///   抵押预约时段人数
        /// </summary>
        public int mortcount { get; set; }

        /// <summary>
        ///一体化预约时段人数
        /// </summary>
        public int appointmentcount { get; set; }

        /// <summary>
        /// 公积金预约时段人数
        /// </summary>
        public int pubfundscount { get; set; }
              /// <summary>
              /// 单独交税时段人数
              /// </summary>
        public int taxpaycount { get; set; }

    }
}
