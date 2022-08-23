using System;
using System.Threading.Tasks;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;

namespace IIRS.IRepository
{
    public interface IUserOrganizationRepository : IBaseRepository<Sys_UserOrganization>//类名
    {
        Task<Sys_UserOrganization> SaveUserOrganization(Guid uid, Guid oid);
        Task<Guid?> GetOrganizationIdByUid(Guid uid);
    }
}
