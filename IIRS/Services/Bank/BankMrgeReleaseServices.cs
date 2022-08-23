using IIRS.IRepository.Base;
using IIRS.IServices.Bank;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services.Bank
{
    public class BankMrgeReleaseServices : BaseServices, IBankMrgeReleaseServices
    {
        private readonly ILogger<BankMrgeReleaseServices> _logger;

        IDBTransManagement _dbTransManagement;
        public BankMrgeReleaseServices(IDBTransManagement dbTransManagement, ILogger<BankMrgeReleaseServices> logger) : base(dbTransManagement)
        {
            this._logger = logger;
            this._dbTransManagement = dbTransManagement;
        }

        /// <summary>
        /// 查询要抵押不动产信息
        /// </summary>
        /// <param name="BDCZMH">不动产证明号</param>
        /// <param name="DYRMC">抵押人名称</param>
        /// <param name="Bank_ID">抵押权人(银行)编码</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        public PageModel<MrgeCertInfoVModel> GetMrgeCertInfo(string BDCZMH, string DYRMC, string Bank_ID, int pageIndex = 1, int pageSize = 10)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            //Bank_ID = "91211000701714516X";
            List<string> ComparisonList = base.Db.Queryable<BANK_COMPARATIVE>().Where(S => S.BANK_ID == Bank_ID).Select(S => S.C_NAME).ToList();
            base.ChangeDB(SysConst.DB_CON_BDC);
            
            var zmListResult = base.Db.Queryable<DJ_TSGL, DJ_DY, DJ_SJD>((TS, DY, SD) => new object[] { JoinType.Right, DY.SLBH == TS.SLBH, JoinType.Inner, SD.SLBH == DY.SLBH })
    .Where((TS, DY, SD) => (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null)
    && (DY.LIFECYCLE == 0 || DY.LIFECYCLE == null)
    && SqlFunc.Subqueryable<V_DJ_QLRGL_ListModel>().Where(G => DY.SLBH == G.SLBH
     && (G.QLRLX == "抵押权人" && ComparisonList.Contains(G.QLRMC)))
    .WhereIF(!string.IsNullOrEmpty(DYRMC), (G) => G.QLRLX == "抵押人" && G.QLRMC.Contains(DYRMC)).Any())
    .WhereIF(!string.IsNullOrEmpty(BDCZMH), (TS, DY, SD) => SqlFunc.Contains(DY.BDCZMH, BDCZMH))
    .GroupBy((TS, DY, SD) => new { TS.BDCLX, DY.ZGZQSE, DY.BDBZZQSE, DY.DBFW, DY.SLBH, DY.BDCZMH, DY.BDCDYH, DY.DYLX, SD.ZL })
    .Select((TS, DY, SD) => new MrgeCertInfoVModel
    {
        RN = SqlFunc.MappingColumn<int>(Convert.ToInt32(DY.DYSW), "ROW_NUMBER() OVER(ORDER BY SYSDATE)"),
        SLBH = DY.SLBH,
        BDCZMH = DY.BDCZMH,
        BDBZZQSE = DY.BDBZZQSE,
        ZGZQSE = DY.ZGZQSE,
        DBFW = DY.DBFW,
        BDCDYH = DY.BDCDYH,
        DYLX = DY.DYLX,
        ZL = SD.ZL,
        BDCLX = TS.BDCLX,
        QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == DY.SLBH && F.QLRLX == "抵押人").Select(GG => GG.QLRMC),
        ZT = SqlFunc.MappingColumn(TS.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(TS.DJZL))")
    }).ToPageList(pageIndex, pageSize);
            var zms = zmListResult.Cast<MrgeCertInfoVModel>().Select(s => s.BDCZMH).ToArray();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{

            //    _logger.LogDebug(sql);
            //};
            var xgzhsList = base.Db.Queryable<XGDJZX_INFO, REGISTRATION_INFO, BankAuthorize, IFLOW_ACTION>((ZX, R, B, FW) => ZX.XID == R.XID && R.AUZ_ID == B.BID && B.STATUS == FW.FLOW_ID)
    .Where((ZX, R, B, FW) => R.NEXT_XID == null && FW.TERMINATION_NODE == "0" && zms.Contains(ZX.XGZH))
    .Select((ZX, R, B, FW) => new
    {
        ZX.XGZH
    }).Distinct().ToListAsync().Result;
            foreach (var xgzh in xgzhsList)
            {
                foreach (MrgeCertInfoVModel vModel in zmListResult)
                {
                    if (xgzh.XGZH == vModel.BDCZMH)
                    {
                        vModel.IS_DOING = 1;
                        break;
                    }
                }
            }

            //string pageDataJson = await base.Db.Queryable<DJ_TSGL, DJ_DY, DJ_SJD>((TS, DY, SD) => new object[] { JoinType.Right, DY.SLBH == TS.SLBH, JoinType.Inner, SD.SLBH == DY.SLBH })
            //    .Where((TS, DY, SD) => (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null)
            //    && (DY.LIFECYCLE == 0 || DY.LIFECYCLE == null)
            //    && SqlFunc.Subqueryable<V_DJ_QLRGL_ListModel>().Where(G => DY.SLBH == G.SLBH
            //     && (G.QLRLX == "抵押权人" && ComparisonList.Contains(G.QLRMC)))
            //    .WhereIF(!string.IsNullOrEmpty(DYRMC), (G) => G.QLRLX == "抵押人" && G.QLRMC.Contains(DYRMC)).Any())
            //    .WhereIF(!string.IsNullOrEmpty(BDCZMH), (TS, DY, SD) => SqlFunc.Contains(DY.BDCZMH, BDCZMH))
            //    .GroupBy((TS, DY, SD) => new { TS.BDCLX, DY.ZGZQSE, DY.BDBZZQSE, DY.DBFW, DY.SLBH, DY.BDCZMH, DY.BDCDYH, DY.DYLX, SD.ZL })
            //    .Select((TS, DY, SD) => new
            //    {
            //        RN = SqlFunc.MappingColumn(DY.BDCDYH, "ROW_NUMBER() OVER(ORDER BY SYSDATE)"),
            //        DY.SLBH,
            //        DY.BDCZMH,
            //        DY.BDBZZQSE,
            //        DY.ZGZQSE,
            //        DY.DBFW,
            //        DY.BDCDYH,
            //        DY.DYLX,
            //        SD.ZL,
            //        BDCLX = TS.BDCLX,
            //        QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == DY.SLBH && F.QLRLX == "抵押人").Select(GG => GG.QLRMC),
            //        ZT = SqlFunc.MappingColumn(TS.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(TS.DJZL))")
            //    }).ToJsonPageAsync(pageIndex, pageSize);
            return new PageModel<MrgeCertInfoVModel>
            {
                data = zmListResult,
                dataCount = 0,
                PageSize = pageSize,
                page = pageIndex
            };
        }

        /// <summary>
        /// 房屋抵押注销不动产中心审批
        /// </summary>
        /// <param name="AuzInfo">订单表</param>
        /// <param name="regInfo">注册信息</param>
        /// <param name="jsonData">登记信息保存暂存信息表</param>
        /// <param name="spInfo">审批信息表</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="zxInfo">注销信息</param>
        /// <returns>多表操作影响记录数之和</returns>
        public int Auditing(BankAuthorize AuzInfo, REGISTRATION_INFO regInfo, SysDataRecorderModel jsonData, SPB_INFO spInfo, IFLOW_DO_ACTION flowInfo, XGDJZX_INFO zxInfo)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);

            try
            {
                int count = 0;
                this._dbTransManagement.BeginTran();
                count += base.Db.Updateable(jsonData).UpdateColumns(D => new
                {
                    D.SAVEDATAJSON
                }).Where(S => S.BUS_PK == spInfo.XID).ExecuteCommand();

                count += base.Db.Updateable(regInfo).UpdateColumns(R => new
                {
                    R.IS_ACTION_OK
                }).Where(S => S.XID == regInfo.XID).ExecuteCommand();

                base.Db.Deleteable<SPB_INFO>().Where(S => S.XID == spInfo.XID).ExecuteCommand();
                count += base.Db.Updateable(zxInfo).UpdateColumns(zl => new
                {
                    zl.SPRQ,
                    zl.SPBZ,
                }).Where(S => S.XID == zxInfo.XID).ExecuteCommand();
                count += base.Db.Insertable(spInfo).ExecuteCommand();
                count += base.Db.Updateable(AuzInfo).UpdateColumns(auz => new
                {
                    auz.STATUS,
                    auz.PRE_STATUS
                }).Where(S => S.BID == AuzInfo.BID).ExecuteCommand();
                count = base.Db.Insertable(flowInfo).InsertColumns(fw => new
                {
                    fw.PK,
                    fw.FLOW_ID,
                    fw.PRE_FLOW_ID,
                    fw.AUZ_ID,
                    fw.CDATE,
                    fw.USER_NAME
                }).ExecuteCommand();

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
    }
}
