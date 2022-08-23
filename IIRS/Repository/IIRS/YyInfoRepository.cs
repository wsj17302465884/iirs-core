using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class YyInfoRepository : BaseRepository<YY_INFO>, IYyInfoRepository
    {
        private readonly ILogger<YyInfoRepository> _logger;
        public YyInfoRepository(IDBTransManagement dbTransManagement, ILogger<YyInfoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
