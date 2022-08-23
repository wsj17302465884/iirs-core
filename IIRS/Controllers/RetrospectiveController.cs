using IIRS.IRepository.Base;
using IIRS.IRepository.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.ViewModel;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    public class RetrospectiveController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        private readonly ILogger<RetrospectiveController> _logger;
        private readonly IRetrospectiveRepository _retrospectiveRepository;

        public RetrospectiveController(IDBTransManagement dbTransManagement, ILogger<RetrospectiveController> logger, IRetrospectiveRepository retrospectiveRepository)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _retrospectiveRepository = retrospectiveRepository;
            _logger = logger;
        }
        [HttpGet]
        public async Task<MessageModel<List<RetrospectiveModel>>> GetRetrospectiveList(string qlrmc,string zjhm)
        {
            string first;
            string second;
            string fifteenZjhm = "";
            if(zjhm.Length == 18)
            {
                first = zjhm.Substring(0, 5);
                second = zjhm.Substring(10, 8);
                fifteenZjhm = first + second;
            }
            
            var data = await _retrospectiveRepository.Query(i => i.qlrmc == qlrmc && (i.zjhm == fifteenZjhm || i.zjhm == zjhm));
            if (data.Count > 0)
            {
                return new MessageModel<List<RetrospectiveModel>>()
                {
                    msg = "获取成功",
                    success = true,
                    response = data
                };
            }
            else
            {
                return new MessageModel<List<RetrospectiveModel>>()
                {
                    msg = "未获取到数据",
                    success = false,
                    response = null
                };
            }
        }
    }
}
