using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.IServices;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using static IIRS.Models.ViewModel.ROWS_ring;

namespace IIRS.Services
{
    public class ChartServices : BaseServices, IChartServices
    {
        private readonly ILogger<ChartServices> _logger;
        private readonly IDBTransManagement _dbTransManagement;
        private readonly IRegistration_infoRepository _Registration_infoRepository;
        public ChartServices(IDBTransManagement dbTransManagement, ILogger<ChartServices> logger , IRegistration_infoRepository Registration_infoRepository) : base(dbTransManagement)
        {
            _logger = logger;
            _dbTransManagement = dbTransManagement;
            _Registration_infoRepository = Registration_infoRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="Loginregid"></param>
        /// <returns></returns>
        public async Task<ChartVModel> GetDataCounts(string date,Guid Loginregid )
        {
            ChartData_ring ring = new ChartData_ring();
            ChartData_pre pre = new ChartData_pre();
            ChartData_line line = new ChartData_line();
            DataItem data = new DataItem();
            DateTime datetime = Convert.ToDateTime(date);
            datetime = datetime.Date;

            /*      var nowdate = new DateTime();*/
            DateTime nowdate = DateTime.Now;
            DateTime thistime;
            DateTime lastyear;
            DateTime Clastyear;
            DateTime lasttwoyear;
            var thisyearresult = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE < nowdate&&IN.SAVEDATE>nowdate.AddYears(-1)).ToList();
            var LASTyearresult = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE < nowdate.AddYears(-1) && IN.SAVEDATE > nowdate.AddYears(-2)).ToList();
            //改变日期的月份，减去一个月份   
            thistime = DateTime.Parse(DateTime.Parse(datetime.ToString("yyyy-MM-dd")).AddMonths(-1).ToShortDateString());
                thistime = DateTime.Parse(DateTime.Parse(thistime.ToString("yyyy-MM-dd")).AddDays(-1).ToShortDateString());
                var result = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE > thistime).ToList();
            //本月，减去一个月份   
            nowdate = DateTime.Parse(DateTime.Parse(nowdate.ToString("yyyy-MM-dd")).AddMonths(-1).ToShortDateString());
            nowdate = DateTime.Parse(DateTime.Parse(nowdate.ToString("yyyy-MM-dd")).AddDays(-1).ToShortDateString());
            var newresult = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE > nowdate).ToList();
            
            #region
            //上一个月
            DateTime lasttime;
                lasttime = DateTime.Parse(DateTime.Parse(nowdate.ToString("yyyy-MM-dd")).AddMonths(-1).ToShortDateString());
                lasttime = DateTime.Parse(DateTime.Parse(lasttime.ToString("yyyy-MM-dd")).AddDays(-1).ToShortDateString());
                var lastresult = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE < nowdate && IN.SAVEDATE > lasttime).ToList();
                //上二个月
                DateTime lasttime2;
                lasttime2 = DateTime.Parse(DateTime.Parse(lasttime.ToString("yyyy-MM-dd")).AddMonths(-1).ToShortDateString());
                lasttime2 = DateTime.Parse(DateTime.Parse(lasttime2.ToString("yyyy-MM-dd")).AddDays(-1).ToShortDateString());
                var lastresult2 = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE < lasttime && IN.SAVEDATE > lasttime2).ToList();
                //上三个月
                DateTime lasttime3;
                lasttime3 = DateTime.Parse(DateTime.Parse(lasttime2.ToString("yyyy-MM-dd")).AddMonths(-1).ToShortDateString());
                lasttime3 = DateTime.Parse(DateTime.Parse(lasttime3.ToString("yyyy-MM-dd")).AddDays(-1).ToShortDateString());
                var lastresult3 = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE < lasttime2 && IN.SAVEDATE > lasttime3).ToList();
                //上四个月
                DateTime lasttime4;
                lasttime4 = DateTime.Parse(DateTime.Parse(lasttime3.ToString("yyyy-MM-dd")).AddMonths(-1).ToShortDateString());
                lasttime4 = DateTime.Parse(DateTime.Parse(lasttime4.ToString("yyyy-MM-dd")).AddDays(-1).ToShortDateString());
                var lastresult4 = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE < lasttime3 && IN.SAVEDATE > lasttime4).ToList();
                //上五个月
                DateTime lasttime5;
                lasttime5 = DateTime.Parse(DateTime.Parse(lasttime4.ToString("yyyy-MM-dd")).AddMonths(-1).ToShortDateString());
                lasttime5 = DateTime.Parse(DateTime.Parse(lasttime5.ToString("yyyy-MM-dd")).AddDays(-1).ToShortDateString());
                var lastresult5 = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE < lasttime4 && IN.SAVEDATE > lasttime5).ToList();
                //上六个月
                DateTime lasttime6;
                lasttime6 = DateTime.Parse(DateTime.Parse(lasttime5.ToString("yyyy-MM-dd")).AddMonths(-1).ToShortDateString());
                lasttime6 = DateTime.Parse(DateTime.Parse(lasttime6.ToString("yyyy-MM-dd")).AddDays(-1).ToShortDateString());
                var lastresult6 = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE < lasttime5 && IN.SAVEDATE > lasttime6).ToList();
            #endregion
            //发生日期改变的上一年
            Clastyear = DateTime.Parse(DateTime.Parse(datetime.ToString("yyyy-MM-dd")).AddYears(1).ToShortDateString());
            //不发生日期改变的上一年
            lastyear = DateTime.Parse(DateTime.Parse(nowdate.ToString("yyyy-MM-dd")).AddMonths(1).AddYears(-1).ToShortDateString());
            //不发生日期改变的前两年
            lasttwoyear = DateTime.Parse(DateTime.Parse(nowdate.ToString("yyyy-MM-dd")).AddMonths(1).AddYears(-2).ToShortDateString());
            var dycounts = 0;
                var zydycounts = 0;
                var dyzxcounts = 0;
            var Cdycounts = 0;
            var Czydycounts = 0;
            var Cdyzxcounts = 0;
            var lastdycounts = 0;
                var lastzydycounts = 0;
                var lastdyzxcounts = 0;
                var lastdycounts2 = 0;
                var lastzydycounts2 = 0;
                var lastdyzxcounts2 = 0;
                var lastdycounts3 = 0;
                var lastzydycounts3 = 0;
                var lastdyzxcounts3 = 0;
                var lastdycounts4 = 0;
                var lastzydycounts4 = 0;
                var lastdyzxcounts4 = 0;
                var lastdycounts5 = 0;
                var lastzydycounts5 = 0;
                var lastdyzxcounts5 = 0;
                var lastdycounts6 = 0;
                var lastzydycounts6 = 0;
                var lastdyzxcounts6 = 0;
                var lastyeardycounts = 0;
                var lastyearzydycounts = 0;
                var lastyeardyzxcounts = 0;
                var lastyearresult = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE < nowdate.AddMonths(1) && IN.SAVEDATE > lastyear).ToList();
            var lasttwoyearresult = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE < lastyear && IN.SAVEDATE > lasttwoyear).ToList();
            var Clastyearresult = base.Db.Queryable<REGISTRATION_INFO>().Where(IN => IN.SAVEDATE > datetime && IN.SAVEDATE < Clastyear).ToList();
            /*   base.Db.Aop.OnLogExecuting = (sql, pars) =>
                        {
                            _logger.LogDebug(sql);
                        };*/
            //时间变化表格数据
            var USERLIST = await base.Db.Queryable<Sys_Organization_top, Sys_Userinfo, Sys_UserOrganization, REGISTRATION_INFO>((A, B, C, D) => B.Id == C.UserId && C.OrgId == Loginregid && A.Id == C.OrgId)
                       .Where((A, B, C, D) => B.Id.ToString() == D.USER_ID&&D.SAVEDATE<Clastyear&&D.SAVEDATE> datetime)
                        .GroupBy((A, B, C, D) => new
                        {
                            realname = B.RealName,
                            userid = D.USER_ID,
                            djzl = D.DJZL,
                            count = SqlFunc.AggregateCount(D.DJZL)
                        }).Select((A, B, C, D) => new ChartTableVModel()
                        {
                            REALNAME = B.RealName,
                            USERID = D.USER_ID,
                            DJZL = D.DJZL,
                            COUNT = SqlFunc.AggregateCount(D.DJZL)
                        }).ToListAsync();
            //页面加载表格数据
            var date1 = nowdate.AddDays(1).AddMonths(1);
            var USERLIST2 = await base.Db.Queryable<Sys_Organization_top, Sys_Userinfo, Sys_UserOrganization, REGISTRATION_INFO>((A, B, C, D) => B.Id == C.UserId && C.OrgId == Loginregid && A.Id == C.OrgId)
           .Where((A, B, C, D) => B.Id.ToString() == D.USER_ID && D.SAVEDATE < date1 && D.SAVEDATE >lastyear )
            .GroupBy((A, B, C, D) => new
            {
                realname = B.RealName,
                userid = D.USER_ID,
                djzl = D.DJZL,
                count = SqlFunc.AggregateCount(D.DJZL)
            }).Select((A, B, C, D) => new ChartTableVModel()
            {
                REALNAME = B.RealName,
                USERID = D.USER_ID,
                DJZL = D.DJZL,
                COUNT = SqlFunc.AggregateCount(D.DJZL)
            }).ToListAsync();
            var USERLISTS = await base.Db.Queryable<Sys_Organization_top, Sys_Userinfo, Sys_UserOrganization, REGISTRATION_INFO>((A, B, C, D) => B.Id == C.UserId && C.OrgId == Loginregid && A.Id == C.OrgId)
           .Where((A, B, C, D) => B.Id.ToString() == D.USER_ID)
            .GroupBy((A, B, C, D) => new
            {
                realname = B.RealName,
                userid = D.USER_ID,
                count = SqlFunc.AggregateCount(D.DJZL)
            }).Select((A, B, C, D) => new ChartTableVModel()
            {
                REALNAME = B.RealName,
                USERID = D.USER_ID,
                COUNT = SqlFunc.AggregateCount(D.DJZL)
            }).ToListAsync();


               
            var aa = nowdate.AddDays(1).AddMonths(1) == datetime;


                if (newresult.Count > 0)
                {
                    foreach (var re in newresult)
                    {
                        if (re.DJZL == 23)
                        {
                            dycounts += 1;
                        }
                        if (re.DJZL == 21)
                        {
                            zydycounts += 1;
                        }
                        if (re.DJZL == 24)
                        {
                            dyzxcounts += 1;
                        }


                    }
                }
            if (!aa)
            {
                if (USERLIST.Count > 0)
                {

                    for (var i = 0; i < USERLISTS.Count; i++)
                    {
                        var ZY = 0;
                        var DY = 0;
                        var ZX = 0;
                        var a = USERLISTS[i].REALNAME;
                        for (var j = 0; j < USERLIST.Count; j++)
                        {
                            if (a == USERLIST[j].REALNAME)
                            {
                                if (USERLIST[j].DJZL == 21)
                                {
                                    ZY = USERLIST[j].COUNT;
                                }
                                else if (USERLIST[j].DJZL == 23)
                                {
                                    DY = USERLIST[j].COUNT;
                                }
                                else if (USERLIST[j].DJZL == 24)
                                {
                                    ZX = USERLIST[j].COUNT;
                                }
                            }
                        }
                        data.ITEM.Add(new TableData()
                        {
                            username = USERLISTS[i].REALNAME,
                            mortgage = DY,
                            cancellation = ZX,
                            transfer = ZY,
                        });

                    }
                }
                foreach (var re in Clastyearresult)
                {
                    if (re.DJZL == 23)
                    {
                        Cdycounts += 1;
                    }
                    if (re.DJZL == 21)
                    {
                        Czydycounts += 1;
                    }
                    if (re.DJZL == 24)
                    {
                        Cdyzxcounts += 1;
                    }


                }
                ring.rows.Add(new ROWS_ring()
                {
                    dept = "抵押",
                    count = Cdycounts,
                });
                ring.rows.Add(new ROWS_ring()
                {
                    dept = "转移及抵押",
                    count = Czydycounts,
                });
                ring.rows.Add(new ROWS_ring()
                {
                    dept = "注销",
                    count = Cdyzxcounts,
                });
            }
            else
            {
                if (USERLIST2.Count > 0)
                {

                    for (var i = 0; i < USERLISTS.Count; i++)
                    {
                        var ZY = 0;
                        var DY = 0;
                        var ZX = 0;
                        var a = USERLISTS[i].REALNAME;
                        for (var j = 0; j < USERLIST2.Count; j++)
                        {
                            if (a == USERLIST2[j].REALNAME)
                            {
                                if (USERLIST2[j].DJZL == 21)
                                {
                                    ZY = USERLIST2[j].COUNT;
                                }
                                else if (USERLIST2[j].DJZL == 23)
                                {
                                    DY = USERLIST2[j].COUNT;
                                }
                                else if (USERLIST2[j].DJZL == 24)
                                {
                                    ZX = USERLIST2[j].COUNT;
                                }
                            }
                        }
                        data.ITEM.Add(new TableData()
                        {
                            username = USERLISTS[i].REALNAME,
                            mortgage = DY,
                            cancellation = ZX,
                            transfer = ZY,
                        });

                    }
                }
                ring.rows.Add(new ROWS_ring()
                {
                    dept = "抵押",
                    count = dycounts,
                });
                ring.rows.Add(new ROWS_ring()
                {
                    dept = "转移及抵押",
                    count = zydycounts,
                });
                ring.rows.Add(new ROWS_ring()
                {
                    dept = "注销",
                    count = dyzxcounts,
                });
            }
                if (lastresult.Count > 0)
                {
                    foreach (var re in lastresult)
                    {
                        if (re.DJZL == 23)
                        {
                            lastdycounts += 1;
                        }
                        if (re.DJZL == 22)
                        {
                            lastzydycounts += 1;
                        }
                        if (re.DJZL == 24)
                        {
                            lastdyzxcounts += 1;
                        }
                    }
                }
                if (lastresult2.Count > 0)
                {
                    foreach (var re in lastresult2)
                    {
                        if (re.DJZL == 23)
                        {
                            lastdycounts2 += 1;
                        }
                        if (re.DJZL == 22)
                        {
                            lastzydycounts2 += 1;
                        }
                        if (re.DJZL == 24)
                        {
                            lastdyzxcounts2 += 1;
                        }
                    }
                }
                if (lastresult3.Count > 0)
                {
                    foreach (var re in lastresult3)
                    {
                        if (re.DJZL == 23)
                        {
                            lastdycounts3 += 1;
                        }
                        if (re.DJZL == 22)
                        {
                            lastzydycounts3 += 1;
                        }
                        if (re.DJZL == 24)
                        {
                            lastdyzxcounts3 += 1;
                        }
                    }
                }
                if (lastresult4.Count > 0)
                {
                    foreach (var re in lastresult4)
                    {
                        if (re.DJZL == 23)
                        {
                            lastdycounts4 += 1;
                        }
                        if (re.DJZL == 22)
                        {
                            lastzydycounts4 += 1;
                        }
                        if (re.DJZL == 24)
                        {
                            lastdyzxcounts4 += 1;
                        }
                    }
                }
                if (lastresult5.Count > 0)
                {
                    foreach (var re in lastresult5)
                    {
                        if (re.DJZL == 23)
                        {
                            lastdycounts5 += 1;
                        }
                        if (re.DJZL == 22)
                        {
                            lastzydycounts5 += 1;
                        }
                        if (re.DJZL == 24)
                        {
                            lastdyzxcounts5 += 1;
                        }
                    }
                }
                if (lastresult6.Count > 0)
                {
                    foreach (var re in lastresult6)
                    {
                        if (re.DJZL == 23)
                        {
                            lastdycounts6 += 1;
                        }
                        if (re.DJZL == 22)
                        {
                            lastzydycounts6 += 1;
                        }
                        if (re.DJZL == 24)
                        {
                            lastdyzxcounts6 += 1;
                        }
                    }
                }
                if (lastyearresult.Count > 0)
                {
                    foreach (var re in lastyearresult)
                    {
                        if (re.DJZL == 23)
                        {
                            lastyeardycounts += 1;
                        }
                        if (re.DJZL == 22)
                        {
                            lastyearzydycounts += 1;
                        }
                        if (re.DJZL == 24)
                        {
                            lastyeardyzxcounts += 1;
                        }
                    }
                }
                ring.columns = new List<string>();

                ring.columns.Add("dept");
                ring.columns.Add("count");
            var thisdycounts = 0;
            var thiszydycounts = 0;
            var thisdyzxcounts = 0;
            foreach (var re in thisyearresult)
            {
                if (re.DJZL == 23)
                {
                    thisdycounts += 1;
                }
                if (re.DJZL == 21)
                {
                    thiszydycounts += 1;
                }
                if (re.DJZL == 24)
                {
                    thisdyzxcounts += 1;
                }
            }
            var lastdycounts1 = 0;
            var lastzydycounts1 = 0;
            var lastdyzxcounts1 = 0;
            foreach (var re in LASTyearresult)
            {
                if (re.DJZL == 23)
                {
                    lastdycounts1 += 1;
                }
                if (re.DJZL == 21)
                {
                    lastzydycounts1 += 1;
                }
                if (re.DJZL == 24)
                {
                    lastdyzxcounts1 += 1;
                }
            }
            var lastdtwoycounts1 = 0;
            var lastztwozydycounts1 = 0;
            var lasttwodyzxcounts1 = 0;
            foreach (var re in lasttwoyearresult)
            {
                if (re.DJZL == 23)
                {
                    lastdtwoycounts1 += 1;
                }
                if (re.DJZL == 21)
                {
                    lastztwozydycounts1 += 1;
                }
                if (re.DJZL == 24)
                {
                    lasttwodyzxcounts1 += 1;
                }
            }
            pre.COLUMNS = new List<string>();
                pre.COLUMNS.Add("日期");
                pre.COLUMNS.Add("抵押");
                pre.COLUMNS.Add("转移及抵押");
                pre.COLUMNS.Add("注销");



            pre.ROWS.Add(new ROWS_pre()
            {
                date = $"{lasttwoyear.Date.Year}年",
                dy = lastdtwoycounts1,
                zx = lasttwodyzxcounts1,
                zydy = lastztwozydycounts1,
            });
            pre.ROWS.Add(new ROWS_pre()
            {
                date = $"{lastyear.Date.Year}年",
                dy = lastdycounts1,
                zx = lastdyzxcounts1,
                zydy = lastzydycounts1,
            });
            pre.ROWS.Add(new ROWS_pre()
            {
                date = $"{nowdate.Date.Year}年",
                dy = thisdycounts,
                zx = thisdyzxcounts,
                zydy = thiszydycounts,
            });
            line.COLUMNS = new List<string>();
                line.COLUMNS.Add("日期");
                line.COLUMNS.Add("抵押");
                line.COLUMNS.Add("转移及抵押");
                line.COLUMNS.Add("注销");
                line.ROWS.Add(new ROWS_line()
                {
                    date = $"{lasttime5.Date.Year}年{lasttime5.AddMonths(1).Date.Month}月份",
                    dy = lastdycounts6,
                    zx = lastzydycounts6,
                    zydy = lastdyzxcounts6,
                });
                line.ROWS.Add(new ROWS_line()
                {
                    date = $"{lasttime4.Date.Year}年{lasttime4.AddMonths(1).Date.Month}月份",
                    dy = lastdycounts5,
                    zx = lastzydycounts5,
                    zydy = lastdyzxcounts5,
                });
                line.ROWS.Add(new ROWS_line()
                {
                    date = $"{lasttime3.Date.Year}年{lasttime3.AddMonths(1).Date.Month}月份",
                    dy = lastdycounts4,
                    zx = lastzydycounts4,
                    zydy = lastdyzxcounts4,
                });
                line.ROWS.Add(new ROWS_line()
                {
                    date = $"{lasttime2.Date.Year}年{lasttime2.AddMonths(1).Date.Month}月份",
                    dy = lastdycounts3,
                    zx = lastzydycounts3,
                    zydy = lastdyzxcounts3,
                });
                line.ROWS.Add(new ROWS_line()
                {
                    date = $"{lasttime.Date.Year}年{lasttime.AddMonths(1).Date.Month}月份",
                    dy = lastdycounts2,
                    zx = lastzydycounts2,
                    zydy = lastdyzxcounts2,
                });
/*                line.ROWS.Add(new ROWS_line()
                {
                    日期 = $"{nowdate.Date.Year}年{nowdate.Date.Month}月份",
                    抵押 = lastdycounts,
                    注销 = lastzydycounts,
                    转移及抵押 = lastdyzxcounts,
                });*/
                line.ROWS.Add(new ROWS_line()
                {
                    date = $"{nowdate.Date.Year}年{nowdate.AddMonths(1).Date.Month}月份",
                    dy = dycounts,
                    zx = dyzxcounts,
                    zydy = zydycounts,
                });
                ChartVModel model = new ChartVModel()
                {
                    ChartData_ring = new ChartData_ring()
                    {
                        columns = ring.columns,
                        rows = ring.rows
                    },
                    ChartData_line = new ChartData_line()
                    {
                        COLUMNS = line.COLUMNS,
                        ROWS = line.ROWS,
                    },
                    ChartData_pre = new ChartData_pre()
                    {
                        COLUMNS = pre.COLUMNS,
                        ROWS = pre.ROWS,
                    },
                    TableData = data


                };
                return model;


        }

    }
}
