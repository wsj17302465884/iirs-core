using IIRS.IRepository.Base;
using IIRS.IRepository.LYSXK209;
using IIRS.IServices.WQ;
using IIRS.Models.EntityModel.LYSXK209;
using IIRS.Models.ViewModel.WQ;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;

namespace IIRS.Gov.Services.Law
{
    public class WQServices : BaseServices, IWQServices
    {
        private readonly ILogger<WQServices> _logger;
        private readonly IDBTransManagement _dbTransManagement;
        private readonly IWQ_LOGRepository _iWQ_LOGRepository;
        private readonly IV_BDCZJK_WQ_LSRepository _BDCZJK_WQ_LSRepository;
        public WQServices(IDBTransManagement dbTransManagement, ILogger<WQServices> logger, IWQ_LOGRepository wQ_LOGRepository, IV_BDCZJK_WQ_LSRepository v_BDCZJK_WQ_LSRepository) : base(dbTransManagement)
        {
            _logger = logger;
            _dbTransManagement = dbTransManagement;
            _iWQ_LOGRepository = wQ_LOGRepository;
            _BDCZJK_WQ_LSRepository = v_BDCZJK_WQ_LSRepository;

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
        public MessageResult GetResult(string xm, string sfzh, string htbh, string cxrxm, string cxrzjhm, string dw)
        {
            base.ChangeDB(SysConst.DB_CON_LYSXK209);
            try
            {
                var eModelLists = this._BDCZJK_WQ_LSRepository.Query(A => A.HTBH == htbh).Result;
                WQ_LOG msg_log = new WQ_LOG()
                {
                    PK = Guid.NewGuid().ToString("N"),
                    cdate = DateTime.Now,
                    CXRXM = cxrxm,
                    CXRZJHM = cxrzjhm,
                    DW = dw,
                };
                if (eModelLists.Count == 0)
                {
                    msg_log.EX_MSG = "合同编号:" + htbh + "未查询到信息";
                    _iWQ_LOGRepository.Add(msg_log);
                    return new MessageResult()
                    {
                        code = "400",
                        message = "合同编号:" + htbh + "未查询到信息"
                    };
                }
                bool is_ok = false;
                var buyers = string.Empty;
                var buyerzjhms = string.Empty;
                var BUYERXZ = string.Empty;
                var BUYERHJXZQH = string.Empty;
                var BUYERHJ = string.Empty;
                var seller = string.Empty;
                var sellerzjhm = string.Empty;
                var SELLERXZ = string.Empty;
                var SELLERHJXZQH = string.Empty;
                var SELLERHJ = string.Empty;
                V_BDCZJK_WQ_LS model = new V_BDCZJK_WQ_LS();
                foreach (var item in eModelLists)
                {
                    if (item.JYZQC == xm && item.JYZZJHM == sfzh)
                    {
                        is_ok = true;
                    }
                    if (item.JYZLB == "受让方")
                    {
                        buyers += item.JYZQC + ",";
                        buyerzjhms += item.JYZZJHM + ",";
                        BUYERXZ += item.JYZXZ + ",";
                        //BUYERHJXZQH += item.JYZHJXZQH + ",";
                        BUYERHJ += item.JYZHJ + ",";
                    }
                    else if (item.JYZLB == "出让人")
                    {
                        seller = item.JYZQC;
                        sellerzjhm = item.JYZZJHM;
                        SELLERXZ = item.JYZXZ;
                        //SELLERHJXZQH = item.JYZHJXZQH;
                        SELLERHJ = item.JYZHJ;
                        model = item;
                    }

                }
                if (is_ok)
                {
                    WQResult messageResult = new WQResult()

                    {
                        HTBH = model.HTBH,
                        YWBH = model.YWBH,
                        LLYWLB = model.LLYWLB,
                        SJYWLB = model.SJYWLB,
                        //房屋信息
                        //FWBM = model.FWBM,
                        FWZT = model.FWZT,
                        //XZQHDM = model.XZQHDM,
                        QX = model.QX,
                        XZJDB = model.XZJDB,
                        LJX = model.LJX,
                        XQ = model.XQ,
                        LH = model.LH,
                        SZQSC = model.SZQSC,
                        SZZZC = model.SZZZC,
                        MYC = model.MYC,
                        DY = model.DY,
                        FH = model.FH,
                        FWZL = model.FWZL,
                        HXJS = model.HXJS,
                        HXJG = model.HXJG,
                        FWCX = model.FWCX,
                        JZMJ = model.JZMJ,
                        TNJZMJ = model.TNJZMJ,
                        GTJZMJ = model.GTJZMJ,
                        JZJG = model.JZJG,
                        FWYT = model.FWYT,
                        FWXZ = model.FWXZ,
                        //FWXZBM = model.FWXZBM,
                        GYFS = model.GYFS,
                        CJJE = model.CJJE,
                        DJ= model.DJ,
                        FKLX = model.FKLX,
                        DKFS = model.DKFS,
                        HTSXRQ = model.HTSXRQ,
                        YWBJSJ = model.YWBJSJ,
                        //YWBLSZXZQHDM = model.YWBLSZXZQHDM,
                        GJ = model.GJ,
                        BDCDYH = model.BDCDYH,
                        LXDH = model.LXDH,
                        DZ =model.DZ,
                        FWLX = model.FWLX,
                        SZFE =model.SZFE,
                        //买卖双方信息
                        BUYERMC =buyers.Substring(0,buyers.Length-1),
                        BUYZJHM=buyerzjhms.Substring(0,buyerzjhms.Length-1),
                        BUYERXZ = BUYERXZ.Substring(0,BUYERXZ.Length-1),
                        BUYERHJ = BUYERHJ.Substring(0,BUYERHJ.Length-1),
                        //BUYERHJXZQH = BUYERHJXZQH.Substring(0,BUYERHJXZQH.Length-1),
                        SELLERMC = seller,
                        SELLERZJHM = sellerzjhm,
                        SELLERXZ = SELLERXZ,
                        //SELLERHJXZQH = SELLERHJXZQH,
                        SELLERHJ = SELLERHJ
            
                    };
                    MessageResult result =   new MessageResult()
                    {
                        code = "200",
                        message = "查询成功",
                        data = messageResult

                    };
                    var jsonData = JsonConvert.SerializeObject(result);
                    msg_log.MSG = jsonData;
                    _iWQ_LOGRepository.Add(msg_log);
                    return result;
                }
                else
                {
                    msg_log.EX_MSG = "输入姓名：" + xm + "身份证号：" + sfzh + "未匹配合同编号" + htbh;
                    _iWQ_LOGRepository.Add(msg_log);
                    return new MessageResult()
                    {
                        code = "400",
                        message = "输入姓名：" + xm + "身份证号：" + sfzh + "未匹配合同编号"
                    };

                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);

                return new MessageResult()
                {
                    code = "500",
                    message = "程序错误" + ex.Message
                };
            }
           
           
        }
    }
}
