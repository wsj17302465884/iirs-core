using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.IServices.Bank
{
    public interface IBankQueryServices
    {
        Task<PageModel<AgencyTaskVModel>> GetBankTaskList(string slbh,string jbr,string lczl, int intPageIndex, int PageSize);
        Task<List<IFLOW_ACTION_GROUP>> GetActionGroupList();

        /// <summary>
        /// 办件查询
        /// </summary>
        /// <param name="lczl">流程种类</param>
        /// <param name="Start">起始日期</param>
        /// <param name="termination">终止日期</param>
        /// <param name="intPageIndex">当前页标</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns></returns>
        Task<PageModel<SJD_INFO>> GetBusinessinlist(string lczl, DateTime Start, DateTime termination, int intPageIndex, int PageSize);

    }
}
