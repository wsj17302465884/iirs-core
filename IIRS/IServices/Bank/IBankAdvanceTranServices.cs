using IIRS.Models.EntityModel.BDC;
using IIRS.Models.ViewModel.BDC;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices.Bank
{
    /// <summary>
    /// 银行 -- 预告抵押接口服务
    /// </summary>
    public interface IBankAdvanceTranServices
    {
        /// <summary>
        /// 查询预告下是否有房产
        /// </summary>
        /// <param name="slbh">预告受理编码</param>
        /// <returns></returns>
        Task<List<DJ_XGDJGL>> GetYgBdczhInfo(string slbh);
        Task<List<AdvanceVModel>> GetBdcHourseInfo(string XM, string ZJHM, string BDCZH, string BDCLX);
    }
}
