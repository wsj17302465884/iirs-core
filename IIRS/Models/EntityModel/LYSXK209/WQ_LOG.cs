using System;
using SqlSugar;

namespace IIRS.Models.EntityModel.LYSXK209
{
    [SugarTable("WQ_LOG", Utilities.Common.SysConst.DB_CON_LYSXK209)]
    public class WQ_LOG
    {
        public WQ_LOG()
        {

        }

        /// <summary>
        /// 主键编号
        /// </summary>
        public string PK { get; set; }

        /// <summary>
        /// 查询人姓名
        /// </summary>
        public string CXRXM { get; set; }

        /// <summary>
        /// 查询人身份证号
        /// </summary>
        public string CXRZJHM { get; set; }
        /// <summary>
        /// 单位
        /// </summary>
        public string DW { get; set; }
        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime cdate { get; set; }

        /// <summary>
        /// 传输数据
        /// </summary>
        public string POST_DATA { get; set; }

        /// <summary>
        /// 信息
        /// </summary>
        public string MSG { get; set; }

        /// <summary>
        /// 异常信息
        /// </summary>
        public string EX_MSG { get; set; }
    }
}
