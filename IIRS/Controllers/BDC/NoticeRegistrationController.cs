using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using IIRS.Utilities.Common;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IIRS.Controllers.BDC
{
    /// <summary>
    /// 预告登记
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    public class NoticeRegistrationController : ControllerBase
    {
        private readonly ILogger<NoticeRegistrationController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly INoticeRegistrationServices _registrationServices;

        public NoticeRegistrationController(INoticeRegistrationServices registrationServices, ILogger<NoticeRegistrationController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _registrationServices = registrationServices;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// 获取房屋信息
        /// </summary>
        /// <param name="tstybm">图署统一编码</param>
        /// <param name="zl">坐落</param>
        /// <param name="intPageIndex">当前页标</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<FC_H_QSDC>>> NoticeSelectHouse(string tstybm, string zl, int intPageIndex, int PageSize)
        {
            try
            {
                var data = await _registrationServices.NoticeSelectHouse(tstybm, zl, intPageIndex, PageSize);
                if(data.data.Count > 0)
                {
                    return new MessageModel<PageModel<FC_H_QSDC>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<PageModel<FC_H_QSDC>>()
                    {
                        msg = "未查询到数据！！！",
                        success = false,
                        response = null
                    };
                }
                

            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PageModel<FC_H_QSDC>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode+ex.Message,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 预告登记数据提交
        /// </summary>
        /// <param name="StrInsertModel">不动产数据JSON</param>
        /// <param name="strFileTree">附件JSON</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> NoticePost([FromForm] string StrInsertModel, [FromForm] string strFileTree)
        {
            NoticeRegistrationVModel NoticeRegistrationModel = new NoticeRegistrationVModel();
            List<Base64FilesVModel> fileTree = null;
            List<PUB_ATT_FILE> attFileModel = null;
            if (!string.IsNullOrEmpty(StrInsertModel))
            {
                try
                {
                    string saveDataJson = HttpUtility.UrlDecode(StrInsertModel);
                    NoticeRegistrationModel = JsonConvert.DeserializeObject<NoticeRegistrationVModel>(saveDataJson);
                }
                catch (Exception ex)
                {
                    return new MessageModel<string>()
                    {
                        msg = "不动产数据保存格式错误，请与管理员联系。" + ex.ToString(),
                        success = false
                    };
                }
            }

            

            if (!string.IsNullOrEmpty(strFileTree))
            {
                try
                {
                    fileTree = JsonConvert.DeserializeObject<List<Base64FilesVModel>>(strFileTree);
                    string newSLBH = NoticeRegistrationModel.slbh;//服务器端获取受理编号
                    int imgCount = fileTree.Where(S => S.children.Count > 0).Count();
                    if (imgCount > 0)
                    {
                        attFileModel = SysUtility.UploadSysBase64File(this._webHostEnvironment.WebRootPath, newSLBH, fileTree);

                    }
                }
                catch (Exception ex)
                {
                    return new MessageModel<string>()
                    {
                        msg = "附件数据保存格式错误，请与管理员联系" + ex.ToString(),
                        success = false
                    };
                }
            }
            try
            {
                var data = await _registrationServices.InsertNoticePost(NoticeRegistrationModel, attFileModel);
                if (data != "")
                {
                    return new MessageModel<string>()
                    {
                        msg = "数据保存成功！",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "数据保存失败！",
                        success = false
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
                    success = false
                };
            }

        }

        /// <summary>
        /// 获取房产户权籍调查信息
        /// </summary>
        /// <param name="bdcdyh">当前业务不动产单元号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<fc_h_qsdcVmodel>>> GetAdvanceByHouseInfo(string bdcdyh)
        {
            //bdcdyh = "211011003201GB00177F99999999";
            try
            {
                var data = await _registrationServices.GetAdvanceByHouseInfo(bdcdyh);
                if (data.Count > 0)
                {
                    return new MessageModel<List<fc_h_qsdcVmodel>>()
                    {
                        msg = "调用接口成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<fc_h_qsdcVmodel>>()
                    {
                        msg = "调用接口成功",
                        success = true,
                        response = data
                    };
                }

            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<fc_h_qsdcVmodel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
    }
}
