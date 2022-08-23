using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class Xgdjgl_infoRepository : BaseRepository<XGDJGL_INFO>, IXgdjgl_infoRepository
    {
        private readonly ILogger<Xgdjgl_infoRepository> _logger;
        public Xgdjgl_infoRepository(IDBTransManagement dbTransManagement, ILogger<Xgdjgl_infoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
