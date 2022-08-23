using IIRS.IRepository;
using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.IRepository.BDC;
using IIRS.IRepository.IIRS;
using IIRS.IServices;
using IIRS.IServices.Bank;
using IIRS.Models.EntityModel.BANK;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BANK;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using RT.Comb;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services.Bank
{
    public class BankServices : BaseServices, IBankServices
    {
        private readonly ILogger<IBankServices> _logger;

        IDBTransManagement _dbTransManagement;

        private readonly ISerialNumberRepository _serialNumber;

        private readonly IBankUserInfoRepository _bankUserInfo;

        private readonly IBusinessRequestRepository _businessRequest;

        private readonly ICertificationRepository _certificationRepository;

        private readonly IDYServices _dYServices;

        private readonly IBankAuthorizeRepository _bankAuthorizeRepository;

        private readonly IOrderHouseAssociationRepository _orderHouseAssociationRepository;

        private readonly IBankRelatedIIRSRepository _bankRelatedIIRSRepository;

        private readonly IDO_ACTIONRepository _dO_ACTIONRepository;

        private readonly IQlrgl_infoRepository _qlrgl_InfoRepository;

        private readonly IRegistration_infoRepository _registration_InfoRepository;

        private readonly ITsgl_infoRepository _tsgl_InfoRepository;

        private readonly IXgdjgl_infoRepository _xgdjgl_InfoRepository;

        private readonly IXgdjzx_infoRepository _xgdjzx_InfoRepository;

        private readonly IBRWR_INFRepository _jkrxxRepository;

        private readonly IBNK_HDL_AGNC_PSN_INFRepository _yhjbdlrRepository;

        private readonly IMRTG_PSN_INFRepository _dyrxxRepository;

        private readonly IATCHRepository _fjRepository;

        private readonly IMRTG_REALEST_UNIT_INFRepository _dybdcdyxxRepository;

        private readonly IDYZXSQRepository _dyzxsqRepository;

        private readonly IHouseStatusRepository _houseStatusRepository;

        private readonly ITsglRepository _tsglRepository;

        private readonly IDJ_CFRepository _CFRepository;
        public BankServices(IDBTransManagement dbTransManagement, ILogger<IBankServices> logger, ISerialNumberRepository serialNumber, IBankUserInfoRepository bankUserInfo, IBusinessRequestRepository businessRequest, ICertificationRepository certificationRepository, IDYServices dYServices, IOrderHouseAssociationRepository orderHouseAssociationRepository, IBankAuthorizeRepository bankAuthorizeRepository, IBankRelatedIIRSRepository bankRelatedIIRSRepository, IDO_ACTIONRepository dO_ACTIONRepository, IQlrgl_infoRepository qlrgl_InfoRepository, IRegistration_infoRepository registration_InfoRepository, ITsgl_infoRepository tsgl_InfoRepository, IXgdjgl_infoRepository xgdjgl_InfoRepository, IXgdjzx_infoRepository xgdjzx_InfoRepository, IBRWR_INFRepository jkrxxRepository, IBNK_HDL_AGNC_PSN_INFRepository yhjbdlrRepository, IMRTG_PSN_INFRepository dyrxxRepository, IATCHRepository fjRepository, IMRTG_REALEST_UNIT_INFRepository dybdcdyxxRepository, IDYZXSQRepository dyzxsqRepository, IHouseStatusRepository houseStatusRepository, ITsglRepository tsglRepository, IDJ_CFRepository CFRepository) : base(dbTransManagement)
        {
            this._logger = logger;
            this._dbTransManagement = dbTransManagement;
            _serialNumber = serialNumber;
            _bankUserInfo = bankUserInfo;
            _businessRequest = businessRequest;
            _certificationRepository = certificationRepository;
            _dYServices = dYServices;
            _dO_ACTIONRepository = dO_ACTIONRepository;
            _orderHouseAssociationRepository = orderHouseAssociationRepository;
            _bankAuthorizeRepository = bankAuthorizeRepository;
            _bankRelatedIIRSRepository = bankRelatedIIRSRepository;
            _qlrgl_InfoRepository = qlrgl_InfoRepository;
            _registration_InfoRepository = registration_InfoRepository;
            _tsgl_InfoRepository = tsgl_InfoRepository;
            _xgdjgl_InfoRepository = xgdjgl_InfoRepository;
            _xgdjzx_InfoRepository = xgdjzx_InfoRepository;
            _jkrxxRepository = jkrxxRepository;
            _yhjbdlrRepository = yhjbdlrRepository;
            _dyrxxRepository = dyrxxRepository;
            _fjRepository = fjRepository;
            _dybdcdyxxRepository = dybdcdyxxRepository;
            _dyzxsqRepository = dyzxsqRepository;
            _houseStatusRepository = houseStatusRepository;
            _tsglRepository = tsglRepository;
            _CFRepository = CFRepository;
        }

        public async Task<MrgeReleaseVModel> GetMortgageInfo(string BDCZMH)
        {
            MrgeReleaseVModel model = new MrgeReleaseVModel();
            await Task.CompletedTask;
            return model;
//            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
//            var sysDicList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 1)).ToListAsync();
//            var sysQllxList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 4)).ToListAsync();    //房屋权利类型
//            var sysQlxzList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 3)).ToListAsync();    //房屋权利性质
//            var systdQllxList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 6)).ToListAsync();    //房屋权利类型
//            var systdQlxzList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 7)).ToListAsync();    //房屋权利性质

            //            //sysDicList.Cast<SYS_DIC>().Select(s=>new { DEFINED_CODE=s.DEFINED_CODE, DNAME=s.DNAME }).ToDictionary<>

            //            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_BDC);
            //            MrgeReleaseVModel dyData = new MrgeReleaseVModel();
            //            #region 抵押房屋信息查询            
            //            var resultZS = await base.Db.Queryable<DJ_TSGL, DJ_DY, DJ_QLRGL, DJ_SJD>((TS, DY, R, S)
            //    => new object[] { JoinType.Inner, TS.SLBH == DY.SLBH, JoinType.Inner, DY.SLBH == R.SLBH, JoinType.Inner, DY.SLBH == S.SLBH })
            //.Where((TS, DY, R, S) => ((TS.LIFECYCLE == null || TS.LIFECYCLE == 0) && (DY.LIFECYCLE == null || DY.LIFECYCLE == 0) && R.QLRLX == "抵押人") && DY.BDCZMH == BDCZMH)
            //.GroupBy((TS, DY, R, S) => new { BDCDYH = DY.BDCDYH, BDCZMH = DY.BDCZMH, SLBH = DY.SLBH, ZL = S.ZL })
            //.Select((TS, DY, R, S) => new
            //{
            //    BDCZMH = DY.BDCZMH,
            //    ZL = S.ZL,
            //    BDCDYH = DY.BDCDYH,
            //    SLBH = DY.SLBH,
            //    DYR = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(R.QLRMC))"),
            //    TSTYBMS = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(TS.TSTYBM))")
            //}).ToListAsync();
            //            if (resultZS.Count == 0)
            //            {
            //                throw new ApplicationException("未查询到该受理编号");
            //            }

            //            var houseRoot = new MrgeReleaseHouseVModel()
            //            {
            //                BDCZH = resultZS[0].BDCZMH,
            //                SLBH = resultZS[0].SLBH,
            //                BDCDYH = resultZS[0].BDCDYH,
            //                ZSLX = "房屋抵押证明",
            //                ZL = resultZS[0].ZL,
            //                QLRMC = resultZS[0].DYR,
            //                hasChildren = true
            //            };
            //            string[] BDCZ_TSTYBM = resultZS[0].TSTYBMS.Split(new char[] { ',' });
            //            //}

            //            dyData.selectHouse = houseRoot;
            //            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //            //{
            //            //    _logger.LogDebug(sql);
            //            //};
            //            var resultHouse = await base.Db.Queryable<DJ_TSGL, DJ_DJB, DJ_QLRGL, DJ_XGDJGL, DJ_SJD, QL_FWXG, QL_TDXG>((TS, DJB, R, Z, S, FW, TD)
            //      => new object[] { JoinType.Inner, TS.SLBH == DJB.SLBH, JoinType.Inner, DJB.SLBH == R.SLBH, JoinType.Inner, DJB.SLBH == Z.FSLBH, JoinType.Inner, DJB.SLBH == S.SLBH, JoinType.Left, DJB.SLBH == FW.SLBH, JoinType.Left, DJB.SLBH == TD.SLBH })
            //.Where((TS, DJB, R, Z, S, FW, TD) => ((TS.LIFECYCLE == null || TS.LIFECYCLE == 0) && (DJB.LIFECYCLE == null || DJB.LIFECYCLE == 0) && R.QLRLX == "权利人") && BDCZ_TSTYBM.Contains(TS.TSTYBM))
            //.GroupBy((TS, DJB, R, Z, S, FW, TD) => new { BDCDYH = TS.BDCDYH, BDCZMH = DJB.BDCZH, TSTYBM = TS.TSTYBM, XGZLX = Z.XGZLX, ZL = S.ZL, FSXBH = S.SLBH, BDCLX = TS.BDCLX, QLLX = FW.QLLX, QLXZ = FW.QLXZ, JZMJ = FW.JZMJ, TDQLLX = TD.QLLX, TDQLXZ = TD.QLXZ, TDMJ = TD.DYTDMJ })
            //.Select((TS, DJB, R, Z, S, FW, TD) => new
            //{
            //    BDCZMH = DJB.BDCZH,
            //    BDCDYH = TS.BDCDYH,
            //    BDCLX = TS.BDCLX,
            //    TSTYBM = TS.TSTYBM,
            //    XGZLX = Z.XGZLX,
            //    FSXBH = S.SLBH,
            //    ZL = S.ZL,
            //    DYR = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(R.QLRMC))"),
            //    A = SqlFunc.AggregateMax(TS.TSTYBM),
            //    QLLX = FW.QLLX,
            //    QLXZ = FW.QLXZ,
            //    JZMJ = FW.JZMJ,
            //    TDQLLX = TD.QLLX,
            //    TDQLXZ = TD.QLXZ,
            //    TDMJ = TD.DYTDMJ
            //}).ToListAsync();

            //            foreach (var h in resultHouse)
            //            {
            //                var qllx_zwmObj = sysQllxList.Where(s => s.DEFINED_CODE == h.QLLX).FirstOrDefault();
            //                var qlxz_zwmObj = sysQlxzList.Where(s => s.DEFINED_CODE == h.QLXZ).FirstOrDefault();
            //                var tdqllx_zwmObj = systdQllxList.Where(s => s.DEFINED_CODE == h.TDQLLX).FirstOrDefault();
            //                var tdqlxz_zwmObj = systdQlxzList.Where(s => s.DEFINED_CODE == h.TDQLXZ).FirstOrDefault();
            //                houseRoot.children.Add(new MrgeReleaseHouseVModel()
            //                {
            //                    BDCZH = h.BDCZMH,
            //                    ZSLX = h.XGZLX,
            //                    SLBH = h.FSXBH,
            //                    ZL = h.ZL,
            //                    TSTYBM = h.TSTYBM,
            //                    BDCDYH = h.BDCDYH,
            //                    QLRMC = h.DYR,
            //                    BDCLX = h.BDCLX,
            //                    qllx = qllx_zwmObj != null ? qllx_zwmObj.DNAME : string.Empty,
            //                    qlxz = qlxz_zwmObj != null ? qlxz_zwmObj.DNAME : string.Empty,
            //                    jzmj = h.JZMJ,
            //                    tdqllx = tdqllx_zwmObj != null ? tdqllx_zwmObj.DNAME : string.Empty,
            //                    tdqlxz = tdqlxz_zwmObj != null ? tdqlxz_zwmObj.DNAME : string.Empty,
            //                    tdmj = h.TDMJ
            //                });
            //            }


            //            #endregion
            //            #region 权利人信息查询
            //            var resultMrge = await base.Db.Queryable<DJ_TSGL, DJ_DY, DJ_SJD>((TS, DY, S) => new object[] { JoinType.Inner, TS.SLBH == DY.SLBH, JoinType.Inner, TS.SLBH == S.SLBH })
            //.Where((TS, DY, S) => ((TS.LIFECYCLE == null || TS.LIFECYCLE == 0) && (DY.LIFECYCLE == null || DY.LIFECYCLE == 0) && BDCZ_TSTYBM.Contains(TS.TSTYBM)))
            //.Select((TS, DY, S) => new
            //{
            //    XGZH = DY.XGZH,
            //    ZL = S.ZL,
            //    DYLX = DY.DYLX,
            //    DYSW = DY.DYSW,
            //    DYFS = DY.DYFS,
            //    DYMJ = DY.DYMJ,
            //    BDBZZQSE = DY.BDBZZQSE,
            //    PGJE = DY.PGJE,
            //    HTH = DY.HTH,
            //    FJ = DY.FJ,
            //    QLQSSJ = DY.QLQSSJ,
            //    QLJSSJ = DY.QLJSSJ,
            //    DYQX = DY.DYQX
            //}).FirstAsync();
            //            dyData.ZL = resultMrge.ZL;
            //            dyData.DYLX = resultMrge.DYLX;
            //            dyData.DYSW = Convert.ToInt32(resultMrge.DYSW);
            //            dyData.DYFS = resultMrge.DYFS;
            //            dyData.dyMJ = resultMrge.DYMJ ?? 0;
            //            dyData.PGJE = resultMrge.PGJE ?? 0;
            //            dyData.HTH = resultMrge.HTH;
            //            dyData.BZ = resultMrge.FJ;
            //            dyData.LXQX = resultMrge.DYQX;
            //            if (resultMrge.BDBZZQSE != null)
            //            {
            //                dyData.BDBZQSE = Convert.ToInt32(resultMrge.BDBZZQSE);
            //            }
            //            if (resultMrge.QLQSSJ != null)
            //            {
            //                dyData.ZWLXQXQSRQ = resultMrge.QLQSSJ ?? DateTime.Now;
            //            }
            //            if (resultMrge.QLJSSJ != null)
            //            {
            //                dyData.ZWLXQXJZRQ = resultMrge.QLQSSJ ?? DateTime.Now;
            //            }
            //            if (resultMrge.QLQSSJ != null || resultMrge.QLJSSJ != null)
            //            {
            //                dyData.LXZWQX_STR = $"{resultMrge.QLQSSJ:yyyy-MM-dd} - {resultMrge.QLJSSJ:yyyy-MM-dd}";
            //            }

            //            var resultQLR = await base.Db.Queryable<DJ_TSGL, DJ_QLRGL>((TS, R) => new object[] { JoinType.Inner, TS.SLBH == R.SLBH })
            //.Where((TS, R) => (R.QLRLX == "抵押人" && (TS.LIFECYCLE == null || TS.LIFECYCLE == 0) && (R.LIFECYCLE == null || R.LIFECYCLE == 0) && BDCZ_TSTYBM.Contains(TS.TSTYBM)))
            //.Select((TS, R) => new
            //{
            //    GLBM = R.GLBM,
            //    SLBH = R.SLBH,
            //    YWBM = R.YWBM,
            //    ZJHM = R.ZJHM,
            //    QLRID = R.QLRID,
            //    QLRMC = R.QLRMC,
            //    ZJLB = R.ZJLB,
            //    DH = R.DH,
            //    QLRLX = R.QLRLX,
            //    SXH = R.SXH,
            //    GYFE = R.GYFE
            //}).OrderBy("R.SXH").Distinct().ToListAsync();
            //            foreach (var qlr in resultQLR)
            //            {
            //                var zjlb_zwmObj = sysDicList.Where(s => s.DEFINED_CODE == qlr.ZJLB).FirstOrDefault();
            //                dyData.selectPerson.Add(new DyPersonVModel()
            //                {
            //                    DH = qlr.DH,
            //                    GYFE = qlr.GYFE,
            //                    QLRID = qlr.QLRID,
            //                    QLRMC = qlr.QLRMC,
            //                    SXH = Convert.ToInt32(qlr.SXH),
            //                    ZJHM = qlr.ZJHM,
            //                    ZJLB = qlr.ZJLB,
            //                    ZJLB_ZWM = zjlb_zwmObj != null ? zjlb_zwmObj.DNAME : string.Empty,
            //                });
            //            }
            //            #endregion
            //            return dyData;
        }

        public async Task<string> GetDyzxsq(DYZXSQ model, BRWR_INF jkrModel, BNK_HDL_AGNC_PSN_INF yhjbdlrModel, ATCH fjModel, MRTG_PSN_INF dyrModel, MRTG_REALEST_UNIT_INF bdcdyqkModel)
        {
            await Task.CompletedTask;
            return "";
            //string slbh = "";
            //string message = "";
            //string xid = Provider.Sql.Create().ToString();//主键
            //string newSlbh = _dYServices.GetSLBH();
            //DateTime saveTime = DateTime.Now;
            //BANKRELATEDIIRS bankModel = new BANKRELATEDIIRS();
            //List<OrderHouseAssociation> HouseList = new List<OrderHouseAssociation>();
            //List<TSGL_INFO> tsglList = new List<TSGL_INFO>();
            //List<XGDJGL_INFO> xgdjglList = new List<XGDJGL_INFO>();
            //List<QLRGL_INFO> qlrglList = new List<QLRGL_INFO>();
            //BankAuthorize bankAuthorize = new BankAuthorize();
            //BUSINESS_REQUEST resultModel = new BUSINESS_REQUEST();
            //bankAuthorize.BID = Provider.Sql.Create().ToString();
            //bool isOk = false;
            //var data = await _certificationRepository.Query(i => i.bdczmh.Contains(model.REALEST_ECB_NO) && i.Dyr.Contains(dyrModel.MRTG_PSN_NM) && i.Dyqr.Contains(model.MRTG_AGNC_PSN_NM));
            //var HouseData = await GetMortgageInfo(bdcdyqkModel.REALEST_RGSCTF_NO);

            //#region 验证流水是否有效
            //var serialData = await _serialNumber.Query(i => i.SERIALNUMBER == model.SRCSYS);
            //if (serialData.Count > 0)
            //{
            //    resultModel.SID = Guid.NewGuid();
            //    resultModel.SERIALNUMBER = serialData[0].SERIALNUMBER;
            //    var BUSINESS_TYPE = resultModel.BUSINESS_TYPE == "42" ? "抵押注销登记" : "预告注销登记";
            //    resultModel.BUSINESS_TYPE = BUSINESS_TYPE;
            //    resultModel.REQUESTDATE = DateTime.Now;
            //    if (data.Count > 0)
            //    {
            //        resultModel.ZT = 0;
            //        resultModel.SLBH = data[0].slbh;
            //        resultModel.BID = bankAuthorize.BID;
            //        resultModel.RESULT = "请求成功，可以办理";
            //        isOk = true;
            //    }
            //    else
            //    {
            //        resultModel.ZT = 1;
            //        resultModel.RESULT = "请求失败，无法办理";
            //    }
            //    var resultCount = await _businessRequest.Add(resultModel);

            //}
            //#endregion

            //#region 插入IIRS
            //if (data.Count > 0 && isOk)
            //{
            //    try
            //    {
            //        bankAuthorize.DOCUMENTTYPE = data[0].Dyr_Zjlb;
            //        bankAuthorize.DOCUMENTNUMBER = data[0].Dyr_Zjhm;
            //        bankAuthorize.AUTHORIZATIONDATE = DateTime.Now;
            //        bankAuthorize.STATUS = SysFlowConst.FLOW_DYBGZX_DBDCZXDYSP;
            //        bankAuthorize.rightname = data[0].Dyr;
            //        bankAuthorize.BankCode = model.MRTG_AGNC_PSN_CRDT_NO;
            //        bankAuthorize.BankName = model.MRTG_AGNC_PSN_NM;

            //        xgdjglList.Add(new XGDJGL_INFO()
            //        {
            //            BGBM = Provider.Sql.Create().ToString(),
            //            ZSLBH = newSlbh,
            //            FSLBH = HouseData.selectHouse.SLBH,
            //            BGRQ = saveTime,
            //            BGLX = "抵押注销",
            //            XGZLX = HouseData.selectHouse.ZSLX,
            //            XGZH = HouseData.selectHouse.BDCZH,
            //            PID = "",
            //            XID = xid
            //        });

            //        foreach (var item in HouseData.selectHouse.children)
            //        {
            //            HouseList.Add(new OrderHouseAssociation
            //            {
            //                OID = Provider.Sql.Create().ToString(),
            //                BID = bankAuthorize.BID,
            //                CERTIFICATENUMBER = item.BDCZH,
            //                ACCEPTANCENUMBER = item.SLBH,
            //                NUMBERID = item.TSTYBM,
            //                ADDRESS = item.ZL,
            //                rightname = item.QLRMC,
            //                AUTHORIZATIONDATE = saveTime,
            //                qllx = item.qllx,
            //                qlxz = item.qlxz,
            //                jzmj = item.jzmj,
            //                tdqllx = item.tdqllx,
            //                tdqlxz = item.tdqlxz,
            //                tdmj = item.tdmj,
            //                bdclx = item.BDCLX
            //            });

            //            tsglList.Add(new TSGL_INFO()
            //            {
            //                GLBM = Provider.Sql.Create().ToString(),
            //                SLBH = newSlbh,
            //                BDCLX = item.BDCLX,//房屋或土地
            //                TSTYBM = item.TSTYBM,
            //                BDCDYH = item.BDCDYH,
            //                DJZL = "抵押注销",
            //                CSSJ = saveTime,
            //                XID = xid
            //            });
            //            xgdjglList.Add(new XGDJGL_INFO()
            //            {
            //                BGBM = Provider.Sql.Create().ToString(),
            //                ZSLBH = newSlbh,
            //                FSLBH = item.SLBH,
            //                BGRQ = saveTime,
            //                BGLX = "抵押注销",
            //                XGZLX = item.ZSLX,
            //                XGZH = item.BDCZH,
            //                PID = HouseData.selectHouse.SLBH,
            //                XID = xid
            //            });
            //        }

            //        XGDJZX_INFO zxInfo = new XGDJZX_INFO()
            //        {
            //            SLBH = newSlbh,
            //            XGZH = model.REALEST_ECB_NO,
            //            BDCDYH = HouseData.selectHouse.BDCDYH,
            //            DJLX = "注销登记",
            //            DJYY = model.LOUT_RSN,
            //            DLRXM = HouseData.DYZXLXR,
            //            DLJGMC = model.MRTG_AGNC_PSN_NM,
            //            SPBZ = HouseData.BZ,
            //            SQRQ = saveTime,
            //            SJR = model.MNPLT_USR_NM,
            //            XID = xid
            //        };
            //        REGISTRATION_INFO regInfo = new REGISTRATION_INFO()
            //        {
            //            XID = xid,
            //            YWSLBH = newSlbh,
            //            SLBH = newSlbh,
            //            DJZL = 1,
            //            ZL = data[0].zl,
            //            ORG_ID = "95856595-658e-417a-9c2a-01730e3f7970",    //组织机构编码暂时没有 95856595-658e-417a-9c2a-01730e3f7970
            //            USER_ID = model.MNPLT_USR_ECD,  //操作员编码
            //            BDCZH = model.REALEST_ECB_NO,     //存不动产证明号
            //            QLRMC = string.Join(",", HouseData.selectPerson.Cast<DyPersonVModel>().OrderBy(S => S.SXH).Select(S => S.QLRMC).ToArray()),
            //            REMARK2 = "抵押注销",
            //            AUZ_ID = bankAuthorize.BID,
            //            IS_ACTION_OK = 0,
            //            //SaveDataJson = saveDataJson
            //        };

            //        foreach (var person in HouseData.selectPerson)
            //        {
            //            qlrglList.Add(new QLRGL_INFO()
            //            {
            //                GLBM = Provider.Sql.Create().ToString(),
            //                SLBH = newSlbh,
            //                YWBM = newSlbh,
            //                ZJHM = person.ZJHM,
            //                QLRID = person.QLRID,
            //                QLRMC = person.QLRMC,
            //                ZJLB = person.ZJLB,
            //                ZJLB_ZWM = person.ZJLB_ZWM,
            //                DH = person.DH,
            //                QLRLX = "抵押人",
            //                SXH = person.SXH,
            //                XID = xid
            //            });
            //        }
            //        qlrglList.Add(new QLRGL_INFO()
            //        {
            //            GLBM = Provider.Sql.Create().ToString(),
            //            SLBH = newSlbh,
            //            YWBM = newSlbh,
            //            ZJHM = "91211000701714516X",
            //            QLRID = "yunsun001",
            //            QLRMC = "中国建设银行股份有限公司辽阳分行",
            //            ZJLB = "8",
            //            ZJLB_ZWM = "统一社会信用代码",
            //            DH = "",
            //            QLRLX = "抵押权人",
            //            XID = xid
            //        });



            //        IFLOW_DO_ACTION iFLOW_DO_ACTION = new IFLOW_DO_ACTION();
            //        iFLOW_DO_ACTION.PK = Provider.Sql.Create().ToString();
            //        iFLOW_DO_ACTION.FLOW_ID = Convert.ToInt32(bankAuthorize.STATUS);
            //        iFLOW_DO_ACTION.AUZ_ID = bankAuthorize.BID;
            //        iFLOW_DO_ACTION.CDATE = saveTime;
            //        iFLOW_DO_ACTION.USER_NAME = yhjbdlrModel.AGNC_PSN_NM;

            //        bankModel.RelatedId = Guid.NewGuid();
            //        bankModel.BankId = Guid.NewGuid();
            //        bankModel.Srcsys = model.TXN_STM_REF_NO;
            //        bankModel.IIRSBid = bankAuthorize.BID;
            //        bankModel.Slbh = data[0].slbh;
            //        bankModel.QueryDate = saveTime;

            //        _dbTransManagement.BeginTran();
            //        var bankAuthorizeCount = _bankAuthorizeRepository.Add(bankAuthorize);
            //        var OrderHouseAssociationCount = _orderHouseAssociationRepository.Add(HouseList);
            //        var actionCount = _dO_ACTIONRepository.Add(iFLOW_DO_ACTION);
            //        var RelatedCount = _bankRelatedIIRSRepository.Add(bankModel);
            //        var regInfoCount = _registration_InfoRepository.Add(regInfo);
            //        var tsglCount = _tsgl_InfoRepository.Add(tsglList);
            //        var qlrglCount = _qlrgl_InfoRepository.Add(qlrglList);
            //        var xgdjglCount = _xgdjgl_InfoRepository.Add(xgdjglList);
            //        var xgdjzxCount = _xgdjzx_InfoRepository.Add(zxInfo);
            //        _dbTransManagement.CommitTran();

            //        message = "查询成功";
            //    }
            //    catch (Exception ex)
            //    {
            //        _dbTransManagement.RollbackTran();
            //        _logger.LogError(ex, ex.Message);
            //        throw ex;
            //    }

            //}
            //else
            //{
            //    message = "未查询到数据";
            //}

            //#endregion

            //try
            //{
            //    _dbTransManagement.BeginTran();
            //    var fjCount = await _fjRepository.Add(fjModel);
            //    var yhjbdlrCount = await _yhjbdlrRepository.Add(yhjbdlrModel);
            //    var jkrCount = await _jkrxxRepository.Add(jkrModel);
            //    var dyrxxCount = await _dyrxxRepository.Add(dyrModel);
            //    var bdcdyqkCount = await _dybdcdyxxRepository.Add(bdcdyqkModel);
            //    var count = await _dyzxsqRepository.Add(model);
            //    _dbTransManagement.CommitTran();
            //    if (data.Count > 0)
            //    {
            //        for (int i = 0; i < data.Count; i++)
            //        {
            //            slbh += data[i].slbh + ",";
            //        }
            //        slbh = slbh.Substring(0, slbh.Length - 1);
            //    }
            //    return slbh;

            //}
            //catch (Exception ex)
            //{
            //    _dbTransManagement.RollbackTran();
            //    _logger.LogError(ex, ex.Message);
            //    throw ex;
            //}
        }

        public async Task<YzhdyResultVModel> GetYzhdyResult(YZHDYCX_REQUEST model)
        {
            YzhdyResultVModel ResultModel = new YzhdyResultVModel();
            await Task.CompletedTask;
            return ResultModel;
            //string tstybm = "";
            //string DySlbh = "";

            //var data = await _certificationRepository.Query(i => i.bdczmh.Contains(model.REALEST_RGSCTF_NO) && i.Dyr.Contains(model.REALEST_WGHT_PSN_NM) && i.Dyqr.Contains(model.MRTG_WGHT_PSN_NM));
            //var HouseData = await GetMortgageInfo(model.REALEST_RGSCTF_NO);

            //DySlbh = data[0].slbh;
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            //base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_BDC);
            ////获取本行抵押权人信息
            //var ownerDyqrxxData = await base.Db.Queryable<DJ_DY, DJ_QLRGL, DJ_QLR>((A, B, C) => A.SLBH == B.SLBH && B.QLRID == C.QLRID ).Where((A, B, C) => (A.LIFECYCLE == 0 || A.LIFECYCLE == null) && A.BDCZMH == model.REALEST_RGSCTF_NO && B.QLRLX == "抵押权人" && C.QLRMC == "中国建设银行股份有限公司辽阳分行").Select((A, B, C) => new
            //{
            //    MRTG_WGHT_PSN_TP = "抵押权人类型",
            //    MRTG_WGHT_PSN_CLASS = "抵押权人类别",
            //    MRTG_WGHT_PSN_NM = C.QLRMC,
            //    MRTG_WGHT_PSN_CD = C.ZJHM,
            //    MRTG_WGHT_PSN_CRDT_TP = C.ZJLB,
            //    MRTG_WGHT_PSN_CRDT_NO = C.ZJHM,
            //    MRTG_WGHT_PSN_CTC_TEL = "抵押权人联系电话",
            //    MRTG_WGHT_PSN_ADR = C.TXDZ,
            //    PRIM_CLM_WRNT_TOTAL_AMT = A.BDBZZQSE,     //被担保主债权数额
            //    STK_MRTG_MOD = A.DYFS,
            //    STK_RGHT_VAL_AREA = A.DYMJ,     //存量权利价值面积
            //    STK_MRTG_WGHT_RG_DT = A.DJRQ,
            //    PRIM_CLM_WRNT_AMT = A.BDBZZQSE,       //主债权担保金额
            //    CLM_NUM_AMT = A.BDBZZQSE,  //债权金额
            //    DBT_PRFMN_STDT = A.QLQSSJ,
            //    DBT_PRFMN_EDDT = A.QLJSSJ,
            //    Mrtg_Amt = A.BDBZZQSE,  //抵押金额
            //    DYSW = A.DYSW
            //}).ToListAsync();

            ////获取TSTYBM
            //var tstybmData = await base.Db.Queryable<DJ_DJB, DJ_TSGL>((A, B) => A.SLBH == B.SLBH).Where((A, B) => A.BDCZH == model.REALEST_WRNT_NO && (A.LIFECYCLE == 0 || A.LIFECYCLE == null)).Select((A, B) => new
            //{
            //    TSTYBM = B.TSTYBM
            //}).ToListAsync();
            //tstybm = tstybmData[0].TSTYBM;

            //ResultModel.Ret_Code = "返回码";
            //ResultModel.Ret_Data = "返回说明";
            //ResultModel.Bsn_Pts_No = "业务件号";
            //ResultModel.Ori_Mrtg_BsnID = "原抵押业务ID";
            //ResultModel.If_Can_Mrg_Lout_And_Mrtg = "是否可合并办理注销+抵押";
            //ResultModel.Not_Can_Mrtg_Rsn = "不可合并办理的原因";
            //ResultModel.RealEst_RgsCtf_No = "不动产登记证明号";
            ////ResultModel.Mrtg_Wght_Psn_InfList

            ////债务人信息
            //OBLG zwrModel = new OBLG();
            ////zwrModel.OID = Guid.NewGuid().ToString();
            ////zwrModel.ZWRXX_ID = Guid.NewGuid().ToString("N");
            //zwrModel.OBLG_NM = "债务人名称";

            //try
            //{
            //    foreach (var item in ownerDyqrxxData)
            //    {
            //        MRTG_WGHT_PSN_INF dyqrModel = new MRTG_WGHT_PSN_INF();
            //        dyqrModel.MRTG_WGHT_PSN_TP = item.MRTG_WGHT_PSN_TP;
            //        dyqrModel.MRTG_WGHT_PSN_CLASS = item.MRTG_WGHT_PSN_CLASS;
            //        dyqrModel.MRTG_WGHT_PSN_NM = item.MRTG_WGHT_PSN_NM;
            //        dyqrModel.MRTG_WGHT_PSN_CD = item.MRTG_WGHT_PSN_CD;
            //        dyqrModel.MRTG_WGHT_PSN_CRDT_TP = item.MRTG_WGHT_PSN_CRDT_TP;
            //        dyqrModel.MRTG_WGHT_PSN_CRDT_NO = item.MRTG_WGHT_PSN_CRDT_NO;
            //        dyqrModel.MRTG_WGHT_PSN_CTC_TEL = item.MRTG_WGHT_PSN_CTC_TEL;
            //        dyqrModel.MRTG_WGHT_PSN_ADR = item.MRTG_WGHT_PSN_ADR;
            //        dyqrModel.PRIM_CLM_WRNT_TOTAL_AMT = Convert.ToDouble(item.PRIM_CLM_WRNT_TOTAL_AMT);     //被担保主债权数额
            //        dyqrModel.STK_MRTG_MOD = item.STK_MRTG_MOD;
            //        dyqrModel.STK_RGHT_VAL_AREA = Convert.ToDouble(item.STK_RGHT_VAL_AREA);     //存量权利价值面积
            //        dyqrModel.STK_MRTG_WGHT_RG_DT = item.STK_MRTG_WGHT_RG_DT.ToString();
            //        dyqrModel.PRIM_CLM_WRNT_AMT = Convert.ToDouble(item.PRIM_CLM_WRNT_AMT);        //主债权担保金额
            //        dyqrModel.CLM_NUM_AMT = Convert.ToDouble(item.CLM_NUM_AMT);   //债权金额
            //        dyqrModel.DBT_PRFMN_STDT = item.DBT_PRFMN_STDT.ToString();
            //        dyqrModel.DBT_PRFMN_EDDT = item.DBT_PRFMN_EDDT.ToString();
            //        dyqrModel.CLM_NUM_AMT = Convert.ToDouble(item.CLM_NUM_AMT);  //抵押金额
            //        //dyqrModel.ZWRXX_ID = zwrModel.ZWRXX_ID;
            //        dyqrModel.OBLG.Add(zwrModel);
            //        ResultModel.Mrtg_Wght_Psn_Inf.Add(dyqrModel);
            //    }
            //}
            //catch (Exception ex)
            //{

            //    throw ex;
            //}


            ////获取房屋状态以及房屋相关信息
            //var houseData = await _houseStatusRepository.Query(i => i.Tstybm == tstybmData[0].TSTYBM);

            ////不动产信息赋值
            //#region 不动产信息赋值
            //REALEST_INF bdcxxModel = new REALEST_INF();
            //bdcxxModel.HS_ECD = houseData[0].Tstybm;
            //if(!houseData[0].Djzl.Contains("查封"))   //没有查封状态
            //{
            //    bdcxxModel.IF_CNHDL = "可以办理";
            //}
            //else
            //{
            //    bdcxxModel.IF_CNHDL = "不可以办理";
            //    bdcxxModel.NOT_CNHDL_CMNT = "存在查封状态";
            //}
            //bdcxxModel.REALEST_UNIT_NO = houseData[0].Bdcdyh;
            ////抵押人信息赋值
            //MRTG_PSN_INF dyrModel = new MRTG_PSN_INF();
            //dyrModel.MRTG_PSN_NM = houseData[0].Dyr;
            //dyrModel.MRTG_PSN_TP = "抵押人";
            //dyrModel.MRTG_PSN_CRDT_TP = houseData[0].Dyr_Zjlb;
            //dyrModel.MRTG_PSN_CRDT_NO = houseData[0].Dyr_Zjhm;
            //bdcxxModel.MRTG_PSN_INF.Add(dyrModel);

            ////获取DY，CF,YG,YY次数
            //int dycount = 0;
            //int cfcount = 0;
            //int ygcount = 0;
            //int yycount = 0;
            //var CountData = await _tsglRepository.Query(i => i.TSTYBM == tstybm && (i.LIFECYCLE == 0 || i.LIFECYCLE == null));
            //for (int i = 0; i < CountData.Count; i++)
            //{
            //    if(CountData[i].DJZL == "抵押")
            //    {
            //        dycount += 1;
            //    }
            //    else if(CountData[i].DJZL == "查封")
            //    {
            //        cfcount += 1;
            //    }
            //    else if (CountData[i].DJZL == "预告")
            //    {
            //        ygcount += 1;
            //    }
            //    else if (CountData[i].DJZL == "异议")
            //    {
            //        yycount += 1;
            //    }
            //}

            //bdcxxModel.HAD_MRTG_CNT = dycount;
            //bdcxxModel.HAD_SEALUP_CNT = cfcount;
            //bdcxxModel.FRCST_RGS_CNT = ygcount;
            //bdcxxModel.OBJTN_RGS_CNT = yycount;
            //bdcxxModel.MRTG_ORDER_PRCD =Convert.ToDecimal(ownerDyqrxxData[0].DYSW);
            //bdcxxModel.IF_EXST_OTHR_MRTG_STTN = dycount > 1 ? "存在其他抵押情况" : "不存在其他抵押情况";
            ////bdcxxModel.IF_EXST_MRTG_WGHT_FRCST_RGS = ygcount > 0 ? "存在抵押权预告登记" : "不存在抵押权预告登记";
            //bdcxxModel.IF_EXST_FRCST_RGS = ygcount > 0 ? "存在预告登记" : "不存在预告登记";
            //bdcxxModel.IF_EXST_OBJTN_RGS = yycount > 0 ? "存在异议登记" : "不存在异议登记";
            //bdcxxModel.HS_LO = houseData[0].Zl;

            //var houseAttributeData = await base.Db.Queryable<FC_H_QSDC, FC_Z_QSDC, ZD_QSDC, DJ_TSGL, DJ_DJB, DJ_QLRGL, DJ_QLR>((A, B, C, D, E, F, G) => new object[] { JoinType.Inner, A.LSZTYBM == B.TSTYBM, JoinType.Left, B.ZDTYBM == C.ZDTYBM, JoinType.Inner, D.TSTYBM == A.TSTYBM, JoinType.Inner, D.SLBH == E.SLBH, JoinType.Inner, F.SLBH == E.SLBH, JoinType.Inner, F.QLRID == G.QLRID }).Where((A, B, C, D, E, F, G) => A.TSTYBM == tstybm && E.BDCZH == model.REALEST_WRNT_NO && F.QLRLX == "权利人" && (D.LIFECYCLE == 0 || D.LIFECYCLE == null)).Select((A, B, C, D, E, F, G) => new
            //{
            //    zl = A.ZL,
            //    zcs = B.ZCS,
            //    myc = A.MYC,
            //    fwjg = B.FWJG,
            //    tdsyqrmc = G.QLRMC,
            //    bdccqrzjhm = G.ZJHM,
            //    fwjzmj = A.JZMJ,
            //    fwycjzmj = A.YCJZMJ,
            //    tdsqyqx = C.SYQX,
            //    mjdw ="平方米",
            //    fwtnmj = A.TNJZMJ,
            //    scmj = A.JZMJ,
            //    tdftmj=C.FTMJ,
            //    fwyt = A.GHYT,
            //    tdyt = C.PZTDYT,
            //    tdzzrq = C.ZZRQ,
            //    tdqllx = C.QLLX,
            //    tdqlxz = C.QLXZ,
            //    qzfj = E.FJ,
            //    qlfe = F.GYFE,
            //    cxrq = DateTime.Now,
            //    gyqk = F.GYFS,
            //    fwqlxz = A.QLXZ,
            //    fwqllx = A.QLLX,
            //    qlzzrq = A.TDZZRQ,
            //    dbrq = E.DJRQ
            //}).ToListAsync();

            //var sysDicList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 1)).ToListAsync();
            //var sysQlxzList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 3)).ToListAsync();    //房屋权利性质
            //var sysQllxList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 4)).ToListAsync();    //房屋权利类型
            //var sysFwytList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 5)).ToListAsync();    //房屋用途类型
            //var systdQllxList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 6)).ToListAsync();    //土地权利类型
            //var systdQlxzList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 7)).ToListAsync();    //土地权利性质
            //var systdGhytList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 8)).ToListAsync();    //土地用途类型
            //var sysFwjgList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 9)).ToListAsync();    //房屋结构
            ////ZJLB_ZWM = zjlb_zwmObj != null ? zjlb_zwmObj.DNAME : string.Empty,
            //var fwjg = sysFwjgList.Where(s => s.DEFINED_CODE == houseAttributeData[0].fwjg).FirstOrDefault();
            //var tdQlxz = systdQlxzList.Where(s => s.DEFINED_CODE == houseAttributeData[0].tdqlxz).FirstOrDefault();
            //var Fwyt = sysFwytList.Where(s => s.DEFINED_CODE == houseAttributeData[0].fwyt).FirstOrDefault();
            //var tdGhyt = systdGhytList.Where(s => s.DEFINED_CODE == houseAttributeData[0].tdyt).FirstOrDefault();
            //var tdQllx = systdQllxList.Where(s => s.DEFINED_CODE == houseAttributeData[0].tdqllx).FirstOrDefault();
            //var fwqlxz = sysQlxzList.Where(s => s.DEFINED_CODE == houseAttributeData[0].fwqlxz).FirstOrDefault();
            //var fwqllx = sysQllxList.Where(s => s.DEFINED_CODE == houseAttributeData[0].fwqllx).FirstOrDefault();

            //bdcxxModel.TOT_LYR_NUM = Convert.ToDecimal(houseAttributeData[0].zcs);
            //bdcxxModel.HS_WBT_LYR_NUM = Convert.ToDecimal(houseAttributeData[0].myc);
            ////bdcxxModel.HS_STC = houseAttributeData[0].fwjg;
            //bdcxxModel.HS_STC = fwjg != null ? fwjg.DNAME : string.Empty;
            //bdcxxModel.LAND_US_WGHT_PSN_NM = houseAttributeData[0].tdsyqrmc;
            //bdcxxModel.LAND_CHAR = tdQlxz != null ? tdQlxz.DNAME : string.Empty;
            //bdcxxModel.LAND_US_DDLN = houseAttributeData[0].tdsqyqx;
            //bdcxxModel.REALEST_WGHT_PSN_NM = houseAttributeData[0].tdsyqrmc;
            //bdcxxModel.REALEST_WGHT_PSN_ = houseAttributeData[0].bdccqrzjhm;
            //bdcxxModel.HS_CNSTRCTAREA = Convert.ToDecimal(houseAttributeData[0].fwjzmj);
            //bdcxxModel.HS_CNSTRCTAREA_UNIT = houseAttributeData[0].mjdw;
            //bdcxxModel.HS_SET_INNR_AREA = Convert.ToDecimal(houseAttributeData[0].fwtnmj);
            //bdcxxModel.REALI_CNSTRCTAREA= Convert.ToDecimal(houseAttributeData[0].fwjzmj);
            //bdcxxModel.LAND_APOR_AREA = Convert.ToDecimal(houseAttributeData[0].tdftmj);
            //bdcxxModel.HS_USE = Fwyt != null ? Fwyt.DNAME : string.Empty;
            //bdcxxModel.LAND_USE = tdGhyt != null ? tdGhyt.DNAME : string.Empty;
            //bdcxxModel.LND_US_WGHT_TMDT = houseAttributeData[0].tdzzrq.ToString();
            //bdcxxModel.LND_RGHT_TP = tdQllx != null ? tdQllx.DNAME : string.Empty;
            //bdcxxModel.LND_RGHT_CHAR = tdQlxz != null ? tdQlxz.DNAME : string.Empty;
            //bdcxxModel.WRNT_ATCH = houseAttributeData[0].qzfj;
            //bdcxxModel.WGHT_LOT = houseAttributeData[0].gyqk;
            //bdcxxModel.ENQR_TM = houseAttributeData[0].cxrq.ToString();
            //bdcxxModel.COM_STTN = houseAttributeData[0].gyqk;
            //bdcxxModel.HS_CHAR_NM = fwqlxz != null ? fwqlxz.DNAME : string.Empty;
            //bdcxxModel.HS_TP = fwqllx != null ? fwqllx.DNAME : string.Empty;
            //bdcxxModel.RGHT_TMDT = houseAttributeData[0].tdzzrq.ToString();
            //bdcxxModel.RG_DT = houseAttributeData[0].dbrq.ToString();
            //bdcxxModel.REC_MRTG = dycount > 0 ? "有抵押" : "无抵押";
            //bdcxxModel.IF_SEALUP = cfcount > 0 ? "有查封" : "无查封";

            //#endregion

            //#region 查封信息
            //var CfSlbh = "";
            //foreach (var item in CountData)
            //{
            //    if(item.DJZL == "查封")
            //    {
            //        CfSlbh += item.SLBH + ",";
            //    }
            //}
            //CfSlbh = CfSlbh.Substring(0, CfSlbh.Length - 1);
            //var CfData = await _CFRepository.Query(i => i.SLBH == CfSlbh && (i.LIFECYCLE == 0 || i.LIFECYCLE == null));
            //if(CfData.Count>0)
            //{
            //    foreach (var item in CfData)
            //    {
            //        SEALUP_INF CfModel = new SEALUP_INF();
            //        CfModel.SEALUP_SN = item.CFBH;
            //        CfModel.SEALUP_UNIT_NM = item.CFJG;
            //        CfModel.SEALUP_ATCH = item.FJ;
            //        CfModel.SEALUP_STTM = item.CFQSSJ.ToString();
            //        CfModel.SEALUP_EDTM = item.CFJSSJ.ToString();
            //        CfModel.SEALUP_FLNO = item.CFWH;
            //        CfModel.SEALUP_INPUT_TM = item.DJSJ.ToString();
            //        CfModel.SEALUP_TP = item.CFLX;
            //        CfModel.RLTM_BY_SEALUP_APLY_PSN = item.QLR;    //后续需要修改，qlr表
            //        bdcxxModel.SEALUP_INF.Add(CfModel);

            //        Admn_Rst XzxzModel = new Admn_Rst();
            //        XzxzModel.Admn_Rst_StDt = item.CFQSSJ.ToString();
            //        XzxzModel.Admn_Rst_Cntnt = item.CFLX;
            //        bdcxxModel.Admn_Rst.Add(XzxzModel);
            //    }

            //}
            //#endregion

            //#region 权利登记信息
            //Rght_Rgs_Inf qldjxxModel = new Rght_Rgs_Inf();
            //qldjxxModel.RealEst_Wght_Rltv_CrtfNo = model.REALEST_WRNT_NO;
            //qldjxxModel.Rght_RgDt_Tm = "";
            //var qldjxxData = await base.Db.Queryable<DJ_DJB, DJ_QLRGL, DJ_QLR>((A, B, C) => A.SLBH == B.SLBH && B.QLRID == C.QLRID).Where((A, B, C) => A.BDCZH == model.REALEST_WRNT_NO).Select((A, B, C) => new
            //{
            //    bdczh = A.BDCZH,
            //    zt = A.LIFECYCLE,
            //    qllx = B.QLRLX,
            //    qlrbm = B.QLRID,
            //    qlrmc = C.QLRMC,
            //    qlrlx = B.QLRLX,
            //    zjlb = B.ZJLB,
            //    zjhm = C.ZJHM,
            //    lxdh = C.DH,
            //    lxdz = C.TXDZ,
            //    qldjfj = A.FJ,
            //    gyfs = A.GYFS,
            //    qlbl = B.GYFE
            //}).ToListAsync();

            //foreach (var item in qldjxxData)
            //{
            //    Rght_Rgs_Inf_Rght_Psn_Inf qlrxxModel = new Rght_Rgs_Inf_Rght_Psn_Inf();
            //    qlrxxModel.RealEst_Wrnt_No = item.bdczh;
            //    if(item.zt == 0 || item.zt is null)
            //    {
            //        qlrxxModel.Own_St = "现实";
            //    }
            //    else
            //    {
            //        qlrxxModel.Own_St = "历史";
            //    }
            //    qlrxxModel.Rght_Tp = item.qllx;
            //    qlrxxModel.Rght_Psn_No = item.qlrbm;
            //    qlrxxModel.Rght_Psn_Nm = item.qlrmc;
            //    qlrxxModel.Rght_Psn_Tp = item.qlrlx;
            //    qlrxxModel.Rght_Psn_Crdt_Tp = item.zjlb;
            //    qlrxxModel.Rght_Psn_Crdt_No = item.zjhm;
            //    qlrxxModel.Rght_Psn_Ctc_Tel = item.lxdh;
            //    qlrxxModel.Rght_Psn_Adr = item.lxdz;
            //    qlrxxModel.Rght_Rgs_Atch = item.qldjfj;
            //    qlrxxModel.Com_Mod = item.gyfs;
            //    qlrxxModel.Rght_Pctg = item.qlbl;
            //    qldjxxModel.Rght_Rgs_Inf_Rght_Psn_Inf.Add(qlrxxModel);

            //}
            //bdcxxModel.Rght_Rgs_Inf.Add(qldjxxModel);
            //#endregion
            //ResultModel.RealEst_Inf.Add(bdcxxModel);
            //return ResultModel;


        }
    }
}
