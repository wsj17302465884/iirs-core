using System;
using System.Collections.Generic;
using System.Linq;

namespace IIRS.Utilities.Common
{
    /// <summary>
    /// 泛型递归求树形结构
    /// </summary>
    public static class Recursion
    {
        /// <summary>
        /// 递归取权限树
        /// </summary>
        /// <param name="all"></param>
        /// <param name="curItem"></param>
        /// <param name="pid"></param>
        /// <param name="needbtn"></param>
        public static void LoopToAppendChildren(List<PermissionTree> all, PermissionTree curItem, Guid pid, bool needbtn)
        {

            var subItems = all.Where(ee => ee.Pid == curItem.Value).ToList();

            var btnItems = subItems.Where(ss => ss.Isbtn == true).ToList();
            if (subItems.Count > 0)
            {
                curItem.Btns = new List<PermissionTree>();
                curItem.Btns.AddRange(btnItems);
            }
            else
            {
                curItem.Btns = null;
            }

            if (!needbtn)
            {
                subItems = subItems.Where(ss => ss.Isbtn == false).ToList();
            }
            if (subItems.Count > 0)
            {
                curItem.Children = new List<PermissionTree>();
                curItem.Children.AddRange(subItems);
            }
            else
            {
                curItem.Children = null;
            }

            if (curItem.Isbtn)
            {
                //curItem.label += "按钮";
            }

            foreach (var subItem in subItems)
            {
                if (subItem.Value == pid && pid != Guid.Empty)
                {
                    //subItem.disabled = true;//禁用当前节点
                }
                LoopToAppendChildren(all, subItem, pid, needbtn);
            }
        }

        /// <summary>
        /// 递归取菜单树
        /// </summary>
        /// <param name="all"></param>
        /// <param name="curItem"></param>
        public static void LoopNaviBarAppendChildren(List<NavigationBar> all, NavigationBar curItem)
        {

            var subItems = all.Where(ee => ee.Pid == curItem.Id).ToList();

            if (subItems.Count > 0)
            {
                curItem.Children = new List<NavigationBar>();
                curItem.Children.AddRange(subItems);
            }
            else
            {
                curItem.Children = null;
            }


            foreach (var subItem in subItems)
            {
                LoopNaviBarAppendChildren(all, subItem);
            }
        }

        /// <summary>
        /// 泛型递归
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="all"></param>
        /// <param name="curItem"></param>
        /// <param name="parentIdName"></param>
        /// <param name="idName"></param>
        /// <param name="childrenName"></param>
        public static void LoopToAppendChildrenT<T>(List<T> all, T curItem, string parentIdName = "PId", string idName = "Id", string childrenName = "Children")
        {
            var subItems = all.Where(ee => ee.GetType().GetProperty(parentIdName).GetValue(ee, null).ToString() == curItem.GetType().GetProperty(idName).GetValue(curItem, null).ToString()).ToList();

            if (subItems.Count > 0) curItem.GetType().GetProperty(childrenName).SetValue(curItem, subItems);
            foreach (var subItem in subItems)
            {
                LoopToAppendChildrenT(all, subItem, parentIdName, idName, childrenName);
            }
        }
    }

    public class PermissionTree
    {
        public Guid Value { get; set; }
        public Guid Pid { get; set; }
        public string Label { get; set; }
        public float Order { get; set; }
        public bool Isbtn { get; set; }
        public bool Disabled { get; set; }
        public List<PermissionTree> Children { get; set; }
        public List<PermissionTree> Btns { get; set; }
    }

    public class NavigationBar
    {
        public Guid Id { get; set; }
        public Guid Pid { get; set; }
        public float Order { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Redirect { get; set; }
        public bool IsHide { get; set; } = false;
        public bool IsButton { get; set; } = false;
        public string Path { get; set; }
        public string Func { get; set; }
        public string IconCls { get; set; }
        public NavigationBarMeta Meta { get; set; }
        public List<NavigationBar> Children { get; set; }
    }

    public class NavigationBarMeta
    {
        public string Title { get; set; }
        public bool RequireAuth { get; set; } = true;
        public bool NoTabPage { get; set; } = false;
    }

    public class OrganizationTree
    {
        public Guid Id { get; set; }
        public Guid PId { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public bool IsDelete { get; set; }
        public bool Disabled { get; set; }
        public List<OrganizationTree> Children { get; set; }
    }
}
