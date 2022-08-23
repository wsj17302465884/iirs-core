using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IZdQsdcRepository : IBaseRepository<ZD_QSDC>
    {
        Task<List<ZD_QSDC>> GetZdInfo(string zdtybm);
    }
}
