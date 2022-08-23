using IIRS.IServices;
using IIRS.Models.ViewModel;
using IIRS.Utilities.Common;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using QRCoder;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 接口API控制器
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class CertificateElecController : ControllerBase
    {
        private readonly ILogger<CertificateElecController> _logger;

        private readonly IDYServices _IDYServices;

        public CertificateElecController(ILogger<CertificateElecController> logger, IDYServices IDYServices)
        {
            this._logger = logger;
            this._IDYServices = IDYServices;
        }


        [HttpGet]
        public async Task<MessageModel<IActionResult>> CertificateElec(string dySlbh)
        {
            string createPdfPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Temp", $"{dySlbh}_{Guid.NewGuid().ToString("N")}.pdf");
            try
            {
                var model = await _IDYServices.GetBdczmPdfInfo(dySlbh);
                if (model != null)
                {
                    string text_QT = string.Concat(model.QT), text_FJ = string.Concat(model.FJ), text_QLR = string.Concat(model.DYQRRMC)
                    , text_YWR = string.Concat(model.DYRMC)
                    , text_ZL = string.Concat(model.ZL)
                    , text_BDCDYH = string.Concat(model.BDCDYH);
                    string templeName = string.Empty, myrcodName = string.Empty, qrcodeName = string.Empty;
                    var install_Family = new SixLabors.Fonts.FontCollection().Install(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Font", "SimHei.ttf"));
                    var fontDefault = new SixLabors.Fonts.Font(install_Family, 50);  //默认字体
                    int tmpCount = 0;
                    using (Image image = Image.Load(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "BDCDJZM.png")))
                    {
                        #region 时间
                        image.Mutate(x => x.DrawText(
                                model.DJRQ.Year.ToString(), new SixLabors.Fonts.Font(install_Family, 70), Color.Black,
                                 new System.Numerics.Vector2(960, 1690)));

                        image.Mutate(x => x.DrawText(
                                model.DJRQ.Month.ToString("D2"), new SixLabors.Fonts.Font(install_Family, 70), Color.Black,
                                 new System.Numerics.Vector2(1190, 1690)));

                        image.Mutate(x => x.DrawText(
                                model.DJRQ.Day.ToString("D2"), new SixLabors.Fonts.Font(install_Family, 70), Color.Black,
                                 new System.Numerics.Vector2(1340, 1690)));
                        #endregion

                        image.Mutate(x => x.DrawText(model.ZSXLH, new SixLabors.Fonts.Font(install_Family, 80), Color.Black,
                             new System.Numerics.Vector2(880, 2075)));

                        #region 证明号
                        string ZMH_01 = model.SSJC, ZMH_02 = model.FZND, ZMH_03 = model.JGJC, ZMH_04 = model.ZSH;
                        var fontZMH = new SixLabors.Fonts.Font(install_Family, 65);
                        image.Mutate(x => x.DrawText(ZMH_01, fontZMH, Color.Black
                            , new System.Numerics.Vector2(1876, 327)));

                        image.Mutate(x => x.DrawText(ZMH_02, fontZMH, Color.Black
                            , new System.Numerics.Vector2(2010, 327)));

                        image.Mutate(x => x.DrawText(ZMH_03, fontZMH, Color.Black
                            , new System.Numerics.Vector2(2260, 327)));

                        image.Mutate(x => x.DrawText(ZMH_04, fontZMH, Color.Black
                            , new System.Numerics.Vector2(2850, 327)));
                        #endregion

                        image.Mutate(x => x.DrawText(model.LX, fontDefault, Color.Black,
                             new System.Numerics.Vector2(2355, 500)));

                        #region 权利人
                        tmpCount = SysUtility.ComputeChineseCount(text_QLR);
                        if (tmpCount <= 16)
                        {
                            image.Mutate(x => x.DrawText(text_QLR,
                                     fontDefault, Color.Black,
                                     new System.Numerics.Vector2(2355, 650)));
                        }
                        else if (tmpCount > 16 && tmpCount <= 25)
                        {
                            image.Mutate(x => x.DrawText(text_QLR,
                                     new SixLabors.Fonts.Font(install_Family, 32), Color.Black,
                                     new System.Numerics.Vector2(2355, 650)));
                        }
                        else
                        {
                            image.Mutate(x => x.DrawText(
                                    SysUtility.WrapText(text_QLR, 48),   //文字内容
                                     new SixLabors.Fonts.Font(install_Family, 40), Color.Black,
                                     new System.Numerics.Vector2(2355, 630))
                                 );
                        }
                        #endregion

                        #region 义务人
                        tmpCount = SysUtility.ComputeChineseCount(text_YWR);
                        if (tmpCount <= 16)
                        {
                            image.Mutate(x => x.DrawText(
                                    text_YWR,   //文字内容
                                     fontDefault, Color.Black,
                                     new System.Numerics.Vector2(2355, 800))
                                 );
                        }
                        else if (tmpCount > 16 && tmpCount <= 25)
                        {
                            image.Mutate(x => x.DrawText(
                                    text_YWR,   //文字内容
                                     new SixLabors.Fonts.Font(install_Family, 32), Color.Black,
                                     new System.Numerics.Vector2(2355, 800))
                                 );
                        }
                        else
                        {
                            image.Mutate(x => x.DrawText(SysUtility.WrapText(text_YWR, 48),
                                     new SixLabors.Fonts.Font(install_Family, 32), Color.Black,
                                     new System.Numerics.Vector2(2355, 780)));
                        }
                        #endregion

                        #region 坐落
                        tmpCount = SysUtility.ComputeChineseCount(text_ZL);
                        if (tmpCount <= 16)
                        {
                            image.Mutate(x => x.DrawText(text_ZL,
                                     fontDefault, Color.Black,
                                     new System.Numerics.Vector2(2355, 950)));
                        }
                        else if (tmpCount > 16 && tmpCount <= 25)
                        {
                            image.Mutate(x => x.DrawText(
                                    text_ZL,   //文字内容
                                     new SixLabors.Fonts.Font(install_Family, 32), Color.Black,
                                     new System.Numerics.Vector2(2355, 950))
                                 );
                        }
                        else
                        {

                            image.Mutate(x => x.DrawText(SysUtility.WrapText(text_ZL, 48),
                                     new SixLabors.Fonts.Font(install_Family, 32), Color.Black,
                                     new System.Numerics.Vector2(2355, 906)));
                        }
                        #endregion

                        #region 不动产单元号
                        tmpCount = SysUtility.ComputeChineseCount(text_BDCDYH);
                        if (tmpCount <= 16)
                        {
                            image.Mutate(x => x.DrawText(
                                    text_BDCDYH,   //文字内容
                                     fontDefault, Color.Black,
                                     new System.Numerics.Vector2(2355, 1100))
                                 );
                        }
                        else if (tmpCount > 16 && tmpCount <= 25)
                        {
                            image.Mutate(x => x.DrawText(
                                    text_BDCDYH,   //文字内容
                                     new SixLabors.Fonts.Font(install_Family, 32), Color.Black,
                                     new System.Numerics.Vector2(2355, 1100))
                                 );
                        }
                        else
                        {
                            image.Mutate(x => x.DrawText(SysUtility.WrapText(text_BDCDYH,48),
                                     new SixLabors.Fonts.Font(install_Family, 40), Color.Black,
                                     new System.Numerics.Vector2(2355, 1050)));
                        }
                        #endregion

                        #region 其他

                        var textLines = text_QT.Split(new string[] { "\n" }, StringSplitOptions.None);
                        double lineCount = -1;
                        //算法：先判断汉字数量，如果每行都不超过15个字（包括每行的换行情况）并且小于等于7行则使用默认字体，
                        //否则以最小字体当前行左上角排列方式
                        for (int i = 0; i < textLines.Length; i++)
                        {
                            textLines[i] = textLines[i].Trim();
                            lineCount += Math.Ceiling((SysUtility.ComputeChineseCount(textLines[i]) / 16F));//计算行数
                            if (lineCount > 8)
                            {
                                break;
                            }
                            textLines[i] = SysUtility.WrapText(textLines[i], 16);
                        }
                        if (lineCount <= 8)
                        {
                            float yOffset = (8 - float.Parse(string.Concat(lineCount))) * 24;
                            string newText = string.Join("\n", textLines);
                            image.Mutate(x => x.DrawText(newText, fontDefault, Color.Black,
                            new System.Numerics.Vector2(2355, 1205 + yOffset)));//每个字的行高大概47个像素
                        }
                        else
                        {
                            textLines = text_QT.Split(new string[] { "\n" }, StringSplitOptions.None);
                            for (int i = 0; i < textLines.Length; i++)
                            {
                                textLines[i] = textLines[i].Trim();
                                textLines[i] = SysUtility.WrapText(textLines[i], 29);
                            }
                            string newText = string.Join("\n", textLines);
                            int newlines = newText.Count<char>(c => c == '\n') + 1;
                            if (newlines <= 16)//如果小于等于14行，则计算偏移量
                            {
                                float yOffset = (16 - newlines) * 14;//每个字28个像素高度
                                image.Mutate(x => x.DrawText(newText,   //文字内容
                                     new SixLabors.Fonts.Font(install_Family, 28), Color.Black,
                                     new System.Numerics.Vector2(2355, 1205 + yOffset)));
                            }
                            else
                            {
                                image.Mutate(x => x.DrawText(newText,   //文字内容
                                 new SixLabors.Fonts.Font(install_Family, 28), Color.Black,
                                 new System.Numerics.Vector2(2355, 1205)));
                            }
                        }
                        #endregion

                        #region 附记
                        //int kk = text_FJ.Count<char>(c => c == '\n');
                        
                        if (!string.IsNullOrEmpty(text_FJ))
                        {
                            var textFjLines = text_FJ.Split(new string[] { "\n" }, StringSplitOptions.None);
                            lineCount = 0;
                            for (int i = 0; i < textFjLines.Length; i++)
                            {
                                textFjLines[i] = textFjLines[i].Trim();
                                lineCount += Math.Ceiling((SysUtility.ComputeChineseCount(textFjLines[i]) / 16F));//计算行数
                                if (lineCount > 7)
                                {
                                    break;
                                }
                                textFjLines[i] = SysUtility.WrapText(textFjLines[i], 16);
                            }
                            if (lineCount <= 7)
                            {
                                float yOffset = (7 - float.Parse(string.Concat(lineCount))) * 24;//计算Y轴偏移量,每个字的行高大概47个像素,每少一行大概减掉偏移量24个像素
                                string newText = string.Join("\n", textFjLines);
                                image.Mutate(x => x.DrawText(newText, fontDefault, Color.Black,
                                new System.Numerics.Vector2(2355, 1730 + yOffset)));
                            }
                            else
                            {
                                textFjLines = text_FJ.Split(new string[] { "\n" }, StringSplitOptions.None);
                                for (int i = 0; i < textFjLines.Length; i++)
                                {
                                    textFjLines[i] = textFjLines[i].Trim();
                                    textFjLines[i] = SysUtility.WrapText(textFjLines[i], 29);
                                }
                                string newText = string.Join("\n", textFjLines);
                                int newlines = newText.Count<char>(c => c == '\n') + 1;//得到换行数量+1，共计多少行
                                if (newlines <= 13)//如果小于等于14行，则计算偏移量
                                {
                                    float yOffset = (13 - newlines) * 14;//每个字28个像素高度
                                    image.Mutate(x => x.DrawText(newText,   //文字内容
                                         new SixLabors.Fonts.Font(install_Family, 28), Color.Black,
                                         new System.Numerics.Vector2(2355, 1720 + yOffset)));
                                }
                                else
                                {
                                    image.Mutate(x => x.DrawText(newText,   //文字内容
                                         new SixLabors.Fonts.Font(install_Family, 25), Color.Black,
                                         new System.Numerics.Vector2(2355, 1720)));
                                }
                            }
                        }

                        //int lineFeedCount = textFjLines.Length;//记录换行字符数量
                        //for (int i = 0; i < textFjLines.Length; i++)//循环排查是否某行有超长可能并换行
                        //{
                        //    textFjLines[i] = SysUtility.WrapText(textFjLines[i].Trim(), 46);
                        //    lineFeedCount += textFjLines[i].Count<char>(c => c == '\n');//记录是否超长增加换行数量，以记录总数量
                        //}
                        //string newTextFJ = string.Join("\n", textFjLines);
                        //if (lineFeedCount <= 5)//标准6行格式
                        //{
                        //    image.Mutate(x => x.DrawText(newTextFJ,
                        //        new SixLabors.Fonts.Font(install_Family, 31), Color.Black,
                        //        new System.Numerics.Vector2(2355, 1750)));
                        //}
                        #endregion
                        string bdczmNum = $"{ model.SSJC }({ model.FZND }){ model.JGJC }不动产证明第{ model.ZSH }号";
                        #region 二维码
                        string text_EWM = $"{model.SLBH}$${ bdczmNum }";
                        using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
                        {
                            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(text_EWM, QRCodeGenerator.ECCLevel.Q))
                            {
                                using (Base64QRCode qrCode = new Base64QRCode(qrCodeData))
                                {
                                    string strBase64Img = qrCode.GetGraphic(20, "#000000", "#FFFFFF", false, Base64QRCode.ImageType.Png);
                                    using (Image imageQR = Image.Load(Convert.FromBase64String(strBase64Img)))
                                    {
                                        imageQR.Mutate(ctx => ctx.Resize(350, 350));
                                        image.Mutate(x => x.DrawImage(imageQR, new Point(500, 1500), 1));
                                    }
                                }
                            }
                        }

                        #endregion

                        #region 生成PDF文件
                        using (var ImgStream = new MemoryStream())
                        {
                            image.SaveAsJpeg(ImgStream);
                            PdfDocument doc = new PdfDocument();
                            var imagePDF = Spire.Pdf.Graphics.PdfImage.FromStream(ImgStream);
                            PdfPageBase page = doc.Pages.Add(new System.Drawing.SizeF(imagePDF.Width, imagePDF.Height), new PdfMargins(0));
                            page.Canvas.DrawImage(imagePDF, 0, 0, image.Width, image.Height);
                            string pfxPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/cert/openssl.pfx");
                            var cert = new Spire.Pdf.Security.PdfCertificate(pfxPath, "kknd123.0");
                            var signature = new Spire.Pdf.Security.PdfSignature(doc, page, cert, "辽阳市不动产登记中心");
                            signature.SignFontColor = System.Drawing.Color.Black;
                            signature.Bounds = new System.Drawing.RectangleF(new System.Drawing.PointF(1150, 1500), new System.Drawing.SizeF(300, 300));

                            string signImagPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template/OfficialSeal.png");
                            signature.SignImageSource = PdfImage.FromFile(signImagPath);
                            signature.GraphicsMode = Spire.Pdf.Security.GraphicMode.SignImageOnly;
                            signature.NameLabel = "不动产登记号:";
                            signature.Name = bdczmNum;
                            signature.DateLabel = "签名日期:";
                            signature.Date = DateTime.Now;
                            signature.DocumentPermissions = Spire.Pdf.Security.PdfCertificationFlags.AllowFormFill | Spire.Pdf.Security.PdfCertificationFlags.ForbidChanges;
                            signature.Certificated = true;
                            signature.SignDetailsFont = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 15f);
                            signature.SignNameFont = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 15f);
                            signature.SignImageLayout = Spire.Pdf.Security.SignImageLayout.None;
                            doc.SaveToFile(createPdfPath);
                        }
                        #endregion

                        //#region 生成PDF文件
                        //using (var ImgStream = new MemoryStream())
                        //{
                        //    image.SaveAsJpeg(ImgStream);
                        //    byte[] buffer = ImgStream.ToArray();
                        //    ImgStream.Close();
                        //    var pdfImg = iTextSharp.text.Image.GetInstance(buffer, true);
                        //    var pageSize = new iTextSharp.text.Rectangle(image.Width + 20, image.Height);
                        //    var document = new iTextSharp.text.Document(pageSize, 0, 0, 0, 0);
                        //    var writer = iTextSharp.text.pdf.PdfWriter.GetInstance(document, new FileStream(createPdfPath, FileMode.Create));
                        //    document.Open();
                        //    var table = new iTextSharp.text.pdf.PdfPTable(1);
                        //    table.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_MIDDLE;
                        //    table.SetWidths(new int[] { 100 });
                        //    table.WidthPercentage = 100;

                        //    var cell = new iTextSharp.text.pdf.PdfPCell(pdfImg);
                        //    cell.BorderColor = iTextSharp.text.BaseColor.WHITE;
                        //    cell.BorderWidth = 0;
                        //    cell.Padding = 0;
                        //    cell.HorizontalAlignment = iTextSharp.text.pdf.PdfPCell.ALIGN_MIDDLE;
                        //    table.AddCell(cell);
                        //    document.Add(table);
                        //    document.Close();
                        //    writer.Close();
                        //}
                        //#endregion
                    }
                    return new MessageModel<IActionResult>()
                    {
                        msg = "获取成功",
                        success = true,
                        response = PhysicalFile(createPdfPath, "application/pdf", $"{ model.SSJC }({ model.FZND }){ model.JGJC }不动产证明第{ model.ZSH }号.pdf")
                    };
                }
                else
                {
                    return new MessageModel<IActionResult>()
                    {
                        msg = "获取成功",
                        success = false,
                        response = null
                    };
                }
            }
            catch (Exception ex)
            {
                string ee = ex.Message;
                string logErrorCode = Guid.NewGuid().ToString("N");
                string errorLog = $"CertificateElecController.CertificateElec:【错误代码：{logErrorCode},原因:{ex.Message}】";
                this._logger.LogDebug(errorLog);
                return new MessageModel<IActionResult>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null
                };
            }
        }

    }
}
