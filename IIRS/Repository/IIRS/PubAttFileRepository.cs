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
    public class PubAttFileRepository : BaseRepository<PUB_ATT_FILE>, IPubAttFileRepository
    {
        private readonly ILogger<PubAttFileRepository> _logger;
        public PubAttFileRepository(IDBTransManagement dbTransManagement, ILogger<PubAttFileRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
