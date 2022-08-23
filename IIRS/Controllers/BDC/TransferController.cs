using IIRS.IServices;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC.Condition;
using IIRS.Models.ViewModel.TAX;
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

namespace IIRS.Controllers.BDC
{
    /// <summary>
    /// 转移登记
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    public class TransferController : ControllerBase
    {
        readonly IMortgageServices _mortgageService;
        private readonly IPublicBusinessServices _services;
        private readonly IConditionQueryServices _queryServices;
        private readonly ILogger<TransferController> _logger;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public TransferController(IMortgageServices mortgageService, IPublicBusinessServices services, IConditionQueryServices queryServices, ILogger<TransferController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _mortgageService = mortgageService;
            _logger = logger;
            _services = services;
            _queryServices = queryServices;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 转移登记数据提交
        /// </summary>
        /// <param name="StrInsertModel">不动产数据JSON</param>
        /// <param name="StrInsertTaxModel">税务数据JSON</param>
        /// <param name="strFileTree">附件JSON</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromForm] string StrInsertModel, [FromForm] string StrInsertTaxModel, [FromForm] string strFileTree)
        {
            InsertTransferVModel TransferModel = new InsertTransferVModel();
            InsertTaxVModel TaxModel = new InsertTaxVModel();
            List<Base64FilesVModel> fileTree = null;
            List<PUB_ATT_FILE> attFileModel = null;
            if (!string.IsNullOrEmpty(StrInsertModel))
            {
                try
                {
                    string saveDataJson = HttpUtility.UrlDecode(StrInsertModel);
                    TransferModel = JsonConvert.DeserializeObject<InsertTransferVModel>(saveDataJson);
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

            if (!string.IsNullOrEmpty(StrInsertTaxModel))
            {
                try
                {
                    string saveDataJson = HttpUtility.UrlDecode(StrInsertTaxModel);
                    TaxModel = JsonConvert.DeserializeObject<InsertTaxVModel>(saveDataJson);
                }
                catch (Exception ex)
                {
                    return new MessageModel<string>()
                    {
                        msg = "税务数据保存格式错误，请与管理员联系。" + ex.ToString(),
                        success = false
                    };
                }
            }

            if (!string.IsNullOrEmpty(strFileTree))
            {
                try
                {
                    fileTree = JsonConvert.DeserializeObject<List<Base64FilesVModel>>(strFileTree);
                    string newSLBH = TransferModel.newSlbh;//服务器端获取受理编号
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
                var data = await _queryServices.InsertTransferPost(TransferModel, TaxModel, attFileModel);                
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
        /// 税务信息提交
        /// </summary>
        /// <param name="StrInsertTaxModel">税务信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<int>> TaxPost([FromForm] string StrInsertTaxModel)
        {
            try
            {

                InsertTaxVModel Model = JsonConvert.DeserializeObject<InsertTaxVModel>(HttpUtility.UrlDecode(StrInsertTaxModel));
                var data = await _queryServices.InsertTaxPost(Model);
                if(data > 0)
                {
                    return new MessageModel<int>()
                    {
                        msg = "数据保存成功！",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<int>()
                    {
                        msg = "数据保存失败！",
                        success = false,
                        response = data
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<int>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = 0
                };
            }
            

            
        }

        /// <summary>
        /// 登记查询房屋信息
        /// </summary>        
        /// <param name="bdcdyh">不动产单元号</param>
        /// <param name="bdczh">不动产证号</param>
        /// <param name="slbh">受理编号</param>
        /// <param name="qlrmc">权利人名称</param>
        /// <param name="zl">坐落</param>
        /// <param name="zslx">证书类型</param>
        /// <param name="intPageIndex">当前页标</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<ConditionQueryResultVModel>>> QueryHouseInfo(string bdcdyh, string bdczh, string slbh, string qlrmc, string zl, string zslx, int intPageIndex, int PageSize)
        {
            try
            {
                if (bdcdyh == null && bdczh == null && slbh == null && qlrmc == null && zl == null)
                {
                    return new MessageModel<PageModel<ConditionQueryResultVModel>>()
                    {
                        msg = "获取失败,请输入任意条件进行查询。",
                        success = false,
                        response = null
                    };
                }
                else
                {
                    var data = await _queryServices.GetQueryResult(bdcdyh, bdczh, slbh, qlrmc, zl, zslx, intPageIndex, PageSize);
                    return new MessageModel<PageModel<ConditionQueryResultVModel>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }

            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PageModel<ConditionQueryResultVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 查询幢信息
        /// </summary>
        /// <param name="bdcdyh">不动产单元号</param>
        /// <param name="zl">坐落</param>
        /// <param name="zh">自然幢号</param>
        /// <param name="xmmc">项目名称</param>
        /// <param name="intPageIndex">当前页标</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<FC_Z_QSDC>>> GetFc_zResult(string bdcdyh, string zl, string zh, string xmmc, int intPageIndex, int PageSize)
        {
            try
            {
                if (bdcdyh == null && zl == null && zh == null && xmmc == null)
                {
                    return new MessageModel<PageModel<FC_Z_QSDC>>()
                    {
                        msg = "获取失败，请输入任意条件进行查询。",
                        success = false,
                        response = null
                    };
                }
                else
                {
                    var data = await _queryServices.GetFc_zResult(bdcdyh, zl, zh, xmmc, intPageIndex, PageSize);
                    if(data.dataCount > 0)
                    {
                        return new MessageModel<PageModel<FC_Z_QSDC>>()
                        {
                            msg = "获取成功",
                            success = true,
                            response = data
                        };
                    }
                    else
                    {
                        return new MessageModel<PageModel<FC_Z_QSDC>>()
                        {
                            msg = "未查询到数据",
                            success = false,
                            response = null
                        };
                    }
                    
                }

            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"TransferController.GetFc_zResult:错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PageModel<FC_Z_QSDC>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 查询户信息
        /// </summary>
        /// <param name="tstybm">幢TSTYBM</param>
        /// <param name="intPageIndex">当前页标</param>
        /// <param name="PageSize">每页大小</param>
        /// <param name="bdcdyh">不动产单元号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<fc_hResultVModel>>> GetFc_hResult(string tstybm, int intPageIndex, int PageSize, string bdcdyh)
        {
            try
            {
                if (tstybm == "")
                {
                    return new MessageModel<PageModel<fc_hResultVModel>>()
                    {
                        msg = "获取失败,请选择幢信息。",
                        success = false,
                        response = null
                    };
                }
                else
                {
                    var data = await _queryServices.GetFc_hResult(tstybm, intPageIndex, PageSize, bdcdyh);
                    return new MessageModel<PageModel<fc_hResultVModel>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }

            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"TransferController.GetFc_hResult:错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<PageModel<fc_hResultVModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }


        /// <summary>
        /// 获取税务相关信息
        /// </summary>
        /// <param name="tstybm">房屋TSTYBM</param>
        /// <param name="xzqh">行政区划:白塔区</param>
        /// <param name="zcs">总层数</param>
        /// <param name="ghyt">规划用途</param>
        /// <param name="jzmj">建筑面积</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<TaxVModel>> GetTaxInfo(string tstybm, string xzqh, string zcs, string ghyt, decimal jzmj)
        {
            try
            {
                if(string.IsNullOrEmpty(zcs) || string.IsNullOrEmpty(ghyt) || jzmj == 0)
                {
                    string errorDynCode = Guid.NewGuid().ToString();
                    _logger.LogDebug($"TransferController.GetTaxInfo:错误码:{errorDynCode}");
                    return new MessageModel<TaxVModel>()
                    {
                        msg = "请输入正确的总层数："+ zcs + "规划用途：" + ghyt + "建筑面积：" + jzmj,
                        success = false,
                        response = null
                    };
                }
                else
                {
                    if (!string.IsNullOrEmpty(xzqh))
                    {
                        xzqh = xzqh.Trim();
                        var strTax = await _queryServices.GetTaxInfo(tstybm, xzqh, zcs, ghyt, jzmj);
                        if (strTax != null)
                        {
                            return new MessageModel<TaxVModel>()
                            {
                                msg = "获取成功",
                                success = true,
                                response = strTax
                            };
                        }
                        else
                        {
                            return new MessageModel<TaxVModel>()
                            {
                                msg = "获取失败",
                                success = false,
                                response = null
                            };
                        }
                    }
                    else
                    {
                        string errorDynCode = Guid.NewGuid().ToString();
                        _logger.LogDebug($"TransferController.GetTaxInfo:错误码:{errorDynCode}");
                        return new MessageModel<TaxVModel>()
                        {
                            msg = "行政区划为空。" + errorDynCode,
                            success = false,
                            response = null
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"TransferController.GetTaxInfo:错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<TaxVModel>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 获取缴费类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> GetPaymentType()
        {
            try
            {
                var data = await _queryServices.GetPaymentType();
                if (data.Count > 0)
                {
                    return new MessageModel<List<SYS_DIC>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<SYS_DIC>>()
                    {
                        msg = "获取失败",
                        success = true,
                        response = null
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"TransferController.GetPaymentType:错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }
        /// <summary>
        /// 缴费标注明细
        /// </summary>
        /// <param name="itemNote">缴费类型</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> GetPaymentTypeInfo(string itemNote)
        {
            //itemNote = "不动产登记费";
            try
            {
                var data = await _queryServices.GetPaymentTypeInfo(itemNote);
                if (data.Count > 0)
                {
                    return new MessageModel<List<SYS_DIC>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<List<SYS_DIC>>()
                    {
                        msg = "获取失败",
                        success = true,
                        response = null
                    };
                }
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"TransferController.GetPaymentTypeInfo:错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        
    }
}
