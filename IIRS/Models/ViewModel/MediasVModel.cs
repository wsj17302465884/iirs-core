using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IIRS.Models.ViewModel
{
    public class MediasItems
    {
        /// <summary>
        /// 网络图片
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SrcPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TempPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DesPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Type { get; set; }
    }

    
    public class MediasGroups
    {
        /// <summary>
        /// 分组编号
        /// </summary>
        public string GroupsID { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [JsonProperty("Name")]
        public string GroupsName { get; set; }

        /// <summary>
        /// 数据集
        /// </summary>
        [JsonProperty("Items")]
        public List<MediasItems> Items { get; set; } = new List<MediasItems>();

    }

    public class MediasVModel
    {
        [JsonProperty("Groups")]
        public List<MediasGroups> Groups { get; set; } = new List<MediasGroups>();
    }
}
