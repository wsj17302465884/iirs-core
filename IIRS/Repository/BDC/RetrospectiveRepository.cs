using IIRS.IRepository.Base;
using IIRS.IRepository.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.BDC
{
    public class RetrospectiveRepository:BaseRepository<RetrospectiveModel>, IRetrospectiveRepository
    {
        public RetrospectiveRepository(IDBTransManagement dbTransManagement, ILogger<RetrospectiveRepository> logger) : base(dbTransManagement)
        {
            
        }
    }
}
