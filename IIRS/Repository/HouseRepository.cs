using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class HouseRepository : BaseRepository<HouseStatusModel>,IHouseRepository
    {
        private readonly ILogger<HouseRepository> _logger;
        public HouseRepository(IDBTransManagement dbTransManagement, ILogger<HouseRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<List<HouseStatusModel>> GetHouseStatusList(string zd_tstybm)
        {
            //日志
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            return await base.Query(a => zd_tstybm.Split(new char[] { ',' }).Contains(a.Zd_tstybm));
            
        }
    }
}
