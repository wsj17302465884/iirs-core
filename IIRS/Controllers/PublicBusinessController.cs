using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
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
    /// 在建工程抵押土地相关信息
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    public class PublicBusinessController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        private readonly ILogger<PublicBusinessController> _logger;

        private readonly IPublicBusinessServices _publicBusinessServices;

        private readonly IHouseStatusRepository _houseStatusRepository;

        public PublicBusinessController(IDBTransManagement dbTransManagement, ILogger<PublicBusinessController> logger, IPublicBusinessServices publicBusinessServices,IHouseStatusRepository houseStatusRepository)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _publicBusinessServices = publicBusinessServices;
            _houseStatusRepository = houseStatusRepository;
            _logger = logger;
        }
        /// <summary>
        /// 获取流程节点名称
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<IFLOW_ACTION>>> GetFlowActionModels(int groupId)
        {
            try
            {
                var data = await _publicBusinessServices.GetFlowActionModels(groupId);
                if(data.Count>0)
                {
                    return new MessageModel<List<IFLOW_ACTION>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<IFLOW_ACTION>>()
                    {
                        msg = "未获取到数据",
                        success = false,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<IFLOW_ACTION>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 获取任务列表
        /// </summary>
        /// <param name="intPageIndex"></param>
        /// <param name="zjhm"></param>
        /// <param name="jbr"></param>
        /// <param name="flowId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<BankAuthorize>>> GetUpcomingTasksListToPage(int intPageIndex, string zjhm, string jbr, int flowId)
        {            
            try
            {
                var data = await _publicBusinessServices.GetUpcomingTasksListToPage(intPageIndex, zjhm, jbr, flowId);
                BankAuthorize model = new BankAuthorize();
                List<BankAuthorize> models = new List<BankAuthorize>();

                for (int i = 0; i < data.data.Count; i++)
                {
                    model = new BankAuthorize();
                    model.BID = data.data[i].BID;
                    model.DOCUMENTTYPE = data.data[i].DOCUMENTTYPE;
                    model.DOCUMENTNUMBER = data.data[i].DOCUMENTNUMBER;
                    model.AUTHORIZATIONDATE = data.data[i].AUTHORIZATIONDATE;
                    model.STATUS = data.data[i].STATUS;
                    model.rightname = data.data[i].rightname;
                    model.BankCode = data.data[i].BankCode;
                    model.BankName = data.data[i].BankName;
                    model.FlowName = data.data[i].FlowName;
                    models.Add(model);
                }
                data.data.Clear();
                foreach (var item in models)
                {
                    data.data.Add(item);
                }
                data.page = data.page;
                data.pageCount = data.pageCount;
                data.PageSize = data.PageSize;
                data.dataCount = data.dataCount;

                if (data.data.Count > 0)
                {
                    return new MessageModel<PageModel<BankAuthorize>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<PageModel<BankAuthorize>>()
                    {
                        msg = "未获取到数据",
                        success = false,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PageModel<BankAuthorize>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 获取房屋相关信息
        /// </summary>
        /// <param name="bdczh">不动产证号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<HouseStatusViewModel>>> GetHouseInfoByBdczh(string bdczh)
        {
            try
            {
                var data = await _publicBusinessServices.GetHouseInfoByBdczh(bdczh);
                if(data.Count>0)
                {
                    var houseData = await _houseStatusRepository.Query(i => i.Tstybm == data[0].tstybm);
                    if(houseData.Count > 0)
                    {
                        return new MessageModel<List<HouseStatusViewModel>>()
                        {
                            msg = "获取成功",
                            success = true,
                            response = houseData
                        };
                    }
                    else
                    {
                        return new MessageModel<List<HouseStatusViewModel>>()
                        {
                            msg = "未获取到数据",
                            success = false,
                            response = null
                        };
                    }
                    
                }
                else
                {
                    return new MessageModel<List<HouseStatusViewModel>>()
                    {
                        msg = "未获取到数据",
                        success = false,
                        response = null
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<HouseStatusViewModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        
    }
}
