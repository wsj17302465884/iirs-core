using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class Bus_visit_logRepository : BaseRepository<BUS_VISIT_LOG>, IBus_visit_logRepository
    {
        private readonly ILogger<Bus_visit_logRepository> _logger;
        public Bus_visit_logRepository(IDBTransManagement dbTransManagement, ILogger<Bus_visit_logRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
