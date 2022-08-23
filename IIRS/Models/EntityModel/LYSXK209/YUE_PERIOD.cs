using System;
using SqlSugar;

namespace IIRS.Models.EntityModel.LYSXK209
{
    [SugarTable("YUE_PERIOD", Utilities.Common.SysConst.DB_CON_LYSXK209)]
    public class YUE_PERIOD
    {
        public YUE_PERIOD()
        {

        }
        /// <summary>
        /// 主键编号
        /// </summary>
        public Guid ID { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        public int ISDELETED { get; set; }

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
