using System;
using System.Linq;
using System.Threading.Tasks;
using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;

namespace IIRS.Repository
{
    public class OrganizationRepository : BaseRepository<Sys_Organization>, IOrganizationRepository
    {
        public OrganizationRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }

        public async Task<Sys_Organization> SaveOrganization(string organizationName, Guid pid)
        {
            Sys_Organization organization = new Sys_Organization(organizationName, pid);
            var organizationList = await Query(a => a.Name == organization.Name);
            if (organizationList.Count > 0)
            {
                return organizationList.FirstOrDefault();
            }
            else
            {
                await Add(organization);
                return organization;
            }
        }

        public async Task<string> GetOrganizationNameByOid(Guid oid)
        {
            return ((await QueryById(oid))?.Name);
        }
    }
}
