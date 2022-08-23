using IIRS.IServices.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Models.ViewModel.LAW;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface ICourtBdcServices : IBaseServices
    {
        Task<DJ_TSGL> GetTsglListByZjhm(string qlrmc, string zjhm);

        Task<Message> GetLawList(string qlrmc, string zjhm);
    }
}
