using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class OBJTN_RGS_DTLRepository : BaseRepository<OBJTN_RGS_DTL>, IOBJTN_RGS_DTLRepository
    {
        public OBJTN_RGS_DTLRepository(IDBTransManagement dbTransManagement, ILogger<OBJTN_RGS_DTLRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
