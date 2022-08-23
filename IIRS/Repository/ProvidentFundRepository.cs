using IIRS.IRepository.Base;
using IIRS.IRepository;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using IIRS.Models.EntityModel.BDC;
using System;
using SqlSugar;

namespace IIRS.Repository.IIRS
{
    public class ProvidentFundRepository : BaseRepository<ProvidentFundModel>, IProvidentFundRepository
    {
        private readonly ILogger<ProvidentFundRepository> _logger;
        public ProvidentFundRepository(IDBTransManagement dbTransManagement, ILogger<ProvidentFundRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<List<ProvidentFundModel>> GetProvidentFundList(string slbh)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            return await QueryMuch<DJ_DJB, DJ_QLRGL, DJ_QLR, ProvidentFundModel>(
                (djb, qlrgl, qlr) => new object[] {
                    JoinType.Left, djb.SLBH == qlrgl.SLBH,
                    JoinType.Left,  qlrgl.QLRID == qlr.QLRID
                },

                (djb, qlrgl, qlr) => new ProvidentFundModel()
                {
                    pid = Guid.NewGuid(),
                    qlrmc = qlr.QLRMC,
                    zjhm = qlr.ZJHM,
                    queryDate = DateTime.Now,
                },

                (djb, qlrgl, qlr) => (djb.LIFECYCLE == 0 || djb.LIFECYCLE == null) && qlrgl.QLRLX == "权利人" && djb.SLBH == slbh
                );
        }
    }
}
