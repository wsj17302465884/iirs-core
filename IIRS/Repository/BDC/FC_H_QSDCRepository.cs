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
    public class FC_H_QSDCRepository : BaseRepository<FC_H_QSDC>, IFC_H_QSDCRepository
    {
        public FC_H_QSDCRepository(IDBTransManagement dbTransManagement, ILogger<FC_H_QSDCRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
