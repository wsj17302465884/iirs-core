using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class Tsgl_infoRepository : BaseRepository<TSGL_INFO>, ITsgl_infoRepository
    {
        private readonly ILogger<Tsgl_infoRepository> _logger;
        public Tsgl_infoRepository(IDBTransManagement dbTransManagement, ILogger<Tsgl_infoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
