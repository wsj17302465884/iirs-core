using IIRS.IRepository.Base;
using IIRS.IServices.Bank;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Services.Bank
{
    public class BankQueryServices : BaseServices, IBankQueryServices
    {
        private readonly ILogger<BankQueryServices> _logger;
        private readonly IDBTransManagement _dBTransManagement;
        public BankQueryServices(IDBTransManagement dbTransManagement, ILogger<BankQueryServices> logger) : base(dbTransManagement)
        {
            _logger = logger;
            _dBTransManagement = dbTransManagement;
            
        }
        /// <summary>
        /// 获取流程名称
        /// </summary>
        /// <returns></returns>
        public async Task<List<IFLOW_ACTION_GROUP>> GetActionGroupList()
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            var data = await base.Db.Queryable<IFLOW_ACTION_GROUP>().Where(it => it.IS_DETELE == 0).ToListAsync();
            return data;
        }

        /// <summary>
        /// 获取待办任务
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <param name="jbr">经办人</param>
        /// <param name="lczl">流程种类</param>
        /// <param name="intPageIndex">当前页标</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns></returns>
        public async Task<PageModel<AgencyTaskVModel>> GetBankTaskList(string slbh, string jbr, string lczl, int intPageIndex, int PageSize)
        {
            RefAsync<int> totalCount = 0;
            PageModel<AgencyTaskVModel> pageModel = new PageModel<AgencyTaskVModel>();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            var data = await base.Db.Queryable<BankAuthorize, REGISTRATION_INFO, QLRGL_INFO,IFLOW_ACTION_GROUP, IFLOW_ACTION>((A, B, C,D,E) => new object[] { JoinType.Inner, A.BID == B.AUZ_ID, JoinType.Inner, B.YWSLBH == C.SLBH, JoinType.Inner, B.DJZL == D.GROUP_ID, JoinType.Inner, A.STATUS == E.FLOW_ID })
                .WhereIF(!string.IsNullOrEmpty(slbh), (A, B, C, D, E) => B.YWSLBH.Contains(slbh))
                .WhereIF(!string.IsNullOrEmpty(lczl), (A, B, C, D, E) => B.DJZL.ToString().Contains(lczl)).Where((A, B, C, D, E) => B.USER_ID == jbr && B.NEXT_XID == null)
                .GroupBy((A, B, C, D, E) => new
                {
                    slbh = B.YWSLBH,
                    xid = B.XID,
                    lczl = D.GNAME,
                    statusId = A.STATUS,
                    status = E.FLOW_NAME,
                    dateTime = A.AUTHORIZATIONDATE,
                    next_xid = B.NEXT_XID,
                    is_action_ok = B.IS_ACTION_OK,
                    vue_url = E.BANK_VUE_URL,
                    vue_name = E.VUE_NAME
                }).OrderBy((A, B, C, D, E) => A.AUTHORIZATIONDATE,OrderByType.Desc).Select((A, B, C, D, E) => new AgencyTaskVModel
                {
                    slbh = B.YWSLBH,
                    xid = B.XID,
                    qlrmc = SqlFunc.MappingColumn(C.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(C.QLRMC))"),
                    lczl = D.GNAME,
                    statusId = A.STATUS,
                    status = E.FLOW_NAME,
                    dateTime = A.AUTHORIZATIONDATE,
                    next_xid = B.NEXT_XID,
                    is_action_ok = B.IS_ACTION_OK,
                    vue_url = E.BANK_VUE_URL,
                    vue_name = E.VUE_NAME
                }).ToPageListAsync(intPageIndex, PageSize, totalCount);

            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / PageSize.ObjToDecimal()).ObjToInt();
            pageModel.data = data;
            pageModel.page = intPageIndex;
            pageModel.PageSize = PageSize;
            pageModel.dataCount = totalCount;
            pageModel.pageCount = pageCount;

            return pageModel;
        }

        /// <summary>
        /// 办件查询
        /// </summary>
        /// <param name="lczl">流程种类</param>
        /// <param name="Start">起始日期</param>
        /// <param name="termination">终止日期</param>
        /// <param name="intPageIndex">当前页标</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns></returns>
        public async Task<PageModel<SJD_INFO>> GetBusinessinlist(string lczl, DateTime Start, DateTime termination, int intPageIndex, int PageSize)
        {
            RefAsync<int> totalCount = 0;
            PageModel<SJD_INFO> pageModel = new PageModel<SJD_INFO>();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var data = await base.Db.Queryable<SJD_INFO>()
                .WhereIF(!string.IsNullOrEmpty(lczl), (A) => A.LCMC.Contains(lczl))
                .Where((A) => SqlFunc.Between(A.SJSJ, Start, termination))
                .GroupBy((A) => new
                {
                    LCMC = A.LCMC,
                }).Select((A) => new SJD_INFO
                {
                    LCMC = A.LCMC,
                    count = SqlFunc.AggregateCount(A.LCMC)
                }).ToPageListAsync(intPageIndex, PageSize, totalCount);

            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / PageSize.ObjToDecimal()).ObjToInt();
            pageModel.page = intPageIndex;
            pageModel.PageSize = PageSize;
            pageModel.dataCount = totalCount;
            pageModel.pageCount = pageCount;
            pageModel.data = data;

            return pageModel;
        }
    }
}
