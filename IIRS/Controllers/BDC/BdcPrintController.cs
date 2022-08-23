using IIRS.IRepository.Base;
using IIRS.IServices.BDC;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC.print;
using IIRS.Models.ViewModel.Print;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RT.Comb;
using Spire.Pdf;
using Spire.Pdf.AutomaticFields;
using Spire.Pdf.Graphics;
using Spire.Pdf.Grid;
using Spire.Pdf.Widget;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Controllers.BDC
{
    /// <summary>
    /// 不动产打印Controller
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    public class BdcPrintController : ControllerBase
    {
        private readonly IDBTransManagement _dbTransManagement;
        private readonly ILogger<BdcPrintController> _logger;
        private readonly IBdcPrintServices _bdcPrintServices;

        public BdcPrintController(IDBTransManagement dbTransManagement, ILogger<BdcPrintController> logger, IBdcPrintServices bdcPrintServices)
        {
            _dbTransManagement = dbTransManagement;
            _logger = logger;
            _bdcPrintServices = bdcPrintServices;
        }
        /// <summary>
        /// 一般抵押打印申报主债权合同及抵押合同
        /// </summary>
        /// <param name="strMrgePrintModel"></param>
        /// <returns></returns>
        //[HttpPost]
        //public async Task<MessageModel<MrgePrintVModel>> GetMrgePrint([FromForm] string strMrgePrintModel)
        //{
        //    String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
        //    String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
        //    try
        //    {
        //        //MrgePrintVModel model = JsonConvert.DeserializeObject<MrgePrintVModel>(HttpUtility.UrlDecode(strMrgePrintModel));
        //        MrgePrintVModel model = new MrgePrintVModel();
        //        model.dyht = "合同1235";
        //        model.dyr = "张三";
        //        model.dyr_zjlb = "身份证";
        //        model.dyr_zjhm = "11111111";
        //        model.dyqr = "李四";
        //        model.dyqr_zjlb = "身份证";
        //        model.dyqr_zjhm = "22222222";
        //        model.zwr = "顽固";
        //        model.zwr_zjlb = "身份证";
        //        model.zwr_zjhm = "333333";
        //        model.bdbzzqse = "10000000";
        //        model.dylx = "一般抵押";
        //        model.qsrq = "2020年10月1日";
        //        model.zzrq = "2021年9月30日";
        //        model.zwqx = model.qsrq + " 至 \r\n " + model.zzrq;
        //        model.zl = "地址111111111111111";
        //        model.bdcdyh = "bdcdyh111111111";
        //        model.bdczh = "bdczh222222222";
        //        model.jzmj = "102.22";
        //        model.fj = "fj22222222222222";

        //        // 生成PDF

        //        var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "MgrePdf.pdf");

        //        PdfDocument doc = new PdfDocument();
        //        doc.LoadFromFile(TemplateFile);

        //        var formWidget = doc.Form as PdfFormWidget;

        //        for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
        //        {
        //            var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
        //            switch (print.Name)
        //            {
        //                case "dyht":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.dyht;
        //                    break;
        //                case "dyqr":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.dyqr;
        //                    break;
        //                case "dyqr_zjlb":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.dyqr_zjlb;
        //                    break;
        //                case "dyqr_zjhm":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.dyqr_zjhm;
        //                    break;
        //                case "dyr":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.dyr;
        //                    break;
        //                case "dyr_zjlb":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.dyr_zjlb;
        //                    break;
        //                case "dyr_zjhm":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.dyr_zjhm;
        //                    break;
        //                case "zwr":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.zwr;
        //                    break;
        //                case "zwr_zjlb":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.zwr_zjlb;
        //                    break;
        //                case "zwr_zjhm":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.zwr_zjhm;
        //                    break;
        //                case "bdbzzqse":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.bdbzzqse;
        //                    break;
        //                case "dylx_1":
        //                    if(model.dylx == "一般抵押")
        //                    {
        //                        print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
        //                        print.Text += "R";
        //                    }else
        //                    {
        //                        print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
        //                        print.Text = "o";
        //                    }
        //                    //print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    //print.Text = model.dylx;
        //                    break;
        //                case "dylx_2":
        //                    if (model.dylx == "最高额抵押")
        //                    {
        //                        print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
        //                        print.Text += "R";
        //                    }
        //                    else
        //                    {
        //                        print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
        //                        print.Text = "o";
        //                    }
        //                    break;
        //                case "zwqx":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.zwqx;
        //                    break;
        //                case "zl":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.zl;
        //                    break;
        //                case "bdcdyh":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.bdcdyh;
        //                    break;
        //                case "bdczh":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.bdczh;
        //                    break;
        //                case "jzmj":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.jzmj;
        //                    break;
        //                case "fj":
        //                    print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
        //                    print.Text = model.fj;
        //                    break;
        //            }
        //        }
        //        //保存结果文档

        //        formWidget.IsFlatten = true;
        //        Stream stream = new MemoryStream();
        //        doc.SaveToStream(stream);

        //        byte[] arr = new byte[stream.Length];
        //        stream.Position = 0;
        //        stream.Read(arr, 0, (int)stream.Length);
        //        stream.Close();
        //        model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);

        //        if (model != null)
        //        {
        //            return new MessageModel<MrgePrintVModel>()
        //            {
        //                msg = "打印成功",
        //                success = true,
        //                response = model
        //            };
        //        }
        //        else
        //        {
        //            return new MessageModel<MrgePrintVModel>()
        //            {
        //                msg = "打印失败",
        //                success = false,
        //                response = model
        //            };
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        string errorDynCode = Guid.NewGuid().ToString();
        //        _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
        //        return new MessageModel<MrgePrintVModel>()
        //        {
        //            msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
        //            success = false,
        //            response = null
        //        };
        //    }
        //}
        /// <summary>
        /// 转移登记 - 不动产登记审批表打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<TransferSpbPrintVModel>> GetTransferSpbPrint(string xid)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            try
            {
                var model = await _bdcPrintServices.GetTransferSpbPrint(xid);
                if (model != null)
                {

                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "TransferSpbPrint.pdf");

                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;

                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.slbh + "";
                                break;
                            case "qlrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlrmc + "";
                                break;
                            case "qlrlx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlr_zjlb + "";
                                break;
                            case "bdczh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdczh + "";
                                break;
                            case "bdcdyh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdcdyh + "";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zl + " ";
                                break;
                            case "qlsdfs_1":
                                if (model.qlsdfs == "地表")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "qlsdfs_2":
                                if (model.qlsdfs == "地上")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "qlsdfs_3":
                                if (model.qlsdfs == "地下")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "qllx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qllx + " ";
                                break;
                            case "qlxz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlxz + " ";
                                break;
                            case "ywrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywrmc + " ";
                                break;
                            case "bdclx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdclx + " ";
                                break;
                            case "jzmj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.fwmj + " ";
                                break;
                            case "old_bdczh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.old_bdczh + " ";
                                break;
                            case "ghyt":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ghyt + " ";
                                break;
                            case "gyqk_1":
                                if (model.gyfs == "0")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "gyqk_2":
                                if (model.gyfs == "1")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "gyqk_3":
                                if (model.gyfs == "2")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "gyfe":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.gyfe + " ";
                                break;
                            case "syqx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.syqx + " ";
                                break;
                            case "bdbzqse":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdbzqse + " ";
                                break;
                            case "lxqx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.lxqx + " ";
                                break;
                            case "qt":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qt + " ";
                                break;
                            case "bz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.spbz + " ";
                                break;
                            case "fj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.fj + " ";
                                break;
                        }
                    }
                    //保存结果文档

                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "转移登记审批表测试.pdf");
                    doc.SaveToFile(savePath);
                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);
                    return new MessageModel<TransferSpbPrintVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = model
                    };

                }
                else
                {
                    return new MessageModel<TransferSpbPrintVModel>()
                    {
                        msg = "打印失败",
                        success = false,
                        response = null
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<TransferSpbPrintVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<TransferSpbPrintVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }
        /// <summary>
        /// 转移登记 - 不动产登记收件收据打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<TransferSjsjPrintVModel>> TransferSjsjPrint(string xid)
        {
            //xid = "21c0ae4b-5f44-4030-a494-0178d84f1158";
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            try
            {
                var model = await _bdcPrintServices.TransferSjsjPrint(xid);
                if (model.slbh != null)
                {
                    
                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "TransferSjPrint.pdf");

                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;

                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.slbh;
                                break;
                            case "xgzh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.xgzh;
                                break;
                            case "sqr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sqr;
                                break;
                            case "jjr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jjr;
                                break;
                            case "ywlx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywlx;
                                break;
                            case "tel":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.tel + " ";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zl + " ";
                                break;
                            case "fj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.fj + " ";
                                break;
                            case "sjr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sjr + " ";
                                break;
                            case "djrq":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.djrq.ToString() + " ";
                                break;
                            case "cnrq":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.cnrq.ToString() + " ";
                                break;
                            default:
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text =  " ";
                                break;

                        }
                    }
                    //保存结果文档

                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "转移登记收据测试.pdf");
                    doc.SaveToFile(savePath);
                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);
                    return new MessageModel<TransferSjsjPrintVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = model
                    };

                }
                else
                {
                    return new MessageModel<TransferSjsjPrintVModel>()
                    {
                        msg = "打印失败,请与管理员联系",
                        success = false,
                        response = null
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<TransferSjsjPrintVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<TransferSjsjPrintVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }
        /// <summary>
        /// 转移登记 - 不动产登记申请表打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<TransferSqbPrintVModel>> TransferSqbPrint(string xid)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            TransferSqbPrintVModel model = new TransferSqbPrintVModel();
            try
            {
                var data = await _bdcPrintServices.TransferSqbPrint(xid);
                model = data;
                if (model != null)
                {
                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "TransferSqbPrint.pdf");

                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;
                    #region
                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            #region 
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.slbh + " ";
                                break;
                            case "djrq":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.Concat(model.djrq) + " ";
                                break;
                            case "sjr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sjr + " ";
                                break;
                            case "qlrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlrmc + " ";
                                break;
                            case "qlr_zjlb":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlr_zjlb + " ";
                                break;
                            case "qlr_zjhm":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlr_zjhm + " ";
                                break;
                            case "ywrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywrmc + " ";
                                break;
                            case "ywr_zjlb":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywr_zjlb + " ";
                                break;
                            case "ywr_zjhm":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywr_zjhm + " ";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zl + " ";
                                break;
                            case "bdcdyh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdcdyh + " ";
                                break;
                            case "bdclx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdclx + " ";
                                break;
                            case "jzmj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.Concat(model.mj) + " ";
                                break;
                            case "djyy":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.djyy + " ";
                                break;
                            case "ghyt":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ghyt + " ";
                                break;
                            case "old_bdczh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.old_bdczh + " ";
                                break;
                            case "bdbzqse":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.Concat(model.bdbzqse);
                                break;
                            case "lxqx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.lxqx + " ";
                                break;                            
                            case "djyy_zmwj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.djyy_zmwj + " ";
                                break;
                            case "fj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.fj + " ";
                                break;
                            #endregion

                            #region djlx
                            case "djlx_1":
                                if (model.djlx == "首次登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_2":
                                if (model.djlx == "转移登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_3":
                                if (model.djlx == "变更登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_4":
                                if (model.djlx == "注销登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_5":
                                if (model.djlx == "更正登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_6":
                                if (model.djlx == "异议登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_7":
                                if (model.djlx == "预告登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_8":
                                if (model.djlx == "查封登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_9":
                                if (model.djlx == "其他")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            #endregion

                            #region djsyq
                            case "djsyq_1":
                                if (model.djsyq == "土地所有权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_2":
                                if (model.djsyq == "国有建设用地使用权及房屋所有权登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_3":
                                if (model.djsyq == "宅基地使用权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_4":
                                if (model.djsyq == "集体建设用地使用权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_5":
                                if (model.djsyq == "构筑物所有权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_6":
                                if (model.djsyq == "林地使用权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_7":
                                if (model.djsyq == "海域使用权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_8":
                                if (model.djsyq == "无居民海岛使用权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_9":
                                if (model.bdclx1 == "房屋所有权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_10":
                                if (model.djsyq == "土地承包经营权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_11":
                                if (model.djsyq == "森林、林木所有权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_12":
                                if (model.djsyq == "森林、林木使用权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_13":
                                if (model.djsyq == "抵押权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_14":
                                if (model.djsyq == "地役权")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsyq_15":
                                if (model.djsyq == "其他")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            default:
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = "";
                                break;
                                #endregion
                        }
                    }
                    #endregion
                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "转移登记申请表测试.pdf");
                    doc.SaveToFile(savePath);
                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);

                    return new MessageModel<TransferSqbPrintVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = model
                    };
                }
                else
                {
                    return new MessageModel<TransferSqbPrintVModel>()
                    {
                        msg = "打印失败",
                        success = false,
                        response = null
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<TransferSqbPrintVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<TransferSqbPrintVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }

        /// <summary>
        /// 转移登记 - 不动产登记收费单打印
        /// </summary>
        /// <param name="xid"></param>
        /// <param name="slbh">转移登记受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<TransferSfdPrintVModel>> TransferSfdPrint(string xid,string slbh)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            TransferSfdPrintVModel model = new TransferSfdPrintVModel();
            try
            {
                model = await _bdcPrintServices.TransferSfdPrint(xid,slbh);
                if (model != null)
                {
                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "TransferSfdPrint.pdf");

                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;
                    #region
                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            #region 
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.slbh + " ";
                                break;
                            case "ywlx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywlx + " ";
                                break;
                            case "xmmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.xmmc + " ";
                                break;
                            case "jfbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jfbh + " ";
                                break;
                            case "jfdw":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jfdw + " ";
                                break;
                            case "dh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dh + " ";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zl + " ";
                                break;
                            case "ysje":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ysje + " ";
                                break;
                            case "ssje":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ssje + " ";
                                break;
                            case "slr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jbr + " ";
                                break;
                            case "skrq":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.skrq + " ";
                                break;
                            #endregion

                            #region
                            case "sfxm1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfxm1 + " ";
                                break;
                            case "sfxm2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfxm2 + " ";
                                break;
                            case "sfxm3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfxm3 + " ";
                                break;
                            case "sfxm4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfxm4 + " ";
                                break;
                            case "jldw1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jldw1 + " ";
                                break;
                            case "jldw2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jldw2 + " ";
                                break;
                            case "jldw3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jldw3 + " ";
                                break;
                            case "jldw4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jldw4 + " ";
                                break;
                            case "sl1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sl1 + " ";
                                break;
                            case "sl2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sl2 + " ";
                                break;
                            case "sl3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sl3 + " ";
                                break;
                            case "sl4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sl4 + " ";
                                break;
                            case "sfbz1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfbz1 + " ";
                                break;
                            case "sfbz2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfbz2 + " ";
                                break;
                            case "sfbz3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfbz3 + " ";
                                break;
                            case "sfbz4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfbz4 + " ";
                                break;
                            case "jmje1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmje1 + " ";
                                break;
                            case "jmje2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmje2 + " ";
                                break;
                            case "jmje3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmje3 + " ";
                                break;
                            case "jmje4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmje4 + " ";
                                break;
                            case "jmyy1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmyy1 + " ";
                                break;
                            case "jmyy2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmyy2 + " ";
                                break;
                            case "jmyy3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmyy3 + " ";
                                break;
                            case "jmyy4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmyy4 + " ";
                                break;
                            case "hsje1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.hsje1 + " ";
                                break;
                            case "hsje2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.hsje2 + " ";
                                break;
                            case "hsje3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.hsje3 + " ";
                                break;
                            case "hsje4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.hsje4 + " ";
                                break;
                            case "bz1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bz1 + " ";
                                break;
                            case "bz2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bz2 + " ";
                                break;
                            case "bz3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bz3 + " ";
                                break;
                            case "bz4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bz4 + " ";
                                break;
                                #endregion
                        }
                    }
                    #endregion
                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "转移登记收费单测试.pdf");
                    doc.SaveToFile(savePath);
                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);

                    return new MessageModel<TransferSfdPrintVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = model
                    };
                }
                else
                {
                    return new MessageModel<TransferSfdPrintVModel>()
                    {
                        msg = "打印失败",
                        success = false,
                        response = null
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<TransferSfdPrintVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<TransferSfdPrintVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }
        #region 抵押打印
        /// <summary>
        /// 一般抵押 - 不动产登记收件收据打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<BdcGeneralMrgeSjsjPrintVModel>> GeneralMrgeSjsjPrint(string xid)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            //xid = "21c0ae4b-5f44-4030-a494-0178d84f1158";
            try
            {
                var model = await _bdcPrintServices.GeneralMrgeSjsjPrint(xid);
                if (model.slbh != null)
                {

                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "GeneralMrgeSjPrint.pdf");  //TransferSjPrint.pdf 修改名字为抵押收件PDF

                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;

                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.slbh;
                                break;
                            case "xgzh":    // 不动产证明号
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.xgzh + " ";
                                break;
                            case "dyqr":     //  申请人
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 8f);
                                print.Text = model.jjr + " ";
                                break;
                            case "dyr":     //抵押人
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dyr + " ";
                                break;
                            case "jjr":     //交件人
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jjr + " ";
                                break;
                            case "xmmc":        //业务类型
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.djlx+ " ";
                                break;
                            case "tel":     //电话
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.tel + " ";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zl + " ";
                                break;
                            case "fj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.fj + " ";
                                break;
                            case "sjr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sjr + " ";
                                break;
                            case "sjsj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sjsj.ToString() + " ";
                                break;
                            case "cnsj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.cnrq.ToString() + " ";
                                break;
                            default:
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text =  "";
                                break;

                        }
                    }
                    //保存结果文档

                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "抵押登记收件收据测试.pdf");
                    doc.SaveToFile(savePath);
                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);
                    return new MessageModel<BdcGeneralMrgeSjsjPrintVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = model
                    };

                }
                else
                {
                    return new MessageModel<BdcGeneralMrgeSjsjPrintVModel>()
                    {
                        msg = "打印失败,请与管理员联系。",
                        success = false,
                        response = null
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<BdcGeneralMrgeSjsjPrintVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<BdcGeneralMrgeSjsjPrintVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }
        /// <summary>
        /// 一般抵押 - 不动产登记申请表打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<DySqbPrintVModel>> GeneralMrgeSqbPrint(string xid)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            try
            {
                //xid = "ceshixid123";
                var model = await _bdcPrintServices.GeneralMrgeSqbPrint(xid);
                if (model != null)
                {
                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "GeneralMrgeSqbPrint.pdf");

                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;
                    #region
                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            #region 
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.Concat(model.slbh) + " ";
                                break;                           
                            case "djrq":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.Concat(model.djrq) + " ";
                                break;
                            case "sjr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sjr + " ";
                                break;
                            case "qlrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlrmc + " ";
                                break;
                            case "qlr_zjlb":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlr_zjlb + " ";
                                break;
                            case "qlr_zjhm":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 8f);
                                print.Text = model.qlr_zjhm + " ";
                                break;
                            case "ywrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywrmc + " ";
                                break;
                            case "ywr_zjlb":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywr_zjlb + " ";
                                break;
                            case "ywr_zjhm":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywr_zjhm + " ";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zl + " ";
                                break;
                            case "bdcdyh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdcdyh + " ";
                                break;
                            case "jzmj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.Concat(model.mj) + " ";
                                break;                            
                            case "bdczh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 8f);
                                print.Text = model.xgzh + " ";
                                break;
                            case "qllx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qllx + " ";
                                break;
                            case "qlxz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlxz + " ";
                                break;
                            case "bdbzqse":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.Concat(model.bdbzqse);
                                break;
                            case "lxqx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.lxqx + " ";
                                break;                            
/*                            case "fj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.fj + " ";
                                break;*/
                            case "dbfw":
                               print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                               print.Text = model.dbfw + " ";
                               break;
                            case "djyy":
                               print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                               print.Text = model.djyy + " ";
                               break;
                            case "djyy_zmwj":
                               print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.djyy_zmwj + " ";
                                break;
                            case "sfyd1":
                                if (model.sfyd1 == "是")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "sfyd2":
                                if (model.sfyd2 == "否")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            #endregion

                            #region djlx
                            case "dylx_1":
                                if (model.dylx == "一般抵押")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "dylx_2":
                                if (model.dylx == "最高额抵押")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "dylx_3":
                                if (model.dylx == "在建工程抵押")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "dylx_4":
                                if (model.dylx == "其它")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            
                            #endregion

                            #region djlx
                            case "djlx_1":
                                if (model.djlx == "首次登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_2":
                                if (model.djlx == "转移登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_3":
                                if (model.djlx == "变更登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_4":
                                if (model.djlx == "注销登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_5":
                                if (model.djlx == "预告登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx_6":
                                if (model.djlx == "其它")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;

                            #endregion
                            default:
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = "";
                                break;
                        }
                    }
                    #endregion
                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "抵押登记申请表测试.pdf");
                    doc.SaveToFile(savePath);

                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);
                    return new MessageModel<DySqbPrintVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = model
                    };
                }
                else
                {
                    return new MessageModel<DySqbPrintVModel>()
                    {
                        msg = "打印失败",
                        success = false,
                        response = null
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<DySqbPrintVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<DySqbPrintVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }
        /// <summary>
        /// 一般抵押 - 抵押登记清单打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<BdcGeneralMrgeQdPrintVModel>> BdcGeneralMrgeQdPrint(string xid)
        {
            //xid = "21c0ae4b-5f44-4030-a494-0178d84f1158";
            try
            {
                var list = await _bdcPrintServices.BdcGeneralMrgeQdPrint(xid);
                if(list.modelList.Count > 0)
                {
                    DateTimeOffset dto = new DateTimeOffset(DateTime.Now);
                    var unixTime = Convert.ToInt32(dto.ToUnixTimeSeconds());

                    var rnd = new Random(unixTime);

                    String FontFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Font", "NSimSun-02.ttf");
                   
                    // 创建PDF文档
                    var doc = new PdfDocument();

                    // 设置纸张类型为 A4，纸张方向为横向，其宽度为842，高度为595
                    doc.PageSettings.Size = PdfPageSize.A4;
                    doc.PageSettings.Orientation = PdfPageOrientation.Landscape;
                    doc.PageSettings.Margins = new PdfMargins(0);

                    //创建PdfMargins对象，指定期望设置的页边距
                    PdfMargins margins = new PdfMargins(20, 20, 20, 20);
                    doc.Template.Top = CreateHeaderTemplate(doc, margins);
                    doc.Template.Bottom = CreateFooterTemplate(doc, margins);

                    //在文档模板的左右部分应用空白模板 
                    doc.Template.Left = new PdfPageTemplateElement(margins.Left, doc.PageSettings.Size.Height);
                    doc.Template.Right = new PdfPageTemplateElement(margins.Right, doc.PageSettings.Size.Height);

                    // 根据文档默认设置创建第一页（文档至少包含一页内容）
                    var page1 = doc.Pages.Add();

                    // 页面内容尺寸
                    var pageSize = page1.GetClientSize();

                    // 创建写文本的默认笔刷
                    PdfSolidBrush brush = new PdfSolidBrush(new PdfRGBColor(0, 0, 0));

                    // 自定义页面标题 PdfTrueTypeFont
                    var titleFont = new PdfTrueTypeFont(FontFilePath, 18f, PdfFontStyle.Bold);

                    // 标题内容
                    string title = "不动产抵押物清单";

                    // 计算当前字体标题内容的尺寸
                    var titleSize = titleFont.MeasureString(title);

                    // 在页面居中位置写文本
                    page1.Canvas.DrawString(title, titleFont, brush, (pageSize.Width - titleSize.Width) / 2, 0);

                    // 受理编号字体
                    var slbhFont = new PdfTrueTypeFont(FontFilePath, 9f);

                    // 受理编号内容：
                    string slbh = "受理编号：" + list.modelList[0].slbh;

                    // 计算当前字体受理编号内容的尺寸
                    var slbhSize = slbhFont.MeasureString(slbh);

                    // 与标题下对齐
                    page1.Canvas.DrawString(slbh, slbhFont, brush, margins.Left, titleSize.Height - slbhSize.Height);

                    //设置表格默认字体
                    var gridFont = new PdfTrueTypeFont(FontFilePath, 8f);

                    //创建一个PdfGrid对象
                    PdfGrid grid = new PdfGrid();

                    //设置单元格边距
                    grid.Style.CellPadding = new PdfPaddings(1, 1, 1, 1);
                    grid.Style.Font = gridFont;
                    grid.AllowCrossPages = true;

                    //添加标题
                    PdfGridRow headerRow = grid.Rows.Add();

                    // 标题行背景色为灰色
                    headerRow.Style.BackgroundBrush = PdfBrushes.LightGray;

                    // 为表格添加 11 列
                    grid.Columns.Add(11);

                    // 设置每列宽度
                    grid.Columns[0].Width = 21;
                    grid.Columns[1].Width = 80;
                    grid.Columns[2].Width = 80;
                    grid.Columns[3].Width = 100;
                    grid.Columns[4].Width = 60;
                    grid.Columns[5].Width = 60;
                    grid.Columns[6].Width = 100;
                    grid.Columns[7].Width = 100;
                    grid.Columns[8].Width = 50;
                    grid.Columns[9].Width = 50;
                    grid.Columns[10].Width = 100;

                    // 设置表格标题行文本
                    headerRow.Cells[0].Value = "编号";
                    headerRow.Cells[1].Value = "不动产权证号";
                    headerRow.Cells[2].Value = "不动产单元号";
                    headerRow.Cells[3].Value = "坐落";
                    headerRow.Cells[4].Value = "宗地面积/房屋建筑面积(㎡)";
                    headerRow.Cells[5].Value = "土地分摊面积(㎡)";
                    headerRow.Cells[6].Value = "权利人名称";
                    headerRow.Cells[7].Value = "权利人证件号";
                    headerRow.Cells[8].Value = "用途";
                    headerRow.Cells[9].Value = "土地权利性质";
                    headerRow.Cells[10].Value = "附记";

                    // 设置表格标题行文本对齐方式
                    headerRow.Cells[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                    headerRow.Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                    headerRow.Cells[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                    headerRow.Cells[3].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                    headerRow.Cells[4].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                    headerRow.Cells[5].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                    headerRow.Cells[6].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                    headerRow.Cells[7].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                    headerRow.Cells[8].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                    headerRow.Cells[9].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                    headerRow.Cells[10].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);

                    for (int i = 0; i < list.modelList.Count; i++)
                    {
                        var data = list.modelList[i];
                        PdfGridRow row = grid.Rows.Add();
                        row.Cells[0].Value = (i + 1).ToString();
                        row.Cells[1].Value = data.bdczh;
                        row.Cells[2].Value = data.bdcdyh;
                        row.Cells[3].Value = data.zl;
                        row.Cells[4].Value = data.mj.ToString();
                        row.Cells[5].Value = data.ftmj.ToString();
                        row.Cells[6].Value = data.qlrmc;
                        row.Cells[7].Value = data.zjhm;
                        row.Cells[8].Value = data.yt_zwm;
                        row.Cells[9].Value = data.tdqlxz_zwm;
                        row.Cells[10].Value = data.fj;

                        row.Cells[0].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                        row.Cells[1].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                        row.Cells[2].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                        row.Cells[3].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                        row.Cells[4].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                        row.Cells[5].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                        row.Cells[6].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                        row.Cells[7].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                        row.Cells[8].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                        row.Cells[9].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                        row.Cells[10].StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle);
                    }

                    //设置边框颜色、粗细
                    PdfBorders borders = new PdfBorders();
                    borders.All = new PdfPen(new PdfRGBColor(0, 0, 0), 1f);
                    foreach (PdfGridRow pgr in grid.Rows)
                    {
                        foreach (PdfGridCell pgc in pgr.Cells)
                        {
                            pgc.Style.Borders = borders;
                        }
                    }

                    //在指定为绘入表格
                    grid.Draw(page1, 0.5f, titleSize.Height + 11);

                    //保存到文档
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "PDFS");

                    if (!Directory.Exists(savePath)) Directory.CreateDirectory(savePath);

                    savePath = Path.Combine(savePath, list.modelList[0].slbh + "PDF表格.pdf");

                    //保存到文档
                    doc.SaveToFile(savePath);

                    //formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);

                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    list.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);

                    return new MessageModel<BdcGeneralMrgeQdPrintVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = list
                    };
                }
                else
                {
                    return new MessageModel<BdcGeneralMrgeQdPrintVModel>()
                    {
                        msg = "打印失败",
                        success = false,
                        response = null
                    };
                }
                
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<BdcGeneralMrgeQdPrintVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<BdcGeneralMrgeQdPrintVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }
        /// <summary>
        /// 一般抵押 - 不动产登记收费单打印
        /// </summary>
        /// <param name="xid"></param>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<GeneralMrgeSfdPrintVModel>> GeneralMrgeSfdPrint(string xid,string slbh)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            GeneralMrgeSfdPrintVModel model = new GeneralMrgeSfdPrintVModel();
            try
            {
                model = await _bdcPrintServices.GeneralMrgeSfdPrint(xid,slbh);
                if (model != null)
                {
                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "GeneralMrgeSfdPrint.pdf");

                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;
                    #region
                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            #region 
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.slbh + " ";
                                break;
                            case "ywlx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywlx + " ";
                                break;
                            case "xmmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.xmmc + " ";
                                break;
                            case "jfbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jfbh + " ";
                                break;
                            case "jfdw":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jfdw + " ";
                                break;
                            case "dh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dh + " ";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zl + " ";
                                break;
                            case "ysje":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ysje + " ";
                                break;
                            case "ssje":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ssje + " ";
                                break;
                            case "slr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jbr + " ";
                                break;
                            case "skrq":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.skrq + " ";
                                break;
                            #endregion

                            #region
                            case "sfxm1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfxm1 + " ";
                                break;
                            case "sfxm2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfxm2 + " ";
                                break;
                            case "sfxm3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfxm3 + " ";
                                break;
                            case "sfxm4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfxm4 + " ";
                                break;
                            case "jldw1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jldw1 + " ";
                                break;
                            case "jldw2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jldw2 + " ";
                                break;
                            case "jldw3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jldw3 + " ";
                                break;
                            case "jldw4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jldw4 + " ";
                                break;
                            case "sl1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sl1 + " ";
                                break;
                            case "sl2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sl2 + " ";
                                break;
                            case "sl3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sl3 + " ";
                                break;
                            case "sl4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sl4 + " ";
                                break;
                            case "sfbz1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfbz1 + " ";
                                break;
                            case "sfbz2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfbz2 + " ";
                                break;
                            case "sfbz3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfbz3 + " ";
                                break;
                            case "sfbz4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sfbz4 + " ";
                                break;
                            case "jmje1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmje1 + " ";
                                break;
                            case "jmje2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmje2 + " ";
                                break;
                            case "jmje3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmje3 + " ";
                                break;
                            case "jmje4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmje4 + " ";
                                break;
                            case "jmyy1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmyy1 + " ";
                                break;
                            case "jmyy2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmyy2 + " ";
                                break;
                            case "jmyy3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmyy3 + " ";
                                break;
                            case "jmyy4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jmyy4 + " ";
                                break;
                            case "hsje1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.hsje1 + " ";
                                break;
                            case "hsje2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.hsje2 + " ";
                                break;
                            case "hsje3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.hsje3 + " ";
                                break;
                            case "hsje4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.hsje4 + " ";
                                break;
                            case "bz1":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bz1 + " ";
                                break;
                            case "bz2":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bz2 + " ";
                                break;
                            case "bz3":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bz3 + " ";
                                break;
                            case "bz4":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bz4 + " ";
                                break;
                            default:
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = "";
                                break;

                                #endregion
                        }
                    }
                    #endregion

                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "抵押登记收费单测试.pdf");
                    doc.SaveToFile(savePath);
                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);

                    return new MessageModel<GeneralMrgeSfdPrintVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = model
                    };
                }
                else
                {
                    return new MessageModel<GeneralMrgeSfdPrintVModel>()
                    {
                        msg = "打印失败",
                        success = false,
                        response = null
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<GeneralMrgeSfdPrintVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<GeneralMrgeSfdPrintVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }
        /// <summary>
        /// 一般抵押 - 不动产登记审批表
        /// </summary>
        /// <param name="xid"></param>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<DySpbPrintVModel>> GeneralMrgeSpbPrint(string xid,string slbh)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            try
            {
                var model = await _bdcPrintServices.GeneralMrgeSpbPrint(xid,slbh);
                int j = 0;
                if (model != null)
                {

                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "DyspbPDF.pdf");

                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;

                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.slbh + "";
                                break;
                            case "qlrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlrmc + "";
                                break;
                            case "xgzh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.xgzh + "";
                                break;
                            case "bdcdyh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdcdyh + "";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zl + "";
                                break;
                            case "fwqllx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.fwqllx + "";
                                break;
                            case "fwqlxz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.fwqlxz + "";
                                break;
                            case "bdclx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdclx + "";
                                break;
                            case "fwmj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jzmj + "";
                                break;
                            case "gzwlx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.gzwlx + "";
                                break;
                            case "lz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.lz + "";
                                break;
                            case "gyfe":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.gyfe + "";
                                break;
                            case "ybdczh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ybdczh + "";
                                break;
                            case "syqx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.syqx + "";
                                break;
                            case "ywrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywrmc + "";
                                break;
                            case "gyfs1":
                                if (model.gyfs1 == "0")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "gyfs2":
                                if (model.gyfs1 == "1")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "gyfs3":
                                if (model.gyfs1 == "2")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "zqse":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zqse + "";
                                break;
                            case "lxqx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.lxqx + "";
                                break;
                            case "dyfw":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dyfw + "";
                                break;
                            case "qtqlzk":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qtqlqk + "";
                                break;
                            case "sjr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sjr + "";
                                break;
                            case "sjsj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sjsj + "";
                                break;
/*                            case "scr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.scr + "";
                                break;
                            case "scsj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.scsj + "";
                                break;*/
                            case "bz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bz + "";
                                break;
                            default:
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = "";
                                break;

                        }
                    }
                    //保存结果文档

                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "抵押登记审批表测试.pdf");
                    doc.SaveToFile(savePath);
                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);
                    return new MessageModel<DySpbPrintVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = model
                    };

                }
                else
                {
                    return new MessageModel<DySpbPrintVModel>()
                    {
                        msg = "打印失败",
                        success = false,
                        response = null
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<DySpbPrintVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<DySpbPrintVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }
        #endregion
        #region 抵押注销打印
        /// <summary>
        /// 抵押注销 - 不动产登记收件收据打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<MrgeReleaseSjPrintVModel>> MrgeReleaseSjPrint(string xid)
        {
            //xid = "21c0ae4b-5f44-4030-a494-0178d84f1158";
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            try
            {
                var model = await _bdcPrintServices.MrgeReleaseSjPrint(xid);
                if (model.slbh != null)
                {
                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "MrgeReleaseSjPrint.pdf");
                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;
                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.slbh;
                                break;
                            case "xgzh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.xgzh;
                                break;
                            case "dyqr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sqr + "";
                                break;
                            case "dyr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dyr + "";
                                break;
                            case "jjr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.jjr + "";
                                break;
                            case "xmmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywlx;
                                break;
                            case "tel":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.tel + " ";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zl + " ";
                                break;
                            case "fj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.fj + " ";
                                break;
                            case "sjr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sjr + " ";
                                break;
                            case "sjsj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.djrq.ToString() + " ";
                                break;
                            case "cnsj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.cnsj.ToString() + " ";
                                break;

                        }
                    }
                    //保存结果文档
                    
                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "抵押注销收件收据测试.pdf");
                    doc.SaveToFile(savePath);

                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);

                    return new MessageModel<MrgeReleaseSjPrintVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = model
                    };

                }
                else
                {
                    return new MessageModel<MrgeReleaseSjPrintVModel>()
                    {
                        msg = "打印失败,请与管理员联系",
                        success = false,
                        response = null
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<MrgeReleaseSjPrintVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<MrgeReleaseSjPrintVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }
        /// <summary>
        /// 抵押注销 - 不动产登记申请表打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<MrgeReleaseSqbVModel>> MrgeReleaseSqbPrint(string xid)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            try
            {
                var model = await _bdcPrintServices.MrgeReleaseSqbPrint(xid);
                if (model != null)
                {

                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Template", "MrgeReleaseSqbPrint.pdf");
                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;
                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.slbh;
                                break;
                            case "sjrq":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sjrq + " ";
                                break;
                            case "sjr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sjr;
                                break;
                            #region djlx
                            case "djsy_1":
                                if (model.djsy == "一般抵押")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsy_2":
                                if (model.djsy == "最高额抵押")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsy_3":
                                if (model.djsy == "在建工程抵押")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsy_4":
                                if (model.djsy == "其他")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlb_1":
                                if (model.djlb == "首次登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlb_2":
                                if (model.djlb == "转移登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlb_3":
                                if (model.djlb == "变更登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlb_4":
                                if (model.djlb == "抵押注销")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlb_5":
                                if (model.djlb == "预告登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlb_6":
                                if (model.djlb == "其他")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            #endregion
                            case "dyqrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dyqrmc + " ";
                                break;
                            case "dyqr_zjlb":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dyqr_zjlb;
                                break;
                            case "dyqr_zjhm":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dyqr_zjhm + " ";
                                break;
                            case "dyrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dyrmc + " ";
                                break;
                            case "dyr_zjlb":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dyr_zjlb + " ";
                                break;
                            case "dyr_zjhm":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dyr_zjhm + " ";
                                break;
                            case "bdcdyh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdcdyh + " ";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zl + " ";
                                break;
                            case "qllx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qllx + " ";
                                break;
                            case "qlxz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlxz + " ";
                                break;
                            case "bbczsh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdcdyh + " ";
                                break;
                            case "mj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.IsNullOrEmpty(model.mj + "") ? "" : (model.mj + "㎡");
                                break;
                            case "bdczmh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdczmh + " ";
                                break;
                            case "bdbzqse":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdbzqse + " ";
                                break;
                            case "zwlxqx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zwlxqx + " ";
                                break;
                            case "dbfw":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dbfw + " ";
                                break;
                            #region
                            case "sfyd_1":
                                if (model.sfyd == "是")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "sfyd_2":
                                if (model.sfyd == "否")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            #endregion
                            case "djyy":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.djyy + " ";
                                break;
                            case "djyyzmwj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.djyyzmwj + " ";
                                break;
                            case "bz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bz + " ";
                                break;
                            default:
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = "";
                                break;
                        }
                    }
                    //保存结果文档

                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "DYZX_SQB.pdf");
                    doc.SaveToFile(savePath);
                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);

                    return new MessageModel<MrgeReleaseSqbVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = model
                    };

                }
                else
                {
                    return new MessageModel<MrgeReleaseSqbVModel>()
                    {
                        msg = "打印失败",
                        success = false,
                        response = null
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<MrgeReleaseSqbVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<MrgeReleaseSqbVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }

        /// <summary>
        /// 抵押注销 - 不动产登记审批表
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<MrgeReleaseSpbVModel>> MrgeReleaseSpbPrint(string xid)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            try
            {
                var model = await _bdcPrintServices.MrgeReleaseSpbPrint(xid);
                int j = 0;
                if (model != null)
                {

                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "MrgeReleaseSpb.pdf");

                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;

                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.slbh + "";
                                break;
                            case "qlrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlrmc + "";
                                break;
                            case "bdczmh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdczmh + "";
                                break;
                            case "csr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sjr + "";
                                break;
                            case "fwmj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.IsNullOrEmpty(model.fwmj + "") ? "" : (model.fwmj + "㎡");
                                break;
                            case "old_bdcqzh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.old_bdcqzh + "";
                                break;
                            case "ghyt":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ghyt + "";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zl + "";
                                break;
                            case "bdclx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdclx + "";
                                break;
                            case "syqx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.syqx + "";
                                break;
                            case "ywrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywrmc + "";
                                break;
                            case "bdbzqse":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdbzqse + "";
                                break;
                            case "lxqx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.lxqx + "";
                                break;
                            case "dbfw":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dbfw + "";
                                break;
                            case "yd1":
                                if (model.yd1 == "是")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "yd2":
                                if (model.yd1 == "否")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "qt":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qt + "";
                                break;
                            case "cssj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.cssj + "";
                                break;
                            case "qllx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qllx + "";
                                break;
                            case "qlxz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlxz + "";
                                break;
                            case "bz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bz + "";
                                break;
                            default:
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = "";
                                break;

                        }
                    }
                    //保存结果文档

                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "抵押注销审批表测试.pdf");
                    _logger.LogDebug($"打印信息savePath:{savePath}");
                    doc.SaveToFile(savePath);
                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);
                    return new MessageModel<MrgeReleaseSpbVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = model
                    };

                }
                else
                {
                    return new MessageModel<MrgeReleaseSpbVModel>()
                    {
                        msg = "打印失败",
                        success = false,
                        response = null
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<MrgeReleaseSpbVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<MrgeReleaseSpbVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }

        /// <summary>
        /// 抵押注销 - 不动产登记清单打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<MrgeReleaseQdPrintVmodel>> MrgeReleaseQdPrint(string xid)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
          
            try
            {
                var list = await _bdcPrintServices.MrgeReleaseQdPrint(xid);
                if (list.modelList.Count > 0)
                {
                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "MrgeReleaseQdPrint.pdf");

                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;
                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = list.modelList[0].slbh + "";
                                break;
                            case "bdczmh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = list.modelList[0].bdczmh + "";
                                break;
                            case "bdczh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = list.modelList[0].bdczh + "";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = list.modelList[0].zl + "";
                                break;
                            case "qlrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = list.modelList[0].qlrmc + "";
                                break;
                            case "yt":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = list.modelList[0].yt + "";
                                break;
                            case "mj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = list.modelList[0].mj + "";
                                break;
                            case "zslx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = list.modelList[0].zslx + "";
                                break;
                            default:
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = "";
                                break;

                        }
                    }
                    //保存结果文档

                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "抵押注销抵押物清单测试.pdf");
                    doc.SaveToFile(savePath);
                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    list.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);


                    return new MessageModel<MrgeReleaseQdPrintVmodel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = list
                    };
                }
                else
                {
                    return new MessageModel<MrgeReleaseQdPrintVmodel>()
                    {
                        msg = "打印失败",
                        success = false,
                        response = null
                    };
                }

            }
            catch (ApplicationException ex)
            {
                return new MessageModel<MrgeReleaseQdPrintVmodel>()
                {
                    msg = ex.Message,
                    success = false,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<MrgeReleaseQdPrintVmodel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }

        #endregion
        private static PdfPageTemplateElement CreateHeaderTemplate(PdfDocument doc, PdfMargins margins)
        {
            //获取页面大小
            SizeF pageSize = doc.PageSettings.Size;

            //创建PdfPageTemplateElement对象headerSpace，即作页眉模板
            PdfPageTemplateElement headerSpace = new PdfPageTemplateElement(pageSize.Width, margins.Top);
            headerSpace.Foreground = false;

            //声明x,y两个float型变量
            float x = margins.Left;
            float y = 0;

            //返回headerSpace
            return headerSpace;
        }
        private static PdfPageTemplateElement CreateFooterTemplate(PdfDocument doc, PdfMargins margins)
        {
            // 设置全局字体文件路径
            String FontFilePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Font", "NSimSun-02.ttf");
            //获取页面大小
            SizeF pageSize = doc.PageSettings.Size;

            //创建PdfPageTemplateElement对象footerSpace，即页脚模板
            PdfPageTemplateElement footerSpace = new PdfPageTemplateElement(pageSize.Width, margins.Bottom);
            footerSpace.Foreground = false;

            //声明x,y两个float型变量
            float x = margins.Left;
            float y = 0.5f;

            //在footerSpace中绘制文字
            y = y + 2;
            PdfTrueTypeFont font = new PdfTrueTypeFont(FontFilePath, 10f, PdfFontStyle.Bold);

            //在footerSpace中绘制当前页码和总页码
            PdfPageNumberField number = new PdfPageNumberField();
            PdfPageCountField count = new PdfPageCountField();
            PdfCompositeField compositeField = new PdfCompositeField(font, PdfBrushes.Black, "第{0}页/共{1}页", number, count);
            compositeField.StringFormat = new PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Top);
            SizeF size = font.MeasureString(compositeField.Text);
            compositeField.Bounds = new RectangleF((pageSize.Width - x - size.Width) / 2, y, size.Width, size.Height);
            compositeField.Draw(footerSpace.Graphics);

            //返回footerSpace
            return footerSpace;
        }

        #region 预告抵押申请表打印
        /// <summary>
        /// 预告抵押 - 预告抵押申请表打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<DyYgSqbPrintVModel>> DyYgSqbPrintPrint(string xid)
        {
            String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
            String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
            try
            {
                var model = await _bdcPrintServices.DyYgSqbPrintPrint(xid);
                if (model.slbh != null)
                {

                    var TemplateFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "YgDySqbPrint.pdf");  //TransferSjPrint.pdf 修改名字为抵押收件PDF

                    PdfDocument doc = new PdfDocument();
                    doc.LoadFromFile(TemplateFile);

                    var formWidget = doc.Form as PdfFormWidget;

                    for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                    {
                        var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                        switch (print.Name)
                        {
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.slbh;
                                break;
                            case "sjsj":    // 不动产证明号
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.sjsj.ToString() + " ";
                                break;
                            case "sjr":     //  申请人
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 8f);
                                print.Text = model.sjr + " ";
                                break;
                            #region djlx
                            case "djsy1":
                                if (model.djsy == "一般抵押")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsy2":
                                if (model.djsy == "最高额抵押")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsy3":
                                if (model.djsy == "在建工程抵押")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djsy4":
                                if (model.djsy == "其它")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx1":
                                if (model.djlx == "首次登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx2":
                                if (model.djlx == "转移登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx3":
                                if (model.djlx == "变更登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx4":
                                if (model.djlx == "抵押注销")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx5":
                                if (model.djlx == "预告登记")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "djlx6":
                                if (model.djlx == "其他")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "sfyd1":
                                if (model.sfyd1 == "1")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "sfyd2":
                                if (model.sfyd2 != "1")
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            #endregion
                            case "qlrxm":     //抵押人
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlrxm + " ";
                                break;
                            case "qlr_zjzl":     //交件人
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlr_zjzl + " ";
                                break;
                            case "qlr_zjhm":        //业务类型
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlr_zjhm + " ";
                                break;
                            case "ywrxm":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywrmc + " ";
                                break;
                            case "ywr_zjzl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywr_zjzl + " ";
                                break;
                            case "ywr_zjhm":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.ywr_zjhm + " ";
                                break;
                            case "bdcdyh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdcdyh + " ";
                                break;
                            case "zl":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zl + " ";
                                break;
                            case "qlxz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.qlxz + " ";
                                break;
                            case "xgzh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 8f);
                                print.Text = model.xgzh + " ";
                                break;
                            case "mj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.mj + " ";
                                break;
                            case "bdcqlsdqx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdcqlsdqx + " ";
                                break;
                            case "bdbzqse":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bdbzqse + " ";
                                break;
                            case "zwlxqx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zwlxqx + " ";
                                break;
                            case "zjjzwdyfw":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zjjzwdyfw + " ";
                                break;
                            case "dbfw":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.dbfw + " ";
                                break;
                            case "djyy":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.djyy + " ";
                                break;
                            case "zmwj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.zmwj + " ";
                                break;
                            case "bz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = model.bz + " ";
                                break;
   
                            default:
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = "";
                                break;

                        }
                    }
                    //保存结果文档

                    formWidget.IsFlatten = true;
                    Stream stream = new MemoryStream();
                    doc.SaveToStream(stream);
                    var savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "预告抵押申请表.pdf");
                    doc.SaveToFile(savePath);
                    byte[] arr = new byte[stream.Length];
                    stream.Position = 0;
                    stream.Read(arr, 0, (int)stream.Length);
                    stream.Close();
                    model.PDFFile = "data:application/pdf;base64," + Convert.ToBase64String(arr);
                    return new MessageModel<DyYgSqbPrintVModel>()
                    {
                        msg = "打印成功",
                        success = true,
                        response = model
                    };

                }
                else
                {
                    return new MessageModel<DyYgSqbPrintVModel>()
                    {
                        msg = "打印失败,请与管理员联系。",
                        success = false,
                        response = null
                    };
                }
            }
            catch (ApplicationException ex)
            {
                return new MessageModel<DyYgSqbPrintVModel>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"MrgeReleaseController.Post:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);

                return new MessageModel<DyYgSqbPrintVModel>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null,
                };
            }
        }
        #endregion

    }
}
