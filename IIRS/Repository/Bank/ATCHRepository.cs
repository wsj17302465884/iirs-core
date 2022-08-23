using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class ATCHRepository : BaseRepository<ATCH>, IATCHRepository
    {
        public ATCHRepository(IDBTransManagement dbTransManagement, ILogger<ATCHRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
