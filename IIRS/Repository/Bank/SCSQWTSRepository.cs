using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class SCSQWTSRepository : BaseRepository<SCSQWTS>, ISCSQWTSRepository
    {
        public SCSQWTSRepository(IDBTransManagement dbTransManagement, ILogger<SCSQWTSRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
