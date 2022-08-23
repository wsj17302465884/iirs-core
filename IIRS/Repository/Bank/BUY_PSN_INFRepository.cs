using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class BUY_PSN_INFRepository : BaseRepository<BUY_PSN_INF>, IBUY_PSN_INFRepository
    {
        public BUY_PSN_INFRepository(IDBTransManagement dbTransManagement, ILogger<BUY_PSN_INFRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
