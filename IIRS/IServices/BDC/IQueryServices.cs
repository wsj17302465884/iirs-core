using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.IServices.BDC
{
    public interface IQueryServices
    {
        Task<PageModel<AgencyTaskVModel>> GetAgencyTaskList(string slbh,string jbr,string lczl, int IsAction,int intPageIndex, int PageSize);
        Task<List<IFLOW_ACTION_GROUP>> GetActionGroupList();
    }
}
