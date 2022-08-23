using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class SpbInfoRepository : BaseRepository<SPB_INFO>, ISpbInfoRepository
    {
        private readonly ILogger<SpbInfoRepository> _logger;
        public SpbInfoRepository(IDBTransManagement dbTransManagement, ILogger<SpbInfoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
