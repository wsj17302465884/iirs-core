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
    public class SerialNumberRepository : BaseRepository<SERIAL_NUMBER>, ISerialNumberRepository
    {
        public SerialNumberRepository(IDBTransManagement dbTransManagement, ILogger<SerialNumberRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
