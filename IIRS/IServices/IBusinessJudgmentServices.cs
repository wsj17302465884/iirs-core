using IIRS.IServices.Base;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using IIRS.Models.ViewModel.BDC.judgment;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface IBusinessJudgmentServices : IBaseServices
    {
        /// <summary>
        /// 判断是否包含查封抵押异议
        /// </summary>
        /// <param name="yw_slbh">当前业务SLBH</param>
        /// <param name="dj_slbh">登记SLBH</param>
        /// <returns></returns>
        Task<List<JudgmentMortgage1>> GetBusinessJudgment1(string yw_slbh, string dj_slbh);
        /// <summary>
        /// 判断是否有其他业务正在进行
        /// </summary>
        /// <param name="yw_slbh">当前业务受理编号</param>
        /// <param name="qz_slbh">权证受理编号</param>
        /// <returns></returns>
        Task<List<JudgmentMortgage1>> GetBusinessJudgment2(string yw_slbh, string qz_slbh);

        /// <summary>
        /// 当前业务权证对应证书
        /// </summary>
        /// <param name="ywlx">业务类型：抵押或抵押变更</param>
        /// <param name="bdclx">不动产类型：房屋或者宗地</param>
        /// <param name="tstybm">图属统一编码</param>
        /// <param name="qz_slbh">权证受理编号</param>
        /// <param name="yw_slbh">当前办理业务受理编号</param>
        /// <param name="dy_slbh">当前抵押受理编号</param>
        /// <returns></returns>
        Task<string> GetBusinessJudgment3(string ywlx, string bdclx, string tstybm, string qz_slbh,string yw_slbh,string dy_slbh);

        Task<List<JudgmentMortgage1>> GetBusinessJudgment4(string yw_slbh, string qz_slbh, string tstybm);
    }
}
