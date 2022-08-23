using IIRS.Utilities.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.BANK
{
    [SugarTable("USER_INFO", SysConst.DB_CON_BANK)]
    public class UserInfo
    {
        public UserInfo()
        {

        }
        /// <summary>
        /// 序号
        /// </summary>
        public string USERID { get; set; }
        /// <summary>
        /// 登录名
        /// </summary>
        public string USERNAME { get; set; }
        /// <summary>
        /// 登录密码
        /// </summary>
        public string USERPWD { get; set; }
        /// <summary>
        /// 操作员名称
        /// </summary>
        public string OPERATORNAME { get; set; }
        /// <summary>
        /// 组织机构名称
        /// </summary>
        public string BANKNAME { get; set; }
        /// <summary>
        /// 是否有效(0:有效 1：无效)
        /// </summary>
        public int IS_OK { get; set; }
    }
}
