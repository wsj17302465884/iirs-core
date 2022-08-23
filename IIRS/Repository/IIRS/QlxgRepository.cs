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
    public class QlxgRepository : BaseRepository<QL_XG_INFO>, IQlxgRepository
    {
        private readonly ILogger<QlxgRepository> _logger;
        public QlxgRepository(IDBTransManagement dbTransManagement, ILogger<QlxgRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
