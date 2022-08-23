using System;
using RT.Comb;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    /// <summary>
    /// 角色表
    /// </summary>
    [SugarTable("SYS_ROLE", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class Sys_Role
    {
        public Sys_Role()
        {
            OrderSort = 1;
            IsDeleted = false;
        }

        public Sys_Role(string name, Guid oid)
        {
            ID = Provider.Sql.Create();
            IsDeleted = false;
            Enabled = true;
            OrgId = oid;
            Name = name;
            Description = "";
            OrderSort = 1;
        }

        public Sys_Role(Guid id, Guid orgid, string name)
        {
            ID = id;
            IsDeleted = false;
            Enabled = true;
            OrgId = orgid;
            Name = name;
            Description = "";
            OrderSort = 1;
        }

        [SugarColumn(IsPrimaryKey = true)]
        public Guid ID { get; set; }

        /// <summary>
        /// 是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 组织机构编号
        /// </summary>
        public Guid OrgId { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }

        /// <summary>
        /// 组织机构名列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string OrgNames { get; set; }
    }
}
