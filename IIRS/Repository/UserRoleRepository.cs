using System;
using System.Linq;
using System.Threading.Tasks;
using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;

namespace IIRS.Repository
{
    /// <summary>
    /// UserRoleRepository
    /// </summary>	
    public class UserRoleRepository : BaseRepository<Sys_UserRole>, IUserRoleRepository
    {
        public UserRoleRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }

        public async Task<Guid?> GetRoleIdByUid(Guid uid)
        {
            return (await Query(d => d.UserId == uid)).OrderByDescending(d => d.Id).LastOrDefault()?.RoleId;
        }

        public async Task<Sys_UserRole> SaveUserRole(Guid uid, Guid rid)
        {
            Sys_UserRole userRole = new Sys_UserRole(uid, rid);
            var userList = await Query(a => a.UserId == userRole.UserId && a.RoleId == userRole.RoleId);
            if (userList.Count > 0)
            {
                return userList.FirstOrDefault();
            }
            else
            {
                await Add(userRole);
                return userRole;
            }
        }
    }
}
