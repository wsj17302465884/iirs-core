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
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class MortgageServices : BaseServices, IMortgageServices
    {
        private readonly ILogger<MortgageServices> _logger;

        IDBTransManagement _dbTransManagement;
        public MortgageServices(IDBTransManagement dbTransManagement, ILogger<MortgageServices> logger) : base(dbTransManagement)
        {
            _logger = logger;
            this._dbTransManagement = dbTransManagement;
        }

        
        /// <summary>
        /// 根据图属统一编码查询证件号码
        /// </summary>
        /// <param name="zjhm"></param>
        /// <returns></returns>
        public async Task<MessageModel<List<MortgageViewModel>>> GetTstybmByZjhm(string zjhm)
        {            
            Expression<Func<DJ_TSGL, DJ_QLRGL, object[]>> _joinExpression = (a, b) => new object[]
                 { JoinType.Inner, b.SLBH == a.SLBH};

            Expression<Func<DJ_TSGL, DJ_QLRGL, MortgageViewModel>> _selectExpression = (a, b) => new MortgageViewModel() { Tstybm = a.TSTYBM };

            Expression<Func<DJ_TSGL, DJ_QLRGL, bool>> _whereExpression =
            (a, b) => b.ZJHM == zjhm && a.DJZL == "权属" && (a.LIFECYCLE == 0 || a.LIFECYCLE == null);

            //var data = await _mortgageServices.Query<DJ_TSGL, DJ_QLRGL, MortgageViewModel>(joinExpression, selectExpression, whereExpression);


            try
            {
                var data = await base.Query<DJ_TSGL, DJ_QLRGL, MortgageViewModel>(_joinExpression, _selectExpression, _whereExpression);
                return new MessageModel<List<MortgageViewModel>>()
                {
                    msg = "获取成功",
                    success = true,
                    response = data
                };
            }
            catch (Exception ex)
            {

                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<MortgageViewModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 根据企业名称获得该企业名下的所有TSTYBM（企业权利人类型为：权利人）
        /// </summary>
        /// <param name="qlrmc"></param>
        /// <returns></returns>
        public async Task<MessageModel<List<MortgageViewModel>>> GetTstybmCountByQlrmc(string qlrmc)
        {
            Expression<Func<DJ_QLR, DJ_QLRGL, DJ_TSGL, DJ_SJD, object[]>> _joinExpression = (a, b, c, d) => new object[]
                  { JoinType.Inner, a.QLRID == b.QLRID, JoinType.Inner, c.SLBH == b.SLBH ,JoinType.Inner, c.SLBH == d.SLBH};

            Expression<Func<DJ_QLR, DJ_QLRGL, DJ_TSGL, DJ_SJD, MortgageViewModel>> _selectExpression = (a, b, c, d) => new MortgageViewModel() { Tstybm = c.TSTYBM, Zl = d.ZL };

            Expression<Func<DJ_QLR, DJ_QLRGL, DJ_TSGL, DJ_SJD, bool>> _whereExpression =
            (a, b, c, d) => a.QLRMC == qlrmc && (c.LIFECYCLE == 0 || c.LIFECYCLE == null) && c.DJZL == "权属" && b.QLRLX == "权利人";



            try
            {
                var data = await base.Query<DJ_QLR, DJ_QLRGL, DJ_TSGL, DJ_SJD, MortgageViewModel>(_joinExpression, _selectExpression, _whereExpression);
                return new MessageModel<List<MortgageViewModel>>()
                {
                    msg = "获取成功",
                    success = true,
                    response = data
                };
            }
            catch (Exception ex)
            {

                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<MortgageViewModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        /// <summary>
        /// 根据证件号码查询有多少房子
        /// </summary>
        /// <param name="zjhm"></param>
        /// <returns></returns>
        public async Task<MessageModel<List<MortgageViewModel>>> GetTstybmCountByZjhm(string zjhm)
        {
            Expression<Func<DJ_QLR, DJ_QLRGL, DJ_DJB, DJ_TSGL, FC_H_QSDC, object[]>> _joinExpression = (a, b, c, d, e) => new object[]
                 { JoinType.Inner, a.QLRID == b.QLRID, JoinType.Inner, c.SLBH == b.SLBH ,JoinType.Inner,d.SLBH == c.SLBH ,JoinType.Inner,e.TSTYBM == d.TSTYBM};

            Expression<Func<DJ_QLR, DJ_QLRGL, DJ_DJB, DJ_TSGL, FC_H_QSDC, MortgageViewModel>> _selectExpression = (a, b, c, d, e) => new MortgageViewModel() { Qlrmc = a.QLRMC, Zjhm = a.ZJHM, Zl = e.ZL, Tstybm = d.TSTYBM, Bdczh = c.BDCZH };

            Expression<Func<DJ_QLR, DJ_QLRGL, DJ_DJB, DJ_TSGL, FC_H_QSDC, bool>> _whereExpression =
            (a, b, c, d, e) => a.ZJHM == zjhm && (b.LIFECYCLE == 0 || b.LIFECYCLE == null) && (c.LIFECYCLE == 0 || c.LIFECYCLE == null) &&
            (d.LIFECYCLE == 0 || d.LIFECYCLE == null) && d.DJZL == "权属";

            try
            {
                var data = await base.Query<DJ_QLR, DJ_QLRGL, DJ_DJB, DJ_TSGL, FC_H_QSDC, MortgageViewModel>(_joinExpression, _selectExpression, _whereExpression);
                return new MessageModel<List<MortgageViewModel>>()
                {
                    msg = "获取成功",
                    success = true,
                    response = data
                };
            }
            catch (Exception ex)
            {

                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<MortgageViewModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        public async Task<List<HouseAuthorizeVModel>> GetHouseMessages(string Documentnumber)
        {
            
            Expression<Func<BankAuthorize, OrderHouseAssociation, object[]>> _joinExpression = (a, b) => new object[]
                 { JoinType.Inner, a.BID == b.BID};

            Expression<Func<BankAuthorize, OrderHouseAssociation, HouseAuthorizeVModel>> _selectExpression = (a, b) => new HouseAuthorizeVModel() {rightname = a.rightname, DOCUMENTTYPE = a.DOCUMENTTYPE, DOCUMENTNUMBER = a.DOCUMENTNUMBER, AUTHORIZATIONDATE = a.AUTHORIZATIONDATE, AUTHORIZATIONDEADLINE = a.AUTHORIZATIONDEADLINE, STATUS = a.STATUS, CERTIFICATENUMBER = b.CERTIFICATENUMBER, ACCEPTANCENUMBER = b.ACCEPTANCENUMBER, houseStatus = b.houseStatus, NUMBERID = b.NUMBERID, ADDRESS = b.ADDRESS };

            Expression<Func<BankAuthorize, OrderHouseAssociation, bool>> _whereExpression =
            (a, b) => a.DOCUMENTNUMBER == Documentnumber;
                
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            string _strOrderByFileds = "a.AUTHORIZATIONDATE desc";
            int pageIndex = 1;
            int pageSize = 20;

            return await base.Query<BankAuthorize, OrderHouseAssociation, HouseAuthorizeVModel>(_joinExpression, _selectExpression, _whereExpression, pageIndex, pageSize ,_strOrderByFileds);
        }

        /// <summary>
        /// 根据图属统一编码查询房屋情况
        /// </summary>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="tstybm"></param>
        /// <returns></returns>
        public async Task<PageModel<MortgageViewModel>> QueryMortgageList(int intPageIndex, int intPageSize, string tstybm)
        {
            #region 
            Expression<Func<DJ_TSGL, DJ_QLRGL, DJ_DJB, DJ_SJD, QL_FWXG, FC_H_QSDC, DJ_DY, DJ_CF, object[]>> _joinExpression = (a, b, c, d, e, f, g, h) => new object[]
                 { JoinType.Left, b.SLBH == a.SLBH, JoinType.Left, c.SLBH == a.SLBH ,JoinType.Left,d.SLBH == a.SLBH ,JoinType.Left,e.SLBH == a.SLBH ,JoinType.Left,f.TSTYBM == a.TSTYBM ,JoinType.Left , g.SLBH == a.SLBH , JoinType.Left ,h.SLBH == a.SLBH};

            Expression<Func<DJ_TSGL, DJ_QLRGL, DJ_DJB, DJ_SJD, QL_FWXG, FC_H_QSDC, DJ_DY, DJ_CF, MortgageViewModel>> _selectExpression = (a, b, c, d, e, f, g, h) => new MortgageViewModel() { Bdczh = c.BDCZH, Djzl = a.DJZL, Qlrlx = b.QLRLX, Qlrmc = b.QLRMC, Zjlb = b.ZJLB, Zjhm = b.ZJHM, Zl = d.ZL, Qllx = e.QLLX, Qlxz = e.QLXZ, Ghyt = e.GHYT, Jzmj = e.JZMJ, Tdsyqx = f.TDSYQX, Qt = c.QT, DjbDjrq = c.DJRQ, DjbFj = c.FJ, Bdczmh = g.BDCZMH, Dymj = g.DYMJ, Dymj2 = g.DYMJ2, Bdbzzqse = g.BDBZZQSE, Dyfs = g.DYFS, Dyqx = g.DYQX, DyQt = g.QT, DyDjrq = g.DJRQ, Dyfj = g.FJ, Cfwh = h.CFWH, Cfjg = h.CFJG, Dbr = h.DBR, Cfqx = h.CFQX, Cfrq = h.DJSJ, Cfwj = h.FJ, Tstybm = a.TSTYBM };

            Expression<Func<DJ_TSGL, DJ_QLRGL, DJ_DJB, DJ_SJD, QL_FWXG, FC_H_QSDC, DJ_DY, DJ_CF, bool>> _whereExpression =
            (a, b, c, d, e, f, g, h) => tstybm.Split(new char[] { ',' }).Contains(a.TSTYBM) && (a.LIFECYCLE == 0 || a.LIFECYCLE == null);
            #endregion

            var data = await base.QueryResultList<DJ_TSGL, DJ_QLRGL, DJ_DJB, DJ_SJD, QL_FWXG, FC_H_QSDC, DJ_DY, DJ_CF, MortgageViewModel>(_joinExpression, _selectExpression, _whereExpression);

            MortgageViewModel it = new MortgageViewModel();
            List<MortgageViewModel> mList = new List<MortgageViewModel>();



            for (int i = 0; i < data.dataCount; i++)
            {
                if (data.data[i].Djzl == "权属" && data.data[i].Qlrlx == "权利人")
                {
                    it.Tstybm = data.data[i].Tstybm;
                    it.Bdczh = data.data[i].Bdczh;
                    it.Qlrlx = data.data[i].Qlrlx;
                    it.Qlrmc = data.data[i].Qlrmc;
                    it.Zjlb = data.data[i].Zjlb;
                    it.Zjhm = data.data[i].Zjhm;
                    it.Gyfs = data.data[i].Gyfs;
                    it.Zl = data.data[i].Zl;
                    it.Qllx = data.data[i].Qllx;
                    it.Qlxz = data.data[i].Qlxz;
                    it.Ghyt = data.data[i].Ghyt;
                    it.Jzmj = data.data[i].Jzmj;
                    it.Tdsyqx = data.data[i].Tdsyqx;
                    it.Qt = data.data[i].Qt;
                    it.DjbDjrq = data.data[i].DjbDjrq;
                    it.DjbFj = data.data[i].DjbFj;
                }
                if (data.data[i].Djzl == "抵押")
                {
                    if (data.data[i].Qlrlx == "抵押人")
                    {
                        it.Dyzt = "已抵押";
                        it.Bdczmh = data.data[i].Bdczmh;
                        it.Dyr_Name = data.data[i].Qlrmc;
                        it.Dyr_Zjlb = data.data[i].Zjlb;
                        it.Dyr_Zjhm = data.data[i].Zjhm;
                        it.Dymj = data.data[i].Dymj;
                        it.Bdbzzqse = data.data[i].Bdbzzqse;
                        it.Dyfs = data.data[i].Dyfs;
                        it.Dyqx = data.data[i].Dyqx;
                        it.DyQt = data.data[i].DyQt;
                        it.DyDjrq = data.data[i].DyDjrq;
                        it.Dyfj = data.data[i].Dyfj;
                    }
                    if (data.data[i].Qlrlx == "抵押权人")
                    {
                        it.Dyqr_Name = data.data[i].Qlrmc;
                        it.Dyqr_Zjlb = data.data[i].Zjlb;
                        it.Dyqr_Zjhm = data.data[i].Zjhm;
                    }
                }
                if (data.data[i].Djzl == "查封")
                {
                    it.Cfzt = "查封";
                    it.Cfwh = data.data[i].Cfwh;
                    it.Cfjg = data.data[i].Cfjg;
                    it.Dbr = data.data[i].Dbr;
                    it.Cfqx = data.data[i].Cfqx;
                    it.Cfrq = data.data[i].Cfrq;
                    it.Cffj = data.data[i].Cffj;
                }

                mList.Add(it);

            }

            data.data.Clear();
            data.data.Add(it);
            data.dataCount = 1;
            return data;
        }

        public async Task<List<BankAuthorize>> GetBankAuthorizes(string documentnumber, int status)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            Expression<Func<BankAuthorize, IFLOW_ACTION, object[]>> _joinExpression = (a, b) => new object[]
                 { JoinType.Inner, a.STATUS == b.FLOW_ID};

            Expression<Func<BankAuthorize, IFLOW_ACTION, BankAuthorize>> _selectExpression = (a, b) => new BankAuthorize() {BID = a.BID, rightname = a.rightname, DOCUMENTTYPE = a.DOCUMENTTYPE, DOCUMENTNUMBER = a.DOCUMENTNUMBER, AUTHORIZATIONDATE = a.AUTHORIZATIONDATE, AUTHORIZATIONDEADLINE = a.AUTHORIZATIONDEADLINE, STATUS = a.STATUS, FlowName = b.FLOW_NAME};

            Expression<Func<BankAuthorize, IFLOW_ACTION, bool>> _whereExpression =
            (a, b) => a.DOCUMENTNUMBER == documentnumber || a.STATUS == status;

            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            string _strOrderByFileds = "a.AUTHORIZATIONDATE desc";
            int pageIndex = 1;
            int pageSize = 20;

            return await base.Query<BankAuthorize, IFLOW_ACTION, BankAuthorize>(_joinExpression, _selectExpression, _whereExpression, pageIndex, pageSize, _strOrderByFileds);
        }

        /// <summary>
        /// 根据不动产证明号查询相关房屋Tstybm
        /// </summary>
        /// <param name="bdczmh"></param>
        /// <returns></returns>
        public async Task<List<DJ_TSGL>> GetTstybmByBdczmh(string bdczmh)
        {
            Expression<Func<DJ_TSGL, DJ_DY, object[]>> _joinExpression = (a, b) => new object[]
                 { JoinType.Inner, a.SLBH == b.SLBH};

            Expression<Func<DJ_TSGL, DJ_DY, DJ_TSGL>> _selectExpression = (a, b) => new DJ_TSGL() { TSTYBM = a.TSTYBM };

            Expression<Func<DJ_TSGL, DJ_DY, bool>> _whereExpression =
            (a, b) => b.BDCZMH == bdczmh && (a.LIFECYCLE == 0 || a.LIFECYCLE == null);

            base.ChangeDB("GL");

            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            return await base.Query<DJ_TSGL, DJ_DY, DJ_TSGL>(_joinExpression, _selectExpression, _whereExpression);
        }

        public async Task<List<IFLOW_ACTION>> GetFlowActionList(short groupId)
        {
            Expression<Func<IFLOW_ACTION, IFLOW_ACTION_GROUP, object[]>> _joinExpression = (a, b) => new object[]
                 { JoinType.Inner, a.GROUP_ID == b.GROUP_ID};

            Expression<Func<IFLOW_ACTION, IFLOW_ACTION_GROUP, IFLOW_ACTION>> _selectExpression = (a, b) => new IFLOW_ACTION() { FLOW_ID = a.FLOW_ID,FLOW_NAME = a.FLOW_NAME };

            Expression<Func<IFLOW_ACTION, IFLOW_ACTION_GROUP, bool>> _whereExpression =
            (a, b) => b.GROUP_ID == groupId;

            base.ChangeDB(SysConst.DB_CON_IIRS);

            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            return await base.Query<IFLOW_ACTION, IFLOW_ACTION_GROUP, IFLOW_ACTION>(_joinExpression, _selectExpression, _whereExpression);
        }

        #region 内网
        public async Task<List<DJ_TSGL>> GetTstybmListByZjhm(string zjhm,string bdczh, string zjlb)
        {
            string first = "";
            string second = "";
            Expression<Func<DJ_TSGL, DJ_QLRGL, DJ_DJB, SYS_DIC, bool>> _whereExpression;
            if(zjlb == "1" || zjlb == "身份证")
            {
                if(zjhm != "")
                {
                    first = zjhm.Substring(0, 5);
                    second = zjhm.Substring(10, 8);
                }
            }
            

            string fifteenZjhm = first + second;

            base.ChangeDB(SysConst.DB_CON_BDC);

            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            Expression<Func<DJ_TSGL, DJ_QLRGL, DJ_DJB, SYS_DIC, object[]>> _joinExpression = (a, b, c, d) => new object[]
                 { JoinType.Inner, b.SLBH == a.SLBH ,JoinType.Inner, c.SLBH == a.SLBH ,JoinType.Inner ,b.ZJLB == d.DEFINED_CODE};

            Expression<Func<DJ_TSGL, DJ_QLRGL, DJ_DJB, SYS_DIC, DJ_TSGL>> _selectExpression = (a, b, c, d) => new DJ_TSGL() { TSTYBM = a.TSTYBM, dyr = b.QLRMC, zjlb_zwm = d.DNAME };

            if(zjlb != "身份证")
            {
                _whereExpression = (a, b, c, d) => (b.ZJHM == zjhm || b.ZJHM == fifteenZjhm) && c.BDCZH.Contains(bdczh) && (a.LIFECYCLE == 0 || a.LIFECYCLE == null) && b.QLRLX == "权利人" && 
                d.GID == 1 && b.ZJLB == zjlb;
            }
            else
            {
                _whereExpression = (a, b, c, d) => (b.ZJHM == zjhm || b.ZJHM == fifteenZjhm) && c.BDCZH.Contains(bdczh) && (a.LIFECYCLE == 0 || a.LIFECYCLE == null) && b.QLRLX == "权利人" && d.GID == 1;
            }
            return await base.Query<DJ_TSGL, DJ_QLRGL, DJ_DJB, SYS_DIC, DJ_TSGL>(_joinExpression, _selectExpression, _whereExpression);
        }

        public async Task<List<DJ_TSGL>> GetTstybmListByEnterpriseZjhm(string zjhm, string bdczh, int queryval)
        {
            Expression<Func<DJ_TSGL, DJ_QLRGL, DJ_DJB,SYS_DIC, bool>> _whereExpression;

            base.ChangeDB(SysConst.DB_CON_BDC);

            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            Expression<Func<DJ_TSGL, DJ_QLRGL, DJ_DJB, SYS_DIC, object[]>> _joinExpression = (a, b, c,d) => new object[]
                 { JoinType.Inner, b.SLBH == a.SLBH ,JoinType.Inner, c.SLBH == a.SLBH ,JoinType.Inner ,b.ZJLB == d.DEFINED_CODE};

            Expression<Func<DJ_TSGL, DJ_QLRGL, DJ_DJB, SYS_DIC, DJ_TSGL>> _selectExpression = (a, b, c,d) => new DJ_TSGL() { TSTYBM = a.TSTYBM,dyr = b.QLRMC ,zjlb_zwm = d.DNAME};

            if(queryval == 2)
            {
                //模糊查询
                _whereExpression = (a, b, c,d) => b.ZJHM == zjhm && c.BDCZH.Contains(bdczh) && (a.LIFECYCLE == 0 || a.LIFECYCLE == null) && (b.LIFECYCLE == 0 || b.LIFECYCLE == null) && (c.LIFECYCLE == 0 || c.LIFECYCLE == null) && b.QLRLX == "权利人" && d.GID == 1;
            }else
            {
                //精准查询
                _whereExpression = (a, b, c,d) => b.ZJHM == zjhm && c.BDCZH == bdczh && a.DJZL == "权属" && (a.LIFECYCLE == 0 || a.LIFECYCLE == null) && (b.LIFECYCLE == 0 || b.LIFECYCLE == null) && (c.LIFECYCLE == 0 || c.LIFECYCLE == null) && b.QLRLX == "权利人" && d.GID == 1;
            }

            return await base.Query<DJ_TSGL, DJ_QLRGL, DJ_DJB,SYS_DIC, DJ_TSGL>(_joinExpression, _selectExpression, _whereExpression);

            
        }

        public async Task<int> SaveBankauthorize(BankAuthorize bank, List<OrderHouseAssociation> orderHouseList)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            try
            {
                this._dbTransManagement.BeginTran();
                int count = await base.Db.Insertable(bank).InsertColumns(it => new
                {
                    it.BID,
                    it.DOCUMENTTYPE,
                    it.DOCUMENTNUMBER,
                    it.AUTHORIZATIONDATE,
                    it.STATUS,
                    it.rightname,
                    it.BankCode,
                    it.BankName
                }).ExecuteReturnIdentityAsync();
                count = await base.Db.Insertable(orderHouseList).InsertColumns(it=> new
                {
                    it.OID,
                    it.BID,
                    it.CERTIFICATENUMBER,
                    it.ACCEPTANCENUMBER,
                    it.NUMBERID,
                    it.ADDRESS,
                    it.rightname,
                    it.AUTHORIZATIONDATE,
                    it.qllx,
                    it.qlxz,
                    it.jzmj,
                    it.tdqllx,
                    it.tdqlxz,
                    it.tdmj,
                    it.bdclx
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

        public async Task<List<OrderHouseAssociation>> GetDoSubmit(string tstybm)
        {
            Expression<Func<OrderHouseAssociation,BankAuthorize,IFLOW_ACTION, IFLOW_ACTION_GROUP, object[]>> _joinExpression = (a, b,c,d) => new object[]
                 { JoinType.Inner, a.BID == b.BID,JoinType.Inner, c.FLOW_ID == b.STATUS ,JoinType.Inner, c.GROUP_ID == d.GROUP_ID};

            Expression<Func<OrderHouseAssociation, BankAuthorize, IFLOW_ACTION, IFLOW_ACTION_GROUP, OrderHouseAssociation>> _selectExpression = (a, b,c,d) => new OrderHouseAssociation() { CERTIFICATENUMBER = a.CERTIFICATENUMBER, NUMBERID = a.NUMBERID,flow_name = c.FLOW_NAME,gname=d.GNAME };

            Expression<Func<OrderHouseAssociation, BankAuthorize, IFLOW_ACTION, IFLOW_ACTION_GROUP, bool>> _whereExpression =
            (a, b, c, d) => tstybm.Split(new char[] { ',' }).Contains(a.NUMBERID);

            base.ChangeDB(SysConst.DB_CON_IIRS);

            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            return await base.Query<OrderHouseAssociation, BankAuthorize, IFLOW_ACTION, IFLOW_ACTION_GROUP, OrderHouseAssociation>(_joinExpression, _selectExpression, _whereExpression);
        }

        public async Task<string> SaveReturnBid(BankAuthorize bank, List<OrderHouseAssociation> orderHouseList, IFLOW_DO_ACTION iflowDoAction)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            try
            {
                this._dbTransManagement.BeginTran();
                int count = await base.Db.Insertable(bank).InsertColumns(it => new
                {
                    it.BID,
                    it.DOCUMENTTYPE,
                    it.DOCUMENTNUMBER,
                    it.AUTHORIZATIONDATE,
                    it.STATUS,
                    it.rightname,
                    it.BankCode,
                    it.BankName
                }).ExecuteReturnIdentityAsync();
                count = await base.Db.Insertable(orderHouseList).InsertColumns(it => new
                {
                    it.OID,
                    it.BID,
                    it.CERTIFICATENUMBER,
                    it.ACCEPTANCENUMBER,
                    it.NUMBERID,
                    it.ADDRESS,
                    it.rightname,
                    it.AUTHORIZATIONDATE,
                    it.qllx,
                    it.qlxz,
                    it.jzmj,
                    it.tdqllx,
                    it.tdqlxz,
                    it.tdmj,
                    it.bdclx
                }).ExecuteReturnIdentityAsync();
                count = await base.Db.Insertable(iflowDoAction).InsertColumns(it => new
                {
                    it.PK,
                    it.FLOW_ID,
                    it.AUZ_ID,
                    it.CDATE,
                    it.USER_NAME
                }).ExecuteReturnIdentityAsync();
                this._dbTransManagement.CommitTran();
                return bank.BID;
            }
            catch (Exception ex)
            {
                this._dbTransManagement.RollbackTran();
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }


        #endregion
    }
}
