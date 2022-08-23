using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class MRTG_REALEST_UNIT_INFRepository : BaseRepository<MRTG_REALEST_UNIT_INF>, IMRTG_REALEST_UNIT_INFRepository
    {
        public MRTG_REALEST_UNIT_INFRepository(IDBTransManagement dbTransManagement, ILogger<MRTG_REALEST_UNIT_INFRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
