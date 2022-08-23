using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class Qlrgl_infoRepository : BaseRepository<QLRGL_INFO>, IQlrgl_infoRepository
    {
        private readonly ILogger<Qlrgl_infoRepository> _logger;
        public Qlrgl_infoRepository(IDBTransManagement dbTransManagement, ILogger<Qlrgl_infoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
