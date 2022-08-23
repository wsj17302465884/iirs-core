using System;
using RT.Comb;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    /// 用户组织机构关联表
    ///</summary>
    [SugarTable("SYS_USERORGANIZATION", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class Sys_UserOrganization
    {
        /// <summary>
        /// 初始化用户组织机构关联表
        /// </summary>
        public Sys_UserOrganization() { }

        /// <summary>
        /// 初始化用户组织机构关联表
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="oid"></param>
        public Sys_UserOrganization(Guid uid, Guid oid)
        {
            Id = Provider.Sql.Create();
            IsDeleted = false;
            UserId = uid;
            OrgId = oid;
        }

        /// <summary>
        /// 编号
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        /// <summary>
        /// 是否删除，逻辑上的删除，非物理删除
        /// </summary>           
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 用户编号
        /// </summary>           
        public Guid UserId { get; set; }

        /// <summary>
        /// 组织机构编号
        /// </summary>           
        public Guid OrgId { get; set; }

    }
}
