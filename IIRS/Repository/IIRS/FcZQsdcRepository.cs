using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class FcZQsdcRepository : BaseRepository<FC_Z_QSDC_INFO>, IFcZQsdcRepository
    {
        private readonly ILogger<FcZQsdcRepository> _logger;
        public FcZQsdcRepository(IDBTransManagement dbTransManagement, ILogger<FcZQsdcRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
