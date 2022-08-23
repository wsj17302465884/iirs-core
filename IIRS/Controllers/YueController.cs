using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IRepository.LYSXK209;
using IIRS.IServices;
using IIRS.IServices.WQ;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.EntityModel.LYSXK209;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Models.ViewModel.WQ;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RT.Comb;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;

namespace IIRS.Controllers
{
    /// <summary>
    /// 排队预约放号
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    public class YueController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        private readonly ILogger<YueController> _logger;

        private readonly IYUE_DATESCHEDULE_NEWRepository _yue_date;
        private readonly IYUEServices _iYUEServices;
        /// <summary>
        /// 预约类型资料库
        /// </summary>
        private readonly IYue_TypeRepository _yue_Type;

        private readonly IYue_PeriodRepository _yue_Period;
        /// <summary>
        /// 日期计划表资料库
        /// </summary>
        private readonly IYue_DateScheduleRepository _yue_DateSchedule;

        /// <summary>
        /// 时间段类型计划表资料库
        /// </summary>
        private readonly IYue_PTAmountRepository _yue_PTAmount;

        public YueController(IDBTransManagement dbTransManagement, ILogger<YueController> logger, IYUE_DATESCHEDULE_NEWRepository yue_date, 
            IYUEServices yUEServices,
             IYue_TypeRepository yue_Type,
                IYue_DateScheduleRepository yue_DateSchedule,
            IYue_PTAmountRepository yue_PTAmount,
             IYue_PeriodRepository yue_Period)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _yue_date = yue_date;
            _iYUEServices = yUEServices;
            _logger = logger;
            _yue_Type = yue_Type;
            _yue_DateSchedule = yue_DateSchedule;
            _yue_PTAmount = yue_PTAmount;
            _yue_Period = yue_Period;
        }

        /// <summary>
        /// 保存预约放号日期数据
        /// </summary>
        /// <param name="dataVModel">信息日期列表</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<int>> PostDateList([FromForm]string dataVModel)
        {
            try
            {
                int count = 0;
                DateTime saveTime = DateTime.Now;
                string saveDataJson = HttpUtility.UrlDecode(dataVModel);
                List<DataVModelR> model = null;
                try
                {
                    model = JsonConvert.DeserializeObject<List<DataVModelR>>(saveDataJson);
                }
                catch (Exception ex)
                {
                    return new MessageModel<int>()
                    {
                        msg = "数据保存格式错误，请与管理员联系",
                        success = false
                    };
                }
                var dateScheduleList = new List<Yue_DateSchedule>();//添加
                var dateScheduleList2 = new List<Yue_DateSchedule>();//更新
                foreach (var item in model)
                {
                    var data = _yue_DateSchedule.Query(A => A.SCHEDULEDATE == item.scheduledate).Result;
                    if (data.Count >1)//视为更新
                    {
                        foreach( var yue in data)
                        {
                            var amountList = await _yue_PTAmount.Query(a => a.ID == yue.AMOUNT_ID);
                            foreach(var amount in amountList)
                            {
                                var typeList = await _yue_Type.Query(a => a.ID == amount.TYPE_ID);
                                if (typeList.Count > 0)
                                {
                                    if (typeList[0].NAME == "单独交税")
                                    {
                                        yue.SCHEDULEAMOUNT = item.taxPayCount;
                                    }
                                    else if (typeList[0].NAME == "公积金抵押一体化")
                                    {
                                        yue.SCHEDULEAMOUNT = item.pubFundsCount;
                                    }
                                    else if (typeList[0].NAME == "抵押房产登记")
                                    {
                                        yue.SCHEDULEAMOUNT = item.mortCount;
                                    }
                                    else if (typeList[0].NAME == "交易、产权、税收一体化登记")
                                    {
                                        yue.SCHEDULEAMOUNT = item.appointmentCount;
                                    }
                                }
                               
                            }
                            dateScheduleList2.Add(yue);
                        }
                        //data[0].week = item.week;
                        //data[0].isrest = item.isRest;
                        //data[0].mortstate = item.mortState;
                        //data[0].dealstate = item.dealState;
                        //data[0].pubfundsstate = item.pubFundsState;
                        //data[0].appointmentcount = item.appointmentCount;
                        //data[0].mortcount = item.mortCount;
                        //data[0].pubfundscount = item.pubFundsCount;
                        //data[0].taxpaycount = item.taxPayCount;
                    }
                    else //视为添加
                    {
                       
                        var amountList = await _yue_PTAmount.Query(a => a.ISDELETED == false);
                        var typeList = await _yue_Type.Query(a => a.ISDELETED == false);
                        foreach (var amount in amountList)
                        {
                            foreach(var type in typeList)
                            {
                                var dateSchedule = new Yue_DateSchedule()
                                {
                                    ID = Provider.Sql.Create(),
                                    SCHEDULEDATE = item.scheduledate,
                                    AMOUNT_ID = amount.ID,
                                    SCHEDULEAMOUNT = 0,
                                    USEDAMOUNT = 0
                                };
                                if (type.ID == amount.TYPE_ID)
                                {
                                   
                                    if (type.NAME== "单独交税")
                                    {
                                        dateSchedule.SCHEDULEAMOUNT = item.taxPayCount;
                                    }
                                    else if(type.NAME== "公积金抵押一体化")
                                    {
                                        dateSchedule.SCHEDULEAMOUNT = item.pubFundsCount;
                                    }
                                    else if(type.NAME == "抵押房产登记")
                                    {
                                        dateSchedule.SCHEDULEAMOUNT = item.mortCount;
                                    }
                                    else if(type.NAME == "交易、产权、税收一体化登记")
                                    {
                                        dateSchedule.SCHEDULEAMOUNT = item.appointmentCount;
                                    }
                                    dateScheduleList.Add(dateSchedule);
                                }
                            }
                         
                          
                        }
                       
                       
                    }
                }

                count += await _yue_DateSchedule.Add(dateScheduleList);
                if (dateScheduleList2.Count > 0)
                {
                    await _yue_DateSchedule.Update(dateScheduleList2);
                }
               
                //return new MessageModel<int>()
                //{
                //    Success = true,
                //    Msg = "",
                //    Response = await _yue_DateSchedule.Add(dateScheduleList)
                //};
                return   new MessageModel<int>()
                {
                    msg = "保存成功",
                    success = true,
                    response = count
                };


                //   model = await _printPdfServices.GetPdfInfo(slbh);


            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<int>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                };
            }
        }

        /// <summary>
        /// 获取放号日期数据
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<DataVModelR>>> GetDateList()
        {
            try
            {
                int count = 0;
                var saveTime = DateTime.Now.ToString("yyyy-MM-dd");
                //var saveTime = "2021-01-01";
                //var data = _yue_date.Query(A => Convert.ToDateTime(A.cdate) >= saveTime).Result;
                var data =await _iYUEServices.GetDateListAsync(saveTime);
                // string saveDataJson = HttpUtility.UrlDecode(dataVModel);
                return new MessageModel<List<DataVModelR>>()
                {
                    msg = "获取成功",
                    success = true,
                    response = data
                };
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<DataVModelR>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                };
            }
        }

        /// <summary>
        /// 获取预约记录
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<RecordVModel>>> GetRecordList(string starttime , string endtime)
        {
            try
            {
                int count = 0;
                //var starttime = DateTime.Now.ToString(endtime, "yyyy-MM-dd");
                //var endtime = DateTime.Now.ToString("yyyy-MM-dd");
                //var saveTime = "2021-01-01";
                //var data = _yue_date.Query(A => Convert.ToDateTime(A.cdate) >= saveTime).Result;
                var data = await _iYUEServices.GetRecordListAsync(starttime,endtime);
                // string saveDataJson = HttpUtility.UrlDecode(dataVModel);
                return new MessageModel<List<RecordVModel>>()
                {
                    msg = "获取成功",
                    success = true,
                    response = data
                };
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<RecordVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                };
            }
        }
        /// <summary>
        /// 获取时间段下放号量数据
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<TimeVModel>>> GetTimeList()
        {
            try
            {
                int count = 0;
                var saveTime = DateTime.Now.ToString("yyyy-MM-dd");
              
                var data = await _iYUEServices.GetTimeListAsync(saveTime);
                // string saveDataJson = HttpUtility.UrlDecode(dataVModel);
                return new MessageModel<List<TimeVModel>>()
                {
                    msg = "获取成功",
                    success = true,
                    response = data
                };
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<TimeVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                };
            }
        }
        /// <summary>
        /// 修改时间段下放号量数据
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpPost]
        public MessageModel<string> ChangeTimeList([FromForm] string dataVModel)
        {
            try
            {
               
                string saveDataJson = HttpUtility.UrlDecode(dataVModel);
                List<TimeVModel> model = null;
                try
                {
                    model = JsonConvert.DeserializeObject<List<TimeVModel>>(saveDataJson);
                }
                catch (Exception ex)
                {
                    return new MessageModel<string>()
                    {
                        msg = "数据保存格式错误，请与管理员联系",
                        success = false
                    };
                }
                var data = _iYUEServices.ChangeTimeListAsync(model);
                return new MessageModel<string>()
                {
                    msg = "程序执行成功 获取结果:" + data,
                    success = true,
                };
                // string saveDataJson = HttpUtility.UrlDecode(dataVModel);

            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<string>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                };
            }
        }
        /// <summary>
        /// 修改时间段类型
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpPost]
        public MessageModel<string> ChangeTimeValue([FromForm] string dataVModel)
        {
            try
            {

                string saveDataJson = HttpUtility.UrlDecode(dataVModel);
                List<PeriodVModel> model = null;
                try
                {
                    model = JsonConvert.DeserializeObject<List<PeriodVModel>>(saveDataJson);
                }
                catch (Exception ex)
                {
                    return new MessageModel<string>()
                    {
                        msg = "数据保存格式错误，请与管理员联系",
                        success = false
                    };
                }
                var data = _iYUEServices.ChangeTimeValue(model);
                return new MessageModel<string>()
                {
                    msg = "程序执行成功 获取结果:" + data,
                    success = true,
                };
                // string saveDataJson = HttpUtility.UrlDecode(dataVModel);

            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<string>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                };
            }
        }
        /// <summary>
        /// 获取时间段类型
        /// </summary>
        /// 
        /// <returns></returns>
        [HttpGet]
        public MessageModel<List<YUE_PERIOD>> GetTimeValue()
        {
            try
            {
                var data = _yue_Period.Query().Result;

                return new MessageModel<List<YUE_PERIOD>>()
                {
                    msg = "获取成功",
                    success = true,
                    response = data
                };
                // string saveDataJson = HttpUtility.UrlDecode(dataVModel);

            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<YUE_PERIOD>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                };
            }
        }
        /// <summary>
        /// 获取拿号记录信息
        /// </summary>
        /// <returns></returns>
        //[HttpGet]
        //public async Task<MessageModel<List<OrderVModel>>> GetTimeList()
        //{

        //}


    }
}
