using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class OBLGRepository : BaseRepository<OBLG>, IOBLGRepository
    {
        public OBLGRepository(IDBTransManagement dbTransManagement, ILogger<OBLGRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
