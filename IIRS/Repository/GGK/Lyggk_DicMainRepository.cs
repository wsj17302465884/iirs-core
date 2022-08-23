using IIRS.IRepository.Base;
using IIRS.IRepository.GGK;
using IIRS.Models.EntityModel.GGK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Repository.GGK
{
    public class Lyggk_DicMainRepository : BaseRepository<Lyggk_DicMain>, ILyggk_DicMainRepository
    {
        private readonly ILogger<Lyggk_DicMainRepository> _logger;
        public Lyggk_DicMainRepository(IDBTransManagement dbTransManagement, ILogger<Lyggk_DicMainRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
