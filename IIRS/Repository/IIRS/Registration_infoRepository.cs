using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class Registration_infoRepository : BaseRepository<REGISTRATION_INFO>, IRegistration_infoRepository
    {
        private readonly ILogger<Registration_infoRepository> _logger;
        public Registration_infoRepository(IDBTransManagement dbTransManagement, ILogger<Registration_infoRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
