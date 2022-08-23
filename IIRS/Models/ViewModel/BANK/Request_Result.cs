using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BANK
{
    public class Request_Result
    {
        /// <summary>
        /// 请求结果
        /// </summary>
        public Request_Result()
        {

        }
        public string Ret_Code { get; set; }
        public string Ret_Data { get; set; }
        public string Query_Tm { get; set; }
    }
}
