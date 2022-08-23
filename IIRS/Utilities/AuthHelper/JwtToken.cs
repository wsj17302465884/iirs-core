using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using IIRS.Models.ViewModel;

namespace IIRS.Utilities.AuthHelper
{
    /// <summary>
    /// JWTToken生成类
    /// </summary>
    public class JwtToken
    {
        /// <summary>
        /// 获取基于JWT的Token
        /// </summary>
        /// <param name="claims">需要在登陆的时候配置</param>
        /// <param name="permissionRequirement">在startup中定义的参数</param>
        /// <returns></returns>
        public static TokenModel BuildJwtToken(Claim[] claims, PermissionRequirement permissionRequirement)
        {
            var now = DateTime.Now;
            // 实例化JwtSecurityToken
            var jwt = new JwtSecurityToken(
                issuer: permissionRequirement.Issuer,
                audience: permissionRequirement.Audience,
                claims: claims,
                notBefore: now,
                expires: now.Add(permissionRequirement.Expiration),
                signingCredentials: permissionRequirement.SigningCredentials
            );
            // 生成 Token
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            //打包返回前台
            //此处不能使用元组，必须 new，否则返回前台的数据无法正常显示名称
            var responseJson = new TokenModel
            {
                Token = encodedJwt,
                ExpiresIn = permissionRequirement.Expiration.TotalSeconds,
                TokenType = "Bearer"
            };
            return responseJson;
        }
    }
}
