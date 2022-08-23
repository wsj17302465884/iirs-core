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
    public class Lyggk_DicItemRepository : BaseRepository<Lyggk_DicItem>, ILyggk_DicItemRepository
    {
        private readonly ILogger<Lyggk_DicItemRepository> _logger;
        public Lyggk_DicItemRepository(IDBTransManagement dbTransManagement, ILogger<Lyggk_DicItemRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
