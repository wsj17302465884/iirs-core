using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class CMRCLHS_CTR_FLG_BUY_PSN_INFRepository : BaseRepository<CMRCLHS_CTR_FLG_BUY_PSN_INF>, ICMRCLHS_CTR_FLG_BUY_PSN_INFRepository
    {
        public CMRCLHS_CTR_FLG_BUY_PSN_INFRepository(IDBTransManagement dbTransManagement, ILogger<CMRCLHS_CTR_FLG_BUY_PSN_INFRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
