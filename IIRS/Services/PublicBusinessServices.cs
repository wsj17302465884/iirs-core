using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IRepository.BDC;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using IIRS.Models.ViewModel.BDC.TraceBack;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class PublicBusinessServices : BaseServices, IPublicBusinessServices
    {
        private readonly ILogger<PublicBusinessServices> _logger;
        private readonly IDBTransManagement _dbTransManagement;
        private readonly IDJ_CFRepository _dJ_CF;
        private readonly IDJ_DJBRepository _dJ_DJB;
        private readonly IDJ_SJDRepository _dJ_SJD;
        private readonly IDJ_QLRGLRepository _qLRGLRepository;
        private readonly IDYRepository _dYRepository;
        public PublicBusinessServices(IDBTransManagement dbTransManagement, ILogger<PublicBusinessServices> logger, IDJ_CFRepository dJ_CF, IDJ_DJBRepository dJ_DJB, IDJ_SJDRepository dJ_SJD, IDJ_QLRGLRepository qLRGLRepository, IDYRepository dYRepository) : base(dbTransManagement)
        {
            _logger = logger;
            _dbTransManagement = dbTransManagement;
            _dJ_CF = dJ_CF;
            _dJ_DJB = dJ_DJB;
            _dJ_SJD = dJ_SJD;
            _qLRGLRepository = qLRGLRepository;
            _dYRepository = dYRepository;
        }

        public async Task<List<BusinessModel>> GeneralMortgageBusiness(string tstybm, string slbh)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            List<BusinessModel> modelList = new List<BusinessModel>();
            BusinessModel model = null;

            var cfResult = await base.Db.Queryable<DJ_CF, DJ_TSGL>((a, b) => new object[] { JoinType.Inner, a.SLBH == b.SLBH }).Where((a, b) => !b.SLBH.Contains(slbh) && b.TSTYBM.Contains(tstybm) && (b.LIFECYCLE == null || b.LIFECYCLE == 0)).Select((a, b) => new
            {
                SLBH = a.SLBH,
                XGXX = a.CFWH,
                ZL = "查封"
            }).ToListAsync();

            var qsResult = await base.Db.Queryable<DJ_DJB, DJ_TSGL>((a, b) => new object[] { JoinType.Inner, a.SLBH == b.SLBH }).Where((a, b) => !b.SLBH.Contains(slbh) && b.TSTYBM.Contains(tstybm) && (b.LIFECYCLE == null || b.LIFECYCLE == 0)).Select((a, b) => new
            {
                SLBH = a.SLBH,
                XGXX = a.BDCZH,
                ZL = "权属"
            }).ToListAsync();

            var dyResult = await base.Db.Queryable<DJ_DY, DJ_TSGL>((a, b) => new object[] { JoinType.Inner, a.SLBH == b.SLBH }).Where((a, b) => !b.SLBH.Contains(slbh) && b.TSTYBM.Contains(tstybm) && (b.LIFECYCLE == null || b.LIFECYCLE == 0)).Select((a, b) => new
            {
                SLBH = a.SLBH,
                XGXX = a.BDCZMH,
                ZL = "抵押"
            }).ToListAsync();

            foreach (var Cfitem in cfResult)
            {
                model = new BusinessModel();
                model.slbh = Cfitem.SLBH;
                model.xgxx = Cfitem.XGXX;
                model.djzl = Cfitem.ZL;
                modelList.Add(model);
            }

            foreach (var Qsitem in qsResult)
            {
                model = new BusinessModel();
                model.slbh = Qsitem.SLBH;
                model.xgxx = Qsitem.XGXX;
                model.djzl = Qsitem.ZL;
                modelList.Add(model);
            }

            foreach (var Dyitem in dyResult)
            {
                model = new BusinessModel();
                model.slbh = Dyitem.SLBH;
                model.xgxx = Dyitem.XGXX;
                model.djzl = Dyitem.ZL;
                modelList.Add(model);
            }
            return modelList;

        }

        public async Task<List<IFLOW_ACTION>> GetFlowActionModels(int groupId)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            try
            {
                List<IFLOW_ACTION> ModelList = new List<IFLOW_ACTION>();
                IFLOW_ACTION model = null;
                var result = await base.Db.Queryable<IFLOW_ACTION, IFLOW_ACTION_GROUP>((A, B) => new object[] { JoinType.Inner, A.GROUP_ID == B.GROUP_ID}).Where((A, B) => (B.GROUP_ID == groupId)).Select((A, B) => new
                {
                    FLOWID = A.FLOW_ID,
                    FLOWNAME = A.FLOW_NAME,
                    GROUPID = A.GROUP_ID
                }).ToListAsync();

                foreach (var item in result)
                {
                    model = new IFLOW_ACTION();
                    model.FLOW_ID = item.FLOWID;
                    model.FLOW_NAME = item.FLOWNAME;
                    model.GROUP_ID = item.GROUPID;
                    ModelList.Add(model);
                }

                return ModelList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<PageModel<BankAuthorize>> GetUpcomingTasksListToPage(int intPageIndex, string zjhm, string jbr, int flowId)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            string first = "";
            string second = "";
            string fifteenZjhm = "";
            if (zjhm != null)
            {
                first = zjhm.Substring(0, 5);
                second = zjhm.Substring(10, 8);
                fifteenZjhm = first + second;
            }

            Expression<Func<BankAuthorize, IFLOW_ACTION, object[]>> _joinExpression = (a, b) => new object[] {JoinType.Inner, a.STATUS == b.FLOW_ID};

            Expression<Func<BankAuthorize, IFLOW_ACTION, BankAuthorize>> _selectExpression = (a, b) => new BankAuthorize() { BID = a.BID, rightname = a.rightname, DOCUMENTTYPE = a.DOCUMENTTYPE, DOCUMENTNUMBER = a.DOCUMENTNUMBER, AUTHORIZATIONDATE = a.AUTHORIZATIONDATE, AUTHORIZATIONDEADLINE = a.AUTHORIZATIONDEADLINE, STATUS = a.STATUS, FlowName = b.FLOW_NAME, BankCode = a.BankCode, BankName = a.BankName };

            Expression<Func<BankAuthorize, IFLOW_ACTION, bool>> _whereExpression =
            (a, b) => (a.DOCUMENTNUMBER.Contains(zjhm) || a.DOCUMENTNUMBER.Contains(fifteenZjhm)) && a.BankName == jbr && a.STATUS == flowId;


            string _strOrderByFileds = "a.AUTHORIZATIONDATE desc";

            return await base.QueryResultList<BankAuthorize, IFLOW_ACTION, BankAuthorize>(_joinExpression, _selectExpression, _whereExpression, intPageIndex, SysConst.SYS_DEFAULT_PAGE_SIZE_TEN, _strOrderByFileds);

        }

        public async Task<List<V_HouseModel>> GetHouseInfoByBdczh(string bdczh)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            List<V_HouseModel> modelList = new List<V_HouseModel>();
            V_HouseModel model = null;
            var result = await base.Db.Queryable<DJ_DJB, QL_FWXG,DJ_SJD,DJ_TSGL>((A, B,C,D) => new object[] { JoinType.Inner, A.SLBH == B.SLBH, JoinType.Inner, B.SLBH == C.SLBH, JoinType.Inner, C.SLBH == D.SLBH }).Where((A, B,C,D) => (A.BDCZH == bdczh && (A.LIFECYCLE == 0 || A.LIFECYCLE == null))).Select((A, B,C,D) => new
            {
                bdczh = A.BDCZH,
                jzmj = B.JZMJ,
                zl = C.ZL,
                tstybm = D.TSTYBM
            }).ToListAsync();

            foreach (var item in result)
            {
                model = new V_HouseModel();
                model.bdczh = item.bdczh;
                model.jzmj = item.jzmj;
                model.zl = item.zl;
                model.tstybm = item.tstybm;
                modelList.Add(model);
            }

            return modelList;
        }

        /// <summary>
        /// 获取追溯信息
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <param name="djzl">登记种类</param>
        /// <returns></returns>
        public async Task<TraceBackVModel> GetTraceBackInfo(string slbh, string djzl)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            TraceBackVModel model = new TraceBackVModel();
            if(djzl == "权属")
            {
                var sjdData = await _dJ_SJD.Query(i => i.SLBH == slbh);                
                var xgzhData = await base.Db.Queryable<DJ_QLRGL, DJ_XGDJGL, DJ_SJD>((A, B, C) => new object[] { JoinType.Inner, A.SLBH == B.FSLBH, JoinType.Left, B.FSLBH == C.SLBH }).Where((A, B, C) => B.ZSLBH == slbh && (A.QLRLX == "权利人" || A.QLRLX == null)).GroupBy((A, B, C) => new { xgzh = B.XGZH, xgzlx = B.XGZLX, zl = C.ZL}).Select((A, B, C) => new 
                {
                    xgzh = B.XGZH,
                    xgzlx = B.XGZLX,
                    qlrmc = SqlFunc.MappingColumn(A.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(A.QLRMC))"),
                    zl = C.ZL
                }).ToListAsync();
                //var qlrData = await _qLRGLRepository.Query(qlr => qlr.SLBH == slbh && qlr.QLRLX == "权利人");
                var dyrData = await _qLRGLRepository.Query(dyr => dyr.SLBH == slbh && dyr.QLRLX == "抵押人");
                var dyqrData = await _qLRGLRepository.Query(dyqr => dyqr.SLBH == slbh && dyqr.QLRLX == "抵押权人");
                if (sjdData.Count > 0)
                {
                    model.DJ_SJDMOdel = sjdData[0];
                }
                if(xgzhData.Count > 0)
                {
                    foreach (var item in xgzhData)
                    {
                        xgzhModel xgzh = new xgzhModel();
                        xgzh.xgzh = item.xgzh;
                        xgzh.xgzlx = item.xgzlx;
                        xgzh.qlrmc = item.qlrmc;
                        xgzh.zl = item.zl;
                        model.xgzhList.Add(xgzh);
                    }
                }
                
                if (dyrData.Count > 0)
                {
                    model.dyrList = dyqrData;
                }
                if (dyqrData.Count > 0)
                {
                    model.dyqrList = dyqrData;
                }


            }
            else if (djzl == "权属注销")
            {

            }
            else if (djzl == "抵押")
            {
                var sjdData = await _dJ_SJD.Query(i => i.SLBH == slbh);
                #region 查询xgzhData信息
                var xgzhData = await base.Db.UnionAll<xgzhModel>(
                    base.Db.Queryable<DJ_QLRGL, DJ_XGDJGL, DJ_SJD>((A, B, C) => new object[] { JoinType.Inner, A.SLBH == B.FSLBH, JoinType.Left, C.SLBH == B.FSLBH }).Where((A, B, C) => B.ZSLBH == slbh && A.QLRLX == "抵押权人" && B.XGZLX == "房屋抵押证明").GroupBy((A, B, C) => new { xgzh = B.XGZH, xgzlx = B.XGZLX, zl = C.ZL }).Select((A, B, C) => new xgzhModel()
                    {
                        xgzh = B.XGZH,
                        xgzlx = B.XGZLX,
                        qlrmc = SqlFunc.MappingColumn(A.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(A.QLRMC))"),
                        zl = C.ZL
                    }),
                    base.Db.Queryable<DJ_QLRGL, DJ_XGDJGL, DJ_SJD>((A, B, C) => new object[] { JoinType.Inner, A.SLBH == B.FSLBH, JoinType.Left, C.SLBH == B.FSLBH }).Where((A, B, C) => B.ZSLBH == slbh && A.QLRLX == "权利人" && B.XGZLX == "房产证").GroupBy((A, B, C) => new { xgzh = B.XGZH, xgzlx = B.XGZLX, zl = C.ZL }).Select((A, B, C) => new xgzhModel()
                    {
                        xgzh = B.XGZH,
                        xgzlx = B.XGZLX,
                        qlrmc = SqlFunc.MappingColumn(A.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(A.QLRMC))"),
                        zl = C.ZL
                    })
                    ).ToListAsync();

                if (xgzhData.Count > 0)
                {
                    foreach (var item in xgzhData)
                    {
                        xgzhModel xgzh = new xgzhModel();
                        xgzh.xgzh = item.xgzh;
                        xgzh.xgzlx = item.xgzlx;
                        xgzh.qlrmc = item.qlrmc;
                        xgzh.zl = item.zl;
                        model.xgzhList.Add(xgzh);
                    }
                }
                #endregion

                #region 查询抵押人、抵押权人及赋值
                var dyrData = await _qLRGLRepository.Query(dyr => dyr.SLBH == slbh && dyr.QLRLX == "抵押人");
                var dyqrData = await _qLRGLRepository.Query(dyqr => dyqr.SLBH == slbh && dyqr.QLRLX == "抵押权人");
                if (dyrData.Count > 0)
                {
                    model.dyrList = dyqrData;
                }
                if (dyqrData.Count > 0)
                {
                    model.dyqrList = dyqrData;
                }
                #endregion

                #region 查询djbData信息
                var djbData = await base.Db.Queryable<DJ_XGDJGL, DJ_DJB>((A, B) => A.FSLBH == B.SLBH).Where((A, B) => A.XGZLX == "房产证" && A.ZSLBH == slbh).Select((A, B) => new djbModel()
                {
                    slbh = B.SLBH,
                    bdczh = B.BDCZH,
                    bdcdyh = B.BDCDYH
                }).ToListAsync();

                if(djbData.Count > 0)
                {
                    foreach (var item in djbData)
                    {
                        djbModel djb = new djbModel();
                        djb.slbh = item.slbh;
                        djb.bdczh = item.bdczh;
                        djb.bdcdyh = item.bdcdyh;
                        model.djbList.Add(djb);
                    }
                }
                #endregion

                #region 查询抵押信息及赋值
                var dyData = await _dYRepository.Query(i => i.SLBH == slbh);
                if(dyData.Count > 0)
                {
                    model.DJ_DYModel = dyData[0];
                }
                #endregion
            }
            else if (djzl == "抵押注销")
            {

            }
            else if(djzl == "查封")
            {
                var sjdData = await _dJ_SJD.Query(i => i.SLBH == slbh);
                model.DJ_SJDMOdel = sjdData[0];
                var cfqdData = await base.Db.Queryable<DJ_TSGL, DJ_CF, DJ_SJD, DJ_QLRGL>((A, B, C, D) => new object[] { JoinType.Inner, A.SLBH == B.SLBH, JoinType.Inner, B.SLBH == C.SLBH, JoinType.Left, C.SLBH == D.SLBH }).Where((A, B, C, D) => B.SLBH == slbh).GroupBy((A, B, C, D) => new { xgzh = B.XGZH, bdcdyh = B.BDCDYH, zl = C.ZL, cflx = B.CFLX, cffw = B.CFFW, fj = B.FJ }).Select((A, B, C, D) => new cfModel() 
                {
                    xgzh = B.XGZH,
                    bdcdyh = B.BDCDYH,
                    zl = C.ZL,
                    qlrmc = SqlFunc.MappingColumn(D.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(D.QLRMC))"),
                    zjhm = SqlFunc.MappingColumn(D.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(D.ZJHM))"),
                    cflx = B.CFLX,
                    cffw = B.CFFW,
                    fj = B.FJ
                }).ToListAsync();

                if(cfqdData.Count > 0)
                {
                    foreach (var item in cfqdData)
                    {
                        cfModel cf = new cfModel();
                        cf.xgzh = item.xgzh;
                        cf.bdcdyh = item.bdcdyh;
                        cf.zl = item.zl;
                        cf.qlrmc = item.qlrmc;
                        cf.zjhm = item.zjhm;
                        cf.cflx = item.cflx;
                        cf.cffw = item.cffw;
                        cf.fj = item.fj;
                        model.cfList.Add(cf);
                    }                    
                }
                var cfData = await _dJ_CF.Query(i => i.SLBH == slbh);
                model.DJ_CFModel = cfData[0];
            }
            else if (djzl == "查封注销")
            {

            }
            else if (djzl == "解封")
            {

            }
            else if (djzl == "预告")
            {

            }
            else if (djzl == "异议")
            {

            }

            return model;
        }
    }
}
