using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class LandRepository : BaseRepository<LandStatusModel> ,ILandRepository
    {
        private readonly ILogger<LandRepository> _logger;
        public LandRepository(IDBTransManagement dbTransManagement, ILogger<LandRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<List<LandStatusModel>> GetLandStatusList(string h_tstybm)
        {
            //日志
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};

            return await base.Query(a => h_tstybm == a.H_tstybm);
        }
    }
}
