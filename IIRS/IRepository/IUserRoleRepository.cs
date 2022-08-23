using System;
using System.Threading.Tasks;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;

namespace IIRS.IRepository
{
    /// <summary>
	/// IUserRoleRepository
	/// </summary>	
	public interface IUserRoleRepository : IBaseRepository<Sys_UserRole>//类名
    {
        Task<Sys_UserRole> SaveUserRole(Guid uid, Guid rid);
        Task<Guid?> GetRoleIdByUid(Guid uid);
    }
}
