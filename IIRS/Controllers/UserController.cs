using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Utilities;
using IIRS.Utilities.AuthHelper;
using IIRS.Utilities.Filter;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RT.Comb;

namespace IIRS.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    [Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class UserController : ControllerBase
    {
        readonly IUserInfoRepository _userInfoRepository;
        readonly IUserRoleRepository _userRoleRepository;
        readonly IRoleRepository _roleRepository;
        readonly IUserOrganizationRepository _userOrganizationRepository;
        readonly IOrganizationRepository _organizationRepository;
        readonly IDBTransManagement _dBTransManagement;
        private readonly ILogger<UserController> _logger;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="userInfoRepository"></param>
        /// <param name="userRoleRepository"></param>
        /// <param name="roleRepository"></param>
        /// <param name="userOrganizationRepository"></param>
        /// <param name="organizationRepository"></param>
        /// <param name="dBTransManagement"></param>
        /// <param name="logger"></param>
        public UserController(IUserInfoRepository userInfoRepository,
            IUserRoleRepository userRoleRepository,
            IRoleRepository roleRepository,
            IUserOrganizationRepository userOrganizationRepository,
            IOrganizationRepository organizationRepository,
            IDBTransManagement dBTransManagement,
            ILogger<UserController> logger)
        {
            _userInfoRepository = userInfoRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
            _userOrganizationRepository = userOrganizationRepository;
            _organizationRepository = organizationRepository;
            _dBTransManagement = dBTransManagement;
            _logger = logger;
        }

        /// <summary>
        /// 获取全部用户
        /// </summary>
        /// <param name="page">第几页</param>
        /// <param name="key">用户登录名或用户真实名称包含字符串</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<Sys_Userinfo>>> Get(int page = 1, string key = "")
        {
            if (string.IsNullOrEmpty(key) || string.IsNullOrWhiteSpace(key))
            {
                key = "";
            }
            int intPageSize = 50;


            var data = await _userInfoRepository.QueryPage(a => a.IsDeleted != true && a.Status >= 0 && ((a.LoginName != null && a.LoginName.Contains(key)) || (a.RealName != null && a.RealName.Contains(key))), page, intPageSize, " Id desc ");


            #region UserRoles

            // 这里可以封装到多表查询，此处简单处理
            var allUserRoles = await _userRoleRepository.Query(d => d.IsDeleted == false);
            var allRoles = await _roleRepository.Query(d => d.IsDeleted == false);

            var sysUserInfos = data.data;
            foreach (var item in sysUserInfos)
            {
                var currentUserRoles = allUserRoles.Where(d => d.UserId == item.Id).Select(d => d.RoleId).ToList();
                item.RIDs = currentUserRoles;
                item.RoleNames = allRoles.Where(d => currentUserRoles.Contains(d.ID)).Select(d => d.Name).ToList();
            }
            #endregion

            #region UserOrganizations
            var allUserOrganizations = await _userOrganizationRepository.Query(d => d.IsDeleted == false);
            var allOrganizations = await _organizationRepository.Query(d => d.IsDeleted == false);

            foreach (var item in sysUserInfos)
            {
                var currentUserOrganization = allUserOrganizations.Where(d => d.UserId == item.Id).Select(d => d.OrgId).ToList();
                item.OIDs = currentUserOrganization;
                item.OrgNames = allOrganizations.Where(d => currentUserOrganization.Contains(d.Id)).Select(d => d.Name).ToList();
            }
            #endregion

            data.data = sysUserInfos;


            return new MessageModel<PageModel<Sys_Userinfo>>()
            {
                msg = "获取成功",
                success = data.dataCount >= 0,
                response = data
            };

        }

        /// <summary>
        /// 获取用户详情根据token
        /// 【无权限】
        /// </summary>
        /// <param name="token">令牌</param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        public async Task<MessageModel<Sys_Userinfo>> GetInfoByToken(string token)
        {
            var data = new MessageModel<Sys_Userinfo>();
            if (!string.IsNullOrEmpty(token))
            {
                var tokenModel = JwtHelper.SerializeJwt(token);
                if (tokenModel != null && tokenModel.Uid != Guid.Empty)
                {
                    var userinfo = await _userInfoRepository.QueryById(tokenModel.Uid);
                    var userOrgInfo = await _userOrganizationRepository.GetOrganizationIdByUid(tokenModel.Uid);
                    var orgInfo = await _organizationRepository.QueryById(userOrgInfo.Value);
                    userinfo.Organization = orgInfo;
                    if (userinfo != null)
                    {
                        data.response = userinfo;
                        data.success = true;
                        data.msg = "获取成功";
                    }
                }
            }
            return data;
        }

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="userInfo">待添加用户信息</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<MessageModel<string>> Post([FromBody] Sys_Userinfo userInfo)
        {
            if(userInfo == null && userInfo.LoginName.Trim() == "")
            {
                return new MessageModel<string>()
                {
                    msg = $"登录名为空",
                    success = false
                };
            }
            var userInfoList = await _userInfoRepository.Query(d => d.LoginName == userInfo.LoginName && d.IsDeleted == false);
            if (userInfoList.Count > 0)
            {
                return new MessageModel<string>()
                {
                    msg = $"登录名 {userInfo.LoginName} 已经存在",
                    success = false
                };
            }
            var data = new MessageModel<string>();
            try
            {
                _dBTransManagement.BeginTran();
                //创建编号
                userInfo.Id = Provider.Sql.Create();

                userInfo.LoginPWD = EncryptHelper.MD5Encrypt32(userInfo.LoginPWD);
                userInfo.Remark = userInfo.LoginName;

                var id = await _userInfoRepository.Add(userInfo);

                if (userInfo.RIDs.Count > 0)
                {
                    // 添加角色
                    var userRolsAdd = new List<Sys_UserRole>();
                    userInfo.RIDs.ForEach(rid =>
                    {
                        userRolsAdd.Add(new Sys_UserRole(userInfo.Id, rid));
                    });

                    await _userRoleRepository.Add(userRolsAdd);
                }

                if (userInfo.OIDs.Count > 0)
                {
                    // 添加部门
                    var userOrgnazitionsAdd = new List<Sys_UserOrganization>();
                    userInfo.OIDs.ForEach(oid =>
                    {
                        userOrgnazitionsAdd.Add(new Sys_UserOrganization(userInfo.Id, oid));
                    });

                    await _userOrganizationRepository.Add(userOrgnazitionsAdd);
                }

                _dBTransManagement.CommitTran();

                data.success = id > 0;
                if (data.success)
                {
                    data.response = id.ObjToString();
                    data.msg = "添加成功";
                }
            }
            catch(Exception e)
            {
                _dBTransManagement.RollbackTran();
                _logger.LogError(e, e.Message);
                data.msg = e.Message;
                data.success = false;
            }
            return data;
        }

        /// <summary>
        /// 更新用户、部门与角色
        /// </summary>
        /// <param name="userInfo">待更新用户信息</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<MessageModel<string>> Put([FromBody] Sys_Userinfo userInfo)
        {
            // 这里使用事务处理

            var data = new MessageModel<string>();
            try
            {
                _dBTransManagement.BeginTran();

                if (userInfo != null && userInfo.Id != Guid.Empty)
                {
                    // 无论 Update Or Add , 先删除当前用户的全部 U_R 关系
                    var usreroles = (await _userRoleRepository.Query(d => d.UserId == userInfo.Id)).Select(d => d.Id.ToString()).ToArray();
                    if (usreroles.Count() > 0)
                    {
                        var isAllDeleted = await _userRoleRepository.DeleteByIds(usreroles);
                    }

                    if (userInfo.RIDs.Count > 0)
                    {
                        // 然后再执行添加操作
                        var userRolsAdd = new List<Sys_UserRole>();
                        userInfo.RIDs.ForEach(rid =>
                        {
                            userRolsAdd.Add(new Sys_UserRole(userInfo.Id, rid));
                        });

                        await _userRoleRepository.Add(userRolsAdd);

                    }

                    // 无论 Update Or Add , 先删除当前用户的全部 U_O 关系
                    var usreorgnazitions = (await _userOrganizationRepository.Query(d => d.UserId == userInfo.Id)).Select(d => d.Id.ToString()).ToArray();
                    if (usreorgnazitions.Count() > 0)
                    {
                        var isAllDeleted = await _userOrganizationRepository.DeleteByIds(usreorgnazitions);
                    }

                    if (userInfo.OIDs.Count > 0)
                    {
                        // 然后再执行添加操作
                        var userOrgnazitionsAdd = new List<Sys_UserOrganization>();
                        userInfo.OIDs.ForEach(oid =>
                        {
                            userOrgnazitionsAdd.Add(new Sys_UserOrganization(userInfo.Id, oid));
                        });

                        await _userOrganizationRepository.Add(userOrgnazitionsAdd);
                    }

                    data.success = await _userInfoRepository.Update(userInfo);

                    _dBTransManagement.CommitTran();

                    if (data.success)
                    {
                        data.msg = "更新成功";
                        data.response = userInfo?.Id.ObjToString();
                    }
                }
                else
                {
                    data.msg = "用户编号为空";
                    data.success = false;
                }
            }
            catch (Exception e)
            {
                _dBTransManagement.RollbackTran();
                _logger.LogError(e, e.Message);
                data.msg = e.Message;
                data.success = false;
            }

            return data;
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户ID</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<MessageModel<string>> Delete(Guid id)
        {
            var data = new MessageModel<string>();
            try
            {
                _dBTransManagement.BeginTran();
                if (id != Guid.Empty)
                {
                    var userDetail = await _userInfoRepository.QueryById(id);
                    userDetail.IsDeleted = true;
                    data.success = await _userInfoRepository.Update(userDetail);

                    // 删除当前用户的全部 U_R 关系
                    var usreroles = (await _userRoleRepository.Query(d => d.UserId == id)).Select(d => d.Id.ToString()).ToArray();
                    if (usreroles.Count() > 0)
                    {
                        data.success = await _userRoleRepository.DeleteByIds(usreroles);
                    }

                    // 删除当前用户的全部 U_O 关系
                    var usreorgnazitions = (await _userOrganizationRepository.Query(d => d.UserId == id)).Select(d => d.Id.ToString()).ToArray();
                    if (usreorgnazitions.Count() > 0)
                    {
                        data.success = await _userOrganizationRepository.DeleteByIds(usreorgnazitions);
                    }
                }

                _dBTransManagement.CommitTran();
                if (data.success)
                {
                    data.msg = "删除成功";
                }
            }
            catch (Exception e)
            {
                _dBTransManagement.RollbackTran();
                _logger.LogError(e, e.Message);
                data.msg = e.Message;
                data.success = false;
            }
            return data;
        }
    }
}
