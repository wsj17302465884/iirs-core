using IIRS.IRepository.Base;
using IIRS.IServices.Bank;
using IIRS.IServices.BDC;
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
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services.Bank
{
    public class BankChangeMrgeServices : BaseServices, IBankChangeMrgeServices
    {
        private readonly ILogger<BankChangeMrgeServices> _logger;

        IDBTransManagement _dbTransManagement;
        public BankChangeMrgeServices(IDBTransManagement dbTransManagement, ILogger<BankChangeMrgeServices> logger) : base(dbTransManagement)
        {
            this._logger = logger;
            this._dbTransManagement = dbTransManagement;
        }

        /// <summary>
        /// 房屋转移抵押不动产中心审批
        /// </summary>
        /// <param name="AuzInfo">订单表</param>
        /// <param name="regInfo">注册信息</param>
        /// <param name="jsonData">登记信息保存暂存信息表</param>
        /// <param name="spInfo">审批信息表</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="dyInfo">抵押信息</param>
        /// <param name="djInfo">登记信息</param>
        /// <returns>多表操作影响记录数之和</returns>
        public int Auditing(BankAuthorize AuzInfo, REGISTRATION_INFO regInfo, SysDataRecorderModel jsonData, SPB_INFO spInfo, IFLOW_DO_ACTION flowInfo, DY_INFO dyInfo, DJB_INFO djInfo)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);

            try
            {
                int count = 0;
                this._dbTransManagement.BeginTran();
                count += base.Db.Updateable(regInfo).UpdateColumns(R => new
                {
                    R.IS_ACTION_OK
                }).Where(S => S.XID == regInfo.XID).ExecuteCommand();
                count += base.Db.Updateable(jsonData).UpdateColumns(D => new
                {
                    D.SAVEDATAJSON
                }).Where(S => S.BUS_PK == spInfo.XID).ExecuteCommand();
                base.Db.Deleteable<SPB_INFO>().Where(S => S.XID == spInfo.XID).ExecuteCommand();
                count += base.Db.Updateable(dyInfo).UpdateColumns(zl => new
                {
                    zl.SPBZ
                }).Where(S => S.XID == dyInfo.XID).ExecuteCommand();
                count += base.Db.Updateable(djInfo).UpdateColumns(zl => new
                {
                    zl.SPBZ,
                    zl.SPRQ
                }).Where(S => S.xid == djInfo.xid).ExecuteCommand();
                count += base.Db.Updateable(AuzInfo).UpdateColumns(auz => new
                {
                    auz.STATUS,
                    auz.PRE_STATUS
                }).Where(S => S.BID == AuzInfo.BID).ExecuteCommand();
                count += base.Db.Insertable(spInfo).ExecuteCommand();
                count = base.Db.Insertable(flowInfo).ExecuteCommand();

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
