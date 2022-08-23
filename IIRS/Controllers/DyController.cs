using IIRS.IRepository;
using IIRS.IServices;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Utilities.Common;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RT.Comb;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    public class DyController : ControllerBase
    {
        private readonly ILogger<DyController> _logger;
        readonly IDYServices _IDYServices;
        readonly IUserInfoRepository _userInfoRepository;
        readonly IWebHostEnvironment _webHostEnvironment;
        public DyController(ILogger<DyController> logger, IUserInfoRepository userInfoRepository, IDYServices dyServices, IWebHostEnvironment webHostEnvironment)
        {
            this._logger = logger;
            this._IDYServices = dyServices;
            _userInfoRepository = userInfoRepository;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public string Upload(string slbh)
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
                        string path = string.Format(@"\upload\{0:yyyy}\{0:MM}\{0:dd}\{1}\", DateTime.Now, slbh);
                        string absPath = this._webHostEnvironment.WebRootPath + path;//物理绝对路径
                        if (!Directory.Exists(absPath))
                        {
                            Directory.CreateDirectory(absPath);
                        }
                        else //如果目录已经存在，则说明是二次操作，则应该先删除对应数据库记录以及文件夹文件
                        {
                            Directory.Delete(absPath, true);
                            Directory.CreateDirectory(absPath);
                        }
                        Dictionary<string, string> dicGroups = new Dictionary<string, string>();
                        Dictionary<string, int> dicGroupsOdrNum = new Dictionary<string, int>();
                        string groupPK = string.Empty, groupName = string.Empty;
                        int groupOdrNum = -1;
                        string fileDisplayName = string.Empty;
                        foreach (var file in files)
                        {
                            if (file.Name.Contains("@@"))
                            {
                                string[] fileInfo = file.Name.Split("@@");//数组0分组名称，数组1文件显示名称
                                if (fileInfo.Length == 2)
                                {
                                    groupName = fileInfo[0];
                                    fileDisplayName = fileInfo[1];
                                    if (!dicGroups.Keys.Contains(groupName))
                                    {
                                        groupPK = Provider.Sql.Create().ToString("N");
                                        dicGroups.Add(fileInfo[0], groupPK);
                                        groupOdrNum = 1;
                                        dicGroupsOdrNum.Add(fileInfo[0], groupOdrNum);
                                    }
                                    else
                                    {
                                        groupOdrNum = dicGroupsOdrNum[fileInfo[0]];
                                        groupOdrNum++;
                                        dicGroupsOdrNum[fileInfo[0]] = groupOdrNum;
                                        groupPK = dicGroups[fileInfo[0]];
                                    }
                                    string mediaType = Path.GetExtension(file.FileName);
                                    fileModel = new PUB_ATT_FILE()
                                    {
                                        BAK_PK = slbh,
                                        DISPLAY_NAME = fileDisplayName,//file.FileName;
                                        MEDIA_TYPE = mediaType.Replace(".", ""),
                                        FILE_ID = Provider.Sql.Create().ToString("N"),
                                        FILE_NAME = Provider.Sql.Create().ToString("N") + mediaType,
                                        PATH = path,
                                        CDATE = DateTime.Now,
                                        GROUP_ID = groupPK,
                                        GROUP_NAME = groupName,
                                        ODR = groupOdrNum
                                    };
                                    uploadFile.Add(fileModel);
                                    var fullName = Path.Combine(absPath, fileModel.FILE_NAME);
                                    if (!System.IO.File.Exists(fullName))
                                    {
                                        using (var fileStream = new FileStream(fullName, FileMode.Create))
                                        {
                                            file.CopyTo(fileStream);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    this._IDYServices.SaveUploadFileDB(uploadFile);
                    return "OK";
                }
                catch (Exception ex)
                {
                    return "文件保存失败,原因:" + ex.Message;
                }
            }
            catch (Exception ex2)
            {
                return "文件保存失败,原因:" + ex2.Message;
            }
        }

        [HttpGet]
        public async Task<MessageModel<IFLOW_ACTION_BACK>> FlowBackReason(string AUZ_ID, int flow_ID)
        {
            var reasonModel = await this._IDYServices.FlowBackReason(AUZ_ID, flow_ID);
            return new MessageModel<IFLOW_ACTION_BACK>()
            {
                msg = "获取成功",
                success = true,
                response = reasonModel
            };
        }

        /// <summary>
        /// 查询退回原因
        /// </summary>
        /// <param name="XID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<IFLOW_ACTION_BACK>> FlowBackReasonByXID(string XID)
        {
            var reasonModel = await this._IDYServices.FlowBackReason(XID);
            return new MessageModel<IFLOW_ACTION_BACK>()
            {
                msg = "获取成功",
                success = true,
                response = reasonModel
            };
        }

        [HttpGet]
        public async Task<MessageModel<DyVModel>> GetDyInfo(string bdzcz, bool IsNewSlbh)
        {
            List<string> bdczh = JsonConvert.DeserializeObject<List<string>>(HttpUtility.UrlDecode(bdzcz));
            //List<string> bdczh = new List<string>();
            //bdczh.Add(bdzcz);
            DyVModel dyInfo = await this._IDYServices.GetHouseInfo(bdczh);
            if (IsNewSlbh && dyInfo != null)
            {
                dyInfo.NewSlbh = this._IDYServices.GetSLBH();//服务器端获取受理编号
            }
            var treeData= await this._IDYServices.GetInitMedias(2);
            Base64FilesVModel treeModel = null;
            if (treeData.Groups.Count > 0)
            {
                treeModel = new Base64FilesVModel()
                {
                    ID = Guid.Empty.ToString("N"),
                    IMG = "",
                    Level = 0,
                    label = "附件选择"
                };
                foreach (MediasGroups attFile in treeData.Groups)
                {
                    treeModel.children.Add(new Base64FilesVModel()
                    {
                        ID = attFile.GroupsID,
                        IMG = "",
                        Level = 1,
                        label = attFile.GroupsName
                    });
                }
            }
            dyInfo.attFiles = treeModel;
            return new MessageModel<DyVModel>()
            {
                msg = "获取成功",
                success = true,
                response = dyInfo
            };
        }

        [HttpGet]
        public MessageModel<string> NewSLBH()
        {
            string slbh = this._IDYServices.GetSLBH();
            return new MessageModel<string>()
            {
                msg = "获取成功",
                success = true,
                response = slbh
            };
        }

        /// <summary>
        /// 初始化抵押上报文件格式信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<MediasVModel>> GetInitMedias(int GID)
        {
            var data = await this._IDYServices.GetInitMedias(GID);
            return new MessageModel<MediasVModel>()
            {
                msg = "获取成功",
                success = true,
                response = data
            };
        }

        [HttpGet]
        public async Task<MessageModel<List<El_CascaderTree>>> MediasQuery(string slbh)
        {
            List<El_CascaderTree> data = null;
            if (!string.IsNullOrEmpty(slbh))
            {
                try
                {
                    var fileList = await _IDYServices.UploadFileQuery(slbh);
                    if (fileList.Count > 0)
                    {
                        data = new List<El_CascaderTree>();
                        var groups = fileList.Cast<PUB_ATT_FILE>().Select(s => new { GROUP_ID = s.GROUP_ID, GROUP_NAME = s.GROUP_NAME }).Distinct();
                        El_CascaderTree treeData = null;
                        foreach (var group in groups)
                        {
                            treeData = new El_CascaderTree()
                            {
                                label = group.GROUP_NAME,
                                value = group.GROUP_ID
                            };
                            var result = fileList.Where(s => s.GROUP_ID == treeData.value).OrderBy(e => e.ODR);
                            if (result.Count() > 0)
                            {
                                treeData.children = new List<El_CascaderTree>();
                                foreach (var value in result)
                                {
                                    
                                    treeData.children.Add(new El_CascaderTree()
                                    {
                                        label = value.DISPLAY_NAME,
                                        value = this.HttpContext.Request.Host.Value + value.PATH + value.FILE_NAME
                                    });
                                }
                            }
                            data.Add(treeData);
                        }

                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            return new MessageModel<List<El_CascaderTree>>()
            {
                msg = "查询成功",
                success = true,
                response = data
            };
        }

        [HttpPost]
        public async Task<MessageModel<int>> UpdateFile([FromForm] string slbh, [FromForm] string strFileTree)
        {
            try
            {
                List<Base64FilesVModel> fileTree = JsonConvert.DeserializeObject<List<Base64FilesVModel>>(strFileTree);
                int imgCount = fileTree.Where(S => S.children.Count > 0).Count();
                if (imgCount > 0)
                {
                    var attFileModel = SysUtility.UploadSysBase64File(this._webHostEnvironment.WebRootPath, slbh, fileTree);
                    int count = await _IDYServices.UpdateFile(attFileModel, slbh);
                    return new MessageModel<int>()
                    {
                        msg = "数据保存成功！",
                        success = true,
                        response = count
                    };
                }
                else
                {
                    //if (slbh.Length == 12)
                    //{
                    //    string strSaveDay = slbh.Substring(0, 8);
                    //}
                    //202009228002
                    return new MessageModel<int>()
                    {
                        msg = "数据保存成功！",
                        success = true,
                        response = 0
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<int>()
                {
                    msg = $"错误，原因：{ex.Message}！",
                    success = false,
                    response = 0
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"DyController.UpdateFile:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<int>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = 0
                };
            }

        }

        /// <summary>
        /// 初始化抵押上报文件树形格式信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<Base64FilesVModel>> GetInitMediasTree(int GID, string slbh)
        {
            try
            {
                var data = await this._IDYServices.GetInitMedias(GID);
                List<PUB_ATT_FILE> fileList = null;
                if (!string.IsNullOrEmpty(slbh))
                {
                    fileList = await _IDYServices.UploadFileQuery(slbh);
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

        [HttpGet]
        public async Task<MessageModel<int>> ApplyFinish(string slbh)
        {
            try
            {
                int count = await _IDYServices.UpdateFinish(slbh);
                return new MessageModel<int>()
                {
                    msg = "保存成功",
                    success = true,
                    response = count
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"DyController.UpdateFinish:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<int>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = 0
                }; ;
            }
        }

        [HttpPost]
        public async Task<MessageModel<int>> Post([FromForm] int IsTemp, [FromForm] string strDYVModel, [FromForm] string strFileTree)
        {
            string newSLBH = string.Empty;
            try
            {
                int count = 0;
                DateTime saveTime = DateTime.Now;
                string saveDataJson = HttpUtility.UrlDecode(strDYVModel);
                HouseVModel dyInfo = null;
                //string newSLBH = "20200813095210";
                try
                {
                    dyInfo = JsonConvert.DeserializeObject<HouseVModel>(saveDataJson);
                }
                catch
                {
                    return new MessageModel<int>()
                    {
                        msg = "数据保存格式错误，请于管理员联系",
                        success = false,
                        response = 0
                    };
                }
                newSLBH = dyInfo.SLBH;//服务器端获取受理编号
                List<Base64FilesVModel> fileTree = JsonConvert.DeserializeObject<List<Base64FilesVModel>>(strFileTree);
                List<PUB_ATT_FILE> attFileModel = null;
                string xid = Provider.Sql.Create().ToString();//主键
                int imgCount = fileTree.Where(S => S.children.Count > 0).Count();
                if (imgCount > 0)
                {
                    attFileModel = SysUtility.UploadSysBase64File(this._webHostEnvironment.WebRootPath, newSLBH, fileTree);
                    foreach(var file in attFileModel)
                    {
                        file.XID = xid;
                    }
                }
                //strDYVModel = HttpUtility.UrlDecode(strDYVModel);
                //DYVModel model = JsonConvert.DeserializeObject<DYVModel>(strDYVModel);

                List<TSGL_INFO> tsglET = new List<TSGL_INFO>();
                List<XGDJGL_INFO> xgdjglET = new List<XGDJGL_INFO>();
                List<QLRGL_INFO> qlrglET = new List<QLRGL_INFO>();
                
                dyInfo.CNSJ = saveTime.AddDays(3);
                string xgzh = string.Empty;
                if (dyInfo.selectDyHouse.Count > 0)
                {
                    xgzh = dyInfo.selectDyHouse[0].BDCZH + (dyInfo.selectDyHouse.Count > 1 ? "等" + dyInfo.selectDyHouse.Count + "个" : string.Empty);
                }
                DY_INFO dyET = new DY_INFO()
                {
                    SLBH = newSLBH,
                    YWSLBH = newSLBH,
                    DJLX = "抵押登记",
                    DJYY = dyInfo.DJYY,
                    XGZH = xgzh,
                    ZLXX = dyInfo.ZL,
                    //.selectDyHouse[0].ZL + (dyInfo.selectDyHouse.Count > 1 ? "等" + dyInfo.selectDyHouse.Count + "个" : string.Empty),
                    SQRQ = saveTime,
                    DYLX = dyInfo.DYLX,
                    DYSW = dyInfo.DYSW,
                    DYFS = dyInfo.DYFS,
                    DYMJ = dyInfo.dyMJ,
                    BDBZZQSE = dyInfo.BDBZQSE,
                    PGJE = dyInfo.PGJE,
                    HTH = dyInfo.HTH,
                    LXR = dyInfo.DYLXR,
                    LXRDH = dyInfo.DYLXRDH,
                    CNSJ = dyInfo.CNSJ,
                    //SJR = dyInfo.SJR,
                    FJ = dyInfo.BZ,
                    ZWR = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(p => p.QLRMC).ToArray()),
                    ZWRZJH = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(p => p.ZJHM).ToArray()),
                    ZWRZJLX = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(p => p.ZJLB).Distinct().ToArray()),//改成中文类型
                    DLJGMC = dyInfo.DYQRMC,
                    QLQSSJ = dyInfo.ZWLXQXQSRQ.Date,
                    QLJSSJ = dyInfo.ZWLXQXJZRQ.Date,
                    DYQX = dyInfo.LXQX.ToString(),
                    XID = xid
                };
                IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = SysFlowConst.FLOW_YBDY_DBDCZXDYSP,//根据是否为暂存操作，判断当前流程节点提交信息
                    AUZ_ID = dyInfo.AUZ_ID,
                    CDATE = saveTime,
                    USER_NAME = dyInfo.DYLXR
                };
                string slbhStr = string.Empty;
                if (dyInfo.selectDyHouse.Count > 0)
                {
                    slbhStr = dyInfo.selectDyHouse[0].SLBH + (dyInfo.selectDyHouse.Count > 1 ? "等" + dyInfo.selectDyHouse.Count + "个" : string.Empty);
                }
                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    XID = xid,
                    YWSLBH = newSLBH,
                    DJZL = 910,
                    BDCZH = dyET.XGZH,
                    SLBH = slbhStr,
                    QLRMC = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(s => s.QLRMC).ToArray()),
                    //dyInfo.selectDyPerson[0].QLRMC
                    ZL = dyET.ZLXX,
                    ORG_ID = dyInfo.BankDeptID,
                    USER_ID = dyInfo.DYQRMC_ID,
                    TEL = dyInfo.DYLXRDH,
                    AUZ_ID = dyInfo.AUZ_ID,
                    HTH = dyInfo.HTH,
                    REMARK1 = "个人抵押",
                    SJR = dyInfo.SJR,
                    IS_ACTION_OK = 0,
                    SaveDataJson = saveDataJson
                };
                //string warmMsg = string.Empty;
                //for (int i = dyInfo.attFiles.Count - 1; i >= 0; i--)
                //{
                //    var data = dyInfo.mediasVModel.Groups.Cast<MediasGroups>().Where(s => s.GroupsName == dyInfo.attFiles[i].GROUP_NAME);
                //    if (data.Count() == 1)
                //    {
                //        var selectGroup = data.FirstOrDefault();
                //        dyInfo.attFiles[i].GROUP_ID = selectGroup.GroupsID;
                //        var itemResult = selectGroup.Items.Cast<MediasItems>().Where(mi => mi.Name == dyInfo.attFiles[i].FILE_NAME);
                //        if (itemResult.Count() == 1)
                //        {
                //            var selectItem = itemResult.FirstOrDefault();
                //            dyInfo.attFiles[i].DISPLAY_NAME = selectItem.Name;
                //        }
                //        else
                //        {
                //            dyInfo.attFiles.Remove(dyInfo.attFiles[i]);
                //            continue;
                //        }
                //    }
                //    else
                //    {
                //        dyInfo.attFiles.Remove(dyInfo.attFiles[i]);
                //        continue;
                //    }
                //}

                foreach (var hourse in dyInfo.selectDyHouse)
                {
                    tsglET.Add(new TSGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        BDCLX = hourse.BDCLX,//房屋或土地
                        TSTYBM = hourse.TSTYBM,
                        BDCDYH = hourse.BDCDYH,
                        DJZL = "抵押",
                        CSSJ = saveTime,
                        XID = xid
                    });
                    xgdjglET.Add(new XGDJGL_INFO()
                    {
                        BGBM = Provider.Sql.Create().ToString(),
                        ZSLBH = newSLBH,
                        FSLBH = hourse.SLBH,
                        BGRQ = saveTime,
                        BGLX= "抵押",
                        XGZLX = hourse.ZSLX,
                        XGZH = hourse.BDCZH,
                        XID = xid
                    });
                }
                
                foreach (var person in dyInfo.selectDyPerson)
                {
                    qlrglET.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        YWBM = newSLBH,
                        ZJHM = person.ZJHM,
                        QLRID = person.QLRID,
                        QLRMC = person.QLRMC,
                        //ZJLB = person.ZJLB,
                        ZJLB = person.ZJLB,
                        ZJLB_ZWM = person.ZJLB_ZWM,
                        DH = person.DH,
                        QLRLX = "抵押人",
                        SXH = person.SXH,
                        IS_OWNER = person.IS_OWNER,
                        XID = xid
                    });
                }
                qlrglET.Add(new QLRGL_INFO()
                {
                    GLBM = Provider.Sql.Create().ToString(),
                    SLBH = newSLBH,
                    YWBM = newSLBH,
                    ZJHM = dyInfo.YHYTSHXYDM,
                    QLRID = dyInfo.YHYTSHXYDM2,
                    QLRMC = dyInfo.DYQRMC,
                    ZJLB = "8",//"统一社会信用代码",
                    ZJLB_ZWM = "统一社会信用代码",//"统一社会信用代码",
                    DH = dyInfo.DYLXRDH,
                    QLRLX = "抵押权人",
                    XID = xid
                });
                count = await _IDYServices.Mortgage(regInfo, flowInfo, tsglET, dyET, xgdjglET, qlrglET, attFileModel);
                return new MessageModel<int>()
                {
                    msg = "保存成功",
                    success = true,
                    response = count
                };
            }
            catch (Exception ex)
            {
                if(!string.IsNullOrEmpty(newSLBH))
                {
                    SysUtility.Delete(this._webHostEnvironment.WebRootPath, newSLBH);
                }
                return new MessageModel<int>()
                {
                    msg = "保存失败" + ex.Message,
                    success = false,
                    response = 0
                };
            }

        }

        [HttpPost]
        public async Task<MessageModel<int>> Edit([FromForm] string strDYVModel, [FromForm] string strFileTree)
        {
            string newSLBH = string.Empty;
            try
            {
                int count = 0;
                DateTime saveTime = DateTime.Now;
                string saveDataJson = HttpUtility.UrlDecode(strDYVModel);
                HouseVModel dyInfo = JsonConvert.DeserializeObject<HouseVModel>(saveDataJson);
                //string newSLBH = "20200813095210";
                newSLBH = dyInfo.SLBH;//服务器端获取受理编号
                List<Base64FilesVModel> fileTree = JsonConvert.DeserializeObject<List<Base64FilesVModel>>(strFileTree);
                List<PUB_ATT_FILE> attFileModel = null;
                string xid = Provider.Sql.Create().ToString();//主键
                int imgCount = fileTree.Where(S => S.children.Count > 0).Count();
                if (imgCount > 0)
                {
                    attFileModel = SysUtility.UploadSysBase64File(this._webHostEnvironment.WebRootPath, newSLBH, fileTree);
                    foreach (var file in attFileModel)
                    {
                        file.XID = xid;
                    }
                }
                //strDYVModel = HttpUtility.UrlDecode(strDYVModel);
                //DYVModel model = JsonConvert.DeserializeObject<DYVModel>(strDYVModel);

                List<TSGL_INFO> tsglET = new List<TSGL_INFO>();
                List<XGDJGL_INFO> xgdjglET = new List<XGDJGL_INFO>();
                List<QLRGL_INFO> qlrglET = new List<QLRGL_INFO>();

                dyInfo.CNSJ = saveTime.AddDays(3);
                string xgzh = string.Empty;
                if (dyInfo.selectDyHouse.Count > 0)
                {
                    xgzh = dyInfo.selectDyHouse[0].BDCZH + (dyInfo.selectDyHouse.Count > 1 ? "等" + dyInfo.selectDyHouse.Count + "个" : string.Empty);
                }
                DY_INFO dyET = new DY_INFO()
                {
                    SLBH = newSLBH,
                    YWSLBH = newSLBH,
                    DJLX = "抵押登记",
                    DJYY = dyInfo.DJYY,
                    XGZH = xgzh,
                    ZLXX = dyInfo.ZL,
                    //.selectDyHouse[0].ZL + (dyInfo.selectDyHouse.Count > 1 ? "等" + dyInfo.selectDyHouse.Count + "个" : string.Empty),
                    SQRQ = saveTime,
                    DYLX = dyInfo.DYLX,
                    DYSW = dyInfo.DYSW,
                    DYFS = dyInfo.DYFS,
                    DYMJ = dyInfo.dyMJ,
                    BDBZZQSE = dyInfo.BDBZQSE,
                    PGJE = dyInfo.PGJE,
                    HTH = dyInfo.HTH,
                    LXR = dyInfo.DYLXR,
                    LXRDH = dyInfo.DYLXRDH,
                    CNSJ = dyInfo.CNSJ,
                    //SJR = dyInfo.SJR,
                    FJ = dyInfo.BZ,
                    ZWR = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(p => p.QLRMC).ToArray()),
                    ZWRZJH = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(p => p.ZJHM).ToArray()),
                    ZWRZJLX = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(p => p.ZJLB).Distinct().ToArray()),//改成中文类型
                    DLJGMC = dyInfo.DYQRMC,
                    QLQSSJ = dyInfo.ZWLXQXQSRQ.Date,
                    QLJSSJ = dyInfo.ZWLXQXJZRQ.Date,
                    DYQX = dyInfo.LXQX.ToString(),
                    XID = xid
                };
                IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = SysFlowConst.FLOW_YBDY_DBDCZXDYSP,
                    AUZ_ID = dyInfo.AUZ_ID,
                    CDATE = saveTime,
                    USER_NAME = dyInfo.DYLXR
                };
                string slbhStr = string.Empty;
                if (dyInfo.selectDyHouse.Count > 0)
                {
                    slbhStr = dyInfo.selectDyHouse[0].SLBH + (dyInfo.selectDyHouse.Count > 1 ? "等" + dyInfo.selectDyHouse.Count + "个" : string.Empty);
                }
                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    XID = xid,
                    YWSLBH = newSLBH,
                    DJZL = 1,
                    BDCZH = dyET.XGZH,
                    SLBH = slbhStr,
                    QLRMC = string.Join(",", dyInfo.selectDyPerson.Cast<DyPersonVModel>().Select(s => s.QLRMC).ToArray()),
                    //dyInfo.selectDyPerson[0].QLRMC
                    ZL = dyET.ZLXX,
                    ORG_ID = dyInfo.BankDeptID,
                    USER_ID = dyInfo.DYQRMC_ID,
                    TEL = dyInfo.DYLXRDH,
                    AUZ_ID = dyInfo.AUZ_ID,
                    HTH = dyInfo.HTH,
                    REMARK1 = "个人抵押",
                    SJR = dyInfo.SJR,
                    IS_ACTION_OK = 0,
                    SaveDataJson = saveDataJson
                };
                foreach (var hourse in dyInfo.selectDyHouse)
                {
                    tsglET.Add(new TSGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        BDCLX = hourse.BDCLX,//房屋或土地
                        TSTYBM = hourse.TSTYBM,
                        BDCDYH = hourse.BDCDYH,
                        DJZL = "抵押",
                        CSSJ = saveTime,
                        XID = xid
                    });
                    xgdjglET.Add(new XGDJGL_INFO()
                    {
                        BGBM = Provider.Sql.Create().ToString(),
                        ZSLBH = newSLBH,
                        FSLBH = hourse.SLBH,
                        BGRQ = saveTime,
                        BGLX = "抵押",
                        XGZLX = hourse.ZSLX,
                        XGZH = hourse.BDCZH,
                        XID = xid
                    });
                }

                foreach (var person in dyInfo.selectDyPerson)
                {
                    qlrglET.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        YWBM = newSLBH,
                        ZJHM = person.ZJHM,
                        QLRID = person.QLRID,
                        QLRMC = person.QLRMC,
                        //ZJLB = person.ZJLB,
                        ZJLB = person.ZJLB,
                        ZJLB_ZWM = person.ZJLB_ZWM,
                        DH = person.DH,
                        QLRLX = "抵押人",
                        SXH = person.SXH,
                        IS_OWNER = person.IS_OWNER,
                        XID = xid
                    });
                }
                qlrglET.Add(new QLRGL_INFO()
                {
                    GLBM = Provider.Sql.Create().ToString(),
                    SLBH = newSLBH,
                    YWBM = newSLBH,
                    ZJHM = dyInfo.YHYTSHXYDM,
                    QLRID = dyInfo.YHYTSHXYDM2,
                    QLRMC = dyInfo.DYQRMC,
                    //ZJLB = "8",//"统一社会信用代码",
                    ZJLB = "统一社会信用代码",//"统一社会信用代码",
                    DH = dyInfo.DYLXRDH,
                    QLRLX = "抵押权人",
                    XID = xid
                });
                count = await _IDYServices.Mortgage(regInfo, flowInfo, tsglET, dyET, xgdjglET, qlrglET, attFileModel);
                return new MessageModel<int>()
                {
                    msg = "保存成功",
                    success = true,
                    response = count
                };
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(newSLBH))
                {
                    SysUtility.Delete(this._webHostEnvironment.WebRootPath, newSLBH);
                }
                return new MessageModel<int>()
                {
                    msg = "保存失败" + ex.Message,
                    success = false,
                    response = 0
                };
            }

        }
    }
}
