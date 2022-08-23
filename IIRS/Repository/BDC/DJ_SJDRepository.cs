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
    public class DJ_SJDRepository : BaseRepository<DJ_SJD>, IDJ_SJDRepository
    {
        public DJ_SJDRepository(IDBTransManagement dbTransManagement, ILogger<DJ_SJDRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
