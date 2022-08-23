using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class ConstructionRepository : BaseRepository<ConstructionVModel>, IConstructionRepository
    {
        private readonly ILogger<ConstructionRepository> _logger;
        public ConstructionRepository(IDBTransManagement dbTransManagement, ILogger<ConstructionRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        /// <summary>
        /// 查询在建工程抵押土地信息
        /// </summary>
        /// <param name="zdtybm"></param>
        /// <returns></returns>
        public async Task<List<ConstructionVModel>> GetConstructionList(string zdtybm)
        {
            List<ConstructionVModel> data = new List<ConstructionVModel>();
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            if (zdtybm != null)
            {
                data = await base.Query(a => a.tstybm == zdtybm);
            }

            return data;
        }
    }
}
