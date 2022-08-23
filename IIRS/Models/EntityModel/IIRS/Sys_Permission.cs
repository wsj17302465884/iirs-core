using System;
using System.Collections.Generic;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    /// <summary>
    /// 路由菜单表
    /// </summary>
    [SugarTable("SYS_PERMISSION", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class Sys_Permission
    {
        public Sys_Permission() { }

        public Sys_Permission(Guid id, string code, string name, string redirect, bool isbutton, Guid pid, Guid mid, string icon)
        {
            ID = id;
            IsDeleted = false;
            Enabled = true;
            Code = code;
            Name = name;
            Redirect = redirect;
            IsButton = isbutton;
            IsHide = false;
            Pid = pid;
            Mid = mid;
            OrderSort = 1;
            Icon = icon;
        }

        [SugarColumn(IsPrimaryKey = true)]
        public Guid ID { get; set; }

        /// <summary>
        /// 是否禁用，逻辑上的删除，非物理删除
        /// </summary>
        public bool? IsDeleted { get; set; }

        /// <summary>
        /// 激活状态
        /// </summary>
        public bool Enabled { get; set; }

        /// <summary>
        /// 菜单执行Action名
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 菜单显示名（如用户页、编辑(按钮)、删除(按钮)）
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 重定向地址
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// 是否是按钮
        /// </summary>
        public bool IsButton { get; set; } = false;

        /// <summary>
        /// 是否是隐藏菜单
        /// </summary>
        public bool? IsHide { get; set; } = false;

        /// <summary>
        /// 按钮事件
        /// </summary>
        public string Func { get; set; }

        /// <summary>
        /// 上一级菜单（ Guid.Empty() 表示上一级无菜单）
        /// </summary>
        public Guid Pid { get; set; }

        /// <summary>
        /// 接口api
        /// </summary>
        public Guid Mid { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public float OrderSort { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 菜单描述
        /// </summary>
        public string Description { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<Guid> PidArr { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<string> PnameArr { get; set; } = new List<string>();

        [SugarColumn(IsIgnore = true)]
        public List<string> PCodeArr { get; set; } = new List<string>();

        [SugarColumn(IsIgnore = true)]
        public string MName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public bool HasChildren { get; set; } = true;
    }
}
