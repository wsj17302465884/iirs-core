using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using IIRS.Models.ViewModel.BDC.judgment;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class BusinessJudgmentServices : BaseServices, IBusinessJudgmentServices
    {
        private readonly ILogger<BusinessJudgmentServices> _logger;
        public BusinessJudgmentServices(IDBTransManagement dbTransManagement, ILogger<BusinessJudgmentServices> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }        
        /// <summary>
        /// 判断是否包含查封抵押异议
        /// </summary>
        /// <param name="yw_slbh">当前业务SLBH</param>
        /// <param name="dj_slbh">登记SLBH</param>
        /// <returns></returns>
        public async Task<List<JudgmentMortgage1>> GetBusinessJudgment1(string yw_slbh, string dj_slbh)
        {
            //yw_slbh = "1";
            //dj_slbh = "TDTD-DNW1-001-T";
            base.ChangeDB(SysConst.DB_CON_BDC);

            List<JudgmentMortgage1> modelList = new List<JudgmentMortgage1>();

            #region SQL
            List<SugarParameter> whereParams = new List<SugarParameter>();
            whereParams.Add(new SugarParameter(":yw_slbh", yw_slbh));
            whereParams.Add(new SugarParameter(":dj_slbh", dj_slbh));
            string sql = string.Format(@"SELECT SLBH, BDCZH, '已经注销' type
  FROM DJ_DJB
 WHERE SLBH IN (:dj_slbh)
   AND LIFECYCLE = 1
   AND SLBH NOT LIKE ':yw_slbh%'
UNION ALL
SELECT SLBH, BDCZMH AS BDCZH, '已经注销' type
  FROM DJ_DY
 WHERE SLBH IN (:dj_slbh)
   AND LIFECYCLE = 1
   AND SLBH NOT LIKE ':yw_slbh%'
UNION ALL
SELECT SLBH, BDCZMH AS BDCZH, '已经注销' type
  FROM DJ_YG
 WHERE SLBH IN (:dj_slbh)
   AND LIFECYCLE = 1
   AND SLBH NOT LIKE ':yw_slbh%'
UNION ALL
SELECT SLBH, BDCZMH AS BDCZH, '已经注销' type
  FROM DJ_YY
 WHERE SLBH IN (:dj_slbh)
   AND LIFECYCLE = 1
   AND SLBH NOT LIKE ':yw_slbh%'
UNION ALL
SELECT SLBH, BDCZMH AS BDCZH, '已经注销' type
  FROM DJ_DYQ
 WHERE SLBH IN (:dj_slbh)
   AND LIFECYCLE = 1
   AND SLBH NOT LIKE ':yw_slbh%'
UNION ALL
SELECT SLBH, N'' AS BDCZH, '已经注销' type
  FROM DJ_CF
 WHERE SLBH IN (:dj_slbh)
   AND LIFECYCLE = 1
   AND SLBH NOT LIKE ':yw_slbh%'
UNION ALL
SELECT DISTINCT B.FSLBH AS SLBH, B.XGZH AS BDCZH, '已查封' type
  FROM DJ_CF A
  LEFT JOIN DJ_XGDJGL B
    ON A.SLBH = B.ZSLBH
  LEFT JOIN DJ_TSGL C
    ON A.SLBH = C.SLBH
 WHERE A.SLBH IS NOT NULL
   AND (A.LIFECYCLE IS NULL OR A.LIFECYCLE = 0)
   AND (C.LIFECYCLE IS NULL OR C.LIFECYCLE = 0)
   AND B.FSLBH IN (:dj_slbh)
   AND C.TSTYBM IN (SELECT TS.TSTYBM
                      FROM DJ_TSGL TS
                     WHERE TS.SLBH IN (:dj_slbh))
   AND A.SLBH NOT LIKE ':yw_slbh%'
UNION ALL
SELECT DISTINCT T6.SLBH, T6.BDCZH AS BDCZH, '已查封大证' type
  FROM DJ_CF T1
  LEFT JOIN DJ_XGDJGL T2
    ON T1.SLBH = T2.ZSLBH
  LEFT JOIN DJ_TSGL T3
    ON T1.SLBH = T3.SLBH
  LEFT JOIN (SELECT TSTYBM
               FROM DJ_TSGL
              WHERE SLBH IN (:dj_slbh)
             UNION ALL
             SELECT A.TSTYBM
               FROM ZD_QSDC A
               LEFT JOIN FC_Z_QSDC B
                 ON A.ZDTYBM = B.ZDTYBM
               LEFT JOIN FC_H_QSDC C
                 ON B.TSTYBM = C.LSZTYBM
               LEFT JOIN DJ_TSGL D
                 ON C.TSTYBM = D.TSTYBM
              WHERE D.SLBH IN (:dj_slbh)) T4
    ON T3.TSTYBM = T4.TSTYBM
  LEFT JOIN DJ_XGDJGL T5
    ON T1.SLBH = T5.ZSLBH
  LEFT JOIN DJ_DJB T6
    ON T5.FSLBH = T6.SLBH
 WHERE T1.SLBH IS NOT NULL
   AND (T1.LIFECYCLE IS NULL OR T1.LIFECYCLE = 0)
   AND (T3.LIFECYCLE IS NULL OR T3.LIFECYCLE = 0)
   AND T4.TSTYBM IS NOT NULL
   AND T6.SLBH IS NOT NULL
   AND (T6.ZSLX = '土地不动产证' OR T6.ZSLX = '土地证')
   AND T1.SLBH NOT LIKE ':yw_slbh%'
UNION ALL
SELECT DISTINCT B.SLBH, B.BDCDYH AS BDCZH, '已查封单元' type
  FROM DJ_TSGL A
  LEFT JOIN DJ_TSGL B
    ON A.TSTYBM = B.TSTYBM
 WHERE (A.LIFECYCLE = 0 OR A.LIFECYCLE IS NULL)
   AND (B.LIFECYCLE IS NULL OR B.LIFECYCLE = 0)
   AND A.DJZL = '查封'
   AND B.SLBH IN (:dj_slbh)
   AND A.SLBH NOT LIKE ':yw_slbh%'
UNION ALL
SELECT DISTINCT B.FSLBH AS SLBH, B.XGZH AS BDCZH, '已抵押' type
  FROM DJ_DY A
  LEFT JOIN DJ_XGDJGL B
    ON A.SLBH = B.ZSLBH
  LEFT JOIN DJ_TSGL C
    ON A.SLBH = C.SLBH
 WHERE (A.LIFECYCLE = 0 OR A.LIFECYCLE IS NULL)
   AND (C.LIFECYCLE = 0 OR C.LIFECYCLE IS NULL)
   AND B.FSLBH IN (:dj_slbh)
   AND A.DJRQ IS NOT NULL
   AND C.TSTYBM IN (SELECT TS.TSTYBM
                      FROM DJ_TSGL TS
                     WHERE TS.SLBH IN (:dj_slbh))
   AND A.SLBH NOT LIKE ':yw_slbh%'
UNION ALL
SELECT T6.SLBH, T6.BDCZH, '已抵押大证' TYPE
  FROM DJ_DY T4
  LEFT JOIN DJ_XGDJGL T5
    ON T4.SLBH = T5.ZSLBH
  LEFT JOIN (SELECT T1.SLBH, T1.BDCZH
               FROM DJ_DJB T1
               LEFT JOIN DJ_TSGL T2
                 ON T1.SLBH = T2.SLBH
               LEFT JOIN (SELECT A.TSTYBM
                           FROM DJ_TSGL A
                           LEFT JOIN DJ_DJB B
                             ON A.SLBH = B.SLBH
                          WHERE A.SLBH IN (:dj_slbh)
                            AND B.SLBH IS NOT NULL
                         UNION ALL
                         SELECT D.TSTYBM
                           FROM ZD_QSDC D
                           LEFT JOIN FC_Z_QSDC E
                             ON D.ZDTYBM = E.ZDTYBM
                           LEFT JOIN FC_H_QSDC F
                             ON E.TSTYBM = F.LSZTYBM
                           LEFT JOIN DJ_TSGL G
                             ON F.TSTYBM = G.TSTYBM
                          WHERE G.SLBH IN (:dj_slbh)
                            AND D.TSTYBM IS NOT NULL) T3
                 ON T2.TSTYBM = T3.TSTYBM
              WHERE T3.TSTYBM IS NOT NULL
                AND T1.SLBH IS NOT NULL
                AND (T1.ZSLX = '土地不动产证' OR T1.ZSLX = '土地证')) T6
    ON T5.FSLBH = T6.SLBH
  LEFT JOIN DJ_TSGL T7
    ON T4.SLBH = T7.SLBH
 WHERE T4.DJRQ IS NOT NULL
   AND T4.SLBH IS NOT NULL
   AND T7.SLBH IS NOT NULL
   AND T6.SLBH IS NOT NULL
   AND (T4.LIFECYCLE = 0 OR T4.LIFECYCLE IS NULL)
   AND (T7.LIFECYCLE = 0 OR T7.LIFECYCLE IS NULL)
UNION ALL
SELECT DISTINCT B.FSLBH AS SLBH, B.XGZH AS BDCZH, '已异议' type
  FROM DJ_YY A
  LEFT JOIN DJ_XGDJGL B
    ON A.SLBH = B.ZSLBH
  LEFT JOIN DJ_TSGL C
    ON A.SLBH = C.SLBH
 WHERE (A.LIFECYCLE = 0 OR A.LIFECYCLE IS NULL)
   AND (C.LIFECYCLE = 0 OR C.LIFECYCLE IS NULL)
   AND B.FSLBH IN (:dj_slbh)
   AND A.DJRQ IS NOT NULL
   AND C.TSTYBM IN (SELECT TS.TSTYBM
                      FROM DJ_TSGL TS
                     WHERE TS.SLBH IN (:dj_slbh))
   AND A.SLBH NOT LIKE ':yw_slbh%'");
            #endregion
            var data = await base.Db.Ado.SqlQueryAsync<JudgmentMortgage1>(sql, whereParams);

            foreach (var item in data)
            {
                JudgmentMortgage1 model = new JudgmentMortgage1();
                model.slbh = item.slbh;
                model.bdczh = item.bdczh;
                model.type = item.type;
                modelList.Add(model);
            }
            return modelList;
        }
        /// <summary>
        /// 判断是否有其他业务正在进行
        /// </summary>
        /// <param name="yw_slbh">当前业务受理编号</param>
        /// <param name="qz_slbh">权证受理编号</param>
        /// <returns></returns>
        public async Task<List<JudgmentMortgage1>> GetBusinessJudgment2(string yw_slbh, string qz_slbh)
        {
            
            base.ChangeDB(SysConst.DB_CON_BDC);

            List<JudgmentMortgage1> modelList = new List<JudgmentMortgage1>();

            #region SQL
            List<SugarParameter> whereParams = new List<SugarParameter>();
            whereParams.Add(new SugarParameter(":yw_slbh", yw_slbh));
            whereParams.Add(new SugarParameter(":qz_slbh", qz_slbh));
            string sql = string.Format(@"SELECT B.BDCZH, NVL(C.SLBH, A.ZSLBH) AS SLBH, NVL(C.DJLX, A.BGLX) as type
  FROM DJ_XGDJGL A
  LEFT JOIN DJ_DJB B
    ON A.FSLBH = B.SLBH
  LEFT JOIN DJ_DJB C
    ON A.ZSLBH = C.SLBH
 WHERE B.SLBH IN (:qz_slbh)
   AND C.DJRQ IS NULL
   AND (C.LIFECYCLE = '-1' OR C.LIFECYCLE IS NULL)
   AND C.SLBH IS NOT NULL
   AND A.ZSLBH NOT LIKE :yw_slbh
UNION ALL
SELECT B.BDCZH, NVL(C.SLBH, A.ZSLBH) AS SLBH, NVL(C.DJLX, '抵押登记')
  FROM DJ_XGDJGL A
  LEFT JOIN DJ_DJB B
    ON A.FSLBH = B.SLBH
  LEFT JOIN DJ_DY C
    ON A.ZSLBH = C.SLBH
 WHERE B.SLBH IN (:qz_slbh)
   AND C.DJRQ IS NULL
   AND (C.LIFECYCLE = '-1' OR C.LIFECYCLE IS NULL)
   AND C.SLBH IS NOT NULL
   AND A.ZSLBH NOT LIKE :yw_slbh
UNION ALL
SELECT B.BDCZMH AS BDCZH,
       NVL(C.SLBH, A.ZSLBH) AS SLBH,
       NVL(C.DJLX, '抵押登记')
  FROM DJ_XGDJGL A
  LEFT JOIN DJ_YG B
    ON A.FSLBH = B.SLBH
  LEFT JOIN DJ_DY C
    ON A.ZSLBH = C.SLBH
 WHERE B.SLBH IN (:qz_slbh)
   AND C.DJRQ IS NULL
   AND (C.LIFECYCLE = '-1' OR C.LIFECYCLE IS NULL)
   AND C.SLBH IS NOT NULL
   AND A.ZSLBH NOT LIKE :yw_slbh
UNION ALL
SELECT B.BDCZH, NVL(C.SLBH, A.ZSLBH) AS SLBH, N'异议登记' as type
  FROM DJ_XGDJGL A
  LEFT JOIN DJ_DJB B
    ON A.FSLBH = B.SLBH
  LEFT JOIN DJ_YY C
    ON A.ZSLBH = C.SLBH
 WHERE B.SLBH IN (:qz_slbh)
   AND C.DJRQ IS NULL
   AND (C.LIFECYCLE = '-1' OR C.LIFECYCLE IS NULL)
   AND C.SLBH IS NOT NULL
   AND A.ZSLBH NOT LIKE :yw_slbh
UNION ALL
SELECT B.BDCZMH AS BDCZH,
       NVL(C.SLBH, A.ZSLBH) AS SLBH,
       NVL(C.DJLX, '抵押变更登记')
  FROM DJ_XGDJGL A
  LEFT JOIN DJ_DY B
    ON A.FSLBH = B.SLBH
  LEFT JOIN DJ_DY C
    ON A.ZSLBH = C.SLBH
 WHERE B.SLBH IN (:qz_slbh)
   AND C.DJRQ IS NULL
   AND (C.LIFECYCLE = '-1' OR C.LIFECYCLE IS NULL)
   AND C.SLBH IS NOT NULL
   AND A.ZSLBH NOT LIKE :yw_slbh
UNION ALL
SELECT B.BDCZMH AS BDCZH,
       NVL(C.SLBH, A.ZSLBH) AS SLBH,
       N'异议变更登记' as type
  FROM DJ_XGDJGL A
  LEFT JOIN DJ_YY B
    ON A.FSLBH = B.SLBH
  LEFT JOIN DJ_YY C
    ON A.ZSLBH = C.SLBH
 WHERE B.SLBH IN (:qz_slbh)
   AND C.DJRQ IS NULL
   AND (C.LIFECYCLE = '-1' OR C.LIFECYCLE IS NULL)
   AND C.SLBH IS NOT NULL
   AND A.ZSLBH NOT LIKE :yw_slbh
UNION ALL
SELECT B.BDCZH, NVL(C.SLBH, A.ZSLBH) AS SLBH, N'权证注销登记' as type
  FROM DJ_XGDJGL A
  LEFT JOIN DJ_DJB B
    ON A.FSLBH = B.SLBH
  LEFT JOIN DJ_XGDJZX C
    ON A.ZSLBH = C.SLBH
 WHERE B.SLBH IN (:qz_slbh)
   AND C.DJRQ IS NULL
   AND (B.LIFECYCLE = '-1' OR B.LIFECYCLE IS NULL)
   AND C.SLBH IS NOT NULL
   AND A.ZSLBH NOT LIKE :yw_slbh
UNION ALL
SELECT B.BDCZMH AS BDCZH,
       NVL(C.SLBH, A.ZSLBH) AS SLBH,
       N'抵押注销登记' as type
  FROM DJ_XGDJGL A
  LEFT JOIN DJ_DY B
    ON A.FSLBH = B.SLBH
  LEFT JOIN DJ_XGDJZX C
    ON A.ZSLBH = C.SLBH
 WHERE B.SLBH IN (:qz_slbh)
   AND C.DJRQ IS NULL
   AND (B.LIFECYCLE = '-1' OR B.LIFECYCLE IS NULL)
   AND C.SLBH IS NOT NULL
   AND A.ZSLBH NOT LIKE :yw_slbh
UNION ALL
SELECT B.BDCZMH AS BDCZH,
       NVL(C.SLBH, A.ZSLBH) AS SLBH,
       N'异议注销登记' as type
  FROM DJ_XGDJGL A
  LEFT JOIN DJ_YY B
    ON A.FSLBH = B.SLBH
  LEFT JOIN DJ_XGDJZX C
    ON A.ZSLBH = C.SLBH
 WHERE B.SLBH IN (:qz_slbh)
   AND C.DJRQ IS NULL
   AND (B.LIFECYCLE = '-1' OR B.LIFECYCLE IS NULL)
   AND C.SLBH IS NOT NULL
   AND A.ZSLBH NOT LIKE :yw_slbh");
            
            #endregion
            var data = await base.Db.Ado.SqlQueryAsync<JudgmentMortgage1>(sql, whereParams);

            foreach (var item in data)
            {
                JudgmentMortgage1 model = new JudgmentMortgage1();
                model.slbh = item.slbh;
                model.bdczh = item.bdczh;
                model.type = item.type;
                modelList.Add(model);
            }
            return modelList;
        }

        /// <summary>
        /// 对应房地状态
        /// </summary>
        /// <param name="ywlx">业务类型：抵押或抵押变更</param>
        /// <param name="bdclx">不动产类型：房屋或者宗地</param>
        /// <param name="tstybm">图属统一编码</param>
        /// <param name="qz_slbh">权证受理编号</param>
        /// <param name="yw_slbh">当前办理业务受理编号</param>
        /// <param name="dy_slbh">当前抵押受理编号</param>
        /// <returns></returns>
        public async Task<string> GetBusinessJudgment3(string ywlx, string bdclx, string tstybm, string qz_slbh, string yw_slbh, string dy_slbh)
        {
            string msg = "";
            string sql_cf = "";     //查封SQL
            string sql_dy = "";     //抵押SQL
            string sql_djb = "";    //权属SQL
            string cf_msg = "";     //查封消息msg
            string dy_msg = "";     //抵押消息msg
            string dj_msg = "";     //权属消息msg
            //ywlx = "抵押变更";
            //bdclx = "1";
            //tstybm = "TDTD-DNW1-001-T";
            //qz_slbh = "TDTD-DNW1-001-T";
            //dy_slbh = "TD34557802";
            //yw_slbh = "1";
            List<SugarParameter> whereParams = new List<SugarParameter>();
            base.ChangeDB(SysConst.DB_CON_BDC);
            #region 根据条件判断走哪个SQL
            //业务类型为抵押、权属变更、初始登记
            if (ywlx == "抵押" || ywlx == "权属变更" || ywlx == "初始登记")
            {                
                if (bdclx == "房屋")      //不动产类型为：房屋
                {
                    //房屋对应土地登记信息
                    //yw_slbh = "1";
                    whereParams.Add(new SugarParameter(":tstybm", tstybm));
                    whereParams.Add(new SugarParameter(":qz_slbh", qz_slbh));
                    whereParams.Add(new SugarParameter(":yw_slbh", yw_slbh));
                    sql_cf = @"SELECT SLBH,CFWH AS XGXX,'查封'AS TYPE FROM DJ_CF WHERE SLBH != :qz_slbh AND SLBH != :yw_slbh AND SLBH IN (SELECT SLBH FROM DJ_TSGL WHERE TSTYBM IN (SELECT TSTYBM FROM ZD_QSDC WHERE ZDTYBM IN (SELECT ZDTYBM FROM FC_H_QSDC WHERE TSTYBM IN( :TSTYBM)))AND (LIFECYCLE IS NULL OR LIFECYCLE != '1'))";
                    sql_dy = @"SELECT SLBH,BDCZMH AS XGXX,'抵押'AS TYPE FROM DJ_DY WHERE SLBH != :qz_slbh AND SLBH != :yw_slbh AND SLBH IN (SELECT SLBH FROM DJ_TSGL WHERE TSTYBM IN (SELECT TSTYBM FROM ZD_QSDC WHERE ZDTYBM IN (SELECT ZDTYBM FROM FC_H_QSDC WHERE TSTYBM IN(:TSTYBM)))AND (LIFECYCLE IS NULL OR LIFECYCLE != '1'))";
                    sql_djb = @"SELECT SLBH,BDCZH AS XGXX,'权属'AS TYPE FROM DJ_DJB WHERE SLBH != :qz_slbh AND SLBH != :yw_slbh AND SLBH IN (SELECT SLBH FROM DJ_TSGL WHERE TSTYBM IN (SELECT TSTYBM FROM ZD_QSDC WHERE ZDTYBM IN (SELECT ZDTYBM FROM FC_H_QSDC WHERE TSTYBM IN(:TSTYBM)))AND (LIFECYCLE IS NULL OR LIFECYCLE != '1'))";
                    msg = "房屋对应土地登记有:";
                }
                else   //不动产类型为：宗地
                {                    
                    //判断宗地对应房屋信息
                    //yw_slbh = "1";
                    whereParams.Add(new SugarParameter(":tstybm", tstybm));
                    whereParams.Add(new SugarParameter(":qz_slbh", qz_slbh));
                    whereParams.Add(new SugarParameter(":yw_slbh", yw_slbh));
                    sql_cf = @"SELECT SLBH,CFWH AS XGXX,'查封'AS TYPE FROM DJ_CF WHERE SLBH != :qz_slbh AND SLBH != :yw_slbh AND SLBH IN (SELECT SLBH FROM DJ_TSGL WHERE TSTYBM IN (SELECT TSTYBM FROM FC_H_QSDC WHERE ZDTYBM IN (SELECT ZDTYBM FROM ZD_QSDC WHERE TSTYBM IN (:tstybm)))AND (LIFECYCLE IS NULL OR LIFECYCLE != '1'))";
                    sql_dy = @"SELECT SLBH,BDCZMH AS XGXX,'抵押'AS TYPE FROM DJ_DY WHERE SLBH != :qz_slbh AND SLBH != :yw_slbh AND SLBH IN (SELECT SLBH FROM DJ_TSGL WHERE TSTYBM IN (SELECT TSTYBM FROM FC_H_QSDC WHERE ZDTYBM IN (SELECT ZDTYBM FROM ZD_QSDC WHERE TSTYBM IN (:tstybm)))AND (LIFECYCLE IS NULL OR LIFECYCLE != '1'))";
                    sql_djb = @"SELECT SLBH,BDCZH AS XGXX,'权属'AS TYPE FROM DJ_DJB WHERE SLBH != :qz_slbh AND SLBH != :yw_slbh AND SLBH IN (SELECT SLBH FROM DJ_TSGL WHERE TSTYBM IN (SELECT TSTYBM FROM FC_H_QSDC WHERE ZDTYBM IN (SELECT ZDTYBM FROM ZD_QSDC WHERE TSTYBM IN (:tstybm)))AND (LIFECYCLE IS NULL OR LIFECYCLE != '1'))";
                    msg = "宗地上的房屋有:";
                }
            }
            else if(ywlx == "抵押变更" || ywlx == "抵押转移")   //业务类型为抵押变更、抵押转移
            {
                if (bdclx == "房屋")  //不动产类型为：房屋
                {
                    //判断房子对应宗地信息
                    //yw_slbh = "1";
                    whereParams.Add(new SugarParameter(":tstybm", tstybm));
                    whereParams.Add(new SugarParameter(":qz_slbh", qz_slbh));
                    whereParams.Add(new SugarParameter(":yw_slbh", yw_slbh));
                    whereParams.Add(new SugarParameter(":dy_slbh", dy_slbh));
                    sql_cf = @"SELECT SLBH,CFWH AS XGXX,'查封'AS TYPE FROM DJ_CF WHERE SLBH != :qz_slbh AND SLBH != :yw_slbh AND SLBH != :dy_slbh AND SLBH IN (SELECT SLBH FROM DJ_TSGL WHERE TSTYBM IN (SELECT TSTYBM FROM ZD_QSDC WHERE ZDTYBM IN (SELECT ZDTYBM FROM FC_H_QSDC WHERE TSTYBM IN(:tstybm)))AND (LIFECYCLE IS NULL OR LIFECYCLE != '1'))";
                    sql_dy = @"SELECT SLBH,BDCZMH AS XGXX,'抵押'AS TYPE FROM DJ_DY WHERE SLBH != :qz_slbh AND SLBH != :yw_slbh AND SLBH != :dy_slbh AND SLBH IN (SELECT SLBH FROM DJ_TSGL WHERE TSTYBM IN (SELECT TSTYBM FROM ZD_QSDC WHERE ZDTYBM IN (SELECT ZDTYBM FROM FC_H_QSDC WHERE TSTYBM IN(:tstybm)))AND (LIFECYCLE IS NULL OR LIFECYCLE != '1'))";
                    sql_djb = @"SELECT SLBH,BDCZH AS XGXX,'权属'AS TYPE FROM DJ_DJB WHERE SLBH != :qz_slbh AND SLBH != :yw_slbh AND SLBH != :dy_slbh AND SLBH IN (SELECT SLBH FROM DJ_TSGL WHERE TSTYBM IN (SELECT TSTYBM FROM ZD_QSDC WHERE ZDTYBM IN (SELECT ZDTYBM FROM FC_H_QSDC WHERE TSTYBM IN(:tstybm)))AND (LIFECYCLE IS NULL OR LIFECYCLE != '1'))";
                    msg = "办理" + ywlx + "业务，房屋对应土地登记有:";
                }
                else  //不动产类型为：宗地
                {
                    //判断宗地对应房屋信息
                    whereParams.Add(new SugarParameter(":tstybm", tstybm));
                    whereParams.Add(new SugarParameter(":yw_slbh", yw_slbh));
                    whereParams.Add(new SugarParameter(":qz_slbh", qz_slbh));
                    whereParams.Add(new SugarParameter(":dy_slbh", dy_slbh));
                    sql_cf = @"SELECT SLBH,CFWH AS XGXX,'查封'AS TYPE FROM DJ_CF WHERE SLBH != :qz_slbh AND SLBH != :yw_slbh AND SLBH IN (SELECT SLBH FROM DJ_TSGL WHERE TSTYBM IN (SELECT TSTYBM FROM FC_H_QSDC WHERE ZDTYBM IN (SELECT ZDTYBM FROM ZD_QSDC WHERE TSTYBM IN (:tstybm)))AND (LIFECYCLE IS NULL OR LIFECYCLE != '1') AND SLBH !=:dy_slbh)";
                    sql_dy = @"SELECT SLBH,BDCZMH AS XGXX,'抵押'AS TYPE FROM DJ_DY WHERE SLBH != :qz_slbh AND SLBH != :yw_slbh AND SLBH IN (SELECT SLBH FROM DJ_TSGL WHERE TSTYBM IN (SELECT TSTYBM FROM FC_H_QSDC WHERE ZDTYBM IN (SELECT ZDTYBM FROM ZD_QSDC WHERE TSTYBM IN (:tstybm)))AND (LIFECYCLE IS NULL OR LIFECYCLE != '1')AND SLBH !=:dy_slbh)";
                    sql_djb = @"SELECT SLBH,BDCZH AS XGXX,'权属'AS TYPE FROM DJ_DJB WHERE SLBH != :qz_slbh AND SLBH != :yw_slbh AND SLBH IN (SELECT SLBH FROM DJ_TSGL WHERE TSTYBM IN (SELECT TSTYBM FROM FC_H_QSDC WHERE ZDTYBM IN (SELECT ZDTYBM FROM ZD_QSDC WHERE TSTYBM IN (:tstybm)))AND (LIFECYCLE IS NULL OR LIFECYCLE != '1')AND SLBH !=:dy_slbh)";
                    msg = "办理" + ywlx + "业务，宗地上的房屋有:";
                }
            }
            #endregion
            //根据whereParams里的条件，去查封、抵押、权属的数据
            if(!string.IsNullOrEmpty(sql_cf))
            {
                var cfList = await base.Db.Ado.SqlQueryAsync<JudgmentMortgage>(sql_cf, whereParams);
                if (cfList.Count > 0)
                {
                    cf_msg = "存在查封信息,";
                }
            }
            if (!string.IsNullOrEmpty(sql_dy))
            {
                var dyList = await base.Db.Ado.SqlQueryAsync<JudgmentMortgage>(sql_dy, whereParams);
                if (dyList.Count > 0)
                {
                    dy_msg = "存在抵押信息,";
                }
            }
            if (!string.IsNullOrEmpty(sql_djb))
            {
                var djbList = await base.Db.Ado.SqlQueryAsync<JudgmentMortgage>(sql_djb, whereParams);
                if (djbList.Count > 0)
                {
                    dj_msg = "存在登记信息";
                }
            }
            msg = msg + cf_msg + dy_msg + dj_msg;

            return msg;
        }

        /// <summary>
        /// 选择物判断
        /// </summary>
        /// <param name="yw_slbh">当前业务受理编号</param>
        /// <param name="qz_slbh">权证受理编号</param>
        /// <param name="tstybm">权证受理编号</param>
        /// <returns></returns>
        public async Task<List<JudgmentMortgage1>> GetBusinessJudgment4(string yw_slbh, string qz_slbh,string tstybm)
        {
            //yw_slbh = "202012170117";
            //qz_slbh = "201908010063";
            //tstybm = "TDTD-2013-014-H";
            base.ChangeDB(SysConst.DB_CON_BDC);

            List<JudgmentMortgage1> modelList = new List<JudgmentMortgage1>();

            #region SQL
            List<SugarParameter> whereParams = new List<SugarParameter>();
            whereParams.Add(new SugarParameter(":YW_SLBH", yw_slbh));
            whereParams.Add(new SugarParameter(":QZ_SLBH", qz_slbh));
            whereParams.Add(new SugarParameter(":TSTYBM", tstybm));
            string sql = string.Format(@"SELECT WM_CONCAT(TO_CHAR(SLBH)) SLBH,WM_CONCAT(TO_CHAR(DJZL)) TYPE
  FROM DJ_TSGL
 WHERE SLBH != :QZ_SLBH
   AND SLBH != :YW_SLBH
   AND TSTYBM = :TSTYBM
   AND (LIFECYCLE IS NULL OR LIFECYCLE != '1')
   AND DJZL NOT IN ('查封注销', '抵押注销', '异议注销', '预告注销')");

            #endregion
            var data = await base.Db.Ado.SqlQueryAsync<JudgmentMortgage1>(sql, whereParams);

            foreach (var item in data)
            {
                JudgmentMortgage1 model = new JudgmentMortgage1();
                model.slbh = item.slbh;
                model.bdczh = item.bdczh;
                model.type = item.type;
                if(model.type != null)
                {
                    modelList.Add(model);
                }
                
            }
            return modelList;
        }
    }
}
