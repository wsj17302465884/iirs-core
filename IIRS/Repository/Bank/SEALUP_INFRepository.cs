using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class SEALUP_INFRepository : BaseRepository<SEALUP_INF>, ISEALUP_INFRepository
    {
        public SEALUP_INFRepository(IDBTransManagement dbTransManagement, ILogger<SEALUP_INFRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
