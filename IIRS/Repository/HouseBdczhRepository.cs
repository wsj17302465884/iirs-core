using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class HouseBdczhRepository : BaseRepository<HouseXgzhVMode> , IHouseBdczhRepository
    {
        private readonly ILogger<HouseBdczhRepository> _logger;
        public HouseBdczhRepository(IDBTransManagement dbTransManagement, ILogger<HouseBdczhRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<List<HouseXgzhVMode>> GetBdczhByBdczmh(string bdczmh,int xh)
        {
            List<HouseXgzhVMode> models = new List<HouseXgzhVMode>();
            HouseXgzhVMode model;
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            int index = 0;
            index = xh * 10;
            var data = await base.Query(a => a.bdczmh.Contains(bdczmh));

            for (int i = 0; i < data.Count; i++)
            {
                model = new HouseXgzhVMode();
                model.xh = index + i + 1;
                model.bdczmh = data[i].bdczmh;
                model.bdczh = data[i].bdczh;
                model.zl = data[i].zl;
                model.dymj = data[i].dymj;
                model.Dyr = data[i].Dyr;
                models.Add(model);
            }

            return models;
        }
    }
}
