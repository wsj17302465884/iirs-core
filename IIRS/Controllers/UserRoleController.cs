using System;
using System.Threading.Tasks;
using IIRS.IRepository;
using IIRS.Models.EntityModel;
using IIRS.Models.ViewModel;
using IIRS.Utilities.Filter;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IIRS.Controllers
{
    /// <summary>
    /// 用户角色关系
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    [Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class UserRoleController : Controller
    {
        readonly IUserInfoRepository _userInfoRepository;
        readonly IUserRoleRepository _userRoleRepository;
        readonly IRoleRepository _roleRepository;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfoRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="roleRepository"></param>
        public UserRoleController(IUserInfoRepository userInfoRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _userInfoRepository = userInfoRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 新建用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<Guid>> AddUser(string loginName, string loginPwd, Guid oid)
        {
            var userInfoList = await _userInfoRepository.Query(d => d.LoginName == loginName && d.IsDeleted == false);
            if (userInfoList.Count > 0)
            {
                return new MessageModel<Guid>()
                {
                    msg = $"登录名 {loginName} 已经存在",
                    success = false
                };
            }
            var data = new MessageModel<Guid>();
            var model = await _userInfoRepository.SaveUserInfo(loginName, loginPwd, oid);
            data.success = model.Id != Guid.Empty;
            if (data.success)
            {
                data.response = model.Id;
                data.msg = "添加成功";
            }

            return data;
        }

        /// <summary>
        /// 新建Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<Guid>> AddRole(string roleName, Guid oid)
        {
            var roleList = await _roleRepository.Query(d => d.Name == roleName && d.IsDeleted == false);
            if (roleList.Count > 0)
            {
                return new MessageModel<Guid>()
                {
                    msg = $"角色名 {roleName} 已经存在",
                    success = false
                };
            }
            var data = new MessageModel<Guid>();
            var model = await _roleRepository.SaveRole(roleName, oid);
            data.success = model.ID != Guid.Empty;
            if (data.success)
            {
                data.response = model.ID;
                data.msg = "添加成功";
            }

            return data;
        }

        /// <summary>
        /// 新建用户角色关系
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="rid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<Guid>> AddUserRole(Guid uid, Guid rid)
        {
            var data = new MessageModel<Guid>();
            var model = await _userRoleRepository.SaveUserRole(uid, rid);
            data.success = model.Id != Guid.Empty;
            if (data.success)
            {
                data.response = model.Id;
                data.msg = "添加成功";
            }

            return data;
        }
    }
}