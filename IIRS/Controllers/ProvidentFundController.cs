using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.IServices.WQ;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC.print;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Models.ViewModel.WQ;
using IIRS.Utilities;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Widget;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace IIRS.Controllers
{
    /// <summary>
    /// 公积金查询
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    public class ProvidentFundController : ControllerBase
    {
        private readonly IWQServices _wQServices;
        private readonly IDBTransManagement _dbTransManagement;
        private readonly ILogger<ProvidentFundController> _logger;
        private readonly IProvidentFundRepository _providentFundRepository;
        private readonly IProvidentFundServices _providentFundServices;
        private readonly IUserInfoRepository _userInfoRepository;

        public ProvidentFundController(IDBTransManagement dbTransManagement, ILogger<ProvidentFundController> logger, IProvidentFundRepository providentFundRepository, IProvidentFundServices providentFundServices, IUserInfoRepository userInfoRepository)
        {
            _dbTransManagement = dbTransManagement;
            _logger = logger;
            _providentFundRepository = providentFundRepository;
            _providentFundServices = providentFundServices;
            _userInfoRepository = userInfoRepository;
        }
        /// <summary>
        /// 公积金查询
        /// </summary>
        /// <param name="strProvidentModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<ProvidentFundModel>> GetProvident([FromForm] string strProvidentModel)
        {
            try
            {
                ProvidentFundVModel model = JsonConvert.DeserializeObject<ProvidentFundVModel>(HttpUtility.UrlDecode(strProvidentModel));
                //ProvidentFundVModel model = new ProvidentFundVModel();
                //model.qlrmc = "倪赫男";
                //model.zjhm = "211002198303042929";

                var data = await _providentFundServices.FundModels2(model);
                // 插入记录
                var count = await _providentFundRepository.Add(data);

                // 生成PDF

                var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "PDFTemplate.pdf");

                PdfDocument doc = new PdfDocument();
                doc.LoadFromFile(TemplateFile);

                var formWidget = doc.Form as PdfFormWidget;

                for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                {
                    var ttt = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                    switch (ttt.Name)
                    {
                        case "Name":
                            ttt.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 15f);
                            ttt.Text = data.qlrmc + "";
                            break;
                        case "IDNum":
                            ttt.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 15f);
                            ttt.Text = data.zjhm + "";
                            break;
                        case "SpouseName":
                            ttt.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 15f);
                            ttt.Text = data.qlrmc1 + "";
                            break;
                        case "SpouseIDNum":
                            ttt.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 15f);
                            ttt.Text = data.zjhm1 + "";
                            break;
                        case "CheckResultText":
                            ttt.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 15f);
                            ttt.Text = data.result + "";
                            break;
                        case "OrgName":
                            ttt.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 15f);
                            ttt.Text = "辽阳市不动产登记中心" + "";
                            break;
                        case "CheckDate":
                            ttt.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 15f);
                            ttt.Text = data.queryDate.ToString("yyyy年MM月dd日") + "";
                            break;
                        case "Operator":
                            ttt.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 13f);
                            //ttt.Text = "操作员：" + data.user_name + "           电话：0419 - 2136605";
                            ttt.Text = "操作员：" + data.user_name + "";
                            break;
                        default:
                            ttt.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 15f);
                            ttt.Text = "";
                            break;

                    }
                }
                //保存结果文档

                formWidget.IsFlatten = true;
                Stream stream = new MemoryStream();
                doc.SaveToStream(stream);

                byte[] arr = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(arr, 0, (int)stream.Length);
                stream.Close();
                data.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);

                
                return new MessageModel<ProvidentFundModel>()
                {
                    msg = "获取成功",
                    success = true,
                    response = data
                };

            }
            catch (ApplicationException ex)
            {
                return new MessageModel<ProvidentFundModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null
                };
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<ProvidentFundModel>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
            
        }

        /// <summary>
        /// 获取网签信息
        /// </summary>
        /// <param name="xm">姓名</param>
        /// <param name="sfzh">身份证号</param>
        /// <param name="htbh">合同编号</param>
        /// <param name="cxrxm">查询人姓名</param>
        /// <param name="cxrzjhm">查询人证件号码</param>
        /// <param name="dw">单位</param>
        /// <returns></returns>
        [HttpGet]
        public MessageResult QueryWQInfo(string xm, string sfzh, string htbh, string cxrxm, string cxrzjhm, string dw)
        {
            //孙成义
            //211002198502022912
            //20150900107
            var data = this._wQServices.GetResult(xm, sfzh, htbh, cxrxm, cxrzjhm, dw);
            return data;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="strPwdModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> UpdatePwd([FromForm] string strPwdModel)
        {
            var data = new MessageModel<string>();
            Sys_Userinfo model = JsonConvert.DeserializeObject<Sys_Userinfo>(HttpUtility.UrlDecode(strPwdModel));
            if (model != null)
            {
                var oldModel = await _userInfoRepository.Query(i => i.RealName == model.LoginName);
                if(oldModel != null)
                {
                    if(oldModel[0].LoginPWD == EncryptHelper.MD5Encrypt32(model.oldPWD))
                    {
                        if(model.NewPWD == model.SecondPwd)
                        {
                            oldModel[0].LoginPWD = EncryptHelper.MD5Encrypt32(model.NewPWD);
                            oldModel[0].LastLoginTime = DateTime.Now;
                            await _userInfoRepository.Update(oldModel[0]);
                            data.msg = "修改成功！";
                            data.success = true;
                        }
                        else
                        {
                            data.msg = "二次输入的密码不一致，请重新输入。";
                            data.success = false;
                        }
                    }
                    else
                    {
                        data.msg = "原始密码不正确，请重新输入。";
                        data.success = false;
                    }
                }
                else
                {
                    data.msg = "条件不足，无法修改";
                    data.success = false;
                }
            }else
            {
                data.msg = "条件不足，无法修改";
                data.success = false;
            }
            data.response = model.RealName;
            return data;
        }
        /// <summary>
        /// 查询结果追溯
        /// </summary>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="qlrmc"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<ProvidentFundModel>>> GetProvidentListToPage(DateTime startTime,DateTime endTime,string qlrmc)
        {
            try
            {
                var data = await _providentFundRepository.QueryPage(i => i.qlrmc == qlrmc || (i.queryDate > startTime && i.queryDate < endTime));

                if (data.data.Count > 0)
                {
                    return new MessageModel<PageModel<ProvidentFundModel>>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = data
                    };
                }
                else
                {
                    return new MessageModel<PageModel<ProvidentFundModel>>()
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
                return new MessageModel<PageModel<ProvidentFundModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
            
        }
    }
}
