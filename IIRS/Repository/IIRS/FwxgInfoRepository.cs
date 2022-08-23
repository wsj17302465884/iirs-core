using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class FwxgInfoRepository : BaseRepository<FWXG_INFO>, IFwxgInfoRepository
    {
        private readonly ILogger<FwxgInfoRepository> _logger;
        public FwxgInfoRepository(IDBTransManagement dbTransManagement, ILogger<FwxgInfoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
