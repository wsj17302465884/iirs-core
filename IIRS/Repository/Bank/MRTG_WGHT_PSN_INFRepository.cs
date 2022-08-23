using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class MRTG_WGHT_PSN_INFRepository : BaseRepository<MRTG_WGHT_PSN_INF>, IMRTG_WGHT_PSN_INFRepository
    {
        public MRTG_WGHT_PSN_INFRepository(IDBTransManagement dbTransManagement, ILogger<MRTG_WGHT_PSN_INFRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
