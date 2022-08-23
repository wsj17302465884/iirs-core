using IIRS.IRepository.Base;
using IIRS.IRepository.LYSXK209;
using IIRS.Models.EntityModel.LYSXK209;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
namespace IIRS.Repository.LYSXK209
{
    public class Yue_PeriodRepository : BaseRepository<YUE_PERIOD>, IYue_PeriodRepository
    {
        public Yue_PeriodRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }
    }
}
