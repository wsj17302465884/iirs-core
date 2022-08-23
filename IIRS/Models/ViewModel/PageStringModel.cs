using System;
using System.Collections.Generic;

namespace IIRS.Models.ViewModel
{
    /// <summary>
    /// 通用分页信息类
    /// </summary>
    public class PageStringModel
    {
        /// <summary>
        /// 当前页标
        /// </summary>
        public int page { get; set; } = 1;

        /// <summary>
        /// 总页数
        /// </summary>
        public int pageCount
        {
            get { return dataCount / PageSize + (dataCount % Convert.ToDecimal(PageSize) == 0 ? 0 : 1); }
        }

        /// <summary>
        /// 数据总数
        /// </summary>
        public int dataCount { get; set; } = 0;

        /// <summary>
        /// 每页大小
        /// </summary>
        public int PageSize { set; get; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public string data { get; set; }
    }
}
