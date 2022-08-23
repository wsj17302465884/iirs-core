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
    public class ConstructionChangeRepository : BaseRepository<ConstructionChangeVModel>, IConstructionChangeRepository
    {
        private readonly ILogger<ConstructionChangeRepository> _logger;
        public ConstructionChangeRepository(IDBTransManagement dbTransManagement, ILogger<ConstructionChangeRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        /// <summary>
        /// 查询在建工程抵押变更相关信息
        /// </summary>
        /// <param name="bdczmh"></param>
        /// <param name="bdczh"></param>
        /// <param name="dyr"></param>
        /// <returns></returns>
        public async Task<List<ConstructionChangeVModel>> GetConstructionChangeList(string bdczmh, string bdczh, string dyr)
        {
            
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            return await base.Query(a => a.bdczmh.Contains(bdczmh) && a.xgzh.Contains(bdczh) && a.dyr.Contains(dyr));
        }

        
    }
}
