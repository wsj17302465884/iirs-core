using System;
using SqlSugar;
using RT.Comb;
using System.Collections.Generic;

namespace IIRS.Models.EntityModel.IIRS
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [SugarTable("SYS_USERINFO", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class Sys_Userinfo
    {
        /// <summary>
        /// 初始化用户信息类
        /// </summary>
        public Sys_Userinfo() { }

        public Sys_Userinfo(string loginName, string loginPwd)
        {
            Id = Provider.Sql.Create();
            IsDeleted = false;
            LoginName = loginName;
            LoginPWD = loginPwd;
            RealName = loginName;
            Status = 0;
            LastLoginTime = DateTime.Now;
            ErrorCount = 0;
        }

        /// <summary>
        /// 初始化用户信息类
        /// </summary>
        /// <param name="id">用户编号</param>
        /// <param name="loginName">登录名</param>
        /// <param name="loginPWD">密码</param>
        public Sys_Userinfo(Guid id, string loginName, string loginPWD)
        {
            Id = id;
            IsDeleted = false;
            LoginName = loginName;
            LoginPWD = loginPWD;
            RealName = loginName;
            Status = 0;
            LastLoginTime = DateTime.Now;
            ErrorCount = 0;
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        /// <summary>
        /// 登录密码
        /// </summary>
        public string LoginPWD { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 用户状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 联系方式
        /// </summary>
        public string TEL { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime LastLoginTime { get; set; } = DateTime.Now;

        /// <summary>
        /// 登录错误次数
        /// </summary>
        public int ErrorCount { get; set; }

        /// <summary>
        /// 用户角色编号列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Guid> RIDs { get; set; }

        /// <summary>
        /// 用户角色名列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<string> RoleNames { get; set; }

        /// <summary>
        /// 用户组织机构编号列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<Guid> OIDs { get; set; }

        /// <summary>
        /// 用户组织机构名列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<string> OrgNames { get; set; }

        /// <summary>
        /// 用户组织机构名列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public Sys_Organization Organization { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string oldPWD { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string NewPWD { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string SecondPwd { get; set; }
    }
}
