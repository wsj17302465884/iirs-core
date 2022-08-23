using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class DjbInfoRepository : BaseRepository<DJB_INFO>, IDjbInfoRepository
    {
        private readonly ILogger<DjbInfoRepository> _logger;
        public DjbInfoRepository(IDBTransManagement dbTransManagement, ILogger<DjbInfoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
