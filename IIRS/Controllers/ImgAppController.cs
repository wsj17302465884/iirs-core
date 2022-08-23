using IIRS.IRepository.IIRS;
using IIRS.IServices;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RT.Comb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 图片高级编辑外部接口
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class ImgAppController : ControllerBase
    {
        readonly ICommonServices _ICommonServices;
        readonly ISysDataRecorderRepository _SysDataRecorderRepository;
        private readonly ILogger<CommonController> _logger;

        public ImgAppController(ICommonServices commonServices, ILogger<CommonController> logger, ISysDataRecorderRepository sysDataRecorderRepository)
        {
            this._ICommonServices = commonServices;
            this._SysDataRecorderRepository = sysDataRecorderRepository;
            this._logger = logger;
        }

        /// <summary>
        /// 暂存图片查询
        /// </summary>
        /// <param name="PK">主键编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<string>> ImgQueryByPK(Guid PK)
        {
            if (PK != null)
            {
                string _pk = PK.ToString("N").ToUpper();
                var result = await this._SysDataRecorderRepository.QueryById(_pk);
                if (result != null)
                {
                    return new MessageModel<string>()
                    {
                        msg = result.REMARKS1,
                        success = true,
                        response = result.SAVEDATAJSON
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "无数据",
                        success = false,
                    };
                }
            }
            else
            {
                return new MessageModel<string>()
                {
                    msg = "请填写输入参数！",
                    success = false,
                };
            }
        }

        /// <summary>
        /// 通过APP添加中间交互图片信息
        /// </summary>
        /// <param name="PK">编辑图片主键</param>
        /// <param name="base64Img">图片Base64文本</param>
        /// <returns>通信主键</returns>
        [HttpPost]
        public async Task<MessageModel<string>> ImgAppEdit([FromForm] Guid PK, [FromForm] string base64Img)
        {
            try
            {
                SysDataRecorderModel imgModel = new SysDataRecorderModel()
                {
                    PK = PK.ToString("N").ToUpper(),
                    CDATE = DateTime.Now.Date,
                    SAVEDATAJSON = base64Img,
                    REMARKS5 = "DEL_IMG_EDIT"
                };
                List<string> cols = new List<string>();
                cols.Add("CDATE");
                cols.Add("SAVEDATAJSON");
                bool count = await this._SysDataRecorderRepository.Update(imgModel, cols);
                return new MessageModel<string>()
                {
                    msg = "成功",
                    success = true,
                    response = count ? imgModel.PK : string.Empty
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<string>()
                {
                    msg = $"系统错误，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 通过APP添加中间交互图片信息
        /// </summary>
        /// <param name="ImgName">显示图片名称</param>
        /// <param name="base64Img">图片Base64文本</param>
        /// <returns>通信主键</returns>
        [HttpPost]
        public async Task<MessageModel<string>> ImgAppSave([FromForm] string ImgName, [FromForm] string base64Img)
        {
            try
            {
                string _PK = Provider.Sql.Create().ToString("N").ToUpper();
                SysDataRecorderModel imgModel = new SysDataRecorderModel()
                {
                    PK = _PK,
                    BUS_PK = _PK,
                    CDATE = DateTime.Now.Date,
                    SAVEDATAJSON = base64Img,
                    REMARKS1 = ImgName,
                    REMARKS5 = "DEL_IMG_EDIT"
                };
                int count = await this._ICommonServices.BizJsonInsert(imgModel);
                return new MessageModel<string>()
                {
                    msg = "成功",
                    success = true,
                    response = count > 0 ? imgModel.PK : string.Empty
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<string>()
                {
                    msg = $"系统错误，原因：" + ex.Message,
                    success = false
                };
            }
        }
    }
}
