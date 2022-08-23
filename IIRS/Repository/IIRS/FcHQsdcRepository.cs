using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class FcHQsdcRepository : BaseRepository<FC_H_QSDC_INFO>, IFcHQsdcRepository
    {
        private readonly ILogger<FcHQsdcRepository> _logger;
        public FcHQsdcRepository(IDBTransManagement dbTransManagement, ILogger<FcHQsdcRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
