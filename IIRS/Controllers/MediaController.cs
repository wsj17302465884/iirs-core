using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using RT.Comb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 系统字典表信息查询
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class MediaController : Controller
    {
        readonly IWebHostEnvironment _webHostEnvironment;

        public MediaController(IWebHostEnvironment webHostEnvironment)
        {
            this._webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 上传临时文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<MessageModel<string>> UploadTempFilePost()
        {
            var files = Request.Form.Files;
            //string mediaType = string.Empty;
            try
            {
                if (files.Count > 0)
                {
                    string path = string.Format(@"\TempUpload\{0:yyyyMM}\", DateTime.Now);
                    string absPath = this._webHostEnvironment.WebRootPath + path;//物理绝对路径
                    if (!Directory.Exists(absPath))
                    {
                        Directory.CreateDirectory(absPath);
                    }
                    var file = files[0];
                    string mediaType = Path.GetExtension(file.FileName).ToLower();
                    string fileName = Provider.Sql.Create().ToString("N") + mediaType;
                    string fullName = Path.Combine(absPath, fileName);
                    if (!System.IO.File.Exists(fullName))
                    {
                        using (var fileStream = new FileStream(fullName, FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        //mediaType = Path.GetExtension(fullName).ToLower();
                    }

                    return new MessageModel<string>()
                    {
                        msg = "数据查询成功",
                        success = true,
                        response = @$"{{""path"":""/TempUpload/{DateTime.Now.ToString("yyyyMM")}/{fileName}"",""mediaType"":""{mediaType}"" }}"
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

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<MessageModel<List<PUB_ATT_FILE>>> Upload()
        {
            try
            {
                PUB_ATT_FILE upload = new PUB_ATT_FILE();
                var files = Request.Form.Files;
                List<PUB_ATT_FILE> uploadFile = new List<PUB_ATT_FILE>();
                PUB_ATT_FILE fileModel = null;
                try
                {
                    if (files.Count > 0)
                    {
                        string path = string.Format(@"\upload\{0:yyyy}\{0:MM}\{0:dd}\", DateTime.Now);
                        if (!Directory.Exists(this._webHostEnvironment.WebRootPath + path))
                        {
                            Directory.CreateDirectory(this._webHostEnvironment.WebRootPath + path);
                        }
                        foreach (var file in files)
                        {
                            fileModel = new PUB_ATT_FILE();
                            fileModel.DISPLAY_NAME = file.FileName;
                            fileModel.MEDIA_TYPE = Path.GetExtension(file.FileName);
                            fileModel.FILE_ID = Provider.Sql.Create().ToString("N");
                            fileModel.FILE_NAME = fileModel.FILE_ID + fileModel.MEDIA_TYPE;
                            fileModel.PATH = path;
                            var fullName = Path.Combine(this._webHostEnvironment.WebRootPath + path, fileModel.FILE_NAME);
                            if (!System.IO.File.Exists(fullName))
                            {
                                using (var fileStream = new FileStream(fullName, FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                }
                            }
                            uploadFile.Add(fileModel);
                        }
                    }
                    return new MessageModel<List<PUB_ATT_FILE>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = uploadFile
                    };
                }
                catch (Exception ex)
                {
                    return new MessageModel<List<PUB_ATT_FILE>>()
                    {
                        msg = "文件保存失败,原因:" + ex.Message,
                        success = true,
                        response = uploadFile
                    };
                }
            }
            catch (Exception ex2)
            {
                return new MessageModel<List<PUB_ATT_FILE>>()
                {
                    msg = "文件上传失败,原因:" + ex2.Message,
                    success = true,
                    response = null
                };
            }
        }
    }
}