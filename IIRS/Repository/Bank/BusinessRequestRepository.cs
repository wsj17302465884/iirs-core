using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Repository.Bank
{
    public class BusinessRequestRepository : BaseRepository<BUSINESS_REQUEST>, IBusinessRequestRepository
    {
        public BusinessRequestRepository(IDBTransManagement dbTransManagement, ILogger<BusinessRequestRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
