using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class CTR_AND_CLM_STTNRepository : BaseRepository<CTR_AND_CLM_STTN>, ICTR_AND_CLM_STTNRepository
    {
        public CTR_AND_CLM_STTNRepository(IDBTransManagement dbTransManagement, ILogger<CTR_AND_CLM_STTNRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
