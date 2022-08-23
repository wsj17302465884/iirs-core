using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class REALEST_INFRepository : BaseRepository<REALEST_INF>, IREALEST_INFRepository
    {
        public REALEST_INFRepository(IDBTransManagement dbTransManagement, ILogger<REALEST_INFRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
