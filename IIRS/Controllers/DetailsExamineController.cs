using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel;
using IIRS.Models.ViewModel;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 详情查看
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    public class DetailsExamineController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        private readonly ILogger<DetailsExamineController> _logger;

        private readonly IDetailsExamineRepository _detailsExamineRepository;

        public DetailsExamineController(IDBTransManagement dbTransManagement, ILogger<DetailsExamineController> logger, IDetailsExamineRepository detailsExamineRepository)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _detailsExamineRepository = detailsExamineRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<MessageModel<List<DJZLViewTree>>> GetDetailsExamineTreeList(string tstybm)
        {
            try
            {
                var data = await _detailsExamineRepository.GetDetailsExamineTreeList(tstybm);
                return new MessageModel<List<DJZLViewTree>>()
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
                return new MessageModel<List<DJZLViewTree>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
    }
}
