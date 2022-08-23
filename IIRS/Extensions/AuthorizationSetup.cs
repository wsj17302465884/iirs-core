using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using IIRS.Utilities;
using IIRS.Utilities.AuthHelper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace IIRS.Extensions
{
    /// <summary>
    /// 自定义 Authorization 安装类
    /// </summary>
    public static class AuthorizationSetup
    {
        /// <summary>
        /// 安装自定义 Authorization 方法
        /// </summary>
        /// <param name="services"></param>
        public static void AddAuthorizationSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            #region 基于策略的授权
            services.AddAuthorization(options =>
            {
                options.AddPolicy("Client", policy => policy.RequireRole("Client").Build());
                options.AddPolicy("Admin", policy => policy.RequireRole("Admin").Build());
                options.AddPolicy("SystemOrAdmin", policy => policy.RequireRole("Admin", "System"));
                options.AddPolicy("A_S_O", policy => policy.RequireRole("Admin", "System", "Others"));
            });
            #endregion

            #region 参数
            //读取配置文件
            var symmetricKeyAsBase64 = AppsettingsUtility.SiteConfig.Encrypt.Secret;
            var keyByteArray = Encoding.ASCII.GetBytes(symmetricKeyAsBase64);
            var signingKey = new SymmetricSecurityKey(keyByteArray);
            var Issuer = AppsettingsUtility.SiteConfig.Encrypt.Issuer;
            var Audience = AppsettingsUtility.SiteConfig.Encrypt.Audience;

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

            // 如果要数据库动态绑定，这里先留个空，后边处理器里动态赋值
            var permission = new List<PermissionItem>();

            // 角色与接口的权限要求参数
            var permissionRequirement = new PermissionRequirement(
                "/api/denied",// 拒绝授权的跳转地址（目前无用）
                permission,
                ClaimTypes.Role,//基于角色的授权
                Issuer,//发行人
                Audience,//听众
                signingCredentials,//签名凭据
                expiration: TimeSpan.FromSeconds(60 * 60)//接口的过期时间
                );
            #endregion

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Permissions.Name,
                         policy => policy.Requirements.Add(permissionRequirement));
            });

            // 令牌验证参数
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,        //验证发行人的签名密钥
                IssuerSigningKey = signingKey,          //签名密钥
                ValidateIssuer = true,                  //验证发行人
                ValidIssuer = Issuer,                   //发行人
                ValidateAudience = true,                //验证订阅人
                ValidAudience = Audience,               //订阅人
                ValidateLifetime = true,                //验证生命周期
                ClockSkew = TimeSpan.FromSeconds(30),   //这个是定义的过期的缓存时间
                RequireExpirationTime = true,           //是否要求过期
            };

            // core自带官方JWT认证
            // 开启Bearer认证
            services.AddAuthentication(o => {
                o.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                o.DefaultChallengeScheme = nameof(ApiResponseHandler);
                o.DefaultForbidScheme = nameof(ApiResponseHandler);
            })
             // 添加JwtBearer服务
             .AddJwtBearer(o =>
             {
                 o.TokenValidationParameters = tokenValidationParameters;
                 o.Events = new JwtBearerEvents
                 {
                     OnChallenge = context =>
                     {
                         context.Response.Headers.Add("Token-Error", context.ErrorDescription);
                         return Task.CompletedTask;
                     },
                     OnAuthenticationFailed = context =>
                     {
                         var token = context.Request.Headers["Authorization"].ObjToString().Replace("Bearer ", "");
                         var jwtToken = (new JwtSecurityTokenHandler()).ReadJwtToken(token);

                         if (jwtToken.Issuer != Issuer)
                         {
                             context.Response.Headers.Add("Token-Error-Iss", "issuer is wrong!");
                         }

                         if (jwtToken.Audiences.FirstOrDefault() != Audience)
                         {
                             context.Response.Headers.Add("Token-Error-Aud", "Audience is wrong!");
                         }
                         // 如果过期，则把<是否过期>添加到，返回头信息中
                         if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                         {
                             context.Response.Headers.Add("Token-Expired", "true");
                         }
                         return Task.CompletedTask;
                     },
                     // 为 SignalR 服务加上 AccessToken 头
                     OnMessageReceived = context =>
                     {
                         var accessToken = context.Request.Query["access_token"];

                         // If the request is for our hub...
                         var path = context.HttpContext.Request.Path;
                         if (!string.IsNullOrEmpty(accessToken) &&
                             (path.StartsWithSegments(AppsettingsUtility.SiteConfig.SignalRPath)))
                         {
                             // Read the token out of the query string
                             context.Token = accessToken;
                         }
                         return Task.CompletedTask;
                     }
                 };
             })
             .AddScheme<AuthenticationSchemeOptions, ApiResponseHandler>(nameof(ApiResponseHandler), o => { });

            // 注入权限处理器
            services.AddScoped<IAuthorizationHandler, PermissionHandler>();
            services.AddSingleton(permissionRequirement);
        }
    }
}
