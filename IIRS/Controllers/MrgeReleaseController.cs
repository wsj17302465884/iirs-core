using IIRS.IRepository;
using IIRS.IServices;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
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
    public class MrgeReleaseController : ControllerBase
    {
        private readonly ILogger<MrgeReleaseController> _logger;
        readonly IMrgeReleaseServices _mrService;
        readonly IUserInfoRepository _userInfoRepository;
        readonly IWebHostEnvironment _webHostEnvironment;
        readonly IMortgageServices _mortgageServices;
        public MrgeReleaseController(ILogger<MrgeReleaseController> logger, IUserInfoRepository userInfoRepository, IMrgeReleaseServices mrgeReleaseService, IMortgageServices mortgageServices, IWebHostEnvironment webHostEnvironment)
        {
            this._logger = logger;
            this._mrService = mrgeReleaseService;
            this._userInfoRepository = userInfoRepository;
            this._mortgageServices = mortgageServices;
            this._webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public async Task<MessageModel<string>> RegistrationJsonQuery(string AUZ_ID)
        {
            string json = await _mrService.RegistrationJsonQuery(AUZ_ID);
            return new MessageModel<string>()
            {
                msg = "获取成功",
                success = true,
                response = json
            };
        }

        [HttpPost]
        public async Task<MessageModel<string>> SaveBankAuthorize([FromForm] string strModel)
        {
            try
            {
                DateTime saveTime = DateTime.Now;
                string newBid = Provider.Sql.Create().ToString();
                AuthorizedHouseVModel model = JsonConvert.DeserializeObject<AuthorizedHouseVModel>(HttpUtility.UrlDecode(strModel));
                BankAuthorize bankAuthorize = new BankAuthorize()
                {
                    BID = newBid,
                    DOCUMENTTYPE = model.zjlb,
                    DOCUMENTNUMBER = model.dyr_zjhm,
                    AUTHORIZATIONDATE = saveTime,
                    STATUS = SysFlowConst.FLOW_DYBGZX_DYHSL,
                    rightname = model.dyr,
                    BankCode = model.YHYTSHXYDM,
                    BankName = model.DYQRMC
                };
                List<OrderHouseAssociation> orderHouseAssociationsList = new List<OrderHouseAssociation>();

                foreach (var item in model.selectHouseInfo)
                {
                    orderHouseAssociationsList.Add(new OrderHouseAssociation
                    {
                        OID = Provider.Sql.Create().ToString(),
                        BID = bankAuthorize.BID,
                        CERTIFICATENUMBER = item.bdczh,
                        ACCEPTANCENUMBER = item.slbh,
                        NUMBERID = item.tstybm,
                        ADDRESS = item.zl,
                        rightname = item.qlrmc,
                        AUTHORIZATIONDATE = saveTime,
                        qllx = item.qllx,
                        qlxz = item.qlxz,
                        jzmj = item.jzmj,
                        tdqllx = item.tdqllx,
                        tdqlxz = item.tdqlxz,
                        tdmj = item.dytdmj,
                        bdclx = item.bdclx
                    });
                }
                IFLOW_DO_ACTION IflowDoAction = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = Convert.ToInt32(model.STATUS),
                    AUZ_ID = bankAuthorize.BID,
                    CDATE = DateTime.Now,
                    USER_NAME = model.SJR
                };
                newBid = await _mortgageServices.SaveReturnBid(bankAuthorize, orderHouseAssociationsList, IflowDoAction);
                return new MessageModel<string>()
                {
                    msg = "获取成功",
                    success = true,
                    response = newBid
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<string>()
                {
                    msg = "保存失败" + ex.Message,
                    success = false,
                    response = string.Empty
                };
            }
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
                    this._mrService.SaveUploadFileDB(uploadFile);
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
        public async Task<MessageModel<MrgeReleaseVModel>> GetMortgageInfo(string BDCZMH)
        {
            try
            {
                //List<string> bdczh = new List<string>();
                //bdczh.Add("FHID966602#00520501");
                //bdczh.Add("HDC-171213093610-84AV4W1YILR601PE868");
                MrgeReleaseVModel vmodel = await this._mrService.GetMortgageInfo(BDCZMH);
                return new MessageModel<MrgeReleaseVModel>()
                {
                    msg = "获取成功",
                    success = true,
                    response = vmodel
                };
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<MrgeReleaseVModel>()
                {
                    msg = $"错误，原因：{ex.Message}！",
                    success = false,
                    response = null
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.GetMortgageInfo:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<MrgeReleaseVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null
                };
            }
            
        }

        /// <summary>
        /// 初始化抵押上报文件格式信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<MediasVModel>> GetInitMedias()
        {
            var data = await this._mrService.GetInitMedias();
            return new MessageModel<MediasVModel>()
            {
                msg = "获取成功",
                success = true,
                response = data
            };
        }

        [HttpPost]
        public async Task<MessageModel<int>> Post([FromForm] string strMrgeReleaseVModel, [FromForm] string strFileTree)
        {
            try
            {
                int count = 0;
                DateTime saveTime = DateTime.Now;
                string saveDataJson = HttpUtility.UrlDecode(strMrgeReleaseVModel);
                MrgeReleaseVModel releaseInfo = JsonConvert.DeserializeObject<MrgeReleaseVModel>(saveDataJson);
                string newSLBH = releaseInfo.SLBH;//服务器端获取受理编号
                
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
                List<TSGL_INFO> tsglET = new List<TSGL_INFO>();
                List<XGDJGL_INFO> xgdjglET = new List<XGDJGL_INFO>();
                List<QLRGL_INFO> qlrglET = new List<QLRGL_INFO>();
                
                IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = SysFlowConst.FLOW_DYBGZX_DBDCZXDYSP,
                    AUZ_ID = releaseInfo.AUZ_ID,
                    CDATE = saveTime,
                    USER_NAME = releaseInfo.SJR
                };
                string bdczmh = string.Empty;//不动产证明号

                bdczmh = releaseInfo.selectHouse.BDCZH;
                xgdjglET.Add(new XGDJGL_INFO()
                {
                    BGBM = Provider.Sql.Create().ToString(),
                    ZSLBH = newSLBH,
                    FSLBH = releaseInfo.selectHouse.SLBH,
                    BGRQ = saveTime,
                    BGLX = "抵押注销",
                    XGZLX = releaseInfo.selectHouse.ZSLX,
                    XGZH = releaseInfo.selectHouse.BDCZH,
                    PID = "",
                    XID = xid
                });

                foreach (var sun in releaseInfo.selectHouse.children)
                {
                    tsglET.Add(new TSGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        BDCLX = sun.BDCLX,//房屋或土地
                        TSTYBM = sun.TSTYBM,
                        BDCDYH = sun.BDCDYH,
                        DJZL = "抵押注销",
                        CSSJ = saveTime,
                        XID = xid
                    });
                    xgdjglET.Add(new XGDJGL_INFO()
                    {
                        BGBM = Provider.Sql.Create().ToString(),
                        ZSLBH = newSLBH,
                        //FSLBH = sun.SLBH,
                        FSLBH = sun.SLBH,
                        BGRQ = saveTime,
                        BGLX = "抵押注销",
                        XGZLX = sun.ZSLX,
                        XGZH = sun.BDCZH,
                        PID = releaseInfo.selectHouse.SLBH,
                        XID = xid
                    });
                }

                XGDJZX_INFO zxInfo = new XGDJZX_INFO()
                {
                    SLBH = newSLBH,
                    XGZH = bdczmh,
                    BDCDYH = releaseInfo.selectHouse.BDCDYH,
                    DJLX = "注销登记",
                    DJYY = releaseInfo.ZXYY,
                    DLRXM = releaseInfo.DYZXLXR,
                    DLJGMC = releaseInfo.DYQRMC,
                    SPBZ = releaseInfo.BZ,
                    SQRQ = saveTime,
                    SJR = releaseInfo.SJR,
                    XID = xid
                };
                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    XID = xid,
                    YWSLBH = newSLBH,
                    SLBH = newSLBH,
                    DJZL = 1,
                    ZL = releaseInfo.ZL,
                    ORG_ID = releaseInfo.BankDeptID,
                    USER_ID = releaseInfo.DYQRMC_ID,
                    TEL = releaseInfo.DYZXLXRDH,
                    HTH = releaseInfo.HTH,
                    BDCZH = bdczmh,
                    QLRMC = string.Join(",", releaseInfo.selectPerson.Cast<DyPersonVModel>().OrderBy(S => S.SXH).Select(S => S.QLRMC).ToArray()),
                    REMARK2 = "抵押注销",
                    AUZ_ID = releaseInfo.AUZ_ID,
                    IS_ACTION_OK = 0,
                    SaveDataJson = saveDataJson
                };
                foreach (var person in releaseInfo.selectPerson)
                {
                    qlrglET.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        YWBM = newSLBH,
                        ZJHM= person.ZJHM,
                        QLRID = person.QLRID,
                        QLRMC= person.QLRMC,
                        ZJLB = person.ZJLB,
                        ZJLB_ZWM = person.ZJLB_ZWM,
                        DH = person.DH,
                        QLRLX = "抵押人",
                        SXH= person.SXH,
                        XID = xid
                    });
                }
                qlrglET.Add(new QLRGL_INFO()
                {
                    GLBM = Provider.Sql.Create().ToString(),
                    SLBH = newSLBH,
                    YWBM = newSLBH,
                    ZJHM = releaseInfo.YHYTSHXYDM,
                    QLRID = releaseInfo.YHYTSHXYDM2,
                    QLRMC = releaseInfo.DYQRMC,
                    ZJLB = "8",
                    ZJLB_ZWM = "统一社会信用代码",
                    DH = releaseInfo.DYZXLXRDH,
                    QLRLX = "抵押权人",
                    XID = xid
                });
                count = await _mrService.MortgageRelease(regInfo, zxInfo, flowInfo, tsglET, xgdjglET, qlrglET, attFileModel);
                return new MessageModel<int>()
                {
                    msg = "获取成功",
                    success = true,
                    response = count
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<int>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = 0
                };
            }

        }

        [HttpPost]
        public async Task<MessageModel<int>> Edit([FromForm] string strMrgeReleaseVModel, [FromForm] string strFileTree, [FromForm] string delXID)
        {
            try
            {
                int count = 0;
                DateTime saveTime = DateTime.Now;
                MrgeReleaseVModel releaseInfo = JsonConvert.DeserializeObject<MrgeReleaseVModel>(HttpUtility.UrlDecode(strMrgeReleaseVModel));
                string newSLBH = releaseInfo.SLBH;//服务器端获取受理编号
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
                List<TSGL_INFO> tsglET = new List<TSGL_INFO>();
                List<XGDJGL_INFO> xgdjglET = new List<XGDJGL_INFO>();
                List<QLRGL_INFO> qlrglET = new List<QLRGL_INFO>();

                IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = SysFlowConst.FLOW_DYBGZX_DBDCZXDYSP,
                    AUZ_ID = releaseInfo.AUZ_ID,
                    CDATE = saveTime,
                    USER_NAME = releaseInfo.SJR
                };
                string bdczmh = string.Empty;//不动产证明号

                bdczmh = releaseInfo.selectHouse.BDCZH;
                xgdjglET.Add(new XGDJGL_INFO()
                {
                    BGBM = Provider.Sql.Create().ToString(),
                    ZSLBH = newSLBH,
                    FSLBH = releaseInfo.selectHouse.SLBH,
                    BGRQ = saveTime,
                    BGLX = "抵押注销",
                    XGZLX = releaseInfo.selectHouse.ZSLX,
                    XGZH = releaseInfo.selectHouse.BDCZH,
                    PID = "",
                    XID = xid
                });

                foreach (var sun in releaseInfo.selectHouse.children)
                {
                    tsglET.Add(new TSGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        BDCLX = sun.BDCLX,//房屋或土地
                        TSTYBM = sun.TSTYBM,
                        BDCDYH = sun.BDCDYH,
                        DJZL = "抵押注销",
                        CSSJ = saveTime,
                        XID = xid
                    });
                    xgdjglET.Add(new XGDJGL_INFO()
                    {
                        BGBM = Provider.Sql.Create().ToString(),
                        ZSLBH = newSLBH,
                        //FSLBH = sun.SLBH,
                        FSLBH = sun.SLBH,
                        BGRQ = saveTime,
                        BGLX = "抵押注销",
                        XGZLX = sun.ZSLX,
                        XGZH = sun.BDCZH,
                        PID = releaseInfo.selectHouse.SLBH,
                        XID = xid
                    });
                }

                XGDJZX_INFO zxInfo = new XGDJZX_INFO()
                {
                    SLBH = newSLBH,
                    XGZH = bdczmh,
                    BDCDYH = releaseInfo.selectHouse.BDCDYH,
                    DJLX = "注销登记",
                    DJYY = releaseInfo.ZXYY,
                    DLRXM = releaseInfo.DYZXLXR,
                    DLJGMC = releaseInfo.DYQRMC,
                    SPBZ = releaseInfo.BZ,
                    SQRQ = saveTime,
                    SJR = releaseInfo.SJR,
                    XID = xid
                };
                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    XID = xid,
                    YWSLBH = newSLBH,
                    SLBH = newSLBH,
                    DJZL = 1,
                    ZL = releaseInfo.ZL,
                    ORG_ID = releaseInfo.BankDeptID,
                    USER_ID = releaseInfo.DYQRMC_ID,
                    TEL = releaseInfo.DYZXLXRDH,
                    HTH = releaseInfo.HTH,
                    BDCZH = bdczmh,
                    QLRMC = string.Join(",", releaseInfo.selectPerson.Cast<DyPersonVModel>().OrderBy(S => S.SXH).Select(S => S.QLRMC).ToArray()),
                    REMARK2 = "抵押注销",
                    AUZ_ID = releaseInfo.AUZ_ID,
                    IS_ACTION_OK = 0
                };
                foreach (var person in releaseInfo.selectPerson)
                {
                    qlrglET.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = newSLBH,
                        YWBM = newSLBH,
                        ZJHM = person.ZJHM,
                        QLRID = person.QLRID,
                        QLRMC = person.QLRMC,
                        ZJLB = person.ZJLB,
                        ZJLB_ZWM = person.ZJLB_ZWM,
                        DH = person.DH,
                        QLRLX = "抵押人",
                        SXH = person.SXH,
                        XID = xid
                    });
                }
                qlrglET.Add(new QLRGL_INFO()
                {
                    GLBM = Provider.Sql.Create().ToString(),
                    SLBH = newSLBH,
                    YWBM = newSLBH,
                    ZJHM = releaseInfo.YHYTSHXYDM,
                    QLRID = releaseInfo.YHYTSHXYDM2,
                    QLRMC = releaseInfo.DYQRMC,
                    ZJLB = "8",//"统一社会信用代码",
                    ZJLB_ZWM = "统一社会信用代码",
                    DH = releaseInfo.DYZXLXRDH,
                    QLRLX = "抵押权人",
                    XID = xid
                });
                count = await _mrService.MortgageRelease(regInfo, zxInfo, flowInfo, tsglET, xgdjglET, qlrglET, attFileModel);
                return new MessageModel<int>()
                {
                    msg = "获取成功",
                    success = true,
                    response = count
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<int>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = 0
                };
            }
        }
    }
}
