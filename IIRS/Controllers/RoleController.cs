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
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    [Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class RoleController : ControllerBase
    {
        readonly IRoleRepository _roleRepository;


        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 获取全部角色
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="key">角色名称包含字符串</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<Sys_Role>>> Get(int page = 1, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }

            int intPageSize = 50;

            var data = await _roleRepository.QueryPage(a => a.IsDeleted != true && (a.Name != null && a.Name.Contains(key)), page, intPageSize, " Id desc ");

            return new MessageModel<PageModel<Sys_Role>>()
            {
                msg = "获取成功",
                success = data.dataCount >= 0,
                response = data
            };

        }

        /// <summary>
        /// 获取指定组织机构下的全部角色
        /// </summary>
        /// <param name="oid">组织机构编号</param>
        /// <param name="page">第几页</param>
        /// <param name="key">角色名称包含字符串</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<Sys_Role>>> GetRolesByOrg(Guid oid, int page = 1, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }

            int intPageSize = 50;

            var data = await _roleRepository.QueryPage(a => a.IsDeleted != true && a.OrgId == oid && (a.Name != null && a.Name.Contains(key)), page, intPageSize, " Id desc ");

            return new MessageModel<PageModel<Sys_Role>>()
            {
                msg = "获取成功",
                success = data.dataCount >= 0,
                response = data
            };
        }

        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="role">待添加角色信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] Sys_Role role)
        {
            var roleList = await _roleRepository.Query(d => d.Name == role.Name && d.OrgId == role.OrgId && d.IsDeleted == false);
            if(roleList.Count> 0)
            {
                return new MessageModel<string>()
                {
                    msg = $"在编号为 {role.OrgId} 的组织机构下，角色名 {role.Name} 已经存在",
                    success = false
                };
            }
            var data = new MessageModel<string>();

            //创建编号
            role.ID = Provider.Sql.Create();

            var id = (await _roleRepository.Add(role));
            data.success = id > 0;
            if (data.success)
            {
                data.response = id.ObjToString();
                data.msg = "添加成功";
            }

            return data;
        }

        /// <summary>
        /// 更新角色
        /// </summary>
        /// <param name="role">待更新接口信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] Sys_Role role)
        {
            var data = new MessageModel<string>();
            if (role != null && role.ID != Guid.Empty)
            {
                data.success = await _roleRepository.Update(role);
                if (data.success)
                {
                    data.msg = "更新成功";
                    data.response = role?.ID.ObjToString();
                }
            }

            return data;
        }

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="id">角色ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(Guid id)
        {
            var data = new MessageModel<string>();
            if (id != Guid.Empty)
            {
                var userDetail = await _roleRepository.QueryById(id);
                userDetail.IsDeleted = true;
                data.success = await _roleRepository.Update(userDetail);
                if (data.success)
                {
                    data.msg = "删除成功";
                    data.response = userDetail?.ID.ObjToString();
                }
            }

            return data;
        }
    }
}
