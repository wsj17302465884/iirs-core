using System;
using RT.Comb;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    /// <summary>
    /// 用户角色关联表
    /// </summary>
    [SugarTable("SYS_USERROLE", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class Sys_UserRole
    {
        public Sys_UserRole()
        {
        }

        public Sys_UserRole(Guid uid, Guid rid)
        {
            Id = Provider.Sql.Create();
            IsDeleted = false;
            UserId = uid;
            RoleId = rid;
        }

        /// <summary>
        /// ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        ///获取或设置是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }
    }
}