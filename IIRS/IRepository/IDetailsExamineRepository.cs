using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IDetailsExamineRepository : IBaseRepository<DetailsExamineVModel>
    {
        Task<List<DJZLViewTree>> GetDetailsExamineTreeList(string tstybm);
    }
}
