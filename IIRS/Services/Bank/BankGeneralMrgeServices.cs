using IIRS.IRepository.Base;
using IIRS.IServices.Bank;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services.Bank
{
    public class BankGeneralMrgeServices : BaseServices, IBankGeneralMrgeServices
    {
        private readonly ILogger<BankGeneralMrgeServices> _logger;

        IDBTransManagement _dbTransManagement;
        public BankGeneralMrgeServices(IDBTransManagement dbTransManagement, ILogger<BankGeneralMrgeServices> logger) : base(dbTransManagement)
        {
            this._logger = logger;
            this._dbTransManagement = dbTransManagement;
        }

        /// <summary>
        /// 查询房屋信息
        /// </summary>
        /// <param name="XM">姓名</param>
        /// <param name="ZJHM">证件号码（身份证）</param>
        /// <param name="BDCZH">不动产证号</param>
        /// <param name="BDCLX">不动产类型</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        public async Task<PageStringModel> GetBdcHourseInfo(string XM, string ZJHM, string BDCZH,string BDCLX, int pageIndex = 1, int pageSize = 10)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            //RefAsync<int> totalCount = 0;
            string pageDataJson = string.Empty;
            string ZJHM_15 = string.Empty;
            if (ZJHM.Length == 18)//证件号码如果为18位，视为身份证，并转换15位老身份证进行业务查询
            {
                ZJHM_15 = ZJHM.Substring(0, 6) + ZJHM.Substring(8, 9);
            }
            if (BDCLX == "房屋")
            {
                pageDataJson = await base.Db.Queryable<DJ_TSGL, DJ_DJB, DJ_SJD, FC_H_QSDC, FC_Z_QSDC>((TS, D, SD, FC, FZ) => TS.SLBH == D.SLBH && D.SLBH == SD.SLBH && TS.TSTYBM == FC.TSTYBM && FC.LSZTYBM == FZ.TSTYBM)
                .Where((TS, D, SD, FC, FZ) => (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null) && TS.BDCLX == BDCLX
                && D.BDCZH == BDCZH && SqlFunc.Subqueryable<V_DJ_QLRGL_ListModel>().Where(G => D.SLBH == G.SLBH
                && G.QLRMC == XM && G.QLRLX == "权利人" && (G.ZJHM == ZJHM || G.ZJHM == ZJHM_15)).Any()
                && (D.ZSLX == "房屋不动产证" || D.ZSLX == "房产证")
                && D.DJRQ != null && D.BDCZH != null && D.ZSXLH != null)
                .GroupBy((TS, D, SD, FC, FZ) => new { FZ.FWJG, FZ.ZCS, FC.HZCS, FC.MYC, D.ZSLX, D.FJ, TS.TSTYBM, TS.SLBH, SD.DJDL, D.BDCZH, D.BDCDYH, D.DJRQ, SD.ZL, TS.BDCLX })
                .Select((TS, D, SD, FC, FZ) => new
                {
                    RN = SqlFunc.MappingColumn(TS.TSTYBM, "ROW_NUMBER() OVER(ORDER BY SYSDATE)"),
                    TSTYBM = TS.TSTYBM,
                    SLBH = TS.SLBH,
                    DJDL = SD.DJDL,
                    BDCZH = D.BDCZH,
                    BDCDYH = D.BDCDYH,
                    DJRQ = D.DJRQ,
                    QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.QLRMC),
                    ZJHM = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.ZJHM),
                    ZL = SD.ZL,
                    BDCLX = TS.BDCLX,
                    FWMJ = SqlFunc.Subqueryable<QL_FWXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.AggregateMax(s.JZMJ.ToString())),
                    //TDMJ = SqlFunc.Subqueryable<QL_TDXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.IsNull(SqlFunc.AggregateMax(s.DYTDMJ.ToString()), SqlFunc.AggregateMax(s.GYTDMJ.ToString()))),
                    TDMJ = "",
                    ZSLX = D.ZSLX,
                    ZT = SqlFunc.MappingColumn(TS.DJZL, "(SELECT WM_CONCAT(DISTINCT TO_CHAR(TS2.DJZL)) FROM DJ_TSGL TS2 WHERE TS2.TSTYBM = TS.TSTYBM AND TS2.DJZL NOT IN ('异议注销'，'查封注销'，'抵押注销'，'预告注销') AND ((TS2.LIFECYCLE = 0) OR (TS2.LIFECYCLE IS NULL)))"),
                    //ZT = SqlFunc.MappingColumn(TS.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(TS.DJZL))"),
                    DJB_FJ = D.FJ,
                    SZC = FC.MYC,
                    HZCS = FC.HZCS,
                    FWJG = FZ.FWJG,
                    FWJG_ZWM = SqlFunc.Subqueryable<T_ENUM_FC_FWJG_EModel>().Where(F => F.FWJGID == FZ.FWJG).Select(s => SqlFunc.AggregateMax(s.FWJGQC)),
                    ZZCS = FZ.ZCS
                }).ToJsonPageAsync(pageIndex, pageSize);
            }
            else
            {
                pageDataJson = await base.Db.Queryable<DJ_DJB, DJ_SJD, DJ_TSGL, ZD_QSDC>((D, SD, TS, ZD) => new object[] { JoinType.Inner, D.SLBH == SD.SLBH, JoinType.Inner, TS.SLBH == SD.SLBH, JoinType.Left, ZD.TSTYBM == TS.TSTYBM })
                .Where((D, SD, TS, ZD) => (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null) && TS.BDCLX == BDCLX
                && D.BDCZH == BDCZH && SqlFunc.Subqueryable<V_DJ_QLRGL_ListModel>().Where(G => D.SLBH == G.SLBH
                && G.QLRMC == XM && G.QLRLX == "权利人" && (G.ZJHM == ZJHM || G.ZJHM == ZJHM_15)).Any()
                && (ZD.LIFECYCLE == 0 || ZD.LIFECYCLE == null) && (D.LIFECYCLE == 0 || D.LIFECYCLE == null)
                && (D.ZSLX == "土地不动产证" || D.ZSLX == "土地证" || D.ZSLX == "大土地证")
                && D.DJRQ != null && D.BDCZH != null && D.ZSXLH != null)
                .GroupBy((D, SD, TS, ZD) => new { D.FJ, D.ZSLX, TS.TSTYBM, TS.SLBH, SD.DJDL, D.BDCZH, D.BDCDYH, D.DJRQ, SD.ZL, TS.BDCLX })
                .Select((D, SD, TS, ZD) => new
                {
                    RN = SqlFunc.MappingColumn(TS.TSTYBM, "ROW_NUMBER() OVER(ORDER BY SYSDATE)"),
                    TSTYBM = TS.TSTYBM,
                    SLBH = TS.SLBH,
                    DJDL = SD.DJDL,
                    BDCZH = D.BDCZH,
                    BDCDYH = D.BDCDYH,
                    DJRQ = D.DJRQ,
                    QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.QLRMC),
                    ZJHM = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.ZJHM),
                    //QLRMC = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'权利人',R.QLRMC,'')))"),
                    //ZJHM = SqlFunc.MappingColumn(R.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'权利人',R.ZJHM,'')))"),
                    ZL = SD.ZL,
                    BDCLX = TS.BDCLX,
                    FWMJ = "",
                    TDMJ = SqlFunc.Subqueryable<QL_TDXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.IsNull(SqlFunc.AggregateMax(s.DYTDMJ.ToString()), SqlFunc.AggregateMax(s.GYTDMJ.ToString()))),
                    ZSLX = D.ZSLX,
                    ZT = SqlFunc.MappingColumn(TS.DJZL, "(SELECT WM_CONCAT(DISTINCT TO_CHAR(TS2.DJZL)) FROM DJ_TSGL TS2 WHERE TS2.TSTYBM = TS.TSTYBM AND TS2.DJZL NOT IN ('异议注销'，'查封注销'，'抵押注销'，'预告注销') AND ((TS2.LIFECYCLE = 0) OR (TS2.LIFECYCLE IS NULL)))"),
                    //ZT = SqlFunc.MappingColumn(TS.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(TS.DJZL))"),
                    DJB_FJ = D.FJ,
                    SZC = -100,
                    HZCS = -1,
                    FWJG = -1,
                    FWJG_ZWM = "",
                    ZZCS = -1
                }).ToJsonPageAsync(pageIndex, pageSize);
            }

            //RefAsync<int> totalCount = 0;
            //string pageDataJson = await base.Db.Queryable<DJ_TSGL, DJ_DJB, DJ_QLRGL, DJ_QLR, DJ_SJD>((TS, D, GL, R, SD) => TS.SLBH == D.SLBH && D.SLBH == GL.SLBH && GL.QLRID == R.QLRID && D.SLBH == SD.SLBH)
            //    .Where((TS, D, GL, R, SD) => (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null) && GL.QLRLX == "权利人")
            //    .WhereIF(!string.IsNullOrEmpty(BDCZH), (TS, D, GL, R, SD) => SqlFunc.Contains(D.BDCZH, BDCZH))
            //    .WhereIF(!string.IsNullOrEmpty(BDCLX) && (new string[] { "宗地", "房屋" }).Contains(BDCLX), (TS, D, GL, R, SD) => TS.BDCLX == BDCLX)
            //    .WhereIF(!string.IsNullOrEmpty(QLRMC), (TS, D, GL, R, SD) => SqlFunc.Contains(R.QLRMC, QLRMC))
            //    .WhereIF(!string.IsNullOrEmpty(ZL), (TS, D, GL, R, SD) => SqlFunc.Contains(SD.ZL, ZL))
            //    .GroupBy((TS, D, GL, R, SD) => new { TS.TSTYBM, TS.SLBH, SD.DJDL, D.BDCZH, D.BDCDYH, D.DJRQ, SD.ZL, TS.BDCLX })
            //    .Select((TS, D, GL, R, SD) => new
            //    {
            //        TSTYBM = TS.TSTYBM,
            //        SLBH = TS.SLBH,
            //        DJDL = SD.DJDL,
            //        BDCZH = D.BDCZH,
            //        BDCDYH = D.BDCDYH,
            //        DJRQ = D.DJRQ,
            //        QLRMC = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'权利人',R.QLRMC,'')))"),
            //        ZJHM = SqlFunc.MappingColumn(R.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'权利人',R.ZJHM,'')))"),
            //        ZL = SD.ZL,
            //        BDCLX = TS.BDCLX,
            //        FWMJ = SqlFunc.Subqueryable<QL_FWXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.AggregateMax(s.JZMJ.ToString())),
            //        TDMJ = SqlFunc.Subqueryable<QL_TDXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.IsNull(SqlFunc.AggregateMax(s.DYTDMJ.ToString()), SqlFunc.AggregateMax(s.GYTDMJ.ToString()))),
            //        ZT = SqlFunc.MappingColumn(TS.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(TS.DJZL))")
            //    }).ToJsonPageAsync(pageIndex, pageSize, totalCount);
            return new PageStringModel()
            {
                data = pageDataJson,
                dataCount = 0,
                PageSize = pageSize,
                page = pageIndex
            };
        }
        /// <summary>
        /// 查询房屋信息
        /// </summary>
        /// <param name="XM">姓名</param>
        /// <param name="ZJHM">证件号码（身份证）</param>
        /// <param name="BDCZH">不动产证号</param>
        /// <param name="BDCLX">不动产类型</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        public async Task<PageStringModel> GetAdvanceHourseInfo(string XM, string ZJHM, string BDCZH, string BDCLX, int pageIndex = 1, int pageSize = 10)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            //RefAsync<int> totalCount = 0;
            string pageDataJson = string.Empty;
            string ZJHM_15 = string.Empty;
            if (ZJHM.Length == 18)//证件号码如果为18位，视为身份证，并转换15位老身份证进行业务查询
            {
                ZJHM_15 = ZJHM.Substring(0, 6) + ZJHM.Substring(8, 9);
            }
            if (BDCLX == "房屋")
            {
                pageDataJson = await base.Db.Queryable<DJ_TSGL, DJ_YG, DJ_SJD, FC_H_QSDC, FC_Z_QSDC>((TS, D, SD, FC, FZ) => TS.SLBH == D.SLBH && D.SLBH == SD.SLBH && TS.TSTYBM == FC.TSTYBM && FC.LSZTYBM == FZ.TSTYBM)
                .Where((TS, D, SD, FC, FZ) => (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null) && TS.BDCLX == BDCLX
                && D.BDCZMH == BDCZH && SqlFunc.Subqueryable<V_DJ_QLRGL_ListModel>().Where(G => D.SLBH == G.SLBH
                && G.QLRMC == XM && G.QLRLX == "权利人" && (G.ZJHM == ZJHM || G.ZJHM == ZJHM_15)).Any()
                && D.DJRQ != null && D.BDCZMH != null && D.ZSXLH != null)
                .GroupBy((TS, D, SD, FC, FZ) => new { FZ.FWJG, FZ.ZCS, FC.HZCS, FC.MYC,  D.FJ, TS.TSTYBM, TS.SLBH, SD.DJDL, D.BDCZMH, D.BDCDYH, D.DJRQ, SD.ZL, TS.BDCLX })
                .Select((TS, D, SD, FC, FZ) => new
                {
                    RN = SqlFunc.MappingColumn(TS.TSTYBM, "ROW_NUMBER() OVER(ORDER BY SYSDATE)"),
                    TSTYBM = TS.TSTYBM,
                    SLBH = TS.SLBH,
                    DJDL = SD.DJDL,
                    BDCZH = D.BDCZMH,
                    BDCDYH = D.BDCDYH,
                    DJRQ = D.DJRQ,
                    QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.QLRMC),
                    ZJHM = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.ZJHM),
                    ZL = SD.ZL,
                    BDCLX = TS.BDCLX,
                    FWMJ = SqlFunc.Subqueryable<QL_FWXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.AggregateMax(s.JZMJ.ToString())),
                    //TDMJ = SqlFunc.Subqueryable<QL_TDXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.IsNull(SqlFunc.AggregateMax(s.DYTDMJ.ToString()), SqlFunc.AggregateMax(s.GYTDMJ.ToString()))),
                    TDMJ = "",
                    ZT = SqlFunc.MappingColumn(TS.DJZL, "(SELECT WM_CONCAT(DISTINCT TO_CHAR(TS2.DJZL)) FROM DJ_TSGL TS2 WHERE TS2.TSTYBM = TS.TSTYBM AND TS2.DJZL NOT IN ('异议注销'，'查封注销'，'抵押注销'，'预告注销') AND ((TS2.LIFECYCLE = 0) OR (TS2.LIFECYCLE IS NULL)))"),
                    //ZT = SqlFunc.MappingColumn(TS.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(TS.DJZL))"),
                    DJB_FJ = D.FJ,
                    SZC = FC.MYC,
                    HZCS = FC.HZCS,
                    FWJG = FZ.FWJG,
                    FWJG_ZWM = SqlFunc.Subqueryable<T_ENUM_FC_FWJG_EModel>().Where(F => F.FWJGID == FZ.FWJG).Select(s => SqlFunc.AggregateMax(s.FWJGQC)),
                    ZZCS = FZ.ZCS
                }).ToJsonPageAsync(pageIndex, pageSize);
            }
            else
            {
                pageDataJson = await base.Db.Queryable<DJ_YG, DJ_SJD, DJ_TSGL, ZD_QSDC>((D, SD, TS, ZD) => new object[] { JoinType.Inner, D.SLBH == SD.SLBH, JoinType.Inner, TS.SLBH == SD.SLBH, JoinType.Left, ZD.TSTYBM == TS.TSTYBM })
                .Where((D, SD, TS, ZD) => (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null) && TS.BDCLX == BDCLX
                && D.BDCZMH == BDCZH && SqlFunc.Subqueryable<V_DJ_QLRGL_ListModel>().Where(G => D.SLBH == G.SLBH
                && G.QLRMC == XM && G.QLRLX == "权利人" && (G.ZJHM == ZJHM || G.ZJHM == ZJHM_15)).Any()
                && (ZD.LIFECYCLE == 0 || ZD.LIFECYCLE == null) && (D.LIFECYCLE == 0 || D.LIFECYCLE == null)
                && D.DJRQ != null && D.BDCZMH != null && D.ZSXLH != null)
                .GroupBy((D, SD, TS, ZD) => new { D.FJ, TS.TSTYBM, TS.SLBH, SD.DJDL, D.BDCZMH, D.BDCDYH, D.DJRQ, SD.ZL, TS.BDCLX })
                .Select((D, SD, TS, ZD) => new
                {
                    RN = SqlFunc.MappingColumn(TS.TSTYBM, "ROW_NUMBER() OVER(ORDER BY SYSDATE)"),
                    TSTYBM = TS.TSTYBM,
                    SLBH = TS.SLBH,
                    DJDL = SD.DJDL,
                    BDCZH = D.BDCZMH,
                    BDCDYH = D.BDCDYH,
                    DJRQ = D.DJRQ,
                    QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.QLRMC),
                    ZJHM = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.ZJHM),
                    //QLRMC = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'权利人',R.QLRMC,'')))"),
                    //ZJHM = SqlFunc.MappingColumn(R.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'权利人',R.ZJHM,'')))"),
                    ZL = SD.ZL,
                    BDCLX = TS.BDCLX,
                    FWMJ = "",
                    TDMJ = SqlFunc.Subqueryable<QL_TDXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.IsNull(SqlFunc.AggregateMax(s.DYTDMJ.ToString()), SqlFunc.AggregateMax(s.GYTDMJ.ToString()))),
                    ZT = SqlFunc.MappingColumn(TS.DJZL, "(SELECT WM_CONCAT(DISTINCT TO_CHAR(TS2.DJZL)) FROM DJ_TSGL TS2 WHERE TS2.TSTYBM = TS.TSTYBM AND TS2.DJZL NOT IN ('异议注销'，'查封注销'，'抵押注销'，'预告注销') AND ((TS2.LIFECYCLE = 0) OR (TS2.LIFECYCLE IS NULL)))"),
                    //ZT = SqlFunc.MappingColumn(TS.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(TS.DJZL))"),
                    DJB_FJ = D.FJ,
                    SZC = -100,
                    HZCS = -1,
                    FWJG = -1,
                    FWJG_ZWM = "",
                    ZZCS = -1
                }).ToJsonPageAsync(pageIndex, pageSize);
            }

            return new PageStringModel()
            {
                data = pageDataJson,
                dataCount = 0,
                PageSize = pageSize,
                page = pageIndex
            };
        }

        /// <summary>
        /// 查询不动产权利人
        /// </summary>
        /// <param name="SLBH">登记簿受理编号</param>
        /// <returns></returns>
        public async Task<List<QLR_VModel>> GetBankQlrInfo(string[] SLBH)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var resultZJLB = await base.Db.Queryable<SYS_DIC>().Where(G => G.GID == 1).ToListAsync();
            var dicZjlb = resultZJLB.ToDictionary(x => x.DEFINED_CODE, x => x.DNAME);

            base.ChangeDB(SysConst.DB_CON_BDC);
            List<QLR_VModel> resultQlrList = await base.Db.Queryable<DJ_YG, DJ_QLRGL, DJ_QLR>((D, GL, R) => D.SLBH == GL.SLBH && GL.QLRID == R.QLRID)
                .Where((D, GL, R) => (D.LIFECYCLE == 0 || D.LIFECYCLE == null) && GL.QLRLX == "权利人" && SLBH.Contains(D.SLBH))
                .GroupBy((D, GL, R) => new { GL.GYFE, GL.GYFS, R.ZJLB, R.QLRMC, R.ZJHM })
                .Select((D, GL, R) => new QLR_VModel
                {
                    QLRID = SqlFunc.AggregateMax(R.QLRID),
                    ZJLB = R.ZJLB,
                    QLRMC = R.QLRMC,
                    GYFS = GL.GYFS,
                    GYFE = GL.GYFE,
                    ZJHM = R.ZJHM,
                    DH = SqlFunc.AggregateMax(R.DH)
                }).ToListAsync();

            foreach (var qlr in resultQlrList)
            {
                qlr.ZJLB_ZWM = dicZjlb[qlr.ZJLB];
                qlr.ISCERTIFIED = 0;
                qlr.IS_OWNER = 1;
            }
            return resultQlrList;
        }
        /// <summary>
        /// 房屋抵押不动产中心审批
        /// </summary>
        /// <param name="AuzInfo">订单表</param>
        /// <param name="regInfo">注册信息</param>
        /// <param name="jsonData">登记信息保存暂存信息表</param>
        /// <param name="spInfo">审批信息表</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="dyInfo">注销信息</param>
        /// <returns>多表操作影响记录数之和</returns>
        public int Auditing(BankAuthorize AuzInfo, REGISTRATION_INFO regInfo, SysDataRecorderModel jsonData, SPB_INFO spInfo, IFLOW_DO_ACTION flowInfo, DY_INFO dyInfo)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);

            try
            {
                int count = 0;
                this._dbTransManagement.BeginTran();
                count += base.Db.Updateable(jsonData).UpdateColumns(D => new
                {
                    D.SAVEDATAJSON
                }).Where(S => S.BUS_PK == spInfo.XID).ExecuteCommand();

                count += base.Db.Updateable(regInfo).UpdateColumns(R => new
                {
                    R.IS_ACTION_OK
                }).Where(S => S.XID == regInfo.XID).ExecuteCommand();

                base.Db.Deleteable<SPB_INFO>().Where(S => S.XID == spInfo.XID).ExecuteCommand();
                count += base.Db.Updateable(dyInfo).UpdateColumns(zl => new
                {
                    zl.SPBZ
                }).Where(S => S.XID == dyInfo.XID).ExecuteCommand();
                count += base.Db.Insertable(spInfo).ExecuteCommand();
                count += base.Db.Updateable(AuzInfo).UpdateColumns(auz => new
                {
                    auz.STATUS,
                    auz.PRE_STATUS
                }).Where(S => S.BID == AuzInfo.BID).ExecuteCommand();

                count = base.Db.Insertable(flowInfo).InsertColumns(fw => new
                {
                    fw.PK,
                    fw.FLOW_ID,
                    fw.PRE_FLOW_ID,
                    fw.AUZ_ID,
                    fw.CDATE,
                    fw.USER_NAME
                }).ExecuteCommand();

                this._dbTransManagement.CommitTran();
                return count;
            }
            catch (Exception ex)
            {
                this._dbTransManagement.RollbackTran();
                //_logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

    }
}
