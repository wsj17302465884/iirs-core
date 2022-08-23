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
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    [Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class UserOrganizationController : Controller
    {
        readonly IUserInfoRepository _userInfoRepository;
        readonly IUserOrganizationRepository _userOrganizationRepository;
        readonly IOrganizationRepository _organizationRepository;

        public UserOrganizationController(IUserInfoRepository userInfoRepository, IUserOrganizationRepository userOrganizationRepository, IOrganizationRepository organizationRepository)
        {
            _userInfoRepository = userInfoRepository;
            _userOrganizationRepository = userOrganizationRepository;
            _organizationRepository = organizationRepository;
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
        /// 新建组织机构
        /// </summary>
        /// <param name="organizationName"></param>
        /// <param name="pid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<Guid>> AddOrganization(string organizationName, Guid pid)
        {
            var organizationNameList = await _organizationRepository.Query(d => d.Name == organizationName && d.PId == pid && d.IsDeleted == false);
            if (organizationNameList.Count > 0)
            {
                return new MessageModel<Guid>()
                {
                    msg = $"在编号为 {pid} 的组织机构下，名为 {organizationName} 已经存在",
                    success = false
                };
            }
            var data = new MessageModel<Guid>();
            var model = await _organizationRepository.SaveOrganization(organizationName, pid);
            data.success = model.Id != Guid.Empty;
            if (data.success)
            {
                data.response = model.Id;
                data.msg = "添加成功";
            }

            return data;
        }

        /// <summary>
        /// 新建用户组织机构关系
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<Guid>> AddUserOrganization(Guid uid, Guid oid)
        {
            var data = new MessageModel<Guid>();
            var model = await _userOrganizationRepository.SaveUserOrganization(uid, oid);
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
