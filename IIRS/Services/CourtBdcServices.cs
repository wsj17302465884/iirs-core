using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.ViewModel.LAW;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
//using static IIRS.Models.ViewModel.LAW.LawBdcVModel.Message;

namespace IIRS.Services
{
    /// <summary>
    /// 法院接口
    /// </summary>
    public class CourtBdcServices : BaseServices, ICourtBdcServices
    {
        private readonly ILogger<CourtBdcServices> _logger;
        private readonly IDBTransManagement _dbTransManagement;
        public CourtBdcServices(IDBTransManagement dbTransManagement, ILogger<CourtBdcServices> logger) : base(dbTransManagement)
        {
            _logger = logger;
            _dbTransManagement = dbTransManagement;
        }

        public async Task<Message> GetLawList(string qlrmc, string zjhm)
        {
            //qlrmc = "辽阳红美置业有限公司";
            //zjhm = "58073473-9";
            base.ChangeDB(SysConst.DB_CON_BDC);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            Message model = new Message();
            Data dataModel = new Data();
            Head headModel = new Head();
            BDCCXJG BdccxjgModel = new BDCCXJG();
            try
            {
                #region 房屋权属
                var FcResult = await base.Db.Queryable<DJ_DJB, DJ_SJD, QL_FWXG, DJ_QLRGL, DJ_TSGL, FC_H_QSDC>((a, b, c, d, e, f) => new object[] { JoinType.Inner, a.SLBH == b.SLBH, JoinType.Inner, b.SLBH == c.SLBH, JoinType.Inner, c.SLBH == d.SLBH, JoinType.Inner, e.SLBH == a.SLBH, JoinType.Inner, e.TSTYBM == f.TSTYBM }).Where((a, b, c, d, e, f) => d.QLRMC == qlrmc && d.ZJHM == zjhm && (d.LIFECYCLE == 0 || d.LIFECYCLE == null) && (d.QLRLX == "权利人" || d.QLRLX == null) && (e.LIFECYCLE == 0 || e.LIFECYCLE == null) && e.BDCLX == "房屋").Select((a, b, c, d, e, f) => new
                {
                    BDCDYH = a.BDCDYH,
                    FDZL = b.ZL,
                    JZMJ = c.JZMJ,
                    GHYT = c.GHYT,
                    FWXZ = c.QLXZ,
                    TDSYQSSJ = f.TDQSRQ,
                    TDSYJSSJ = f.TDZZRQ,
                    BDCQZH = a.BDCZH,
                    JGJC = a.JGJC,
                    FEBL = d.GYFE,
                    YWH = a.SLBH

                }).ToListAsync();

                foreach (var fw in FcResult)
                {
                    var QlrResult = await QueryMuch<DJ_QLRGL, DJ_DJB, DJ_SJD, QLRXX>((a, b, c) => new object[]
                    {
                        JoinType.Inner, a.SLBH == b.SLBH,
                        JoinType.Inner, b.SLBH == c.SLBH
                    },
                    (a, b, c) => new QLRXX()
                    {
                        BDCDYH = b.BDCDYH,
                        SXH = SqlFunc.IF(a.SXH == null).Return("!@#$$$^YUNSUN*").End(a.SXH.ToString()),
                        QLRMC = a.QLRMC,
                        BDCQZH = b.BDCZH,
                        ZJZL = a.ZJLB,
                        ZJH = a.ZJHM,
                        FZJG = b.FZJG,
                        DH = a.DH,
                        QLRLX = a.QLRLX,
                        GYFS = a.GYFS,
                        YWH = a.SLBH
                    },
                    (a, b, c) => a.SLBH == fw.YWH
                    );

                    BdccxjgModel.FDCQLIST.Add(new FDCQ
                    {
                        BDCDYH = fw.BDCDYH,
                        FDZL = fw.FDZL,
                        JZMJ = fw.JZMJ.ToString() != "" ? fw.JZMJ.ToString() : "",
                        FWXZ = fw.FWXZ,
                        TDSYQSSJ = fw.TDSYQSSJ.ToString() != "" ? fw.TDSYQSSJ.ToString() : "",
                        TDSYJSSJ = fw.TDSYJSSJ.ToString() != "" ? fw.TDSYJSSJ.ToString() : "",
                        BDCQZH = fw.BDCQZH,
                        DJJG = fw.JGJC,
                        FEBL = fw.FEBL,
                        YWH = fw.YWH,
                        QLRXXLIST = QlrResult
                    });
                }
                #endregion

                #region 土地权属
                var ZdResult = await base.Db.Queryable<DJ_DJB, DJ_SJD, QL_TDXG, DJ_QLRGL, DJ_TSGL>((a, b, c, d, e) => new object[] { JoinType.Inner, a.SLBH == b.SLBH, JoinType.Inner, b.SLBH == c.SLBH, JoinType.Inner, c.SLBH == d.SLBH, JoinType.Inner, e.SLBH == a.SLBH }).Where((a, b, c, d, e) => d.QLRMC == qlrmc && d.ZJHM == zjhm && (d.LIFECYCLE == 0 || d.LIFECYCLE == null) && (d.QLRLX == "权利人" || d.QLRLX == null) && (e.LIFECYCLE == 0 || e.LIFECYCLE == null) && e.BDCLX == "宗地").Select((a, b, c, d, e) => new
                {
                    BDCDYH = a.BDCDYH,
                    ZL = b.ZL,
                    SYQMJ = c.GYTDMJ,
                    SYQMJ1 = c.DYTDMJ,
                    MJDW = "平方米",
                    YT = c.TDYT,
                    QLXZ = c.QLXZ,
                    BDCQZH = a.BDCZH,
                    DJJG = a.JGJC,
                    FEBL = d.GYFE,
                    YWH = a.SLBH
                }).ToListAsync();

                foreach (var zd in ZdResult)
                {
                    var QlrResult = await QueryMuch<DJ_QLRGL, DJ_DJB, DJ_SJD, QLRXX>((a, b, c) => new object[]
                    {
                        JoinType.Inner, a.SLBH == b.SLBH,
                        JoinType.Inner, b.SLBH == c.SLBH
                    },
                    (a, b, c) => new QLRXX()
                    {
                        BDCDYH = b.BDCDYH,
                        SXH = SqlFunc.IF(a.SXH==null).Return("!@#$$$^YUNSUN*").End(a.SXH.ToString()),
                        QLRMC = a.QLRMC,
                        BDCQZH = b.BDCZH,
                        ZJZL = a.ZJLB,
                        ZJH = a.ZJHM,
                        FZJG = b.FZJG,
                        DH = a.DH,
                        QLRLX = a.QLRLX,
                        GYFS = a.GYFS,
                        YWH = a.SLBH
                    },
                    (a, b, c) => a.SLBH == zd.YWH
                    );
                    BdccxjgModel.JSYDSYQLIST.Add(new JSYDSYQ
                    {
                        BDCDYH = zd.BDCDYH,
                        ZL = zd.ZL,
                        YT = zd.YT,
                        QLXZ = zd.QLXZ,
                        BDCQZH = zd.BDCQZH,
                        DJJG = zd.DJJG,
                        FEBL = zd.FEBL,
                        YWH = zd.YWH,
                        QLRXXLIST = QlrResult
                    });
                }
                #endregion

                #region 抵押信息
                var DyResult = base.Db.Queryable<DJ_QLRGL, DJ_TSGL, DJ_DY, DJ_SJD>((a, b, c, d) => new object[] { JoinType.Inner, a.SLBH == b.SLBH, JoinType.Inner, b.SLBH == c.SLBH, JoinType.Inner, c.SLBH == d.SLBH}).Where((a, b, c, d) => a.QLRMC == qlrmc && a.ZJHM == zjhm && (a.LIFECYCLE == 0 || a.LIFECYCLE == null) && (b.LIFECYCLE == 0 || b.LIFECYCLE == null) && a.QLRLX == "抵押人" && b.DJZL == "抵押").Select((a, b, c, d) => new
                {
                    BDCDYH = c.BDCDYH,
                    DYBDCLX = 1,
                    ZL = d.ZL,
                    DYR = a.QLRMC,
                    DYFS = c.DYFS,
                    ZWLXQSSJ = c.QLQSSJ,
                    ZWLXJSSJ = c.QLJSSJ,
                    BDBZZQSE = c.BDBZZQSE,
                    BDCDJZMH = c.BDCZMH,
                    DJJG = c.JGJC,
                    YWH = c.SLBH
                }).ToList();

                foreach (var DyItem in DyResult)
                {
                    BdccxjgModel.DYAQLIST.Add(new DYAQ
                    {
                        BDCDYH = DyItem.BDCDYH,
                        DYBDCLX = DyItem.DYBDCLX.ToString() != "" ? DyItem.DYBDCLX.ToString() : "",
                        ZL = DyItem.ZL,
                        DYR = DyItem.DYR,
                        DYFS = DyItem.DYFS,
                        ZWLXQSSJ = DyItem.ZWLXQSSJ.ToString() != "" ? DyItem.ZWLXQSSJ.ToString() : "",
                        ZWLXJSSJ = DyItem.ZWLXJSSJ.ToString() != "" ? DyItem.ZWLXJSSJ.ToString() : "",
                        BDCDJZMH = DyItem.BDCDJZMH,
                        DJJG = DyItem.DJJG,
                        YWH = DyItem.YWH
                    });
                }
                #endregion

                #region 查封信息
                var CfResult = base.Db.Queryable<DJ_QLRGL, DJ_TSGL, DJ_CF, DJ_SJD>((a, b, c, d) => new object[] { JoinType.Inner, a.SLBH == b.SLBH, JoinType.Inner, b.SLBH == c.SLBH, JoinType.Inner, c.SLBH == d.SLBH }).Where((a, b, c, d) => a.QLRMC == qlrmc && a.ZJHM == zjhm && (a.LIFECYCLE == 0 || a.LIFECYCLE == null)  && (b.LIFECYCLE == 0 || b.LIFECYCLE == null) && b.DJZL == "查封").Select((a, b, c, d) => new
                {
                    BDCDYH = c.BDCDYH,
                    ZL = d.ZL,
                    CFJG = c.CFJG,
                    CFLX = c.CFLX,
                    CFWH = c.CFWH,
                    CFQSSJ = c.CFQSSJ,
                    CFJSSJ = c.CFJSSJ,
                    DJJG = c.DJJG,
                    YWH = c.SLBH
                }).ToList();

                string cflx = "";
                foreach (var CfItem in CfResult)
                {
                    if(CfItem.CFLX == "查封")
                    {
                        cflx = "1";
                    }else if(CfItem.CFLX == "轮候查封")
                    {
                        cflx = "2";
                    }
                    else if (CfItem.CFLX == "预查封")
                    {
                        cflx = "3";
                    }
                    else if (CfItem.CFLX == "轮候预查封")
                    {
                        cflx = "4";
                    }
                    BdccxjgModel.CFDJLIST.Add(new CFDJ
                    {
                        BDCDYH = CfItem.BDCDYH,
                        ZL = CfItem.ZL,
                        CFJG = CfItem.CFJG,
                        CFLX = cflx,
                        CFWH = CfItem.CFWH,
                        CFQSSJ = CfItem.CFQSSJ.ToString() != "" ? CfItem.CFQSSJ.ToString() : "",
                        CFJSSJ = CfItem.CFJSSJ.ToString() != "" ? CfItem.CFJSSJ.ToString() : "",
                        DJJG = CfItem.DJJG,
                        YWH = CfItem.YWH
                    });
                }
                #endregion

                #region 预告信息
                var YgResult = base.Db.Queryable<DJ_QLRGL, DJ_TSGL, DJ_YG, DJ_SJD, QL_FWXG>((a, b, c, d, e) => new object[] { JoinType.Inner, a.SLBH == b.SLBH, JoinType.Inner, b.SLBH == c.SLBH, JoinType.Inner, c.SLBH == d.SLBH, JoinType.Inner, e.SLBH == b.SLBH}).Where((a, b, c, d, e) => a.QLRMC == qlrmc && a.ZJHM == zjhm && (a.LIFECYCLE == 0 || a.LIFECYCLE == null) && (b.LIFECYCLE == 0 || b.LIFECYCLE == null) && (c.LIFECYCLE == 0 || c.LIFECYCLE == null) && b.BDCLX == "预告").Select((a, b, c, d, e) => new
                {
                    BDCDYH = c.BDCDYH,
                    YGDJZL = c.DJYY,
                    ZL = d.ZL,
                    GHYT = e.GHYT,
                    JZMJ = e.JZMJ,
                    BDCDJZMH = c.BDCZMH,
                    DJJG = c.SPDW,
                    YWH = a.SLBH
                }).ToList();

                string djzl = "";
                foreach (var yg in YgResult)
                {
                    if(yg.YGDJZL.Contains("抵押"))
                    {
                        djzl = "3";
                    }else
                    {
                        djzl = "1";
                    }
                    var QlrResult = base.Db.Queryable<DJ_QLRGL, DJ_DJB, DJ_SJD>((a, b, c) => new object[] { JoinType.Inner, a.SLBH == b.SLBH, JoinType.Inner, b.SLBH == c.SLBH, JoinType.Inner}).Where((a, b, c) => a.SLBH == yg.YWH).Select((a, b, c) => new QLRXX
                    {
                        BDCDYH = b.BDCDYH,
                        SXH = SqlFunc.IF(a.SXH == null).Return("!@#$$$^YUNSUN*").End(a.SXH.ToString()),
                        QLRMC = a.QLRMC,
                        BDCQZH = b.BDCZH,
                        ZJZL = a.ZJLB,
                        ZJH = a.ZJHM,
                        FZJG = b.FZJG,
                        DH = a.DH,
                        QLRLX = a.QLRLX,
                        GYFS = a.GYFS,
                        YWH = a.SLBH
                    }).ToList();

                    BdccxjgModel.YGDJLIST.Add(new YGDJ
                    {
                        BDCDYH = yg.BDCDYH,
                        YGDJZL = djzl,
                        ZL = yg.ZL,
                        GHYT = yg.GHYT,
                        JZMJ = yg.JZMJ.ToString() != "" ? yg.JZMJ.ToString() :"",
                        BDCDJZMH = yg.BDCDJZMH,
                        DJJG = yg.DJJG,
                        YWH = yg.YWH,
                        QLRXXLIST = QlrResult
                    });
                }

                #endregion

                #region 异议信息
                var YyResult = base.Db.Queryable<DJ_QLRGL, DJ_TSGL, DJ_YY, DJ_SJD>((a, b, c, d) => new object[] { JoinType.Inner, a.SLBH == b.SLBH, JoinType.Inner, b.SLBH == c.SLBH, JoinType.Inner, c.SLBH == d.SLBH }).Where((a, b, c, d) => a.QLRMC == qlrmc && a.ZJHM == zjhm && (a.LIFECYCLE == 0 || a.LIFECYCLE == null) && (b.LIFECYCLE == 0 || b.LIFECYCLE == null) && b.DJZL == "异议").Select((a, b, c, d) => new
                {
                    BDCDYH = c.BDCDYH,
                    ZL = d.ZL,
                    YYSQR = a.QLRMC,
                    YYSX = c.YYSX,
                    BDCDJZMH = c.BDCZMH,
                    DJSJ = c.DJRQ,
                    BEIZ = c.SPBZ,
                    YWH = c.SLBH
                }).ToList();

                foreach (var YyItem in YyResult)
                {                    
                    BdccxjgModel.YYXXLIST.Add(new YYXX
                    {
                        BDCDYH = YyItem.BDCDYH,
                        ZL = YyItem.ZL,
                        YYSQR = YyItem.YYSQR,
                        YYSX = YyItem.YYSX,
                        BDCDJZMH = YyItem.BDCDJZMH,
                        DJSJ = YyItem.DJSJ.ToString() != "" ? YyItem.DJSJ.ToString() : "",
                        BEIZ = YyItem.BEIZ,
                        YWH = YyItem.YWH
                    });
                }
                #endregion

                
                headModel.CREATETIME = DateTime.Now.ToShortDateString();
                headModel.RESPONSECODE = "0000";
                headModel.RESPONSEINFO = "!@#$$$^YUNSUN*";
                BdccxjgModel.CXQQDH = "20111128320000100002";
                if (BdccxjgModel.JSYDSYQLIST.Count == 0 && BdccxjgModel.FDCQLIST.Count == 0)
                {                    
                    BdccxjgModel.RESULT = "0100";
                }
                else
                {
                    BdccxjgModel.RESULT = "0000";
                }
                
                dataModel.BDCFKLIST.Add(BdccxjgModel);
                model.Data = dataModel;
                model.Head = headModel;

                return  model;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }

        public async Task<DJ_TSGL> GetTsglListByZjhm(string qlrmc, string zjhm)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            List<DJ_TSGL> modelList = new List<DJ_TSGL>();
            DJ_TSGL model = null;
            string DjbSLbh = "";

            var Result = await base.Db.Queryable<DJ_QLRGL, DJ_TSGL>((a, b) => new object[] { JoinType.Inner, a.SLBH == b.SLBH }).Where((a, b) => a.ZJHM == zjhm && a.QLRMC == qlrmc && (b.LIFECYCLE == null || b.LIFECYCLE == 0)).Select((a, b) => new
            {
                SLBH = a.SLBH,
                BDCLX = b.BDCLX,
                TSTYBM = b.TSTYBM,
                BDCDYH = b.BDCDYH,
                DJZL = b.DJZL
            }).ToListAsync();

            for (int i = 0; i < Result.Count; i++)
            {
                if(Result[i].DJZL == "权属")
                {
                    DjbSLbh = DjbSLbh + ",";
                }
            }

            foreach (var i in Result)
            {
                model = new DJ_TSGL();
                model.SLBH = i.SLBH;
                model.BDCLX = i.BDCLX;
                model.TSTYBM = i.TSTYBM;
                model.BDCDYH = i.BDCDYH;
                model.DJZL = i.DJZL;
                modelList.Add(model);
            }



            return null;
        }

        public string GetSqlIn(string sqlParam, string columnName)
        {
            int width = sqlParam.IndexOf("'", 1) - 1;
            string temp = string.Empty;

            for (int i = 0; i < sqlParam.Length; i += 1000 * (width + 3))
            {
                if (i + 1000 * (width + 3) - 1 < sqlParam.Length)
                {
                    temp = temp + sqlParam.Substring(i, 1000 * (width + 3) - 1)
                    + ") OR " + columnName + " IN (";
                }
                else
                {
                    temp = temp + sqlParam.Substring(i, sqlParam.Length - i);
                }
            }

            return temp;
        }
    }
}
