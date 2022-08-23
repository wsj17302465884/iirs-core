using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Repository.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class BankAuthorizeRepository : BaseRepository<BankAuthorize>, IBankAuthorizeRepository
    {
        private readonly ILogger<BankAuthorizeRepository> _logger;
        public BankAuthorizeRepository(IDBTransManagement dbTransManagement, ILogger<BankAuthorizeRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<PageModel<BankAuthorize>> GetAuthorizationListToPage(int intPageIndex, string zjhm, string jbr, int flowId)
        {
            string _strOrderByFileds = "authorizationdate desc";

            //日志
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            string first = zjhm.Substring(0, 5);
            string second = zjhm.Substring(10, 8);
            string fifteenZjhm = first + second;

            Expression<Func<BankAuthorize, bool>> _whereExpression = a => a.DOCUMENTNUMBER.Contains(zjhm) || a.DOCUMENTNUMBER.Contains(fifteenZjhm) && a.BankName == jbr && a.STATUS == flowId;

            return await base.QueryPage(_whereExpression, intPageIndex, SysConst.SYS_DEFAULT_PAGE_SIZE_TEN, _strOrderByFileds);
        }

        public async Task<List<BankAuthorize>> GetBankAuthorizeList(string documentnumber, int status)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            return await base.Query(a => a.DOCUMENTNUMBER == documentnumber || a.STATUS == status);
        }

        public async Task<List<BankAuthorize>> GetBankAuthorizeList()
        {
            return await base.Query();
        }
    }
}
