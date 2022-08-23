using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class SjdInfoRepository : BaseRepository<SJD_INFO>, ISjdInfoRepository
    {
        private readonly ILogger<SjdInfoRepository> _logger;
        public SjdInfoRepository(IDBTransManagement dbTransManagement, ILogger<SjdInfoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
