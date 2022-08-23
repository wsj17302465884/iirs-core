using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Repository.IIRS
{
    public class DO_ACTIONRepository : BaseRepository<IFLOW_DO_ACTION>, IDO_ACTIONRepository
    {
        private readonly ILogger<DO_ACTIONRepository> _logger;
        public DO_ACTIONRepository(IDBTransManagement dbTransManagement, ILogger<DO_ACTIONRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
