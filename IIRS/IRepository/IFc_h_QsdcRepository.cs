using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IFc_h_QsdcRepository : IBaseRepository<FC_H_QSDC>
    {
        Task<PageModel<FC_H_QSDC>> GetFCHList(string lsfwbh, int intPageIndex);
    }
}
