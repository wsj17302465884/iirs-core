using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.ViewModel;
using IIRS.Services.Base;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class ImmovablesRelationQueryServices : BaseServices, IImmovablesRelationQueryServices
    {
        private readonly ILogger<ImmovablesRelationQueryServices> _logger;
        public ImmovablesRelationQueryServices(IDBTransManagement dbTransManagement, ILogger<ImmovablesRelationQueryServices> logger) : base(dbTransManagement)
        {
            this._logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="BDCDYH">不动产单元号</param>
        /// <param name="ZSLX">证书类型</param>
        /// <param name="BDCZH">不动产权证书号</param>
        /// <param name="SLBH">登记受理编号</param>
        /// <param name="DY_QLR">权利人</param>
        /// <param name="ZL">坐落</param>
        /// <param name="PageIndex">分页页面</param>
        /// <param name="PageSize">每页行数</param>
        /// <param name="isComputeCount">是否计算总数据量</param>
        /// <returns></returns>
        public async Task<PageModel<ImmovablesRelationVModel>> ImmovablesRelationQuery(string BDCDYH, int ZSLX, string BDCZH, string SLBH, string DY_QLR, string ZL, int PageIndex, int PageSize, bool isComputeCount)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_BDC);
            int rowCount = 0;
            int pageNum = 0;
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};

            string[] pk = null;
            if (isComputeCount)
            {
                RefAsync<int> countAll = 0;
                var result = base.Db.Queryable<DJ_TSGL, DJ_DJB, DJ_YG, DJ_YY, DJ_SJD, DJ_QLRGL, DJ_QLR>((C, DJ, YG, YY, B, D, A) => new object[]{ JoinType.Left, C.SLBH == DJ.SLBH,JoinType.Left, C.SLBH == YG.SLBH,JoinType.Left, C.SLBH == YY.SLBH
                 ,JoinType.Inner, C.SLBH == B.SLBH
                 ,JoinType.Inner, D.SLBH == B.SLBH
                 ,JoinType.Inner, A.QLRID == D.QLRID})
.Where((C, DJ, YG, YY, B, D, A) => (C.LIFECYCLE == null || C.LIFECYCLE == 0) && (D.LIFECYCLE == null || D.LIFECYCLE == 0))
.WhereIF(!string.IsNullOrEmpty(DY_QLR), (C, DJ, YG, YY, B, D, A) => D.QLRLX == "权利人" && A.QLRMC.Contains(DY_QLR))
.WhereIF(!string.IsNullOrEmpty(ZL), (C, DJ, YG, YY, B, D, A) => B.ZL.Contains(ZL))
.WhereIF(!string.IsNullOrEmpty(BDCZH), (C, DJ, YG, YY, B, D, A) => DJ.BDCZH == BDCZH)
.WhereIF(!string.IsNullOrEmpty(BDCDYH), (C, DJ, YG, YY, B, D, A) => DJ.BDCDYH == BDCDYH)
.WhereIF(!string.IsNullOrEmpty(SLBH), (C, DJ, YG, YY, B, D, A) => DJ.SLBH == SLBH)
.Select((C) => C.TSTYBM).GroupBy("C.TSTYBM,C.SLBH").OrderBy("C.SLBH").ToPageListAsync(PageIndex, PageSize, countAll).Result;
                //        var result = (base.Db.Queryable<DJ_TSGL, DJ_DJB, DJ_YG, DJ_YY, DJ_SJD, DJ_QLRGL, DJ_QLR>((C, DJ, YG, YY, B, D, A) => new object[]{ JoinType.Left, C.SLBH == DJ.SLBH,JoinType.Left, C.SLBH == YG.SLBH,JoinType.Left, C.SLBH == YY.SLBH
                //         ,JoinType.Inner, C.SLBH == B.SLBH
                //         ,JoinType.Inner, D.SLBH == B.SLBH
                //         ,JoinType.Inner, A.QLRID == D.QLRID})
                //.Where((C, DJ, YG, YY, B, D, A) => (C.LIFECYCLE == null || C.LIFECYCLE == 0) && (D.LIFECYCLE == null || D.LIFECYCLE == 0))
                //.WhereIF(!string.IsNullOrEmpty(DY_QLR), (C, DJ, YG, YY, B, D, A) => D.QLRLX == "权利人" && A.QLRMC.Contains(DY_QLR))
                //.WhereIF(!string.IsNullOrEmpty(ZL), (C, DJ, YG, YY, B, D, A) => B.ZL.Contains(ZL))
                //.WhereIF(!string.IsNullOrEmpty(BDCZH), (C, DJ, YG, YY, B, D, A) => DJ.BDCZH == BDCZH)
                //.WhereIF(!string.IsNullOrEmpty(BDCDYH), (C, DJ, YG, YY, B, D, A) => DJ.BDCDYH == BDCDYH)
                //.WhereIF(!string.IsNullOrEmpty(SLBH), (C, DJ, YG, YY, B, D, A) => DJ.SLBH == SLBH)
                //.Select((C) => C.TSTYBM).OrderBy("C.SLBH").Distinct()).ToPageListAsync(PageIndex, PageSize, countAll).Result;
                rowCount = countAll.Value;
                pageNum = countAll.Value / PageSize + (countAll.Value % PageSize > 0 ? 1 : 0);
                pk = result.ToArray();
            }
            else
            {
                var result = base.Db.Queryable<DJ_TSGL, DJ_DJB, DJ_YG, DJ_YY, DJ_SJD, DJ_QLRGL, DJ_QLR>((C, DJ, YG, YY, B, D, A) => new object[]{ JoinType.Left, C.SLBH == DJ.SLBH,JoinType.Left, C.SLBH == YG.SLBH,JoinType.Left, C.SLBH == YY.SLBH
                 ,JoinType.Inner, C.SLBH == B.SLBH
                 ,JoinType.Inner, D.SLBH == B.SLBH
                 ,JoinType.Inner, A.QLRID == D.QLRID})
        .Where((C, DJ, YG, YY, B, D, A) => (C.LIFECYCLE == null || C.LIFECYCLE == 0) && (D.LIFECYCLE == null || D.LIFECYCLE == 0))
        .WhereIF(!string.IsNullOrEmpty(DY_QLR), (C, DJ, YG, YY, B, D, A) => D.QLRLX == "权利人" && A.QLRMC.Contains(DY_QLR))
        .WhereIF(!string.IsNullOrEmpty(ZL), (C, DJ, YG, YY, B, D, A) => B.ZL.Contains(ZL))
        .WhereIF(!string.IsNullOrEmpty(BDCZH), (C, DJ, YG, YY, B, D, A) => DJ.BDCZH == BDCZH)
        .WhereIF(!string.IsNullOrEmpty(BDCDYH), (C, DJ, YG, YY, B, D, A) => DJ.BDCDYH == BDCDYH)
        .WhereIF(!string.IsNullOrEmpty(SLBH), (C, DJ, YG, YY, B, D, A) => DJ.SLBH == SLBH)
        .Select((C) => C.TSTYBM).GroupBy("C.TSTYBM,C.SLBH").OrderBy("C.SLBH").ToPageListAsync(PageIndex, PageSize).Result;
                pk = result.ToArray();
            }
            var resultTS = await base.Db.Queryable<HouseStatusViewModel>().Where(it => pk.Contains(it.Tstybm)).ToListAsync();
            List<ImmovablesRelationVModel> modelList = new List<ImmovablesRelationVModel>();
            foreach (var tsMsg in resultTS.ToArray())
            {
                modelList.Add(new ImmovablesRelationVModel()
                {
                    Tstybm = tsMsg.Tstybm,
                    Slbh = tsMsg.Slbh,
                    Bdczh = tsMsg.Bdczh,
                    //DJRQ = Convert.ToDateTime(tsMsg.DjbDjrq) ?? tsMsg.YgDjrq,
                    DJZL = tsMsg.Djzl,
                    QLR = tsMsg.Qlrmc ?? tsMsg.Yg_Qlrmc ?? tsMsg.Yy_Qlrmc,
                    YWR = tsMsg.Ywr,
                    DJLX = string.IsNullOrEmpty(tsMsg.YG_Slbh) ? "房屋预告证明" : tsMsg.DJBZSLX ?? "房屋不动产证"
                });
            }

            return new PageModel<ImmovablesRelationVModel>() { dataCount = rowCount, pageCount = pageNum, page = PageIndex, PageSize = PageSize, data = modelList };

            //return new PageModel<dynamic>() { dataCount = 1000, pageCount = 30, page = PageIndex, PageSize = PageSize, data = aaa as List<dynamic>() };
            //Queryable <HouseStatusViewModel>()
            //var data = await _mortgageServices.Query<DJ_TSGL, DJ_QLRGL, MortgageViewModel>(joinExpression, selectExpression, whereExpression);
            //var data = await base.Query<DJ_TSGL, DJ_QLRGL, MortgageViewModel>(_joinExpression, _selectExpression, _whereExpression);

            //return data;
            //           SELECT C.TSTYBM,d.zl
            // FROM DJ_TSGL C LEFT JOIN DJ_DJB DJ ON C.SLBH = DJ.SLBH
            // LEFT JOIN DJ_YG YG ON C.SLBH = YG.SLBH
            // , DJ_QLR A, DJ_QLRGL B, dj_sjd d
            //WHERE A.QLRID = B.QLRID and d.slbh = b.slbh
            //  AND C.SLBH = B.SLBH
            //  AND(C.LIFECYCLE IS NULL OR C.LIFECYCLE = 0)
            //  AND(B.LIFECYCLE IS NULL OR B.LIFECYCLE = 0)
            //  AND B.QLRLX = '权利人' AND A.QLRMC LIKE '%张%'
            //  AND D.ZL LIKE '%白塔区%'--坐落
            // AND DJ.BDCZH =:BDCZH--不动产证号
            // AND YG.BDCZMH =:BDCZH--不动产证号

            // AND C.BDCDYH LIKE '%%'--不动产单元号
            //AND DJ.SLBH =:SLBH--受理编号


            //var dt2 = base.Db.Ado.UseStoredProcedure().GetDataTable("sp_school", nameP, ageP);


            //            #region SQL语句方式
            //            string sqlWhere = string.Empty;
            //            List<SugarParameter> whereParams = new List<SugarParameter>();
            //            if (!string.IsNullOrEmpty(BDCDYH))
            //            {
            //                sqlWhere += " AND DJ.BDCDYH LIKE '%'||:BDCDYH|| '%'";
            //                whereParams.Add(new SugarParameter(":BDCDYH", BDCDYH));
            //            }
            //            if (!string.IsNullOrEmpty(SLBH))
            //            {
            //                sqlWhere += " AND DJ.SLBH=:SLBH";
            //                whereParams.Add(new SugarParameter(":SLBH", SLBH));
            //            }
            //            if (!string.IsNullOrEmpty(BDCZH))
            //            {
            //                if (ZSLX == 1)//不动产证号
            //                {
            //                    sqlWhere += " AND DJ.BDCZH=:BDCZH";
            //                }
            //                else
            //                {
            //                    sqlWhere += " AND YG.BDCZMH=:BDCZH";
            //                }
            //                whereParams.Add(new SugarParameter(":BDCZH", BDCZH));
            //            }

            //            if (!string.IsNullOrEmpty(DY_QLR))
            //            {
            //                sqlWhere += " AND (QLR.QLRMC LIKE '%'||:DY_QLR|| '%' OR YGQLR.QLRMC LIKE '%'||:DY_QLR|| '%' OR YYQLR.QLRMC LIKE '%'||:DY_QLR|| '%') ";
            //                whereParams.Add(new SugarParameter(":DY_QLR", DY_QLR));
            //            }
            //            if (!string.IsNullOrEmpty(ZL))
            //            {
            //                sqlWhere += " AND SJD.ZL LIKE '%'||:ZL|| '%'";
            //                whereParams.Add(new SugarParameter(":ZL", ZL));
            //            }
            //            int count = 0;
            //            if (isComputeCount)
            //            {
            //                string sqlCount = $@"SELECT COUNT(1) FROM (SELECT ROWNUM RN,BDCZH,SLBH,ZL,YGQT,NVL(NVL(QLRMC,YG_QLRMC),YY_QLRMC) QLR
            //,NVL(NVL(YWR,YG_YWR),YY_YWR) YWR FROM (
            //SELECT M.TSTYBM ,
            //       MAX(DJ.BDCZH) BDCZH ,
            //       MAX(DJ.SLBH) SLBH,
            //       MAX(SJD.ZL) ZL,
            //       FC.JZMJ,
            //       MAX(YG.QT) YGQT,
            //       WM_CONCAT(DISTINCT TO_CHAR(M.DJZL)) DJZL,
            //       LISTAGG(CASE WHEN QLRGL.QLRLX = '权利人' THEN TO_CHAR(QLR.QLRMC) ELSE NULL END,',') WITHIN GROUP(ORDER BY QLRGL.SXH ASC) QLRMC,
            //       LISTAGG(CASE WHEN QLRGL.QLRLX = '义务人' THEN TO_CHAR(QLR.QLRMC) ELSE NULL END,',') WITHIN GROUP(ORDER BY QLRGL.SXH ASC) YWR,
            //       LISTAGG(CASE WHEN YGQLRGL.QLRLX = '权利人' THEN TO_CHAR(YGQLR.QLRMC) ELSE NULL END,',') WITHIN GROUP(ORDER BY YGQLRGL.SXH ASC) YG_QLRMC,
            //       LISTAGG(CASE WHEN YGQLRGL.QLRLX = '义务人' THEN TO_CHAR(YGQLR.QLRMC) ELSE NULL END,',') WITHIN GROUP(ORDER BY YGQLRGL.SXH ASC) YG_YWR,
            //       LISTAGG(CASE WHEN YYQLRGL.QLRLX = '权利人' THEN TO_CHAR(YYQLR.QLRMC) ELSE NULL END,',') WITHIN GROUP(ORDER BY YYQLRGL.SXH ASC) YY_QLRMC,
            //       LISTAGG(CASE WHEN YYQLRGL.QLRLX = '义务人' THEN TO_CHAR(YYQLR.QLRMC) ELSE NULL END,',') WITHIN GROUP(ORDER BY YYQLRGL.SXH ASC) YY_YWR
            //  FROM DJ_TSGL M
            //  LEFT JOIN DJ_CF CF ON M.SLBH = CF.SLBH
            //  LEFT JOIN DJ_YY YY ON M.SLBH = YY.SLBH
            //  LEFT JOIN DJ_QLRGL YYQLRGL ON YY.SLBH = YYQLRGL.SLBH
            //  LEFT JOIN DJ_QLR YYQLR ON YYQLRGL.QLRID = YYQLR.QLRID
            //  LEFT JOIN DJ_DY DY ON M.SLBH = DY.SLBH
            //  LEFT JOIN DJ_QLRGL DYQLRGL ON DY.SLBH = DYQLRGL.SLBH
            //  LEFT JOIN DJ_QLR DYQLR ON DYQLRGL.QLRID = DYQLR.QLRID
            //  LEFT JOIN DJ_YG YG ON M.SLBH = YG.SLBH
            //  LEFT JOIN DJ_QLRGL YGQLRGL ON YG.SLBH = YGQLRGL.SLBH
            //  LEFT JOIN DJ_QLR YGQLR ON YGQLRGL.QLRID = YGQLR.QLRID
            //  LEFT JOIN DJ_DJB DJ ON M.SLBH = DJ.SLBH
            //  LEFT JOIN DJ_QLRGL QLRGL ON DJ.SLBH = QLRGL.SLBH
            //  LEFT JOIN DJ_QLR QLR ON QLRGL.QLRID = QLR.QLRID
            //  LEFT JOIN QL_FWXG FWXG ON FWXG.SLBH = DJ.SLBH
            //  LEFT JOIN DJ_SJD SJD ON SJD.SLBH = DJ.SLBH
            //  LEFT JOIN QL_TDXG TDXG ON TDXG.SLBH = FWXG.SLBH, FC_H_QSDC FC
            // WHERE FC.TSTYBM = M.TSTYBM AND (M.LIFECYCLE = 0 OR M.LIFECYCLE IS NULL) {sqlWhere}
            // GROUP BY M.TSTYBM, FC.JZMJ))";
            //                count = await base.Db.Ado.SqlQuerySingleAsync<int>(sqlCount, whereParams);
            //            }
            //            whereParams.Add(new SugarParameter(":PageIndex", PageIndex));
            //            whereParams.Add(new SugarParameter(":PageSize", PageSize));
            //            string sql = $@"SELECT * FROM (SELECT ROWNUM RN,BDCZH,SLBH,ZL,YGQT,NVL(NVL(QLRMC,YG_QLRMC),YY_QLRMC) QLR
            //,NVL(NVL(YWR,YG_YWR),YY_YWR) YWR FROM (
            //SELECT M.TSTYBM ,
            //       MAX(DJ.BDCZH) BDCZH ,
            //       MAX(DJ.SLBH) SLBH,
            //       MAX(SJD.ZL) ZL,
            //       FC.JZMJ,
            //       MAX(YG.QT) YGQT,
            //       WM_CONCAT(DISTINCT TO_CHAR(M.DJZL)) DJZL,
            //       LISTAGG(CASE WHEN QLRGL.QLRLX = '权利人' THEN TO_CHAR(QLR.QLRMC) ELSE NULL END,',') WITHIN GROUP(ORDER BY QLRGL.SXH ASC) QLRMC,
            //       LISTAGG(CASE WHEN QLRGL.QLRLX = '义务人' THEN TO_CHAR(QLR.QLRMC) ELSE NULL END,',') WITHIN GROUP(ORDER BY QLRGL.SXH ASC) YWR,
            //       LISTAGG(CASE WHEN YGQLRGL.QLRLX = '权利人' THEN TO_CHAR(YGQLR.QLRMC) ELSE NULL END,',') WITHIN GROUP(ORDER BY YGQLRGL.SXH ASC) YG_QLRMC,
            //       LISTAGG(CASE WHEN YGQLRGL.QLRLX = '义务人' THEN TO_CHAR(YGQLR.QLRMC) ELSE NULL END,',') WITHIN GROUP(ORDER BY YGQLRGL.SXH ASC) YG_YWR,
            //       LISTAGG(CASE WHEN YYQLRGL.QLRLX = '权利人' THEN TO_CHAR(YYQLR.QLRMC) ELSE NULL END,',') WITHIN GROUP(ORDER BY YYQLRGL.SXH ASC) YY_QLRMC,
            //       LISTAGG(CASE WHEN YYQLRGL.QLRLX = '义务人' THEN TO_CHAR(YYQLR.QLRMC) ELSE NULL END,',') WITHIN GROUP(ORDER BY YYQLRGL.SXH ASC) YY_YWR
            //  FROM DJ_TSGL M
            //  LEFT JOIN DJ_CF CF ON M.SLBH = CF.SLBH
            //  LEFT JOIN DJ_YY YY ON M.SLBH = YY.SLBH
            //  LEFT JOIN DJ_QLRGL YYQLRGL ON YY.SLBH = YYQLRGL.SLBH
            //  LEFT JOIN DJ_QLR YYQLR ON YYQLRGL.QLRID = YYQLR.QLRID
            //  LEFT JOIN DJ_DY DY ON M.SLBH = DY.SLBH
            //  LEFT JOIN DJ_QLRGL DYQLRGL ON DY.SLBH = DYQLRGL.SLBH
            //  LEFT JOIN DJ_QLR DYQLR ON DYQLRGL.QLRID = DYQLR.QLRID
            //  LEFT JOIN DJ_YG YG ON M.SLBH = YG.SLBH
            //  LEFT JOIN DJ_QLRGL YGQLRGL ON YG.SLBH = YGQLRGL.SLBH
            //  LEFT JOIN DJ_QLR YGQLR ON YGQLRGL.QLRID = YGQLR.QLRID
            //  LEFT JOIN DJ_DJB DJ ON M.SLBH = DJ.SLBH
            //  LEFT JOIN DJ_QLRGL QLRGL ON DJ.SLBH = QLRGL.SLBH
            //  LEFT JOIN DJ_QLR QLR ON QLRGL.QLRID = QLR.QLRID
            //  LEFT JOIN QL_FWXG FWXG ON FWXG.SLBH = DJ.SLBH
            //  LEFT JOIN DJ_SJD SJD ON SJD.SLBH = DJ.SLBH
            //  LEFT JOIN QL_TDXG TDXG ON TDXG.SLBH = FWXG.SLBH, FC_H_QSDC FC
            // WHERE FC.TSTYBM = M.TSTYBM AND (M.LIFECYCLE = 0 OR M.LIFECYCLE IS NULL) {sqlWhere}
            // GROUP BY M.TSTYBM, FC.JZMJ)) WHERE RN BETWEEN (:PageIndex - 1) * :PageSize + 1 AND :PageIndex * :PageSize";
            //            var dataList = await base.Db.Ado.SqlQueryAsync<dynamic>(sql, whereParams);
            //            return new PageModel<dynamic>() { dataCount = count, pageCount = 30, page = PageIndex, PageSize = PageSize, data = dataList };
            //            #endregion
        }
    }
}
