using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;

namespace IIRS.IRepository
{
    /// <summary>
	/// IModuleRepository
	/// </summary>	
	public interface IDYRepository : IBaseRepository<DJ_DY>//类名
    {
        Task<List<DJ_DY>> GetDyInfo(string slbh);
    }
}
