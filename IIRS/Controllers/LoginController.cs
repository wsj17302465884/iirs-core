using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IIRS.IRepository;
using IIRS.Models.ViewModel;
using IIRS.Utilities;
using IIRS.Utilities.AuthHelper;
using IIRS.Utilities.Filter;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace IIRS.Controllers
{
    /// <summary>
    /// 登录控制器
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    [TypeFilter(typeof(ClientIdCheckFilter))]
    public class LoginController : ControllerBase
    {
        /// <summary>
        /// 必要权限参数
        /// </summary>
        readonly PermissionRequirement _requirement;

        readonly IUserInfoRepository _userInfoRepository;

        /// <summary>
        /// 日志记录器
        /// </summary>
        private readonly ILogger<LoginController> _logger;

        private readonly IRoleModulePermissionRepository _roleModulePermissionRepository;

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="requirement"></param>
        /// <param name="userInfoRepository"></param>
        /// <param name="roleModulePermissionRepository"></param>
        /// <param name="logger"></param>
        public LoginController(PermissionRequirement requirement, IUserInfoRepository userInfoRepository, IRoleModulePermissionRepository roleModulePermissionRepository, ILogger<LoginController> logger)
        {
            _requirement = requirement;
            _userInfoRepository = userInfoRepository;
            _roleModulePermissionRepository = roleModulePermissionRepository;
            _logger = logger;
        }

        /// <summary>
        /// 获取JWT的方法
        /// </summary>
        /// <param name="name">登录名</param>
        /// <param name="pass">密码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<TokenModel>> GetJwtToken(string name = "", string pass = "")
        {
            string jwtStr = string.Empty;
            var data = new MessageModel<TokenModel>();

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(pass))
            {
                data.success = false;
                data.msg = "用户名或密码不能为空";
                return data;
            }

            pass = EncryptHelper.MD5Encrypt32(EncryptHelper.RsaDecrypt(pass));

            var user = await _userInfoRepository.Query(a => a.LoginName == name && a.LoginPWD == pass && a.IsDeleted == false);

            if (user.Count > 0)
            {
                var userRoles = await _userInfoRepository.GetUserRoleNameStr(name, pass);
                //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, name),
                    new Claim(JwtRegisteredClaimNames.Jti, user.FirstOrDefault().Id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
                claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

                // 将最新的角色和接口列表更新
                // 但是这样有个问题,就是如果修改了某一个角色的菜单权限,不会立刻更新,
                // 需要让用户退出重新登录

                var _roleModuleMaps = await _roleModulePermissionRepository.RoleModuleMaps();
                var list = (from item in _roleModuleMaps
                            where item.IsDeleted == false
                            orderby item.ID
                            select new PermissionItem
                            {
                                Url = item.Module?.LinkUrl,
                                Role = item.Role?.Name,
                            }).ToList();

                _requirement.Permissions = list;

                //用户标识
                var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                identity.AddClaims(claims);

                //_logger.LogDebug($"用户 {name} 成功登陆");

                var token = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                //return new JsonResult(token);
                data.success = true;
                data.msg = "认证成功";
                data.response = token;
                return data;
            }
            else
            {
                data.success = false;
                data.msg = "认证失败";
                return data;
            }
        }

        /// <summary>
        /// 请求刷新Token（以旧换新）
        /// </summary>
        /// <param name="token">旧Token</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<TokenModel>> RefreshToken(string token = "")
        {
            string jwtStr = string.Empty;
            var data = new MessageModel<TokenModel>();

            if (string.IsNullOrEmpty(token))
            {
                data.success = false;
                data.msg = "Token无效，请重新登录";
                return data;
            }
            var tokenModel = JwtHelper.SerializeJwt(token);
            if (tokenModel != null && tokenModel.Uid != Guid.Empty)
            {
                var user = await _userInfoRepository.QueryById(tokenModel.Uid);
                if (user != null)
                {
                    var userRoles = await _userInfoRepository.GetUserRoleNameStr(user.LoginName, user.LoginPWD);
                    //如果是基于用户的授权策略，这里要添加用户;如果是基于角色的授权策略，这里要添加角色
                    var claims = new List<Claim> {
                    new Claim(ClaimTypes.Name, user.LoginName),
                    new Claim(JwtRegisteredClaimNames.Jti, tokenModel.Uid.ObjToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.Now.AddSeconds(_requirement.Expiration.TotalSeconds).ToString()) };
                    claims.AddRange(userRoles.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));

                    //用户标识
                    var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
                    identity.AddClaims(claims);

                    var refreshToken = JwtToken.BuildJwtToken(claims.ToArray(), _requirement);
                    //return new JsonResult(refreshToken);
                    data.success = true;
                    data.msg = "认证成功";
                    data.response = refreshToken;
                    return data;
                }
            }
            data.success = false;
            data.msg = "认证失败";
            return data;
        }

        #region 测试加密解密

        ///// <summary>
        ///// 测试 MD5 加密字符串
        ///// </summary>
        ///// <param name="password">待加密密码</param>
        ///// <returns></returns>
        //[HttpGet]
        //public string Md5Password(string password = "")
        //{
        //    return EncryptHelper.MD5Encrypt32(password);
        //}

        ///// <summary>
        ///// 测试 RSA 加密字符串
        ///// </summary>
        ///// <param name="password">待加密密码</param>
        ///// <returns></returns>
        //[HttpGet]
        //public string RSAEncript(string password)
        //{
        //    return EncryptHelper.RsaEncrypt(password);
        //}

        ///// <summary>
        ///// 测试 RSA 解密字符串
        ///// </summary>
        ///// <param name="text"></param>
        ///// <returns></returns>
        //[HttpGet]
        //public string RSADecrypt(string text)
        //{
        //    return EncryptHelper.RsaDecrypt(text);
        //}

        #endregion

        /// <summary>
        /// 返回RSA公钥串
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public MessageModel<string> RsaPublicKey()
        {
            //_logger.LogDebug(EncryptHelper.GetRSAPublicKey());
            var data = new MessageModel<string>();
            try
            {
                data.success = true;
                data.msg = "获取成功";
                data.response = EncryptHelper.GetRSAPublicKey();
                return data;
            }
            catch (Exception ex)
            {
                data.success = false;
                data.msg = ex.Message;
                return data;
            }
        }
    }
}
