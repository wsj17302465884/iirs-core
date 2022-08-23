using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IIflowActionRepository : IBaseRepository<IFLOW_ACTION>
    {
        Task<List<IFLOW_ACTION>> GetFlowActionListList();

        Task<List<IFLOW_ACTION>> GetFlowName();

        Task<IFLOW_ACTION> GetFlowNameById(string flowId);
    }
}
