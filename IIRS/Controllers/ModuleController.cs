using IIRS.IRepository;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Utilities;
using IIRS.Utilities.Filter;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RT.Comb;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 接口API控制器
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    [Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class ModuleController : ControllerBase
    {
        /// <summary>
        /// 接口API参数
        /// </summary>
        readonly IModuleRepository _moduleRepository;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="moduleRepository"></param>
        public ModuleController(IModuleRepository moduleRepository)
        {
            _moduleRepository = moduleRepository;
        }

        /// <summary>
        /// 获取全部接口api
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="key">模块名称包含字符串</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<Sys_Module>>> Get(int page = 1, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            int intPageSize = 50;

            Expression<Func<Sys_Module, bool>> whereExpression = a => a.IsDeleted != true && (a.Name != null && a.Name.Contains(key));

            var data = await _moduleRepository.QueryPage(whereExpression, page, intPageSize, " Id desc ");

            return new MessageModel<PageModel<Sys_Module>>()
            {
                msg = "获取成功",
                success = data.dataCount >= 0,
                response = data
            };

        }

        /// <summary>
        /// 添加一条接口信息
        /// </summary>
        /// <param name="module">待添加接口信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] Sys_Module module)
        {
            var moduleList = await _moduleRepository.Query(d => d.LinkUrl == module.LinkUrl && d.IsDeleted == false);
            if (moduleList.Count > 0)
            {
                return new MessageModel<string>()
                {
                    msg = $"接口 {module.LinkUrl} 已经存在",
                    success = false
                };
            }
            var data = new MessageModel<string>();

            //创建编号
            module.ID = Provider.Sql.Create();

            var id = (await _moduleRepository.Add(module));
            data.success = id > 0;
            if (data.success)
            {
                data.response = id.ObjToString();
                data.msg = "添加成功";
            }

            return data;
        }

        /// <summary>
        /// 更新接口信息
        /// </summary>
        /// <param name="module">待更新接口信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] Sys_Module module)
        {
            var data = new MessageModel<string>();

            if (module != null && module.ID != Guid.Empty)
            {
                data.success = await _moduleRepository.Update(module);
                if (data.success)
                {
                    data.msg = "更新成功";
                    data.response = module?.ID.ObjToString();
                }
            }

            return data;
        }

        /// <summary>
        /// 删除一条接口
        /// </summary>
        /// <param name="id">接口ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(Guid id)
        {
            var data = new MessageModel<string>();
            if (id != Guid.Empty)
            {
                var moduleDetail = await _moduleRepository.QueryById(id);
                moduleDetail.IsDeleted = true;
                data.success = await _moduleRepository.Update(moduleDetail);
                if (data.success)
                {
                    data.msg = "删除成功";
                    data.response = moduleDetail?.ID.ObjToString();
                }
            }

            return data;
        }
    }
}
