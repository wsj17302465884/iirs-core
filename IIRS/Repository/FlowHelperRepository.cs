using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel;
using IIRS.Repository.Base;
using IIRS.Utilities.FlowHelper;
using Newtonsoft.Json.Linq;
using SqlSugar;

namespace IIRS.Repository
{
    /// <summary>
    /// RoleRepository
    /// </summary>	
    public class FlowHelperRepository : BaseRepository<FlowInstance>, IFlowHelperRepository
    {
        public FlowHelperRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }

        public async Task<FlowInstance> CreateFlowInstance(FlowInfoEnum flowType, string flowJsonData)
        {
            var result = base.Db.Queryable<FlowPublish, FlowAction>((it, fa) => it.PUBLISH_ID == fa.PUBLISH_ID)
                .Where((it, fa) => it.IS_PUBLISH == 1 && it.FLOW_ID == (int)flowType && fa.ACTION_MARK == 0)
                .Select((it, fa) => new { PublishID = it.PUBLISH_ID, ActionID = fa.ACTION_ID }).ToList();
            if (result.Count > 0)
            {
                FlowInstance model = new FlowInstance()
                {
                    CURRENT_ACTION = result[0].ActionID,
                    PUBLISH_ID = result[0].PublishID,
                    END_MARK = 0,
                    BUS_JSON = flowJsonData
                };
                await base.Add(model);
                return model;
            }
            else
            {
                throw new Exception("流程创建失败，原因:未能获取到该流程的发布版本或该流程没有设置启动流程节点");
            }
        }
    }
}
