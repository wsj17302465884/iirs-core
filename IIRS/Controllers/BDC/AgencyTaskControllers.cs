using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Controllers.BDC
{
    /// <summary>
    /// 任务列表
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    public class AgencyTaskControllers : ControllerBase
    {
        readonly IQueryServices _queryServices;
        private readonly ILogger<AgencyTaskControllers> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AgencyTaskControllers(IQueryServices queryServices, ILogger<AgencyTaskControllers> logger, IWebHostEnvironment webHostEnvironment)
        {            
            _logger = logger;
            _queryServices = queryServices;
            _webHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <param name="jbr">经办人</param>
        /// <param name="lczl">流程种类</param>
        /// <param name="IsAction">流程种类</param>
        /// <param name="intPageIndex">当前页标</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<AgencyTaskVModel>>> GetAgencyTaskList(string slbh, string jbr, string lczl, int IsAction,  int intPageIndex, int PageSize)
        {
            try
            {
                var data = await _queryServices.GetAgencyTaskList(slbh, jbr, lczl, IsAction, intPageIndex, PageSize);
                if(data.data.Count > 0)
                {
                    return new MessageModel<PageModel<AgencyTaskVModel>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<PageModel<AgencyTaskVModel>>()
                    {
                        msg = "未获取到数据。",
                        success = false,
                        response = null
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"AgencyTaskControllers.GetAgencyTaskList错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PageModel<AgencyTaskVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 获取流程名称
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<IFLOW_ACTION_GROUP>>> GetActionGroupList()
        {
            try
            {
                var data = await _queryServices.GetActionGroupList();
                if(data.Count > 0)
                {
                    return new MessageModel<List<IFLOW_ACTION_GROUP>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<IFLOW_ACTION_GROUP>>()
                    {
                        msg = "未获取到数据。",
                        success = false,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"AgencyTaskControllers.GetActionGroupList错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<IFLOW_ACTION_GROUP>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
    }
}
