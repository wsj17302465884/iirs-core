using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IIRS.IRepository;
using IIRS.IServices;
using IIRS.Models.ViewModel;
using IIRS.Utilities;
using IIRS.Utilities.AuthHelper;
using IIRS.Utilities.Common;
using IIRS.Utilities.Filter;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIRS.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    public class ImmovablesRelationQueryController : ControllerBase
    {
        private readonly ILogger<ImmovablesRelationQueryController> _logger;
        readonly IImmovablesRelationQueryServices _ImmovablesRelationQueryServices;
        public ImmovablesRelationQueryController(IImmovablesRelationQueryServices immovablesRelationQueryServices, ILogger<ImmovablesRelationQueryController> logger)
        {
            _ImmovablesRelationQueryServices = immovablesRelationQueryServices;
            this._logger = logger;
        }

        /// <summary>
        /// 不动产相关信息查询
        /// </summary>
        /// <param name="BDCDYH">不动产单元号</param>
        /// <param name="ZSLX">证书类型</param>
        /// <param name="BDCZH">不动产权证书号</param>
        /// <param name="SLBH">登记受理编号</param>
        /// <param name="DY_QLR">权利人</param>
        /// <param name="ZL">坐落</param>
        /// <param name="PageIndex">分页页面</param>
        /// <param name="isComputeCount">分页页面</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<ImmovablesRelationVModel>>> Get(string BDCDYH, int ZSLX, string BDCZH, string SLBH, string DY_QLR, string ZL, int PageIndex,bool isComputeCount)
        {
            try
            {
                var data = await _ImmovablesRelationQueryServices.ImmovablesRelationQuery(BDCDYH, ZSLX, BDCZH, SLBH, DY_QLR, ZL, PageIndex, SysConst.SYS_DEFAULT_PAGE_SIZE, isComputeCount);
                return new MessageModel<PageModel<ImmovablesRelationVModel>>()
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
                return new MessageModel<PageModel<ImmovablesRelationVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
            
        }
    }
}
