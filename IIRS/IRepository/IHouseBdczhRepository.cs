using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IHouseBdczhRepository : IBaseRepository<HouseXgzhVMode>
    {
        Task<List<HouseXgzhVMode>> GetBdczhByBdczmh(string bdczmh,int xh);
    }
}
