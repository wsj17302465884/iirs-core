using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.IServices.Bank
{
    public interface IBankMrgeReleaseServices
    {
        /// <summary>
        /// 查询要抵押不动产信息
        /// </summary>
        /// <param name="BDCZMH">不动产证明号</param>
        /// <param name="DYRMC">抵押人名称</param>
        /// <param name="Bank_ID">抵押权人(银行)编码</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        PageModel<MrgeCertInfoVModel> GetMrgeCertInfo(string BDCZMH, string DYRMC, string Bank_ID, int pageIndex = 1, int pageSize = 10);

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
        int Auditing(BankAuthorize AuzInfo, REGISTRATION_INFO regInfo, SysDataRecorderModel jsonData, SPB_INFO spInfo, IFLOW_DO_ACTION flowInfo, XGDJZX_INFO zxInfo);
    }
}
