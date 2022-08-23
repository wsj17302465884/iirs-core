using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;

namespace IIRS.Repository
{
    public class PermissionRepository : BaseRepository<Sys_Permission>, IPermissionRepository
    {
        public PermissionRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }
    }
}
