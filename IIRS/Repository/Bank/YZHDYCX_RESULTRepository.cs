using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class YZHDYCX_RESULTRepository : BaseRepository<YZHDYCX_RESULT>, IYZHDYCX_RESULTRepository
    {
        public YZHDYCX_RESULTRepository(IDBTransManagement dbTransManagement, ILogger<YZHDYCX_RESULTRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
