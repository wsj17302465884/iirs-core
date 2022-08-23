using IIRS.IRepository.Base;
using IIRS.IRepository.LYSXK209;
using IIRS.Models.EntityModel.LYSXK209;
using IIRS.Repository.Base;

namespace IIRS.Repository.LYSXK209
{
    public class WQ_LOGRepository : BaseRepository<WQ_LOG>, IWQ_LOGRepository
    {
        public WQ_LOGRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }
    }
}