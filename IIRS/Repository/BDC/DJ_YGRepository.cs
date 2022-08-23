using IIRS.IRepository.Base;
using IIRS.IRepository.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.BDC
{
    public class DJ_YGRepository : BaseRepository<DJ_YG>, IDJ_YGRepository
    {
        public DJ_YGRepository(IDBTransManagement dbTransManagement, ILogger<DJ_YGRepository> logger) : base(dbTransManagement)
        {

        }
    }
}