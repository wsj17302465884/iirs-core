using IIRS.IRepository;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Utilities;
using IIRS.Utilities.Common;
using IIRS.Utilities.Filter;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RT.Comb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 组织机构管理
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    [Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class OrganizationController : ControllerBase
    {
        readonly IOrganizationRepository _organizationRepository;
        private readonly ILogger<OrganizationController> _logger;

        public OrganizationController(IOrganizationRepository organizationRepository, ILogger<OrganizationController> logger)
        {
            _organizationRepository = organizationRepository;
            _logger = logger;
        }

        /// <summary>
        /// 获取全部组织机构
        /// </summary>
        /// <param name="page">页码</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<Sys_Organization>>> Get(int page = 1, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            int intPageSize = 50;

            Expression<Func<Sys_Organization, bool>> whereExpression = a => a.IsDeleted != true && (a.Name != null && a.Name.Contains(key));

            var data = await _organizationRepository.QueryPage(whereExpression, page, intPageSize, " Id desc ");

            return new MessageModel<PageModel<Sys_Organization>>()
            {
                msg = "获取成功",
                success = data.dataCount >= 0,
                response = data
            };

        }

        /// <summary>
        /// 添加组织机构
        /// </summary>
        /// <param name="organization">组织机构信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] Sys_Organization organization)
        {
            var organizationList = await _organizationRepository.Query(d => d.Name == organization.Name && d.PId == organization.PId && d.IsDeleted == false);
            if (organizationList.Count > 0)
            {
                return new MessageModel<string>()
                {
                    msg = $"当前级别下，组织机构 {organization.Name} 已经存在",
                    success = false
                };
            }
            var data = new MessageModel<string>();

            //创建编号
            organization.Id = Provider.Sql.Create();

            var id = (await _organizationRepository.Add(organization));
            data.success = id > 0;
            if (data.success)
            {
                data.response = id.ObjToString();
                data.msg = "添加成功";
            }

            return data;
        }

        /// <summary>
        /// 更新组织机构
        /// </summary>
        /// <param name="organization">组织机构信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] Sys_Organization organization)
        {
            var data = new MessageModel<string>();

            if (organization != null && organization.Id != Guid.Empty)
            {
                data.success = await _organizationRepository.Update(organization);
                if (data.success)
                {
                    data.msg = "更新成功";
                    data.response = organization?.Id.ObjToString();
                }
            }

            return data;
        }

        /// <summary>
        /// 获取组织机构树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<OrganizationTree>> GetOrganizationTree()
        {
            this._logger.LogDebug("测试：AAAA");
            try
            {
                var data = new MessageModel<OrganizationTree>();

                var organizations = await _organizationRepository.Query(d => d.IsDeleted == false);
                var organizationTrees = (from child in organizations
                                         where child.IsDeleted == false
                                         orderby child.Id
                                         select new OrganizationTree
                                         {
                                             Id = child.Id,
                                             PId = child.PId,
                                             Name = child.Name,
                                             Remark = child.Description,
                                             Disabled = false
                                         }).ToList();
                OrganizationTree rootRoot = new OrganizationTree
                {
                    Id = Guid.Empty,
                    PId = Guid.Empty,
                    Name = "根节点",
                    Disabled = true
                };

                Recursion.LoopToAppendChildrenT<OrganizationTree>(organizationTrees, rootRoot, "PId", "Id");

                data.success = true;
                if (data.success)
                {
                    data.response = rootRoot;
                    data.msg = "获取成功";
                }

                return data;
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"OrganizationController.GetOrganizationTree:【错误代码：{logErrorCode},原因:{ex.Message}】";

                this._logger.LogDebug(errorLog);
                return new MessageModel<OrganizationTree>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null
                };
            }
            
        }

        /// <summary>
        /// 删除组织机构
        /// </summary>
        /// <param name="id">组织机构编号</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(Guid id)
        {
            var data = new MessageModel<string>();
            if (id != Guid.Empty)
            {
                var organizationDetail = await _organizationRepository.QueryById(id);
                organizationDetail.IsDeleted = true;
                data.success = await _organizationRepository.Update(organizationDetail);
                if (data.success)
                {
                    data.msg = "删除成功";
                    data.response = organizationDetail?.Id.ObjToString();
                }
            }

            return data;
        }

        /// <summary>
        /// 查询树形 Table
        /// </summary>
        /// <param name="f">父节点</param>
        /// <param name="key">关键字</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<List<Sys_Organization>>> GetTreeTable(Guid f = new Guid(), string key = "")
        {
            try
            {
                List<Sys_Organization> organizations = new List<Sys_Organization>();
                var organizationsList = await _organizationRepository.Query(d => d.IsDeleted == false);
                if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
                {
                    key = "";
                }

                if (key != "")
                {
                    organizations = organizationsList.Where(a => a.Name.Contains(key)).ToList();
                }
                else
                {
                    organizations = organizationsList.Where(a => a.PId == f).ToList();
                }

                foreach (var item in organizations)
                {
                    item.HasChildren = organizationsList.Where(d => d.PId == item.Id).Any();
                }

                return new MessageModel<List<Sys_Organization>>()
                {
                    msg = "获取成功",
                    success = organizations.Count >= 0,
                    response = organizations
                };
            }
            catch (Exception ex)
            {
                string logErrorCode = Provider.Sql.Create().ToString("N");
                string errorLog = $"OrganizationController.GetTreeTable:【错误代码：{logErrorCode},原因:{ex.Message}】";

                this._logger.LogDebug(errorLog);
                return new MessageModel<List<Sys_Organization>>()
                {
                    msg = $"系统错误，错误代码：{logErrorCode},请与管理员联系！",
                    success = false,
                    response = null
                };
            }
            
        }
    }
}
