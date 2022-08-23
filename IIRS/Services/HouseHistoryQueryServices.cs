using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IRepository.BDC;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC.TraceBack;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class HouseHistoryQueryServices : BaseServices, IHouseHistoryQueryServices
    {
        private readonly ILogger<ProvidentFundServices> _logger;
        private readonly IDBTransManagement _dbTransManagement;
        private readonly IDJ_SJDRepository _dJ_SJD;
        private readonly IDJ_QLRGLRepository _qLRGLRepository;
        private readonly IDJ_CFRepository _dJ_CF;
        private readonly IDJ_YYRepository _dJ_YY;
        private readonly IDJ_YGRepository _dJ_YG;
        private readonly IDYRepository _dYRepository;
        private readonly IDJ_SPBRepository _SPBRepository;
        public HouseHistoryQueryServices(IDBTransManagement dbTransManagement, ILogger<ProvidentFundServices> logger, IDJ_SJDRepository dJ_SJD, IDJ_QLRGLRepository qLRGLRepository, IDJ_CFRepository dJ_CF, IDJ_YGRepository dJ_YG, IDJ_YYRepository dJ_YY, IDYRepository dYRepository,IDJ_SPBRepository SPBRepository) : base(dbTransManagement)
        {
            this._logger = logger;
            this._dbTransManagement = dbTransManagement;
            this._dJ_SJD = dJ_SJD;
            this._qLRGLRepository = qLRGLRepository;
            this._dJ_CF = dJ_CF;
            this._dJ_YY = dJ_YY;
            this._dJ_YG = dJ_YG;
            this._dYRepository = dYRepository;
            this._SPBRepository = SPBRepository;
        }

        /// <summary>
        /// 追述附件查询
        /// </summary>
        /// <param name="CXLX">查询类型</param>
        /// <param name="SJLSZT">数据历史状态，1:历史，0：现实，其他：全部</param>
        /// <param name="SLBH">受理编号</param>
        /// <param name="BDCZH">不动产证号（证明号）</param>
        /// <param name="BDCDYH">不动产单元号</param>
        /// <param name="FZRQ">发证日期</param>
        /// <param name="QLRMC">权利人名称</param>
        /// <param name="ZJHM">证件编号</param>
        /// <param name="ZL">坐落</param>
        /// <param name="FJ">附记</param>
        /// <param name="BDCDJZT">不动产登记状态</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns></returns>
        public async Task<PageStringModel> HouseHistoryQuery(string CXLX,string SJLSZT, string SLBH,string BDCZH
            ,string BDCDYH,DateTime? FZRQ,string QLRMC,string ZJHM,string ZL,string FJ
            ,string BDCDJZT, int pageIndex,int pageSize)
        {
            if (string.IsNullOrEmpty(CXLX))
            {
                return null;
            }
            base.ChangeDB(SysConst.DB_CON_BDC);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            string ZJHM_15 = string.Empty;
            if (ZJHM.Length == 18)//证件号码如果为18位，视为身份证，并转换15位老身份证进行业务查询
            {
                ZJHM_15 = ZJHM.Substring(0, 6) + ZJHM.Substring(8, 9);
            }
            RefAsync<int> totalCount = 0;
            string pageDataJson = string.Empty;
            if (CXLX == "权证")
            {
                base.Db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    _logger.LogDebug(sql);
                };
                pageDataJson = await base.Db.Queryable<DJ_TSGL, DJ_DJB, DJ_QLRGL, DJ_QLR, DJ_SJD>((TS, D, GL, Q, SD) => D.SLBH == TS.SLBH && GL.SLBH == D.SLBH && GL.QLRID == Q.QLRID && D.SLBH == SD.SLBH)
                    .WhereIF(SJLSZT == "0", (TS, D, GL, Q, SD) => (TS.LIFECYCLE == null || TS.LIFECYCLE == 0))
                    .WhereIF(SJLSZT == "1", (TS, D, GL, Q, SD) => !((TS.LIFECYCLE == null || TS.LIFECYCLE == 0)))
                    .WhereIF(!string.IsNullOrEmpty(SLBH), (TS, D, GL, Q, SD) => TS.SLBH.Contains(SLBH))
                    .WhereIF(!string.IsNullOrEmpty(BDCZH), (TS, D, GL, Q, SD) => D.BDCZH.Contains(BDCZH))
                    .WhereIF(!string.IsNullOrEmpty(BDCDYH), (TS, D, GL, Q, SD) => TS.BDCDYH.Contains(BDCDYH))
                    .WhereIF(FZRQ != null, (TS, D, GL, Q, SD) => SqlFunc.DateIsSame(D.FZRQ, FZRQ))
                    .WhereIF(!string.IsNullOrEmpty(QLRMC), (TS, D, GL, Q, SD) => (GL.QLRLX == "权利人" || GL.QLRLX == "") && Q.QLRMC.Contains(QLRMC))
                    .WhereIF(!string.IsNullOrEmpty(ZJHM), (TS, D, GL, Q, SD) => Q.ZJHM.Contains(ZJHM) || (SqlFunc.Length(ZJHM_15) == 15 && Q.ZJHM.Contains(ZJHM_15)))
                    .WhereIF(!string.IsNullOrEmpty(ZL), (TS, D, GL, Q, SD) => SD.ZL.Contains(ZL))
                    .WhereIF(!string.IsNullOrEmpty(FJ), (TS, D, GL, Q, SD) => D.FJ.Contains(FJ))
                    .WhereIF(!string.IsNullOrEmpty(BDCDJZT), (TS, D, GL, Q, SD) => TS.DJZL == BDCDJZT) //????????????????
                    .GroupBy((TS, D, GL, Q, SD) => new { TS.SLBH, TS.BDCLX, TS.TSTYBM, SD.DJDL, D.BDCZH, D.BDCDYH, D.DJRQ, D.LIFECYCLE, SD.ZL, D.ZSXLH })
                    .Select((TS, D, GL, Q, SD) => new
                    {
                        TSTYBM = TS.TSTYBM,
                        SLBH = TS.SLBH,
                        DJDL = SD.DJDL,
                        BDCZH = D.BDCZH,
                        BDCDYH = D.BDCDYH,
                        DJRQ = D.DJRQ,
                        QLRMC = SqlFunc.MappingColumn(Q.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'权利人',Q.QLRMC,'')))"),
                        ZJHM = SqlFunc.MappingColumn(Q.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'权利人',Q.ZJHM,'')))"),
                        LIFECYCLE = D.LIFECYCLE,
                        ZL = SD.ZL,
                        ZSXLH = D.ZSXLH,
                        BDCLX = TS.BDCLX,
                        FWMJ = SqlFunc.Subqueryable<QL_FWXG>().Where(F => F.SLBH == TS.SLBH).Select(s =>SqlFunc.ToString(SqlFunc.AggregateMax(s.JZMJ))),
                        TDMJ = SqlFunc.Subqueryable<QL_TDXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.IsNull(SqlFunc.ToString(SqlFunc.AggregateMax(s.DYTDMJ)),SqlFunc.ToString( SqlFunc.AggregateMax(s.GYTDMJ))))
                    }).ToJsonPageAsync(pageIndex, pageSize, totalCount);
            }
            else if (CXLX == "证明")
            {
                pageDataJson = await base.Db.Queryable<DJ_DY, DJ_QLRGL, DJ_QLR, DJ_SJD>((D, GL, Q, SD) => GL.SLBH == D.SLBH && GL.QLRID == Q.QLRID && D.SLBH == SD.SLBH)
                    .WhereIF(SJLSZT == "0", (D, GL, Q, SD) => (D.LIFECYCLE == null || D.LIFECYCLE == 0))
                    .WhereIF(SJLSZT == "1", (D, GL, Q, SD) => !((D.LIFECYCLE == null || D.LIFECYCLE == 0)))
                    .WhereIF(!string.IsNullOrEmpty(SLBH), (D, GL, Q, SD) => D.SLBH.Contains(SLBH))
                    .WhereIF(!string.IsNullOrEmpty(BDCZH), (D, GL, Q, SD) => D.BDCZMH.Contains(BDCZH))
                    .WhereIF(!string.IsNullOrEmpty(BDCDYH), (D, GL, Q, SD) => D.BDCDYH.Contains(BDCDYH))
                    .WhereIF(FZRQ != null, (D, GL, Q, SD) => SqlFunc.DateIsSame(D.FZRQ, FZRQ))
                    .WhereIF(!string.IsNullOrEmpty(QLRMC), (D, GL, Q, SD) => (GL.QLRLX == "抵押人" || GL.QLRLX == "") && Q.QLRMC.Contains(QLRMC))
                    .WhereIF(!string.IsNullOrEmpty(ZJHM), (D, GL, Q, SD) => Q.ZJHM.Contains(ZJHM) || (SqlFunc.Length(ZJHM_15) == 15 && Q.ZJHM.Contains(ZJHM_15)))
                    .WhereIF(!string.IsNullOrEmpty(ZL), (D, GL, Q, SD) => SD.ZL.Contains(ZL))
                    .WhereIF(!string.IsNullOrEmpty(FJ), (D, GL, Q, SD) => D.FJ.Contains(FJ))
                    //.WhereIF(!string.IsNullOrEmpty(BDCDJZT), (D, GL, Q, SD) => TS.DJZL == BDCDJZT) //????????????????
                    .GroupBy((D, GL, Q, SD) => new { D.LIFECYCLE, D.SLBH, SD.DJDL, D.BDCZMH, D.BDCDYH, D.DJRQ, SD.ZL, D.ZSXLH, D.DYMJ })
                    .Select((D, GL, Q, SD) => new
                    {
                        TSTYBM = "",
                        SLBH = D.SLBH,
                        DJDL = SD.DJDL,
                        BDCZH = D.BDCZMH,
                        BDCDYH = D.BDCDYH,
                        DJRQ = D.DJRQ,
                        QLRMC = SqlFunc.MappingColumn(Q.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'抵押人', Q.QLRMC,'')))"),
                        ZJHM = SqlFunc.MappingColumn(Q.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'抵押人',''),Q.ZJHM))"),
                        LIFECYCLE = D.LIFECYCLE,
                        ZL = SD.ZL,
                        ZSXLH = D.ZSXLH,
                        BDCLX = "",
                        FWMJ = D.DYMJ,
                        TDMJ = D.DYMJ
                    }).ToJsonPageAsync(pageIndex, pageSize, totalCount);
            }
            else if (CXLX == "查封")
            {
                //base.Db.Aop.OnLogExecuting = (sql, pars) =>
                //{
                //    _logger.LogDebug(sql);
                //};
                pageDataJson = await base.Db.Queryable<DJ_CF, DJ_SJD, DJ_TSGL, DJ_TSGL, QL_TDXG, QL_FWXG, FC_H_QSDC, ZD_QSDC>((CF, SD, TS, TS2, TD, FW, FWH, ZD) =>
                  new object[] { JoinType.Inner, CF.SLBH == SD.SLBH ,JoinType.Inner, SD.SLBH == TS.SLBH,JoinType.Inner, TS.TSTYBM == TS2.TSTYBM,
                JoinType.Left, TS2.SLBH == TD.SLBH,JoinType.Left, TS2.SLBH == FW.SLBH ,
                JoinType.Left, TS2.TSTYBM == FWH.TSTYBM,JoinType.Left, TS2.TSTYBM==ZD.TSTYBM })
                    .Where((CF, SD, TS, TS2, TD, FW, FWH, ZD) => TS2.DJZL == "权属" && (TS.LIFECYCLE == null || TS.LIFECYCLE == 0))
                    .WhereIF(SJLSZT == "0", (CF, SD, TS, TS2, TD, FW, FWH, ZD) => (TS.LIFECYCLE == null || TS.LIFECYCLE == 0))
                    .WhereIF(SJLSZT == "1", (CF, SD, TS, TS2, TD, FW, FWH, ZD) => !((TS.LIFECYCLE == null || TS.LIFECYCLE == 0)))
                    .WhereIF(!string.IsNullOrEmpty(SLBH), (CF, SD, TS, TS2, TD, FW, FWH, ZD) => CF.SLBH.Contains(SLBH))
                    .WhereIF(!string.IsNullOrEmpty(BDCZH), (CF, SD, TS, TS2, TD, FW, FWH, ZD) => CF.CFWH.Contains(BDCZH))
                    .WhereIF(!string.IsNullOrEmpty(BDCDYH), (CF, SD, TS, TS2, TD, FW, FWH, ZD) => TS.BDCDYH.Contains(BDCDYH))
                    .Select((CF, SD, TS, TS2, TD, FW, FWH, ZD) => new
                    {
                        TSTYBM = TS2.TSTYBM,
                        SLBH = SD.SLBH,
                        DJDL = SD.DJDL,
                        BDCZH = CF.CFWH,
                        BDCDYH = TS.BDCDYH,
                        CFLX = CF.CFLX,
                        DJRQ = CF.DJSJ,
                        QLRMC = "",
                        ZJHM = "",
                        LIFECYCLE = TS.LIFECYCLE,
                        ZL = SD.ZL,
                        ZSXLH = "",
                        BDCLX = TS.BDCLX,
                        FWMJ = SqlFunc.IF(CF.CFLX.Contains("预查封")).Return(FWH.JZMJ).End(FW.JZMJ),
                        TDMJ = SqlFunc.IF(CF.CFLX.Contains("预查封")).Return(ZD.FZMJ).End(TD.GYTDMJ),
                        FJ = CF.FJ
                    })
                    .ToJsonPageAsync(pageIndex, pageSize, totalCount);
            }

            PageStringModel page = new PageStringModel()
            {
                data = pageDataJson,
                dataCount = totalCount,
                PageSize = pageSize,
                page = pageIndex
            };
            return page;
        }

        /// <summary>
        /// 房屋追述查询
        /// </summary>
        /// <param name="CXLX">查询类型</param>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        public async Task<HouseRecountListVModel> HouseRecount(string CXLX, string slbh)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            HouseRecountListVModel recount = new HouseRecountListVModel();
            if (CXLX == "权证")
            {
                #region 加载当前数据
                var ftsgl = await base.Db.Queryable<DJ_TSGL, DJ_DJB, DJ_SJD>((TS, D, SJ) => TS.SLBH == D.SLBH && D.SLBH == SJ.SLBH)
                .Where((TS, D, SJ) => TS.SLBH == slbh)
                .GroupBy((TS, D, SJ) => new { TS.TSTYBM, TS.SLBH, TS.DJZL, SJ.DJDL, D.BDCDYH, D.BDCZH, D.DJRQ, D.LIFECYCLE })
                .Select((TS, D, SJ) => new { TSTYBM = TS.TSTYBM, SLBH = TS.SLBH, DJZL = TS.DJZL, DJDL = SJ.DJDL, BDCDYH = D.BDCDYH, BDCZH = D.BDCZH, DJRQ = D.DJRQ, LIFECYCLE = D.LIFECYCLE }).ToListAsync();

                foreach (var result in ftsgl)
                {
                    if (result.DJZL == "权属")
                    {
                        string state = string.Empty;
                        if ((result.LIFECYCLE == null || result.LIFECYCLE == 0) && result.DJRQ != null)
                        {
                            state = "现实";
                        }
                        else if ((result.LIFECYCLE == null || result.LIFECYCLE == -1) && result.DJRQ == null)
                        {
                            state = "进行中";
                        }
                        else
                        {
                            state = "历史";
                        }
                        recount.currentList.TSTYBM = result.TSTYBM;
                        recount.currentList.SLBH = result.SLBH;
                        recount.currentList.BDCDYH = result.BDCDYH;
                        recount.currentList.BDCZH = result.BDCZH;
                        recount.currentList.DJDL = result.DJDL;
                        recount.currentList.DJRQ = result.DJRQ;
                        recount.currentList.State = state;
                    }
                }

                #endregion
                #region 加载历史数据

                var ftsgl3 = await base.Db.Queryable<DJ_XGDJGL, DJ_TSGL, DJ_DJB, DJ_SJD>((GL, TS, D, SJ) => new object[] { JoinType.Left, GL.FSLBH == TS.SLBH, JoinType.Inner, TS.SLBH == D.SLBH, JoinType.Inner, D.SLBH == SJ.SLBH })
                    .Where((GL, TS, D, SJ) => GL.ZSLBH == slbh && TS.DJZL == "权属")
                    .GroupBy((GL, TS, D, SJ) => new { TS.TSTYBM, D.SLBH, TS.DJZL, SJ.DJDL, D.BDCDYH, D.BDCZH, D.DJRQ, D.LIFECYCLE })
                    .Select((GL, TS, D, SJ) => new { TSTYBM = TS.TSTYBM, SLBH = D.SLBH, DJZL = TS.DJZL, DJDL = SJ.DJDL, BDCDYH = D.BDCDYH, BDCZH = D.BDCZH, DJRQ = D.DJRQ, LIFECYCLE = D.LIFECYCLE }).ToListAsync();
                if (ftsgl3.Count != 0)
                {
                    foreach (var result in ftsgl3)
                    {
                        if (result.DJZL == "权属")
                        {
                            string state = string.Empty;
                            if ((result.LIFECYCLE == null || result.LIFECYCLE == 0) && result.DJRQ != null)
                            {
                                state = "现实";
                            }
                            else if ((result.LIFECYCLE == null || result.LIFECYCLE == -1) && result.DJRQ == null)
                            {
                                state = "进行中";
                            }
                            else
                            {
                                state = "历史";
                            }
                            recount.historyList.Add(new HouseRecountVModel()
                            {
                                TSTYBM = result.TSTYBM,
                                SLBH = result.SLBH,
                                BDCDYH = result.BDCDYH,
                                BDCZH = result.BDCZH,
                                DJDL = result.DJDL,
                                DJRQ = result.DJRQ,
                                State = state
                            });
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                #endregion
                #region 加载后续数据
                base.Db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    _logger.LogDebug(sql);
                };
                var ftsgl2 = await base.Db.Queryable<DJ_XGDJGL, DJ_TSGL, DJ_DJB, DJ_SJD>((GL, TS, D, SJ) => new object[] { JoinType.Left, GL.ZSLBH == TS.SLBH, JoinType.Inner, TS.SLBH == D.SLBH, JoinType.Inner, D.SLBH == SJ.SLBH })
                    .Where((GL, TS, D, SJ) => GL.FSLBH == slbh && TS.DJZL == "权属")
                    .GroupBy((GL, TS, D, SJ) => new { TS.TSTYBM, D.SLBH, TS.DJZL, SJ.DJDL, D.BDCDYH, D.BDCZH, D.DJRQ, D.LIFECYCLE })
                    .Select((GL, TS, D, SJ) => new { TSTYBM = TS.TSTYBM, SLBH = D.SLBH, DJZL = TS.DJZL, DJDL = SJ.DJDL, BDCDYH = D.BDCDYH, BDCZH = D.BDCZH, DJRQ = D.DJRQ, LIFECYCLE = D.LIFECYCLE }).ToListAsync();
                if (ftsgl2.Count != 0)//没有历史追述了
                {
                    foreach (var result in ftsgl2)
                    {
                        string state = string.Empty;
                        if ((result.LIFECYCLE == null || result.LIFECYCLE == 0) && result.DJRQ != null)
                        {
                            state = "现实";
                        }
                        else if ((result.LIFECYCLE == null || result.LIFECYCLE == -1) && result.DJRQ == null)
                        {
                            state = "进行中";
                        }
                        else
                        {
                            state = "历史";
                        }
                        recount.futureList.Add(new HouseRecountVModel()
                        {
                            TSTYBM = result.TSTYBM,
                            SLBH = result.SLBH,
                            BDCDYH = result.BDCDYH,
                            BDCZH = result.BDCZH,
                            DJDL = result.DJDL,
                            DJRQ = result.DJRQ,
                            State = state
                        });
                    }
                }
                #endregion
            }
            else if (CXLX == "证明")
            {
                #region 加载当前数据
                var ftsgl = await base.Db.Queryable<DJ_DY>()
                .Where(DY => DY.SLBH == slbh)
                .GroupBy(DY => new { DY.SLBH, DY.BDCDYH, DY.BDCZMH, DY.FZRQ, DY.LIFECYCLE })
                .Select(DY => new { SLBH = DY.SLBH, BDCDYH = DY.BDCDYH, BDCZH = DY.BDCZMH, DJRQ = DY.FZRQ, LIFECYCLE = DY.LIFECYCLE }).ToListAsync();

                foreach (var result in ftsgl)
                {
                    string state = string.Empty;
                    if ((result.LIFECYCLE == null || result.LIFECYCLE == 0) && result.DJRQ != null)
                    {
                        state = "现实";
                    }
                    else if ((result.LIFECYCLE == null || result.LIFECYCLE == -1) && result.DJRQ == null)
                    {
                        state = "进行中";
                    }
                    else
                    {
                        state = "历史";
                    }
                    recount.currentList.TSTYBM = "";
                    recount.currentList.SLBH = result.SLBH;
                    recount.currentList.BDCDYH = result.BDCDYH;
                    recount.currentList.BDCZH = result.BDCZH;
                    recount.currentList.DJRQ = result.DJRQ;
                    recount.currentList.State = state;
                }

                #endregion
                #region 加载历史数据

                var resulLeft = await base.Db.Queryable<DJ_XGDJGL, DJ_DY>((GL, DY) => GL.FSLBH == DY.SLBH)
                .Where((GL, DY) => GL.ZSLBH == slbh)
                .GroupBy((GL, DY) => new { DY.SLBH, DY.BDCDYH, DY.BDCZMH, DY.FZRQ, DY.LIFECYCLE })
                .Select((GL, DY) => new { SLBH = DY.SLBH, BDCDYH = DY.BDCDYH, BDCZH = DY.BDCZMH, DJRQ = DY.FZRQ, LIFECYCLE = DY.LIFECYCLE }).ToListAsync();
                foreach (var result in resulLeft)
                {
                    string state = string.Empty;
                    if ((result.LIFECYCLE == null || result.LIFECYCLE == 0) && result.DJRQ != null)
                    {
                        state = "现实";
                    }
                    else if ((result.LIFECYCLE == null || result.LIFECYCLE == -1) && result.DJRQ == null)
                    {
                        state = "进行中";
                    }
                    else
                    {
                        state = "历史";
                    }
                    recount.historyList.Add(new HouseRecountVModel()
                    {
                        TSTYBM = "",
                        SLBH = result.SLBH,
                        BDCDYH = result.BDCDYH,
                        BDCZH = result.BDCZH,
                        DJDL = "",
                        DJRQ = result.DJRQ,
                        State = state
                    });
                }
                #endregion
                #region 加载后续数据
                var resulRight = await base.Db.Queryable<DJ_XGDJGL, DJ_DY>((GL, DY) => GL.ZSLBH == DY.SLBH )
                .Where((GL, DY) => GL.FSLBH == slbh)
                .GroupBy((GL, DY) => new { DY.SLBH, DY.BDCDYH, DY.BDCZMH, DY.FZRQ, DY.LIFECYCLE })
                .Select((GL, DY) => new { SLBH = DY.SLBH, BDCDYH = DY.BDCDYH, BDCZH = DY.BDCZMH, DJRQ = DY.FZRQ, LIFECYCLE = DY.LIFECYCLE }).ToListAsync();

                foreach (var result in resulRight)
                {
                    string state = string.Empty;
                    if ((result.LIFECYCLE == null || result.LIFECYCLE == 0) && result.DJRQ != null)
                    {
                        state = "现实";
                    }
                    else if ((result.LIFECYCLE == null || result.LIFECYCLE == -1) && result.DJRQ == null)
                    {
                        state = "进行中";
                    }
                    else
                    {
                        state = "历史";
                    }
                    recount.futureList.Add(new HouseRecountVModel()
                    {
                        TSTYBM = "",
                        SLBH = result.SLBH,
                        BDCDYH = result.BDCDYH,
                        BDCZH = result.BDCZH,
                        DJDL = "",
                        DJRQ = result.DJRQ,
                        State = state
                    });
                }
                #endregion
            }
            else if (CXLX == "查封")
            {
                #region 加载当前数据
                var resultCF = await base.Db.Queryable<DJ_CF,DJ_TSGL>((CF, TS) => CF.SLBH == TS.SLBH)
                .Where((CF, TS) => CF.SLBH == slbh)
                .GroupBy((CF, TS) => new { CF.SLBH, CF.BDCDYH, CF.CFWH, CF.LIFECYCLE })
                .Select((CF, TS) => new { SLBH = CF.SLBH, BDCDYH = CF.BDCDYH, BDCZH = CF.CFWH, LIFECYCLE = CF.LIFECYCLE }).ToListAsync();

                foreach (var result in resultCF)
                {
                    string state = string.Empty;
                    if (result.LIFECYCLE == null || result.LIFECYCLE == 0)
                    {
                        state = "现实";
                    }
                    else if (result.LIFECYCLE == null || result.LIFECYCLE == -1)
                    {
                        state = "进行中";
                    }
                    else
                    {
                        state = "历史";
                    }
                    recount.currentList.TSTYBM = "";
                    recount.currentList.SLBH = result.SLBH;
                    recount.currentList.BDCDYH = result.BDCDYH;
                    recount.currentList.BDCZH = result.BDCZH;
                    recount.currentList.State = state;
                }

                #endregion
                #region 加载历史数据

                var resulLeft = await base.Db.Queryable<DJ_XGDJGL, DJ_CF>((GL, CF) => GL.FSLBH == CF.SLBH)
                .Where((GL, CF) => GL.ZSLBH == slbh)
                .GroupBy((GL, CF) => new { CF.SLBH, CF.BDCDYH, CF.LIFECYCLE })
                .Select((GL, CF) => new { SLBH = CF.SLBH, BDCDYH = CF.BDCDYH, LIFECYCLE = CF.LIFECYCLE }).ToListAsync();

                foreach (var result in resulLeft)
                {
                    string state = string.Empty;
                    if (result.LIFECYCLE == null || result.LIFECYCLE == 0)
                    {
                        state = "现实";
                    }
                    else if (result.LIFECYCLE == null || result.LIFECYCLE == -1)
                    {
                        state = "进行中";
                    }
                    else
                    {
                        state = "历史";
                    }
                    recount.historyList.Add(new HouseRecountVModel()
                    {
                        TSTYBM = "",
                        SLBH = result.SLBH,
                        BDCDYH = result.BDCDYH,
                        DJDL = "",
                        State = state
                    });
                }
                #endregion
                #region 加载后续数据
                var resulRight = await base.Db.Queryable<DJ_XGDJGL, DJ_CF>((GL, CF) => GL.ZSLBH == CF.SLBH)
                .Where((GL, CF) => GL.FSLBH == slbh)
                .GroupBy((GL, CF) => new { CF.SLBH, CF.BDCDYH, CF.LIFECYCLE })
                .Select((GL, CF) => new { SLBH = CF.SLBH, BDCDYH = CF.BDCDYH, LIFECYCLE = CF.LIFECYCLE }).ToListAsync();

                foreach (var result in resulLeft)
                {
                    string state = string.Empty;
                    if (result.LIFECYCLE == null || result.LIFECYCLE == 0)
                    {
                        state = "现实";
                    }
                    else if (result.LIFECYCLE == null || result.LIFECYCLE == -1)
                    {
                        state = "进行中";
                    }
                    else
                    {
                        state = "历史";
                    }
                    recount.futureList.Add(new HouseRecountVModel()
                    {
                        TSTYBM = "",
                        SLBH = result.SLBH,
                        BDCDYH = result.BDCDYH,
                        DJDL = "",
                        State = state
                    });
                }

                #endregion
            }
            return recount;
        }


        /// <summary>
        /// 房屋业务信息追述
        /// </summary>
        /// <param name="CXLX">查询类型</param>
        /// <param name="tstybm">图属统一编码</param>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        public async Task<List<El_CascaderTree>> HouseBusinessRecount(string CXLX, string tstybm, string slbh)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            List<El_CascaderTree> treeRoot = new List<El_CascaderTree>();
            if (CXLX == "权证")
            {
                #region 查询当前受理编号权属信息
                var resultDj = await base.Db.Queryable<DJ_DJB, DJ_TSGL>((D, TS) => D.SLBH == TS.SLBH)
                .Where((D, TS) => TS.SLBH == slbh)
                .Select((D, TS) => new { BDCZH = D.BDCZH, SLBH = D.SLBH, LIFECYCLE = D.LIFECYCLE })
                .SingleAsync();
                if (resultDj == null)
                {
                    return null;
                }
                else
                {
                    El_CascaderTree djTree = new El_CascaderTree()
                    {
                        label = $"{resultDj.BDCZH}【{resultDj.LIFECYCLE}】",
                        value = resultDj.SLBH
                    };
                    treeRoot.Add(djTree);
                }
                #endregion

                #region 查询当前图属抵押信息
                var resultDy = await base.Db.Queryable<DJ_DY, DJ_TSGL>((DY, TS) => DY.SLBH == TS.SLBH)
                    .Where((DY, TS) => TS.TSTYBM == tstybm)
                    .Select((DY, TS) => new { BDCZMH = DY.BDCZMH, SLBH = DY.SLBH, LIFECYCLE = DY.LIFECYCLE, DJRQ = DY.DJRQ })
                    .OrderBy(DY => DY.DJRQ)
                    .ToListAsync();
                if (resultDy.Count > 0)
                {
                    foreach (var dy in resultDy)
                    {

                        El_CascaderTree dyTree = new El_CascaderTree()
                        {
                            label = $"{dy.BDCZMH}【{dy.LIFECYCLE}】",
                            value = dy.SLBH
                        };
                        treeRoot.Add(dyTree);
                        var resultDyzx = await base.Db.Queryable<DJ_XGDJGL, DJ_XGDJZX>((GL, ZX) => GL.ZSLBH == ZX.SLBH)
                    .Where((GL, ZX) => GL.FSLBH == dy.SLBH)
                    .Select((GL, ZX) => new { ZXPZH = ZX.ZXPZH, SLBH = ZX.SLBH, DJRQ = ZX.DJRQ, BGLX = GL.BGLX })
                    .ToListAsync();
                        foreach (var zx in resultDyzx)
                        {
                            dyTree.children.Add(new El_CascaderTree()
                            {
                                label = zx.BGLX + zx.DJRQ == null ? "" : "_登记日期:" + ((DateTime)zx.DJRQ).ToString("yyyy-MM-dd"),
                                value = dy.SLBH
                            });
                        }
                    }
                }
                #endregion
            }
            else if (CXLX == "证明")
            {
                var resultDy = await base.Db.Queryable<DJ_DY>()
                    .Where(DY => DY.SLBH == slbh)
                    .Select(DY => new { BDCZMH = DY.BDCZMH, SLBH = DY.SLBH, LIFECYCLE = DY.LIFECYCLE, DJRQ = DY.DJRQ })
                    .OrderBy(DY => DY.DJRQ)
                    .SingleAsync();
                if (resultDy != null)
                {
                    string state = string.Empty;
                    if (resultDy.LIFECYCLE == null || resultDy.LIFECYCLE == 0)
                    {
                        state = "现实";
                    }
                    else if (resultDy.LIFECYCLE == -1)
                    {
                        state = "进行中";
                    }
                    else
                    {
                        state = "历史";
                    }
                    El_CascaderTree dyTree = new El_CascaderTree()
                    {
                        label = resultDy.BDCZMH + "(" + state + ")",
                        value = resultDy.SLBH
                    };
                    treeRoot.Add(dyTree);
                    SetChidrenNode(dyTree, resultDy.SLBH);
                    void SetChidrenNode(El_CascaderTree fNode, string dySlbh)
                    {
                        var resulRight = base.Db.Queryable<DJ_XGDJGL, DJ_DY, DJ_TSGL>((GL, DY, TS) => GL.FSLBH == DY.SLBH && GL.ZSLBH == TS.SLBH)
                .Where((GL, DY, TS) => DY.SLBH == dySlbh)
                .GroupBy((GL, DY, TS) => new { TS.SLBH, TS.DJZL, TS.LIFECYCLE })
                .Select((GL, DY, TS) => new { SLBH = TS.SLBH, DJZL = TS.DJZL, LIFECYCLE = TS.LIFECYCLE }).ToList();
                        foreach (var result in resulRight)
                        {
                            string state = string.Empty;
                            if (result.LIFECYCLE == null || result.LIFECYCLE == 0)
                            {
                                state = "现实";
                            }
                            else if (result.LIFECYCLE == -1)
                            {
                                state = "进行中";
                            }
                            else
                            {
                                state = "历史";
                            }
                            El_CascaderTree sunNode = new El_CascaderTree()
                            {
                                label = (result.DJZL.Contains("注销") ? "抵押注销:" : "抵押变更:") + result.SLBH + "(" + state + ")",
                                value = result.SLBH
                            };
                            fNode.children.Add(sunNode);
                            SetChidrenNode(sunNode, result.SLBH);
                        }
                    }
                }
            }
            else if (CXLX == "查封")
            {
                #region 加载后续数据
                var resulCF = await base.Db.Queryable<DJ_TSGL, DJ_CF>((TS, CF) => TS.SLBH == CF.SLBH)
                .Where((TS, CF) => TS.SLBH == slbh)
                .GroupBy((TS, CF) => new { CF.CFLX, CFWH = CF.CFWH, CF.SLBH, CF.BDCDYH, CF.LIFECYCLE })
                .Select((TS, CF) => new { CFLX = CF.CFLX, CFWH = CF.CFWH, SLBH = CF.SLBH, BDCDYH = CF.BDCDYH, LIFECYCLE = CF.LIFECYCLE }).SingleAsync();
                if (resulCF != null)
                {
                    string state = string.Empty;
                    if (resulCF.LIFECYCLE == null || resulCF.LIFECYCLE == 0)
                    {
                        state = "现实";
                    }
                    else if (resulCF.LIFECYCLE == -1)
                    {
                        state = "进行中";
                    }
                    else
                    {
                        state = "历史";
                    }
                    El_CascaderTree cfTree = new El_CascaderTree()
                    {
                        label = $"{resulCF.CFWH}【{state}】",
                        value = resulCF.SLBH
                    };
                    treeRoot.Add(cfTree);
                    SetCfChidrenNode(cfTree, resulCF.SLBH);
                    var resultCF_ZX = base.Db.Queryable<DJ_XGDJGL, DJ_XGDJZX>((GL, ZX) => GL.ZSLBH == ZX.SLBH)
                .Where((GL, ZX) => GL.FSLBH == resulCF.SLBH)
                .Select((GL, ZX) => new { SLBH = ZX.SLBH, BGLX = GL.BGLX, DJRQ = ZX.DJRQ }).Single();
                    if (resultCF_ZX != null)
                    {
                        El_CascaderTree cfzxTree = new El_CascaderTree()
                        {
                            label = $"注销({resulCF.CFWH})" + resultCF_ZX.DJRQ == null ? "【进行中】" : "",
                            value = resulCF.SLBH
                        };
                        treeRoot.Add(cfzxTree);
                    }
                    void SetCfChidrenNode(El_CascaderTree fNode, string cfSlbh)
                    {
                        var resultCF = base.Db.Queryable<DJ_XGDJGL, DJ_CF>((GL, CF) => GL.ZSLBH == CF.SLBH)
                .Where((GL, CF) => GL.FSLBH == cfSlbh)
                .Select((GL, CF) => new { CFLX = CF.CFLX, SLBH = CF.SLBH, BGLX = GL.BGLX, CF.LIFECYCLE }).ToList();
                        if (resultCF.Count > 0)
                        {
                            foreach (var result in resultCF)
                            {
                                string state = string.Empty;
                                if (result.LIFECYCLE == null || result.LIFECYCLE == 0)
                                {
                                    state = "现实";
                                }
                                else if (result.LIFECYCLE == -1)
                                {
                                    state = "进行中";
                                }
                                else
                                {
                                    state = "历史";
                                }
                                El_CascaderTree sunNode = new El_CascaderTree()
                                {
                                    label = result.CFLX + "(" + state + ")",
                                    value = result.SLBH
                                };
                                fNode.children.Add(sunNode);
                                SetCfChidrenNode(sunNode, result.SLBH);
                            }
                        }
                    }
                }
                #endregion
            }
            return treeRoot;
        }

        /// <summary>
        /// 获取追溯信息
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <param name="djzl">登记种类</param>
        /// <returns></returns>
        public async Task<TraceBackVModel> GetTraceBackInfo(string slbh, string djzl)
        {
          /*  base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDic = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 1, 3, 4, 5, 6, 7, 8, 9 }).ToListAsync();*/
            base.ChangeDB(SysConst.DB_CON_BDC);
            var sjdData = await _dJ_SJD.Query(i => i.SLBH == slbh);
            TraceBackVModel model = new TraceBackVModel();
            if (djzl == "权属")
            {
                //var xgzhData = await base.Db.Queryable<DJ_QLRGL, DJ_XGDJGL, DJ_SJD>((A, B, C) => new object[] { JoinType.Inner, A.SLBH == B.FSLBH, JoinType.Left, B.FSLBH == C.SLBH }).Where((A, B, C) => B.ZSLBH == slbh && (A.QLRLX == "权利人" || A.QLRLX == null)).GroupBy((A, B, C) => new { xgzh = B.XGZH, xgzlx = B.XGZLX, zl = C.ZL }).Select((A, B, C) => new
                //{
                //    xgzh = B.XGZH,
                //    xgzlx = B.XGZLX,
                //    qlrmc = SqlFunc.MappingColumn(A.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(A.QLRMC))"),
                //    zl = C.ZL
                //}).ToListAsync();
                var xgzhData = await base.Db.Queryable<DJ_QLRGL, DJ_XGDJGL, DJ_SJD, DJ_DJB, QL_FWXG, QL_TDXG>((A, B, C, D, E, F) => new object[] { JoinType.Left, A.SLBH == B.ZSLBH, JoinType.Left, B.ZSLBH == C.SLBH, JoinType.Left, C.SLBH == D.SLBH, JoinType.Left, D.SLBH == E.SLBH, JoinType.Left, E.SLBH == F.SLBH }).Where((A, B, C, D, E, F) => B.ZSLBH == slbh && (A.QLRLX == "权利人" || A.QLRLX == null)).GroupBy((A, B, C, D, E, F) => new { xgzh = B.XGZH, xgzlx = B.XGZLX, zl = C.ZL, bdcdyh = D.BDCDYH, qt = D.QT, fj = D.FJ, qdfs = E.QDFS, pgje = E.PGJE, qdje = E.QDJG, fwlx = E.QLLX, fwxz = E.QLXZ, fwyt = E.GHYT, jzmj = E.JZMJ, tnjzmj = E.TNJZMJ, ftjzmj = E.FTJZMJ, tdyt = F.TDYT, tdxz = F.QLXZ, tdlx = F.QLLX, gytdmj = F.GYTDMJ, dytdmj = F.DYTDMJ, fttdmj = F.FTTDMJ, jzzdmj = F.JZZDMJ, syqx = F.SYQX, qsrq = F.QSRQ, zzrq = F.ZZRQ}).Select((A, B, C, D, E, F) => new 
                {
                    qlrmc = SqlFunc.MappingColumn(A.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(A.QLRMC))"),
                    xgzh = B.XGZH,
                    xgzlx = B.XGZLX,
                    zl = C.ZL,
                    bdcdyh = D.BDCDYH,
                    qt = D.QT,
                    fj = D.FJ,
                    qdfs = E.QDFS,
                    pgje = E.PGJE,
                    qdje = E.QDJG,
                    fwlx = E.QLLX,
                    fwxz = E.QLXZ,
                    fwyt = E.GHYT,
                    jzmj = E.JZMJ,
                    tnjzmj = E.TNJZMJ,
                    ftjzmj = E.FTJZMJ,
                    tdyt = F.TDYT,
                    tdlx = F.QLLX,
                    tdxz = F.QLXZ,
                    gytdmj = F.GYTDMJ,
                    dytdmj = F.DYTDMJ,
                    fttdmj = F.FTTDMJ,
                    jzzdmj = F.JZZDMJ,
                    syqx = F.SYQX,
                    qsrq = F.QSRQ,
                    zzrq = F.ZZRQ
                }).ToListAsync();
                if(xgzhData.Count > 0)
                {
                    for (int i = 0; i < xgzhData.Count; i++)
                    {
                        xgzhModel xgzh = new xgzhModel();
                        var fwqlxz = "";
                        var fwqllx = "";
                        var Fwyt = "";
                        var tdQllx = "";
                        var tdQlxz = "";
                        var tdGhyt = "";

                     /*   var fwqlxz = sysDic.Where(s => s.DEFINED_CODE == xgzhData[i].fwxz && s.GID == 3).FirstOrDefault();
                        var fwqllx = sysDic.Where(s => s.DEFINED_CODE == xgzhData[i].fwlx && s.GID == 4).FirstOrDefault();
                        var Fwyt = sysDic.Where(s => s.DEFINED_CODE == xgzhData[i].fwyt && s.GID == 5).FirstOrDefault();
                        var tdQllx = sysDic.Where(s => s.DEFINED_CODE == xgzhData[i].tdlx && s.GID == 6).FirstOrDefault();
                        var tdQlxz = sysDic.Where(s => s.DEFINED_CODE == xgzhData[i].tdxz && s.GID == 7).FirstOrDefault();
                        var tdGhyt = sysDic.Where(s => s.DEFINED_CODE == xgzhData[i].tdyt && s.GID == 8).FirstOrDefault();*/

                        xgzh.qlrmc = xgzhData[i].qlrmc;
                        xgzh.xgzh = xgzhData[i].xgzh;
                        xgzh.xgzlx = xgzhData[i].xgzlx;
                        xgzh.zl = xgzhData[i].zl;
                        xgzh.bdcdyh = xgzhData[i].bdcdyh;
                        xgzh.qt = xgzhData[i].qt;
                        xgzh.fj = xgzhData[i].fj;
                        xgzh.qdfs = xgzhData[i].qdfs;
                        xgzh.qdjg = xgzhData[i].qdje;
                        xgzh.pgje = xgzhData[i].pgje;
                       // xgzh.fwlx_zwm = fwqllx != null ? fwqllx.DNAME : string.Empty;
                       // xgzh.fwxz_zwm = fwqlxz != null ? fwqlxz.DNAME : string.Empty;
                       // xgzh.fwyt_zwm = Fwyt != null ? Fwyt.DNAME : string.Empty;
                        xgzh.jzmj = xgzhData[i].jzmj;
                        xgzh.tnjzmj = xgzhData[i].tnjzmj;
                        xgzh.ftjzmj = xgzhData[i].ftjzmj;
                      //  xgzh.tdyt_zwm = tdGhyt != null ? tdGhyt.DNAME : string.Empty;
                      //  xgzh.tdlx_zwm = tdQllx != null ? tdQllx.DNAME : string.Empty;
                       // xgzh.tdxz_zwm = tdQlxz != null ? tdQlxz.DNAME : string.Empty;
                        xgzh.gytdmj = xgzhData[i].gytdmj;
                        xgzh.dytdmj = xgzhData[i].dytdmj;
                        xgzh.fttdmj = xgzhData[i].fttdmj;
                        xgzh.jzzdmj = xgzhData[i].jzzdmj;
                        xgzh.syqx = xgzhData[i].syqx;
                        xgzh.qsrq = xgzhData[i].qsrq;
                        xgzh.zzrq = xgzhData[i].zzrq;
                        model.xgzhList.Add(xgzh);
                    }

                }
                
                var qlrData = await _qLRGLRepository.Query(qlr => qlr.SLBH == slbh && qlr.QLRLX == "权利人");
                var ywrData = await _qLRGLRepository.Query(ywr => ywr.SLBH == slbh && ywr.QLRLX == "义务人");
                var dyrData = await _qLRGLRepository.Query(dyr => dyr.SLBH == slbh && dyr.QLRLX == "抵押人");
                var dyqrData = await _qLRGLRepository.Query(dyqr => dyqr.SLBH == slbh && dyqr.QLRLX == "抵押权人");
                var spbData = await _SPBRepository.Query(spb => spb.SLBH == slbh);
                if(spbData.Count > 0)
                {
                    model.spbList = spbData;
                }
                if (sjdData.Count > 0)
                {
                    model.DJ_SJDMOdel = sjdData[0];
                }
                //if (xgzhData.Count > 0)
                //{
                //    foreach (var item in xgzhData)
                //    {
                //        xgzhModel xgzh = new xgzhModel();
                //        xgzh.xgzh = item.xgzh;
                //        xgzh.xgzlx = item.xgzlx;
                //        xgzh.qlrmc = item.qlrmc;
                //        xgzh.zl = item.zl;
                //        model.xgzhList.Add(xgzh);
                //    }
                //}

                if (dyrData.Count > 0)
                {
                    model.dyrList = dyqrData;
                }
                if (dyqrData.Count > 0)
                {
                    model.dyqrList = dyqrData;
                }
                if(qlrData.Count > 0)
                {
                    model.qlrList = qlrData;
                }
                if(ywrData.Count > 0)
                {
                    model.ywrrList = ywrData;
                }
            }
            else if (djzl == "权属注销")
            {

            }
            else if (djzl == "抵押")
            {
                //var sjdData = await _dJ_SJD.Query(i => i.SLBH == slbh);
                #region 查询xgzhData信息
                base.Db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    _logger.LogDebug(sql);
                };
                var xgzhData = await base.Db.UnionAll<xgzhModel>(
                    base.Db.Queryable<DJ_QLRGL, DJ_XGDJGL, DJ_SJD>((A, B, C) => new object[] { JoinType.Inner, A.SLBH == B.ZSLBH, JoinType.Left, C.SLBH == B.ZSLBH }).Where((A, B, C) => B.ZSLBH == slbh && A.QLRLX == "抵押权人" && (B.XGZLX == "房屋抵押证明" || B.XGZLX == "房屋预告证明" || B.XGZLX == "房产抵押证明" || B.XGZLX == "土地抵押证明" || B.XGZLX == "房屋异议证明")).GroupBy((A, B, C) => new { xgzh = B.XGZH, xgzlx = B.XGZLX, zl = C.ZL }).Select((A, B, C) => new xgzhModel()
                    {
                        xgzh = B.XGZH,
                        xgzlx = B.XGZLX,
                        qlrmc = SqlFunc.MappingColumn(A.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(A.QLRMC))"),
                        zl = C.ZL
                    }),
                    base.Db.Queryable<DJ_QLRGL, DJ_XGDJGL, DJ_SJD>((A, B, C) => new object[] { JoinType.Inner, A.SLBH == B.FSLBH, JoinType.Left, C.SLBH == B.FSLBH }).Where((A, B, C) => B.ZSLBH == slbh && A.QLRLX == "权利人" && (B.XGZLX == "房产证" || B.XGZLX == "房屋不动产证" || B.XGZLX == "土地不动产证" || B.XGZLX == "土地" || B.XGZLX == "土地证")).GroupBy((A, B, C) => new { xgzh = B.XGZH, xgzlx = B.XGZLX, zl = C.ZL }).Select((A, B, C) => new xgzhModel()
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
                var djbData = await base.Db.Queryable<DJ_XGDJGL, DJ_DJB>((A, B) => A.FSLBH == B.SLBH).Where((A, B) => ((A.XGZLX == "房产证" || A.XGZLX == "房屋不动产证" || A.XGZLX == "土地不动产证" || A.XGZLX == "土地" || A.XGZLX == "土地证")) && A.ZSLBH == slbh).Select((A, B) => new djbModel()
                {
                    slbh = B.SLBH,
                    bdczh = B.BDCZH,
                    bdcdyh = B.BDCDYH
                }).ToListAsync();

                if (djbData.Count > 0)
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
                if (dyData.Count > 0)
                {
                    model.DJ_DYModel = dyData[0];
                }
                #endregion

                var spbData = await _SPBRepository.Query(spb => spb.SLBH == slbh);
                if (spbData.Count > 0)
                {
                    model.spbList = spbData;
                }
            }
            else if (djzl == "抵押注销")
            {

            }
            else if (djzl == "查封")
            {
                //var sjdData = await _dJ_SJD.Query(i => i.SLBH == slbh);
                if(sjdData.Count>0)
                {
                    model.DJ_SJDMOdel = sjdData[0];
                }
                
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

                var qlrData = await _qLRGLRepository.Query(qlr => qlr.SLBH == slbh && qlr.QLRLX == "权利人");
                var ywrData = await _qLRGLRepository.Query(ywr => ywr.SLBH == slbh && ywr.QLRLX == "义务人");

                if (cfqdData.Count > 0)
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
                var spbData = await _SPBRepository.Query(spb => spb.SLBH == slbh);
                if (spbData.Count > 0)
                {
                    model.spbList = spbData;
                }
                if (qlrData.Count > 0)
                {
                    model.qlrList = qlrData;
                }
                if (ywrData.Count > 0)
                {
                    model.ywrrList = ywrData;
                }
            }
            else if (djzl == "查封注销")
            {

            }
            else if (djzl == "解封")
            {

            }
            else if (djzl == "预告")
            {
                if (sjdData.Count > 0)
                {
                    model.DJ_SJDMOdel = sjdData[0];
                }
                var ygData = await base.Db.Queryable<DJ_TSGL, DJ_YG, DJ_SJD, DJ_QLRGL, QL_FWXG>((A, B, C, D, E) =>
                 new Object[] { JoinType.Inner, A.SLBH == B.SLBH, JoinType.Inner, B.SLBH == C.SLBH, JoinType.Left, C.SLBH == D.SLBH, JoinType.Inner, E.SLBH == C.SLBH }).
                Where((A, B, C, D, E) => B.SLBH == slbh)
                ///GroupBy((A, B, C, D) => new { xgzh = B.XGZH, bdcdyh = B.BDCDYH, zl = C.ZL, fj = B.FJ })
                .Select((A, B, C, D, E) => new ygModel()
                {
                    SLBH = B.SLBH,
                    DJZL = B.DJLX,
                    DJDL = C.DJDL,
                    YGZL = B.YGDJZL,
                    DJYY = B.DJYY,
                    BDCZH = B.BDCZMH,
                    BDCDYH = B.BDCDYH,
                    DJRQ = B.DJRQ,
                    ZL = C.ZL,
                    MJ = E.JZMJ,
                    QLLX = E.QLLX,
                    QLXZ = E.QLXZ,
                    SYQX = "",
                    YT = E.GHYT,
                    QLQTQK = "",
                    FJ = B.FJ,
                    ZSBH = C.HTBH,
                    GLGSD = "市本级",
                    CDATE = DateTime.Now
                }).ToListAsync();
                if (ygData.Count > 0)
                {
                    foreach (var item in ygData)
                    {
                        ygModel yg = new ygModel();
                        yg.SLBH = item.SLBH;
                        yg.DJZL = item.DJZL;
                        yg.DJDL = item.DJDL;
                        yg.YGZL = item.YGZL;
                        yg.DJYY = item.DJYY;
                        yg.BDCZH = item.BDCZH;
                        yg.BDCDYH = item.BDCDYH;
                        yg.DJRQ = item.DJRQ;
                        yg.ZL = item.ZL;
                        yg.MJ = item.MJ;
                        yg.QLLX = item.QLLX;
                        yg.QLXZ = item.QLXZ;
                        yg.SYQX = item.SYQX;
                        yg.YT = item.YT;
                        yg.QLQTQK = item.QLQTQK;
                        yg.FJ = item.FJ;
                        yg.ZSBH = item.ZSBH;
                        yg.GLGSD = item.GLGSD;
                        yg.CDATE = item.CDATE;
                        model.ygList.Add(yg);
                    }
                }
                var qlrData = await _qLRGLRepository.Query(qlr => qlr.SLBH == slbh && qlr.QLRLX == "权利人");
                var ywrData = await _qLRGLRepository.Query(ywr => ywr.SLBH == slbh && ywr.QLRLX == "义务人");
                //DJ_YG
                var yGData = await _dJ_YG.Query(i => i.SLBH == slbh);
                model.DJ_YGModel = yGData[0];
                if (qlrData.Count > 0)
                {
                    model.qlrList = qlrData;
                }
                if (ywrData.Count > 0)
                {
                    model.ywrrList = ywrData;
                }
            }
            else if (djzl == "异议")
            {
                if (sjdData.Count > 0)
                {
                    model.DJ_SJDMOdel = sjdData[0];
                }
                var yyqdData = await base.Db.Queryable<DJ_TSGL, DJ_YY, DJ_SJD, DJ_QLRGL>((A, B, C, D) =>
                new object[] { JoinType.Inner, A.SLBH == B.SLBH, JoinType.Inner, B.SLBH == C.SLBH, JoinType.Left, C.SLBH == D.SLBH })
                    .Where((A, B, C, D) => B.SLBH == slbh)
                    //.GroupBy((A, B, C, D) => new { xgzh = B.XGZH, bdcdyh = B.BDCDYH, zl = C.ZL, fj = B.FJ })
                    .Select((A, B, C, D) => new yyModel()
                    {
                        SLBH = B.SLBH,
                        DJLZ = "异议",
                        DJDL = C.DJDL,
                        CFLX = "无",
                        YYYY = B.YYSX,
                        CFWH = "无",
                        BDCDYH = B.BDCDYH,
                        DJRQ = B.DJRQ,
                        SQSX = B.YYSX,
                        ZL = B.WZ,
                        SQSJ = B.DJRQ,
                        FJ = B.FJ,
                        GLGSD = "市本级",
                        CDATE = DateTime.Now
                    }).ToListAsync();
                //
                if (yyqdData.Count > 0)
                {
                    foreach (var item in yyqdData)
                    {
                        yyModel yy = new yyModel();
                        yy.SLBH = item.SLBH;
                        yy.DJLZ = item.DJLZ;
                        yy.DJDL = item.DJDL;
                        yy.CFLX = item.CFLX;
                        yy.YYYY = item.YYYY;
                        yy.CFWH = item.CFWH;
                        yy.BDCDYH = item.BDCDYH;
                        yy.DJRQ = item.DJRQ;
                        yy.SQSX = item.SQSX;
                        yy.ZL = item.ZL;
                        yy.SQSJ = item.SQSJ;
                        yy.FJ = item.FJ;
                        yy.GLGSD = item.GLGSD;
                        yy.CDATE = item.CDATE;
                        model.yyList.Add(yy);
                    }
                }
                var qlrData = await _qLRGLRepository.Query(qlr => qlr.SLBH == slbh && qlr.QLRLX == "权利人");
                var ywrData = await _qLRGLRepository.Query(ywr => ywr.SLBH == slbh && ywr.QLRLX == "义务人");
                var yyData = await _dJ_YY.Query(i => i.SLBH == slbh);
                model.DJ_YYModel = yyData[0];
                if (qlrData.Count > 0)
                {
                    model.qlrList = qlrData;
                }
                if (ywrData.Count > 0)
                {
                    model.ywrrList = ywrData;
                }
            }


            return model;
        }
    }
}
