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
    public class DJ_CFRepository : BaseRepository<DJ_CF>, IDJ_CFRepository
    {
        public DJ_CFRepository(IDBTransManagement dbTransManagement, ILogger<DJ_CFRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
