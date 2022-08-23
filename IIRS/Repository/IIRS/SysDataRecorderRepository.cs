using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class SysDataRecorderRepository : BaseRepository<SysDataRecorderModel>, ISysDataRecorderRepository
    {
        private readonly ILogger<SysDataRecorderRepository> _logger;
        public SysDataRecorderRepository(IDBTransManagement dbTransManagement, ILogger<SysDataRecorderRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
