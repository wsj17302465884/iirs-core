using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class ZdQsdcInfoRepository : BaseRepository<ZD_QSDC_INFO>, IZdQsdcInfoRepository
    {
        private readonly ILogger<ZdQsdcInfoRepository> _logger;
        public ZdQsdcInfoRepository(IDBTransManagement dbTransManagement, ILogger<ZdQsdcInfoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
