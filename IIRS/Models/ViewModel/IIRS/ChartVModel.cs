using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel
{
    public class ChartVModel
    {
        /// <summary>
        /// 环形图
        /// </summary>
        public ChartData_ring ChartData_ring { get; set; }
        /// <summary>
        /// 数据表格
        /// </summary>
        public DataItem TableData { get; set; } 
        /// <summary>
        /// 柱状图
        /// </summary>
        public ChartData_pre ChartData_pre { get; set; }
        /// <summary>
        /// 折线图
        /// </summary>
        public ChartData_line ChartData_line { get; set; }

    }
    public class ChartData_ring
    {
        /// <summary>
        /// 
        /// </summary>
        public List<string> columns { get; set; } 
        /// <summary>
        /// 
        /// </summary>
        public List<ROWS_ring> rows { get; set; } =new List<ROWS_ring>();
    }

    public class TableData
    {
        public string username { get; set; }
        public int mortgage { get; set; }
        public int cancellation { get; set; }
        public int transfer { get; set; }
    }
    public class DataItem
    {
        public List<TableData> ITEM { get; set; } = new List<TableData>();

    }
    public class ChartData_pre
    {
        public List<string> COLUMNS { get; set; } 
        public List<ROWS_pre> ROWS { get; set; } = new List<ROWS_pre>();
    }
    public class ChartData_line
    {
        public List<string> COLUMNS { get; set; } 
        public List<ROWS_line> ROWS { get; set; } = new List<ROWS_line>();
    }


    public class ROWS_ring
    {

            public string dept { get; set; }
            public int count { get; set; }


    }
    public class ROWS_pre
    {

        [JsonProperty("日期")]
        public string date { get; set; }
        [JsonProperty("抵押")]
        public int dy { get; set; }
        [JsonProperty("注销")]
        public int zx { get; set; }
        [JsonProperty("转移及抵押")]
        public int zydy { get; set; }


    }
    public class ROWS_line
    {
        [JsonProperty("日期")]
        public string date { get; set; }
        [JsonProperty("抵押")]
        public int dy { get; set; }
        [JsonProperty("注销")]
        public int zx { get; set; }
        [JsonProperty("转移及抵押")]
        public int zydy { get; set; }


    }
}
