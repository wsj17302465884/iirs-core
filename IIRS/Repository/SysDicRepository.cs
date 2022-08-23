using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;

namespace IIRS.Repository
{
    public class SysDicRepository : BaseRepository<SYS_DIC>, ISysDicRepository
    {
        public SysDicRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {

        }
    }
}
