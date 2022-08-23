using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
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
    /// 在建工程抵押土地相关信息
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    public class ConstructionController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        private readonly ILogger<ConstructionController> _logger;

        private readonly IConstructionRepository _constructionRepository;

        private readonly IConstructionServices _constructionServices;

        private readonly IZdQsdcRepository _zdQsdcRepository;

        private readonly IFc_h_QsdcRepository _fc_H_QsdcRepository;

        private readonly IConstructionChangeRepository _constructionChangeRepository;

        

        public ConstructionController(IDBTransManagement dbTransManagement, ILogger<ConstructionController> logger, IConstructionRepository constructionRepository, IConstructionServices constructionServices, IZdQsdcRepository zdQsdcRepository, IFc_h_QsdcRepository fc_H_QsdcRepository, IConstructionChangeRepository constructionChangeRepository)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _constructionRepository = constructionRepository;
            _constructionServices = constructionServices;
            _zdQsdcRepository = zdQsdcRepository;
            _fc_H_QsdcRepository = fc_H_QsdcRepository;
            _constructionChangeRepository = constructionChangeRepository;
            _logger = logger;
        }
        /// <summary>
        /// 查询在建工程抵押土地信息
        /// </summary>
        /// <param name="zdtybm"></param>
        /// <param name="IsNewSlbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<ConstructionVModel>>> GetConstructionList(string zdtybm, bool IsNewSlbh)
        {
            List<ConstructionVModel> models = new List<ConstructionVModel>();
            ConstructionVModel model;
            try
            {
                var data = await _constructionRepository.GetConstructionList(zdtybm);
                string NewSlbh = _constructionServices.GetSLBH();

                if(data.Count > 0 && IsNewSlbh)
                {

                    foreach (var item in data)
                    {
                        model = new ConstructionVModel();
                        model.bdczh = item.bdczh;
                        model.fzmj = item.fzmj;
                        model.NewSlbh = NewSlbh;
                        model.qlrmc = item.qlrmc;
                        model.slbh = item.slbh;
                        model.tdzl = item.tdzl;
                        model.tstybm = item.tstybm;
                        model.zdtybm = item.zdtybm;
                        model.bdcdyh = item.bdcdyh;
                        models.Add(model);
                    }
                    return new MessageModel<List<ConstructionVModel>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = models
                    };
                }

                return new MessageModel<List<ConstructionVModel>>()
                {
                    msg = "获取成功",
                    success = true,
                    response = models
                };

            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<ConstructionVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 获取抵押人信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<DJ_QLR>>> GetDyrList(string slbh)
        {
            try
            {
                var data = await _constructionServices.GetDyrInfo(slbh);
                return new MessageModel<List<DJ_QLR>>()
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
                return new MessageModel<List<DJ_QLR>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 获取幢编号
        /// </summary>
        /// <param name="zdtybm"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<FC_Z_QSDC>>> GetFwbhList(string zdtybm)
        {
            try
            {
                var data = await _constructionServices.GetFwbhList(zdtybm);
                return new MessageModel<List<FC_Z_QSDC>>()
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
                return new MessageModel<List<FC_Z_QSDC>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 获取宗地相关信息
        /// </summary>
        /// <param name="zdtybm"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<ZD_QSDC>>> GetZdInfo(string zdtybm)
        {
            try
            {
                var data = await _zdQsdcRepository.GetZdInfo(zdtybm);
                return new MessageModel<List<ZD_QSDC>>()
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
                return new MessageModel<List<ZD_QSDC>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 获取户相关信息
        /// </summary>
        /// <param name="lsfwbh"></param>
        /// <param name="intPageIndex"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<FC_H_QSDC>>> GetFCHList(string lsfwbh, int intPageIndex)
        {
            try
            {
                var data = await _fc_H_QsdcRepository.GetFCHList(lsfwbh,intPageIndex);
                return new MessageModel<PageModel<FC_H_QSDC>>()
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
                return new MessageModel<PageModel<FC_H_QSDC>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 提交数据
        /// </summary>
        /// <param name="strDYVModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<int>> Post([FromForm] string strDYVModel)
        {
            try
            {
                int count = 0;
                DateTime saveTime = DateTime.Now;
                ParcelVModel ParcelInfo = JsonConvert.DeserializeObject<ParcelVModel>(HttpUtility.UrlDecode(strDYVModel));
                string newSLBH = _constructionServices.GetSLBH();//服务器端获取受理编号
                List<TSGL_INFO> tsglET = new List<TSGL_INFO>();
                List<XGDJGL_INFO> xgdjglET = new List<XGDJGL_INFO>();
                List<QLRGL_INFO> qlrglET = new List<QLRGL_INFO>();
                ParcelInfo.CNSJ = saveTime.AddDays(3);
                DY_INFO dyET = new DY_INFO()
                {                    
                    SLBH = Provider.Sql.Create().ToString(),
                    YWSLBH = newSLBH,
                    DJLX = "抵押登记",      //抵押登记
                    DJYY = ParcelInfo.DJYY,
                    XGZH = ParcelInfo.selectZdMessage[0].BDCZH,   //土地相关证号
                    ZLXX = ParcelInfo.ZL,   //户坐落.....等
                    //.selectHouseInfo[0].ZL + (ParcelInfo.selectHouseInfo.Count > 1 ? "等" + ParcelInfo.selectHouseInfo.Count + "个" : string.Empty),
                    SQRQ = saveTime,
                    DYLX = "在建工程抵押",     //在建工程抵押
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
                    //SJR = ParcelInfo.SJR,
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
                    FLOW_ID = 6,
                    AUZ_ID = ParcelInfo.AUZ_ID,
                    CDATE = saveTime,
                    USER_NAME = ParcelInfo.DYLXR                    
                };
                REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
                {
                    XID = Provider.Sql.Create().ToString(),
                    YWSLBH = newSLBH,
                    DJZL = 1,
                    BDCZH = ParcelInfo.selectZdMessage[0].BDCZH,          //土地证号
                    //SLBH = ParcelInfo.selectHouseInfo[0].SLBH + (ParcelInfo.selectHouseInfo.Count > 1 ? "等" + ParcelInfo.selectHouseInfo.Count + "个" : string.Empty),
                    SLBH = ParcelInfo.selectZdMessage[0].SLBH,
                    QLRMC = string.Join(",", ParcelInfo.selectDyPerson.Cast<DyrVModel>().Select(s => s.QLRMC).ToArray()),
                    //ParcelInfo.selectDyPerson[0].QLRMC
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
                        SLBH = newSLBH,
                        BDCLX = "房屋",
                        TSTYBM = hourse.TSTYBM,
                        BDCDYH = hourse.BDCDYH,
                        DJZL = "抵押",
                        CSSJ = saveTime
                    });
                }

                foreach (var item in ParcelInfo.selectZdMessage)
                {
                    xgdjglET.Add(new XGDJGL_INFO()
                    {
                        BGBM = Provider.Sql.Create().ToString(),
                        ZSLBH = newSLBH,
                        FSLBH = item.SLBH,    //土地受理编号
                        BGRQ = saveTime,
                        BGLX = "在建工程抵押",            //在建工程抵押
                        XGZLX = "土地证",       //土地证
                        XGZH = item.BDCZH         //土地的相关证号
                    });
                }

                foreach (var person in ParcelInfo.selectDyPerson)
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
                        DH = person.DH,
                        QLRLX = "抵押人",
                        //SXH = person.SXH
                    });
                }
                qlrglET.Add(new QLRGL_INFO()
                {
                    GLBM = Provider.Sql.Create().ToString(),
                    SLBH = newSLBH,
                    YWBM = newSLBH,
                    ZJHM = ParcelInfo.YHYTSHXYDM,
                    QLRID = ParcelInfo.YHYTSHXYDM2,
                    QLRMC = ParcelInfo.DYQRMC,
                    ZJLB = "8",//"统一社会信用代码",
                    DH = ParcelInfo.DYLXRDH,
                    QLRLX = "抵押权人"
                });
                count = await _constructionServices.Construction(regInfo, flowInfo, tsglET, dyET, xgdjglET, qlrglET);
                return new MessageModel<int>()
                {
                    msg = "获取成功",
                    success = true,
                    response = count
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<int>()
                {
                    msg = "保存失败" + ex.Message,
                    success = false,
                    response = 0
                };
                //string errorDynCode = Guid.NewGuid().ToString();
                //_logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                //return new MessageModel<PageModel<ImmovablesRelationVModel>>()
                //{
                //    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                //    success = false,
                //    response = null
                //};
            }

        }

        /// <summary>
        /// 获取宗地TSTYBM
        /// </summary>
        /// <param name="zjhm">证件号码</param>
        /// <param name="bdczh">不动产证号</param>
        /// <param name="qlrmc">权利人名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<DJ_TSGL>>> GetZdTstybmByZjhm(string zjhm, string bdczh, string qlrmc)
        {
            try
            {
                var data = await _constructionServices.GetZdTstybmByZjhm(zjhm,bdczh,qlrmc);
                if(data.Count > 0)
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
                        msg = "未获取到查询数据",
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
        /// 查询在建工程抵押变更相关信息
        /// </summary>
        /// <param name="bdczmh"></param>
        /// <param name="bdczh"></param>
        /// <param name="dyr"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<ConstructionChangeVModel>>> GetConstructionChangeList(string bdczmh, string bdczh, string dyr)
        {
            try
            {
                var data = await _constructionChangeRepository.GetConstructionChangeList(bdczmh, bdczh, dyr);
                if (data.Count > 0)
                {
                    return new MessageModel<List<ConstructionChangeVModel>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<ConstructionChangeVModel>>()
                    {
                        msg = "未获取到查询数据",
                        success = false,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<ConstructionChangeVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }


        


    }
}
