using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class SfdFbInfoRepository : BaseRepository<SFD_FB_INFO>, ISfdFbInfoRepository
    {
        private readonly ILogger<SfdFbInfoRepository> _logger;
        public SfdFbInfoRepository(IDBTransManagement dbTransManagement, ILogger<SfdFbInfoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
