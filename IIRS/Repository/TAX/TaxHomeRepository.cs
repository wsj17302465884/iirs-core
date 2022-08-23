using IIRS.IRepository.Base;
using IIRS.IRepository.TAX;
using IIRS.Models.EntityModel.Tax;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.TAX
{
    public class TaxHomeRepository : BaseRepository<TAX_EXISTING_HOME>, ITaxHomeRepository
    {
        private readonly ILogger<TaxHomeRepository> _logger;
        public TaxHomeRepository(IDBTransManagement dbTransManagement, ILogger<TaxHomeRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
