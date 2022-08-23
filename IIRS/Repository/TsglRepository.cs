using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class TsglRepository : BaseRepository<DJ_TSGL>, ITsglRepository
    {
        private readonly ILogger<TsglRepository> _logger;
        public TsglRepository(IDBTransManagement dbTransManagement, ILogger<TsglRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<List<DJ_TSGL>> GetTsglList(string tstybm)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            return await base.Query(a => a.TSTYBM == tstybm);
        }

        public async Task<List<DJ_TSGL>> GetTstybmBySlbh(string slbh)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            return await base.Query(a => a.SLBH == slbh);
        }
    }
}
