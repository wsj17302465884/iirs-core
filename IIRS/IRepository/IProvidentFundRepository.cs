using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IProvidentFundRepository : IBaseRepository<ProvidentFundModel>
    {
        Task<List<ProvidentFundModel>> GetProvidentFundList(string slbh);
    }
}
