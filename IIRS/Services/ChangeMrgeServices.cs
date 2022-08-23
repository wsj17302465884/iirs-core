using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services
{
    /// <summary>
    /// 转移抵押合并办理
    /// </summary>
    public class ChangeMrgeServices : BaseServices, IChangeMrgeServices
    {
        private readonly ILogger<ChangeMrgeServices> _logger;
        private readonly IDBTransManagement _dbTransManagement;
        public ChangeMrgeServices(IDBTransManagement dbTransManagement, ILogger<ChangeMrgeServices> logger) : base(dbTransManagement)
        {
            _logger = logger;
            _dbTransManagement = dbTransManagement;
        }

        /// <summary>
        /// 保存转移抵押合并办理
        /// </summary>
        /// <param name="isInsert">当前操作是否暂存</param>
        /// <param name="DjInfo">收件单表信息</param>
        /// <param name="AuzInfo">订单表信息</param>
        /// <param name="jsonData">登记信息保存暂存信息表</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="spInfo">审批信息表</param>
        /// <param name="djbInfo">登记簿信息表</param>
        /// <param name="TsglInfo">图属关联</param>
        /// <param name="DyInfo">抵押信息</param>
        /// <param name="XgdjglInfos">相关登记关联信息</param>
        /// <param name="qlrglInfos">权利人关联信息</param>
        /// <param name="qlxgInfo">权利相关信息</param>
        /// <param name="uploadFiles">附件信息</param>
        /// <param name="sfdList">收费单（抵押、登记）</param>
        /// <param name="sfdDetailsList">收费单明细（抵押、登记）</param>
        /// <param name="sjdList">收件单</param>
        /// <param name="OldXID">历史XID（仅当退回编辑时使用）</param>
        /// <returns></returns>
        public int ChangeMrgesSave(bool isInsert, REGISTRATION_INFO DjInfo, BankAuthorize AuzInfo, SysDataRecorderModel jsonData, IFLOW_DO_ACTION flowInfo, SPB_INFO spInfo, DJB_INFO djbInfo,List<TSGL_INFO> TsglInfo, DY_INFO DyInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, QL_XG_INFO qlxgInfo, List<PUB_ATT_FILE> uploadFiles, List<SFD_INFO> sfdList, List<SFD_FB_INFO> sfdDetailsList, List<SJD_INFO> sjdList, string OldXID)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            //var flowGroup = base.Db.Queryable<IFLOW_ACTION>().Where(F => F.FLOW_ID == flowInfo.FLOW_ID).Single();
            //if (flowGroup != null)
            //{
            //    DjInfo.DJZL = flowGroup.GROUP_ID;
            //}
            //else
            //{
            //    throw new ApplicationException("错误的流程节点编号:" + flowInfo.FLOW_ID);
            //}
            try
            {
                int count = 0;
                this._dbTransManagement.BeginTran();
                //base.Db.Aop.OnLogExecuting = (sql, pars) =>
                //{
                //    _logger.LogDebug(sql);
                //};
                if (AuzInfo != null)//说明是暂存不做任何流程操作
                {
                    var bankDb = base.Db.Queryable<BankAuthorize>().Where(W => W.BID == AuzInfo.BID).Single();
                    if (bankDb != null)//为旧件
                    {
                        count += base.Db.Updateable(AuzInfo).UpdateColumns(auz => new
                        {
                            auz.STATUS,
                            auz.PRE_STATUS
                        }).Where(b => b.BID == AuzInfo.BID).ExecuteCommand();
                    }
                    else
                    {
                        if (AuzInfo != null)
                        {
                            count += base.Db.Insertable(AuzInfo).InsertColumns(bank => new
                            {
                                bank.BID,
                                bank.AUTHORIZATIONDATE,
                                bank.STATUS,
                                bank.PRE_STATUS
                            }).ExecuteCommand();
                        }
                    }
                    if (flowInfo != null)
                    {
                        count = base.Db.Insertable(flowInfo).InsertColumns(fw => new
                        {
                            fw.PK,
                            fw.FLOW_ID,
                            fw.PRE_FLOW_ID,
                            fw.AUZ_ID,
                            fw.CDATE,
                            fw.USER_NAME
                        }).ExecuteCommand();
                    }
                }
                
                bool isBack = !string.IsNullOrEmpty(OldXID);//历史XID，说明为退回件
                if (isBack)//如果是退回件，将当前XID置为历史
                {
                    REGISTRATION_INFO setHistory = new REGISTRATION_INFO()
                    {
                        NEXT_XID = djbInfo.xid
                    };
                    count = base.Db.Updateable(setHistory).UpdateColumns(it => new
                    {
                        it.NEXT_XID
                    }).Where(S => S.XID == OldXID).ExecuteCommand();

                    SysDataRecorderModel historyJson = new SysDataRecorderModel()
                    {
                        IS_STOP = 1
                    };
                    
                    count = base.Db.Updateable(historyJson).UpdateColumns(it => new
                    {
                        it.IS_STOP
                    }).Where(S => S.BUS_PK == OldXID).ExecuteCommand();
 
                    count += base.Db.Updateable(new SPB_INFO() { XID = DjInfo.XID }).UpdateColumns(sp => new
                    {
                        sp.XID
                    }).Where(S => S.XID == OldXID).ExecuteCommand();
                    count += base.Db.Insertable(DjInfo).ExecuteCommand();
                    count += base.Db.Insertable(jsonData).ExecuteCommand();
                }
                if (isInsert)//如果为新增操作
                {
                    //if (!isBack)//如果不是退回件，启动新流程
                    //{

                    //}
                    //else
                    //{
                    //    base.Db.Deleteable<SFD_FB_INFO>().Where(D => D.XID == OldXID).ExecuteCommand();
                    //    base.Db.Deleteable<SFD_INFO>().Where(D => D.XID == OldXID).ExecuteCommand();
                    //    base.Db.Deleteable<DJB_INFO>().Where(D => D.xid == OldXID).ExecuteCommand();
                    //    base.Db.Deleteable<DY_INFO>().Where(D => D.XID == OldXID).ExecuteCommand();
                    //    base.Db.Deleteable<QL_XG_INFO>().Where(D => D.XID == OldXID).ExecuteCommand();
                    //}
                    
                    count += base.Db.Insertable(DjInfo).ExecuteCommand();
                    count += base.Db.Insertable(djbInfo).ExecuteCommand();
                    count += base.Db.Insertable(DyInfo).ExecuteCommand();
                    count += base.Db.Insertable(jsonData).ExecuteCommand();
                    if (spInfo != null)
                    {
                        count += base.Db.Insertable(spInfo).ExecuteCommand();
                    }
                    count += base.Db.Insertable(qlxgInfo).ExecuteCommand();
                }
                else
                {
                    count += base.Db.Updateable(qlxgInfo).ExecuteCommand();
                    if (spInfo != null)
                    {
                        count += base.Db.Updateable(spInfo).UpdateColumns(sp => new
                        {
                            sp.SLBH,
                            sp.SPDX,
                            sp.SPYJ,
                            sp.SPR,
                            sp.SPRQ,
                            sp.SPTXR,
                            sp.XID
                        }).Where(S => S.XID == spInfo.XID).ExecuteCommand();
                    }
                    count = base.Db.Updateable(DjInfo).UpdateColumns(it => new
                    {
                        it.BDCZH,
                        it.QLRMC,
                        it.ZL,
                        it.ORG_ID,
                        it.TEL,
                        it.HTH,
                        it.SJR,
                        it.PDH,
                        it.IS_ACTION_OK
                    }).Where(S => S.XID == DjInfo.XID).ExecuteCommand();
                    count += base.Db.Updateable(djbInfo).UpdateColumns(dj => new
                    {
                        dj.SLBH,
                        dj.DJLX,
                        dj.DJYY,
                        dj.SQRQ,
                        dj.BDCZH,
                        dj.GYFS,
                        dj.ZSLX,
                        dj.QT,
                        dj.FJ,
                        dj.XGZH,
                        dj.SSJC,
                        dj.JGJC,
                        dj.FZND,
                        dj.ZSH,
                        dj.xid
                    }).ExecuteCommand();
                    base.Db.Updateable(jsonData).UpdateColumns(json => new
                    {
                        json.SAVEDATAJSON,
                        json.REMARKS1,
                        json.REMARKS2,
                        json.REMARKS3,
                        json.REMARKS4,
                        json.REMARKS5
                    }).Where(S => S.BUS_PK == jsonData.BUS_PK).ExecuteCommand();

                    count += base.Db.Updateable(DyInfo).UpdateColumns(dy => new
                    {
                        dy.SLBH,
                        dy.DYLX,
                        dy.DJYY,
                        dy.XGZH,
                        dy.SQRQ,
                        dy.QT,
                        dy.DYSW,
                        dy.DYFS,
                        dy.DYMJ,
                        dy.BDBZZQSE,
                        dy.PGJE,
                        dy.HTH,
                        dy.LXR,
                        dy.LXRDH,
                        dy.CNSJ,
                        dy.FJ,
                        dy.ZWR,
                        dy.ZWRZJH,
                        dy.ZWRZJLX,
                        dy.DLJGMC,
                        dy.QLQSSJ,
                        dy.QLJSSJ,
                        dy.DYQX,
                        dy.XID,
                        dy.ZGZQSE
                    }).Where(D => D.SLBH == DyInfo.SLBH).ExecuteCommand();
                    //由于修改数据集合对象，所以先删除再进行插入操作
                    count += base.Db.Deleteable<TSGL_INFO>().Where(S => S.XID == DjInfo.XID).ExecuteCommand();
                    count += base.Db.Deleteable<XGDJGL_INFO>().Where(S => S.XID == DjInfo.XID).ExecuteCommand();
                    count += base.Db.Deleteable<QLRGL_INFO>().Where(S => S.XID == DjInfo.XID).ExecuteCommand();
                    count += base.Db.Deleteable<PUB_ATT_FILE>().Where(S => S.XID == DjInfo.XID).ExecuteCommand();
                    count += base.Db.Deleteable<SJD_INFO>().Where(S => S.XID == DjInfo.XID).ExecuteCommand();
                }
                count += base.Db.Insertable(sjdList.ToArray()).ExecuteCommand();
                if (sfdList.Count > 0)
                {
                    count += base.Db.Insertable(sfdList.ToArray()).ExecuteCommand();
                }
                if (sfdDetailsList.Count > 0)
                {
                    count += base.Db.Insertable(sfdDetailsList.ToArray()).ExecuteCommand();
                }
                if (TsglInfo.Count > 0)
                {
                    count += base.Db.Insertable(TsglInfo.ToArray()).ExecuteCommand();
                }
                if (XgdjglInfos.Count > 0)
                {
                    count += base.Db.Insertable(XgdjglInfos.ToArray()).ExecuteCommand();
                }
                if (qlrglInfos.Count > 0)
                {
                    count += base.Db.Insertable(qlrglInfos.ToArray()).InsertColumns(qlrgl => new
                    {
                        qlrgl.GLBM,
                        qlrgl.SLBH,
                        qlrgl.YWBM,
                        qlrgl.ZJHM,
                        qlrgl.QLRID,
                        qlrgl.QLRMC,
                        qlrgl.GYFE,
                        qlrgl.GYFS,
                        qlrgl.ZJLB,
                        qlrgl.ZJLB_ZWM,
                        qlrgl.DH,
                        qlrgl.QLRLX,
                        qlrgl.SXH,
                        qlrgl.IS_OWNER,
                        qlrgl.XID
                    }).ExecuteCommand();
                }
                if (uploadFiles != null && uploadFiles.Count > 0)
                {
                    count = base.Db.Insertable(uploadFiles.ToArray()).ExecuteCommand();
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

        /// <summary>
        /// 获取本年度不动产证明编号计数器值
        /// </summary>
        /// <returns>不动产证明号+证明号编号</returns>
        public async Task<string[]> GetBDCZH_NUM()
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            string numStr = string.Concat(await base.Db.Ado.GetScalarAsync("SELECT BDCZH_SEQNUM() FROM DUAL"));
            int num = -1;
            if (int.TryParse(numStr, out num))
            {
                return new string[] { $"辽({DateTime.Now.Year})辽阳市不动产权第{numStr}号", numStr };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 查询权利人信息
        /// </summary>
        /// <param name="bdzcz">不动产证号</param>
        /// <returns></returns>
        public async Task<ChangeMrgeHouseVModel> GetHouseInfo(string bdzcz)
        {
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            base.ChangeDB(SysConst.DB_CON_BDC);

            var resultHouse = await base.Db.Queryable<DJ_DJB, DJ_SJD, DJ_QLRGL, DJ_QLR, DJ_TSGL>((B, S, QLRGL, QLR, TS) => B.SLBH == S.SLBH && B.SLBH == QLRGL.SLBH && QLR.QLRID == QLRGL.QLRID && TS.SLBH == B.SLBH)
.Where((B, S, QLRGL, QLR, TS) => ((B.LIFECYCLE == null || B.LIFECYCLE == 0) && QLRGL.QLRLX == "权利人"
&& (QLRGL.LIFECYCLE == null || QLRGL.LIFECYCLE == 0) && bdzcz == B.BDCZH))
.GroupBy((B, S, QLRGL, QLR, TS) => new { BDCLX = TS.BDCLX, TSTYBM = TS.TSTYBM, BDCZH = B.BDCZH, BDCDYH = TS.BDCDYH, SLBH = B.SLBH, ZL = S.ZL, ZSLX = B.ZSLX, FJ = B.FJ })
.Select((B, S, QLRGL, QLR, TS) => new ChangeMrgeHouseVModel()
{
    BDCLX = TS.BDCLX,
    TSTYBM = TS.TSTYBM,
    BDCZH = B.BDCZH,
    BDCDYH = TS.BDCDYH,
    SLBH = B.SLBH,
    QLRMC = SqlFunc.MappingColumn(QLR.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(QLR.QLRMC))"),
    ZL = S.ZL,
    ZSLX = B.ZSLX,
    FWMJ = SqlFunc.Subqueryable<QL_FWXG>().Where(F => F.SLBH == B.SLBH).Select(s => SqlFunc.AggregateMax(s.JZMJ.ToString())),
    TDMJ = SqlFunc.Subqueryable<QL_TDXG>().Where(F => F.SLBH == B.SLBH).Select(s => SqlFunc.IsNull(SqlFunc.AggregateMax(s.DYTDMJ.ToString()), SqlFunc.AggregateMax(s.GYTDMJ.ToString()))),
    FJ = B.FJ
}).FirstAsync();
            return resultHouse;
        }

        /// <summary>
        /// 查询权利人信息
        /// </summary>
        /// <param name="bdzcz">不动产证号</param>
        /// <returns></returns>
        public async Task<List<ChangeMrgePersonVModel>> GetPersonInfo(string bdzcz)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDicList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 1)).ToListAsync();
            //var dicZjlb = sysDicList.ToDictionary(x => x.DEFINED_CODE, x => x.DNAME);
            base.ChangeDB(SysConst.DB_CON_BDC);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            var resultPerson = await base.Db.Queryable<DJ_DJB, DJ_SJD, DJ_QLRGL, DJ_QLR>((B, S, QLRGL, QLR) => B.SLBH == S.SLBH && B.SLBH == QLRGL.SLBH && QLR.QLRID == QLRGL.QLRID )
.Where((B, S, QLRGL, QLR) => ((B.LIFECYCLE == null || B.LIFECYCLE == 0) && QLRGL.QLRLX == "权利人"
&& (QLRGL.LIFECYCLE == null || QLRGL.LIFECYCLE == 0) && bdzcz==B.BDCZH))
.Select((B, S, QLRGL, QLR) => new ChangeMrgePersonVModel()
{
    QLRID = QLR.QLRID,
    QLRMC = QLR.QLRMC,
    ZJLB = QLRGL.ZJLB,
    ZJHM = QLR.ZJHM,
    SXH = QLRGL.SXH,
    DH = QLRGL.DH,
    GYFS = QLRGL.GYFS,
    GYFE = QLRGL.GYFE
}).ToListAsync();
            foreach(var model in resultPerson)
            {
                var zjlb_zwmObj = sysDicList.Where(s => s.DEFINED_CODE == model.ZJLB).FirstOrDefault();
                if (zjlb_zwmObj != null)
                {
                    model.ZJLB_ZWM = zjlb_zwmObj.DNAME;
                }
            }
            return resultPerson;
        }

        /// <summary>
        /// 获取土地房屋权利信息
        /// </summary>
        /// <param name="bdzcz">(现实手)不动产证明号</param>
        /// <returns></returns>
        public async Task<QL_XG_INFO> GetLandHouseRightInfo(string bdzcz)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDicList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SqlFunc.ContainsArray(new int[] { 3, 4, 6, 5, 7, 8, 9 }, SYS.GID))).ToListAsync();
            SYS_DIC dicVal = null;
            base.ChangeDB(SysConst.DB_CON_BDC);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            var resultLandHouse = await base.Db.Queryable<QL_FWXG, DJ_TSGL, DJ_DJB, FC_H_QSDC, FC_Z_QSDC>((FW, TS, D, FC, FZ) => FW.SLBH == TS.SLBH && TS.SLBH == D.SLBH && TS.TSTYBM == FC.TSTYBM && FC.LSZTYBM == FZ.TSTYBM)
.Where((FW, TS, D, FC, FZ) => ((TS.LIFECYCLE == null || TS.LIFECYCLE == 0) && bdzcz == D.BDCZH))
.Select((FW, TS, D, FC, FZ) => new QL_XG_INFO()
{
    SLBH = D.SLBH,
    FW_QLLX = FW.QLLX,
    FW_QLXZ = FW.QLXZ,
    FW_JZMJ = FW.JZMJ,
    FW_TNJZMJ = FW.TNJZMJ,
    FW_FTJZMJ = FW.FTJZMJ,
    FW_YC_JZMJ = FC.YCJZMJ,
    FW_YC_TNJZMJ = FC.YCTNJZMJ,
    FW_YC_FTJZMJ = FC.YCFTJZMJ,
    FW_FWGHYT = FW.GHYT,
    QDFS = FW.QDFS,
    QDJG = FW.QDJG,
    PGJE = FW.PGJE,
    FW_FWJG = FZ.FWJG,
    FW_FWZCS = SqlFunc.IsNull(FC.HZCS, FZ.ZCS),
    FW_SZCS = FC.MYC
}).SingleAsync();


            if (resultLandHouse != null)
            {
                dicVal = sysDicList.Where(s => s.GID == 4 && s.DEFINED_CODE == resultLandHouse.FW_QLLX).SingleOrDefault();
                if (dicVal != null)//房屋权利类型
                {
                    resultLandHouse.FW_QLLX_ZWM = dicVal.DNAME;
                }
                dicVal = sysDicList.Where(s => s.GID == 5 && s.DEFINED_CODE == resultLandHouse.FW_FWGHYT).SingleOrDefault();
                if (dicVal != null)//房屋规划用途
                {
                    resultLandHouse.FW_FWGHYT_ZWM = dicVal.DNAME;
                }
                dicVal = sysDicList.Where(s => s.GID == 9 && s.DEFINED_CODE == resultLandHouse.FW_FWJG).SingleOrDefault();
                if (dicVal != null)//房屋结构
                {
                    resultLandHouse.FW_FWJG_ZWMS = dicVal.DNAME;
                }
                var resultLand = await base.Db.Queryable<QL_TDXG, DJ_TSGL, DJ_DJB>((TD, TS, D) => TD.SLBH == TS.SLBH && TS.SLBH == D.SLBH)
.Where((TD, TS, D) => ((TS.LIFECYCLE == null || TS.LIFECYCLE == 0) && bdzcz == D.BDCZH))
.Select((TD, TS, D) => new {
    SLBH = D.SLBH,
    TD_QLLX = TD.QLLX,
    TD_QLXZ = TD.QLXZ,
    TD_GYTDMJ = TD.GYTDMJ,
    TD_DYTDMJ = TD.DYTDMJ,
    TD_FTTDMJ = TD.FTTDMJ,
    TD_JZZDMJ = TD.JZZDMJ,
    TD_SYQX = TD.SYQX,
    TD_QSRQ = TD.QSRQ,
    TD_ZZRQ = TD.ZZRQ,
    TD_TDYT = TD.TDYT
}).SingleAsync();
                if (!string.IsNullOrEmpty(resultLandHouse.FW_QLXZ))
                {
                    dicVal = sysDicList.Where(s => s.GID == 3 && s.DEFINED_CODE == resultLandHouse.FW_QLXZ).SingleOrDefault();
                    if (dicVal != null)//房屋权利性质中文描述
                    {
                        resultLandHouse.FW_QLXZ_ZWMS += dicVal.DNAME;
                    }
                }
                if (resultLand != null)
                {
                    resultLandHouse.TD_QLLX = resultLand.TD_QLLX;
                    resultLandHouse.TD_QLXZ = resultLand.TD_QLXZ;
                    resultLandHouse.TD_GYTDMJ = resultLand.TD_GYTDMJ;
                    resultLandHouse.TD_DYTDMJ = resultLand.TD_DYTDMJ;
                    resultLandHouse.TD_FTTDMJ = resultLand.TD_FTTDMJ;
                    resultLandHouse.TD_JZZDMJ = resultLand.TD_JZZDMJ;
                    resultLandHouse.TD_SYQX = resultLand.TD_SYQX;
                    resultLandHouse.TD_QSRQ = resultLand.TD_QSRQ;
                    resultLandHouse.TD_ZZRQ = resultLand.TD_ZZRQ;
                    resultLandHouse.TD_TDYT = resultLand.TD_TDYT;
                    if (!string.IsNullOrEmpty(resultLand.TD_QLLX))
                    {
                        dicVal = sysDicList.Where(s => s.GID == 6 && s.DEFINED_CODE == resultLand.TD_QLLX).SingleOrDefault();
                        if (dicVal != null)//土地权利类型
                        {
                            resultLandHouse.TD_QLLX_ZWM = dicVal.DNAME;
                        }
                    }
                    string[] tmpStrArrat = string.Concat(resultLand.TD_QLXZ).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
                    if (tmpStrArrat.Length > 0)
                    {
                        resultLandHouse.TD_QLXZ_ZWMS = string.Empty;
                        foreach (var qlxz in tmpStrArrat)
                        {
                            dicVal = sysDicList.Where(s => s.GID == 7 && s.DEFINED_CODE == qlxz).SingleOrDefault();
                            if (dicVal != null)//土地权利性质中文描述
                            {
                                resultLandHouse.TD_QLXZ_ZWMS += "," + dicVal.DNAME;
                            }
                        }
                        if (resultLandHouse.TD_QLXZ_ZWMS.Length > 0)
                        {
                            resultLandHouse.TD_QLXZ_ZWMS = resultLandHouse.TD_QLXZ_ZWMS.Substring(1);
                        }
                    }
                    List<string> tdytFullPath = new List<string>();
                    tmpStrArrat = string.Concat(resultLand.TD_TDYT).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
                    if (tmpStrArrat.Length > 0)
                    {
                        resultLandHouse.TD_TDYT_ZWMS = string.Empty;
                        foreach (var tdyt in tmpStrArrat)
                        {
                            bool isOk = false;
                            dicVal = sysDicList.Where(s => s.GID == 8 && s.DEFINED_CODE == tdyt).SingleOrDefault();
                            if (dicVal != null)//土地规划用途中文描述
                            {
                                resultLandHouse.TD_TDYT_ZWMS += "," + dicVal.DNAME;
                                string currentNode = dicVal.FDIC_ID;
                                if (!tdytFullPath.Contains(dicVal.DEFINED_CODE))
                                {
                                    tdytFullPath.Add(dicVal.DEFINED_CODE);
                                }
                                if (!string.IsNullOrEmpty(currentNode))//如果有父节点
                                {
                                    while (!isOk)//获取该节点父节点全路径
                                    {
                                        var fDicVal = sysDicList.Where(s => s.GID == 8 && s.DIC_ID == currentNode).SingleOrDefault();
                                        if (fDicVal != null)
                                        {
                                            if (!tdytFullPath.Contains(fDicVal.DEFINED_CODE))
                                            {
                                                tdytFullPath.Add(fDicVal.DEFINED_CODE);
                                            }
                                            if (!string.IsNullOrEmpty(fDicVal.FDIC_ID))
                                            {
                                                currentNode = fDicVal.FDIC_ID;
                                                isOk = false;
                                            }
                                            else
                                            {
                                                isOk = true;
                                            }
                                        }
                                        else
                                        {
                                            isOk = true;
                                        }
                                    }
                                }
                            }
                        }
                        for (int i = tdytFullPath.Count - 1; i >= 0; i--)
                        {
                            resultLandHouse.TD_TDYT_FULL_PATH += string.Concat(",", tdytFullPath[i]);
                        }
                        //resultLandHouse.TD_TDYT_FULL_PATH = resultLandHouse.TD_TDYT_FULL_PATH.Substring(1);
                        /*if (resultLandHouse.TD_TDYT_ZWMS.Length > 0)
                        {
                            resultLandHouse.TD_TDYT_ZWMS = resultLandHouse.TD_TDYT_ZWMS.Substring(1);
                        }*/
                    }
                }
            }
            return resultLandHouse;
        }
    }
}
