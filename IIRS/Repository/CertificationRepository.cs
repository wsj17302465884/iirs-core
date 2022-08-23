using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.ViewModel;
using IIRS.Repository.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class CertificationRepository : BaseRepository<CertificationVModel>, ICertificationRepository
    {
        private readonly ILogger<CertificationRepository> _logger;
        public CertificationRepository(IDBTransManagement dbTransManagement, ILogger<CertificationRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<List<CertificationVModel>> GetCertificationInfo(string bdczmh, string zjhm, string dyqr)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            return await base.Query(a => a.bdczmh.Contains(bdczmh) && a.Dyr_Zjhm == zjhm && a.Dyqr == dyqr);
        }

        public async Task<List<CertificationVModel>> GetCertificationList(string bdczmh, string dyqr_qlrmc)
        {
            
            //日志
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            return await base.Query(a => a.bdczmh.Contains(bdczmh) && a.Dyqr.Contains(dyqr_qlrmc));
        }

        public async Task<PageModel<CertificationVModel>> GetCertificationListToPage(int intPageIndex, string bdczmh, string dyr, string dyqr)
        {
            //int intPageIndex = 1;
            string _strOrderByFileds = "qljssj desc";

            //日志
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};

            Expression<Func<CertificationVModel, bool>> _whereExpression = a => a.bdczmh.Contains(bdczmh) && a.Dyr.Contains(dyr) && a.Dyqr.Contains(dyqr);

            return await base.QueryPage(_whereExpression, intPageIndex, SysConst.SYS_DEFAULT_PAGE_SIZE, _strOrderByFileds);
        }

        /// <summary>
        /// 不动产证明信息综合查询
        /// </summary>
        /// <param name="bdczmh">不动产证明号</param>
        /// <param name="bdcdyh">不动产单元号</param>
        /// <param name="dySlbh">抵押受理编号</param>
        /// <param name="dyr">抵押人姓名</param>
        /// <param name="intPageIndex">查询分页页面编码</param>
        /// <param name="pageLength">每个分页页面长度</param>
        /// <returns>分页结果集</returns>
        public async Task<PageModel<CertificationVModel>> GetCertificationListToPage(string bdczmh, string bdcdyh, string dySlbh, string dyr, int intPageIndex, int pageLength)
        {
            //int intPageIndex = 1;
            string _strOrderByFileds = "qljssj desc";

            //日志
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            Expression<Func<CertificationVModel, bool>> _whereExpression = a =>

            (bdczmh != null && a.bdczmh.Contains(bdczmh)
            || (bdcdyh != null && a.bdcdyh.Contains(bdcdyh))
            || (dySlbh != null && a.slbh.Contains(dySlbh))
            || (dyr != null && a.Dyr.Contains(dyr)));

            return await base.QueryPage(_whereExpression, intPageIndex, pageLength, _strOrderByFileds);
        }






    }
}
