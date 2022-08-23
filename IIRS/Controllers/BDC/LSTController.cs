using IIRS.IRepository.IIRS;
using IIRS.IServices;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Utilities.Common;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RT.Comb;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace IIRS.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    public class LSTController : ControllerBase
    {
        private readonly ILogger<LSTController> _logger;
        readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICommonServices iCommonServices;
        public LSTController(ILogger<LSTController> logger,ICommonServices commonServices, IWebHostEnvironment webHostEnvironment)
        {
            this._logger = logger;
            this.iCommonServices = commonServices;
            this._webHostEnvironment = webHostEnvironment;
        }
        /// <summary>
        /// 上传政策文件
        /// </summary>
        /// <param name="TITLE">标题</param>
        /// <param name="USERNAME">操作员姓名</param>
        /// <param name="FILEPATH">路径</param>
        /// <param name="SSLB">类别</param>
        /// <param name="base64">base64</param>
        /// <returns></returns>
        [HttpGet]
        public MessageModel<string> FileSubmit(string TITLE,string USERNAME,string FILEPATH,string SSLB,string base64)
        {
            try
            {
                string FID = Guid.NewGuid().ToString();


                LST_FILE FileInfo = new LST_FILE {
                    TITLE =TITLE,
                    USERNAME = USERNAME,
                    CDATE = DateTime.Now,
                    FILEPATH = FILEPATH,
                    FID = FID,
                    SSLB = SSLB,
                    base64=base64
                };
                
                try
                {
                    this.iCommonServices.SaveFile(FileInfo);
                    return new MessageModel<string>()
                    {
                        msg = "保存成功",
                        success = true,
                        response = @$"{{""FID"":""{FileInfo.FID}"",""PATH"":""{FileInfo.FILEPATH}""}}"
                    };
                }
                catch (Exception ex)
                {
                    return new MessageModel<string>()
                    {
                        msg = "数据保存格式错误，原因：" + ex.Message,
                        success = false
                    };
                }
            }catch(Exception ex)
            {
                return new MessageModel<string>()
                {
                    msg = "数据保存格式错误，原因：" + ex.Message,
                    success = false
                };
            }
           
        }

        /// <summary>
        /// 上传临时文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<MessageModel<string>> LSTUploadTempFilePost([FromForm] string strLSTInfo)
        {
            var files = Request.Form.Files;
            //string mediaType = string.Empty;
            try
            {
                //string反编译
                string saveDataJson = HttpUtility.UrlDecode(strLSTInfo);
                LST_FILE FacerecInfo = null;
                string FID = Guid.NewGuid().ToString();
                try
                {
                    FacerecInfo = JsonConvert.DeserializeObject<LST_FILE>(saveDataJson);
                  
                }
                catch (Exception ex)
                {
                    return new MessageModel<string>()
                    {
                        msg = "数据保存格式错误，请与管理员联系",
                        success = false
                    };
                }
                if (files.Count > 0)
                {
                    string path = string.Format(@"\TempUpload\{0:yyyyMM}\", DateTime.Now);
                    string absPath = this._webHostEnvironment.WebRootPath + path;//物理绝对路径
                    if (!Directory.Exists(absPath))
                    {
                        Directory.CreateDirectory(absPath);
                    }
                    //StreamReader sr = new StreamReader(absPath,encoding.Default);
                    //FileReader fr = new FileReader(file);  //字符输入流（file要求是绝对路径）
                    //BufferedReader br = new BufferedReader(fr);  //使文件可按行读取并具有缓冲功能

                    var file = files[0];
                    string Base64 = "";
                    string mediaType = Path.GetExtension(file.FileName).ToLower();
                    string fileName = file.FileName;
                    string fullName = Path.Combine(absPath, file.FileName);
                    if (!System.IO.File.Exists(fullName))
                    {
                        using (var fileStream = new FileStream(fullName, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        //mediaType = Path.GetExtension(fullName).ToLower();
                      
                      
                    }
                    try
                    {
                        Base64 = SysUtility.FileToBase64(fullName);
                        /*                            FileStream fs = new FileStream(fullName, FileMode.Open);
                                                    byte[] bt = new byte[fs.Length];
                                                    fs.Read(bt, 0, bt.Length);
                                                    strRet = Convert.ToBase64String(bt);
                                                    fs.Close();*/
                        LST_FILE FileInfo = new LST_FILE
                        {
                            TITLE = FacerecInfo.TITLE,
                            USERNAME = FacerecInfo.USERNAME,
                            CDATE = DateTime.Now,
                            FILEPATH = fullName,
                            FID = FID,
                            SSLB = FacerecInfo.SSLB,
                            base64 = Base64
                        };
                        this.iCommonServices.SaveFile(FileInfo);
                    }

                    catch (Exception ex)
                    {
                        Base64 = null;
                    }
                    return new MessageModel<string>()
                    {
                        msg = "数据查询成功",
                        success = true,
                        response = @$"{{""path"":""/TempUpload/{DateTime.Now.ToString("yyyyMM")}/{fileName}"",""mediaType"":""{mediaType}"",""base64"":""{Base64}"" }}"
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "请选择上传附件",
                        success = false
                    };
                }
            }
            catch (Exception ex)
            {
                return new MessageModel<string>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = "文件保存失败,原因:" + ex.Message
                };
            }
        }

        /// <summary>
        /// 查询使用者上传文件
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<fileInfoVModel>>> GetFileInfoList( string userName, int pageIndex = 1, int pageSize = 10)
        {
            try
            {
                var data = await this.iCommonServices.GetFileInfoList(userName, pageIndex, pageSize);
                return new MessageModel<PageModel<fileInfoVModel>>()
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
                return new MessageModel<PageModel<fileInfoVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false
                };
            }
        }
        /// <summary>
        /// 查询不动产证明房屋信息
        /// </summary>
        /// <param name="SFZH">身份证号</param>
        /// <param name="QLRMC">权利人名称</param>
        /// <param name="bdczh">不动产证号</param>
        /// <returns>分页结果集</returns>
        [HttpGet]
        public async Task<MessageModel<PageStringModel>> GetBdcDyHouseInfo(string SFZH, string QLRMC, string bdczh)
        {
            try
            {
                //var data = await this._BdcGeneralMrgeServices.GetBdcHourseInfo(SFZH, QLRMC, QLRMC, bdczh);
                var data = new PageStringModel();
                return new MessageModel<PageStringModel>()
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
                return new MessageModel<PageStringModel>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false
                };
            }
        }
        /// <summary>
        /// 查询政策信息
        /// </summary>
        /// <param name="DJZC">登记政策</param>
        /// <returns>分页结果集</returns>
        [HttpGet]
        public async Task<MessageModel<PageStringModel>> GetZCInfo(string DJZC)
        {
            try
            {
                //var data = await this._BdcGeneralMrgeServices.GetBdcHourseInfo(SFZH, QLRMC, QLRMC, bdczh);
                var data = new PageStringModel();
                return new MessageModel<PageStringModel>()
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
                return new MessageModel<PageStringModel>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false
                };
            }
        }
        /// <summary>
        /// 查询办事指南
        /// </summary>
        /// <param name="BSLX">办事类型</param>
        /// <returns>分页结果集</returns>
        [HttpGet]
        public async Task<MessageModel<PageStringModel>> GetZNInfo(string BSLX)
        {
            try
            {
                //var data = await this._BdcGeneralMrgeServices.GetBdcHourseInfo(SFZH, QLRMC, QLRMC, bdczh);
                var data = new PageStringModel();
                return new MessageModel<PageStringModel>()
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
                return new MessageModel<PageStringModel>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false
                };
            }
        }
        /// <summary>
        /// 查询公告共示
        /// </summary>
        /// <returns>分页结果集</returns>
        [HttpGet]
        public async Task<MessageModel<PageStringModel>> GetGGGSInfo()
        {
            try
            {
                //var data = await this._BdcGeneralMrgeServices.GetBdcHourseInfo(SFZH, QLRMC, QLRMC, bdczh);
                var data = new PageStringModel();
                return new MessageModel<PageStringModel>()
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
                return new MessageModel<PageStringModel>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false
                };
            }
        }
    }
}
