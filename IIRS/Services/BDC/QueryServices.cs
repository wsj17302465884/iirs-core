using IIRS.IRepository.Base;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services.BDC
{
    public class BankQueryServices : BaseServices, IQueryServices
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
            var data = await base.Db.Queryable<IFLOW_ACTION_GROUP>().In(it => it.GROUP_ID, new int[] { 21, 22, 23, 24, 25, 26 }).ToListAsync();
            return data;
        }

        /// <summary>
        /// 获取待办任务
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <param name="jbr">经办人</param>
        /// <param name="lczl">流程种类</param>
        /// <param name="IsAction">流程状态（暂存或完成或退回）</param>
        /// <param name="intPageIndex">当前页标</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns></returns>
        public async Task<PageModel<AgencyTaskVModel>> GetAgencyTaskList(string slbh, string jbr, string lczl, int IsAction, int intPageIndex, int PageSize)
        {
            RefAsync<int> totalCount = 0;
            int djzl = 0;
            if(!string.IsNullOrEmpty(lczl))
            {
                djzl = Convert.ToInt32(lczl);
            }
            PageModel<AgencyTaskVModel> pageModel = new PageModel<AgencyTaskVModel>();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            var data = await base.Db.Queryable<BankAuthorize, REGISTRATION_INFO, QLRGL_INFO,IFLOW_ACTION_GROUP, IFLOW_ACTION>((A, B, C,D,E) => A.BID == B.AUZ_ID && B.XID == C.XID && E.GROUP_ID == D.GROUP_ID && A.STATUS == E.FLOW_ID)
                .WhereIF(!string.IsNullOrEmpty(slbh), (A, B, C, D, E) => B.YWSLBH.Contains(slbh))
                .WhereIF(!string.IsNullOrEmpty(lczl), (A, B, C, D, E) => D.GROUP_ID == djzl).Where((A, B, C, D, E) => B.USER_ID == jbr && B.NEXT_XID == null && B.IS_ACTION_OK == IsAction).GroupBy((A, B, C, D, E) => new
                {
                    slbh = B.YWSLBH,
                    xid = B.XID,
                    lczl = D.GNAME,
                    statusId = A.STATUS,
                    status = E.FLOW_NAME,
                    dateTime = A.AUTHORIZATIONDATE,
                    next_xid = B.NEXT_XID,
                    is_action_ok = B.IS_ACTION_OK,
                    vue_url = E.VUE_URL,
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
                    vue_url = E.VUE_URL,
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
    }
}
