using System;
using RT.Comb;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    /// 组织机构表
    ///</summary>
    [SugarTable("SYS_ORGANIZATION", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class Sys_Organization
    {
        /// <summary>
        /// 初始化组织机构表
        /// </summary>
        public Sys_Organization() { }

        /// <summary>
        /// 初始化组织机构表
        /// </summary>
        /// <param name="pid"></param>
        /// <param name="name"></param>
        public Sys_Organization( string name, Guid pid )
        {
            Id = Provider.Sql.Create();
            IsDeleted = false;
            PId = pid;
            Name = name;
            Description = "";
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
        /// 父级编号
        /// </summary>
        public Guid PId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 描述
        /// </summary>           
        public string Description { get; set; }

        /// <summary>
        /// 自定义编码
        /// </summary>           
        public string CustomCode { get; set; }

        /// <summary>
        /// 自定义编码2
        /// </summary>           
        public string CustomCode2 { get; set; }

        /// <summary>
        /// 是否有子节点
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public bool HasChildren { get; set; } = true;

        /// <summary>
        /// 简称
        /// </summary>
        public string NAME_ABB { get; set; }
    }
}