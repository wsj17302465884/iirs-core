using IIRS.IRepository.Base;
using IIRS.IRepository.BDC;
using IIRS.IRepository.IIRS;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Repository.Base;
using IIRS.Services.Base;
using IIRS.Utilities;
using IIRS.Utilities.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.CompilerServices.RuntimeHelpers;

namespace IIRS.Services.BDC
{
    public class PayMentServices : BaseServices, IPayMentServices
    {
        private readonly string GenerateTollCode_URL = "http://192.168.70.115:17001/standard-web/api/standard/invoiceEPayBook";
        //获取缴款书支付二维码URL
        private readonly string getPayQrCode_URL = "http://192.168.70.115:17001/standard-web/api/standard/getPayQrCode";
        private readonly string getPayBookConfirmResult_URL = "http://192.168.70.115:17001/standard-web/api/standard/getPayBookConfirmResult";
        //获取电子票据
        private readonly string getgetEBillByPayCode_URL = "http://192.168.70.115:17001/standard-web/api/standard/getEBillByPayCode";
        private readonly ILogger<PayMentServices> _logger;
        private readonly IPayBookResultRepository _payBookResultRepository;
        private readonly IDJ_SFDRepository _iDJ_SFDRepository;
        private readonly IDBTransManagement _dbTransManagement;
        public PayMentServices(IDBTransManagement dbTransManagement, ILogger<PayMentServices> logger, IPayBookResultRepository payBookResultRepository
            , IDJ_SFDRepository iDJ_SFDRepository) : base(dbTransManagement)
        {
            this._logger = logger;
            this._dbTransManagement = dbTransManagement;
            this._payBookResultRepository = payBookResultRepository;
            this._iDJ_SFDRepository = iDJ_SFDRepository;
        }
        /// <summary>
        /// 线下电子缴款书，生成缴款码
        /// </summary>
        /// <param name="slbh">缴费受理编号</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public async Task<string> GenerateTollCode(string slbh)
        {
            //slbh = "202112300204";
            string message = "";

            PayMentVModel payMent = new PayMentVModel();
            PayMentResultVModel payMentResultVModel = new PayMentResultVModel();
            PayParamVModel payParamVModel = new PayParamVModel();
            PAYBOOKRESULT payResult = new PAYBOOKRESULT();
            base.ChangeDB(SysConst.DB_CON_BDC);

            var vModel = await base.Db.Queryable<DJ_SFD, DJ_SJD,DJ_QLRGL,DJ_QLR>((A,B,C,D)
=> A.SLBH == B.SLBH && B.SLBH == C.SLBH && C.QLRID == D.QLRID).Where((A, B, C, D) => (A.SLBH == slbh && (C.QLRLX == "权利人" || C.QLRLX == "抵押权人")))
.Select((A, B, C, D) => new
{
    SLBH = A.SLBH,
    JFDW = A.JFDW,
    JBR = A.JBR,
    DH = A.DH,
    YSJE = A.YSJE,
    ZJHM = D.ZJHM,
    TZRYDDH = B.TZRYDDH,
    SFZT = A.SFZT,
    FSJFZT = A.FSJFZT
}).ToListAsync();

            string time = DateTime.Now.AddDays(7).ToString("yyyyMMdd");
            if(vModel.Count > 0)
            {
                //未缴费状态
                if (string.IsNullOrEmpty(vModel[0].SFZT) && string.IsNullOrEmpty(vModel[0].FSJFZT))
                {
                    payMent.busNo = vModel[0].SLBH;
                    payMent.effectiveDate = time;
                    payMent.payerType = "1";
                    payMent.payer = vModel[0].JFDW;
                    payMent.author = vModel[0].JBR;
                    payMent.idCardNo = vModel[0].ZJHM;
                    payMent.tel = vModel[0] != null ? vModel[0].DH : vModel[0].TZRYDDH;
                    payMent.totalAmt = vModel[0].YSJE;

                    if(payMent.totalAmt <= 0)
                    {
                        return message = "缴费金额为0，无需缴费！";
                    }

                    var SfdFbModel = await base.Db.Queryable<DJ_SFD_FB>().Where(i => i.SLBH == slbh).ToListAsync();
                    if(SfdFbModel.Count > 0)
                    {
                        foreach (var item in SfdFbModel)
                        {
                            chargeDetail model = new chargeDetail();
                            model.chargeName = item.SFXM;
                            model.std = item.SFBZ;
                            model.number = item.SL;
                            model.amt = item.HSJE;
                            payMent.chargeDetail.Add(model);
                        }
                    }
                    else
                    {
                        message = "缺少详细信息，请联系管理员！";
                    }                    
                }
                else
                {
                    message = "已缴费，请勿重复缴费！";
                }
            }
            //毫秒级时间戳
            payParamVModel.noise = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds).ToString();
            string a = JsonConvert.SerializeObject(payMent);
            payParamVModel.data = SysUtility.ToBase64Str(JsonConvert.SerializeObject(payMent));
            string fitst = "region=" + payParamVModel.region + "&deptcode=" + payParamVModel.deptcode + 
                "&appid="+ payParamVModel.appid + "&data=" + payParamVModel.data + "&noise=" + payParamVModel.noise;
            string second = fitst + "&key=0d7e2003c6c4c402eadd50bd44&version=1.0";
            payParamVModel.sign = EncryptHelper.Md5Method(second).ToUpper();

            //最终整合后的参数
            string GenerateParam = JsonConvert.SerializeObject(payParamVModel);

            _logger.LogDebug("电子缴款书请求参数:" + GenerateParam);
            _logger.LogDebug("电子缴款书请求参数data:" + JsonConvert.SerializeObject(payMent));
            using (HttpClient client = new HttpClient())
            {
                //body格式
                HttpContent content = new StringContent(GenerateParam, Encoding.UTF8, "application/json");
                //form-data格式
                //HttpContent content = new FormUrlEncodedContent(new[] { new KeyValuePair<string, string>("body", GenerateParam) });
                var responseData = client.PostAsync(this.GenerateTollCode_URL, content).Result;//得到返回字符流
                if (responseData.StatusCode != HttpStatusCode.OK)//如果接收失败
                {
                    message = "电子缴款书连接失败，请联系管理员!";
                }
                else
                {
                    string suceessMsg = responseData.Content.ReadAsStringAsync().Result;
                    JObject StrJson = (JObject)JsonConvert.DeserializeObject(suceessMsg);
                    string data = SysUtility.FromBase64Str(StrJson["data"].ToString());
                    JObject resultJson = (JObject)JsonConvert.DeserializeObject(data);
                    string result = resultJson["result"].ToString();
                    string retrunMess = SysUtility.FromBase64Str(resultJson["message"].ToString());
                    _logger.LogDebug("电子缴款书返回message:" + retrunMess);
                    if (result == "S0000")
                    {
                        string StrMessage = SysUtility.FromBase64Str(resultJson["message"].ToString());
                        payResult = JsonConvert.DeserializeObject<PAYBOOKRESULT>(StrMessage);
                        var resultData = _payBookResultRepository.Query(i => i.BUSNO == payMent.busNo).Result;
                        var sfdData = await base.Db.Queryable<DJ_SFD>().Where(i => i.SLBH == payMent.busNo).ToListAsync();
                        if (resultData.Count > 0 && sfdData.Count > 0)
                        {
                            var UpdatePay = _payBookResultRepository.Update(resultData[0]).Result;
                            sfdData[0].FSFPH = resultData[0].PAYCODE;
                            var updateSfd = _iDJ_SFDRepository.Update(sfdData[0]).Result;
                            if (UpdatePay)
                            {
                                message = "已更新缴款码！";
                                _logger.LogDebug("电子缴款码更新返回正确接口结果:" + StrMessage);
                            }
                            else
                            {
                                message = "未更新缴款码，请联系管理员！";
                                _logger.LogDebug("未更新缴款码结果:" + StrMessage);
                            }
                        }
                        else
                        {
                            var payCount = _payBookResultRepository.Add(payResult).Result;
                            if (payCount > 0)
                            {
                                message = "成功生成缴款码！";
                                _logger.LogDebug("电子缴款书返回正确接口结果:" + StrMessage);
                            }
                            else
                            {
                                message = "未生成缴款码，请联系管理员！";
                                _logger.LogDebug("未生成缴款码结果:" + StrMessage);
                            }
                        }
                        
                    }
                    else
                    {
                        message = "网络连接错误，请联系管理员！";
                        _logger.LogDebug("电子缴款书返回错误结果:" + SysUtility.FromBase64Str(resultJson["message"].ToString()));
                    }

                }
            }

            return message;
        }

        /// <summary>
        /// 生成缴款书支付二维码URL
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        public async Task<string> GetPayQrCode(string slbh)
        {
            //slbh = "202207050236";
            string message = "";
            string Param = "";

            if(!string.IsNullOrEmpty(GetPayCode(slbh)))
            {
                Param = strParam(GetPayCode(slbh));
                _logger.LogDebug("支付二维码URL请求参数:" + Param);
                try
                {
                    using (HttpClient client = new HttpClient())
                    {
                        HttpContent content = new StringContent(Param, Encoding.UTF8, "application/json");
                        var responseData = client.PostAsync(this.getPayQrCode_URL, content).Result;//得到返回字符流
                        if (responseData.StatusCode != HttpStatusCode.OK)//如果接收失败
                        {
                            message = "生成缴款书支付二维码URL连接失败，请联系管理员!";
                        }
                        else
                        {
                            string suceessMsg = responseData.Content.ReadAsStringAsync().Result;
                            JObject StrJson = (JObject)JsonConvert.DeserializeObject(suceessMsg);
                            string data = SysUtility.FromBase64Str(StrJson["data"].ToString());
                            JObject resultJson = (JObject)JsonConvert.DeserializeObject(data);
                            string result = resultJson["result"].ToString();
                            string StrMessage = SysUtility.FromBase64Str(resultJson["message"].ToString());
                            JObject JsonUrl = (JObject)JsonConvert.DeserializeObject(StrMessage);

                            if (result == "S0000")
                            {
                                string PayUrl = JsonUrl["qrCode"].ToString();
                                _logger.LogDebug("支付二维码URL:" + PayUrl);
                                if (!string.IsNullOrEmpty(PayUrl))
                                {
                                    var payData = await base.Db.Queryable<PAYBOOKRESULT>().Where(i => i.BUSNO == slbh && (i.ISPAY == null || i.BANKISPAY == null)).ToListAsync();
                                    if (payData.Count > 0)
                                    {
                                        foreach (var item in payData)
                                        {
                                            item.PAYURL = PayUrl;
                                            var count = _payBookResultRepository.Update(item);
                                        }
                                    }
                                    message = PayUrl;
                                }
                                else
                                {
                                    //获取支付URL失败
                                    message = "400";
                                }
                            }
                            else
                            {
                                message = "网络连接错误，请联系管理员！";
                                _logger.LogDebug("电子缴款书返回错误结果:" + SysUtility.FromBase64Str(resultJson["message"].ToString()));
                            }
                        }
                    }
                }
                catch (Exception ex )
                {
                    _logger.LogDebug("电子缴款书连接错误:" + ex.ToString());
                    throw ex;
                }
                
            }            
            else
            {
                //未输入受理编号
                message = "500";
            }
            return message;
        }
        /// <summary>
        /// 根据缴款码获取缴款情况
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <param name="userName">操作人</param>
        /// <returns></returns>
        public async Task<string> GetPayResult(string slbh,string userName)
        {
            //slbh = "202112300204";
            string message = "";
            string Param = "";
            if (!string.IsNullOrEmpty(GetPayCode(slbh)))
            {
                Param = strParam(GetPayCode(slbh));
                _logger.LogDebug("获取缴款情况请求参数:" + Param);
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(Param, Encoding.UTF8, "application/json");
                    var responseData = client.PostAsync(this.getPayBookConfirmResult_URL, content).Result;//得到返回字符流
                    if (responseData.StatusCode != HttpStatusCode.OK)//如果接收失败
                    {
                        message = "获取缴款情况连接失败，请联系管理员!";
                    }
                    else
                    {
                        string suceessMsg = responseData.Content.ReadAsStringAsync().Result;
                        JObject StrJson = (JObject)JsonConvert.DeserializeObject(suceessMsg);
                        string data = SysUtility.FromBase64Str(StrJson["data"].ToString());
                        JObject resultJson = (JObject)JsonConvert.DeserializeObject(data);
                        string result = resultJson["result"].ToString();
                        string StrMessage = SysUtility.FromBase64Str(resultJson["message"].ToString());
                        _logger.LogDebug(slbh + "获取缴款情况返回值:" + StrMessage);
                        if (result == "S0000")
                        {
                            JObject JsonMessage = (JObject)JsonConvert.DeserializeObject(StrMessage);
                            string strPaymentChannel = JsonMessage["paymentChannel"].ToString();
                            if(!string.IsNullOrEmpty(strPaymentChannel))
                            {
                                if(UpdateSfzt(slbh,userName) == "200")
                                {
                                    message = "200";
                                }
                                else
                                {
                                    message = "400";
                                }
                            }
                            else
                            {
                                //未查询到缴费信息
                                message = "400";
                            }
                        }
                        else
                        {
                            message = "网络连接错误，请联系管理员！";
                            _logger.LogDebug(slbh + "获取缴款情况返回错误结果:" + SysUtility.FromBase64Str(resultJson["message"].ToString()));
                        }
                    }
                }
            }
            return message;
        }

        /// <summary>
        /// 根据缴款码获取电子票据信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        public async Task<string> GetGenerateTicket(string slbh)
        {
            //slbh = "202112300204";
            string message = string.Empty;
            string Param = string.Empty;
            if (!string.IsNullOrEmpty(GetPayCode(slbh)))
            {
                Param = strParam(GetPayCode(slbh));
                _logger.LogDebug("获取电子票据信息请求参数:" + Param);
                using (HttpClient client = new HttpClient())
                {
                    HttpContent content = new StringContent(Param, Encoding.UTF8, "application/json");
                    var responseData = client.PostAsync(this.getgetEBillByPayCode_URL, content).Result;//得到返回字符流
                    if (responseData.StatusCode != HttpStatusCode.OK)//如果接收失败
                    {
                        message = "获取电子票据信息连接失败，请联系管理员!";
                    }
                    else
                    {
                        string suceessMsg = responseData.Content.ReadAsStringAsync().Result;
                        JObject StrJson = (JObject)JsonConvert.DeserializeObject(suceessMsg);
                        string data = SysUtility.FromBase64Str(StrJson["data"].ToString());
                        JObject resultJson = (JObject)JsonConvert.DeserializeObject(data);
                        string result = resultJson["result"].ToString();
                        string StrMessage = SysUtility.FromBase64Str(resultJson["message"].ToString());
                        _logger.LogDebug(slbh + "获取电子票据信息返回值:" + StrMessage);
                        if (result == "S0000")
                        {
                            JObject JsonMessage = (JObject)JsonConvert.DeserializeObject(StrMessage);
                            string pictureUrl = JsonMessage["pictureUrl"].ToString();
                            if (!string.IsNullOrEmpty(pictureUrl))
                            {
                                var ResultData = await base.Db.Queryable<PAYBOOKRESULT>().Where(i => i.BUSNO == slbh && (i.ISPAY != null || i.BANKISPAY != null)).ToListAsync();
                                if(ResultData.Count > 0)
                                {
                                    ResultData[0].PICTUREURL = pictureUrl;
                                    var count = _payBookResultRepository.Update(ResultData[0]).Result;
                                    //获取电子票据成功
                                    message = "200";
                                }
                                else
                                {
                                    //未获取到有效信息
                                    message = "400";
                                    _logger.LogDebug(slbh + "未获取到有效信息:" + StrMessage);
                                }
                            }
                            else
                            {
                                //获取电子票据失败
                                message = "400";
                                _logger.LogDebug(slbh + "获取电子票据信息返回值:" + StrMessage);
                            }
                        }
                        else
                        {
                            message = "网络连接错误，请联系管理员！";
                            _logger.LogDebug(slbh + "获取缴款情况返回错误结果:" + SysUtility.FromBase64Str(resultJson["message"].ToString()));
                        }
                    }
                }
            }
            else
            {
                message = "请输入受理编号！";
            }
            return message;
        }

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="data">缴款码</param>
        /// <returns></returns>
        public string strParam(string data)
        {
            PayParamVModel payParamVModel = new PayParamVModel();
            PayCodeVModel model = new PayCodeVModel();
            model.payCode = data;

            //毫秒级时间戳
            payParamVModel.noise = Convert.ToInt64((DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalMilliseconds).ToString();
            //string a = JsonConvert.SerializeObject(model);
            payParamVModel.data = SysUtility.ToBase64Str(JsonConvert.SerializeObject(model));
            string fitst = "region=" + payParamVModel.region + "&deptcode=" + payParamVModel.deptcode +
                "&appid=" + payParamVModel.appid + "&data=" + payParamVModel.data + "&noise=" + payParamVModel.noise;
            string second = fitst + "&key=0d7e2003c6c4c402eadd50bd44&version=1.0";
            payParamVModel.sign = EncryptHelper.Md5Method(second).ToUpper();

            //最终整合后的参数
            string GenerateParam = JsonConvert.SerializeObject(payParamVModel);
            return GenerateParam;
        }

        /// <summary>
        /// 获取缴款码
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns>缴款码</returns>
        public string GetPayCode(string slbh)
        {
            string payCode = "";
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var payData = base.Db.Queryable<PAYBOOKRESULT>().Where(i => i.BUSNO == slbh).ToList();
            if (payData.Count > 0)
            {
                payCode = payData[0].PAYCODE;
            }
            return payCode;
        }
        /// <summary>
        /// 更新缴费状态
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <param name="userName">操作员</param>
        /// <returns></returns>
        public string UpdateSfzt(string slbh,string userName)
        {
            string mess = "";
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var payData = base.Db.Queryable<PAYBOOKRESULT>().Where(i => i.BUSNO == slbh && i.ISPAY == null).ToList();
            base.ChangeDB(SysConst.DB_CON_BDC);
            var sfdData = base.Db.Queryable<DJ_SFD>().Where(i => i.SLBH == slbh && (i.SFZT == null && i.FSJFZT == null)).ToList();

            if(payData.Count > 0 && sfdData.Count > 0)
            {
                payData[0].PAYDATE = DateTime.Now;
                payData[0].ISPAY = "1";

                sfdData[0].SFZT = "1";
                sfdData[0].FSJFZT = "1";
                sfdData[0].DYR = userName;
                sfdData[0].DYSJ = DateTime.Now;
                sfdData[0].HSJE = sfdData[0].YSJE;            
                var lysxkCount = _iDJ_SFDRepository.Update(sfdData[0]).Result;
                var iirsCount = _payBookResultRepository.Update(payData[0]).Result;
                if (iirsCount && lysxkCount)
                {
                    mess = "200";
                }
                else
                {
                    mess = "400";
                }
            }
            
            return mess;
        }

        /// <summary>
        /// 何雨檬调取财政接口
        /// </summary>
        /// <returns></returns>
        public int SubmitPayHome()
        {
            int i = 0;

            return i;
        }


    }
}
