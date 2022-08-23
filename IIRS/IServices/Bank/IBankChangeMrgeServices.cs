using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.IServices.Bank
{
    public interface IBankChangeMrgeServices
    {
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
        public int Auditing(BankAuthorize AuzInfo, REGISTRATION_INFO regInfo, SysDataRecorderModel jsonData, SPB_INFO spInfo, IFLOW_DO_ACTION flowInfo, DY_INFO dyInfo, DJB_INFO djInfo);
    }
}
