using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class BdczhRepository : BaseRepository<BdczhVModel>, IBdczhRepository
    {
        private readonly ILogger<BdczhRepository> _logger;
        public BdczhRepository(IDBTransManagement dbTransManagement, ILogger<BdczhRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<List<BdczhVModel>> GetBdczhByBdczmh(string bdczmh,int xh)
        {
            //日志
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            BdczhVModel model;
            List<BdczhVModel> models = new List<BdczhVModel>();
            var data = await base.Query(a => a.bdczmh == bdczmh);

            for (int i = 0; i < data.Count; i++)
            {
                model = new BdczhVModel();
                model.xh = xh * 100 + i + 1;
                model.bdczmh = data[i].bdczmh;
                model.xgzh = data[i].xgzh;
                model.dymj = data[i].dymj;
                model.Dyr = data[i].Dyr;
                model.zl = data[i].zl;
                models.Add(model);
            }

            return models;
        }
    }
}
