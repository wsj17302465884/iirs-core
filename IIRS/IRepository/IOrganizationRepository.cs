using System;
using System.Threading.Tasks;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel;
using IIRS.Models.EntityModel.IIRS;

namespace IIRS.IRepository
{
    public interface IOrganizationRepository : IBaseRepository<Sys_Organization>//类名
    {
        Task<Sys_Organization> SaveOrganization(string organizationName, Guid pid);

        Task<string> GetOrganizationNameByOid(Guid oid);
    }
}
