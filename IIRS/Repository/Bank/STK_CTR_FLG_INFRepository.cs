using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class STK_CTR_FLG_INFRepository : BaseRepository<STK_CTR_FLG_INF>, ISTK_CTR_FLG_INFRepository
    {
        public STK_CTR_FLG_INFRepository(IDBTransManagement dbTransManagement, ILogger<STK_CTR_FLG_INFRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
