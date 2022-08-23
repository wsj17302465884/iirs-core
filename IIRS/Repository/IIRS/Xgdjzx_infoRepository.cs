using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class Xgdjzx_infoRepository : BaseRepository<XGDJZX_INFO>, IXgdjzx_infoRepository
    {
        private readonly ILogger<Xgdjzx_infoRepository> _logger;
        public Xgdjzx_infoRepository(IDBTransManagement dbTransManagement, ILogger<Xgdjzx_infoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
