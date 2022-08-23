using Castle.Core.Internal;
using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
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
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using static IIRS.Models.ViewModel.IIRS.ParcelVModel;

namespace IIRS.Controllers
{
    /// <summary>
    /// 房屋相关信息
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    public class CertificationController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        private readonly ILogger<CertificationController> _logger;

        private readonly ICertificationRepository _certificationRepository;

        private readonly IHouseBdczhRepository _houseBdczhRepository;

        private readonly ITsglRepository _tsglRepository;

        private readonly IHouseStatusRepository _houseStatusRepository;

        private readonly IDYRepository _dYRepository;

        private readonly IOrderHouseAssociationRepository _orderHouseAssociationRepository;

        private readonly IConstructionServices _constructionServices;

        readonly IWebHostEnvironment _webHostEnvironment;

        private readonly IAuthorizationServices _authorizationServices;

        public CertificationController(IDBTransManagement dbTransManagement, ILogger<CertificationController> logger, ICertificationRepository certificationRepository, IHouseBdczhRepository houseBdczhRepository, ITsglRepository tsglRepository, IHouseStatusRepository houseStatusRepository, IDYRepository dYRepository, IOrderHouseAssociationRepository orderHouseAssociationRepository, IConstructionServices constructionServices, IWebHostEnvironment webHostEnvironment, IAuthorizationServices authorizationServices)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _certificationRepository = certificationRepository;
            _houseBdczhRepository = houseBdczhRepository;
            _tsglRepository = tsglRepository;
            _houseStatusRepository = houseStatusRepository;
            _dYRepository = dYRepository;
            _orderHouseAssociationRepository = orderHouseAssociationRepository;
            _constructionServices = constructionServices;
            _authorizationServices = authorizationServices;
            this._webHostEnvironment = webHostEnvironment;
            _logger = logger;
        }

        /// <summary>
        /// 查询房子状态
        /// </summary>
        /// <param name="bdczmh"></param>
        /// <param name="dyqr"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<CertificationVModel>>> GetCertificationList(string bdczmh, string dyqr)
        {
            try
            {
                var data = await _certificationRepository.GetCertificationList(bdczmh, dyqr);
                return new MessageModel<List<CertificationVModel>>()
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
                return new MessageModel<List<CertificationVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 根据证明号和抵押人查询抵押房屋
        /// </summary>
        /// <param name="bdczmh"></param>
        /// <param name="dyr"></param>
        /// <param name="dyqr"></param>
        /// <param name="intPageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<CertificationVModel>>> GetCertificationListToPage(int intPageIndex, string bdczmh, string dyr,string dyqr)
        {
            string slbh = "";
            string tstybm = "";
            string CfBdczhMessage = "";
            string bdczhMessage = "";
            string btnable = "";
            try
            {
                var data = await _certificationRepository.GetCertificationListToPage(intPageIndex, bdczmh, dyr,dyqr);
                CertificationVModel model = new CertificationVModel();
                List<CertificationVModel> models = new List<CertificationVModel>();
                for (int i = 0; i < data.data.Count; i++)
                {
                    model = new CertificationVModel();
                    model.xh = i + 1;
                    model.bdczmh = data.data[i].bdczmh;
                    model.bdczh = data.data[i].bdczh;
                    model.zl = data.data[i].zl;
                    model.qljssj = data.data[i].qljssj;
                    model.dymj = data.data[i].dymj;
                    model.Dyr = data.data[i].Dyr;
                    model.Dyr_Zjlb = data.data[i].Dyr_Zjlb;
                    model.Dyr_Zjhm = data.data[i].Dyr_Zjhm;
                    model.Dyqr = data.data[i].Dyqr;
                    model.Dyqr_Zjlb = data.data[i].Dyqr_Zjlb;
                    model.Dyqr_Zjhm = data.data[i].Dyqr_Zjhm;
                    model.slbh = data.data[i].slbh;
                    model.dyfs = data.data[i].dyfs;
                    slbh = data.data[i].slbh;
                    //foreach (var item in result)
                    //{
                    //    if(model.bdczh == item.Bdczh)
                    //    {
                    //        model.qllx = item.Qllx;
                    //        model.qlxz = item.Qlxz;
                    //        model.tdqllx = item.Tdqllx;
                    //        model.tdqlxz = item.Tdqlxz;
                    //        model.jzmj = item.Jzmj;
                    //        model.gytdmj = item.Gytdmj;
                    //        model.dytdmj = item.Dytdmj;
                    //    }
                    //}
                    if(data.data[i].bdczh != null)
                    {
                        if (!data.data[i].bdczh.Contains("共"))
                        {
                            model.hasChildren = false;
                        }
                    }
                    
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

                if(data.data.Count > 0)
                {
                    var tstybmData = await _tsglRepository.GetTstybmBySlbh(slbh);

                    if (tstybmData.Count > 0)
                    {
                        foreach (var Tstybmitem in tstybmData)
                        {
                            tstybm += Tstybmitem.TSTYBM + ",";
                        }
                        tstybm = tstybm.Substring(0, tstybm.Length - 1);

                        if(tstybm != "")
                        {
                            var HouseData = await _houseStatusRepository.GetHouseSatausList(tstybm);
                            if(HouseData.response.Count > 0)
                            {
                                foreach (var Houseitem in HouseData.response)
                                {
                                    if(Houseitem.Bdclx == null || Houseitem.Bdclx == "")
                                    {
                                        bdczhMessage += Houseitem.Bdczh + ",";
                                    }
                                    if(Houseitem.Djzl.IndexOf("查封") > 0)
                                    {
                                        CfBdczhMessage += Houseitem.Bdczh + ",";
                                    }
                                }
                                if(bdczhMessage != "")
                                {
                                    bdczhMessage = bdczhMessage.Substring(0, bdczhMessage.Length - 1);
                                    bdczhMessage = CfBdczhMessage + "的不动产类型为空，无法办理，请前往不动产登记中心进行办理！";
                                    btnable = "disabled";
                                }
                                if(CfBdczhMessage != "")
                                {
                                    CfBdczhMessage = CfBdczhMessage.Substring(0, CfBdczhMessage.Length - 1);
                                    CfBdczhMessage = CfBdczhMessage + "的房子存在查封，无法办理，请前往不动产登记中心进行办理！";
                                    btnable = "disabled";
                                }

                            }
                        }
                    }
                    data.data[0].bdczhMessage = bdczhMessage;
                    data.data[0].CfBdczhMessage = CfBdczhMessage;
                    data.data[0].btnable = btnable;
                }

                if (data.data.Count > 0)
                {
                    return new MessageModel<PageModel<CertificationVModel>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<PageModel<CertificationVModel>>()
                    {
                        msg = "未获取到数据，请查看查询条件",
                        success = false,
                        response = data
                    };
                }
                
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PageModel<CertificationVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 获取个人抵押信息
        /// </summary>
        /// <param name="bdczmh"></param>
        /// <param name="zjhm"></param>
        /// <param name="dyqr"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<CertificationVModel>>> GetCertificationInfo(string bdczmh, string zjhm, string dyqr)
        {
            string slbh = "";
            string tstybm = "";
            string CfBdczhMessage = "";
            string bdczhMessage = "";
            string btnable = "";
            try
            {
                var data = await _certificationRepository.GetCertificationInfo(bdczmh, zjhm,dyqr);
                CertificationVModel model = new CertificationVModel();
                List<CertificationVModel> models = new List<CertificationVModel>();
                for (int i = 0; i < data.Count; i++)
                {
                    model = new CertificationVModel();
                    model.xh = i + 1;
                    model.bdczmh = data[i].bdczmh;
                    model.bdczh = data[i].bdczh;
                    model.zl = data[i].zl;
                    model.qljssj = data[i].qljssj;
                    model.dymj = data[i].dymj;
                    model.Dyr = data[i].Dyr;
                    model.Dyr_Zjlb = data[i].Dyr_Zjlb;
                    model.Dyr_Zjhm = data[i].Dyr_Zjhm;
                    model.Dyqr = data[i].Dyqr;
                    model.Dyqr_Zjlb = data[i].Dyqr_Zjlb;
                    model.Dyqr_Zjhm = data[i].Dyqr_Zjhm;
                    model.slbh = data[i].slbh;
                    slbh = data[i].slbh;
                    if (!data[i].bdczh.Contains("共"))
                    {
                        model.hasChildren = false;
                    }
                    models.Add(model);
                }

                if (models.Count > 0)
                {
                    var tstybmData = await _tsglRepository.GetTstybmBySlbh(slbh);

                    if (tstybmData.Count > 0)
                    {
                        foreach (var Tstybmitem in tstybmData)
                        {
                            tstybm += Tstybmitem.TSTYBM + ",";
                        }
                        tstybm = tstybm.Substring(0, tstybm.Length - 1);

                        if (tstybm != "")
                        {
                            var HouseData = await _houseStatusRepository.GetHouseSatausList(tstybm);
                            if (HouseData.response.Count > 0)
                            {
                                foreach (var Houseitem in HouseData.response)
                                {
                                    if (Houseitem.Bdclx == null || Houseitem.Bdclx == "")
                                    {
                                        bdczhMessage += Houseitem.Bdczh + ",";
                                    }
                                    if (Houseitem.Djzl.IndexOf("查封") > 0)
                                    {
                                        CfBdczhMessage += Houseitem.Bdczh + ",";
                                    }
                                }
                                if (bdczhMessage != "")
                                {
                                    bdczhMessage = bdczhMessage.Substring(0, bdczhMessage.Length - 1);
                                    bdczhMessage = CfBdczhMessage + "的不动产类型为空，无法办理，请前往不动产登记中心进行办理！";
                                    btnable = "disabled";
                                }
                                if (CfBdczhMessage != "")
                                {
                                    CfBdczhMessage = CfBdczhMessage.Substring(0, CfBdczhMessage.Length - 1);
                                    CfBdczhMessage = CfBdczhMessage + "的房子存在查封，无法办理，请前往不动产登记中心进行办理！";
                                    btnable = "disabled";
                                }

                            }
                        }
                    }
                }

                models[0].bdczhMessage = bdczhMessage;
                models[0].CfBdczhMessage = CfBdczhMessage;
                models[0].btnable = btnable;

                if (models.Count>0)
                {
                    return new MessageModel<List<CertificationVModel>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<CertificationVModel>>()
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
                return new MessageModel<List<CertificationVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 获取该证明号下的相关证号
        /// </summary>
        /// <param name="bdczmh"></param>
        /// <param name="xh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<HouseXgzhVMode>>> GetBdczhByBdczmh(string bdczmh,int xh)
        {
            try
            {
                var data = await _houseBdczhRepository.GetBdczhByBdczmh(bdczmh,xh);
                if(data.Count > 0)
                {
                    return new MessageModel<List<HouseXgzhVMode>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<HouseXgzhVMode>>()
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
                return new MessageModel<List<HouseXgzhVMode>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 获取企业抵押变更下的房屋TSTYBM
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<DJ_TSGL>>> GetTstybmBySlbh(string slbh)
        {
            try
            {
                var data = await _tsglRepository.GetTstybmBySlbh(slbh);
                if (data.Count > 0)
                {
                    return new MessageModel<List<DJ_TSGL>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<DJ_TSGL>>()
                    {
                        msg = "未获取到数据，请查看查询条件",
                        success = false,
                        response = data
                    };
                }

            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<DJ_TSGL>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 获取抵押变更的房屋信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<HouseStatusViewModel>>> GetMortgageChangeHouseList(string slbh)
        {
            string tstybm = "";
            try
            {
                var rtn = new MessageModel<List<HouseStatusViewModel>>();
                var data = await _tsglRepository.GetTstybmBySlbh(slbh);
                if(data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        tstybm += item.TSTYBM + ",";
                    }
                    tstybm = tstybm.Substring(0, tstybm.Length - 1);

                    if(tstybm != "")
                    {
                        var HouseData = await _houseStatusRepository.GetHouseSatausList_New(tstybm);
                        if(HouseData.Count > 0)
                        {
                            rtn.msg = "获取成功";
                            rtn.success = true;
                            rtn.response = HouseData;
                        }
                        else
                        {
                            rtn.msg = "未获取到数据";
                            rtn.success = false;
                            rtn.response = HouseData;
                        }
                    }
                }
                return rtn;
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
        /// <summary>
        /// 根据SLBH获取抵押房屋
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<HouseStatusViewModel>>> GetDyHouseList(string slbh)
        {
            string tstybm = "";
            var rtn = new MessageModel<List<HouseStatusViewModel>>();
            try
            {
                var tstybmData = await _tsglRepository.GetTstybmBySlbh(slbh);
                if (tstybmData.Count > 0)
                {
                    foreach (var Tstybmitem in tstybmData)
                    {
                        tstybm += Tstybmitem.TSTYBM + ",";
                    }
                    tstybm = tstybm.Substring(0, tstybm.Length - 1);

                    if (tstybm != "")
                    {
                        var HouseData = await _houseStatusRepository.GetHouseSatausList_New(tstybm);
                        if (HouseData.Count > 0)
                        {
                            rtn.msg = "获取成功";
                            rtn.success = true;
                            rtn.response = HouseData;
                        }
                        else
                        {
                            rtn.msg = "未获取到数据";
                            rtn.success = false;
                            rtn.response = HouseData;
                        }
                    }

                    
                }
                return rtn;
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
        /// <summary>
        /// 获取抵押信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<DJ_DY>> GetDyInfoMessage(string slbh)
        {
            var rtn = new MessageModel<DJ_DY>();
            try
            {
                var dataDy = await _dYRepository.GetDyInfo(slbh);
                if(dataDy.Count > 0)
                {
                    rtn.msg = "获取成功";
                    rtn.success = true;
                    rtn.response = dataDy[0];
                }
                else
                {
                    rtn.msg = "未获取到数据";
                    rtn.success = false;
                    rtn.response = dataDy[0];
                }
                return rtn;
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<DJ_DY>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 获取授权ID
        /// </summary>
        /// <param name="bdczh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<OrderHouseAssociation>> GetBidByBdczh(string bdczh)
        {
            var rtn = new MessageModel<OrderHouseAssociation>();
            try
            {
                var BidData = await _orderHouseAssociationRepository.GetBIdByBdczh(bdczh);
                if(BidData.Count > 0)
                {
                    rtn.msg = "获取成功";
                    rtn.success = true;
                    rtn.response = BidData[0];
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
                return new MessageModel<OrderHouseAssociation>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 删除授权房屋
        /// </summary>
        /// <param name="bid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<int>> delete(string bid)
        {
            int count = await _orderHouseAssociationRepository.DeleteOrderHouseAssociation(bid);
            if(count > 0)
            {
                return new MessageModel<int>()
                {
                    msg = "删除成功",
                    success = true,
                    response = count
                };
            }
            else
            {
                return new MessageModel<int>()
                {
                    msg = "删除失败",
                    success = false,
                    response = count
                };
            }
            
        }

        /// <summary>
        /// 抵押变更提交数据
        /// </summary>
        /// <param name="strDYVModel"></param>
        /// <param name="strFileTree"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<int>> Post([FromForm] string strDYVModel, [FromForm] string strFileTree)
        {
            string newSLBH= "";
            try
            {
                int count = 0;
                string xgzlx = "";
                DateTime saveTime = DateTime.Now;
                ParcelVModel ParcelInfo = JsonConvert.DeserializeObject<ParcelVModel>(HttpUtility.UrlDecode(strDYVModel));
                //newSLBH = _constructionServices.GetSLBH();//服务器端获取受理编号
                List<TSGL_INFO> tsglET = new List<TSGL_INFO>();
                List<OrderHouseAssociation> ationList = new List<OrderHouseAssociation>();
                List<XGDJGL_INFO> xgdjglET = new List<XGDJGL_INFO>();
                List<QLRGL_INFO> qlrglET = new List<QLRGL_INFO>();

                //List<Base64FilesVModel> fileTree = JsonConvert.DeserializeObject<List<Base64FilesVModel>>(strFileTree);
                //List<PUB_ATT_FILE> attFileModel = SysUtility.UploadSysBase64File(this._webHostEnvironment.WebRootPath, newSLBH, fileTree);

                List<Base64FilesVModel> fileTree = JsonConvert.DeserializeObject<List<Base64FilesVModel>>(strFileTree);
                List<PUB_ATT_FILE> attFileModel = null;
                int imgCount = fileTree.Where(S => S.IMG.Length > 0).Count();
                if (imgCount > 0)
                {
                    attFileModel = SysUtility.UploadSysBase64File(this._webHostEnvironment.WebRootPath, newSLBH, fileTree);
                }
                ParcelInfo.CNSJ = saveTime.AddDays(3);
                if(ParcelInfo.Bdclx)
                {
                    xgzlx = "房屋抵押证明";
                }
                else
                {
                    xgzlx = "土地抵押证明";
                }
                DY_INFO dyET = new DY_INFO()
                {
                    SLBH = ParcelInfo.SLBH,
                    YWSLBH = ParcelInfo.SLBH,
                    DJLX = "抵押登记",      //抵押登记
                    DJYY = ParcelInfo.DJYY,
                    XGZH = ParcelInfo.selectHouseInfo[0].BDCZH,
                    ZLXX = ParcelInfo.ZL,   //户坐落.....等                    
                    SQRQ = saveTime,
                    DYLX = "抵押变更",     //抵押变更
                    DYSW = ParcelInfo.DYSW,
                    DYFS = ParcelInfo.DYFS,
                    DYMJ = ParcelInfo.dyMJ,   //多个户的面积和   
                    BDBZZQSE = ParcelInfo.BDBZQSE,
                    PGJE = ParcelInfo.PGJE,
                    HTH = ParcelInfo.HTH,
                    LXR = ParcelInfo.DYLXR,
                    LXRDH = ParcelInfo.DYLXRDH,
                    BDCDYH = ParcelInfo.bdcdyh,

                    CNSJ = ParcelInfo.CNSJ,
                    FJ = ParcelInfo.BZ,
                    ZWR = string.Concat(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(p => p.QLRMC).ToArray()),
                    ZWRZJH = string.Concat(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(p => p.ZJHM).ToArray()),
                    ZWRZJLX = string.Concat(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(p => p.ZJLB).Distinct().ToArray()),//改成中文类型
                    DLJGMC = ParcelInfo.DYQRMC,
                    QLQSSJ = ParcelInfo.ZWLXQXQSRQ.Date,
                    QLJSSJ = ParcelInfo.ZWLXQXJZRQ.Date,
                    DYQX = ParcelInfo.LXQX.ToString()
                };
                IFLOW_DO_ACTION flowInfo = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    FLOW_ID = 22,
                    AUZ_ID = ParcelInfo.AUZ_ID,
                    CDATE = saveTime,
                    USER_NAME = ParcelInfo.DYLXR
                };
                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    XID = Provider.Sql.Create().ToString(),
                    YWSLBH = ParcelInfo.SLBH,
                    DJZL = 3,   //登记类型（抵押变更）
                    BDCZH = ParcelInfo.selectHouseInfo[0].BDCZH,         
                    SLBH = ParcelInfo.selectHouseInfo[0].SLBH,
                    QLRMC = string.Join(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(s => s.QLRMC).ToArray()),
                    ZL = dyET.ZLXX,         //户坐落加等
                    ORG_ID = ParcelInfo.BankDeptID,
                    USER_ID = ParcelInfo.DYQRMC_ID,
                    TEL = ParcelInfo.DYLXRDH,
                    AUZ_ID = ParcelInfo.AUZ_ID,
                    HTH = ParcelInfo.HTH,
                    REMARK2 = ParcelInfo.DYLX
                };

                IFLOW_DO_ACTION flow = new IFLOW_DO_ACTION()
                {
                    PK = Provider.Sql.Create().ToString(),
                    AUZ_ID = ParcelInfo.AUZ_ID
                };

                foreach (var hourse in ParcelInfo.selectHouseInfo)
                {
                    //需要插入房子的相关信息
                    tsglET.Add(new TSGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = ParcelInfo.SLBH,
                        BDCLX = "房屋",
                        TSTYBM = hourse.TSTYBM,
                        BDCDYH = hourse.BDCDYH,
                        DJZL = "抵押",
                        CSSJ = saveTime
                    });
                    
                    //插入授权订单相关联的房屋信息
                    ationList.Add(new OrderHouseAssociation()
                    {
                        OID= Provider.Sql.Create().ToString(),
                        BID = ParcelInfo.AUZ_ID,
                        CERTIFICATENUMBER = hourse.BDCZH,
                        ACCEPTANCENUMBER = hourse.SLBH,
                        NUMBERID = hourse.TSTYBM,
                        ADDRESS = hourse.ZL,
                        rightname = hourse.QLRMC,
                        AUTHORIZATIONDATE = saveTime,
                        qllx = hourse.qllx,
                        qlxz = hourse.qlxz,
                        jzmj = hourse.jzmj,
                        tdqllx = hourse.tdqllx,
                        tdqlxz = hourse.tdqlxz,
                        tdmj = hourse.tdmj,
                        bdclx = hourse.ZSLX,
                        bdcdyh = hourse.bdcdyh
                    });
                }

                xgdjglET.Add(new XGDJGL_INFO()
                {
                    BGBM = Provider.Sql.Create().ToString(),
                    ZSLBH = ParcelInfo.SLBH,
                    FSLBH = ParcelInfo.DYSLBH,
                    BGRQ = saveTime,
                    BGLX = "抵押变更",
                    XGZLX = xgzlx,
                    XGZH = ParcelInfo.Bdczmh
                });

                foreach (var item in ParcelInfo.selectHouseInfo)
                {
                    xgdjglET.Add(new XGDJGL_INFO()
                    {
                        BGBM = Provider.Sql.Create().ToString(),
                        ZSLBH = ParcelInfo.SLBH,
                        FSLBH = item.SLBH,    //土地受理编号
                        BGRQ = saveTime,
                        BGLX = "抵押",            //在建工程抵押
                        XGZLX = item.ZSLX,       
                        XGZH = item.BDCZH         //土地的相关证号
                    });
                }

                foreach (var person in ParcelInfo.selectDyPerson)
                {
                    qlrglET.Add(new QLRGL_INFO()
                    {
                        GLBM = Provider.Sql.Create().ToString(),
                        SLBH = ParcelInfo.SLBH,
                        YWBM = ParcelInfo.SLBH,
                        ZJHM = person.ZJHM,
                        QLRID = person.QLRID,
                        QLRMC = person.QLRMC,
                        ZJLB = person.ZJLB_ZWM,
                        DH = person.DH,
                        QLRLX = "抵押人",
                        //SXH = person.SXH
                    });
                }
                qlrglET.Add(new QLRGL_INFO()
                {
                    GLBM = Provider.Sql.Create().ToString(),
                    SLBH = ParcelInfo.SLBH,
                    YWBM = ParcelInfo.SLBH,
                    ZJHM = ParcelInfo.YHYTSHXYDM,
                    QLRID = ParcelInfo.YHYTSHXYDM2,
                    QLRMC = ParcelInfo.DYQRMC,
                    ZJLB = "统一社会信用代码",//"统一社会信用代码",
                    DH = ParcelInfo.DYLXRDH,
                    QLRLX = "抵押权人"
                });
                count = await _constructionServices.Certification(regInfo, flowInfo, tsglET, ationList, dyET, xgdjglET, qlrglET, attFileModel);
                return new MessageModel<int>()
                {
                    msg = "获取成功",
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

    }
}
