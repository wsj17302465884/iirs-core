using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class BdcxgxxRepository : BaseRepository<BdcxgxxModel> ,IBdcxgxxRepository
    {
        private readonly ILogger<BdcxgxxRepository> _logger;
        public BdcxgxxRepository(IDBTransManagement dbTransManagement, ILogger<BdcxgxxRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<int> AddBdcxgxxModel(BdcxgxxModel model)
        {
            return await base.Add(model);
        }
    }
}
