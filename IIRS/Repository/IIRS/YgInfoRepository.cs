using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class YgInfoRepository : BaseRepository<YG_INFO>, IYgInfoRepository
    {
        private readonly ILogger<YgInfoRepository> _logger;
        public YgInfoRepository(IDBTransManagement dbTransManagement, ILogger<YgInfoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
