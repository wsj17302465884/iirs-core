using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using SqlSugar;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    /// <summary>
    /// RoleModulePermissionRepository
    /// </summary>
    public class RoleModulePermissionRepository : BaseRepository<Sys_RoleModulePermission>, IRoleModulePermissionRepository
    {
        readonly IRoleRepository _roleRepository;
        readonly IModuleRepository _moduleRepository;

        public RoleModulePermissionRepository(IDBTransManagement dbTransManagement, IRoleRepository roleRepository, IModuleRepository moduleRepository) : base(dbTransManagement)
        {
            _roleRepository = roleRepository;
            _moduleRepository = moduleRepository;
        }

        public async Task<List<Sys_RoleModulePermission>> WithChildrenModel()
        {
            var list = await Task.Run(() => Db.Queryable<Sys_RoleModulePermission>()
                    .Mapper(it => it.Role, it => it.RoleId)
                    .Mapper(it => it.Permission, it => it.PermissionId)
                    .Mapper(it => it.Module, it => it.ModuleId).ToList());

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Sys_RoleModulePermission>> RoleModuleMaps()
        {
            return await QueryMuch<Sys_RoleModulePermission, Sys_Module, Sys_Role, Sys_RoleModulePermission>(
                (rmp, m, r) => new object[] {
                    JoinType.Left, rmp.ModuleId == m.ID,
                    JoinType.Left,  rmp.RoleId == r.ID
                },

                (rmp, m, r) => new Sys_RoleModulePermission()
                {
                    Role = r,
                    Module = m,
                    IsDeleted = rmp.IsDeleted
                },

                (rmp, m, r) => rmp.IsDeleted == false && m.IsDeleted == false && r.IsDeleted == false
                );
        }

        /// <summary>
        /// 获取全部 角色接口(按钮)关系数据
        /// </summary>
        /// <returns></returns>
        public async Task<List<Sys_RoleModulePermission>> GetRoleModule()
        {
            var roleModulePermissions = await base.Query(a => a.IsDeleted == false);
            var roles = await _roleRepository.Query(a => a.IsDeleted == false);
            var modules = await _moduleRepository.Query(a => a.IsDeleted == false);

            if (roleModulePermissions.Count > 0)
            {
                foreach (var item in roleModulePermissions)
                {
                    item.Role = roles.FirstOrDefault(d => d.ID == item.RoleId);
                    item.Module = modules.FirstOrDefault(d => d.ID == item.ModuleId);
                }
            }
            return roleModulePermissions;
        }
    }
}
