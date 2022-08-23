using System;
using RT.Comb;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    /// <summary>
    /// 接口API地址信息表
    /// </summary>
    [SugarTable("SYS_MODULE", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class Sys_Module
    {
        public Sys_Module() { }

        public Sys_Module(string name, string linkurl)
        {
            ID = Provider.Sql.Create();
            IsDeleted = false;
            Enabled = true;
            Name = name;
            LinkUrl = linkurl;
        }

        public Sys_Module(Guid id, string name, string linkurl)
        {
            ID = id;
            IsDeleted = false;
            Enabled = true;
            Name = name;
            LinkUrl = linkurl;
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
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 菜单链接地址
        /// </summary>
        public string LinkUrl { get; set; }

        /// <summary>
        /// /描述
        /// </summary>
        public string Description { get; set; }
    }
}
