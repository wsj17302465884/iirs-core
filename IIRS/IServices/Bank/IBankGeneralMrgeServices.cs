using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.IServices.Bank
{
    public interface IBankGeneralMrgeServices
    {
        /// <summary>
        /// 查询房屋信息
        /// </summary>
        /// <param name="XM">姓名</param>
        /// <param name="ZJHM">证件号码（身份证）</param>
        /// <param name="BDCZH">不动产证号</param>
        /// <param name="BDCLX">不动产类型</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        Task<PageStringModel> GetBdcHourseInfo(string XM, string ZJHM, string BDCZH, string BDCLX, int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 查询预告房屋信息
        /// </summary>
        /// <param name="XM">姓名</param>
        /// <param name="ZJHM">证件号码（身份证）</param>
        /// <param name="BDCZH">不动产证号</param>
        /// <param name="BDCLX">不动产证号</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        Task<PageStringModel> GetAdvanceHourseInfo(string XM, string ZJHM, string BDCZH, string BDCLX, int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 查询不动产权利人
        /// </summary>
        /// <param name="djbSLBH">登记簿受理编号</param>
        /// <returns></returns>
        Task<List<QLR_VModel>> GetBankQlrInfo(string[] djbSLBH);
        /// <summary>
        /// 房屋抵押不动产中心审批
        /// </summary>
        /// <param name="AuzInfo">订单表</param>
        /// <param name="regInfo">注册信息</param>
        /// <param name="jsonData">登记信息保存暂存信息表</param>
        /// <param name="spInfo">审批信息表</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="dyInfo">注销信息</param>
        /// <returns>多表操作影响记录数之和</returns>
        int Auditing(BankAuthorize AuzInfo, REGISTRATION_INFO regInfo, SysDataRecorderModel jsonData, SPB_INFO spInfo, IFLOW_DO_ACTION flowInfo, DY_INFO dyInfo);
    }
}
