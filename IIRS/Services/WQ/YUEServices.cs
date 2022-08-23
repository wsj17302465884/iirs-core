using IIRS.IRepository.Base;
using IIRS.IRepository.LYSXK209;
using IIRS.IServices.WQ;
using IIRS.Models.EntityModel.LYSXK209;
using IIRS.Models.ViewModel.WQ;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Gov.Services.Law
{
    public class YUEServices : BaseServices, IYUEServices
    {
        private readonly ILogger<YUEServices> _logger;
        private readonly IDBTransManagement _dbTransManagement;
        private readonly IWQ_LOGRepository _iWQ_LOGRepository;
        private readonly IYUE_DATESCHEDULE_NEWRepository _yue_date;
        private readonly IYue_DateScheduleRepository _yue_dateSchedule;

        private readonly IYue_PeriodRepository _yue_Period;
        public YUEServices(IDBTransManagement dbTransManagement, ILogger<YUEServices> logger, IWQ_LOGRepository wQ_LOGRepository, IYUE_DATESCHEDULE_NEWRepository yue_date, IYue_DateScheduleRepository yue_DateScheduleRepository, IYue_PeriodRepository yue_Period) : base(dbTransManagement)
        {
            _logger = logger;
            _dbTransManagement = dbTransManagement;
            _iWQ_LOGRepository = wQ_LOGRepository;
            _yue_date = yue_date;
            _yue_dateSchedule = yue_DateScheduleRepository;
            _yue_Period = yue_Period;
        }

        public async Task<bool> ChangeTimeListAsync(List<TimeVModel> data)
        {
            base.ChangeDB(SysConst.DB_CON_LYSXK209);
            bool count;
            List<Yue_DateSchedule> yUE_DATESCHEDULE = new List<Yue_DateSchedule>();
            foreach (var item in data)
            {
                var model =await base.Db.Queryable<Yue_DateSchedule, Yue_PTAmount, Yue_Type,YUE_PERIOD>((A, B, C,D) => A.AMOUNT_ID == B.ID && B.TYPE_ID == C.ID && A.SCHEDULEDATE == item.cdate&&D.ID==B.PERIOD_ID&&D.STARTTIME==item.starttime).Select((A, B, C,D) => new typeVModel
                {
                   TYPENAME= C.NAME,
                   COUNT = A.SCHEDULEAMOUNT,
                   STARTTIME = D.STARTTIME,
                   ID = A.ID,
                   AMOUNTID = A.AMOUNT_ID,
                    USERDAMOUNT = A.USEDAMOUNT,
                }).ToListAsync();
                foreach(var index in model)
                {

                    Yue_DateSchedule ydata = new Yue_DateSchedule();
                    ydata.ID = index.ID;
                    ydata.AMOUNT_ID = index.AMOUNTID;
                    ydata.SCHEDULEDATE = item.cdate;
                    
                    if(index.TYPENAME== "单独交税")
                    {
                        if (item.taxPayCount > index.COUNT||item.taxPayCount>index.USERDAMOUNT)
                        {
                            ydata.SCHEDULEAMOUNT = item.taxPayCount;
                        }
                        else
                        {
                            return false;
                        }
                       
                    }
                    else if(index.TYPENAME== "公积金抵押一体化")
                    {
                        if (item.pubFundsCount > index.COUNT || item.pubFundsCount > index.USERDAMOUNT)
                        {
                            ydata.SCHEDULEAMOUNT = item.pubFundsCount;
                        }
                        else
                        {
                            return false;
                        }
                  
                    }
                    else if (index.TYPENAME == "交易、产权、税收一体化登记")
                    {
                        if (item.appointmentCount > index.COUNT || item.appointmentCount > index.USERDAMOUNT)
                        {
                            ydata.SCHEDULEAMOUNT = item.appointmentCount;
                        }
                        else
                        {
                            return false;
                        }
                    
                    }
                    else if (index.TYPENAME == "抵押房产登记")
                    {
                        if (item.mortCount > index.COUNT || item.mortCount > index.USERDAMOUNT)
                        {
                            ydata.SCHEDULEAMOUNT = item.mortCount;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    yUE_DATESCHEDULE.Add(ydata);
                }
             
               
               
            }
             count= _yue_dateSchedule.Update(yUE_DATESCHEDULE).Result;
            return count;
        }

        public async Task<bool> ChangeTimeValue(List<PeriodVModel> data)
        {
            base.ChangeDB(SysConst.DB_CON_LYSXK209);
            bool count;
            List<YUE_PERIOD> yUE_PERIODs = new List<YUE_PERIOD>();
            foreach (var item in data) {

                var yue_period = _yue_Period.QueryById(item.ID).Result;
                yue_period.STARTTIME = item.STARTTIME;
                yue_period.ISAM = item.ISAM;
                yUE_PERIODs.Add(yue_period);
            }
             return  await _yue_Period.Update(yUE_PERIODs);
            
            throw new NotImplementedException();
        }

        public async Task<List<DataVModelR>> GetDateListAsync(string date)
        {
            List<DataVModelR> model = new List<DataVModelR>();
            base.ChangeDB(SysConst.DB_CON_LYSXK209);
            List<SugarParameter> whereParams = new List<SugarParameter>();
            whereParams.Add(new SugarParameter(":todaydate", date));
            string SQL = string.Format(@"select t.scheduledate,t.scheduleamount,a.name from YUE_DATESCHEDULE t,yue_type a,yue_periodtypeamount b where t.scheduledate>=:todaydate
and t.amount_id = b.id and b.type_id = a.id group by a.name,t.scheduleamount,t.scheduledate order by t.scheduledate asc");
            var data = await base.Db.Ado.SqlQueryAsync<scheduleVModel>(SQL, whereParams);

            string SQL2 = string.Format(@"select t.scheduledate from YUE_DATESCHEDULE t where t.scheduledate>=:todaydate group by t.scheduledate");
            var data2 = await base.Db.Ado.SqlQueryAsync<DataVModelR>(SQL2, whereParams);
            foreach(var index in data2)
            {
              
                foreach (var item in data)
                {

                    if(index.scheduledate == item.scheduledate)
                    {
                        if (item.name == "单独交税")
                        {
                            index.taxPayCount = item.scheduleamount;
                        }else
                        if (item.name == "公积金抵押一体化")
                        {
                            index.pubFundsCount = item.scheduleamount;
                        }
                        else
                        if (item.name== "抵押房产登记")
                        {
                            index.mortCount = item.scheduleamount;
                        }
                        else
                        if (item.name== "交易、产权、税收一体化登记")
                        {
                            index.appointmentCount = item.scheduleamount;
                        }
                    }
                    if (item.pubFundsCount == 0 && item.taxPayCount == 0 && item.appointmentCount == 0 && item.mortCount == 0)
                    {
                        index.isRest = 1;
                    }
                    var day = Convert.ToDateTime(index.scheduledate+" 00:00:00");
                    index.week = GetWeekName(day.DayOfWeek);
                }
            }
            return data2;
           
        }

        public async Task<List<RecordVModel>> GetRecordListAsync(string startdate,string enddate)
        {
            base.ChangeDB(SysConst.DB_CON_LYSXK209);
            List<RecordVModel> records = new List<RecordVModel>();
            List<SugarParameter> whereParams = new List<SugarParameter>();
            whereParams.Add(new SugarParameter(":startdate", startdate));
            whereParams.Add(new SugarParameter(":enddate", enddate));
            string sql = string.Format(@"SELECT
	c.id as idcard,
	c.name,
	b.scheduledate,
	f.name as type,
	d.starttime,
	a.ordercode,
	a.committime as djsj,
	a.state,
	G.PHONENUMBER as phone,
	G.NICKNAME 
FROM
	YUE_RECORD a,
	yue_dateschedule b,
	wx_idcard c,
	yue_period d,
	yue_periodtypeamount e,
	yue_type f,
	WX_USERINFO G 
WHERE
    b.scheduledate>=:startdate and b.scheduledate<=:enddate and
	a.datescheduleid = b.id 
	AND a.openid = c.OpenId 
	AND b.amount_id = e.id 
	AND e.period_id = d.id 
	AND e.type_id = f.id 
	AND G.OPENID = A.OPENID");
            var data = await base.Db.Ado.SqlQueryAsync<RecordVModel>(sql, whereParams);
            return data;
            throw new NotImplementedException();
        }

        public async Task<List<TimeVModel>> GetTimeListAsync(string date)
        {
            base.ChangeDB(SysConst.DB_CON_LYSXK209);
            List<SugarParameter> whereParams = new List<SugarParameter>();
            
            whereParams.Add(new SugarParameter(":todaydate", date));
            string SQL = string.Format(@"select  a.scheduledate as cdate,b.starttime,
sum(decode(d.name,'抵押房产登记',a.scheduleamount)) as mortcount,
sum(decode(d.name, '公积金抵押一体化', a.scheduleamount)) as pubfundscount,
sum(decode(d.name, '单独交税', a.scheduleamount)) as taxpaycount,
sum(decode(d.name, '交易、产权、税收一体化登记', a.scheduleamount)) as appointmentcount
from yue_dateschedule a , yue_period b, yue_periodtypeamount c, yue_type d where a.amount_id = c.id and c.period_id = b.id and c.type_id = d.id
and a.scheduledate = :todaydate
group by b.starttime, a.scheduledate order by b.starttime asc");

            var data = await base.Db.Ado.SqlQueryAsync<TimeVModel>(SQL, whereParams);
            //for (int i = 0; i < data.Count; i++)
            //{
            //    if (data[i].dealState == 1)
            //    {
            //        data[i].appointmentCount = 0;
            //    }
            //    if (data[i].mortState == 1)
            //    {
            //        data[i].mortCount = 0;
            //    }
            //    if (data[i].pubFundsState == 1)
            //    {
            //        data[i].pubFundsCount = 0;
            //    }
            //}
            return data;
        }
        private string GetWeekName(DayOfWeek week)
        {
            string weekName = "";
            switch (week)
            {
                case DayOfWeek.Friday:
                    weekName = "星期五";
                    break;
                case DayOfWeek.Monday:
                    weekName = "星期一";
                    break;
                case DayOfWeek.Saturday:
                    weekName = "星期六";
                    break;
                case DayOfWeek.Sunday:
                    weekName = "星期日";
                    break;
                case DayOfWeek.Thursday:
                    weekName = "星期四";
                    break;
                case DayOfWeek.Tuesday:
                    weekName = "星期二";
                    break;
                case DayOfWeek.Wednesday:
                    weekName = "星期三";
                    break;
                default:
                    break;
            }
            return weekName;
        }
    }
}
