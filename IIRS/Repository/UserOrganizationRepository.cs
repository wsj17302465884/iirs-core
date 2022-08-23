using System;
using System.Linq;
using System.Threading.Tasks;
using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;

namespace IIRS.Repository
{
    public class UserOrganizationRepository : BaseRepository<Sys_UserOrganization>, IUserOrganizationRepository
    {
        public UserOrganizationRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }

        public async Task<Guid?> GetOrganizationIdByUid(Guid uid)
        {
            return (await Query(d => d.UserId == uid)).OrderByDescending(d => d.Id).LastOrDefault()?.OrgId;
        }

        public async Task<Sys_UserOrganization> SaveUserOrganization(Guid uid, Guid oid)
        {
            Sys_UserOrganization userOrganization = new Sys_UserOrganization(uid, oid);
            var userList = await Query(a => a.UserId == userOrganization.UserId && a.OrgId == userOrganization.OrgId);
            if (userList.Count > 0)
            {
                return userList.FirstOrDefault();
            }
            else
            {
                await Add(userOrganization);
                return userOrganization;
            }
        }
    }
}
