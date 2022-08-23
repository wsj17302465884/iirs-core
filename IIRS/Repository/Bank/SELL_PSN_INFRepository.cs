using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class SELL_PSN_INFRepository : BaseRepository<SELL_PSN_INF>, ISELL_PSN_INFRepository
    { 
        public SELL_PSN_INFRepository(IDBTransManagement dbTransManagement, ILogger<SELL_PSN_INFRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
