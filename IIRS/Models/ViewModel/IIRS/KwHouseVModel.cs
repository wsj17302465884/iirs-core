using IIRS.Models.EntityModel.IIRS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IIRS.Models.ViewModel
{
    /// <summary>
    /// 抵押信息表
    /// </summary>
    public class KwHouseVModel
    {
       

        /// <summary>
        /// 抵押房屋
        /// </summary>
        public KwDyHouseVModel selectDyHouse { get; set; }

        /// <summary>
        /// 抵押人信息
        /// </summary>
        public List<KwDyPersonVModel> selectDyPerson { get; set; }

        [JsonProperty("selectrightperson")]
        public List<KwDyRightPersonVModel> selectRightPerson { get; set; } = new List<KwDyRightPersonVModel>();

        /// <summary>
        /// 上传附件信息(已经上传成功文件信息)
        /// </summary>
        public List<PUB_ATT_FILE> attFiles { get; set; } = new List<PUB_ATT_FILE>();

        /// <summary>
        /// 上传附件信息(已经上传成功文件信息)
        /// </summary>
        public List<PUB_ATT_FILE> base64Files { get; set; } = new List<PUB_ATT_FILE>();

        /// <summary>
        /// OCX控件保存附件信息
        /// </summary>
        public MediasVModel mediasVModel { get; set; } = new MediasVModel();

       
    }


    public class DyKwVModel
    {
        /// <summary>
        /// 新受理编号
        /// </summary>
        public string NewSlbh { get; set; }

        /// <summary>
        /// 抵押相关证信息
        /// </summary>
        public List<KwDyHouseVModel> house { get; set; } = new List<KwDyHouseVModel>();
                
        /// <summary>
        /// 抵押人信息
        /// </summary>
        public List<KwDyPersonVModel> person { get; set; } = new List<KwDyPersonVModel>();
        /// <summary>
        /// 抵押权人信息
        /// </summary>
        public List<KwDyRightPersonVModel> rightperson { get; set; } = new List<KwDyRightPersonVModel>();
        /// <summary>
        /// 上传附件初始化树形结构
        /// </summary>
        public Base64FilesVModel attFiles { get; set; } = new Base64FilesVModel();
    }

    /// <summary>
    /// 抵押房产信息表
    /// </summary>
    public class KwDyHouseVModel
    {
        /// <summary>
        /// 受理编号
        /// </summary>
        [JsonProperty("SLBH")]
        public string SLBH { get; set; }
        /// <summary>
        /// 登记大类
        /// </summary>
        [JsonProperty("DJDL")]
        public string DJDL { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [JsonProperty("LIFECYCLE")]
        public decimal? LIFECYCLE { get; set; }
        /// <summary>
        /// 登记原因
        /// </summary>
        [JsonProperty("DJYY")]
        public string DJYY { get; set; }
        /// <summary>
        /// 登记小类
        /// </summary>
        [JsonProperty("DJXL")]
        public string DJXL { get; set; }
        /// <summary>
        /// 不动产证明号
        /// </summary>
        [JsonProperty("BDCZMH")]
        public string BDCZMH { get; set; }
        /// <summary>
        /// 证书序列号
        /// </summary>
        [JsonProperty("ZSXLH")]
        public string ZSXLH { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        [JsonProperty("ZL")]
        public string ZL { get; set; }
        /// <summary>
        /// 登记日期
        /// </summary>
        [JsonProperty("DJRQ")]
        public DateTime? DJRQ { get; set; }
        /// <summary>
        /// 收件备注
        /// </summary>
        [JsonProperty("SJBZ")]
        public string SJBZ { get; set; }
    }

    /// <summary>
    /// 抵押人信息表
    /// </summary>
    public class KwDyPersonVModel
    {
        /// <summary>
        /// 权利人名称
        /// </summary>
        [JsonProperty("QLRMC")]
        public string QLRMC { get; set; }
        /// <summary>
        /// 证件类别
        /// </summary>
        [JsonProperty("ZJLB")]
        public string ZJLB { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        [JsonProperty("ZJHM")]
        public string ZJHM { get; set; }
        /// <summary>
        /// 证件类别中文名
        /// </summary>
        [JsonProperty("ZJLB_ZWM")]
        public string ZJLB_ZWM { get; set; }

    }
    /// <summary>
    /// 抵押权人信息表
    /// </summary>
    public class KwDyRightPersonVModel
    {
        /// <summary>
        /// 权利人名称
        /// </summary>
        [JsonProperty("QLRMC")]
        public string QLRMC { get; set; }
        /// <summary>
        /// 证件类别
        /// </summary>
        [JsonProperty("ZJLB")]
        public string ZJLB { get; set; }
        /// <summary>
        /// 证件号码
        /// </summary>
        [JsonProperty("ZJHM")]
        public string ZJHM { get; set; }
        /// <summary>
        /// 证件类别中文名
        /// </summary>
        [JsonProperty("ZJLB_ZWM")]
        public string ZJLB_ZWM { get; set; }
    }
}