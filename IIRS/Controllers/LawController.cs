using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.LAW;
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
    /// 在建工程抵押土地相关信息
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    public class LawController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        private readonly ILogger<LawController> _logger;

        private readonly ICourtBdcServices _courtBdcServices;

        public LawController(IDBTransManagement dbTransManagement, ILogger<LawController> logger, ICourtBdcServices courtBdcServices)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _courtBdcServices = courtBdcServices;
            _logger = logger;
        }

        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="qlrmc"></param>
        /// <param name="zjhm"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<string>> GetLawList(string qlrmc, string zjhm)
        {            
            try
            {
                var data = await _courtBdcServices.GetLawList(qlrmc, zjhm);
                string strXml = IIRS.Utilities.UtilConvert.ClassToXML(data);
                strXml = strXml.Replace("!@#$$$^YUNSUN*", "");
                if (strXml != "")
                {
                    return new MessageModel<string>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = strXml
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "未获取到数据",
                        success = false,
                        response = ""
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<string>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }







    }
}
