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
    public class BankRelatedIIRSRepository : BaseRepository<BANKRELATEDIIRS>, IBankRelatedIIRSRepository
    {
        private readonly ILogger<BankRelatedIIRSRepository> _logger;
        public BankRelatedIIRSRepository(IDBTransManagement dbTransManagement, ILogger<BankRelatedIIRSRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }
    }
}
