using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IRoleModulePermissionRepository : IBaseRepository<Sys_RoleModulePermission>//类名
    {
        Task<List<Sys_RoleModulePermission>> WithChildrenModel();
        Task<List<Sys_RoleModulePermission>> RoleModuleMaps();

        Task<List<Sys_RoleModulePermission>> GetRoleModule();
    }
}
