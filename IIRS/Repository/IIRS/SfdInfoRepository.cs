using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class SfdInfoRepository : BaseRepository<SFD_INFO>, ISfdInfoRepository
    {
        private readonly ILogger<SfdInfoRepository> _logger;
        public SfdInfoRepository(IDBTransManagement dbTransManagement, ILogger<SfdInfoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
