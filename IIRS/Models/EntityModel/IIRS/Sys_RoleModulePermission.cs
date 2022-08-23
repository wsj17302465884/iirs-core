using System;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    /// <summary>
    /// 
    /// </summary>
    [SugarTable("SYS_ROLEMODULEPERMISSION", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class Sys_RoleModulePermission
    {
        public Sys_RoleModulePermission() { }

        public Sys_RoleModulePermission(Guid id, Guid roleid, Guid moduleid, Guid permissionid)
        {
            ID = id;
            IsDeleted = false;
            RoleId = roleid;
            ModuleId = moduleid;
            PermissionId = permissionid;
        }

        [SugarColumn(IsPrimaryKey = true)]
        public Guid ID { get; set; }

        /// <summary>
        /// 是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// 角色ID
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// 菜单ID
        /// </summary>
        public Guid? ModuleId { get; set; }

        /// <summary>
        /// API ID
        /// </summary>
        public Guid? PermissionId { get; set; }

        // 下边三个实体参数，只是做传参作用，所以忽略下
        [SugarColumn(IsIgnore = true)]
        public Sys_Role Role { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Sys_Module Module { get; set; }
        [SugarColumn(IsIgnore = true)]
        public Sys_Permission Permission { get; set; }
    }
}
