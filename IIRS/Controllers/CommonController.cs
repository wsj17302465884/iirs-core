using IIRS.IRepository;
using IIRS.IRepository.IIRS;
using IIRS.IServices;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Utilities;
using IIRS.Utilities.Common;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RT.Comb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

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
    public class CommonController : Controller
    {
        private readonly ICommonServices _ICommonServices;
        private readonly ISysDataRecorderRepository _SysDataRecorderRepository;
        private readonly ILogger<CommonController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IUserInfoRepository _userInfoRepository;
        private readonly ISpbInfoRepository _spbInfoRepository;
        readonly IDYServices _IDYServices;
        private readonly IChangeMrgeServices _IChangeMrgeServices;

        public CommonController(ICommonServices commonServices, ILogger<CommonController> logger, ISysDataRecorderRepository sysDataRecorderRepository, IWebHostEnvironment webHostEnvironment, IUserInfoRepository userInfoRepository, 
            IDYServices dyServices, ISpbInfoRepository spbInfoRepository, IChangeMrgeServices _IChangeMrgeServices)
        {
            this._ICommonServices = commonServices;
            this._SysDataRecorderRepository = sysDataRecorderRepository;
            this._logger = logger;
            this._webHostEnvironment = webHostEnvironment;
            this._userInfoRepository = userInfoRepository;
            this._IDYServices = dyServices;
            this._spbInfoRepository = spbInfoRepository;
            this._IChangeMrgeServices = _IChangeMrgeServices;
        }

        /// <summary>
        /// 查询当前流程相关信息
        /// </summary>
        /// <param name="XID">流程编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<VerifyFlowVModel>> FlowInfoQuery(string XID)
        {
            if (!string.IsNullOrEmpty(XID))
            {
                try
                {
                    return new MessageModel<VerifyFlowVModel>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = await this._ICommonServices.FlowInfoQuery(XID)
                    };
                }
                catch (Exception ex)
                {
                    string logErrorCode = Provider.Sql.Create().ToString("N");
                    string errorLog = $"CommonController.FlowInfoQuery:【错误代码：{logErrorCode},原因:{ex.Message}】";
                    this._logger.LogDebug(errorLog);
                    return new MessageModel<VerifyFlowVModel>()
                    {
                        msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                        success = false,
                        response = null
                    };
                }
            }
            return new MessageModel<VerifyFlowVModel>()
            {
                msg = $"请输入流程编号！",
                success = false,
                response = null
            };
        }

        /// <summary>
        /// 查询审批表信息
        /// </summary>
        /// <param name="XID">XID</param>
        /// <returns></returns>
        [HttpGet]
        public MessageModel<SPB_INFO> SpbQuery(string XID)
        {
            if (!string.IsNullOrEmpty(XID))
            {
                try
                {
                    var spInfo = this._spbInfoRepository.Query(s => s.XID == XID).Result;
                    if (spInfo.Count > 0)
                    {
                        return new MessageModel<SPB_INFO>()
                        {
                            msg = "获取成功",
                            success = true,
                            response = spInfo[0]
                        };
                    }
                }
                catch (Exception ex)
                {
                    string logErrorCode = Provider.Sql.Create().ToString("N");
                    string errorLog = $"CommonController.GetSpInfo:【错误代码：{logErrorCode},原因:{ex.Message}】";
                    this._logger.LogDebug(errorLog);
                    return new MessageModel<SPB_INFO>()
                    {
                        msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                        success = false,
                        response = null
                    };
                }
            }
            return new MessageModel<SPB_INFO>()
            {
                msg = "获取成功",
                success = true,
                response = null
            };
        }

        /// <summary>
        /// 初始化抵押上报文件树形格式信息
        /// </summary>
        /// <param name="GID">业务分组编号</param>
        /// <param name="XID">受理现实手编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<Base64FilesVModel>> GetInitMediasTree(int GID, string XID)
        {
            try
            {
                var data = await this._IDYServices.GetInitMedias(GID);
                List<PUB_ATT_FILE> fileList = null;
                if (!string.IsNullOrEmpty(XID))
                {
                    fileList = await _ICommonServices.UploadFileQueryByXID(XID);
                }
                if (data.Groups.Count > 0)
                {
                    Base64FilesVModel treeRoot = new Base64FilesVModel()
                    {
                        ID = Guid.Empty.ToString("N"),
                        IMG = "",
                        Level = -1,
                        label = "虚拟节点"
                    };
                    Base64FilesVModel model = new Base64FilesVModel()
                    {
                        ID = Guid.Empty.ToString("N"),
                        IMG = "",
                        Level = 0,
                        label = "附件选择"
                    };
                    treeRoot.children.Add(model);
                    Base64FilesVModel dicNode = null;
                    foreach (MediasGroups attFile in data.Groups)
                    {
                        dicNode = new Base64FilesVModel()
                        {
                            ID = attFile.GroupsID,
                            IMG = "",
                            Level = 1,
                            label = attFile.GroupsName,
                            MaxPageNo = 0
                        };
                        if (fileList != null && fileList.Count > 0)
                        {
                            var fileInfo = fileList.Where(s => s.GROUP_ID == attFile.GroupsID).OrderBy(W => W.ODR).ToList();
                            dicNode.MaxPageNo = fileInfo.Count;
                            foreach (var file in fileInfo)
                            {
                                string imgPath = this._webHostEnvironment.WebRootPath + file.PATH + file.FILE_NAME;//物理绝对路径
                                string imgBase64 = SysUtility.FileToBase64(imgPath);
                                dicNode.children.Add(new Base64FilesVModel()
                                {
                                    ID = file.FILE_ID,
                                    IMG = imgBase64,
                                    IsBase64 = 1,
                                    Level = 2,
                                    label = file.DISPLAY_NAME
                                });
                            }
                        }
                        model.children.Add(dicNode);
                    }
                    return new MessageModel<Base64FilesVModel>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = treeRoot
                    };
                }
                else
                {
                    return new MessageModel<Base64FilesVModel>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = null
                    };
                }
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"DyController.GetInitMediasTree:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<Base64FilesVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 人证识别授权办理
        /// </summary>
        /// <param name="strFaceInfo">认证识别信息</param>
        /// <param name="PASSWORD">授权人密码</param>
        /// <param name="NAME">授权人账号</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<int>> SqrFaceRecognition([FromForm] string strFaceInfo, [FromForm] string PASSWORD, [FromForm] string NAME)
        {
            try
            {
                // var isTure = true;
                int count = 0;
                string saveDataJson = HttpUtility.UrlDecode(strFaceInfo);
                FACE_RECOGNITION_AUTHORIZE FacerecInfo = null;
                try
                {
                    FacerecInfo = JsonConvert.DeserializeObject<FACE_RECOGNITION_AUTHORIZE>(saveDataJson);
                }
                catch (Exception ex)
                {
                    return new MessageModel<int>()
                    {
                        msg = "数据保存格式错误，请与管理员联系",
                        success = false
                    };
                }
                if (string.IsNullOrEmpty(NAME) || string.IsNullOrEmpty(PASSWORD))
                {
                    return new MessageModel<int>()
                    {
                        msg = "用户名或密码不能为空",
                        success = false,
                        response = 0,
                    };
                }

                var pwd = EncryptHelper.MD5Encrypt32(EncryptHelper.RsaDecrypt(PASSWORD));
                var user = await _userInfoRepository.Query(a => a.LoginName == NAME && a.LoginPWD == pwd && a.IsDeleted == false);
                if (user.Count > 0)
                {
                    var userid = user[0].Id;
                    DateTime AUTHORIZE_TIME = DateTime.Now;

                    FACE_RECOGNITION_AUTHORIZE FRAlist = new FACE_RECOGNITION_AUTHORIZE()
                    {
                        PK = Guid.NewGuid(),
                        AUTHORIZE_ID = userid,
                        AUTHORIZE_REMARK = FacerecInfo.AUTHORIZE_REMARK,
                        AUTHORIZE_TIME = AUTHORIZE_TIME,
                        SLBH = FacerecInfo.SLBH,
                        AUTHORIZE_USER = FacerecInfo.AUTHORIZE_USER,
                        AUTHORIZE_USER_ID = FacerecInfo.AUTHORIZE_USER_ID,
                    };

                    count = _ICommonServices.FaceUserRec(FRAlist);
                    return new MessageModel<int>()
                    {
                        msg = "授权完成！",
                        success = true,
                        response = count,
                    };
                }
                else
                {
                    return new MessageModel<int>()
                    {
                        msg = "授权人ID:" + NAME + "账号或密码不正确",
                        success = false,
                        response = 0,
                    };
                }
            }
            catch (Exception ex)
            {
                return new MessageModel<int>()
                {
                    msg = "授权出现异常，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 查询业务表单提交过程JSON数据查询
        /// </summary>
        /// <param name="XID">流程编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<SysDataRecorderModel>> BizJsonDataQueryByXID(string XID)
        {
            try
            {
                return new MessageModel<SysDataRecorderModel>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = await this._ICommonServices.BizJsonDataQuery(XID)
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"CommonController.BizJsonDataQueryByXID:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<SysDataRecorderModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false
                };
            }
        }

        /// <summary>
        /// 查询业务表单提交过程JSON数据查询
        /// </summary>
        /// <param name="PK">业务编号主键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<SysDataRecorderModel>> BizJsonDataQueryPK(string PK)
        {
            try
            {
                if(!string.IsNullOrEmpty(PK))
                {
                    return new MessageModel<SysDataRecorderModel>()
                    {
                        msg = "数据查询成功",
                        success = true,
                        response = await this._SysDataRecorderRepository.QueryById(PK)
                    };
                }
                else
                {
                    return new MessageModel<SysDataRecorderModel>()
                    {
                        msg = "数据查询失败，请输入查询参数",
                        success = false,
                        response = null
                    };
                }
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"CommonController.BizJsonDataQueryPK:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<SysDataRecorderModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false
                };
            }
        }

        /// <summary>
        /// 查询业务表单提交过程JSON数据列表查询
        /// </summary>
        /// <param name="FLOW_ID">流程节点编码</param>
        /// <param name="USER_ID">用户ID</param>
        /// <param name="remarks1">查询条件1</param>
        /// <param name="remarks2">查询条件2</param>
        /// <param name="remarks3">查询条件3</param>
        /// <param name="remarks4">查询条件4</param>
        /// <param name="remarks5">查询条件5</param>
        /// <param name="pageIndex">查询分页页码</param>
        /// <param name="pageSize">每页显示数据数量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<string>> BizJsonDataPageQuery(decimal FLOW_ID, string USER_ID, string remarks1, string remarks2, string remarks3, string remarks4, string remarks5, int pageIndex, int pageSize)
        {
            try
            {
                var jsonDataList = await this._ICommonServices.BizJsonDataPageQuery(FLOW_ID,USER_ID, remarks1, remarks2, remarks3, remarks4, remarks5, pageIndex, pageSize);
                return new MessageModel<string>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = jsonDataList
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"CommonController.BizJsonDataPageQuery:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<string>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 不动产权利人查询
        /// </summary>
        /// <param name="ZJHM">查询证件号码</param>
        /// <param name="MC">查询人名称</param>
        /// <param name="pageIndex">查询分页页码</param>
        /// <param name="pageSize">查询分页每页长度</param>
        /// <returns>权利人结果集</returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<QLR_VModel>>> BdcUserQuery(string ZJHM, string MC, int pageIndex, int pageSize)
        {
            try
            {
                var dicList = await this._ICommonServices.BdcUserQuery(ZJHM, MC, pageIndex, pageSize);
                return new MessageModel<PageModel<QLR_VModel>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<PageModel<QLR_VModel>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 追述附件查询
        /// </summary>
        /// <param name="SLBH">受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<El_CascaderNavTree>> AttachmentQueryBySlbh(string SLBH)
        {
            try
            {
                var dicList = await this._ICommonServices.AttachmentQueryBySlbh(SLBH);
                return new MessageModel<El_CascaderNavTree>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                //var dicList = await this._SysDicRepository.Query(S => S.GID == GID && S.IS_USED == 1);
                return new MessageModel<El_CascaderNavTree>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 获取本年度不动产证明编号计数器值
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<string[]>> GetBDCZH_NUM()
        {
            try
            {
                return new MessageModel<string[]>()
                {
                    msg = "获取成功",
                    success = true,
                    response = await this._ICommonServices.GetBDCZH_NUM()
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"ChangeMrgeController.GetBDCZH_NUM:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<string[]>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 获取本年度不动产证书编号计数器值
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<string[]>> GetBDCZMH_NUM()
        {
            try
            {
                return new MessageModel<string[]>()
                {
                    msg = "获取成功",
                    success = true,
                    response = await this._ICommonServices.GetBDCZMH_NUM()
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"ChangeMrgeController.GetBDCZH_NUM:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<string[]>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null
                };
            }
        }
    }
}