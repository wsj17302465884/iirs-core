using IIRS.IRepository.Base;
using IIRS.IRepository.TAX;
using IIRS.Models.EntityModel.Tax;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.TAX
{
    public class TaxBuyerRepository : BaseRepository<TAX_EXISTING_HOME_BUYER>, ITaxBuyerRepository
    {
        private readonly ILogger<TaxBuyerRepository> _logger;
        public TaxBuyerRepository(IDBTransManagement dbTransManagement, ILogger<TaxBuyerRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
