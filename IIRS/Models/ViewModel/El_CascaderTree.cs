using System.Collections.Generic;

namespace IIRS.Models.ViewModel
{
    public class El_CascaderTree
    {
        public El_CascaderTree()
        {

        }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// tree子集菜单名称
        /// </summary>
        public List<El_CascaderTree> children { get; set; } = new List<El_CascaderTree>();
    }

    public class El_CascaderNavTree
    {
        public El_CascaderNavTree()
        {

        }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        public string value { get; set; }

        /// <summary>
        /// 显示内容
        /// </summary>
        public string NavUrl { get; set; }

        /// <summary>
        /// tree子集菜单名称
        /// </summary>
        public List<El_CascaderNavTree> children { get; set; } = new List<El_CascaderNavTree>();
    }
}
