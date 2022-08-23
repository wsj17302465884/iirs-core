using IIRS.IServices.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Models.ViewModel.BANK;
using IIRS.Models.ViewModel.IIRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.IServices.Bank
{
    public interface IBankServices : IBaseServices
    {
        /// <summary>
        /// 查询不动产抵押信息
        /// </summary>
        /// <param name="BDCZMH">不动产证明号</param>
        /// <returns></returns>
        Task<MrgeReleaseVModel> GetMortgageInfo(string BDCZMH);

        Task<string> GetDyzxsq(DYZXSQ model, BRWR_INF jkrModel, BNK_HDL_AGNC_PSN_INF yhjbdlrModel, ATCH fjModel, MRTG_PSN_INF dyrModel, MRTG_REALEST_UNIT_INF bdcdyqkModel);

        Task<YzhdyResultVModel> GetYzhdyResult(YZHDYCX_REQUEST model);
    }
}
