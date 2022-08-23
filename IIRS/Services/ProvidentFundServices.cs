using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class ProvidentFundServices : BaseServices, IProvidentFundServices
    {
        private readonly ILogger<ProvidentFundServices> _logger;
        private readonly IDBTransManagement _dbTransManagement;
        private readonly IDJ_QLRGLRepository _qlrglRepository;
        public ProvidentFundServices(IDBTransManagement dbTransManagement, ILogger<ProvidentFundServices> logger, IDJ_QLRGLRepository qlrglRepository) : base(dbTransManagement)
        {
            _logger = logger;
            _dbTransManagement = dbTransManagement;
            _qlrglRepository = qlrglRepository;
        }

        public async Task<ProvidentFundModel> FundModels2(ProvidentFundVModel vModel)
        {
            string firstzjhm = "";
            string firstname = "";
            string secondzjhm = "";
            string secondname = "";
            string Fifteen_zjhm = "";
            string UpperZjmh = "";
            string LowerZjhm = "";
            //输入配偶身份证号
            string Fifteen_zjhm1 = "";
            string UpperZjmh1 = "";
            string LowerZjhm1 = "";
            string con_result = "";
            string DyInfo = "";
            int count = 0;
            string mjmask = "";
            string sql = "";
            string strZl = "";
            string StrScdjrq = "";
            base.ChangeDB(SysConst.DB_CON_BDC);
            List<SugarParameter> whereParams = new List<SugarParameter>();
            if (!string.IsNullOrEmpty( vModel.zjhm1))  //输入了配偶的姓名和身份证号
            {
                //输入身份证号
                Fifteen_zjhm = vModel.zjhm[0..6] + vModel.zjhm[8..^1];
                UpperZjmh = vModel.zjhm.ToUpper();
                LowerZjhm = vModel.zjhm.ToLower();
                //输入配偶身份证号
                Fifteen_zjhm1 = vModel.zjhm1[0..6] + vModel.zjhm1[8..^1];
                UpperZjmh1 = vModel.zjhm1.ToUpper();
                LowerZjhm1 = vModel.zjhm1.ToLower();

                
                whereParams.Add(new SugarParameter(":firstzjhm", vModel.zjhm));
                whereParams.Add(new SugarParameter(":upperfirstzjhm", UpperZjmh));
                whereParams.Add(new SugarParameter(":lowerfirstzjhm", LowerZjhm));
                whereParams.Add(new SugarParameter(":Fifteenfirstzjhm", Fifteen_zjhm));
                whereParams.Add(new SugarParameter(":firstname", vModel.qlrmc));
                whereParams.Add(new SugarParameter(":secondzjhm", vModel.zjhm1));
                whereParams.Add(new SugarParameter(":uppersecondzjhm", UpperZjmh1));
                whereParams.Add(new SugarParameter(":lowersecondzjhm", LowerZjhm1));
                whereParams.Add(new SugarParameter(":Fifteensecondzjhm", Fifteen_zjhm1));
                whereParams.Add(new SugarParameter(":secondname", vModel.qlrmc1));
                #region SQL
                sql = string.Format(@"SELECT DISTINCT SLBH,QLRMC, ZJHM, BDCZH, DJRQ, ZL, JZMJ, BDBZZQSE, QLQSSJ,QLJSSJ, DYQR
  FROM (SELECT WM_CONCAT(DISTINCT TO_CHAR(A.QLRMC)) QLRMC,
               WM_CONCAT(DISTINCT TO_CHAR(A.ZJHM)) ZJHM,
               B.SLBH,
               B.BDCZH,
               B.DJRQ,
               C.ZL,
               D.JZMJ,
               F.BDBZZQSE,
               F.QLQSSJ,F.QLJSSJ,
               (SELECT QLRMC
                  FROM V_DJ_QLRGL
                 WHERE ((SLBH = F.SLBH) AND (QLRLX = '抵押权人'))
                   AND ROWNUM = 1) AS DYQR
          FROM DJ_QLRGL A, DJ_DJB B, DJ_SJD C, QL_FWXG D
          LEFT JOIN DJ_XGDJGL E
            ON D.SLBH = E.FSLBH
          LEFT JOIN DJ_DY F
            ON E.ZSLBH = F.SLBH
         WHERE A.SLBH = B.SLBH
           AND B.SLBH = C.SLBH
           AND C.SLBH = D.SLBH
           AND A.SLBH IN (SELECT BB.SLBH
                            FROM DJ_QLR AA ,DJ_QLRGL BB
                           WHERE (AA.ZJHM = :firstzjhm or AA.ZJHM = :upperfirstzjhm or AA.ZJHM = :lowerfirstzjhm or AA.ZJHM = :Fifteenfirstzjhm)
                             AND AA.QLRMC = :firstname
                             AND AA.QLRID =BB.QLRID
                             AND QLRLX = '权利人')
           AND A.QLRLX = '权利人' AND (B.LIFECYCLE = 0 OR B.LIFECYCLE IS NULL)
         GROUP BY B.BDCZH,B.SLBH,
                  B.DJRQ,
                  C.ZL,
                  D.JZMJ,
                  F.SLBH,
                  F.BDBZZQSE,F.QLJSSJ,
                  F.QLQSSJ
        UNION ALL
        SELECT WM_CONCAT(DISTINCT TO_CHAR(A.QLRMC)) QLRMC,
               WM_CONCAT(DISTINCT TO_CHAR(A.ZJHM)) ZJHM,
               B.SLBH,
               B.BDCZH,
               B.DJRQ,
               C.ZL,
               D.JZMJ,
               F.BDBZZQSE,
               F.QLQSSJ,F.QLJSSJ,
               (SELECT QLRMC
                  FROM V_DJ_QLRGL
                 WHERE ((SLBH = F.SLBH) AND (QLRLX = '抵押权人'))
                   AND ROWNUM = 1) AS DYQR
          FROM DJ_QLRGL A, DJ_DJB B, DJ_SJD C, QL_FWXG D
          LEFT JOIN DJ_XGDJGL E
            ON D.SLBH = E.FSLBH
          LEFT JOIN DJ_DY F
            ON E.ZSLBH = F.SLBH
         WHERE A.SLBH = B.SLBH
           AND B.SLBH = C.SLBH
           AND C.SLBH = D.SLBH
           AND A.SLBH IN (SELECT SLBH
                            FROM DJ_QLRGL AA
                           WHERE (AA.ZJHM = :secondzjhm or AA.ZJHM = :uppersecondzjhm or AA.ZJHM = :lowersecondzjhm or AA.ZJHM = :Fifteensecondzjhm) AND AA.QLRMC = :secondname AND QLRLX = '权利人')
           AND A.QLRLX = '权利人' AND (B.LIFECYCLE = 0 OR B.LIFECYCLE IS NULL)
         GROUP BY B.BDCZH,B.SLBH,
                  B.DJRQ,
                  C.ZL,
                  D.JZMJ,
                  F.SLBH,
                  F.BDBZZQSE,F.QLJSSJ,
                  F.QLQSSJ)");
                #endregion
            }
            else   //没有输入配偶的姓名和身份证号，只输入了姓名和身份证号
            {
                //输入身份证号
                Fifteen_zjhm = vModel.zjhm[0..6] + vModel.zjhm[8..^1];
                UpperZjmh = vModel.zjhm.ToUpper();
                LowerZjhm = vModel.zjhm.ToLower();

                whereParams.Add(new SugarParameter(":firstzjhm", vModel.zjhm));
                whereParams.Add(new SugarParameter(":upperfirstzjhm", UpperZjmh));
                whereParams.Add(new SugarParameter(":lowerfirstzjhm", LowerZjhm));
                whereParams.Add(new SugarParameter(":Fifteenfirstzjhm", Fifteen_zjhm));
                whereParams.Add(new SugarParameter(":firstname", vModel.qlrmc));
                #region SQL
                sql = string.Format(@"SELECT DISTINCT SLBH,QLRMC, ZJHM, BDCZH, DJRQ, ZL, JZMJ, BDBZZQSE, QLQSSJ,QLJSSJ, DYQR
  FROM (SELECT WM_CONCAT(DISTINCT TO_CHAR(A.QLRMC)) QLRMC,
               WM_CONCAT(DISTINCT TO_CHAR(A.ZJHM)) ZJHM,
               B.SLBH,
               B.BDCZH,
               B.DJRQ,
               C.ZL,
               D.JZMJ,
               F.BDBZZQSE,
               F.QLQSSJ,
               F.QLJSSJ,
               (SELECT QLRMC
                  FROM V_DJ_QLRGL
                 WHERE ((SLBH = F.SLBH) AND (QLRLX = '抵押权人'))
                   AND ROWNUM = 1) AS DYQR
          FROM DJ_QLRGL A, DJ_DJB B, DJ_SJD C, QL_FWXG D
          LEFT JOIN DJ_XGDJGL E
            ON D.SLBH = E.FSLBH
          LEFT JOIN DJ_DY F
            ON E.ZSLBH = F.SLBH
         WHERE A.SLBH = B.SLBH
           AND B.SLBH = C.SLBH
           AND C.SLBH = D.SLBH
           AND A.SLBH IN (SELECT BB.SLBH
                            FROM DJ_QLR AA ,DJ_QLRGL BB
                           WHERE (AA.ZJHM = :firstzjhm or AA.ZJHM = :upperfirstzjhm or AA.ZJHM = :lowerfirstzjhm or AA.ZJHM = :Fifteenfirstzjhm)
                             AND AA.QLRMC = :firstname
                             AND AA.QLRID =BB.QLRID
                             AND QLRLX = '权利人')
           AND A.QLRLX = '权利人' AND (B.LIFECYCLE = 0 OR B.LIFECYCLE IS NULL)
         GROUP BY B.BDCZH,B.SLBH,
                  B.DJRQ,
                  C.ZL,
                  D.JZMJ,
                  F.SLBH,
                  F.BDBZZQSE,
                  F.QLQSSJ,F.QLJSSJ)");
                #endregion
            }

            var data = await base.Db.Ado.SqlQueryAsync<ProvidentResultVModel>(sql, whereParams);

            for (int i = 0; i < data.Count; i++)
            {
                var strmj = String.Concat(Math.Abs(data[i].JZMJ.Value));
                if(strmj.IndexOf('.') > 0)
                {
                    mjmask = strmj.Replace(strmj.Substring(strmj.IndexOf('.') - 1, 1), "*");
                    mjmask = mjmask.Substring(0, strmj.IndexOf('.') + 1) + "*";
                }
                else
                {                    
                    mjmask = strmj.Replace(strmj.Substring(0,1), "*");
                }
                
                
                if (data[i].DYQR != null)
                {
                    DyInfo = ",抵押权人为：" + data[i].DYQR + ",抵押金额为：" + data[i].BDBZZQSE + "元。" + "抵押起止时间为：" + Convert.ToDateTime(data[i].QLQSSJ).ToLongDateString() + "至" + Convert.ToDateTime(data[i].QLJSSJ).ToLongDateString() + "止。";
                }
                else
                {
                    DyInfo = ",无抵押信息。";
                }
                if(data[i].ZL != "")
                {
                    if (data[i].ZL.Length <= 5)
                    {
                        strZl = data[i].ZL;
                }
                    else
                    {
                        strZl = data[i].ZL.Substring(0, data[i].ZL.Length - 5) + "****" + data[i].ZL.Substring(data[i].ZL.Length - 1);
                    }
                }

                whereParams.Add(new SugarParameter(":SLBH", data[i].SLBH));
                #region 上次取得时间SQL
                string dateSql = "SELECT TO_CHAR(A.DJRQ,'YYYY-MM-DD') AS SCDJRQ FROM DJ_DJB A,DJ_XGDJGL B WHERE A.SLBH = B.FSLBH AND B.XGZLX LIKE '%房%' AND B.ZSLBH = :SLBH";
                var timeData = await base.Db.Ado.SqlQueryAsync<ProvidentResultVModel>(dateSql, whereParams);
                if (timeData.Count > 0)
                {

                
                if (!string.IsNullOrEmpty(timeData[0].SCDJRQ))
                {
                    StrScdjrq = "，上次取得日期为：" + timeData[0].SCDJRQ ;
                }
                }
                else
                {
                    StrScdjrq = "";
                }
                #endregion

                con_result += i + 1 + ".不动产证号：" + data[i].BDCZH + ",权利人：" + data[i].QLRMC + ",地址：" + strZl + ",建筑面积：" + mjmask + "平方米，登记日期：" + (!data[i].DJRQ.HasValue ? "" : ((DateTime)data[i].DJRQ).ToLongDateString()) + StrScdjrq + DyInfo + Environment.NewLine;
            }
            ProvidentFundModel model = new ProvidentFundModel();
            if (vModel.zjhm1 != "")
            {
                model.qlrmc = vModel.qlrmc;
                model.zjhm = vModel.zjhm;
                model.qlrmc1 = vModel.qlrmc1;
                model.zjhm1 = vModel.zjhm1;
            }
            else
            {
                model.qlrmc = vModel.qlrmc;
                model.zjhm = vModel.zjhm;
            }

            if (data.Count > 0)
            {
                model.result = "　　　　经查，在辽阳市不动产登记中心数据库中存在" + data.Count + "条房屋登记记录。" + Environment.NewLine + con_result;
                model.housecount = data.Count;
            }
            else
            {
                model.result = "　　　　经查，在辽阳市不动产登记中心数据库中不存在房屋登记记录。";
                model.housecount = 0;
            }

            model.queryDate = DateTime.Now;
            model.conditionQuery = vModel.qlrmc + ";" + vModel.zjhm + ";" + vModel.qlrmc1 + ";" + vModel.zjhm1;
            model.org_name = vModel.org_name;
            model.user_name = vModel.user_name;
            model.pid = Guid.NewGuid();

            return model;
        }

        public async Task<ProvidentFundModel> FundModels(ProvidentFundVModel vModel)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            ProvidentFundModel model = new ProvidentFundModel();
            string qlrmc = "";
            string qlrmc1 = "";
            int count = 0;
            string con_result = "";
            string Fifteen_zjhm = vModel.zjhm[0..6] + vModel.zjhm[8..^1];
            //第一个权利拥有的房屋
            var result = await base.Db.Queryable<DJ_DJB, DJ_QLRGL, DJ_QLR,DJ_SJD,QL_FWXG>((A, B, C, D, E) => new object[] { JoinType.Inner, A.SLBH == B.SLBH, JoinType.Inner, B.QLRID == C.QLRID, JoinType.Inner, A.SLBH == D.SLBH, JoinType.Inner, A.SLBH == E.SLBH }).Where((A, B, C, D, E) => ((C.ZJHM == vModel.zjhm.ToUpper() || C.ZJHM == Fifteen_zjhm) && (A.LIFECYCLE == 0 || A.LIFECYCLE == null) && B.QLRLX == "权利人")).Select((A, B, C, D, E) => new
            {
                qlrmc = C.QLRMC,
                zjhm = C.ZJHM,
                bdczh = A.BDCZH,
                zl = D.ZL,
                djrq = A.DJRQ,
                jzmj = E.JZMJ
            }).ToListAsync();

            if (vModel.zjhm1 != "")
            {
                string Fifteen_zjhm1 = vModel.zjhm1[0..6] + vModel.zjhm1[8..^1];
                //第二个权利人拥有的房屋
                var result1 = await base.Db.Queryable<DJ_DJB, DJ_QLRGL, DJ_QLR, DJ_SJD, QL_FWXG>((A, B, C, D, E) => new object[] { JoinType.Inner, A.SLBH == B.SLBH, JoinType.Inner, B.QLRID == C.QLRID, JoinType.Inner, A.SLBH == D.SLBH, JoinType.Inner, A.SLBH == E.SLBH }).Where((A, B, C, D, E) => ((C.ZJHM == vModel.zjhm1.ToUpper() || C.ZJHM == Fifteen_zjhm1) && (A.LIFECYCLE == 0 || A.LIFECYCLE == null) && B.QLRLX == "权利人")).Select((A, B, C, D, E) => new
                {
                    qlrmc = C.QLRMC,
                    zjhm = C.ZJHM,
                    bdczh = A.BDCZH,
                    zl = D.ZL,
                    djrq = A.DJRQ,
                    jzmj = E.JZMJ + 0
                }).ToListAsync();

                if(result.Count > 0 && result1.Count > 0)
                {
                    if (result.Count > result1.Count)
                    {
                        qlrmc = result[0].qlrmc;
                        count = result.Count;
                        for (int i = 0; i < result.Count; i++)
                        {
                            var strmj = String.Concat(Math.Abs(result[i].jzmj.Value));
                            var mjmask = strmj.Replace(strmj.Substring(strmj.IndexOf('.') - 1, 1), "*");
                            mjmask = mjmask.Substring(0, strmj.IndexOf('.') + 1) + "*";
                            var strZl = result[i].zl.Substring(0, result[i].zl.Length - 5) + "****" + result[i].zl.Substring(result[i].zl.Length - 1);
                            con_result += i + 1 + ".不动产证号：" + result[i].bdczh + ",地址：" + strZl + ",建筑面积：" + mjmask + "平方米，登记日期：" + (result[i].djrq.HasValue ? "" : ((DateTime)result[i].djrq).ToLongDateString()) + Environment.NewLine;
                        }
                    }
                    else
                    {
                        qlrmc1 = result1[0].qlrmc;
                        count = result1.Count;
                        for (int i = 0; i < result1.Count; i++)
                        {
                            var strmj = String.Concat(Math.Abs(result1[i].jzmj.Value));
                            var mjmask = strmj.Replace(strmj.Substring(strmj.IndexOf('.') - 1, 1), "*");
                            mjmask = mjmask.Substring(0, strmj.IndexOf('.') + 1) + "*";
                            var strZl = result1[i].zl.Substring(0, result1[i].zl.Length - 5) + "****" + result1[i].zl.Substring(result1[i].zl.Length - 1);
                            con_result += i + 1 + ".不动产证号：" + result1[i].bdczh + ",地址：" + strZl + ",建筑面积：" + mjmask + "平方米，登记日期：" + (result[i].djrq.HasValue ? "" : ((DateTime)result[i].djrq).ToLongDateString()) + Environment.NewLine;
                        }
                    }
                }
                
                
            }
            else
            {
                if(result.Count > 0)
                {
                    qlrmc = result[0].qlrmc;
                    count = result.Count;
                    for (int i = 0; i < result.Count; i++)
                    {
                        var strmj = String.Concat(Math.Abs(result[i].jzmj.Value));
                        var mjmask = strmj.Replace(strmj.Substring(strmj.IndexOf('.') - 1, 1), "*");
                        mjmask = mjmask.Substring(0, strmj.IndexOf('.') + 1) + "*";
                        var strZl = result[i].zl.Substring(0, result[i].zl.Length - 5) + "****" + result[i].zl.Substring(result[i].zl.Length - 1);
                        con_result += i + 1 + ".不动产证号：" + result[i].bdczh + ",地址：" + strZl + ",建筑面积：" + mjmask + "平方米，登记日期：" + (result[i].djrq.HasValue ? "" : ((DateTime)result[i].djrq).ToLongDateString()) + Environment.NewLine;
                    }
                }
                

            }
            //未输入第二个权利人姓名，将权利人名称赋值
            if (vModel.qlrmc == "")
            {
                model.qlrmc = qlrmc;
            }
            else
            {
                model.qlrmc = vModel.qlrmc;
            }                
            model.zjhm = vModel.zjhm;

            if (vModel.qlrmc1 == "")
                model.qlrmc1 = qlrmc1;
            else
                model.qlrmc1 = vModel.qlrmc1;
            model.zjhm1 = vModel.zjhm1;
            if(count > 0)
            {
                model.result = "　　　　经查，在辽阳市不动产登记中心数据库中存在" + count + "条房屋登记记录。" + Environment.NewLine + con_result;
                model.housecount = count;
            }
            else
            {
                model.result = "　　　　经查，在辽阳市不动产登记中心数据库中不存在房屋登记记录。";
                model.housecount = 0;
            }
            model.queryDate = DateTime.Now;
            model.conditionQuery = vModel.qlrmc + ";" + vModel.zjhm + ";" + vModel.qlrmc1 + ";" + vModel.zjhm1;
            model.org_name = vModel.org_name;
            model.user_name = vModel.user_name;
            model.pid = Guid.NewGuid();

            return model;
        }

        public async Task<ProvidentFundModel> FundModels1(ProvidentFundVModel vModel)
        {
            //vModel.zjhm = "211002198303042929";
            base.ChangeDB(SysConst.DB_CON_BDC);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            ProvidentFundModel model = new ProvidentFundModel();
            string qlrmc = "";
            string qlrmc1 = "";
            int count = 0;
            string con_result = "";
            string DyInfo = "";
            string Fifteen_zjhm = vModel.zjhm[0..6] + vModel.zjhm[8..^1];
            string UpperZjmh = vModel.zjhm.ToUpper();
            string LowerZjhm = vModel.zjhm.ToLower();

            var result = await base.Db.Queryable<DJ_QLRGL, DJ_QLR, DJ_DJB, DJ_SJD, QL_FWXG, DJ_XGDJGL, DJ_DY>((A, B, C, D, E, F, G) => new object[] { JoinType.Inner, A.QLRID == B.QLRID, JoinType.Inner, A.SLBH == C.SLBH, JoinType.Inner, C.SLBH == D.SLBH, JoinType.Inner, D.SLBH == E.SLBH, JoinType.Left, E.SLBH == F.FSLBH, JoinType.Left, F.ZSLBH == G.SLBH }).Where((A, B, C, D, E, F, G) => (B.ZJHM == UpperZjmh || B.ZJHM == LowerZjhm || B.ZJHM == Fifteen_zjhm) && (C.LIFECYCLE == 0 || C.LIFECYCLE == null) && A.QLRLX == "权利人").Select((A, B, C, D, E, F, G) => new
            {
                qlrmc = B.QLRMC,
                zjhm = B.ZJHM,
                bdczh = C.BDCZH,
                zl = D.ZL,
                djrq = C.DJRQ,
                jzmj = E.JZMJ,
                dyje = G.BDBZZQSE,
                qssj = G.QLQSSJ,
                jssj = G.QLJSSJ,
                dyqr = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(H => H.SLBH == G.SLBH && H.QLRLX == "抵押权人").Select(GG => GG.QLRMC)
            }).ToListAsync();

            if (vModel.zjhm1 != "")
            {
                string Fifteen_zjhm1 = vModel.zjhm1[0..6] + vModel.zjhm1[8..^1];
                //第二个权利人拥有的房屋
                var result1 = await base.Db.Queryable<DJ_QLRGL, DJ_QLR, DJ_DJB, DJ_SJD, QL_FWXG, DJ_XGDJGL, DJ_DY>((A, B, C, D, E, F, G) => new object[] { JoinType.Inner, A.QLRID == B.QLRID, JoinType.Inner, A.SLBH == C.SLBH, JoinType.Inner, C.SLBH == D.SLBH, JoinType.Inner, D.SLBH == E.SLBH, JoinType.Left, E.SLBH == F.FSLBH, JoinType.Left, F.ZSLBH == G.SLBH }).Where((A, B, C, D, E, F, G) => (B.ZJHM == UpperZjmh || B.ZJHM == LowerZjhm || B.ZJHM == Fifteen_zjhm) && (C.LIFECYCLE == 0 || C.LIFECYCLE == null) && A.QLRLX == "权利人").GroupBy((A, B, C, D, E, F, G) => new
                {
                    bdczh = C.BDCZH,
                    zl = D.ZL,
                    djrq = C.DJRQ,
                    jzmj = E.JZMJ,
                    dyje = G.BDBZZQSE,
                    qssj = G.QLQSSJ,
                    jssj = G.QLJSSJ
                }).Select((A, B, C, D, E, F, G) => new
                {
                    qlrmc = SqlFunc.MappingColumn(B.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(B.QLRMC))"),
                    zjhm = SqlFunc.MappingColumn(B.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(B.ZJHM))"),
                    bdczh = C.BDCZH,
                    zl = D.ZL,
                    djrq = C.DJRQ,
                    jzmj = E.JZMJ,
                    dyje = G.BDBZZQSE,
                    qssj = G.QLQSSJ,
                    jssj = G.QLJSSJ,
                    dyqr = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(H => H.SLBH == G.SLBH && H.QLRLX == "抵押权人").Select(GG => GG.QLRMC)
                }).ToListAsync();

                if (result.Count > 0 && result1.Count > 0)
                {
                    if (result.Count > result1.Count)
                    {
                        qlrmc = result[0].qlrmc;
                        count = result.Count;
                        for (int i = 0; i < result.Count; i++)
                        {
                            var strmj = String.Concat(Math.Abs(result[i].jzmj.Value));
                            var mjmask = strmj.Replace(strmj.Substring(strmj.IndexOf('.') - 1, 1), "*");
                            mjmask = mjmask.Substring(0, strmj.IndexOf('.') + 1) + "*";
                            if (result[i].dyqr != null)
                            {
                                DyInfo = ",抵押权人为：" + result[i].dyqr + ",抵押金额为：" + result[i].dyje + "元。" + "抵押起止时间为：" + Convert.ToDateTime(result[i].qssj).ToLongDateString() + "至" + Convert.ToDateTime(result[i].jssj).ToLongDateString() + "止。";
                            }
                            else
                            {
                                DyInfo = "，无抵押信息";
                            }
                            var strZl = result[i].zl.Substring(0, result[i].zl.Length - 5) + "****" + result[i].zl.Substring(result[i].zl.Length - 1);
                            con_result += i + 1 + ".不动产证号：" + result[i].bdczh + ",地址：" + strZl + ",建筑面积：" + mjmask + "平方米，登记日期：" + (result[i].djrq.HasValue ? "" : ((DateTime)result[i].djrq).ToLongDateString()) + DyInfo + Environment.NewLine;
                        }
                    }
                    else
                    {
                        qlrmc1 = result1[0].qlrmc;
                        count = result1.Count;
                        for (int i = 0; i < result1.Count; i++)
                        {
                            var strmj = String.Concat(Math.Abs(result1[i].jzmj.Value));
                            var mjmask = strmj.Replace(strmj.Substring(strmj.IndexOf('.') - 1, 1), "*");
                            mjmask = mjmask.Substring(0, strmj.IndexOf('.') + 1) + "*";
                            if (result[i].dyqr != null)
                            {
                                DyInfo = ",抵押权人为：" + result[i].dyqr + ",抵押金额为：" + result[i].dyje + "元。" + "抵押起止时间为：" + Convert.ToDateTime(result[i].qssj).ToLongDateString() + "至" + Convert.ToDateTime(result[i].jssj).ToLongDateString() + "止。";
                            }
                            else
                            {
                                DyInfo = "，无抵押信息";
                            }
                            var strZl = result1[i].zl.Substring(0, result1[i].zl.Length - 5) + "****" + result1[i].zl.Substring(result1[i].zl.Length - 1);
                            con_result += i + 1 + ".不动产证号：" + result1[i].bdczh + ",地址：" + strZl + ",建筑面积：" + mjmask + "平方米，登记日期：" + (result[i].djrq.HasValue ? "" : ((DateTime)result[i].djrq).ToLongDateString()) + DyInfo + Environment.NewLine;
                        }
                    }
                }
                else if(result.Count == 0)
                {
                    throw new ApplicationException("姓名为：" + vModel.qlrmc + "无房产信息");
                }
            }
            else
            {
                if (result.Count > 0)
                {
                    qlrmc = result[0].qlrmc;
                    count = result.Count;
                    for (int i = 0; i < result.Count; i++)
                    {
                        var strmj = String.Concat(Math.Abs(result[i].jzmj.Value));
                        var mjmask = strmj.Replace(strmj.Substring(strmj.IndexOf('.') - 1, 1), "*");
                        mjmask = mjmask.Substring(0, strmj.IndexOf('.') + 1) + "*";
                        var strZl = result[i].zl.Substring(0, result[i].zl.Length - 5) + "****" + result[i].zl.Substring(result[i].zl.Length - 1);
                        if (result[i].dyqr != null )
                        {
                            DyInfo = ",抵押权人为：" + result[i].dyqr + ",抵押金额为：" + result[i].dyje + "元。" + "抵押起止时间为：" + Convert.ToDateTime(result[i].qssj).ToLongDateString() + "至" + Convert.ToDateTime(result[i].jssj).ToLongDateString() + "止。"; ;
                        }
                        else
                        {
                            DyInfo = "，无抵押信息";
                        }


                        con_result += i + 1 + ".不动产证号：" + result[i].bdczh + ",地址：" + strZl + ",建筑面积：" + mjmask + "平方米，登记日期：" + (result[i].djrq.HasValue ? "" : ((DateTime)result[i].djrq).ToLongDateString()) + DyInfo + Environment.NewLine;
                    }
                }
                else
                {
                    throw new ApplicationException("姓名为：" + vModel.qlrmc + "无房产信息");
                }


            }
            //未输入第二个权利人姓名，将权利人名称赋值
            if (vModel.qlrmc == "")
            {
                model.qlrmc = qlrmc;
            }
            else
            {
                model.qlrmc = vModel.qlrmc;
            }
            model.zjhm = vModel.zjhm;

            if (vModel.qlrmc1 == "")
                model.qlrmc1 = qlrmc1;
            else
                model.qlrmc1 = vModel.qlrmc1;
            model.zjhm1 = vModel.zjhm1;
            if (count > 0)
            {
                model.result = "　　　　经查，在辽阳市不动产登记中心数据库中存在" + count + "条房屋登记记录。" + Environment.NewLine + con_result;
                model.housecount = count;
            }
            else
            {
                model.result = "　　　　经查，在辽阳市不动产登记中心数据库中不存在房屋登记记录。";
                model.housecount = 0;
            }
            model.queryDate = DateTime.Now;
            model.conditionQuery = vModel.qlrmc + ";" + vModel.zjhm + ";" + vModel.qlrmc1 + ";" + vModel.zjhm1;
            model.org_name = vModel.org_name;
            model.user_name = vModel.user_name;
            model.pid = Guid.NewGuid();

            return model;
        }

        /// <summary>
        /// 获取公积金查询数据
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        public async Task<List<ProvidentFundModel>> GetProvidentFundList(string slbh)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            List<ProvidentFundModel> modelList = new List<ProvidentFundModel>();
            ProvidentFundModel model = null; 

            var result = await base.Db.Queryable<DJ_DJB, DJ_QLRGL, DJ_QLR>((A, B, C) => new object[] { JoinType.Inner, A.SLBH == B.SLBH, JoinType.Inner, B.QLRID == C.QLRID }).Where((A, B, C) => (A.SLBH == slbh && (A.LIFECYCLE == 0 || A.LIFECYCLE == null) && B.QLRLX == "权利人")).Select((A, B, C) => new
            {
                qlrmc = C.QLRMC,
                zjhm = C.ZJHM,
                bdczh = A.BDCZH
            }).ToListAsync();

            if(result.Count>0)
            {
                foreach (var item in result)
                {
                    model = new ProvidentFundModel();
                    model.qlrmc = item.qlrmc;
                    model.zjhm = item.zjhm;
                    model.result = "经查，在辽阳市不动产登记中心数据库中存在房屋登记记录。";
                    modelList.Add(model);
                }
            }
            
            
            return modelList;
        }

        

        /// <summary>
        /// 将查询数据入库
        /// </summary>
        /// <param name="models"></param>
        /// <param name="qlrmc"></param>
        /// <param name="zjhm"></param>
        /// <returns></returns>
        public ProvidentFundModel GetProvidentFundModel(List<ProvidentFundModel> models,string qlrmc,string zjhm)
        {
            ProvidentFundModel model = new ProvidentFundModel();
            model.pid = Guid.NewGuid();
            model.qlrmc = qlrmc;
            model.zjhm = zjhm;
            if(models.Count > 0)
            {
                model.result = "经查，在辽阳市不动产登记中心数据库中存在" + models .Count + "条件房屋登记记录。";
            }
            else
            {
                model.result = "经查，在辽阳市不动产登记中心数据库中未见房屋登记记录。";
            }
            model.queryDate = DateTime.Now;
            model.conditionQuery = qlrmc + ";" + zjhm;
            return model;
        }


    }
}
