using IIRS.IRepository.Base;
using IIRS.IRepository.TAX;
using IIRS.Models.EntityModel.Tax;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.TAX
{
    public class TaxAddedHomeRepository : BaseRepository<TAX_ADDED_HOME_HISTORY>, ITaxAddedHomeRepository
    {
        private readonly ILogger<TaxAddedHomeRepository> _logger;
        public TaxAddedHomeRepository(IDBTransManagement dbTransManagement, ILogger<TaxAddedHomeRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
