using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class YZHDYCX_REQUESTRepository : BaseRepository<YZHDYCX_REQUEST>, IYZHDYCX_REQUESTRepository
    {
        public YZHDYCX_REQUESTRepository(IDBTransManagement dbTransManagement, ILogger<YZHDYCX_REQUESTRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
