using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class OrderHouseAssociationRepository : BaseRepository<OrderHouseAssociation>, IOrderHouseAssociationRepository
    {
        private readonly ILogger<OrderHouseAssociationRepository> _logger;
        public OrderHouseAssociationRepository(IDBTransManagement dbTransManagement, ILogger<OrderHouseAssociationRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<int> DeleteOrderHouseAssociation(string bid)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            return await base.Db.Deleteable<OrderHouseAssociation>().Where(it => it.BID == bid).ExecuteCommandAsync();
        }

        public async Task<List<OrderHouseAssociation>> GetBIdByBdczh(string bdczh)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            return await base.Query(a => a.CERTIFICATENUMBER == bdczh);
        }

        public async Task<List<OrderHouseAssociation>> GetDoSubmit(string tstybm)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            return await base.Query(a => tstybm.Split(new char[] { ',' }).Contains(a.NUMBERID));
        }

        public async Task<List<OrderHouseAssociation>> GetOrderHouseAssociationList(string bid)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            return await base.Query(a => a.BID == bid);
        }
    }
}
