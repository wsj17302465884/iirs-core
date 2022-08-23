using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class ConstructionServices : BaseServices, IConstructionServices
    {
        private readonly ILogger<ConstructionServices> _logger;
        IDBTransManagement _dbTransManagement;
        public ConstructionServices(IDBTransManagement dbTransManagement, ILogger<ConstructionServices> logger) : base(dbTransManagement)
        {
            _logger = logger;
            this._dbTransManagement = dbTransManagement;
        }

        public string GetSLBH()
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            string slbh = string.Concat(base.Db.Ado.GetScalar("SELECT SLBH_SEQNUM() FROM DUAL"));//获取系统受理编号
            return slbh;
        }

        /// <summary>
        /// 在建工程提交数据
        /// </summary>
        /// <param name="DjInfo">不动产登记信息表</param>
        /// <param name="flowInfo">抵押审批流程状态时间表</param>
        /// <param name="TsglInfo">图书关联表</param>
        /// <param name="ParcelInfo">抵押信息</param>
        /// <param name="XgdjglInfos">相关登记关联</param>
        /// <param name="qlrglInfos">权利人关联</param>
        /// <returns></returns>
        public async Task<int> Construction(REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, DY_INFO ParcelInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);

            try
            {
                // 父受理编号为登记簿的受理编号 XgdjglInfo.FSLBH
                ParcelInfo.DYSW = 1;//抵押顺位，得计算是否之前有抵押
                
                this._dbTransManagement.BeginTran();
                int count = await base.Db.Insertable(ParcelInfo).InsertColumns(it => new
                {
                    it.SLBH,
                    it.YWSLBH,
                    it.DJLX,
                    it.DJYY,
                    it.XGZH,
                    it.SQRQ,
                    it.DYLX,
                    it.DYSW,
                    it.DYFS,
                    it.DYMJ,
                    it.BDBZZQSE,
                    it.PGJE,
                    it.HTH,
                    it.LXR,
                    it.LXRDH,
                    it.CNSJ,
                    it.FJ,
                    it.DLJGMC,
                    it.QLQSSJ,
                    it.QLJSSJ,
                    it.DYQX,
                    it.BDCDYH
                }).ExecuteReturnIdentityAsync();
                count = await base.Db.Insertable(DjInfo).InsertColumns(it => new
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
                    it.REMARK2
                }).ExecuteReturnIdentityAsync();

                BankAuthorize bnk = new BankAuthorize()
                {
                    BID = DjInfo.AUZ_ID,
                    STATUS = flowInfo.FLOW_ID
                };
                await base.Db.Updateable(bnk).UpdateColumns(it => new { it.STATUS }).ExecuteCommandAsync();
                count = await base.Db.Insertable(TsglInfo.ToArray()).InsertColumns(ts => new
                {
                    ts.GLBM,
                    ts.SLBH,
                    ts.BDCLX,
                    ts.TSTYBM,
                    ts.BDCDYH,
                    ts.DJZL,
                    ts.CSSJ
                }).ExecuteReturnIdentityAsync();
                count = await base.Db.Insertable(flowInfo).InsertColumns(fw => new
                {
                    fw.PK,
                    fw.FLOW_ID,
                    fw.AUZ_ID,
                    fw.CDATE,
                    fw.USER_NAME
                }).ExecuteReturnIdentityAsync();
                count = await base.Db.Insertable(XgdjglInfos.ToArray()).InsertColumns(djgl => new
                {
                    djgl.BGBM,
                    djgl.ZSLBH,
                    djgl.FSLBH,
                    djgl.BGRQ,
                    djgl.BGLX,
                    djgl.XGZLX,
                    djgl.XGZH
                }).ExecuteReturnIdentityAsync();
                count = await base.Db.Insertable(qlrglInfos.ToArray()).InsertColumns(qlrgl => new
                {
                    qlrgl.GLBM,
                    qlrgl.SLBH,
                    qlrgl.YWBM,
                    qlrgl.QLRID,
                    qlrgl.QLRMC,
                    qlrgl.ZJHM,
                    qlrgl.ZJLB,
                    qlrgl.DH,
                    qlrgl.QLRLX,
                    qlrgl.SXH
                }).ExecuteReturnIdentityAsync();
                PUB_ATT_FILE file = new PUB_ATT_FILE()
                {
                    BAK_PK = ParcelInfo.SLBH,
                    BUS_PK = DjInfo.XID
                };
                base.Db.Updateable(file).UpdateColumns(s => new { s.BUS_PK }).WhereColumns(it => new { it.BAK_PK });
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
        /// 获取抵押人信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        public async Task<List<DJ_QLR>> GetDyrInfo(string slbh)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };


            Expression<Func<DJ_QLRGL, DJ_QLR, SYS_DIC, object[]>> _joinExpression = (a, b, c) => new object[]
                  { JoinType.Inner, a.QLRID == b.QLRID, JoinType.Inner, b.ZJLB == c.DEFINED_CODE };

            Expression<Func<DJ_QLRGL, DJ_QLR, SYS_DIC, DJ_QLR>> _selectExpression = (a, b, c) => new DJ_QLR() {QLRID = b.QLRID, QLRMC = b.QLRMC, ZJLB = c.DNAME, ZJHM = b.ZJHM, DH = b.DH ,qlrlx = a.QLRLX};

            Expression<Func<DJ_QLRGL, DJ_QLR, SYS_DIC, bool>> _whereExpression =
            (a, b, c) => c.GID == 1 && a.SLBH == slbh && (a.LIFECYCLE == 0 || a.LIFECYCLE == null);

            base.ChangeDB(SysConst.DB_CON_BDC);

            var dyrResult = await base.Query<DJ_QLRGL, DJ_QLR, SYS_DIC, DJ_QLR>(_joinExpression, _selectExpression, _whereExpression);

            List<DJ_QLR> ModelList = new List<DJ_QLR>();
            DJ_QLR model;

            foreach (var DYR in dyrResult)
            {
                model = new DJ_QLR();
                model.QLRID = DYR.QLRID;
                model.QLRMC = DYR.QLRMC;
                model.ZJLB = DYR.ZJLB;
                model.ZJHM = DYR.ZJHM;
                model.DH = DYR.DH;
                model.qlrlx = DYR.qlrlx;
                ModelList.Add(model);
            }
            return ModelList;
        }

        /// <summary>
        /// 获取幢编号
        /// </summary>
        /// <param name="zdtybm"></param>
        /// <returns></returns>
        public async Task<List<FC_Z_QSDC>> GetFwbhList(string zdtybm)
        {
            Expression<Func<ZD_QSDC, FC_Z_QSDC, object[]>> _joinExpression = (a, b) => new object[]
                  { JoinType.Inner, a.ZDTYBM == b.ZDTYBM};

            Expression<Func<ZD_QSDC, FC_Z_QSDC, FC_Z_QSDC>> _selectExpression = (a, b) => new FC_Z_QSDC() { TSTYBM = b.TSTYBM, FWBH = b.FWBH };

            Expression<Func<ZD_QSDC, FC_Z_QSDC, bool>> _whereExpression =
            (a, b) => a.ZDTYBM != null && a.ZDTYBM == zdtybm && (a.LIFECYCLE == 0 || a.LIFECYCLE == null);

            base.ChangeDB(SysConst.DB_CON_BDC);

            return await base.Query<ZD_QSDC, FC_Z_QSDC, FC_Z_QSDC>(_joinExpression, _selectExpression, _whereExpression);
        }
        /// <summary>
        /// 获取宗地统一编码
        /// </summary>
        /// <param name="zjhm"></param>
        /// <param name="bdczh"></param>
        /// <param name="qlrmc"></param>
        /// <returns></returns>
        public async Task<List<DJ_TSGL>> GetZdTstybmByZjhm(string zjhm, string bdczh, string qlrmc)
        {
            Expression<Func<DJ_TSGL, DJ_DJB,DJ_QLRGL, SYS_DIC, object[]>> _joinExpression = (a, b, c, d) => new object[]
                  { JoinType.Inner, a.SLBH == b.SLBH , JoinType.Inner, c.SLBH == b.SLBH,JoinType.Inner ,c.ZJLB == d.DEFINED_CODE};

            Expression<Func<DJ_TSGL, DJ_DJB, DJ_QLRGL, SYS_DIC, DJ_TSGL>> _selectExpression = (a, b, c, d) => new DJ_TSGL() { TSTYBM = a.TSTYBM, dyr = c.QLRMC, zjlb_zwm = d.DNAME };

            Expression<Func<DJ_TSGL, DJ_DJB, DJ_QLRGL, SYS_DIC, bool>> _whereExpression =
            (a, b, c,d) => (a.LIFECYCLE == 0 || a.LIFECYCLE == null) && (b.LIFECYCLE == 0 || b.LIFECYCLE == null) && (c.LIFECYCLE == 0 || c.LIFECYCLE == null) && c.ZJHM == zjhm && c.QLRMC.Contains(qlrmc) && b.BDCZH.Contains(bdczh) && a.BDCLX == "宗地" && c.QLRLX == "权利人" && d.GID == 1;

            base.ChangeDB(SysConst.DB_CON_BDC);

            return await base.Query<DJ_TSGL, DJ_DJB, DJ_QLRGL, SYS_DIC, DJ_TSGL>(_joinExpression, _selectExpression, _whereExpression);
        }

        /// <summary>
        /// 抵押变更提交数据
        /// </summary>
        /// <param name="DjInfo">不动产登记信息表</param>
        /// <param name="flowInfo">抵押审批流程状态时间表</param>
        /// <param name="TsglInfo">图书关联表</param>
        /// <param name="ationList">订单房屋关联</param>
        /// <param name="ParcelInfo">抵押信息</param>
        /// <param name="XgdjglInfos">相关登记关联</param>
        /// <param name="qlrglInfos">权利人关联</param>
        /// <param name="attFileInfos">权利人关联</param>
        /// <returns></returns>        
        public async Task<int> Certification(REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, List<OrderHouseAssociation> ationList, DY_INFO ParcelInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, List<PUB_ATT_FILE> attFileInfos)
        {
            int count = 0;
            base.ChangeDB(SysConst.DB_CON_IIRS);
            try
            {
                // 父受理编号为登记簿的受理编号 XgdjglInfo.FSLBH
                ParcelInfo.DYSW = 1;//抵押顺位，得计算是否之前有抵押
                var isInsert = base.Db.Queryable<REGISTRATION_INFO>().Single(s => s.YWSLBH == DjInfo.YWSLBH) == null;
                this._dbTransManagement.BeginTran();
                if(isInsert)
                {
                    count = await base.Db.Insertable(DjInfo).InsertColumns(it => new
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
                        it.REMARK2
                    }).ExecuteReturnIdentityAsync();

                    count = await base.Db.Insertable(flowInfo).InsertColumns(fw => new
                    {
                        fw.PK,
                        fw.FLOW_ID,
                        fw.AUZ_ID,
                        fw.CDATE,
                        fw.USER_NAME
                    }).ExecuteReturnIdentityAsync();

                    count = await base.Db.Insertable(ParcelInfo).InsertColumns(it => new
                    {
                        it.SLBH,
                        it.YWSLBH,
                        it.DJLX,
                        it.DJYY,
                        it.XGZH,
                        it.SQRQ,
                        it.DYLX,
                        it.DYSW,
                        it.DYFS,
                        it.DYMJ,
                        it.BDBZZQSE,
                        it.PGJE,
                        it.HTH,
                        it.LXR,
                        it.LXRDH,
                        it.CNSJ,
                        it.FJ,
                        it.DLJGMC,
                        it.QLQSSJ,
                        it.QLJSSJ,
                        it.DYQX,
                        it.BDCDYH
                    }).ExecuteReturnIdentityAsync();
                }
                else
                {
                    count = await base.Db.Updateable<DY_INFO>(ParcelInfo).UpdateColumns(it => new
                    {
                        it.SLBH,
                        it.DJLX,
                        it.DJYY,
                        it.XGZH,
                        it.SQRQ,
                        it.DYLX,
                        it.DYSW,
                        it.DYFS,
                        it.DYMJ,
                        it.BDBZZQSE,
                        it.PGJE,
                        it.HTH,
                        it.LXR,
                        it.LXRDH,
                        it.CNSJ,
                        it.FJ,
                        it.DLJGMC,
                        it.QLQSSJ,
                        it.QLJSSJ,
                        it.DYQX
                    }).WhereColumns(itw => new
                    {
                        itw.SLBH
                    }).ExecuteCommandAsync();

                    count = await base.Db.Updateable(DjInfo).UpdateColumns(it => new
                    {
                        it.XID,
                        it.YWSLBH,
                        it.DJZL,
                        it.BDCZH,
                        it.SLBH,
                        it.QLRMC,
                        it.ZL,
                        it.ORG_ID,
                        it.USER_ID,
                        it.TEL,
                        it.HTH
                    }).WhereColumns(itw => new
                    {
                        itw.YWSLBH
                    }).ExecuteCommandAsync();
                }

                BankAuthorize bnk = new BankAuthorize()
                {
                    BID = DjInfo.AUZ_ID,
                    STATUS = flowInfo.FLOW_ID
                };
                await base.Db.Updateable(bnk).UpdateColumns(it => new { it.STATUS }).ExecuteCommandAsync();

                if (TsglInfo.Count > 0)
                {
                    count = await base.Db.Deleteable<TSGL_INFO>().In(it => it.SLBH, TsglInfo.Cast<TSGL_INFO>().Select(s => s.SLBH).ToArray()).ExecuteCommandAsync();
                    count = await base.Db.Insertable(TsglInfo.ToArray()).InsertColumns(ts => new
                    {
                        ts.GLBM,
                        ts.SLBH,
                        ts.BDCLX,
                        ts.TSTYBM,
                        ts.BDCDYH,
                        ts.DJZL,
                        ts.CSSJ
                    }).ExecuteReturnIdentityAsync();
                }
                if (XgdjglInfos.Count > 0)
                {
                    count = await base.Db.Deleteable<XGDJGL_INFO>().In(it => it.ZSLBH, XgdjglInfos.Cast<XGDJGL_INFO>().Select(s => s.ZSLBH).ToArray()).ExecuteCommandAsync();
                    count = await base.Db.Insertable(XgdjglInfos.ToArray()).InsertColumns(djgl => new
                    {
                        djgl.BGBM,
                        djgl.ZSLBH,
                        djgl.FSLBH,
                        djgl.BGRQ,
                        djgl.BGLX,
                        djgl.XGZLX,
                        djgl.XGZH
                    }).ExecuteReturnIdentityAsync();
                }

                if (qlrglInfos.Count > 0)
                {
                    count = await base.Db.Deleteable<QLRGL_INFO>().In(it => it.SLBH, qlrglInfos.Cast<QLRGL_INFO>().Select(s => s.SLBH).ToArray()).ExecuteCommandAsync();
                    count = await base.Db.Insertable(qlrglInfos.ToArray()).InsertColumns(qlrgl => new
                    {
                        qlrgl.GLBM,
                        qlrgl.SLBH,
                        qlrgl.YWBM,
                        qlrgl.QLRID,
                        qlrgl.QLRMC,
                        qlrgl.ZJHM,
                        qlrgl.ZJLB,
                        qlrgl.DH,
                        qlrgl.QLRLX,
                        qlrgl.SXH
                    }).ExecuteReturnIdentityAsync();
                }

                if (attFileInfos != null)
                {
                    count = await base.Db.Deleteable<PUB_ATT_FILE>().In(it => it.BUS_PK, attFileInfos.Cast<PUB_ATT_FILE>().Select(s => s.BUS_PK).ToArray()).ExecuteCommandAsync();
                    count = await base.Db.Insertable(attFileInfos.ToArray()).ExecuteReturnIdentityAsync();
                }

                count = await base.Db.Insertable(ationList.ToArray()).InsertColumns(at => new
                {
                    at.OID,
                    at.BID,
                    at.CERTIFICATENUMBER,
                    at.ACCEPTANCENUMBER,
                    at.NUMBERID,
                    at.ADDRESS,
                    at.rightname,
                    at.AUTHORIZATIONDATE,
                    at.qllx,
                    at.qlxz,
                    at.jzmj,
                    at.tdqllx,
                    at.tdqlxz,
                    at.tdmj,
                    at.bdclx,
                    at.bdcdyh
                }).ExecuteReturnIdentityAsync();
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
