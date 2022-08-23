using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class IflowActionRepository : BaseRepository<IFLOW_ACTION>, IIflowActionRepository
    {
        private readonly ILogger<IflowActionRepository> _logger;
        public IflowActionRepository(IDBTransManagement dbTransManagement, ILogger<IflowActionRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<List<IFLOW_ACTION>> GetFlowActionListList()
        {
            return await base.Query(a => a.GROUP_ID == 1);
        }

        public async Task<List<IFLOW_ACTION>> GetFlowName()
        {
            return await base.Query(a => a.FLOW_ID == 15 || a.FLOW_ID == 16 || a.FLOW_ID == 17 || a.FLOW_ID == 18);
        }

        public async Task<IFLOW_ACTION> GetFlowNameById(string flowId)
        {
            return await base.QueryById(flowId);
        }
    }
}
