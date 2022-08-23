using IIRS.IRepository.Base;
using IIRS.IRepository.TAX;
using IIRS.Models.EntityModel.Tax;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.TAX
{
    public class TaxSellerRepository : BaseRepository<TAX_EXISTING_HOME_SELLER>, ITaxSellerRepository
    {
        private readonly ILogger<TaxSellerRepository> _logger;
        public TaxSellerRepository(IDBTransManagement dbTransManagement, ILogger<TaxSellerRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
