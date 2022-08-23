using IIRS.IRepository.Base;
using IIRS.IRepository.TAX;
using IIRS.Models.EntityModel.Tax;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.TAX
{
    public class TaxAddedBuyerRepository : BaseRepository<TAX_ADDED_HOME_BUYER_HISTORY>, ITaxAddedBuyerRepository
    {
        private readonly ILogger<TaxAddedBuyerRepository> _logger;
        public TaxAddedBuyerRepository(IDBTransManagement dbTransManagement, ILogger<TaxAddedBuyerRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
