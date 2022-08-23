using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class DYZXSQRepository : BaseRepository<DYZXSQ>, IDYZXSQRepository
    {
        public DYZXSQRepository(IDBTransManagement dbTransManagement, ILogger<DYZXSQRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
