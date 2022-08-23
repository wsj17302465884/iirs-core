using System;
using System.Linq;
using System.Threading.Tasks;
using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using IIRS.Utilities;

namespace IIRS.Repository
{
    public class UserInfoRepository : BaseRepository<Sys_Userinfo>, IUserInfoRepository
    {
        IUserRoleRepository _userRoleRepository;
        IRoleRepository _roleRepository;

        public UserInfoRepository(IDBTransManagement dbBase, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository) : base(dbBase)
        {
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <param name="oid"></param>
        /// <returns></returns>
        public async Task<Sys_Userinfo> SaveUserInfo(string loginName, string loginPwd, Guid oid)
        {
            loginPwd = EncryptHelper.MD5Encrypt32(loginPwd);

            Sys_Userinfo userInfo = new Sys_Userinfo(loginName, loginPwd);
            var userList = await base.Query(a => a.LoginName == userInfo.LoginName && a.LoginPWD == userInfo.LoginPWD);
            if (userList.Count > 0)
            {
                return userList.FirstOrDefault();
            }
            else
            {
                await base.Add(userInfo);
                return userInfo;
            }
        }

        /// <summary>
        /// 获取用户角色，以英文逗号分隔各个角色名
        /// </summary>
        /// <param name="loginName"></param>
        /// <param name="loginPwd"></param>
        /// <returns></returns>
        public async Task<string> GetUserRoleNameStr(string loginName, string loginPwd)
        {
            string roleName = "";
            var user = (await base.Query(a => a.LoginName == loginName && a.LoginPWD == loginPwd)).FirstOrDefault();
            var roleList = await _roleRepository.Query(a => a.IsDeleted == false);
            if (user != null)
            {
                var userRoles = await _userRoleRepository.Query(ur => ur.UserId == user.Id);
                if (userRoles.Count > 0)
                {
                    var arr = userRoles.Select(ur => ur.RoleId.ObjToString()).ToList();
                    var roles = roleList.Where(d => arr.Contains(d.ID.ObjToString()));

                    roleName = string.Join(',', roles.Select(r => r.Name).ToArray());
                }
            }
            return roleName;
        }
    }
}
