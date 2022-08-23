using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class TdxgInfoRepository : BaseRepository<TDXG_INFO>, ITdxgInfoRepository
    {
        private readonly ILogger<TdxgInfoRepository> _logger;
        public TdxgInfoRepository(IDBTransManagement dbTransManagement, ILogger<TdxgInfoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
