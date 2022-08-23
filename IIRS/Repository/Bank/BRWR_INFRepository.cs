using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class BRWR_INFRepository : BaseRepository<BRWR_INF>, IBRWR_INFRepository
    {
        public BRWR_INFRepository(IDBTransManagement dbTransManagement, ILogger<BRWR_INFRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
