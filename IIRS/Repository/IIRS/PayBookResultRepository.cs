using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.IIRS
{
    public class PayBookResultRepository : BaseRepository<PAYBOOKRESULT>,IPayBookResultRepository
    {
        private readonly ILogger<PayBookResultRepository> _logger;
        public PayBookResultRepository(IDBTransManagement dbTransManagement, ILogger<PayBookResultRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
