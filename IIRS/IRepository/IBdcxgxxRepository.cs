using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IBdcxgxxRepository : IBaseRepository<BdcxgxxModel>
    {
        Task<int> AddBdcxgxxModel(BdcxgxxModel model);
    }
}
