using IIRS.IRepository.Base;
using IIRS.IRepository.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Repository.BDC
{
    public class DJ_SPBRepository : BaseRepository<DJ_SPB>, IDJ_SPBRepository
    {
        public DJ_SPBRepository(IDBTransManagement dbTransManagement, ILogger<DJ_SPBRepository> logger) : base(dbTransManagement)
        {

        }
    }
}