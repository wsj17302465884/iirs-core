using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class FRCST_RGS_INFRepository : BaseRepository<FRCST_RGS_INF>, IFRCST_RGS_INFRepository
    {
        public FRCST_RGS_INFRepository(IDBTransManagement dbTransManagement, ILogger<FRCST_RGS_INFRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
