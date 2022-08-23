using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
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
    /// 逻辑判断
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    public class BusinessJudgmentController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        private readonly ILogger<BusinessJudgmentController> _logger;

        private readonly IBusinessJudgmentServices _businessJudgmentServices;

        public BusinessJudgmentController(IDBTransManagement dbTransManagement, ILogger<BusinessJudgmentController> logger, IBusinessJudgmentServices businessJudgmentServices)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _businessJudgmentServices = businessJudgmentServices;
            _logger = logger;
        }
        
        /// <summary>
        /// 判断是否包含查封抵押异议
        /// </summary>
        /// <param name="yw_slbh">当前业务SLBH</param>
        /// <param name="qz_slbh">权证受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<JudgmentMortgage1>>> GetBusinessJudgment1(string yw_slbh, string qz_slbh)
        {
            try
            {
                var data = await _businessJudgmentServices.GetBusinessJudgment1(yw_slbh, qz_slbh);
                if(data.Count > 0)
                {
                    return new MessageModel<List<JudgmentMortgage1>>()
                    {
                        msg = "存在异常状态",
                        success = false,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<JudgmentMortgage1>>()
                    {
                        msg = "无异常状态",
                        success = true,
                        response = data
                    };
                }
                
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<JudgmentMortgage1>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        
        
        /// <summary>
        /// 判断是否有其他业务正在进行
        /// </summary>
        /// <param name="yw_slbh">当前业务受理编号</param>
        /// <param name="qz_slbh">权证受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<JudgmentMortgage1>>> GetBusinessJudgment2(string yw_slbh, string qz_slbh)
        {
            try
            {
                yw_slbh = yw_slbh + "%";
                var data = await _businessJudgmentServices.GetBusinessJudgment2(yw_slbh, qz_slbh);
                if(data.Count > 0)
                {
                    return new MessageModel<List<JudgmentMortgage1>>()
                    {
                        msg = "存在异常状态",
                        success = false,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<JudgmentMortgage1>>()
                    {
                        msg = "无异常状态",
                        success = true,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<JudgmentMortgage1>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 对应房地状态
        /// </summary>
        /// <param name="ywlx">业务类型：抵押或抵押变更</param>
        /// <param name="bdclx">不动产类型：房屋或者宗地</param>
        /// <param name="tstybm">图属统一编码</param>
        /// <param name="qz_slbh">权证受理编号</param>
        /// <param name="yw_slbh">当前办理业务受理编号</param>
        /// <param name="dy_slbh">当前抵押受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<string>> GetBusinessJudgment3(string ywlx, string bdclx, string tstybm, string qz_slbh, string yw_slbh,string dy_slbh)
        {
            try
            {                
                var data = await _businessJudgmentServices.GetBusinessJudgment3(ywlx, bdclx,tstybm, qz_slbh,yw_slbh,dy_slbh);
                if(data.Length > 0)
                {
                    if(!data.EndsWith(":"))
                    {
                        return new MessageModel<string>()
                        {
                            msg = "存在异常状态",
                            success = false,
                            response = data
                        };
                    }
                    else
                    {
                        return new MessageModel<string>()
                        {
                            msg = "无异常状态",
                            success = true,
                            response = null
                        };
                    }
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "无异常状态",
                        success = true,
                        response = null
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
        /// <summary>
        /// 选择物判断
        /// </summary>
        /// <param name="yw_slbh">当前业务受理编号</param>
        /// <param name="qz_slbh">权证受理编号</param>
        /// <param name="tstybm">权证受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<JudgmentMortgage1>>> GetBusinessJudgment4(string yw_slbh, string qz_slbh, string tstybm)
        {
            try
            {
                var data = await _businessJudgmentServices.GetBusinessJudgment4(yw_slbh, qz_slbh, tstybm);
                if (data.Count > 0)
                {
                    return new MessageModel<List<JudgmentMortgage1>>()
                    {
                        msg = "存在异常状态",
                        success = false,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<JudgmentMortgage1>>()
                    {
                        msg = "无异常状态",
                        success = true,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<JudgmentMortgage1>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
    }
}
