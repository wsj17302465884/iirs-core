using IIRS.IRepository.Base;
using IIRS.IRepository.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
namespace IIRS.Repository.BDC
{
    public class DJ_SFDRepository : BaseRepository<DJ_SFD>, IDJ_SFDRepository
    {
        public DJ_SFDRepository(IDBTransManagement dbTransManagement, ILogger<DJ_SFDRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
