using IIRS.IServices.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using IIRS.Models.ViewModel.BDC.TraceBack;
using IIRS.Models.ViewModel.IIRS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface IPublicBusinessServices : IBaseServices
    {
        Task<List<BusinessModel>> GeneralMortgageBusiness(string tstybm, string slbh);
        Task<List<IFLOW_ACTION>> GetFlowActionModels(int groupId);
        Task<PageModel<BankAuthorize>> GetUpcomingTasksListToPage(int intPageIndex, string zjhm, string jbr, int flowId);
        Task<List<V_HouseModel>> GetHouseInfoByBdczh(string bdczh);

        #region 追溯
        Task<TraceBackVModel> GetTraceBackInfo(string slbh, string djzl);
        #endregion
    }
}
