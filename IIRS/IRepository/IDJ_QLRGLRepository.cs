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
	public interface IDJ_QLRGLRepository : IBaseRepository<DJ_QLRGL>//类名
    {
        Task<List<DJ_QLRGL>> GetDyrList(string slbh);
    }
}
