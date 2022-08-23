using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    /// <summary>
    /// RoleRepository
    /// </summary>	
    public class RoleRepository : BaseRepository<Sys_Role>, IRoleRepository
    {
        public RoleRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }

        public async Task<string> GetRoleNameByRid(Guid rid)
        {
            return ((await QueryById(rid))?.Name);
        }

        public async Task<Sys_Role> SaveRole(string roleName, Guid oid)
        {
            Sys_Role role = new Sys_Role(roleName, oid);
            var userList = await Query(a => a.Name == role.Name && a.Enabled);
            if (userList.Count > 0)
            {
                return userList.FirstOrDefault();
            }
            else
            {
                await Add(role);
                return role;
            }
        }
    }
}
