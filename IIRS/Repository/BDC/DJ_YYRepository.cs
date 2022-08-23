using IIRS.IRepository.Base;
using IIRS.IRepository.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.BDC
{
    public class DJ_YYRepository : BaseRepository<DJ_YY>, IDJ_YYRepository
    {
        public DJ_YYRepository(IDBTransManagement dbTransManagement, ILogger<DJ_YYRepository> logger) : base(dbTransManagement)
        {

        }
    }
}