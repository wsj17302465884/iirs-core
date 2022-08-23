using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
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
    public class PrintController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        private readonly ILogger<PrintController> _logger;

        private readonly IPrintPdfServices _printPdfServices;

        

        public PrintController(IDBTransManagement dbTransManagement, ILogger<PrintController> logger, IPrintPdfServices printPdfServices)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _printPdfServices = printPdfServices;
            
            _logger = logger;
        }

        /// <summary>
        /// 获取打印信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PdfVModel>> GetPrintInfo(string slbh)
        {
            try
            {
                var rtn = new MessageModel<PdfVModel>();
                PdfVModel model = new PdfVModel();
                model = await _printPdfServices.GetPdfInfo(slbh);

                if (model != null)
                {
                    rtn.msg = "获取成功";
                    rtn.success = true;
                    rtn.response = model;
                }
                else
                {
                    rtn.msg = "未获取到数据";
                    rtn.success = false;
                    rtn.response = null;
                }
                return rtn;
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PdfVModel>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 获取注销登记打印信息
        /// </summary>
        /// <param name="DySlbh"></param>
        /// <param name="NewSlbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PdfVModel>> GetMrgeReleasePdfInfo(string DySlbh, string NewSlbh)
        {
            try
            {
                var rtn = new MessageModel<PdfVModel>();
                PdfVModel model = new PdfVModel();
                model = await _printPdfServices.GetMrgeReleasePdfInfo(DySlbh,NewSlbh);

                if (model.houseList.Count > 0)
                {
                    rtn.msg = "获取成功";
                    rtn.success = true;
                    rtn.response = model;
                }
                else
                {
                    rtn.msg = "未获取到数据";
                    rtn.success = false;
                    rtn.response = null;
                }
                return rtn;
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PdfVModel>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 查询房屋是否可以打印
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<int>> GetPrintCheckListCount(string slbh)
        {
            try
            {
                var rtn = new MessageModel<int>();
                var ModelList = await _printPdfServices.GetPrintCheckListCount(slbh);

                if (ModelList.Count > 1)
                {
                    rtn.msg = "获取成功";
                    rtn.success = true;
                    rtn.response = ModelList.Count;
                }
                else if(ModelList.Count == 1 || ModelList.Count == 0)
                {
                    rtn.msg = "获取失败";
                    rtn.success = false;
                    rtn.response = ModelList.Count;
                }
                return rtn;
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<int>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = 400
                };
            }
        }
        /// <summary>
        /// 查询打印的房屋信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<OrderHouseAssociation>>> GetPrintCheckList(string slbh)
        {
            try
            {
                var rtn = new MessageModel<List<OrderHouseAssociation>>();
                var data = await _printPdfServices.GetPrintCheckList(slbh);

                if (data.Count > 0)
                {
                    rtn.msg = "获取成功";
                    rtn.success = true;
                    rtn.response = data;
                }
                else 
                {
                    rtn.msg = "获取失败";
                    rtn.success = false;
                    rtn.response = null;
                }
                return rtn;
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<OrderHouseAssociation>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
    }
}
