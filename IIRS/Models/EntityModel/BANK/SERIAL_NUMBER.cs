using IIRS.Utilities.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.BANK
{
    /// <summary>
    /// 申请流水表
    /// </summary>
    [SugarTable("SERIAL_NUMBER", SysConst.DB_CON_BANK)]
    public class SERIAL_NUMBER
    {
        /// <summary>
        /// 申请流水表
        /// </summary>
        public SERIAL_NUMBER()
        {

        }
        /// <summary>
        /// 序号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid SYSID { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string USERID { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string USERPWD { get; set; }
        /// <summary>
        /// 流水号
        /// </summary>
        public string SERIALNUMBER { get; set; }
        /// <summary>
        /// 操作员
        /// </summary>
        public string OPERATOR { get; set; }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string BANKNAME { get; set; }
        /// <summary>
        /// 有效期
        /// </summary>
        public DateTime VALIDPERIOD { get; set; }
        /// <summary>
        /// 状态（0：申请成功 1：办结成功 2：作废）
        /// </summary>
        public int ZT { get; set; }
    }
}
