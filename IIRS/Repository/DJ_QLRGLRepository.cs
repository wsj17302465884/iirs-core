using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    /// <summary>
    /// ModuleRepository
    /// </summary>	
    public class DJ_QLRGLRepository : BaseRepository<DJ_QLRGL>, IDJ_QLRGLRepository
    {
        private readonly ILogger<DJ_QLRGLRepository> _logger;
        public DJ_QLRGLRepository(IDBTransManagement dbTransManagement, ILogger<DJ_QLRGLRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        /// <summary>
        /// 获取权利人类型为抵押人的信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        public async Task<List<DJ_QLRGL>> GetDyrList(string slbh)
        {            
            return await base.Query(a => a.SLBH == slbh && a.QLRLX == "抵押人");
        }
    }
}
