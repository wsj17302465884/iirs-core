using System;
using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;

namespace IIRS.Repository
{
    /// <summary>
    /// ModuleRepository
    /// </summary>	
    public class ModuleRepository : BaseRepository<Sys_Module>, IModuleRepository
    {
        public ModuleRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }
    }
}
