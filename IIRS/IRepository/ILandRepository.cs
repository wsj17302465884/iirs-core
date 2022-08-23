using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface ILandRepository:IBaseRepository<LandStatusModel>
    {
        Task<List<LandStatusModel>> GetLandStatusList(string h_tstybm);
    }
}
