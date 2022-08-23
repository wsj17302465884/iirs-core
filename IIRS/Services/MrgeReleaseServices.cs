using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Services.Base;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class MrgeReleaseServices : BaseServices, IMrgeReleaseServices
    {
        private readonly ILogger<MrgeReleaseServices> _logger;

        IDBTransManagement _dbTransManagement;
        public MrgeReleaseServices(IDBTransManagement dbTransManagement, ILogger<MrgeReleaseServices> logger) : base(dbTransManagement)
        {
            this._logger = logger;
            this._dbTransManagement = dbTransManagement;
        }

        /// <summary>
        /// 查询注销订单(现实手)生成json
        /// </summary>
        /// <param name="auzID"></param>
        /// <returns>json结果集</returns>
        public async Task<string> RegistrationJsonQuery(string auzID)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            var resultZS = await base.Db.Queryable<REGISTRATION_INFO, BankAuthorize>((R, A) => new object[] { JoinType.Inner, R.AUZ_ID == A.BID })
.Where((R, A) => (A.BID == auzID && R.NEXT_XID == null))
.Select((R, A) => new { JSON = R.SaveDataJson }).ToListAsync();
            string json = string.Empty;
            foreach(var r in resultZS)
            {
                json = r.JSON;
            }
            return json;
        }

        /// <summary>
        /// 保存附件文件数据库信息
        /// </summary>
        /// <param name="files">文件信息</param>
        /// <returns></returns>
        public async Task<int> SaveUploadFileDB(List<PUB_ATT_FILE> files)
        {
            if (files.Count > 0)
            {
                base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
                string slbh = files[0].BAK_PK;
                try
                {
                    base.Db.Deleteable<PUB_ATT_FILE>().Where(it => it.BAK_PK == slbh).ExecuteCommand();
                    await base.Db.Insertable(files.ToArray()).ExecuteReturnIdentityAsync();
                }
                catch (Exception ex)
                {
                    string ee = ex.Message;
                }

                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 查询不动产抵押信息
        /// </summary>
        /// <param name="BDCZMH">不动产证明号</param>
        /// <returns></returns>
        public async Task<MrgeReleaseVModel> GetMortgageInfo(string BDCZMH)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            var sysDicList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 1)).ToListAsync();
            //sysDicList.Cast<SYS_DIC>().Select(s=>new { DEFINED_CODE=s.DEFINED_CODE, DNAME=s.DNAME }).ToDictionary<>
            
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_BDC);
            MrgeReleaseVModel dyData = new MrgeReleaseVModel();
            #region 抵押房屋信息查询
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            //            var resultZS = await base.Db.Queryable<DJ_TSGL, DJ_DY, DJ_QLRGL, DJ_XGDJGL, DJ_SJD>((TS, DY, R, Z, S)
            //    => new object[] { JoinType.Inner, TS.SLBH == DY.SLBH, JoinType.Inner, DY.SLBH == R.SLBH, JoinType.Inner, DY.SLBH == Z.ZSLBH, JoinType.Inner, DY.SLBH == S.SLBH })
            //.Where((TS, DY, R, Z, S) => ((TS.LIFECYCLE == null || TS.LIFECYCLE == 0) && (DY.LIFECYCLE == null || DY.LIFECYCLE == 0) && R.QLRLX == "抵押人") && DY.BDCZMH== dyslbh)
            //.GroupBy((TS, DY, R, Z, S) => new { BDCZMH = DY.BDCZMH, TSTYBM = TS.TSTYBM, XGZLX = Z.XGZLX, SLBH = DY.SLBH, ZL = S.ZL })
            //.Select((TS, DY, R, Z, S) => new
            //{
            //BDCZMH = DY.BDCZMH,
            //TSTYBM = TS.TSTYBM,
            //XGZLX = Z.XGZLX,
            //ZL = S.ZL,
            //SLBH=DY.SLBH,
            //DYR = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(R.QLRMC))"),
            //A = SqlFunc.AggregateMax(TS.TSTYBM)
            //}).ToListAsync();
            var resultZS = await base.Db.Queryable<DJ_TSGL, DJ_DY, DJ_QLRGL, DJ_SJD>((TS, DY, R, S)
    => new object[] { JoinType.Inner, TS.SLBH == DY.SLBH, JoinType.Inner, DY.SLBH == R.SLBH, JoinType.Inner, DY.SLBH == S.SLBH })
.Where((TS, DY, R, S) => ((TS.LIFECYCLE == null || TS.LIFECYCLE == 0) && (DY.LIFECYCLE == null || DY.LIFECYCLE == 0) && R.QLRLX == "抵押人") && DY.BDCZMH == BDCZMH)
.GroupBy((TS, DY, R, S) => new { BDCDYH = DY.BDCDYH, BDCZMH = DY.BDCZMH, SLBH = DY.SLBH, ZL = S.ZL })
.Select((TS, DY, R, S) => new
{
    BDCZMH = DY.BDCZMH,
    ZL = S.ZL,
    BDCDYH = DY.BDCDYH,
    SLBH = DY.SLBH,
    DYR = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(R.QLRMC))"),
    TSTYBMS = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(TS.TSTYBM))")
}).ToListAsync();
            if (resultZS.Count==0)
            {
                throw new ApplicationException($"未查询【{BDCZMH}】证明号");
            }

            var houseRoot = new MrgeReleaseHouseVModel()
            {
                BDCZH = resultZS[0].BDCZMH,
                SLBH = resultZS[0].SLBH,
                BDCDYH = resultZS[0].BDCDYH,
                ZSLX = "房屋抵押证明",
                ZL = resultZS[0].ZL,
                QLRMC = resultZS[0].DYR,
                hasChildren = true
            };
            string[] BDCZ_TSTYBM = resultZS[0].TSTYBMS.Split(new char[] { ',' });
            //}

            dyData.selectHouse = houseRoot;
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            var resultHouse = await base.Db.Queryable<DJ_TSGL, DJ_DJB, DJ_QLRGL, DJ_XGDJGL, DJ_SJD>((TS, DJB, R, Z, S)
    => new object[] { JoinType.Inner, TS.SLBH == DJB.SLBH, JoinType.Inner, DJB.SLBH == R.SLBH, JoinType.Inner, DJB.SLBH == Z.FSLBH, JoinType.Inner, DJB.SLBH == S.SLBH })
.Where((TS, DJB, R, Z, S) => ((TS.LIFECYCLE == null || TS.LIFECYCLE == 0) && (DJB.LIFECYCLE == null || DJB.LIFECYCLE == 0) && R.QLRLX == "权利人") && BDCZ_TSTYBM.Contains(TS.TSTYBM))
.GroupBy((TS, DJB, R, Z, S) => new { BDCDYH = TS.BDCDYH, BDCZMH = DJB.BDCZH, TSTYBM = TS.TSTYBM, XGZLX = Z.XGZLX, ZL = S.ZL, FSXBH = S.SLBH, BDCLX = TS.BDCLX })
.Select((TS, DJB, R, Z, S) => new
{
    BDCZMH = DJB.BDCZH,
    BDCDYH=TS.BDCDYH,
    BDCLX = TS.BDCLX,
    TSTYBM = TS.TSTYBM,
    XGZLX = Z.XGZLX,
    FSXBH = S.SLBH,
    ZL = S.ZL,
    DYR = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(R.QLRMC))"),
    A = SqlFunc.AggregateMax(TS.TSTYBM)
}).ToListAsync();
            
            foreach (var h in resultHouse)
            {
                houseRoot.children.Add(new MrgeReleaseHouseVModel()
                {
                    BDCZH = h.BDCZMH,
                    ZSLX = h.XGZLX,
                    SLBH = h.FSXBH,
                    ZL = h.ZL,
                    TSTYBM = h.TSTYBM,
                    BDCDYH = h.BDCDYH,
                    QLRMC = h.DYR,
                    BDCLX = h.BDCLX
                });
            }

            #endregion
            #region 权利人信息查询
            var resultMrge = await base.Db.Queryable<DJ_TSGL, DJ_DY, DJ_SJD>((TS, DY, S) => new object[] { JoinType.Inner, TS.SLBH == DY.SLBH, JoinType.Inner, TS.SLBH == S.SLBH })
.Where((TS, DY, S) => ((TS.LIFECYCLE == null || TS.LIFECYCLE == 0) && (DY.LIFECYCLE == null || DY.LIFECYCLE == 0) && BDCZ_TSTYBM.Contains(TS.TSTYBM)))
.Select((TS, DY, S) => new
{
    XGZH = DY.XGZH,
    ZL = S.ZL,
    DYLX = DY.DYLX,
    DYSW = DY.DYSW,
    DYFS = DY.DYFS,
    DYMJ = DY.DYMJ,
    BDBZZQSE = DY.BDBZZQSE,
    PGJE = DY.PGJE,
    HTH = DY.HTH,
    FJ = DY.FJ,
    QLQSSJ = DY.QLQSSJ,
    QLJSSJ = DY.QLJSSJ,
    DYQX = DY.DYQX
}).FirstAsync();
            dyData.ZL = resultMrge.ZL;
            dyData.DYLX = resultMrge.DYLX;
            dyData.DYSW = Convert.ToInt32(resultMrge.DYSW);
            dyData.DYFS = resultMrge.DYFS;
            dyData.dyMJ = resultMrge.DYMJ ?? 0;
            dyData.PGJE = resultMrge.PGJE ?? 0;
            dyData.HTH = resultMrge.HTH;
            dyData.BZ = resultMrge.FJ;
            dyData.LXQX = resultMrge.DYQX;
            if (resultMrge.BDBZZQSE != null)
            {
                dyData.BDBZQSE = Convert.ToInt32(resultMrge.BDBZZQSE);
            }
            if (resultMrge.QLQSSJ != null)
            {
                dyData.ZWLXQXQSRQ = resultMrge.QLQSSJ ?? DateTime.Now;
            }
            if (resultMrge.QLJSSJ != null)
            {
                dyData.ZWLXQXJZRQ = resultMrge.QLQSSJ ?? DateTime.Now;
            }
            if (resultMrge.QLQSSJ != null || resultMrge.QLJSSJ != null)
            {
                dyData.LXZWQX_STR = $"{resultMrge.QLQSSJ:yyyy-MM-dd} - {resultMrge.QLJSSJ:yyyy-MM-dd}";
            }

            var resultQLR = await base.Db.Queryable<DJ_TSGL, DJ_QLRGL>((TS, R) => new object[] { JoinType.Inner, TS.SLBH == R.SLBH })
.Where((TS, R) => (R.QLRLX == "抵押人" && (TS.LIFECYCLE == null || TS.LIFECYCLE == 0) && (R.LIFECYCLE == null || R.LIFECYCLE == 0) && BDCZ_TSTYBM.Contains(TS.TSTYBM)))
.Select((TS, R) => new
{
    GLBM = R.GLBM,
    SLBH = R.SLBH,
    YWBM = R.YWBM,
    ZJHM = R.ZJHM,
    QLRID = R.QLRID,
    QLRMC = R.QLRMC,
    ZJLB = R.ZJLB,
    DH = R.DH,
    QLRLX = R.QLRLX,
    SXH = R.SXH,
    GYFE = R.GYFE
}).OrderBy("R.SXH").Distinct().ToListAsync();
            foreach (var qlr in resultQLR)
            {
                var zjlb_zwmObj = sysDicList.Where(s => s.DEFINED_CODE == qlr.ZJLB).FirstOrDefault();
                dyData.selectPerson.Add(new DyPersonVModel()
                {
                    DH=qlr.DH,
                    GYFE=qlr.GYFE,
                    QLRID= qlr.QLRID,
                    QLRMC = qlr.QLRMC,
                    SXH= Convert.ToInt32( qlr.SXH),
                    ZJHM = qlr.ZJHM,
                    ZJLB= qlr.ZJLB,
                    ZJLB_ZWM = zjlb_zwmObj != null ? zjlb_zwmObj.DNAME : string.Empty,
                });
            }
            #endregion
            return dyData;
        }

        public string GetSLBH()
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            string slbh = string.Concat(base.Db.Ado.GetScalar("SELECT SLBH_SEQNUM() FROM DUAL"));//获取系统受理编号
            return slbh;
        }

        /// <summary>
        /// 初始化抵押上报文件格式信息
        /// </summary>
        /// <returns></returns>
        public async Task<MediasVModel> GetInitMedias()
        {
            var sysDicList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 2)).ToListAsync();
            MediasVModel medias = new MediasVModel();
            foreach(var pathDic in sysDicList)
            {
                medias.Groups.Add(new MediasGroups()
                {
                    //GroupsID = pathDic.DIC_ID,
                    GroupsName = pathDic.DNAME,
                    Items = null
                });
            }
            return medias;
        }

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="DjInfo">登记信息</param>
        /// <param name="zxInfo">注销信息表</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="TsglInfo">图属信息</param>
        /// <param name="XgdjglInfos">相关登记关联信息</param>
        /// <param name="qlrglInfos">权利人信息</param>
        /// <param name="uploadFiles">附件信息</param>
        /// <returns>多表操作影响记录数之和</returns>
        public async Task<int> MortgageRelease(REGISTRATION_INFO DjInfo, XGDJZX_INFO zxInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, List<PUB_ATT_FILE> uploadFiles)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            
            try
            {
                int count = 0;
                this._dbTransManagement.BeginTran();
                var delData = base.Db.Queryable<REGISTRATION_INFO>().Single(s => s.YWSLBH == DjInfo.YWSLBH && s.NEXT_XID == null);
                if (delData != null && !string.IsNullOrEmpty(delData.XID))
                {
                    count += await base.Db.Updateable<REGISTRATION_INFO>().SetColumns(it => new REGISTRATION_INFO() { NEXT_XID = DjInfo.XID })
.Where(it => it.XID == delData.XID).ExecuteCommandAsync();
                }
                count += await base.Db.Insertable(DjInfo).InsertColumns(it => new
                {
                    it.XID,
                    it.AUZ_ID,
                    it.YWSLBH,
                    it.DJZL,
                    it.BDCZH,
                    it.SLBH,
                    it.QLRMC,
                    it.ZL,
                    it.ORG_ID,
                    it.USER_ID,
                    it.TEL,
                    it.HTH,
                    it.REMARK2,
                    it.IS_ACTION_OK,
                    it.SaveDataJson
                }).ExecuteCommandAsync();
                count += await base.Db.Insertable(zxInfo).InsertColumns(zl => new
                {
                    zl.SLBH,
                    zl.XGZH,
                    zl.DJLX,
                    zl.DJYY,
                    zl.DLRXM,
                    zl.DLJGMC,
                    zl.SPBZ,
                    zl.SQRQ,
                    zl.SJR,
                    zl.BDCDYH,
                    zl.XID
                }).ExecuteCommandAsync();
                int status = flowInfo.FLOW_ID;
                await base.Db.Updateable<BankAuthorize>().SetColumns(it => new BankAuthorize() { STATUS = status })
.Where(it => it.BID == DjInfo.AUZ_ID).ExecuteCommandAsync();

                count += await base.Db.Insertable(TsglInfo.ToArray()).InsertColumns(ts => new
                {
                    ts.GLBM,
                    ts.SLBH,
                    ts.BDCLX,
                    ts.TSTYBM,
                    ts.BDCDYH,
                    ts.DJZL,
                    ts.CSSJ,
                    ts.XID
                }).ExecuteCommandAsync();
                count = await base.Db.Insertable(flowInfo).InsertColumns(fw => new
                {
                    fw.PK,fw.FLOW_ID,fw.AUZ_ID,fw.CDATE,fw.USER_NAME
                }).ExecuteCommandAsync();
                count += await base.Db.Insertable(XgdjglInfos.ToArray()).InsertColumns(djgl => new
                {
                    djgl.BGBM,
                    djgl.ZSLBH,
                    djgl.FSLBH,
                    djgl.BGRQ,
                    djgl.BGLX,
                    djgl.XGZLX,
                    djgl.XGZH,
                    djgl.XID,
                    djgl.PID
                }).ExecuteCommandAsync();
                count += await base.Db.Insertable(qlrglInfos.ToArray()).InsertColumns(qlrgl => new
                {
                    qlrgl.GLBM,
                    qlrgl.SLBH,
                    qlrgl.YWBM,
                    qlrgl.QLRID,
                    qlrgl.QLRMC,
                    qlrgl.ZJHM,
                    qlrgl.ZJLB,
                    qlrgl.ZJLB_ZWM,
                    qlrgl.DH,
                    qlrgl.QLRLX,
                    qlrgl.SXH,
                    qlrgl.XID
                }).ExecuteCommandAsync();
                
                if (uploadFiles != null)
                {
                    count += await base.Db.Insertable(uploadFiles.ToArray()).ExecuteCommandAsync();
                }
                this._dbTransManagement.CommitTran();
                return count;
            }
            catch (Exception ex)
            {
                this._dbTransManagement.RollbackTran();
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        //public async Task<MrgeReleaseVModel> MortgageReleaseQuery(string slbh)
        //{
        //    base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_BDC);
        //    var regInfo = await base.Db.Queryable<REGISTRATION_INFO>().SingleAsync(s => s.YWSLBH == slbh);
        //    if (regInfo != null)
        //    {
        //        MrgeReleaseVModel vModel = new MrgeReleaseVModel();
        //        vModel.SLBH = regInfo.YWSLBH;
        //        vModel.AUZ_ID = regInfo.AUZ_ID;
        //        vModel.HTH = regInfo.HTH;
        //    }
        //}
    }
}
