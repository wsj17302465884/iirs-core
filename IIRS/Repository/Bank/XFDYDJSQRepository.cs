using IIRS.IRepository.Bank;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BANK;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;

namespace IIRS.Repository.Bank
{
    public class XFDYDJSQRepository : BaseRepository<XFDYDJSQ>, IXFDYDJSQRepository
    {
        public XFDYDJSQRepository(IDBTransManagement dbTransManagement, ILogger<XFDYDJSQRepository> logger) : base(dbTransManagement)
        {

        }
    }
}
