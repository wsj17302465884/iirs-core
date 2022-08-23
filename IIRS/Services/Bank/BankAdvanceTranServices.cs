using IIRS.IRepository.Base;
using IIRS.IServices.Bank;
using IIRS.Models.EntityModel.BDC;
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
    public class BankAdvanceTranServices:BaseServices, IBankAdvanceTranServices
    {
        private readonly ILogger<BankAdvanceTranServices> _logger;

        IDBTransManagement _dbTransManagement;
        public BankAdvanceTranServices(IDBTransManagement dbTransManagement, ILogger<BankAdvanceTranServices> logger) : base(dbTransManagement)
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
        /// <returns></returns>
        public async Task<List<AdvanceVModel>> GetBdcHourseInfo(string XM, string ZJHM, string BDCZH, string BDCLX)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);

            //RefAsync<int> totalCount = 0;
            List<AdvanceVModel> Data = null;
            string ZJHM_15 = string.Empty;
            if (ZJHM.Length == 18)//证件号码如果为18位，视为身份证，并转换15位老身份证进行业务查询
            {
                ZJHM_15 = ZJHM.Substring(0, 6) + ZJHM.Substring(8, 9);
            }
            if (BDCLX == "房屋")
            {
                base.Db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    _logger.LogDebug(sql);
                };
                Data = await base.Db.Queryable<DJ_TSGL, DJ_YG, DJ_SJD, FC_H_QSDC, FC_Z_QSDC>((TS, D, SD, FC, FZ) => TS.SLBH == D.SLBH && D.SLBH == SD.SLBH && TS.TSTYBM == FC.TSTYBM && FC.LSZTYBM == FZ.TSTYBM)
                .Where((TS, D, SD, FC, FZ) => (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null) && TS.BDCLX == BDCLX
                && D.BDCZMH == BDCZH && SqlFunc.Subqueryable<V_DJ_QLRGL_ListModel>().Where(G => D.SLBH == G.SLBH
                && G.QLRMC == XM && G.QLRLX == "权利人" && (G.ZJHM == ZJHM || G.ZJHM == ZJHM_15)).Any()
                && D.DJRQ != null && D.BDCZMH != null && D.ZSXLH != null)
                .GroupBy((TS, D, SD, FC, FZ) => new { FZ.FWJG, FZ.ZCS, FC.HZCS, FC.MYC, D.FJ, TS.TSTYBM, TS.SLBH, SD.DJDL, D.BDCZMH, D.BDCDYH, D.DJRQ, SD.ZL, TS.BDCLX })
                .Select((TS, D, SD, FC, FZ) => new AdvanceVModel
                {
                    rn = Convert.ToInt32(SqlFunc.MappingColumn(TS.TSTYBM, "ROW_NUMBER() OVER(ORDER BY SYSDATE)")),
                    tstybm = TS.TSTYBM,
                    slbh = TS.SLBH,
                    djdl = SD.DJDL,
                    bdczh = D.BDCZMH,
                    bdcdyh = D.BDCDYH,
                    djrq = D.DJRQ,
                    qlrmc = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.QLRMC),
                    zjhm = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.ZJHM),
                    zl = SD.ZL,
                    bdclx = TS.BDCLX,
                    fwmj = SqlFunc.Subqueryable<QL_FWXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.AggregateMax(s.JZMJ.ToString())),
                    //TDMJ = SqlFunc.Subqueryable<QL_TDXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.IsNull(SqlFunc.AggregateMax(s.DYTDMJ.ToString()), SqlFunc.AggregateMax(s.GYTDMJ.ToString()))),
                    tdmj = "",
                    zt = SqlFunc.MappingColumn(TS.DJZL, "(SELECT WM_CONCAT(DISTINCT TO_CHAR(TS2.DJZL)) FROM DJ_TSGL TS2 WHERE TS2.TSTYBM = TS.TSTYBM AND TS2.DJZL NOT IN ('异议注销'，'查封注销'，'抵押注销'，'预告注销') AND ((TS2.LIFECYCLE = 0) OR (TS2.LIFECYCLE IS NULL)))"),
                    //ZT = SqlFunc.MappingColumn(TS.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(TS.DJZL))"),
                    djb_fj = D.FJ,
                    szc = FC.MYC,
                    hzcs = FC.HZCS,
                    fwjg = FZ.FWJG,
                    fwjg_zwm = SqlFunc.Subqueryable<T_ENUM_FC_FWJG_EModel>().Where(F => F.FWJGID == FZ.FWJG).Select(s => SqlFunc.AggregateMax(s.FWJGQC)),
                    zzcs = FZ.ZCS
                }).ToListAsync();
            }
            else
            {
                Data = await base.Db.Queryable<DJ_YG, DJ_SJD, DJ_TSGL, ZD_QSDC>((D, SD, TS, ZD) => new object[] { JoinType.Inner, D.SLBH == SD.SLBH, JoinType.Inner, TS.SLBH == SD.SLBH, JoinType.Left, ZD.TSTYBM == TS.TSTYBM })
                .Where((D, SD, TS, ZD) => (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null) && TS.BDCLX == BDCLX
                && D.BDCZMH == BDCZH && SqlFunc.Subqueryable<V_DJ_QLRGL_ListModel>().Where(G => D.SLBH == G.SLBH
                && G.QLRMC == XM && G.QLRLX == "权利人" && (G.ZJHM == ZJHM || G.ZJHM == ZJHM_15)).Any()
                && (ZD.LIFECYCLE == 0 || ZD.LIFECYCLE == null) && (D.LIFECYCLE == 0 || D.LIFECYCLE == null)
                && D.DJRQ != null && D.BDCZMH != null && D.ZSXLH != null)
                .GroupBy((D, SD, TS, ZD) => new { D.FJ, TS.TSTYBM, TS.SLBH, SD.DJDL, D.BDCZMH, D.BDCDYH, D.DJRQ, SD.ZL, TS.BDCLX })
                .Select((D, SD, TS, ZD) => new AdvanceVModel
                {
                    rn = Convert.ToInt32(SqlFunc.MappingColumn(TS.TSTYBM, "ROW_NUMBER() OVER(ORDER BY SYSDATE)")),
                    tstybm = TS.TSTYBM,
                    slbh = TS.SLBH,
                    djdl = SD.DJDL,
                    bdczh = D.BDCZMH,
                    bdcdyh = D.BDCDYH,
                    djrq = D.DJRQ,
                    qlrmc = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.QLRMC),
                    zjhm = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == TS.SLBH && F.QLRLX == "权利人").Select(GG => GG.ZJHM),
                    //QLRMC = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'权利人',R.QLRMC,'')))"),
                    //ZJHM = SqlFunc.MappingColumn(R.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(DECODE(GL.QLRLX,'权利人',R.ZJHM,'')))"),
                    zl = SD.ZL,
                    bdclx = TS.BDCLX,
                    fwmj = "",
                    tdmj = SqlFunc.Subqueryable<QL_TDXG>().Where(F => F.SLBH == TS.SLBH).Select(s => SqlFunc.IsNull(SqlFunc.AggregateMax(s.DYTDMJ.ToString()), SqlFunc.AggregateMax(s.GYTDMJ.ToString()))),
                    zt = SqlFunc.MappingColumn(TS.DJZL, "(SELECT WM_CONCAT(DISTINCT TO_CHAR(TS2.DJZL)) FROM DJ_TSGL TS2 WHERE TS2.TSTYBM = TS.TSTYBM AND TS2.DJZL NOT IN ('异议注销'，'查封注销'，'抵押注销'，'预告注销') AND ((TS2.LIFECYCLE = 0) OR (TS2.LIFECYCLE IS NULL)))"),
                    //ZT = SqlFunc.MappingColumn(TS.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(TS.DJZL))"),
                    djb_fj = D.FJ,
                    szc = "-100",
                    hzcs = "-1",
                    fwjg = "-1",
                    fwjg_zwm = "",
                    zzcs = "-1"
                }).ToListAsync();
            }
            for (int i = 0; i < Data.Count; i++)
            {
                int k = i+1;
                if (Data[i].slbh != null)
                {
                    var childrenData = await base.Db.Queryable<DJ_DJB, DJ_XGDJGL>((A, B) => A.SLBH == B.FSLBH).Where((A, B) => (B.XGZLX == "房屋不动产证" || B.XGZLX == "房产证") && (A.LIFECYCLE == 0 || A.LIFECYCLE == null) && B.ZSLBH == Data[i].slbh)
                        .Select((A, B) => new AdvanceVModel
                        {
                            bdczh = A.BDCZH,
                            bdclx = A.ZSLX,
                            zl = Data[i].zl,
                            qlrmc = Data[i].qlrmc,
                            slbh = B.FSLBH,
                            djrq = B.BGRQ
                        }).ToListAsync();

                    if (childrenData.Count > 0)
                    {
                        Data[i].hasChildren = true;
                        int j = 1;
                        foreach (var childItem in childrenData)
                        {                            
                            childItem.rn = k * 10 + j;
                            Data[i].children.Add(childItem);
                            j++;
                        }
                    }
                }
            }




            return Data;
        }

        /// <summary>
        /// 查询预告下是否有房产
        /// </summary>
        /// <param name="slbh">预告受理编码</param>
        /// <returns></returns>
        public async Task<List<DJ_XGDJGL>> GetYgBdczhInfo(string slbh)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            List<SugarParameter> whereParams = new List<SugarParameter>();
            whereParams.Add(new SugarParameter(":slbh", slbh));
            #region SQL
            string sql = string.Format(@"SELECT ROW_NUMBER() OVER(ORDER BY SYSDATE) RN, A.SLBH,A.BDCZH,A.ZSLX FROM DJ_DJB A,DJ_XGDJGL B WHERE A.SLBH = B.FSLBH AND (B.XGZLX = '房屋不动产证' OR B.XGZLX = '房产证') AND (A.LIFECYCLE IS NULL OR A.LIFECYCLE !='1') AND B.ZSLBH = :slbh");
            #endregion
            var data = await base.Db.Ado.SqlQueryAsync<DJ_XGDJGL>(sql, whereParams);
            return data;
        }
    }
}
