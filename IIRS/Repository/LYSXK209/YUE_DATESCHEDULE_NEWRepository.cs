using IIRS.IRepository.Base;
using IIRS.IRepository.LYSXK209;
using IIRS.Models.EntityModel.LYSXK209;
using IIRS.Repository.Base;

namespace IIRS.Repository.LYSXK209
{
    public class YUE_DATESCHEDULE_NEWRepository : BaseRepository<YUE_DATESCHEDULE_NEW>, IYUE_DATESCHEDULE_NEWRepository
    {
        public YUE_DATESCHEDULE_NEWRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }
    }
}