using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class AuthorizationServices : BaseServices, IAuthorizationServices
    {
        private readonly ILogger<AuthorizationServices> _logger;
        IDBTransManagement _dbTransManagement;
        public AuthorizationServices(IDBTransManagement dbTransManagement, ILogger<AuthorizationServices> logger) : base(dbTransManagement)
        {
            _logger = logger;
            this._dbTransManagement = dbTransManagement;
        }

        public async Task<int> Authorization(BankAuthorize bank, REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, List<OrderHouseAssociation> ationList, DY_INFO ParcelInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            try
            {
                this._dbTransManagement.BeginTran();

                BankAuthorize bankAuthorize = new BankAuthorize()
                {
                    BID = DjInfo.AUZ_ID,
                    STATUS = flowInfo.FLOW_ID
                };
                await base.Db.Updateable(bankAuthorize).UpdateColumns(it => new { it.STATUS }).ExecuteCommandAsync();

                int count = await base.Db.Insertable(ationList.ToArray()).InsertColumns(at => new
                {
                    at.OID,
                    at.BID,
                    at.CERTIFICATENUMBER,
                    at.ACCEPTANCENUMBER,
                    at.NUMBERID,
                    at.ADDRESS,
                    at.rightname,
                    at.AUTHORIZATIONDATE
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

        public async Task<int> Authorization(REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, List<OrderHouseAssociation> ationList, DY_INFO ParcelInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            try
            {
                // 父受理编号为登记簿的受理编号 XgdjglInfo.FSLBH
                ParcelInfo.DYSW = 1;//抵押顺位，得计算是否之前有抵押

                this._dbTransManagement.BeginTran();
                int count = await base.Db.Insertable(ationList.ToArray()).InsertColumns(at => new
                {
                    at.OID,
                    at.BID,
                    at.CERTIFICATENUMBER,
                    at.ACCEPTANCENUMBER,
                    at.NUMBERID,
                    at.ADDRESS,
                    at.rightname,
                    at.AUTHORIZATIONDATE
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

        public async Task<int> DeleteAuthorizationInfo(string bid, string slbh)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            try
            {
                this._dbTransManagement.BeginTran();
                int count = await base.Db.Deleteable<OrderHouseAssociation>().Where(it => it.BID == bid).ExecuteCommandAsync();
                count = await base.Db.Deleteable<IFLOW_DO_ACTION>().Where(it => it.AUZ_ID == bid).ExecuteCommandAsync();
                count = await base.Db.Deleteable<REGISTRATION_INFO>().Where(it => it.AUZ_ID == bid && it.YWSLBH == slbh).ExecuteCommandAsync();
                count = await base.Db.Deleteable<QLRGL_INFO>().Where(it => it.SLBH == slbh && it.QLRLX == "抵押人").ExecuteCommandAsync();
                count = await base.Db.Deleteable<TSGL_INFO>().Where(it => it.SLBH == slbh).ExecuteCommandAsync();
                count = await base.Db.Deleteable<XGDJGL_INFO>().Where(it => it.ZSLBH == slbh && it.BGLX == "抵押").ExecuteCommandAsync();
                count = await base.Db.Deleteable<DY_INFO>().Where(it => it.SLBH == slbh).ExecuteCommandAsync();
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
        public async Task<List<QLRGL_INFO>> GetAuthorizationDyrList(string slbh)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            base.ChangeDB(SysConst.DB_CON_IIRS);
            return await base.Db.Queryable<QLRGL_INFO>().Where(qlrgl => (qlrgl.SLBH == slbh && qlrgl.QLRLX == "抵押人")).ToListAsync();            
        }

        /// <summary>
        /// 获取房屋信息
        /// </summary>
        /// <param name="bid"></param>
        /// <param name="flowId"></param>
        /// <returns></returns>
        public async Task<List<OrderHouseAssociation>> GetAuthorizationHouseList(string bid, int flowId)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            Expression<Func<OrderHouseAssociation, REGISTRATION_INFO, XGDJGL_INFO, BankAuthorize,TSGL_INFO, object[]>> _joinExpression = (a, b, c, d, e) => new object[]
                   { JoinType.Inner, a.BID == b.AUZ_ID , JoinType.Inner, a.ACCEPTANCENUMBER == c.FSLBH,JoinType.Inner ,d.BID == a.BID, JoinType.Inner ,e.TSTYBM == a.NUMBERID};

            Expression<Func<OrderHouseAssociation, REGISTRATION_INFO, XGDJGL_INFO, BankAuthorize, TSGL_INFO, OrderHouseAssociation>> _selectExpression = (a, b, c, d, e) => new OrderHouseAssociation() { BID = a.BID, CERTIFICATENUMBER = a.CERTIFICATENUMBER, ACCEPTANCENUMBER = a.ACCEPTANCENUMBER,NUMBERID = a.NUMBERID,ADDRESS = a.ADDRESS,rightname = a.rightname,AUTHORIZATIONDATE = a.AUTHORIZATIONDATE,newSlbh = b.YWSLBH,xgzlx = c.XGZLX,bdclx = e.BDCLX };

            Expression<Func<OrderHouseAssociation, REGISTRATION_INFO, XGDJGL_INFO, BankAuthorize,TSGL_INFO, bool>> _whereExpression =
            (a, b, c, d, e) => a.BID == bid && d.STATUS == flowId;

            

            return await base.Query<OrderHouseAssociation, REGISTRATION_INFO, XGDJGL_INFO, BankAuthorize, TSGL_INFO, OrderHouseAssociation>(_joinExpression, _selectExpression, _whereExpression);
        }

        //public async Task<PageModel<BankAuthorize>> GetAuthorizationListToPage(int intPageIndex, string zjhm, string jbr, int flowId)
        //{
        //    base.ChangeDB(SysConst.DB_CON_IIRS);
        //    base.Db.Aop.OnLogExecuting = (sql, pars) =>
        //    {
        //        _logger.LogDebug(sql);
        //    };

        //    string first = zjhm.Substring(0, 5);
        //    string second = zjhm.Substring(10, 8);
        //    string fifteenZjhm = first + second;

        //    Expression<Func<BankAuthorize, REGISTRATION_INFO,IFLOW_ACTION, object[]>> _joinExpression = (a, b, c) => new object[]
        //         { JoinType.Inner, a.BID == b.AUZ_ID,JoinType.Inner, a.STATUS == c.FLOW_ID};

        //    Expression<Func<BankAuthorize, REGISTRATION_INFO,IFLOW_ACTION, BankAuthorize>> _selectExpression = (a, b, c) => new BankAuthorize() { BID = a.BID, rightname = a.rightname, DOCUMENTTYPE = a.DOCUMENTTYPE, DOCUMENTNUMBER = a.DOCUMENTNUMBER, AUTHORIZATIONDATE = a.AUTHORIZATIONDATE, AUTHORIZATIONDEADLINE = a.AUTHORIZATIONDEADLINE, STATUS = a.STATUS, FlowName = c.FLOW_NAME,BankCode = a.BankCode,BankName = a.BankName };

        //    Expression<Func<BankAuthorize,REGISTRATION_INFO, IFLOW_ACTION, bool>> _whereExpression =
        //    (a, b, c) => (a.DOCUMENTNUMBER.Contains(zjhm) || a.DOCUMENTNUMBER.Contains(fifteenZjhm)) && a.BankName == jbr && a.STATUS == flowId;
            

        //    string _strOrderByFileds = "a.AUTHORIZATIONDATE desc";

        //    return await base.QueryResultList<BankAuthorize, REGISTRATION_INFO, IFLOW_ACTION, BankAuthorize>(_joinExpression, _selectExpression, _whereExpression, intPageIndex, SysConst.SYS_DEFAULT_PAGE_SIZE_TEN, _strOrderByFileds);

        //}

        public async Task<PageModel<BankAuthorize>> GetAuthorizationListToPage(int intPageIndex, string zjhm, string userId, int flowId)
        {
            string first;
            string second;
            string fifteenZjhm = "";
            if (zjhm != null)
            {
                first = zjhm.Substring(0, 5);
                second = zjhm.Substring(10, 8);
                fifteenZjhm = first + second;
            }
            //string Fifteen_zjhm = vModel.zjhm[0..6] + vModel.zjhm[8..^1];
            base.ChangeDB(SysConst.DB_CON_IIRS);
            Expression<Func<BankAuthorize, REGISTRATION_INFO, IFLOW_ACTION, bool>> _whereExpression;
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            Expression<Func<BankAuthorize, REGISTRATION_INFO, IFLOW_ACTION, object[]>> _joinExpression = (a, b, c) => new object[]
                  { JoinType.Inner, a.BID == b.AUZ_ID,JoinType.Inner, a.STATUS == c.FLOW_ID};

            Expression<Func<BankAuthorize, REGISTRATION_INFO, IFLOW_ACTION, BankAuthorize>> _selectExpression = (a, b, c) => new BankAuthorize() { BID = a.BID, rightname = a.rightname, DOCUMENTTYPE = a.DOCUMENTTYPE, DOCUMENTNUMBER = a.DOCUMENTNUMBER, AUTHORIZATIONDATE = a.AUTHORIZATIONDATE, AUTHORIZATIONDEADLINE = a.AUTHORIZATIONDEADLINE, STATUS = a.STATUS, FlowName = c.FLOW_NAME, BankCode = a.BankCode, BankName = a.BankName };

            if(zjhm == null && flowId == 0)
            {
                _whereExpression = (a, b, c) => b.USER_ID == userId && b.NEXT_XID == null;
            }
            else if(zjhm != null && flowId == 0)
            {
                _whereExpression = (a, b, c) => (a.DOCUMENTNUMBER.Contains(fifteenZjhm) || a.DOCUMENTNUMBER.Contains(zjhm)) && b.USER_ID == userId && b.NEXT_XID == null;
            }
            else
            {
                _whereExpression = (a, b, c) =>  b.USER_ID == userId && a.STATUS == flowId && b.NEXT_XID == null;
            }


            string _strOrderByFileds = "a.AUTHORIZATIONDATE desc";

            return await base.QueryResultList<BankAuthorize, REGISTRATION_INFO, IFLOW_ACTION, BankAuthorize>(_joinExpression, _selectExpression, _whereExpression, intPageIndex, SysConst.SYS_DEFAULT_PAGE_SIZE_TEN, _strOrderByFileds);

        }

        /// <summary>
        /// 获取抵押信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        public async Task<List<DY_INFO>> GetDyInfoModel(string slbh)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            base.ChangeDB(SysConst.DB_CON_IIRS);
            return await base.Db.Queryable<DY_INFO>().Where(a => (a.SLBH == slbh)).ToListAsync();
        }

        public async Task<List<PrintHouseVModel>> GetPrintHouseModels(string bdczmh)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            try
            {
                var FwQllxResult = await base.Db.Queryable<SYS_DIC>().Where(a => (a.GID == 4)).ToListAsync();
                var FwQlxzResult = await base.Db.Queryable<SYS_DIC>().Where(a => (a.GID == 3)).ToListAsync();
                var TdQllxResult = await base.Db.Queryable<SYS_DIC>().Where(a => (a.GID == 6)).ToListAsync();
                var TdQlxzResult = await base.Db.Queryable<SYS_DIC>().Where(a => (a.GID == 7)).ToListAsync();

                base.ChangeDB(SysConst.DB_CON_BDC);
                PrintHouseVModel model = null;
                List<PrintHouseVModel> modelList = new List<PrintHouseVModel>();

                var result = await base.Db.Queryable<DJ_DY, DJ_XGDJGL, DJ_DJB, QL_FWXG, QL_TDXG>((A, B, C, D, E) => new object[] { JoinType.Inner, A.SLBH == B.ZSLBH, JoinType.Inner, C.SLBH == B.FSLBH, JoinType.Left, D.SLBH == C.SLBH, JoinType.Left, E.SLBH == C.SLBH }).Where((A, B, C, D, E) => (A.BDCZMH.Contains(bdczmh))).Select((A, B, C, D, E) => new
                {
                    SLBH = C.SLBH,
                    BDCZH = C.BDCZH,
                    BDCDYH = C.BDCDYH,
                    QLLX = D.QLLX,
                    QLXZ = D.QLXZ,
                    JZMJ = D.JZMJ,
                    TDQLLX = E.QLLX,
                    TDQLXZ = E.QLXZ,
                    GYTDMJ = E.GYTDMJ,
                    DYTDMJ = E.DYTDMJ
                }).ToListAsync();

                foreach (var item in result)
                {
                    model = new PrintHouseVModel();
                    model.Slbh = item.SLBH;
                    model.Bdczh = item.SLBH;
                    model.Jzmj = item.JZMJ != null ? item.JZMJ : 0;
                    model.Dytdmj = item.DYTDMJ != null ? item.DYTDMJ : 0;
                    model.Gytdmj = item.GYTDMJ != null ? item.GYTDMJ : 0;
                    foreach (var FwQllxItem in FwQllxResult)
                    {
                        if (item.QLLX == FwQllxItem.DEFINED_CODE)
                        {
                            model.Qllx = FwQllxItem.DNAME;
                        }
                    }
                    foreach (var FwQlxzItem in FwQlxzResult)
                    {
                        if (item.QLXZ == FwQlxzItem.DEFINED_CODE)
                        {
                            model.Qlxz = FwQlxzItem.DNAME;
                        }
                    }
                    foreach (var TdQllxItem in TdQllxResult)
                    {
                        if (item.TDQLLX == TdQllxItem.DEFINED_CODE)
                        {
                            model.Tdqllx = TdQllxItem.DNAME;
                        }
                    }
                    foreach (var TdQlxzItem in TdQlxzResult)
                    {
                        if (item.TDQLXZ == TdQlxzItem.DEFINED_CODE)
                        {
                            model.Tdqlxz = TdQlxzItem.DNAME;
                        }
                    }


                    modelList.Add(model);
                }

                return modelList;
            }
            catch (Exception ex)
            {

                throw ex;
            }

            

        }

        public async Task<List<IFLOW_ACTION_GROUP>> GetIflowGroupList()
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            base.ChangeDB(SysConst.DB_CON_IIRS);
            return await base.Db.Queryable<IFLOW_ACTION_GROUP>().OrderBy("GROUP_ID").ToListAsync();
        }

        public async Task<List<IFLOW_ACTION>> GetFlowNameByGid(int gid)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            base.ChangeDB(SysConst.DB_CON_IIRS);
            return await base.Db.Queryable<IFLOW_ACTION>().Where(a => (a.GROUP_ID == gid)).OrderBy("GROUP_ID").ToListAsync();
        }
    }
}
