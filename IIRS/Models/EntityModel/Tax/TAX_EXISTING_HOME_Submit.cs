using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.Tax
{
    [SugarTable("TAX_EXISTING_HOME_Submit", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class TAX_EXISTING_HOME_Submit
    {
        public TAX_EXISTING_HOME_Submit()
        {

        }

        /// <summary>
        /// 流水号 - 主键
        /// </summary>
        public string SUBMIT_NUM { get; set; }

        /// <summary>
        /// 受理编号
        /// </summary>
        public string SLBH { get; set; }

        /// <summary>
        /// 接收的数据包编号
        /// </summary>
        public string SJBBH { get; set; } 

        /// <summary>
        /// 合同编号
        /// </summary>
        public string HTBH { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CDATE { get; set; }

        /// <summary>
        /// 是否成功
        /// </summary>
        public int IS_OK { get; set; }

        /// <summary>
        /// 异常信息1
        /// </summary>
        public string MSG { get; set; }

        /// <summary>
        /// 异常信息2
        /// </summary>
        public string EX_MSG { get; set; }

        /// <summary>
        /// 发送次数
        /// </summary>
        public int SEND_TIMES { get; set; }

        /// <summary>
        /// 发包数据
        /// </summary>
        public string POST_DATA { get; set; }

    }
}
