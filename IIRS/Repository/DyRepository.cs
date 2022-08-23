using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    /// <summary>
    /// ModuleRepository
    /// </summary>	
    public class DyRepository : BaseRepository<DJ_DY>, IDYRepository
    {
        private readonly ILogger<DyRepository> _logger;
        public DyRepository(IDBTransManagement dbTransManagement, ILogger<DyRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<List<DJ_DY>> GetDyInfo(string slbh)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            return await base.Query(a => a.SLBH == slbh);
        }
    }
}
