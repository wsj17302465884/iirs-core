using System;
using System.Threading.Tasks;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel;
using IIRS.Utilities.FlowHelper;

namespace IIRS.IRepository
{
    public interface IFlowHelperRepository : IBaseRepository<FlowInstance>
    {
        Task<FlowInstance> CreateFlowInstance(FlowInfoEnum flowType, string flowJsonData);
    }
}
