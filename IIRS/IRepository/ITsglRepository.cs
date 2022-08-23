using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface ITsglRepository : IBaseRepository<DJ_TSGL>
    {
        Task<List<DJ_TSGL>> GetTsglList(string tstybm);

        Task<List<DJ_TSGL>> GetTstybmBySlbh(string slbh);
    }
}
