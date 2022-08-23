using IIRS.Models.EntityModel.BDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.TraceBack
{
    public class TraceBackVModel
    {
        public TraceBackVModel()
        {

        }

        public DJ_SJD DJ_SJDMOdel { get; set; } = new DJ_SJD();
        public List<xgzhModel> xgzhList { get; set; } = new List<xgzhModel>();
        public List<DJ_QLRGL> dyrList { get; set; } = new List<DJ_QLRGL>();
        public List<DJ_QLRGL> dyqrList { get; set; } = new List<DJ_QLRGL>();
        public DJ_XGDJZX DJ_XGDJZXModel { get; set; } = new DJ_XGDJZX();
        public List<DJ_QLRGL> qlrList { get; set; } = new List<DJ_QLRGL>();
        public List<DJ_QLRGL> ywrrList { get; set; } = new List<DJ_QLRGL>();
        public List<djbModel> djbList { get; set; } = new List<djbModel>();
        public DJ_DY DJ_DYModel { get; set; } = new DJ_DY();
        public List<cfModel> cfList { get; set; } = new List<cfModel>();
        public List<yyModel> yyList { get; set; } = new List<yyModel>();
        public List<ygModel> ygList { get; set; } = new List<ygModel>();
        public List<dyModel> dyList { get; set; } = new List<dyModel>();
        public DJ_CF DJ_CFModel { get; set; } = new DJ_CF();
        public DJ_YG DJ_YGModel { get; set; } = new DJ_YG();
        public DJ_YY DJ_YYModel { get; set; } = new DJ_YY();
        public List<DJ_SPB> spbList { get; set; } = new List<DJ_SPB>();
    }

    public class xgzhModel
    {
        public string xgzh { get; set; }
        public string xgzlx { get; set; }
        public string qlrmc { get; set; }
        public string zl { get; set; }
        public string bdcdyh { get; set; }
        public string tdyt { get; set; }
        public string tdyt_zwm { get; set; }
        public string tdlx { get; set; }
        public string tdlx_zwm { get; set; }
        public string tdxz { get; set; }
        public string tdxz_zwm { get; set; }
        public decimal? gytdmj { get; set; }
        public decimal? dytdmj { get; set; }
        public decimal? fttdmj { get; set; }
        public decimal? jzzdmj { get; set; }
        public string syqx { get; set; }
        public DateTime? qsrq { get; set; }
        public DateTime? zzrq { get; set; }
        public string qdfs { get; set; }
        public decimal? pgje { get; set; }
        public decimal? qdjg { get; set; }
        public string fwyt { get; set; }
        public string fwyt_zwm { get; set; }
        public string fwlx { get; set; }
        public string fwlx_zwm { get; set; }
        public string fwxz { get; set; }
        public string fwxz_zwm { get; set; }
        public decimal? jzmj { get; set; }
        public decimal? tnjzmj { get; set; }
        public decimal? ftjzmj { get; set; }
        public string qt { get; set; }
        public string fj { get; set; }

    }

    public class djbModel
    {
        public string slbh { get; set; }
        public string bdczh { get; set; }
        public string bdcdyh { get; set; }
    }
    public class dyModel
    {
        public string slbh { get; set; }
        public string bdczh { get; set; }
        public string bdcdyh { get; set; }
    }
    public class cfModel
    {
        public string xgzh { get; set; }
        public string bdcdyh { get; set; }
        public string zl { get; set; }
        public string qlrmc { get; set; }
        public string zjhm { get; set; }

        public string cflx { get; set; }
        public string cffw { get; set; }
        public string fj { get; set; }
    }
    public class ygModel
    {
        public string SLBH { get; set; }
        public string DJZL { get; set; }
        public string DJDL { get; set; }
        public string YGZL { get; set; }
        public string CFLX { get; set; }
        public string DJYY { get; set; }
        public string BDCZH { get; set; }
        public string BDCDYH { get; set; }
        public DateTime? DJRQ { get; set; }
        public string ZL { get; set; }
        public Decimal? MJ { get; set; }
        public string QLLX { get; set; }
        public string QLXZ { get; set; }
        public string YT { get; set; }
        public string SYQX { get; set; }
        public string QLQTQK { get; set; }
        public string FJ { get; set; }
        public string ZSBH { get; set; }
        public string GLGSD { get; set; }
        public string ZSLX { get; set; }
        public DateTime CDATE { get; set; }
    }
    public class yyModel
    {
        public string SLBH { get; set; }
        public string DJLZ { get; set; }
        public string DJDL { get; set; }
        public string CFLX { get; set; }
        public string YYYY { get; set; }
        public string CFWH { get; set; }
        public string BDCDYH { get; set; }
        public DateTime? DJRQ { get; set; }
        public string SQSX { get; set; }
        public string ZL { get; set; }
        public DateTime? SQSJ { get; set; }
        public string FJ { get; set; }
        public string GLGSD { get; set; }
        public DateTime CDATE { get; set; }
    }
}
