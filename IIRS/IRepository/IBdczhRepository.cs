using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    /// <summary>
    /// 根据抵押不动产证明号查询接口
    /// </summary>
    public interface IBdczhRepository : IBaseRepository<BdczhVModel>
    {
        Task<List<BdczhVModel>> GetBdczhByBdczmh(string bdczmh,int xh);
    }
}
