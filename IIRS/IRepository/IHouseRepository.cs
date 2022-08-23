using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IHouseRepository : IBaseRepository<HouseStatusModel>
    {
        Task<List<HouseStatusModel>> GetHouseStatusList(string zd_tstybm);
    }
}
