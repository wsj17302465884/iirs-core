using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class BNK_HDL_AGNC_PSN_INFRepository : BaseRepository<BNK_HDL_AGNC_PSN_INF>, IBNK_HDL_AGNC_PSN_INFRepository
    {
        public BNK_HDL_AGNC_PSN_INFRepository(IDBTransManagement dbTransManagement, ILogger<BNK_HDL_AGNC_PSN_INFRepository> logger) : base(dbTransManagement)
        {

        }
    }
}