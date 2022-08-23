using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.IRepository.TAX;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.EntityModel.Tax;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC.Condition;
using IIRS.Models.ViewModel.TAX;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using RT.Comb;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace IIRS.Services.BDC
{
    /// <summary>
    /// 条件查询服务
    /// </summary>
    public class ConditionQueryServices : BaseServices, IConditionQueryServices
    {
        private readonly ILogger<ConditionQueryServices> _logger;
        private readonly IDBTransManagement _dBTransManagement;
        private readonly ISysDicRepository _SysDicRepository;
        private readonly ITsgl_infoRepository _tsglRepository;
        private readonly IDJ_QLRGLRepository _dJ_QLRGLRepository;        
        
        private readonly IQlrgl_infoRepository _qlrgl_InfoRepository;        
        private readonly ISfdInfoRepository _sfdInfoRepository;
        private readonly ISfdFbInfoRepository _sfdFbInfoRepository;
        private readonly ISpbInfoRepository _spbInfoRepository;
        private readonly IQlxgRepository _qlxgRepository;
        private readonly ISjdInfoRepository _sjdInfoRepository;
        private readonly IDjbInfoRepository _djbInfoRepository;
        private readonly IXgdjgl_infoRepository _xgdjgl_InfoRepository;
        private readonly IPubAttFileRepository _fileRepository;

        private readonly ITaxBuyerRepository _taxBuyerRepository;
        private readonly ITaxHomeRepository _taxHomeRepository;
        private readonly ITaxSellerRepository _taxSellerRepository;

        private readonly ISysDataRecorderRepository _sysDataRecorderRepository;
        private readonly IDO_ACTIONRepository _dO_ACTIONRepository;
        private readonly IRegistration_infoRepository _registration_InfoRepository;
        private readonly IBankAuthorizeRepository _bankAuthorizeRepository;

        public ConditionQueryServices(IDBTransManagement dbTransManagement, ILogger<ConditionQueryServices> logger, ITsgl_infoRepository tsglRepository, IDJ_QLRGLRepository dJ_QLRGLRepository, ISysDicRepository SysDicRepository, IQlrgl_infoRepository qlrgl_InfoRepository, IRegistration_infoRepository registration_InfoRepository, ISfdInfoRepository sfdInfoRepository, ISfdFbInfoRepository sfdFbInfoRepository, ISpbInfoRepository spbInfoRepository, ITaxBuyerRepository taxBuyerRepository, ITaxHomeRepository taxHomeRepository, ITaxSellerRepository taxSellerRepository, IDO_ACTIONRepository dO_ACTIONRepository, ISjdInfoRepository sjdInfoRepository, IDjbInfoRepository djbInfoRepository, IXgdjgl_infoRepository xgdjgl_InfoRepository,ISysDataRecorderRepository sysDataRecorderRepository, IBankAuthorizeRepository bankAuthorizeRepository, IPubAttFileRepository fileRepository, IQlxgRepository qlxgRepository) : base(dbTransManagement)
        {
            _logger = logger;
            _dBTransManagement = dbTransManagement;
            _tsglRepository = tsglRepository;
            _dJ_QLRGLRepository = dJ_QLRGLRepository;
            _SysDicRepository = SysDicRepository;
            _qlrgl_InfoRepository = qlrgl_InfoRepository;
            _registration_InfoRepository = registration_InfoRepository;
            _sfdInfoRepository = sfdInfoRepository;
            _sfdFbInfoRepository = sfdFbInfoRepository;
            _spbInfoRepository = spbInfoRepository;
            _taxBuyerRepository = taxBuyerRepository;
            _taxHomeRepository = taxHomeRepository;
            _taxSellerRepository = taxSellerRepository;
            _dO_ACTIONRepository = dO_ACTIONRepository;
            _sjdInfoRepository = sjdInfoRepository;
            _djbInfoRepository = djbInfoRepository;
            _xgdjgl_InfoRepository = xgdjgl_InfoRepository;
            _sysDataRecorderRepository = sysDataRecorderRepository;
            _bankAuthorizeRepository = bankAuthorizeRepository;
            _fileRepository = fileRepository;
            _qlxgRepository = qlxgRepository;
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
        public async Task<PageModel<FC_Z_QSDC>> GetFc_zResult(string bdcdyh, string zl, string zh, string xmmc, int intPageIndex, int PageSize)
        {
            RefAsync<int> totalCount = 0;
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDic = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 5,9 }).ToListAsync();
            PageModel<FC_Z_QSDC> pageModel = new PageModel<FC_Z_QSDC>();
            List<FC_Z_QSDC> modelList = new List<FC_Z_QSDC>();
            
            base.ChangeDB(SysConst.DB_CON_BDC);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            #region 查询幢信息
            var BlockData = await base.Db.Queryable<FC_H_QSDC,FC_Z_QSDC>((A, B) => new object[] { JoinType.Left, A.LSZTYBM == B.TSTYBM})
                .WhereIF(!string.IsNullOrEmpty(bdcdyh), (A, B) => A.BDCDYH.Contains(bdcdyh))
                .WhereIF(!string.IsNullOrEmpty(zl), (A, B) => A.ZL.Contains(zl))
                .WhereIF(!string.IsNullOrEmpty(zh), (A, B) => B.ZH.Contains(zh))
                .WhereIF(!string.IsNullOrEmpty(xmmc), (A, B) => B.XMMC.Contains(xmmc)).
                Select((A, B) => new FC_Z_QSDC() 
                {
                    TSTYBM = B.TSTYBM,
                    FWBH = B.FWBH,
                    ZH = B.ZH,
                    LJZH = B.LJZH,
                    XMMC = B.XMMC,
                    ZCS =  B.ZCS,
                    DXCS = B.DXCS,
                    DSCS = B.DSCS,
                    FWJG = B.FWJG,
                    GHYT = B.GHYT
                }).ToPageListAsync(intPageIndex, PageSize, totalCount);
            //将字典值赋值中文名
            if(BlockData.Count > 0)
            {
                foreach (var item in BlockData)
                {
                    FC_Z_QSDC model = new FC_Z_QSDC();
                    var Fwyt = sysDic.Where(s => s.DEFINED_CODE == item.GHYT && s.GID == 5).FirstOrDefault();
                    var fwjg = sysDic.Where(s => s.DEFINED_CODE == item.FWJG && s.GID == 9).FirstOrDefault();
                    item.GHYT = Fwyt != null ? Fwyt.DNAME : string.Empty;
                    item.FWJG = fwjg != null ? fwjg.DNAME : string.Empty;
                    model.TSTYBM = item.TSTYBM;
                    model.FWBH = item.FWBH;
                    model.ZH = item.ZH;
                    model.LJZH = item.LJZH;
                    model.XMMC = item.XMMC;
                    model.ZCS = item.ZCS;
                    model.DXCS = item.DXCS;
                    model.DSCS = item.DSCS;
                    model.FWJG = item.FWJG;
                    model.GHYT = item.GHYT;
                    modelList.Add(model);
                }

                int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / PageSize.ObjToDecimal()).ObjToInt();
                pageModel.page = intPageIndex;
                pageModel.PageSize = PageSize;
                pageModel.dataCount = totalCount;
                pageModel.pageCount = pageCount;
                pageModel.data = modelList;
            }
            

            #endregion
            return pageModel;
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
        public async Task<PageModel<ConditionQueryResultVModel>> GetQueryResult(string bdcdyh, string bdczh, string slbh, string qlrmc, string zl, string zslx, int intPageIndex, int PageSize)
        {
            RefAsync<int> totalCount = 0;
            intPageIndex = 1;
            PageSize = 20;
            base.ChangeDB(SysConst.DB_CON_IIRS);
            PageModel<ConditionQueryResultVModel> pageModel = new PageModel<ConditionQueryResultVModel>();
            var sysDic = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 1, 3, 4, 5, 6, 7, 8, 9 }).ToListAsync();
            //bdczh = "00237901";
            //zl = "白塔区民主路158-23号楼2单元14层";
            //bdczh = "辽(2017)辽阳市不动产权第0009410号";
            List<ConditionQueryResultVModel> modelList = new List<ConditionQueryResultVModel>();

            #region 查询结果赋值
            base.ChangeDB(SysConst.DB_CON_BDC);
            #region 查询房屋的所有属性
            var HouseData = await base.Db.Queryable<DJ_TSGL, DJ_QLRGL, DJ_QLR, DJ_DJB, DJ_SJD, QL_FWXG, QL_TDXG>((A, B, C, D, E, F, G) => new object[] { JoinType.Inner, A.SLBH == B.SLBH, JoinType.Inner, B.QLRID == C.QLRID, JoinType.Inner, B.SLBH == D.SLBH, JoinType.Left, D.SLBH == E.SLBH, JoinType.Left, D.SLBH == F.SLBH, JoinType.Left, D.SLBH == G.SLBH })
                    .WhereIF(!string.IsNullOrEmpty(bdcdyh), (A, B, C, D, E, F, G) => D.BDCDYH.Contains(bdcdyh))
                    .WhereIF(!string.IsNullOrEmpty(bdczh), (A, B, C, D, E, F, G) => D.BDCZH.Contains(bdczh))
                    .WhereIF(!string.IsNullOrEmpty(slbh), (A, B, C, D, E, F, G) => A.SLBH.Contains(slbh))
                    .WhereIF(!string.IsNullOrEmpty(qlrmc), (A, B, C, D, E, F, G) => C.QLRMC.Contains(C.QLRMC))
                    .WhereIF(!string.IsNullOrEmpty(zl), (A, B, C, D, E, F, G) => E.ZL.Contains(zl)).Where((A, B, C, D, E, F, G) => (A.LIFECYCLE == 0 || A.LIFECYCLE == null) && B.QLRLX == "权利人" && D.DJRQ != null && (D.ZSLX == "房屋不动产证" || D.ZSLX == "房产证") && D.BDCZH != null && D.ZSXLH != null).GroupBy((A, B, C, D, E, F, G) => new
                    {
                        tstybm = A.TSTYBM,
                        bdclx = A.BDCLX,
                        zslx = D.ZSLX,
                        slbh = D.SLBH,
                        bdczh = D.BDCZH,
                        bdcdyh = D.BDCDYH,
                        zl = E.ZL,
                        qllx = F.QLLX,
                        qlxz = F.QLXZ,
                        ghyt = F.GHYT,
                        tdqllx = G.QLLX,
                        tdqlxz = G.QLXZ,
                        tdghyt = G.TDYT,
                        jzmj = F.JZMJ,
                        tdmj = G.DYTDMJ
                    })
                    .Select((A, B, C, D, E, F, G) => new ConditionQueryResultVModel()
                    {
                        tstybm = A.TSTYBM,
                        bdclx = A.BDCLX,
                        zslx = D.ZSLX,
                        slbh = D.SLBH,
                        bdczh = D.BDCZH,
                        bdcdyh = D.BDCDYH,
                        qlrmc = SqlFunc.MappingColumn(C.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(C.QLRMC))"),
                        zjlb = SqlFunc.MappingColumn(B.ZJLB, "WM_CONCAT(DISTINCT TO_CHAR(B.ZJLB))"),
                        zl = E.ZL,
                        jzmj = F.JZMJ,
                        tdmj = G.DYTDMJ,
                        qllx = F.QLLX,
                        qlxz = F.QLXZ,
                        ghyt = F.GHYT,
                        tdqllx = G.QLLX,
                        tdqlxz = G.QLXZ,
                        tdghyt = G.TDYT
                    }).ToPageListAsync(intPageIndex, PageSize, totalCount);

            #endregion

            String[] TstybmArr = new String[HouseData.Count()];
            if (HouseData.Count > 0)
            {
                for (int i = 0; i < HouseData.Count; i++)
                {
                    TstybmArr[i] = HouseData[i].tstybm;
                }
            }
            //查询条件查询出房子的所有状态
            var djzlData = base.Db.Queryable<DJ_TSGL>().Where(i => TstybmArr.Contains(i.TSTYBM) && (i.LIFECYCLE == 0 || i.LIFECYCLE == null)).GroupBy(i => new { tstybm = i.TSTYBM }).Select(i => new
            {
                tstybm = i.TSTYBM,
                ZT = SqlFunc.MappingColumn(i.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(DJZL))")
            }).ToList();

            for (int i = 0; i < HouseData.Count; i++)
            {
                for (int j = 0; j < djzlData.Count; j++)
                {
                    var zjlb = string.Join(',', sysDic.Where(s => s.GID == 1 && HouseData[i].zjlb.Split(',').Contains(s.DEFINED_CODE))
                        .Select(s => s.DNAME).ToArray());
                    var fwqlxz = sysDic.Where(s => s.GID == 3 && s.DEFINED_CODE == HouseData[i].qlxz).FirstOrDefault();
                    var fwqllx = sysDic.Where(s => s.GID == 4 && s.DEFINED_CODE == HouseData[i].qllx).FirstOrDefault();
                    var Fwyt = sysDic.Where(s => s.GID == 5 && s.DEFINED_CODE == HouseData[i].ghyt).FirstOrDefault();
                    var tdQlxz = sysDic.Where(s => s.GID == 7 && s.DEFINED_CODE == HouseData[i].tdqlxz).FirstOrDefault();
                    var tdQllx = sysDic.Where(s => s.GID == 6 && s.DEFINED_CODE == HouseData[i].tdqllx).FirstOrDefault();
                    var tdGhyt = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == HouseData[i].tdghyt).FirstOrDefault();

                    HouseData[i].zjlb_zwm = zjlb != null ? zjlb : string.Empty;
                    HouseData[i].qllx_zwm = fwqllx != null ? fwqllx.DNAME : string.Empty;
                    HouseData[i].qlxz_zwm = fwqlxz != null ? fwqlxz.DNAME : string.Empty;
                    HouseData[i].ghyt_zwm = Fwyt != null ? Fwyt.DNAME : string.Empty;
                    HouseData[i].tdqlxz_zwm = tdQlxz != null ? tdQlxz.DNAME : string.Empty;
                    HouseData[i].tdqllx_zwm = tdQllx != null ? tdQllx.DNAME : string.Empty;
                    HouseData[i].tdghyt_zwm = tdGhyt != null ? tdGhyt.DNAME : string.Empty;
                    if (HouseData[i].tstybm == djzlData[j].tstybm)
                    {
                        HouseData[i].zt = djzlData[j].ZT;
                    }
                    var qlrList = await _dJ_QLRGLRepository.Query(qlr => qlr.SLBH == HouseData[i].slbh && qlr.QLRLX == "权利人");
                    foreach (var item in qlrList)
                    {
                        var ywr = sysDic.Where(s => s.GID ==1 && s.DEFINED_CODE == item.ZJLB).FirstOrDefault();
                        item.zjlb_zwm = ywr != null ? ywr.DNAME : string.Empty;
                    }
                    HouseData[i].qlrList = qlrList;
                }

            }
            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / PageSize.ObjToDecimal()).ObjToInt();
            pageModel.page = intPageIndex;
            pageModel.PageSize = PageSize;
            pageModel.dataCount = totalCount;
            pageModel.pageCount = pageCount;
            pageModel.data = HouseData;
            #endregion
            return pageModel;
        }
        /// <summary>
        /// 查询户信息
        /// </summary>
        /// <param name="tstybm">幢TSTYBM</param>
        /// <param name="intPageIndex">当前页标</param>
        /// <param name="PageSize">每页大小</param>
        /// <param name="bdcdyh">不动产单元号</param>
        /// <returns></returns>
        public async Task<PageModel<fc_hResultVModel>> GetFc_hResult(string tstybm, int intPageIndex, int PageSize, string bdcdyh)
        {
            RefAsync<int> totalCount = 0;
            //intPageIndex = 1;
            //PageSize = 20;            
            string pageDataJson = string.Empty;
            PageModel<fc_hResultVModel> PageModel = new PageModel<fc_hResultVModel>();
            List<fc_hResultVModel> modelList = new List<fc_hResultVModel>();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDic = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 1, 3, 4, 5, 6, 7, 8, 9 }).ToListAsync();  
            
            base.ChangeDB(SysConst.DB_CON_BDC);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            var HouseData = await base.Db.Queryable<FC_H_QSDC, ZD_QSDC, FC_Z_QSDC, V_DJ_TSGL>((A, B, C, D) => new Object[] { JoinType.Left, A.ZDTYBM == B.ZDTYBM, JoinType.Left, A.LSZTYBM == C.TSTYBM, JoinType.Left, A.TSTYBM == D.TSTYBM }).WhereIF(!string.IsNullOrEmpty(bdcdyh), (A, B, C, D) => A.BDCDYH.Contains(bdcdyh)).Where((A, B, C, D) => C.TSTYBM == tstybm  && (B.LIFECYCLE == 0 || B.LIFECYCLE == null) && (C.LIFECYCLE == 0 || C.LIFECYCLE == null)).GroupBy((A, B, C, D) => new
            {
                bdcdyh = A.BDCDYH,
                myc = A.MYC,
                tstybm = A.TSTYBM,
                zdtybm = A.ZDTYBM,
                qllx = A.QLLX,
                qlxz = A.QLXZ,
                ghyt = A.GHYT,
                qllx_zwm = A.QLLX,
                qlxz_zwm = A.QLXZ,
                ghyt_zwm = A.GHYT,
                zcs = C.ZCS,
                CG = A.CG,
                jzmj = A.JZMJ,
                ycjzmj = A.YCJZMJ,
                tnjzmj = A.TNJZMJ,
                yctnjzmj = A.YCTNJZMJ,
                ftjzmj = A.FTJZMJ,
                YCFTJZMJ = A.YCFTJZMJ,
                tdqsrq = B.QSRQ,
                zzrq = B.ZZRQ,
                TDZZRQ = A.TDZZRQ,
                tdyt = A.TDYT,
                PZTDYT =B.PZTDYT,
                tdxz = A.TDQLXZ,
                QLXZ = B.QLXZ,
                tdlx = A.TDQLLX,
                QLLX = B.QLLX,                
                fjsm = A.FJSM,
                sm = B.SM,
                zdmj = B.FZMJ,
                fwjg = C.FWJG,
                fwjg_zwm = C.FWJG,
                zl = A.ZL,
                syqx = A.TDSYQX
            }).Select((A, B, C, D) => new fc_hResultVModel()
            {
                bdcdyh = A.BDCDYH,
                myc = A.MYC,
                tstybm = A.TSTYBM,
                zdtybm = A.ZDTYBM,
                qllx = A.QLLX,
                qlxz = A.QLXZ,
                ghyt = A.GHYT,
                qllx_zwm = A.QLLX,
                qlxz_zwm = A.QLXZ,
                ghyt_zwm = A.GHYT,
                zcs = SqlFunc.MappingColumn(C.ZCS, "NVL(A.CG, C.ZCS)"),
                jzmj = SqlFunc.MappingColumn(A.JZMJ, "NVL(A.JZMJ, A.YCJZMJ)"),
                tnjzmj = SqlFunc.MappingColumn(A.TNJZMJ, "NVL(A.TNJZMJ, A.YCTNJZMJ)"),
                ftjzmj = SqlFunc.MappingColumn(A.FTJZMJ, "NVL(A.FTJZMJ, A.YCFTJZMJ)"),
                tdqsrq = B.QSRQ,
                zzrq = SqlFunc.MappingColumn(A.TDZZRQ, "NVL(A.TDZZRQ, B.ZZRQ)"),
                tdyt = SqlFunc.MappingColumn(A.TDYT, "NVL(A.TDYT, B.PZTDYT)"),
                tdxz = SqlFunc.MappingColumn(A.TDQLXZ, "NVL(A.TDQLXZ, B.QLXZ)"),
                tdlx = SqlFunc.MappingColumn(A.TDQLLX, "NVL(A.TDQLLX, B.QLLX)"),
                tdyt_zwm = SqlFunc.MappingColumn(A.TDYT, "NVL(A.TDYT, B.PZTDYT)"),
                tdxz_zwm = SqlFunc.MappingColumn(A.TDQLXZ, "NVL(A.TDQLXZ, B.QLXZ)"),
                tdlx_zwm = SqlFunc.MappingColumn(A.TDQLLX, "NVL(A.TDQLLX, B.QLLX)"),
                fjsm = A.FJSM,
                sm = B.SM,
                zdmj = B.FZMJ,
                fwjg = C.FWJG,
                fwjg_zwm = C.FWJG,
                zl = A.ZL,
                syqx = A.TDSYQX
            }).ToPageListAsync(intPageIndex, PageSize, totalCount);

            String[] TstybmArr = new String[HouseData.Count()];
            if (HouseData.Count > 0)
            {
                for (int i = 0; i < HouseData.Count; i++)
                {
                    TstybmArr[i] = HouseData[i].tstybm;
                }
            }
            //查询条件查询出房子的所有状态
            var djzlData = base.Db.Queryable<DJ_TSGL>().Where(i => TstybmArr.Contains(i.TSTYBM) && (i.LIFECYCLE == 0 || i.LIFECYCLE == null) && i.DJZL != "查封注销" && i.DJZL != "抵押注销" && i.DJZL != "异议注销" && i.DJZL != "预告注销").GroupBy(i => new { tstybm = i.TSTYBM }).Select(i => new
            {
                tstybm = i.TSTYBM,
                ZT = SqlFunc.MappingColumn(i.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(DJZL))")
            }).ToList();
            if(djzlData.Count > 0)
            {
                //给字典值赋值中文名
                for (int i = 0; i < HouseData.Count; i++)
                {
                    fc_hResultVModel model = new fc_hResultVModel();
                    for (int j = 0; j < djzlData.Count; j++)
                    {
                        var fwqlxz = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].qlxz && s.GID == 3).FirstOrDefault();
                        var fwqllx = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].qllx && s.GID == 4).FirstOrDefault();
                        var Fwyt = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].ghyt && s.GID == 5).FirstOrDefault();
                        var Fwjg = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].fwjg && s.GID == 9).FirstOrDefault();
                        var tdQllx = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].tdlx && s.GID == 6).FirstOrDefault();
                        var tdQlxz = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].tdxz && s.GID == 7).FirstOrDefault();
                        var tdGhyt = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].tdyt && s.GID == 8).FirstOrDefault();

                        HouseData[i].qllx_zwm = fwqllx != null ? fwqllx.DNAME : string.Empty;
                        HouseData[i].qlxz_zwm = fwqlxz != null ? fwqlxz.DNAME : string.Empty;
                        HouseData[i].ghyt_zwm = Fwyt != null ? Fwyt.DNAME : string.Empty;
                        HouseData[i].fwjg_zwm = Fwjg != null ? Fwjg.DNAME : string.Empty;
                        HouseData[i].tdxz_zwm = tdQlxz != null ? tdQlxz.DNAME : string.Empty;
                        HouseData[i].tdlx_zwm = tdQllx != null ? tdQllx.DNAME : string.Empty;
                        HouseData[i].tdyt_zwm = tdGhyt != null ? tdGhyt.DNAME : string.Empty;
                        HouseData[i].fj = HouseData[i].fjsm + HouseData[i].sm;
                        model.zl = HouseData[i].zl;
                        model.qzSlbh = HouseData[i].qzSlbh;
                        model.bdcdyh = HouseData[i].bdcdyh;
                        model.myc = HouseData[i].myc;
                        model.tstybm = HouseData[i].tstybm;
                        model.zdtybm = HouseData[i].zdtybm;
                        model.qllx = HouseData[i].qllx;
                        model.qllx_zwm = HouseData[i].qllx_zwm;
                        model.qlxz = HouseData[i].qlxz;
                        model.qlxz_zwm = HouseData[i].qlxz_zwm;
                        model.ghyt = HouseData[i].ghyt;
                        model.ghyt_zwm = HouseData[i].ghyt_zwm;
                        model.zcs = HouseData[i].zcs;
                        model.jzmj = HouseData[i].jzmj;
                        model.tnjzmj = HouseData[i].tnjzmj;
                        model.ftjzmj = HouseData[i].ftjzmj;
                        model.tdqsrq = HouseData[i].tdqsrq;
                        model.zzrq = HouseData[i].zzrq;
                        model.tdlx = HouseData[i].tdlx;
                        model.tdlx_zwm = HouseData[i].tdlx_zwm;
                        model.tdxz = HouseData[i].tdxz;
                        model.tdxz_zwm = HouseData[i].tdxz_zwm;
                        model.tdyt = HouseData[i].tdyt;
                        model.tdyt_zwm = HouseData[i].tdyt_zwm;
                        model.zdmj = HouseData[i].zdmj;
                        model.fwjg = HouseData[i].fwjg;
                        model.fwjg_zwm = HouseData[i].fwjg_zwm;
                        if (model.fwjg_zwm == "钢和钢筋混凝土结构" || model.fwjg_zwm == "钢筋混凝土结构")
                        {
                            model.fwjg_sw = "钢混结构";
                        }
                        else if (model.fwjg_zwm == "钢结构")
                        {
                            model.fwjg_sw = "钢筋结构";
                        }
                        else if (model.fwjg_zwm == "混合结构")
                        {
                            model.fwjg_sw = "混合结构";
                        }
                        else if (model.fwjg_zwm == "砖木结构")
                        {
                            model.fwjg_sw = "砖木结构";
                        }
                        else if (model.fwjg_zwm == "其他结构或简易材料结构")
                        {
                            model.fwjg_sw = "其它结构";
                        }

                        if (HouseData[i].tstybm == djzlData[j].tstybm)
                        {
                            model.zt = djzlData[j].ZT;
                        }
                    }
                    modelList.Add(model);
                }
            }
            else
            {
                for (int i = 0; i < HouseData.Count; i++)
                {
                    fc_hResultVModel model = new fc_hResultVModel();
                    var fwqlxz = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].qlxz && s.GID == 3).FirstOrDefault();
                    var fwqllx = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].qllx && s.GID == 4).FirstOrDefault();
                    var Fwyt = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].ghyt && s.GID == 5).FirstOrDefault();
                    var Fwjg = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].fwjg && s.GID == 9).FirstOrDefault();
                    var tdQllx = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].tdlx && s.GID == 6).FirstOrDefault();
                    var tdQlxz = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].tdxz && s.GID == 7).FirstOrDefault();
                    var tdGhyt = sysDic.Where(s => s.DEFINED_CODE == HouseData[i].tdyt && s.GID == 8).FirstOrDefault();

                    HouseData[i].qllx_zwm = fwqllx != null ? fwqllx.DNAME : string.Empty;
                    HouseData[i].qlxz_zwm = fwqlxz != null ? fwqlxz.DNAME : string.Empty;
                    HouseData[i].ghyt_zwm = Fwyt != null ? Fwyt.DNAME : string.Empty;
                    HouseData[i].fwjg_zwm = Fwjg != null ? Fwjg.DNAME : string.Empty;
                    HouseData[i].tdxz_zwm = tdQlxz != null ? tdQlxz.DNAME : string.Empty;
                    HouseData[i].tdlx_zwm = tdQllx != null ? tdQllx.DNAME : string.Empty;
                    HouseData[i].tdyt_zwm = tdGhyt != null ? tdGhyt.DNAME : string.Empty;
                    HouseData[i].fj = HouseData[i].fjsm + HouseData[i].sm;
                    model.zl = HouseData[i].zl;
                    model.qzSlbh = HouseData[i].qzSlbh;
                    model.bdcdyh = HouseData[i].bdcdyh;
                    model.myc = HouseData[i].myc;
                    model.tstybm = HouseData[i].tstybm;
                    model.zdtybm = HouseData[i].zdtybm;
                    model.qllx = HouseData[i].qllx;
                    model.qllx_zwm = HouseData[i].qllx_zwm;
                    model.qlxz = HouseData[i].qlxz;
                    model.qlxz_zwm = HouseData[i].qlxz_zwm;
                    model.ghyt = HouseData[i].ghyt;
                    model.ghyt_zwm = HouseData[i].ghyt_zwm;
                    model.zcs = HouseData[i].zcs;
                    model.jzmj = HouseData[i].jzmj;
                    model.tnjzmj = HouseData[i].tnjzmj;
                    model.ftjzmj = HouseData[i].ftjzmj;
                    model.tdqsrq = HouseData[i].tdqsrq;
                    model.zzrq = HouseData[i].zzrq;
                    model.tdlx = HouseData[i].tdlx;
                    model.tdlx_zwm = HouseData[i].tdlx_zwm;
                    model.tdxz = HouseData[i].tdxz;
                    model.tdxz_zwm = HouseData[i].tdxz_zwm;
                    model.tdyt = HouseData[i].tdyt;
                    model.tdyt_zwm = HouseData[i].tdyt_zwm;
                    model.zdmj = HouseData[i].zdmj;
                    model.fwjg = HouseData[i].fwjg;
                    model.fwjg_zwm = HouseData[i].fwjg_zwm;
                    if (model.fwjg_zwm == "钢和钢筋混凝土结构" || model.fwjg_zwm == "钢筋混凝土结构")
                    {
                        model.fwjg_sw = "钢混结构";
                    }
                    else if (model.fwjg_zwm == "钢结构")
                    {
                        model.fwjg_sw = "钢筋结构";
                    }
                    else if (model.fwjg_zwm == "混合结构")
                    {
                        model.fwjg_sw = "混合结构";
                    }
                    else if (model.fwjg_zwm == "砖木结构")
                    {
                        model.fwjg_sw = "砖木结构";
                    }
                    else if (model.fwjg_zwm == "其他结构或简易材料结构")
                    {
                        model.fwjg_sw = "其它结构";
                    }

                    model.zt = "";
                    modelList.Add(model);
                }                
            }
            
            
            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / PageSize.ObjToDecimal()).ObjToInt();
            PageModel.page = intPageIndex;
            PageModel.PageSize = PageSize;
            PageModel.pageCount = pageCount;
            PageModel.dataCount = totalCount;
            PageModel.data = modelList;

            return PageModel;
        }

        /// <summary>
        /// 验证是否是数字
        /// </summary>
        /// <param name="strVal"></param>
        /// <returns></returns>
        private bool IsNumberic(string strVal)
        {
            try
            {
                int var1 = Convert.ToInt32(strVal);
                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 转移登记数据提交
        /// </summary>
        /// <param name="StrInsertModel">不动产信息</param>
        /// <param name="StrTaxModel">税务信息</param>
        /// <param name="fileList">附件信息</param>
        /// <returns></returns>
        public async Task<string> InsertTransferPost(InsertTransferVModel StrInsertModel, InsertTaxVModel StrTaxModel, List<PUB_ATT_FILE> fileList)
        {
            string MsgInfo = "";
            string qlrmc = "";
            int isActonOK = 100;
            bool isSubmitWorkFlow = StrInsertModel.commandtype == 1;
            bool isInsert = string.IsNullOrEmpty(StrInsertModel.xid);
            #region 实例化实体类
            REGISTRATION_INFO registration = new REGISTRATION_INFO();
            SysDataRecorderModel RecorderModel = new SysDataRecorderModel();
            SJD_INFO sjdModel = new SJD_INFO();
            SPB_INFO SpbModel = new SPB_INFO();
            DJB_INFO DjbModel = new DJB_INFO();
            SFD_INFO SfdModel = new SFD_INFO();
            List<SFD_FB_INFO> sfdfbList = new List<SFD_FB_INFO>();
            SFD_FB_INFO model = null;
            XGDJGL_INFO xgdjglModel = new XGDJGL_INFO();
            TSGL_INFO TsglModel = new TSGL_INFO();
            QL_XG_INFO QlModel = new QL_XG_INFO();
            FWXG_INFO fwxgModel = new FWXG_INFO();
            TDXG_INFO tdxgModel = new TDXG_INFO();
            BankAuthorize bankModel = new BankAuthorize();
            IFLOW_DO_ACTION do_actionModel = new IFLOW_DO_ACTION();
            QLRGL_INFO qlrModel = null;
            QLRGL_INFO ywrModel = null;
            InsertTransferAndTaxVModel JsonModel = new InsertTransferAndTaxVModel();
            #endregion
            JsonModel.transferModel = StrInsertModel;
            JsonModel.taxModel = StrTaxModel;
            DateTime sjsj = DateTime.Now;
            if (!string.IsNullOrEmpty(StrInsertModel.sjsj))
            {
                sjsj = Convert.ToDateTime(StrInsertModel.sjsj);
            }
                DateTime cnsj = DateTime.Now;
            if (!string.IsNullOrEmpty(StrInsertModel.cnsj))
            {
                cnsj = Convert.ToDateTime(StrInsertModel.cnsj);
            }
            #region 为实体类赋值
            string xid = Provider.Sql.Create().ToString();//主键
            #region JOSN序列化
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            var jsonstring = JsonSerializer.Serialize(JsonModel, options);
            #endregion
            #region 收件单赋值
            sjdModel.SLBH = StrInsertModel.newSlbh;
            sjdModel.DJDL = "200";
            sjdModel.SJSJ = sjsj;
            sjdModel.CNSJ = cnsj;
            sjdModel.LCMC = "转移登记";
            sjdModel.LCLX = "国有建设用地使用权及房屋所有权登记";
            sjdModel.ZL = StrInsertModel.zl;
            sjdModel.SJR = StrInsertModel.sjslry;
            sjdModel.QXDM = StrInsertModel.sjbdcszqy.Trim();
            sjdModel.TZRXM = StrInsertModel.sjtzrmc;
            sjdModel.TZRDH = StrInsertModel.sjtzrdh;
            sjdModel.JHXTID = StrInsertModel.sjjhxtid;
            sjdModel.PRJID = StrInsertModel.newSlbh;
            sjdModel.XID = xid;
            #endregion
            #region 审批表赋值
            //SpbModel.SPBH = Provider.Sql.Create().ToString();
            //SpbModel.SLBH = StrInsertModel.newSlbh;
            //SpbModel.SPDX = "初审意见";
            //SpbModel.SPYJ = StrInsertModel.spcsyj;
            //SpbModel.SPR = StrInsertModel.spspr;
            //if (!string.IsNullOrEmpty(StrInsertModel.spsprq))
            //{
            //    SpbModel.SPRQ = Convert.ToDateTime(StrInsertModel.spsprq);
            //}            
            //SpbModel.SPTXR = StrInsertModel.spspr;  //  需要填写登陆人的用户名即admin
            //SpbModel.XID = xid;
            #endregion
            #region 登记簿赋值
            DjbModel.SLBH = StrInsertModel.newSlbh;
            DjbModel.DJLX = sjdModel.DJDL;
            DjbModel.DJYY = StrInsertModel.djyy;
            DjbModel.SQFBCZ = "否";
            DjbModel.SQRQ = sjsj;
            DjbModel.QT = StrInsertModel.FWQLQTZK;
            DjbModel.FJ = StrInsertModel.FWFJ;
            DjbModel.XGZH = StrInsertModel.xgzh; //需要添加相关证号
            DjbModel.BDCDYH = StrInsertModel.selectHouse[0].bdcdyh;
            DjbModel.GYFS = StrInsertModel.SelectGYFS;
            DjbModel.SPBZ = StrInsertModel.spspbz;
            DjbModel.SZQY = StrInsertModel.sjbdcszqy;
            DjbModel.LIFECYCLE = -1;
            DjbModel.xid = xid;
            #endregion
            #region 收费单赋值
            //SfdModel.SLBH = StrInsertModel.newSlbh;
            //SfdModel.JFBH = StrInsertModel.sfbh;
            //SfdModel.XMMC = StrInsertModel.sfdwradio + sjdModel.LCMC;
            //SfdModel.TXDZ = sjdModel.ZL;
            //SfdModel.DH = sjdModel.TZRDH;
            //SfdModel.JFLX = StrInsertModel.sfjflx;
            //SfdModel.JBR = sjdModel.SJR;
            //SfdModel.JBRQ = sjsj;
            //SfdModel.JFDW = StrInsertModel.sfdwradio;
            //if (!string.IsNullOrEmpty(StrInsertModel.sfyfje))
            //{
            //    SfdModel.YSJE = Convert.ToDecimal(StrInsertModel.sfyfje);//前台传
            //}
            //if (!string.IsNullOrEmpty(StrInsertModel.sfsjje))
            //{
            //    SfdModel.SSJE = Convert.ToDecimal(StrInsertModel.sfsjje);     //前台传
            //}
            
            //SfdModel.JZJF = StrInsertModel.sfinpjfxs;
            //SfdModel.XID = xid;
            #endregion
            #region 收费单副本赋值
            //foreach (var item in StrInsertModel.sfdfbList)
            //{
            //    model = new SFD_FB_INFO();
            //    model.CWSFDXBBH = Guid.NewGuid().ToString();
            //    model.SLBH = StrInsertModel.newSlbh;
            //    model.SFXM = item.dname;    //收费项目
            //    model.JLDW = "元/本";
            //    model.SL = item.is_used;   //需要前台传：数量
            //    model.SFBZ = Convert.ToDecimal(item.defined_code);     //需要前台传：收费标准
            //    model.HSJE = item.mark;     //需要前台传：核收金额
            //    model.XID = xid;
            //    sfdfbList.Add(model);
            //}
            #endregion
            #region 相关登记关联赋值
            xgdjglModel.BGBM = Provider.Sql.Create().ToString();
            xgdjglModel.ZSLBH = StrInsertModel.newSlbh;
            xgdjglModel.FSLBH = StrInsertModel.slbh;
            xgdjglModel.BGRQ = sjsj;
            xgdjglModel.BGLX = "权属变更";   //变更类型需要添什么？
            xgdjglModel.XGZLX = StrInsertModel.zslx;  //证书类型哪里取？
            xgdjglModel.XGZH = StrInsertModel.xgzh;
            xgdjglModel.XID = xid;
            #endregion
            #region 图属关联赋值
            //LIFECYCLE 赋值为-1
            TsglModel.GLBM = Provider.Sql.Create().ToString();
            TsglModel.TSTYBM = StrInsertModel.selectHouse[0].tstybm;
            TsglModel.SLBH = StrInsertModel.newSlbh;
            TsglModel.BDCLX = StrInsertModel.bdclx;
            TsglModel.BDCDYH = StrInsertModel.selectHouse[0].bdcdyh;
            TsglModel.DJZL = "权属";
            TsglModel.CSSJ = sjsj;
            TsglModel.XID = xid;
            TsglModel.LIFECYCLE = -1;
            #endregion
            #region 新权利相关表QL_XG_INFO
            QlModel.XTBH = Provider.Sql.Create().ToString();
            QlModel.SLBH = StrInsertModel.newSlbh;
            QlModel.FW_QLLX = StrInsertModel.selectHouse[0].qllx;
            QlModel.FW_QLLX_ZWM = StrInsertModel.selectHouse[0].qllx_zwm;
            QlModel.FW_QLXZ = StrInsertModel.selectHouse[0].qlxz;
            QlModel.FW_QLXZ_ZWMS = StrInsertModel.selectHouse[0].qlxz_zwm;
            QlModel.FW_JZMJ = StrInsertModel.selectHouse[0].jzmj;
            QlModel.FW_TNJZMJ = StrInsertModel.selectHouse[0].tnjzmj;
            QlModel.FW_FWGHYT = StrInsertModel.selectHouse[0].ghyt;
            QlModel.FW_FWGHYT_ZWM = StrInsertModel.selectHouse[0].ghyt_zwm;
            QlModel.TD_QLLX = StrInsertModel.selectHouse[0].tdlx;
            QlModel.TD_QLLX_ZWM = StrInsertModel.selectHouse[0].tdlx_zwm;
            QlModel.TD_QLXZ = StrInsertModel.selectHouse[0].tdxz;
            QlModel.TD_QLXZ_ZWMS = StrInsertModel.selectHouse[0].tdxz_zwm;
            QlModel.TD_DYTDMJ = StrInsertModel.selectHouse[0].zdmj;
            QlModel.TD_JZZDMJ = Convert.ToDecimal(StrInsertModel.FWZDMJ);
            QlModel.TD_SYQX = StrInsertModel.selectHouse[0].syqx;
            QlModel.TD_QSRQ = StrInsertModel.selectHouse[0].qsrq;
            QlModel.TD_ZZRQ = StrInsertModel.selectHouse[0].zzrq;
            QlModel.TD_TDYT = StrInsertModel.selectHouse[0].tdyt;
            QlModel.TD_TDYT_ZWMS = StrInsertModel.selectHouse[0].tdyt_zwm;
            QlModel.XID = xid;
            #endregion
            #region QL_FWXG赋值
            //fwxgModel.QLBH = Provider.Sql.Create().ToString();
            //fwxgModel.SLBH = StrInsertModel.newSlbh;
            //fwxgModel.QLLX = StrInsertModel.selectHouse[0].qllx;
            //fwxgModel.QLXZ = StrInsertModel.selectHouse[0].qlxz;
            //fwxgModel.JZMJ = StrInsertModel.selectHouse[0].jzmj;
            //fwxgModel.TNJZMJ = StrInsertModel.selectHouse[0].tnjzmj;
            //fwxgModel.FTJZMJ = StrInsertModel.selectHouse[0].ftjzmj;
            //fwxgModel.GHYT = StrInsertModel.selectHouse[0].ghyt;
            //fwxgModel.xid = xid;
            #endregion
            #region QL_TDXG赋值
            //tdxgModel.QLBH = Provider.Sql.Create().ToString();
            //tdxgModel.SLBH = StrInsertModel.newSlbh;
            //tdxgModel.QLLX = StrInsertModel.selectHouse[0].tdlx;
            //tdxgModel.QLXZ = StrInsertModel.selectHouse[0].tdxz;
            //tdxgModel.TDYT = StrInsertModel.selectHouse[0].tdyt;
            //tdxgModel.DYTDMJ = StrInsertModel.selectHouse[0].zdmj;
            //tdxgModel.XID = xid;
            #endregion
            #region BankAuthorize赋值
            bankModel.BID = Provider.Sql.Create().ToString();
            bankModel.AUTHORIZATIONDATE = sjsj;
            bankModel.STATUS = 106;
            #endregion
            #region REGISTRATION_INFO赋值
            if(StrInsertModel.SelectQlrList.Count > 0)
            {
                foreach (var item in StrInsertModel.SelectQlrList)
                {
                    qlrmc += item.qlrmc + ",";
                }
                qlrmc = qlrmc.Substring(0, qlrmc.Length - 1);
            }
            registration.XID = xid;
            registration.SLBH = StrInsertModel.slbh;
            registration.YWSLBH = StrInsertModel.newSlbh;
            registration.DJZL = 22;
            registration.AUZ_ID = bankModel.BID;
            registration.REMARK2 = "转移登记";
            registration.USER_ID = StrInsertModel.orgId;
            //registration.SJR = StrInsertModel.sjslry;
            registration.SAVEDATE = sjsj;
            registration.ZL = StrInsertModel.zl;
            registration.QLRMC = qlrmc;
            registration.BDCZH = StrInsertModel.xgzh;
            #endregion
            #region 插入Json                    
            RecorderModel.PK = Provider.Sql.Create().ToString();
            RecorderModel.BUS_PK = xid;
            RecorderModel.USER_ID = StrInsertModel.orgId;
            RecorderModel.USER_NAME = StrInsertModel.sjslry;
            RecorderModel.SAVEDATAJSON = jsonstring;
            RecorderModel.CDATE = sjsj;
            RecorderModel.IS_STOP = 0;
            #endregion
            #region IFLOW_DO_ACTION赋值
            do_actionModel.PK = Provider.Sql.Create().ToString();
            do_actionModel.FLOW_ID = 106;
            do_actionModel.AUZ_ID = bankModel.BID;
            do_actionModel.CDATE = sjsj;

            do_actionModel.USER_NAME = StrInsertModel.sjslry;
            #endregion
            #region 权利人义务人赋值
            List<QLRGL_INFO> qlrList = new List<QLRGL_INFO>();
            List<QLRGL_INFO> ywrList = new List<QLRGL_INFO>();

            foreach (var item in StrInsertModel.SelectQlrList)
            {
                qlrModel = new QLRGL_INFO();
                qlrModel.GLBM = Provider.Sql.Create().ToString();
                qlrModel.SLBH = StrInsertModel.newSlbh;
                qlrModel.YWBM = StrInsertModel.newSlbh;
                qlrModel.QLRID = Provider.Sql.Create().ToString();
                qlrModel.GYFS = StrInsertModel.SelectGYFS;
                qlrModel.GYFE = item.gyfe;
                qlrModel.QLRLX = "权利人";
                qlrModel.QLRMC = item.qlrmc;
                qlrModel.ZJHM = item.zjhm;
                qlrModel.ZJLB = item.zjlb;
                qlrModel.SXH = item.sxh;
                qlrModel.XID = xid;
                qlrModel.ZJLB_ZWM = item.zjlb_zwm;
                qlrList.Add(qlrModel);
            }

            foreach (var item in StrInsertModel.SelectywrList)
            {
                ywrModel = new QLRGL_INFO();
                ywrModel.GLBM = Provider.Sql.Create().ToString();
                ywrModel.SLBH = StrInsertModel.newSlbh;
                ywrModel.YWBM = StrInsertModel.newSlbh;
                ywrModel.QLRID = Provider.Sql.Create().ToString();
                ywrModel.GYFS = item.gyfs;
                ywrModel.GYFE = item.gyfe;
                ywrModel.QLRLX = "义务人";
                ywrModel.QLRMC = item.qlrmc;
                ywrModel.ZJHM = item.zjhm;
                ywrModel.ZJLB = item.zjlb;
                ywrModel.SXH = item.sxh;
                ywrModel.XID = xid;
                ywrModel.ZJLB_ZWM = item.zjlb_zwm;
                qlrList.Add(ywrModel);
            }
            #endregion

            if(fileList !=null && fileList.Count > 0)
            {
                foreach (var file in fileList)
                {
                    file.XID = xid;
                }
            }
            
            #endregion

            try
            {
                if(isSubmitWorkFlow)    //true时，当前任务为提交当前流程。false时，当前任务为暂存数据
                {
                    registration.IS_ACTION_OK = 1;
                    isActonOK = 1;
                } 
                else
                {
                    registration.IS_ACTION_OK = 0;
                    isActonOK = 0;
                }
                if (isInsert)   //当为true时，xid为null,进行insert操作。为false时，存在xid,进行update操作
                {

                    #region
                    _dBTransManagement.BeginTran();
                    var bankCount = _bankAuthorizeRepository.Add(bankModel).Result;
                    var RegCount = _registration_InfoRepository.Add(registration).Result;                    
                    var RecorderCount = _sysDataRecorderRepository.Add(RecorderModel).Result;
                    var ActionCount = _dO_ACTIONRepository.Add(do_actionModel).Result;
                    var sjdCount = _sjdInfoRepository.Add(sjdModel).Result;
                    var djbCount = _djbInfoRepository.Add(DjbModel).Result;
                    //var spbCount = _spbInfoRepository.Add(SpbModel).Result;
                    //var sfdCount = _sfdInfoRepository.Add(SfdModel).Result;
                    //if(sfdfbList != null && sfdfbList.Count > 0)
                    //{
                    //    var sfdfbCount = _sfdFbInfoRepository.Add(sfdfbList).Result;
                    //}                    
                    var tsglCount = _tsglRepository.Add(TsglModel).Result;
                    var xgdjCount = _xgdjgl_InfoRepository.Add(xgdjglModel).Result;
                    var qlxgCount = _qlxgRepository.Add(QlModel);
                    var qlrCount = _qlrgl_InfoRepository.Add(qlrList).Result;
                    if(fileList != null && fileList.Count > 0)
                    {
                        foreach (var item in fileList)
                        {
                            item.XID = xid;
                        }
                        var fileCount = _fileRepository.Add(fileList).Result;
                        _dBTransManagement.CommitTran();
                        MsgInfo = @$"{{""XID"":""{xid}"",""AUZ_ID"":""{bankModel.BID}"",""JSON_PK"":""{xid}""}}";
                    } 
                    else
                    {
                        if(isActonOK == 1)  //提交完成时，附件不能为空
                        {
                            MsgInfo = $@"{{""error"":""请上传附件信息。""}}";
                            _dBTransManagement.RollbackTran();
                        }
                        else
                        {
                            _dBTransManagement.CommitTran();
                            MsgInfo = @$"{{""XID"":""{xid}"",""AUZ_ID"":""{bankModel.BID}"",""JSON_PK"":""{xid}""}}";
                        }
                    }
                    
                    #endregion
                }
                else
                {
                    try
                    {
                        //存在XID的时候，去查询此条XID是否存在，存在的话读取出status和Bid
                        var data = await base.Db.Queryable<REGISTRATION_INFO, BankAuthorize>((A, B) => A.AUZ_ID == B.BID).Where((A, B) => A.XID == StrInsertModel.xid).Select((A, B) => new
                        {
                            bid = B.BID,
                            status = B.STATUS,
                            xid = A.XID,
                            isActionOk = A.IS_ACTION_OK
                        }).ToListAsync();
                        if(data.Count > 0)
                        {
                            if(data[0].status == 109 && data[0].isActionOk == 2)   //当状态为106并且isActionOK=2为转移登记审批退回，否则为加载数据
                            {
                                try
                                {
                                    var OldXid = data[0].xid;
                                    registration.AUZ_ID = data[0].bid;
                                    //string NewXid = Provider.Sql.Create().ToString();//主键
                                    //被退回的登记，需要先将新的Xid更新到REGISTRATION_INFO的Next_xid更新为NewXid,同时更新BankAuthorize的status为106

                                    _dBTransManagement.BeginTran();
                                    #region 执行更新操作
                                    var Regdata = await base.Db.Queryable<REGISTRATION_INFO>().Where(it => it.XID == OldXid).ToListAsync();
                                    if(Regdata.Count > 0)
                                    {
                                        Regdata[0].NEXT_XID = xid;
                                        Regdata[0].IS_ACTION_OK = isActonOK;
                                        registration.AUZ_ID = Regdata[0].AUZ_ID;
                                    }
                                    var updateReg = _registration_InfoRepository.Update(Regdata[0]).Result;

                                    var RecorderData = await base.Db.Queryable<SysDataRecorderModel>().Where(it => it.BUS_PK == OldXid).ToListAsync();
                                    if(RecorderData.Count > 0)
                                    {
                                        RecorderData[0].BUS_PK = xid;
                                        RecorderData[0].SAVEDATAJSON = jsonstring;
                                    }
                                    var updateRecorder = _sysDataRecorderRepository.Update(RecorderData[0]).Result;

                                    var BankData = await base.Db.Queryable<BankAuthorize>().Where(it => it.BID == registration.AUZ_ID).ToListAsync();
                                    if (BankData.Count > 0)
                                    {
                                        BankData[0].STATUS = 200;
                                    }
                                    var updateBank = _bankAuthorizeRepository.Update(BankData[0]).Result;
                                    #endregion

                                    #region 执行删除操作
                                    var deleteSjd = base.Db.Deleteable<SJD_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();
                                    //var deleteSpb = base.Db.Deleteable<SPB_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();
                                    var deleteDjb = base.Db.Deleteable<DJB_INFO>().Where(it => it.xid == OldXid).ExecuteCommand();
                                    //var deleteSfd = base.Db.Deleteable<SFD_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();
                                    //var deleteSfdfb = base.Db.Deleteable<SFD_FB_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();
                                    var deleteXgdjgl = base.Db.Deleteable<XGDJGL_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();
                                    var deleteTsgl = base.Db.Deleteable<TSGL_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();
                                    var deleteQlxg = base.Db.Deleteable<QL_XG_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();
                                    var deleteQlrgl = base.Db.Deleteable<QLRGL_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();                             
                                    #endregion

                                    #region 执行插入操作 
                                    
                                    var regCount = _registration_InfoRepository.Add(registration).Result;
                                    var ActionCount = _dO_ACTIONRepository.Add(do_actionModel).Result;
                                    var sjdCount = _sjdInfoRepository.Add(sjdModel).Result;
                                    var djbCount = _djbInfoRepository.Add(DjbModel).Result;
                                    //var spbCount = _spbInfoRepository.Add(SpbModel).Result;
                                    //var sfdCount = _sfdInfoRepository.Add(SfdModel).Result;
                                    //var sfdfbCount = _sfdFbInfoRepository.Add(sfdfbList).Result;
                                    var tsglCount = _tsglRepository.Add(TsglModel).Result;
                                    var xgdjCount = _xgdjgl_InfoRepository.Add(xgdjglModel).Result;
                                    var qlxgCount = _qlxgRepository.Add(QlModel).Result;
                                    if (qlrList != null && qlrList.Count > 0)
                                    {
                                        foreach (var QlrItem in qlrList)
                                        {
                                            var qlrCount = _qlrgl_InfoRepository.Add(QlrItem).Result;
                                        }
                                    }                                    
                                    if (fileList != null && fileList.Count > 0)
                                    {
                                        foreach (var item in fileList)
                                        {
                                            item.XID = xid;
                                        }
                                        var fileCount = _fileRepository.Add(fileList).Result;
                                    }
                                    _dBTransManagement.CommitTran();
                                    MsgInfo = @$"{{""XID"":""{xid}"",""AUZ_ID"":""{bankModel.BID}"",""JSON_PK"":""{xid}""}}";

                                    #endregion
                                    _dBTransManagement.CommitTran();
                                }
                                catch (Exception ex)
                                {
                                    _dBTransManagement.RollbackTran();
                                    throw ex;
                                }                                
                            }
                            else
                            {
                                var oldXid = data[0].xid;
                                sjdModel.XID = oldXid;
                                DjbModel.xid = oldXid;
                                SpbModel.XID = oldXid;
                                SfdModel.XID = oldXid;
                                TsglModel.XID = oldXid;
                                xgdjglModel.XID = oldXid;
                                QlModel.XID = oldXid;
                                RecorderModel.BUS_PK = oldXid;
                                registration.IS_ACTION_OK = isActonOK;
                                #region 执行删除操作
                                _dBTransManagement.BeginTran();
                                //var deleteSfdfb = _sfdFbInfoRepository.DeleteById(oldXid); 
                                
                                //var deleteSfdfb = base.Db.Deleteable<SFD_FB_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteQlrgl = base.Db.Deleteable<QLRGL_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteSjd = base.Db.Deleteable<SJD_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteDjb = base.Db.Deleteable<DJB_INFO>().Where(it => it.xid == StrInsertModel.xid).ExecuteCommand();
                                //var deleteSpb = base.Db.Deleteable<SPB_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                //var deleteSfd = base.Db.Deleteable<SFD_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteTsgl = base.Db.Deleteable<TSGL_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteXgdjgl = base.Db.Deleteable<XGDJGL_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteQlxg = base.Db.Deleteable<QL_XG_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteJson = base.Db.Deleteable<SysDataRecorderModel>().Where(it => it.BUS_PK == StrInsertModel.xid).ExecuteCommand();
                                if (fileList != null && fileList.Count > 0)
                                {
                                    foreach (var FileItem in fileList)
                                    {
                                        var deleteFile = base.Db.Deleteable<PUB_ATT_FILE>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                    }
                                }
                                #endregion

                                #region 执行插入操作
                                if(sfdfbList.Count > 0)
                                {
                                    foreach (var item in sfdfbList)
                                    {
                                        item.XID = oldXid;
                                        //var insertSfdfb = _sfdFbInfoRepository.Add(item).Result;
                                        var insertSfdfb = base.Db.Insertable(item).ExecuteCommand();
                                    }                                    
                                }                               
                                if(qlrList.Count > 0)
                                {
                                    foreach (var item in qlrList)
                                    {
                                        item.XID = oldXid;
                                        var insertQlrgl = _qlrgl_InfoRepository.Add(item).Result;
                                    }
                                }
                                
                                                               
                                var insertSjd = _sjdInfoRepository.Add(sjdModel).Result;
                                var insertDjb = _djbInfoRepository.Add(DjbModel).Result;
                                //var insertSpb = _spbInfoRepository.Add(SpbModel).Result;
                                //var insertSfd = _sfdInfoRepository.Add(SfdModel).Result;
                                var insertTsgl = _tsglRepository.Add(TsglModel).Result;
                                var insertXgdjgl = _xgdjgl_InfoRepository.Add(xgdjglModel).Result;
                                var insertQlxg = _qlxgRepository.Add(QlModel).Result;
                                var insertJson = _sysDataRecorderRepository.Add(RecorderModel).Result;
                                if (fileList != null && fileList.Count > 0)
                                {
                                    foreach (var item in fileList)
                                    {
                                        item.XID = oldXid;
                                    }
                                    var fileCount = _fileRepository.Add(fileList).Result;
                                }
                                
                                #endregion

                                #region 执行更新Reg主表状态为提交
                                //var updateReg = _registration_InfoRepository.Update(registration).Result;
                                var count = base.Db.Updateable(registration).UpdateColumns(it => new
                                {
                                    it.IS_ACTION_OK
                                }).Where(S => S.XID == oldXid).ExecuteCommand();
                                _dBTransManagement.CommitTran();
                                #endregion
                                MsgInfo = @$"{{""XID"":""{StrInsertModel.xid}"",""AUZ_ID"":""{data[0].bid}"",""JSON_PK"":""{StrInsertModel.xid}""}}";
                            }
                        }
                        else
                        {
                            MsgInfo = "系统错误，请与管理员联系。";
                        }
                    }
                    catch (Exception ex)
                    {
                        _dBTransManagement.RollbackTran();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                _dBTransManagement.RollbackTran();
                throw ex;
                
            }

            return MsgInfo;
        }
        /// <summary>
        /// 获取税务房屋基本信息
        /// </summary>
        /// <param name="tstybm">FC_H的TSTYBM</param>
        /// <returns></returns>
        public async Task<fc_hResultVModel> GetHResult(string tstybm)
        {
            fc_hResultVModel model = new fc_hResultVModel();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var in1 = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 1, 3, 4,5,6,7,8,9 }).ToListAsync();
            var sysQlxzList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 3)).ToListAsync();    //房屋权利性质
            var sysQllxList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 4)).ToListAsync();    //房屋权利类型
            var sysFwytList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 5)).ToListAsync();    //房屋用途类型
            var systdQllxList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 6)).ToListAsync();    //土地权利类型
            var systdQlxzList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 7)).ToListAsync();    //土地权利性质
            var systdGhytList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 8)).ToListAsync();    //土地用途类型
            var sysFwjgList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 9)).ToListAsync();    //房屋结构

            base.ChangeDB(SysConst.DB_CON_BDC);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            var HouseData = await base.Db.Queryable<FC_H_QSDC, ZD_QSDC, FC_Z_QSDC, DJ_TSGL, DJ_DJB>((A, B, C, D, E) => new Object[] { JoinType.Left, A.ZDTYBM == B.ZDTYBM, JoinType.Left, A.LSZTYBM == C.TSTYBM, JoinType.Left, A.TSTYBM == D.TSTYBM, JoinType.Left, D.SLBH == E.SLBH }).Where((A, B, C, D, E) => A.TSTYBM == tstybm && D.DJZL != "异议注销" && D.DJZL != "预告注销" && D.DJZL != "查封注销" && D.DJZL != "抵押注销" && (D.LIFECYCLE == 0 || D.LIFECYCLE == null) && (B.LIFECYCLE == 0 || B.LIFECYCLE == null) && (C.LIFECYCLE == 0 || C.LIFECYCLE == null)).Select((A, B, C, D, E) => new fc_hResultVModel()
            {
                bdcdyh = A.BDCDYH,
                myc = A.MYC,
                qzSlbh = E.SLBH,
                tstybm = A.TSTYBM,
                zdtybm = A.ZDTYBM,
                qllx = A.QLLX,
                qlxz = A.QLXZ,
                ghyt = A.GHYT,
                zcs = SqlFunc.MappingColumn(C.ZCS, "NVL(A.CG, C.ZCS)"),
                jzmj = A.JZMJ,
                tnjzmj = A.TNJZMJ,
                ftjzmj = A.FTJZMJ,
                tdqsrq = B.QSRQ,
                zzrq = SqlFunc.MappingColumn(A.TDZZRQ, "NVL(A.TDZZRQ, B.ZZRQ)"),
                tdyt = SqlFunc.MappingColumn(A.TDYT, "NVL(A.TDYT, B.PZTDYT)"),
                tdxz = SqlFunc.MappingColumn(A.TDQLXZ, "NVL(A.TDQLXZ, B.QLXZ)"),
                tdlx = SqlFunc.MappingColumn(A.TDQLLX, "NVL(A.TDQLLX, B.QLLX)"),
                fjsm = A.FJSM,
                sm = B.SM,
                zdmj = B.FZMJ,
                fwjg = C.FWJG
            }).ToListAsync();

            for (int i = 0; i < HouseData.Count; i++)            {
                
                var fwqlxz = sysQlxzList.Where(s => s.DEFINED_CODE == HouseData[i].qlxz).FirstOrDefault();
                var fwqllx = sysQllxList.Where(s => s.DEFINED_CODE == HouseData[i].qllx).FirstOrDefault();
                var Fwyt = sysFwytList.Where(s => s.DEFINED_CODE == HouseData[i].ghyt).FirstOrDefault();
                var Fwjg = sysFwjgList.Where(s => s.DEFINED_CODE == HouseData[i].fwjg).FirstOrDefault();
                var tdQlxz = systdQlxzList.Where(s => s.DEFINED_CODE == HouseData[i].tdxz).FirstOrDefault();
                var tdQllx = systdQllxList.Where(s => s.DEFINED_CODE == HouseData[i].tdlx).FirstOrDefault();
                var tdGhyt = systdGhytList.Where(s => s.DEFINED_CODE == HouseData[i].tdyt).FirstOrDefault();
                HouseData[i].qllx = fwqllx != null ? fwqllx.DNAME : string.Empty;
                HouseData[i].qlxz = fwqlxz != null ? fwqlxz.DNAME : string.Empty;
                HouseData[i].ghyt = Fwyt != null ? Fwyt.DNAME : string.Empty;
                HouseData[i].fwjg = Fwjg != null ? Fwjg.DNAME : string.Empty;
                HouseData[i].tdxz = tdQlxz != null ? tdQlxz.DNAME : string.Empty;
                HouseData[i].tdlx = tdQllx != null ? tdQllx.DNAME : string.Empty;
                HouseData[i].tdyt = tdGhyt != null ? tdGhyt.DNAME : string.Empty;
                HouseData[i].fj = HouseData[i].fjsm + HouseData[i].sm;
                model.qzSlbh = HouseData[i].qzSlbh;
                model.bdcdyh = HouseData[i].bdcdyh;
                model.myc = HouseData[i].myc;
                model.tstybm = HouseData[i].tstybm;
                model.zdtybm = HouseData[i].zdtybm;
                model.qllx = HouseData[i].qllx;
                model.qlxz = HouseData[i].qlxz;
                model.ghyt = HouseData[i].ghyt;
                model.zcs = HouseData[i].zcs;
                model.jzmj = HouseData[i].jzmj;
                model.tnjzmj = HouseData[i].tnjzmj;
                model.ftjzmj = HouseData[i].ftjzmj;
                model.tdqsrq = HouseData[i].tdqsrq;
                model.zzrq = HouseData[i].zzrq;
                model.tdlx = HouseData[i].tdlx;
                model.tdxz = HouseData[i].tdxz;
                model.tdyt = HouseData[i].tdyt;
                model.zdmj = HouseData[i].zdmj;
                model.fwjg = HouseData[i].fwjg;
                if (model.fwjg == "钢和钢筋混凝土结构" || model.fwjg == "钢筋混凝土结构")
                {
                    model.fwjg_sw = "钢混结构";
                }
                else if (model.fwjg == "钢结构")
                {
                    model.fwjg_sw = "钢筋结构";
                }
                else if (model.fwjg == "混合结构")
                {
                    model.fwjg_sw = "混合结构";
                }
                else if (model.fwjg == "砖木结构")
                {
                    model.fwjg_sw = "砖木结构";
                }
                else if (model.fwjg == "其他结构或简易材料结构")
                {
                    model.fwjg_sw = "其它结构";
                }
            }

            return model;
        }

        /// <summary>
        /// 获取税务相关信息
        /// </summary>
        /// <param name="tstybm">房屋TSTYBM</param>
        /// <param name="xzqh">行政区划</param>
        /// <param name="zcs">总层数</param>
        /// <param name="ghyt">规划用途</param>
        /// <param name="jzmj">建筑面积</param>
        /// <returns></returns>
        public async Task<TaxVModel> GetTaxInfo(string tstybm,string xzqh, string zcs, string ghyt, decimal jzmj)
        {
            fc_hResultVModel model = new fc_hResultVModel();
            TaxVModel taxVModel = new TaxVModel();
            string itemNote = "";
            int TaxZcs = 0;
            if (IsNumberic(zcs))
            {
                TaxZcs = Convert.ToInt32(zcs);
            }
            base.ChangeDB(SysConst.DB_CON_IIRS);
            //1.证件类别、3.房屋权利性质、4.房屋权利类型、5.房屋用途类型、6.土地权利类型、7.土地权利性质、8.土地用途类型、9.房屋结构
            var dicList = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 1, 3, 4, 5, 6, 7, 8, 9 }).ToListAsync();

            base.ChangeDB(SysConst.DB_CON_BDC);

            #region 获取房屋基本信息
            var HouseData = await base.Db.Queryable<FC_H_QSDC, ZD_QSDC, FC_Z_QSDC, DJ_TSGL, DJ_DJB>((A, B, C, D, E) => new Object[] { JoinType.Left, A.ZDTYBM == B.ZDTYBM, JoinType.Left, A.LSZTYBM == C.TSTYBM, JoinType.Left, A.TSTYBM == D.TSTYBM, JoinType.Left, D.SLBH == E.SLBH }).Where((A, B, C, D, E) => A.TSTYBM == tstybm && D.DJZL != "异议注销" && D.DJZL != "预告注销" && D.DJZL != "查封注销" && D.DJZL != "抵押注销" && (D.LIFECYCLE == 0 || D.LIFECYCLE == null) && (B.LIFECYCLE == 0 || B.LIFECYCLE == null) && (C.LIFECYCLE == 0 || C.LIFECYCLE == null)).Select((A, B, C, D, E) => new fc_hResultVModel()
            {
                bdcdyh = A.BDCDYH,
                myc = A.MYC,
                qzSlbh = E.SLBH,
                tstybm = A.TSTYBM,
                zdtybm = A.ZDTYBM,
                qllx = A.QLLX,
                qlxz = A.QLXZ,
                ghyt = A.GHYT,
                zcs = SqlFunc.MappingColumn(C.ZCS, "NVL(A.CG, C.ZCS)"),
                jzmj = A.JZMJ,
                tnjzmj = A.TNJZMJ,
                ftjzmj = A.FTJZMJ,
                tdqsrq = B.QSRQ,
                zzrq = SqlFunc.MappingColumn(A.TDZZRQ, "NVL(A.TDZZRQ, B.ZZRQ)"),
                tdyt = SqlFunc.MappingColumn(A.TDYT, "NVL(A.TDYT, B.PZTDYT)"),
                tdxz = SqlFunc.MappingColumn(A.TDQLXZ, "NVL(A.TDQLXZ, B.QLXZ)"),
                tdlx = SqlFunc.MappingColumn(A.TDQLLX, "NVL(A.TDQLLX, B.QLLX)"),
                fjsm = A.FJSM,
                sm = B.SM,
                zdmj = B.FZMJ,
                fwjg = C.FWJG,
                zl = A.ZL,
                zh = A.ZH,
                dyh = A.DYH,
                fjh = A.FJH
            }).ToListAsync();

            for (int i = 0; i < HouseData.Count; i++)
            {                
                var fwqlxz = dicList.Where(s => s.GID == 3 && s.DEFINED_CODE == HouseData[i].qlxz).FirstOrDefault();
                var fwqllx = dicList.Where(s => s.GID == 4 && s.DEFINED_CODE == HouseData[i].qllx).FirstOrDefault();
                var Fwyt = dicList.Where(s => s.GID == 5 && s.DEFINED_CODE == HouseData[i].ghyt).FirstOrDefault();
                var Fwjg = dicList.Where(s => s.GID == 9 && s.DEFINED_CODE == HouseData[i].fwjg).FirstOrDefault();
                var tdQllx = dicList.Where(s => s.GID == 6 && s.DEFINED_CODE == HouseData[i].tdlx).FirstOrDefault();
                var tdQlxz = dicList.Where(s => s.GID == 7 && s.DEFINED_CODE == HouseData[i].tdxz).FirstOrDefault();                
                var tdGhyt = dicList.Where(s => s.GID == 8 && s.DEFINED_CODE == HouseData[i].tdyt).FirstOrDefault();
                HouseData[i].qllx = fwqllx != null ? fwqllx.DNAME : string.Empty;
                HouseData[i].qlxz = fwqlxz != null ? fwqlxz.DNAME : string.Empty;
                HouseData[i].ghyt = Fwyt != null ? Fwyt.DNAME : string.Empty;
                HouseData[i].fwjg = Fwjg != null ? Fwjg.DNAME : string.Empty;
                HouseData[i].tdxz = tdQlxz != null ? tdQlxz.DNAME : string.Empty;
                HouseData[i].tdlx = tdQllx != null ? tdQllx.DNAME : string.Empty;
                HouseData[i].tdyt = tdGhyt != null ? tdGhyt.DNAME : string.Empty;
                HouseData[i].fj = HouseData[i].fjsm + HouseData[i].sm;
                model.qzSlbh = HouseData[i].qzSlbh;
                model.bdcdyh = HouseData[i].bdcdyh;
                model.myc = HouseData[i].myc;
                model.tstybm = HouseData[i].tstybm;
                model.zdtybm = HouseData[i].zdtybm;
                model.qllx = HouseData[i].qllx;
                model.qlxz = HouseData[i].qlxz;
                model.ghyt = HouseData[i].ghyt;
                model.zcs = HouseData[i].zcs;
                model.jzmj = HouseData[i].jzmj;
                model.tnjzmj = HouseData[i].tnjzmj;
                model.ftjzmj = HouseData[i].ftjzmj;
                model.tdqsrq = HouseData[i].tdqsrq;
                model.zzrq = HouseData[i].zzrq;
                model.tdlx = HouseData[i].tdlx;
                model.tdxz = HouseData[i].tdxz;
                model.tdyt = HouseData[i].tdyt;
                model.zdmj = HouseData[i].zdmj;
                model.fwjg = HouseData[i].fwjg;
                
            }
            #endregion

            #region SW相关信息赋值
            #region 获取税务-房产类别
            if (ghyt == "11A" || ghyt == "11" || ghyt == "10") //11A:别墅、11：成套住宅、10：住宅
            {
                taxVModel.fclb = "住宅";
            }
            else

            {
                taxVModel.fclb = "非住宅";
            }
            #endregion
            taxVModel.zl = model.zl;
            taxVModel.xzqh = xzqh;
            base.ChangeDB(SysConst.DB_CON_IIRS);
            #region 获取税务-乡镇街道
            taxVModel.xzjd = await _SysDicRepository.Query(S => S.itemNote == xzqh && S.IS_USED == 1);
            #endregion
            #region 获取税务-行政区域
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            var xzqh_data = await _SysDicRepository.Query(i => i.GID == 25 && i.DNAME == xzqh.Trim());
            if(xzqh_data.Count > 0)
            {
                itemNote = "2" + xzqh_data[0].DEFINED_CODE;
            }
            taxVModel.xzqy = await _SysDicRepository.Query(S => S.itemNote == itemNote && S.IS_USED == 1 && S.GID == 23);
            #endregion
            #region 获取税务-所属税务机关
            taxVModel.ssswjg = await _SysDicRepository.Query(S => S.itemNote == itemNote && S.IS_USED == 1 && S.GID == 24);
            #endregion
            #region 获取税务-房屋类别
            taxVModel.fwlb = GetSwFwlb1(TaxZcs, ghyt, jzmj)[0];
            taxVModel.fwlb_id = GetSwFwlb1(TaxZcs, ghyt, jzmj)[1];
            #endregion
            #region 获取税务-权属转移对象
            if (taxVModel.fclb == "住宅")
            {
                taxVModel.qszydx = "商品住房";
                taxVModel.qszydx_dCode = "06";
            }
            else
            {
                taxVModel.qszydx = "非住房";
                taxVModel.qszydx_dCode = "05";
            }
            #endregion
            #region 获取税务-交易类型  需要传入djyy,例如：商品房
            string djyy = "商品房";
            if (djyy == "商品房" || djyy == "买卖" || djyy == "按揭")
            {
                taxVModel.jylx = "房屋买卖";
                taxVModel.jylx_id = "0301";
            }
            else if (djyy == "赠与")
            {
                taxVModel.jylx = "房屋赠与：一般赠与";
                taxVModel.jylx_id = "0302";
            }
            else if (djyy == "继承")
            {
                taxVModel.jylx = "房屋赠与：法定继承";
                taxVModel.jylx_id = "0304";
            }
            else if (djyy == "离婚")
            {
                taxVModel.jylx = "离婚财产分割";
                taxVModel.jylx_id = "0311";
            }
            else if (djyy == "其他情形")
            {
                taxVModel.jylx = "其它";
                taxVModel.jylx_id = "9900";
            }
            #endregion
            taxVModel.zh = model.zh;
            taxVModel.dyh = model.dyh;
            taxVModel.mph = model.fjh;
            taxVModel.zcs = TaxZcs.ToString();
            taxVModel.bdcfwjg = model.fwjg;
            #region 获取SW房屋结构
            if (model.fwjg == "钢和钢筋混凝土结构" || model.fwjg == "钢筋混凝土结构")
            {
                model.fwjg_sw = "钢混结构";
                taxVModel.swfwjg_id = "02";

            }
            else if (model.fwjg == "钢结构")
            {
                model.fwjg_sw = "钢筋结构";
                taxVModel.swfwjg_id = "01";
            }
            else if (model.fwjg == "混合结构")
            {
                model.fwjg_sw = "混合结构";
                taxVModel.swfwjg_id = "03";
            }
            else if (model.fwjg == "砖木结构")
            {
                model.fwjg_sw = "砖木结构";
                taxVModel.swfwjg_id = "04";
            }
            else if (model.fwjg == "其他结构或简易材料结构")
            {
                model.fwjg_sw = "其它结构";
                taxVModel.swfwjg_id = "05";
            }
            taxVModel.swfwjg = model.fwjg_sw;
            #endregion
            #region 获取税务-所在基础层
            if (!string.IsNullOrEmpty(model.myc))
            {
                taxVModel.szjcc = GetMyc1(model.myc);
            }
            #endregion
            #region 获取税务-房屋朝向
            taxVModel.fwcx = await _SysDicRepository.Query(S => S.GID == 21 && S.DNAME != null && S.IS_USED == 1);
            #endregion
            taxVModel.jzmj = jzmj;
            taxVModel.ftmj = model.ftjzmj;
            taxVModel.tnmj = model.tnjzmj;
            taxVModel.sjsj = DateTime.Now.ToShortDateString();
            #endregion

            return taxVModel;
        }

        
        /// <summary>
        /// 获取缴费类型
        /// </summary>
        /// <returns></returns>
        public async Task<List<SYS_DIC>> GetPaymentType()
        {
            List<SYS_DIC> modelList = new List<SYS_DIC>();
            
            var result = await _SysDicRepository.Query(i => i.GID == 27 && i.IS_USED == 1);  
            var data = result.Select(s => new { gid = s.GID,itemNote = s.itemNote }).Distinct();
            foreach (var item in data)
            {
                SYS_DIC model = new SYS_DIC();
                model.GID = item.gid;
                model.itemNote = item.itemNote;
                modelList.Add(model);
            }
            return modelList;
        }

        /// <summary>
        /// 缴费标注明细
        /// </summary>
        /// <param name="itemNote">缴费类型</param>
        /// <returns></returns>
        public async Task<List<SYS_DIC>> GetPaymentTypeInfo(string itemNote)
        {
            var data = await _SysDicRepository.Query(i => i.GID == 27 && i.IS_USED == 1 && i.itemNote == itemNote);
            return data;
        }
        /// <summary>
        /// 税务信息提交
        /// </summary>
        /// <param name="model">税务信息</param>
        /// <returns></returns>
        public async Task<int> InsertTaxPost(InsertTaxVModel model)
        {
            int count = 0;
            List<TAX_EXISTING_HOME_BUYER> BUYER = new List<TAX_EXISTING_HOME_BUYER>();
            List<TAX_EXISTING_HOME_SELLER> SELLER = new List<TAX_EXISTING_HOME_SELLER>();
            try
            {
                #region 税务信息
                TAX_EXISTING_HOME taxModel = new TAX_EXISTING_HOME();
                taxModel.TAX_PK = Provider.Sql.Create().ToString();
                taxModel.SLBH = model.newSlbh;
                taxModel.HTBH = model.xzqh_code + DateTime.Now.ToString("yyyyMMdd") + GetSwhth().ToString("D6");
                taxModel.DZ = model.zl;
                taxModel.XQ_SWJG_DM = model.xzqy;
                taxModel.XZQHSZDM = model.xzqh_code;
                taxModel.JDXZDM = model.xzjd;
                taxModel.SS_SWJG_DM = model.ssswjg;
                taxModel.CQCJH = model.bdczh;    //相关证号
                taxModel.FDC_LH = model.zh;
                taxModel.DY = model.dyh;
                taxModel.FDC_MPH = model.mph;
                taxModel.FWLB_DM = model.fwlb_id;
                taxModel.JZJG_DM = model.swfwjg_id;
                taxModel.TNMJ = Convert.ToDecimal(model.tnmj);
                taxModel.FWJZMJ = Convert.ToDecimal(model.jzmj);
                taxModel.QSQSZYDX_DM = model.qszydx_fclb;
                taxModel.FWCX_DM = model.fwcx;
                taxModel.JYLX_DM = model.jylx_id;
                taxModel.JYHTRQ = DateTime.Now;
                taxModel.HTCJJG = Convert.ToDecimal(model.jyje);   //合同成交价格
                taxModel.BDCDYH = model.bdcdyh;    //不动产单元号
                taxModel.CDATE = DateTime.Now;
                taxModel.TAX_TIME = DateTime.Now;
                taxModel.IS_TAX = 0;
                taxModel.STATE = 0;
                taxModel.FWZCS = model.zcs;
                taxModel.FWSZCS = model.szjcc;

                foreach (var item in model.sw_qlrList)
                {
                    TAX_EXISTING_HOME_BUYER buyerModel = new TAX_EXISTING_HOME_BUYER();
                    buyerModel.PK = Provider.Sql.Create().ToString();
                    buyerModel.TAX_PK = taxModel.TAX_PK;
                    buyerModel.NSRSBH = item.zjhm;
                    buyerModel.NSRMC = item.qlrmc;
                    buyerModel.SFZJZL_DM = GetSwZjlb(item.zjlb_zwm);
                    buyerModel.SZFE = Convert.ToDecimal(item.gyfe);
                    buyerModel.JYBL = Convert.ToDecimal(item.gyfe);
                    buyerModel.DZ = model.zl;
                    buyerModel.LXDH = item.dh;
                    buyerModel.SLBH = taxModel.HTBH;
                    BUYER.Add(buyerModel);
                }

                foreach (var item in model.sw_ywrList)
                {
                    TAX_EXISTING_HOME_SELLER sellerModel = new TAX_EXISTING_HOME_SELLER();
                    sellerModel.PK = Provider.Sql.Create().ToString();
                    sellerModel.TAX_PK = taxModel.TAX_PK;
                    sellerModel.NSRSBH = item.zjhm;
                    sellerModel.NSRMC = item.qlrmc;
                    sellerModel.SFZJZL_DM = GetSwZjlb(item.zjlb_zwm);
                    sellerModel.SZFE = Convert.ToDecimal(item.gyfe);
                    sellerModel.JYBL = Convert.ToDecimal(item.gyfe);
                    sellerModel.DZ = model.zl;
                    sellerModel.LXDH = item.dh;
                    sellerModel.SLBH = taxModel.HTBH;
                    SELLER.Add(sellerModel);
                }

                taxModel.GMFXX = BUYER;
                taxModel.CMFXX = SELLER;
                #endregion

                _dBTransManagement.BeginTran();
                var homeCount = await _taxHomeRepository.Add(taxModel);
                var buyerCount = await _taxBuyerRepository.Add(BUYER);
                var sellerCount = await _taxSellerRepository.Add(SELLER);
                _dBTransManagement.CommitTran();
                count = 1;
                
            }
            catch (Exception ex)
            {
                _dBTransManagement.RollbackTran();
                throw ex;
            }
            return count;
        }

        /// <summary>
        /// 获取税务证件类别
        /// </summary>
        /// <param name="zjlb"></param>
        /// <returns></returns>
        public string GetSwZjlb(string zjlb)
        {
            string zjlb_sw = "";
            if(zjlb == "身份证")
            {
                zjlb_sw = "10";
            }
            else if(zjlb == " 护照")
            {
                zjlb_sw = "21";
            }
            else if (zjlb == " 军官证（士兵证）")
            {
                zjlb_sw = "30";
            }
            else if (zjlb == " 港澳台身份证")
            {
                zjlb_sw = "71";
            }
            else if (zjlb == " 其它")
            {
                zjlb_sw = "90";
            }
            return zjlb_sw;
        }
        /// <summary>
        /// 获取税务房屋类别
        /// </summary>
        /// <param name="zcs">总层数</param>
        /// <param name="ghyt">规划用途</param>
        /// <param name="jzmj">建筑面积</param>
        /// <returns></returns>
        public string[] GetSwFwlb1(int zcs, string ghyt, decimal jzmj)
        {
            string[] msg = new string[2];
            if (zcs == 0 || ghyt == null || jzmj == 0)
            {
                msg[0] = "请输入正确的总层数:" + zcs + "、规划用途: " + ghyt + "、建筑面积:" + jzmj;
                msg[1] = "无字典值";
            }
            else
            {
                if (zcs == 1)    //平房
                {
                    if (ghyt == "住宅")
                    {
                        if (jzmj <= 90)  //面积小于等于90
                        {
                            msg[0] = "平房-存量房-90平方米及以下平房住房";
                            msg[1] = "21";
                        }
                        else
                        {
                            msg[0] = "平房-存量房-平房住房";
                            msg[1] = "20";
                        }
                    }
                    else
                    {
                        msg[0] = "平房-存量房-非住宅";
                        msg[1] = "22";
                    }
                }
                else          //楼房
                {
                    if (ghyt == "别墅")
                    {
                        msg[0] = "房屋-存量房-别墅住房";
                        msg[1] = "12";
                    }
                    else if (ghyt == "住宅")
                    {
                        if (jzmj <= 90)
                        {
                            msg[0] = "房屋-存量房-90平方米及以下普通住房";
                            msg[1] = "09";
                        }
                        else if (jzmj > 90 && jzmj <= 120)
                        {
                            msg[0] = "房屋-存量房-普通住宅";
                            msg[1] = "10";
                        }
                        else
                        {
                            msg[0] = "房屋-存量房-非普通住宅";
                            msg[1] = "11";
                        }
                    }
                    else if (ghyt.Contains("工业"))
                    {
                        msg[0] = "房屋-存量房-工业用房";
                        msg[1] = "14";
                    }
                    else if (ghyt.Contains("商业"))
                    {
                        msg[0] = "房屋-存量房-商业用房";
                        msg[1] = "13";
                    }
                    else if (ghyt.Contains("办公"))
                    {
                        msg[0] = "房屋-存量房-办公用房";
                        msg[1] = "15";
                    }
                    else if (ghyt.Contains("车库"))
                    {
                        msg[0] = "房屋-存量房-车库";
                        msg[1] = "24";
                    }
                    else
                    {
                        msg[0] = "房屋-存量房-其他用房";
                        msg[1] = "16";
                    }
                }
            }

            return msg;
        }
        /// <summary>
        /// 获取税务所在基础层
        /// </summary>
        /// <param name="strMyc"></param>
        /// <returns></returns>
        public string GetMyc1(string strMyc)
        {
            string MycMsg = "";
            if (strMyc != null)
            {
                strMyc = strMyc.Trim();
                int count = Regex.Matches(strMyc, "-").Count;
                string[] arr = strMyc.Split("-");
                if (count > 3)
                {
                    MycMsg = "所在基准层格式不正确。";
                }
                else if (count == 0)
                {
                    strMyc = strMyc.Trim();
                    //判断是否是纯数字
                    if (IsNumberic(strMyc))
                    {
                        MycMsg = strMyc;
                    }
                    else
                    {
                        MycMsg = "所在基准层格式不正确。";
                    }
                }
                else if (count == 1)
                {

                    //判断“-”后的值是否为纯数字
                    if (IsNumberic(arr[1]))
                    {
                        if (arr[0] == "")
                        {
                            MycMsg = strMyc;
                        }
                        else
                        {
                            if (arr[1].Length == 1)     //判断最后一位是否是0
                            {
                                if (arr[1] == "0")
                                {
                                    MycMsg = "所在基准层格式不正确。";
                                }
                                else
                                {
                                    if (Convert.ToInt32(arr[0]) > Convert.ToInt32(arr[1]))      //去小值
                                    {
                                        MycMsg = arr[1];
                                    }
                                    else
                                    {
                                        MycMsg = arr[0];
                                    }
                                }
                            }
                            else
                            {
                                if (Convert.ToInt32(arr[0]) > Convert.ToInt32(arr[1]))      //去小值
                                {
                                    MycMsg = arr[1];
                                }
                                else
                                {
                                    MycMsg = arr[0];
                                }
                            }

                        }

                    }
                    else
                    {
                        MycMsg = "所在基准层格式不正确。";
                    }
                }
                else if (count == 2)
                {
                    if (strMyc.Substring(0, 2) == "--")
                    {
                        MycMsg = "所在基准层格式不正确。";
                    }
                    else if (strMyc.Substring(strMyc.Length - 1, 1) == "-")    //最后一个串是“-”
                    {
                        MycMsg = "所在基准层格式不正确。";
                    }
                    else if (arr[arr.Length - 1].Length == 1)   //判断最后一位是否是0
                    {
                        if (arr[arr.Length - 1] == "0")
                        {
                            MycMsg = "所在基准层格式不正确。";
                        }
                    }
                    else
                    {
                        //去掉“-后” 判断是否是纯数字
                        if (IsNumberic(strMyc.Replace("-", "")))
                        {
                            MycMsg = "1";
                        }
                        else
                        {
                            MycMsg = "所在基准层格式不正确。";
                        }
                    }

                }
                else if (count == 3)
                {
                    if (strMyc.Substring(strMyc.Length - 1, 1) == "-")
                    {
                        MycMsg = "所在基准层格式不正确。";
                    }
                    else if (strMyc.Substring(0, 2) == "--")
                    {
                        MycMsg = "所在基准层格式不正确。";
                    }
                    else if (arr[arr.Length - 1].Length == 1)   //判断最后一位是否是0
                    {
                        if (arr[arr.Length - 1] == "0")
                        {
                            MycMsg = "所在基准层格式不正确。";
                        }
                    }
                    else
                    {
                        if (IsNumberic(strMyc.Replace("-", "")))
                        {
                            if (strMyc.Split("-", StringSplitOptions.RemoveEmptyEntries).Length > 2)    //截取判断数组个数大于2
                            {
                                MycMsg = "所在基准层格式不正确。";
                            }
                            else
                            {
                                string[] arrTwo = strMyc.Split("--");
                                int valueOne = Convert.ToInt32(arrTwo[0]);
                                int valueTwo = Convert.ToInt32(arrTwo[1]);
                                if (Math.Abs(valueOne) > Math.Abs(valueTwo))    //绝对值比较，取小值
                                {
                                    MycMsg = "-" + Math.Abs(valueTwo).ToString();
                                }
                                else
                                {
                                    MycMsg = "-" + Math.Abs(valueOne).ToString();
                                }
                            }
                        }
                        else
                        {
                            MycMsg = "所在基准层格式不正确。";
                        }
                    }
                }
            }
            else
            {
                MycMsg = "所在基准层格式不正确";
            }
            return MycMsg;
        }

        /// <summary>
        /// 获取税务合同号
        /// </summary>
        /// <returns></returns>
        public int GetSwhth()
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            string slbh = string.Concat(base.Db.Ado.GetScalar("SELECT swhtbh_seqnum('SW') FROM DUAL"));//获取系统受理编号
            return Convert.ToInt32(slbh);
        }

        
    }
}
