using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IIRS.Models.ViewModel.BDC
{
    public class AdvanceVModel
    {
        [JsonProperty("RN")]
        public int rn { get; set; }
        [JsonProperty("TSTYBM")]
        public string tstybm { get; set; } = "";
        [JsonProperty("SLBH")]
        public string slbh { get;set; } = "";
        [JsonProperty("DJDL")]
        public string djdl { get; set; } = "";
        [JsonProperty("BDCZH")]
        public string bdczh { get; set; } = "";
        [JsonProperty("BDCDYH")]
        public string bdcdyh { get; set; } = "";
        [JsonProperty("DJRQ")]
        public DateTime? djrq { get; set; } = DateTime.Now;
        [JsonProperty("QLRMC")]
        public string qlrmc { get; set; } = "";
        [JsonProperty("ZJHM")]
        public string zjhm { get; set; } = "";
        [JsonProperty("ZL")]
        public string zl { get; set; } = "";
        [JsonProperty("BDCLX")]
        public string bdclx { get; set; } = "";
        [JsonProperty("FWMJ")]
        public string fwmj { get; set; } = "";
        [JsonProperty("TDMJ")]
        public string tdmj { get; set; } = "";
        [JsonProperty("ZT")]
        public string zt { get; set; } = "";
        [JsonProperty("DJB_FJ")]
        public string djb_fj { get; set; } = "";
        [JsonProperty("SZC")]
        public string szc { get; set; } = "";
        [JsonProperty("HZCS")]
        public string hzcs { get; set; } = "";
        [JsonProperty("FWJG")]
        public string fwjg { get; set; } = "";
        [JsonProperty("FWJG_ZWM")]
        public string fwjg_zwm { get; set; } = "";
        [JsonProperty("ZZCS")]
        public string zzcs { get; set; } = "";
        [JsonProperty("zslx")]
        public string zslx { get; set; } = "";
        /// <summary>
        /// 是否有子对象
        /// </summary>
        public bool hasChildren { get; set; } = false;

        /// <summary>
        /// 子节点
        /// </summary>
        public List<AdvanceVModel> children { get; set; } = new List<AdvanceVModel>();
    }
}
