using IIRS.Utilities.Common;
using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.IIRS
{
    [SugarTable("SYS_DATA_RECORDER", SysConst.DB_CON_IIRS)]
    public class SysDataRecorderModel
    {
        /// <summary>
        /// 主键编号
        /// </summary>
        public string PK { get; set; }

        /// <summary>
        /// 业务编号主键
        /// </summary>
        public string BUS_PK { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public string USER_ID { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string USER_NAME { get; set; }

        /// <summary>
        /// 保存Json格式数据
        /// </summary>
        [SugarColumn(ColumnDataType = "clob")]
        public string SAVEDATAJSON { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(IsOnlyIgnoreInsert = true)]
        public DateTime CDATE { get; set; }

        /// <summary>
        /// 预留功能列1
        /// </summary>
        public string REMARKS1 { get; set; }

        /// <summary>
        /// 预留功能列2
        /// </summary>
        public string REMARKS2 { get; set; }

        /// <summary>
        /// 预留功能列3
        /// </summary>
        public string REMARKS3 { get; set; }

        /// <summary>
        /// 预留功能列4
        /// </summary>
        public string REMARKS4 { get; set; }

        /// <summary>
        /// 预留功能列5
        /// </summary>
        public string REMARKS5 { get; set; }

        /// <summary>
        /// 是否停用,0:在用，1:停用，2退回
        /// </summary>
        public int IS_STOP { get; set; }
    }
}
