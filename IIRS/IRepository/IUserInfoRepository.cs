using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using System;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IUserInfoRepository : IBaseRepository<Sys_Userinfo>//类名
    {
        Task<Sys_Userinfo> SaveUserInfo(string loginName, string loginPwd, Guid oid);
        Task<string> GetUserRoleNameStr(string loginName, string loginPwd);
    }
}
