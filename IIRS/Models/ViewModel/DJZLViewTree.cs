using System.Collections.Generic;

namespace IIRS.Models.ViewModel
{
    public class DJZLViewTree
    {
        public DJZLViewTree()
        {
            Children = new List<DJZLViewTree>();
        }
        /// <summary>
        /// tree父级菜单名称
        /// </summary>
        public string Lable { get; set; }

        /// <summary>
        /// tree子集菜单名称
        /// </summary>
        public List<DJZLViewTree> Children { get; set; }
    }
}
