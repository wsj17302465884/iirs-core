using IIRS.IRepository.Base;
using IIRS.IRepository.LYSXK209;
using IIRS.Models.EntityModel.LYSXK209;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.LYSXK209
{
    public class Yue_PTAmountRepository : BaseRepository<Yue_PTAmount>, IYue_PTAmountRepository
    {
        public Yue_PTAmountRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }
    }
}
