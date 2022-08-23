using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RT.Comb;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 在建工程抵押土地相关信息
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    public class ChartController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        private readonly ILogger<ChartController> _logger;

        private readonly IChartServices _chartServices;

        private readonly IBus_visit_logRepository _Ibus_visit_logRepository;

        public ChartController(IDBTransManagement dbTransManagement, ILogger<ChartController> logger, IChartServices chartServices, IBus_visit_logRepository Bus_visit_logRepository)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _chartServices = chartServices;
            _Ibus_visit_logRepository = Bus_visit_logRepository;
            _logger = logger;
        }

        /// <summary>
        /// 查询办件量
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="loginregid">登陆人组织机构代码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<ChartVModel>> GetChartInfo(string date,Guid loginregid)
        {
            try
            {
                var model = await _chartServices.GetDataCounts(date, loginregid);
                if (model != null)
                {
                    return new MessageModel<ChartVModel>()
                    {
                        msg = "成功",
                        success = true,
                        response = model,
                    };
                }
                else
                {
                    return new MessageModel<ChartVModel>()
                    {
                        msg = "失败",
                        success = false,
                        response = null,
                    };
                }

            }
            catch (ApplicationException ex)
            {
                return new MessageModel<ChartVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<ChartVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }

      
    }
}
