using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using System;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IRoleRepository : IBaseRepository<Sys_Role>//类名
    {
        Task<Sys_Role> SaveRole(string roleName, Guid oid);

        Task<string> GetRoleNameByRid(Guid rid);
    }
}
