using IIRS.IRepository.Base;
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

namespace IIRS.Services
{
    public class BdcGeneralMrgeServices : BaseServices, IBdcGeneralMrgeServices
    {
        private readonly ILogger<BdcGeneralMrgeServices> _logger;

        IDBTransManagement _dbTransManagement;
        public BdcGeneralMrgeServices(IDBTransManagement dbTransManagement, ILogger<BdcGeneralMrgeServices> logger) : base(dbTransManagement)
        {
            this._logger = logger;
            this._dbTransManagement = dbTransManagement;
        }

        /// <summary>
        /// 查询房屋信息
        /// </summary>
        /// <param name="BDCZH">不动产证号</param>
        /// <param name="BDCLX">不动产类型(宗地、房屋,否则去除该条件)</param>
        /// <param name="QLRMC">权利人名称</param>
        /// <param name="ZL">坐落</param>
        /// <param name="DJB_SLBH">(登记簿)受理编号</param>
        /// <param name="BDCDYH">不动产单元和</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        public async Task<PageStringModel> GetBdcHourseInfo(string BDCZH, string BDCLX, string QLRMC, string ZL, string DJB_SLBH,string BDCDYH, int pageIndex = 1, int pageSize = 10)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            //RefAsync<int> totalCount = 0;
            string pageDataJson = string.Empty;
            if (BDCLX == "房屋")
            {
                pageDataJson = await base.Db.Queryable<DJ_TSGL, DJ_DJB, DJ_SJD, FC_H_QSDC, FC_Z_QSDC>((TS, D, SD, FC, FZ) => TS.SLBH == D.SLBH && D.SLBH == SD.SLBH && TS.TSTYBM == FC.TSTYBM && FC.LSZTYBM == FZ.TSTYBM)
                .Where((TS, D, SD, FC, FZ) => (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null) && TS.BDCLX == BDCLX
                && (D.ZSLX == "房屋不动产证" || D.ZSLX == "房产证")
                && D.DJRQ != null && D.BDCZH != null && D.ZSXLH != null)
                .WhereIF(!string.IsNullOrEmpty(DJB_SLBH), (TS, D, SD, FC, FZ) => SqlFunc.Contains(D.SLBH, DJB_SLBH))
                .WhereIF(!string.IsNullOrEmpty(BDCDYH), (TS, D, SD, FC, FZ) => SqlFunc.Contains(D.BDCDYH, BDCDYH))
                .WhereIF(!string.IsNullOrEmpty(QLRMC), @"EXISTS (SELECT * FROM V_DJ_QLRGL G WHERE G.QLRLX = '权利人' AND G.SLBH = D.SLBH AND QLRMC LIKE '%' || '%" + QLRMC + @"%' || ' %')")
                .WhereIF(!string.IsNullOrEmpty(BDCZH), (TS, D, SD, FC, FZ) => SqlFunc.Contains(D.BDCZH, BDCZH))
                .WhereIF(!string.IsNullOrEmpty(ZL), (TS, D, SD, FC, FZ) => SqlFunc.Contains(SD.ZL, ZL))
                .GroupBy((TS, D, SD, FC, FZ) => new { FZ.FWJG, FZ.ZCS, FC.HZCS, FC.MYC, D.ZSLX, D.FJ, TS.TSTYBM, TS.SLBH, SD.DJDL, D.BDCZH, TS.BDCDYH, D.DJRQ, SD.ZL, TS.BDCLX })
                .Select((TS, D, SD, FC, FZ) => new
                {
                    RN = SqlFunc.MappingColumn(TS.TSTYBM, "ROW_NUMBER() OVER(ORDER BY SYSDATE)"),
                    TSTYBM = TS.TSTYBM,
                    SLBH = TS.SLBH,
                    DJDL = SD.DJDL,
                    BDCZH = D.BDCZH,
                    BDCDYH = TS.BDCDYH,
                    DJRQ = D.DJRQ,
                    QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.QLRMC),
                    ZJHM = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.ZJHM),
                    //QLRMC = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'权利人',R.QLRMC,'')))"),
                    //ZJHM = SqlFunc.MappingColumn(R.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'权利人',R.ZJHM,'')))"),
                    ZL = SD.ZL,
                    BDCLX = TS.BDCLX,
                    FWMJ = SqlFunc.Subqueryable<QL_FWXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.AggregateMax(s.JZMJ.ToString())),
                    //TDMJ = SqlFunc.Subqueryable<QL_TDXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.IsNull(SqlFunc.AggregateMax(s.DYTDMJ.ToString()), SqlFunc.AggregateMax(s.GYTDMJ.ToString()))),
                    TDMJ = "",
                    ZSLX = D.ZSLX,
                    //ZT = SqlFunc.MappingColumn(TS.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(TS.DJZL))"),
                    ZT = SqlFunc.MappingColumn(TS.DJZL, "(SELECT WM_CONCAT(DISTINCT TO_CHAR(TS2.DJZL)) FROM DJ_TSGL TS2 WHERE TS2.TSTYBM = TS.TSTYBM AND TS2.DJZL NOT IN ('异议注销'，'查封注销'，'抵押注销'，'预告注销') AND ((TS2.LIFECYCLE = 0) OR (TS2.LIFECYCLE IS NULL)))"),
                    //ZT = SqlFunc.Subqueryable<DJ_TSGL>().Where(TS2 => TS2.TSTYBM == TS.TSTYBM && (TS2.LIFECYCLE == 0 || TS2.LIFECYCLE == null)).Select(TS2 => SqlFunc.MappingColumn(TS2.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(DJZL))")),
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
                && (ZD.LIFECYCLE == 0 || ZD.LIFECYCLE == null) && (D.LIFECYCLE == 0 || D.LIFECYCLE == null)
                && (D.ZSLX == "土地不动产证" || D.ZSLX == "土地证" || D.ZSLX == "大土地证")
                && D.DJRQ != null && D.BDCZH != null && D.ZSXLH != null)
                .WhereIF(!string.IsNullOrEmpty(QLRMC), @"EXISTS (SELECT * FROM V_DJ_QLRGL G WHERE G.QLRLX = '权利人' AND G.SLBH = D.SLBH AND QLRMC LIKE '%' || '%" + QLRMC + @"%' || ' %')")
                .WhereIF(!string.IsNullOrEmpty(BDCZH), (D, SD, TS, ZD) => SqlFunc.Contains(D.BDCZH, BDCZH))
                .WhereIF(!string.IsNullOrEmpty(ZL), (D, SD, TS, ZD) => SqlFunc.Contains(SD.ZL, ZL))
                .GroupBy((D, SD, TS, ZD) => new { D.FJ, D.ZSLX, TS.TSTYBM, TS.SLBH, SD.DJDL, D.BDCZH, TS.BDCDYH, D.DJRQ, SD.ZL, TS.BDCLX })
                .Select((D, SD, TS, ZD) => new
                {
                    RN = SqlFunc.MappingColumn(TS.TSTYBM, "ROW_NUMBER() OVER(ORDER BY SYSDATE)"),
                    TSTYBM = TS.TSTYBM,
                    SLBH = TS.SLBH,
                    DJDL = SD.DJDL,
                    BDCZH = D.BDCZH,
                    BDCDYH = TS.BDCDYH,
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
        /// 抵押勘误查询
        /// </summary>
        /// <param name="BDCDYH">不动产单元号</param>
        /// <param name="BDCZMH">不动产证明号</param>
        /// <param name="SLBH">(登记簿)受理编号</param>
        /// <param name="BDCLX">不动产类型(宗地、房屋,否则去除该条件)</param>
        /// <param name="DYRMC">抵押人名称</param>
        /// <param name="ZL">坐落</param>
        /// <param name="YWRMC">义务人名称</param>
        /// <param name="ZSLX">证书类型</param>
        /// <param name ="LIFE"> 数据状态 </param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        public async Task<PageStringModel> GetBdcCorrigendumInfo(string BDCDYH, string BDCZMH, string SLBH, string BDCLX, string DYRMC, string ZL, string YWRMC, string ZSLX, string LIFE, int pageIndex = 1, int pageSize = 10)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);


            };
            //RefAsync<int> totalCount = 0;
            string pageDataJson = string.Empty;
            if (BDCLX.Contains("房屋"))
            {
                pageDataJson = await base.Db.Queryable<DJ_DY, DJ_TSGL, DJ_SJD>((DY,TS,D) => TS.SLBH == DY.SLBH && DY.SLBH == D.SLBH)
              .Where((DY, TS, D) => (DY.LIFECYCLE == 0 || DY.LIFECYCLE == null) && TS.BDCLX == BDCLX
              &&DY.BDCZMH!=null&&DY.DJRQ!= null && DY.ZSXLH != null
              &&DY.BDCZMH.Contains(BDCZMH)&&DY.SLBH.Contains(SLBH)
              &&TS.BDCLX=="房屋"&&(DY.LIFECYCLE==0||DY.LIFECYCLE==null)
              &&D.ZL.Contains(ZL)
              //&& (D.ZSLX == "房屋不动产证" || D.ZSLX == "房产证")
              //&& D.DJRQ != null && D.BDCZH != null && D.ZSXLH != null)
              ).In((DY,TS,D) => TS.TSTYBM,SqlFunc.Subqueryable<FC_H_QSDC>().Where(F => F.BDCDYH.Contains(BDCDYH)).Select(S=>S.TSTYBM).ToList())
              .In((DY,TS,D)=>DY.SLBH, SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.QLRMC.Contains(DYRMC) && (F.QLRLX == "权利人"||F.QLRLX=="抵押权人")).Select(GG => GG.SLBH).ToList())
              .In((DY, TS, D) => DY.SLBH, SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.QLRMC.Contains(YWRMC) && (F.QLRLX == "义务人" || F.QLRLX == "抵押人")).Select(GG => GG.SLBH).ToList())
              .Select((DY, TS, D) => new
              {
                  SLBH= DY.SLBH,
                  QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH &&( F.QLRLX == "权利人"||F.QLRLX=="抵押权人")).Select(GG => GG.QLRMC),
                  YWRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.QLRMC.Contains(YWRMC) && (F.QLRLX == "义务人" || F.QLRLX == "抵押人")).Select(GG => GG.QLRMC),
                  BDCZH = DY.BDCZMH,
                  BDCDYH = DY.BDCDYH,
                  ZL=D.ZL,
                  ZSLX = "房屋抵押证明"
              }).OrderBy(A=>new { 
              ZL= A.ZL,
              BDCZH= A.BDCZH,
              SLBH=A.SLBH
              }).Distinct()
              .ToJsonPageAsync(pageIndex, pageSize);
            }
            else if (BDCLX.Contains("土地")|| BDCLX.Contains("宗地"))
            {
                pageDataJson = await base.Db.Queryable<DJ_DY, DJ_TSGL, DJ_SJD>((DY, TS, D) => TS.SLBH == DY.SLBH && DY.SLBH == D.SLBH)
         .Where((DY, TS, D) => (DY.LIFECYCLE == 0 || DY.LIFECYCLE == null) && TS.BDCLX == BDCLX
         && DY.BDCZMH != null && DY.DJRQ != null && DY.ZSXLH != null
         && DY.BDCZMH.Contains(BDCZMH) && DY.SLBH.Contains(SLBH)
         && TS.BDCLX == "宗地" && (DY.LIFECYCLE == 0 || DY.LIFECYCLE == null)
         && D.ZL.Contains(ZL)
         ).In((DY, TS, D) => TS.TSTYBM, SqlFunc.Subqueryable<FC_H_QSDC>().Where(F => F.BDCDYH.Contains(BDCDYH)).Select(S => S.TSTYBM).ToList())
         .In((DY, TS, D) => DY.SLBH, SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.QLRMC.Contains(DYRMC) && (F.QLRLX == "权利人" || F.QLRLX == "抵押权人")).Select(GG => GG.SLBH).ToList())
         .In((DY, TS, D) => DY.SLBH, SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.QLRMC.Contains(YWRMC) && (F.QLRLX == "义务人" || F.QLRLX == "抵押人")).Select(GG => GG.SLBH).ToList())
         .Select((DY, TS, D) => new
         {
             SLBH = DY.SLBH,
             QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && (F.QLRLX == "权利人" || F.QLRLX == "抵押权人")).Select(GG => GG.QLRMC),
             YWRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.QLRMC.Contains(YWRMC) && (F.QLRLX == "义务人" || F.QLRLX == "抵押人")).Select(GG => GG.QLRMC),
             BDCZH = DY.BDCZMH,
             BDCDYH = DY.BDCDYH,
             ZL = D.ZL,
             ZSLX = "土地抵押证明"
         }).OrderBy(A => new {
             ZL = A.ZL,
             BDCZH = A.BDCZH,
             SLBH = A.SLBH
         }).Distinct()
         .ToJsonPageAsync(pageIndex, pageSize);
            }
            else if (BDCLX.Contains("林地"))
            {

            }
            else if (BDCLX.Contains("草地"))
            {

            }
            else if (BDCLX.Contains("水域"))
            {

            }
            else if (BDCLX.Contains("宗海"))
            {

            }
            else if (BDCLX.Contains("海岛"))
            {

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
        /// 抵押勘误收件信息
        /// </summary>
        public async Task<KwHouseVModel> GetMrgeCertHouseInfo(string Dy_Slbh)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDicList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 1)).ToListAsync();
            base.ChangeDB(SysConst.DB_CON_BDC);
            KwHouseVModel dyData = new KwHouseVModel();
            var resultDY = await base.Db.Queryable<DJ_DY, DJ_SJD>((DY,  S)
=> DY.SLBH == S.SLBH)
.Where((DY,  S) => (DY.SLBH == Dy_Slbh))
.Select((DY,  S) => new
{
    BDCZH = DY.BDCZMH,
    SLBH = DY.SLBH,
    ZL = S.ZL,
    DJDL = S.DJDL,
    LIFECYCLE = DY.LIFECYCLE,
    DJYY = DY.DJYY,
    DJXL = S.DJXL,
    ZSXLH = DY.ZSXLH,
    DJRQ = DY.DJRQ,
    SJBZ = S.SJBZ

}).SingleAsync();
            if (resultDY != null)
            {
                #region 抵押信息
                dyData.selectDyHouse = new KwDyHouseVModel()
                {
                    SLBH = resultDY.SLBH,
                    BDCZMH = resultDY.BDCZH,
                    DJDL = resultDY.DJDL,
                    LIFECYCLE= resultDY.LIFECYCLE,
                    DJYY = resultDY.DJYY,
                    DJXL = resultDY.DJXL,
                    ZSXLH = resultDY.ZSXLH,
                    DJRQ = resultDY.DJRQ,
                    SJBZ = resultDY.SJBZ,
                    ZL = resultDY.ZL,

                };

                #endregion
                #region 添加抵押人和抵押权人信息
                var resultPerson = await base.Db.Queryable<DJ_QLRGL, DJ_QLR>((G, R) => G.QLRID == R.QLRID)
                    .Where((G, R) => G.SLBH == Dy_Slbh)
                    .Select((G, R) => new
                    {
                        DH = G.DH,
                        GYFE = G.GYFE,
                        QLRID = G.QLRID,
                        QLRMC = R.QLRMC,
                        SXH = R.SXH,
                        ZJHM = R.ZJHM,
                        ZJLB = R.ZJLB,
                        QLRLX = G.QLRLX
                    }).ToListAsync();
                foreach (var user in resultPerson)
                {
                    var zjlb_zwmObj = sysDicList.Where(s => s.DEFINED_CODE == user.ZJLB).FirstOrDefault();
                    if (user.QLRLX == "抵押人")
                    {
                        dyData.selectDyPerson.Add(new KwDyPersonVModel()
                        {

                            QLRMC = user.QLRMC,
                            //SXH = user.SXH,
                            ZJHM = user.ZJHM,
                            ZJLB = user.ZJLB,
                            ZJLB_ZWM = zjlb_zwmObj != null ? zjlb_zwmObj.DNAME : string.Empty,
                        });
                    }
                    else if (user.QLRLX == "抵押权人")
                    {
                        dyData.selectRightPerson.Add(new KwDyRightPersonVModel()
                        {
                            QLRMC = user.QLRMC,
                            //SXH = user.SXH,
                            ZJHM = user.ZJHM,
                            ZJLB = user.ZJLB,
                            ZJLB_ZWM = zjlb_zwmObj != null ? zjlb_zwmObj.DNAME : string.Empty,
                        });
                    }
                }
                #endregion
            }
            return dyData;
        }
        /// <summary>
        /// 查询不动产权利人
        /// </summary>
        /// <param name="djbSLBH">登记簿受理编号</param>
        /// <returns></returns>
        public async Task<List<QLR_VModel>> GetBdcQlrInfo(string[] djbSLBH)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var resultZJLB = await base.Db.Queryable<SYS_DIC>().Where(G => G.GID == 1).ToListAsync();
            var dicZjlb = resultZJLB.ToDictionary(x => x.DEFINED_CODE, x => x.DNAME);

            base.ChangeDB(SysConst.DB_CON_BDC);
            List<QLR_VModel> resultQlrList = await base.Db.Queryable<DJ_DJB, DJ_QLRGL, DJ_QLR>((D, GL, R) => D.SLBH == GL.SLBH && GL.QLRID == R.QLRID)
                .Where((D, GL, R) => (D.LIFECYCLE == 0 || D.LIFECYCLE == null) && GL.QLRLX == "权利人" && djbSLBH.Contains(D.SLBH))
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
        /// 一般抵押业务
        /// </summary>
        /// <param name="DjInfo">登记信息</param>
        /// <param name="AuzInfo">订单表</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="TsglInfo">图属信息</param>
        /// <param name="jsonData">登记信息保存暂存信息表</param>
        /// <param name="DyInfo">抵押信息</param>
        /// <param name="spInfo">审批信息表</param>
        /// <param name="XgdjglInfos">相关登记关联信息</param>
        /// <param name="qlrglInfos">权利人信息</param>
        /// <param name="uploadFiles">附件信息</param>
        /// <param name="sfd">收费单</param>
        /// <param name="sfdList">收费单明细</param>
        /// <param name="sjdInfo">收件单</param>
        /// <param name="qlxgInfo">权利相关信息</param>
        /// <param name="isInsert">是否新增</param>
        /// <param name="IsSubmitFlow">是否提交完成当前流程</param>
        /// <param name="OldXID">历史XID（仅当退回编辑时使用）</param>
        /// <returns>多表操作影响记录数之和</returns>
        public int Mortgage(BankAuthorize AuzInfo, REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, SysDataRecorderModel jsonData, List<TSGL_INFO> TsglInfo, DY_INFO DyInfo, SPB_INFO spInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, List<PUB_ATT_FILE> uploadFiles, SFD_INFO sfd, List<SFD_FB_INFO> sfdList, SJD_INFO sjdInfo, QL_XG_INFO qlxgInfo, bool isInsert, bool IsSubmitFlow, string OldXID)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            //var flowGroup = base.Db.Queryable<IFLOW_ACTION>().Where(F => F.FLOW_ID == flowInfo.FLOW_ID).Single();
            //if (flowGroup != null)
            //{
            //    DjInfo.DJZL = flowGroup.GROUP_ID;
            //}
            //else
            //{
            //    throw new ApplicationException("错误的流程节点编号:" + flowInfo.FLOW_ID);
            //}
            try
            {
                int count = 0;
                this._dbTransManagement.BeginTran();
                if (AuzInfo != null)//说明是暂存不做任何流程操作
                {
                    var bankDb = base.Db.Queryable<BankAuthorize>().Where(W => W.BID == AuzInfo.BID).Single();
                    if (bankDb != null)//为旧件
                    {
                        count += base.Db.Updateable(AuzInfo).UpdateColumns(auz => new
                        {
                            auz.STATUS,
                            auz.PRE_STATUS
                        }).Where(b => b.BID == AuzInfo.BID).ExecuteCommand();
                    }
                    else
                    {
                        if (AuzInfo != null)
                        {
                            count += base.Db.Insertable(AuzInfo).InsertColumns(bank => new
                            {
                                bank.BID,
                                bank.AUTHORIZATIONDATE,
                                bank.STATUS,
                                bank.PRE_STATUS
                            }).ExecuteCommand();
                        }
                    }
                    var flowDb = base.Db.Queryable<IFLOW_DO_ACTION>().Where(W => W.AUZ_ID == flowInfo.AUZ_ID && W.FLOW_ID == flowInfo.FLOW_ID).Single();
                    if (flowInfo == null)
                    {
                        count = base.Db.Insertable(flowInfo).InsertColumns(fw => new
                        {
                            fw.PK,
                            fw.FLOW_ID,
                            fw.PRE_FLOW_ID,
                            fw.AUZ_ID,
                            fw.CDATE,
                            fw.USER_NAME
                        }).ExecuteCommand();
                    }
                }

                bool isBack = !string.IsNullOrEmpty(OldXID);
                if (isBack)
                {
                    REGISTRATION_INFO setHistory = new REGISTRATION_INFO()
                    {
                        NEXT_XID = DjInfo.XID
                    };
                    count += base.Db.Updateable(setHistory).UpdateColumns(it => new
                    {
                        it.NEXT_XID
                    }).Where(S => S.XID == OldXID).ExecuteCommand();

                    SysDataRecorderModel historyJson = new SysDataRecorderModel()
                    {
                        IS_STOP = 1
                    };
                    count += base.Db.Updateable(historyJson).UpdateColumns(it => new
                    {
                        it.IS_STOP
                    }).Where(S => S.BUS_PK == OldXID).ExecuteCommand();

                    //更新审批表XID
                    count += base.Db.Updateable(new SPB_INFO() { XID = DjInfo.XID }).UpdateColumns(sp => new
                    {
                        sp.XID
                    }).Where(S => S.XID == OldXID).ExecuteCommand();
                    count += base.Db.Insertable(DjInfo).ExecuteCommand();
                    count += base.Db.Insertable(jsonData).ExecuteCommand();
                }

                if (isInsert)//新增操作
                {
                    //if (sfd != null)
                    //{
                    //    count = base.Db.Insertable(sfd).ExecuteCommand();
                    //}
                    count += base.Db.Insertable(DjInfo).ExecuteCommand();
                    count += base.Db.Insertable(jsonData).ExecuteCommand();
                    count += base.Db.Insertable(sjdInfo).ExecuteCommand();
                    count += base.Db.Insertable(DyInfo).ExecuteCommand();
                    if (spInfo != null)
                    {
                        count += base.Db.Insertable(spInfo).ExecuteCommand();
                    }
                }
                else
                {
                    //由于修改数据集合对象，所以先删除再进行插入操作,由于退换件产生新XID所以本次操作删除原XID对应的数据
                    string delXidArrayKey = string.IsNullOrEmpty(OldXID) ? DjInfo.XID : OldXID;
                    count += base.Db.Updateable(sjdInfo).UpdateColumns(it => new
                    {
                        it.SLBH,
                        it.LCLX,
                        it.LCMC,
                        it.ZL,
                        it.SJR,
                        it.CNSJ,
                        it.QXDM,
                        it.TZRXM,
                        it.TZRDH,
                        it.XID
                    }).Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    count += base.Db.Updateable(DjInfo).UpdateColumns(it => new
                    {
                        it.IS_ACTION_OK,
                        it.XID
                    }).Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    //if (sfd != null)
                    //{
                    //    count += base.Db.Updateable(sfd).UpdateColumns(s => new
                    //    {
                    //        s.JFBH,
                    //        s.XMMC,
                    //        s.TXDZ,
                    //        s.DH,
                    //        s.JFLX,
                    //        s.JBR,
                    //        s.JBRQ,
                    //        s.YSJE,
                    //        s.SSJE,
                    //        s.JZJF,
                    //        s.XID
                    //    }).Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    //}
                    if (spInfo != null)
                    {
                        count += base.Db.Updateable(spInfo).UpdateColumns(sp => new
                        {
                            sp.SLBH,
                            sp.SPDX,
                            sp.SPYJ,
                            sp.SPR,
                            sp.SPRQ,
                            sp.SPTXR,
                            sp.XID
                        }).Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    }
                    count += base.Db.Updateable(jsonData).UpdateColumns(js => new
                    {
                        js.SAVEDATAJSON,
                        js.BUS_PK,
                        js.REMARKS1,
                        js.REMARKS2,
                        js.REMARKS3,
                        js.REMARKS4,
                        js.REMARKS5
                    }).Where(S => S.BUS_PK == delXidArrayKey).ExecuteCommand();
                    count += base.Db.Updateable(DyInfo).UpdateColumns(it => new
                    {
                        it.SLBH,
                        it.YWSLBH,
                        it.DJLX,
                        it.DJYY,
                        it.XGZH,
                        it.SQRQ,
                        it.DYLX,
                        it.DYSW,
                        it.DYFS,
                        it.DYMJ,
                        it.BDBZZQSE,
                        it.PGJE,
                        it.HTH,
                        it.LXR,
                        it.LXRDH,
                        it.CNSJ,
                        //it.SJR,
                        it.FJ,
                        //it.ZWR,
                        //it.ZWRZJH,
                        //it.ZWRZJLX,
                        it.DLJGMC,
                        it.QLQSSJ,
                        it.QLJSSJ,
                        it.DYQX,
                        it.XID
                    }).Where(S => S.XID == delXidArrayKey).ExecuteCommand();

                    
                    count += base.Db.Deleteable<TSGL_INFO>().Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    count += base.Db.Deleteable<SFD_FB_INFO>().Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    count += base.Db.Deleteable<XGDJGL_INFO>().Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    count += base.Db.Deleteable<QLRGL_INFO>().Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    count += base.Db.Deleteable<QL_XG_INFO>().Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    
                    if (uploadFiles != null && uploadFiles.Count > 0)
                    {
                        base.Db.Deleteable<PUB_ATT_FILE>().Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    }
                }
                if (qlxgInfo != null)
                {
                    count += base.Db.Insertable(qlxgInfo).ExecuteCommand();
                }
                if (TsglInfo != null && TsglInfo.Count > 0)
                {
                    count += base.Db.Insertable(TsglInfo.ToArray()).ExecuteCommand();
                }
                //if (sfdList != null && sfdList.Count > 0)
                //{
                //    count += base.Db.Insertable(sfdList.ToArray()).ExecuteCommand();
                //}
                if (XgdjglInfos.Count > 0)
                {
                    count += base.Db.Insertable(XgdjglInfos.ToArray()).ExecuteCommand();
                }
                if (qlrglInfos.Count > 0)
                {
                    count += base.Db.Insertable(qlrglInfos.ToArray()).ExecuteCommand();
                }
                if (uploadFiles != null && uploadFiles.Count > 0)
                {
                    count += base.Db.Insertable(uploadFiles.ToArray()).ExecuteReturnIdentity();
                }
                this._dbTransManagement.CommitTran();
                return count;
            }
            catch (Exception ex)
            {
                this._dbTransManagement.RollbackTran();
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        /// <summary>
        /// 查询附件信息
        /// </summary>
        /// <param name="XID">业务信息表XID</param>
        /// <returns></returns>
        public async Task<List<PUB_ATT_FILE>> UploadFileQueryByXID(string XID)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            
            return await base.Db.Queryable<PUB_ATT_FILE>().Where(it => it.XID == XID).ToListAsync();
        }

        /// <summary>
        /// 修改该受理编号下文件信息
        /// </summary>
        /// <param name="uploadFiles">文件列表</param>
        /// <param name="xid">业务流程主键编号</param>
        /// <returns></returns>
        public async Task<int> UpdateFile(List<PUB_ATT_FILE> uploadFiles, string xid)
        {
            try
            {
                this._dbTransManagement.BeginTran();
                int count = 0;
                await base.Db.Deleteable<PUB_ATT_FILE>().Where(w => w.XID == xid).ExecuteCommandAsync();
                foreach (var file in uploadFiles)
                {
                    file.XID = xid;
                }
                count = await base.Db.Insertable(uploadFiles.ToArray()).ExecuteCommandAsync();
                //if (uploadFiles == null)//如果上传文件为空则客户端清空所以上传文件
                //{
                //    await base.Db.Deleteable<PUB_ATT_FILE>().Where(w => w.XID == xid).ExecuteCommandAsync();
                //}
                //else
                //{
                //    count = await base.Db.Deleteable<PUB_ATT_FILE>().In(it => it.XID, uploadFiles.Cast<PUB_ATT_FILE>().Select(s => s.BUS_PK).ToArray()).ExecuteCommandAsync();
                //    count = await base.Db.Insertable(uploadFiles.ToArray()).ExecuteReturnIdentityAsync();
                //}
                this._dbTransManagement.CommitTran();
                return count;
            }
            catch (Exception ex)
            {
                this._dbTransManagement.RollbackTran();
                throw ex;
            }
        }

    }
}
