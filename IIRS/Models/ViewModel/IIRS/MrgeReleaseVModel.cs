using IIRS.Models.EntityModel.IIRS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.IIRS
{
    public class MrgeReleaseVModel
    {
        /// <summary>
        /// 抵押类型:抵押、在建工程抵押、预告抵押
        /// </summary>
        public string QRZYQK { get; set; }

        public decimal? BDBZZQSE { get; set; }


        public decimal? ZGZQSE { get; set; }

        /// <summary>
        /// 抵押类型:抵押、在建工程抵押、预告抵押
        /// </summary>
        public string DO_ZDCZMH { get; set; }

        /// <summary>
        /// 抵押类型:抵押、在建工程抵押、预告抵押
        /// </summary>
        public string DYLX { get; set; }

        /// <summary>
        /// 受理编号
        /// </summary>
        public string SLBH { get; set; }

        /// <summary>
        /// 不动产证明号
        /// </summary>
        public string BDCZH { get; set; }

        /// <summary>
        /// 注销原因:主债权消灭,抵押权已实现,抵押权人放弃抵押权等
        /// </summary>
        public string ZXYY { get; set; }

        /// <summary>
        /// 登记种类
        /// </summary>
        public string DJZL { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string HTH { get; set; }

        /// <summary>
        /// 左路
        /// </summary>
        public string ZL { get; set; }

        /// <summary>
        /// 抵押面积
        /// </summary>
        public decimal dyMJ { get; set; }

        /// <summary>
        /// 抵押方式
        /// </summary>
        public string DYFS { get; set; }

        /// <summary>
        /// 评估金额
        /// </summary>
        public decimal PGJE { get; set; }

        /// <summary>
        /// 被担保债权数额
        /// </summary>
        public int BDBZQSE { get; set; }

        /// <summary>
        /// 履行期限
        /// </summary>
        public string LXQX { get; set; }

        /// <summary>
        /// 抵押顺位
        /// </summary>
        public int DYSW { get; set; }

        /// <summary>
        /// 个人授权订单流水号
        /// </summary>           
        public string AUZ_ID { get; set; }

        /// <summary>
        /// 债务履行期限-起始日期
        /// </summary>
        public DateTime ZWLXQXQSRQ { get; set; }

        /// <summary>
        /// 债务履行期限-截止日期
        /// </summary>
        public DateTime ZWLXQXJZRQ { get; set; }

        /// <summary>
        /// 债务履行期限-中文
        /// </summary>
        public string LXZWQX_STR { get; set; }

        /// <summary>
        /// 抵押联系人
        /// </summary>
        public string DYZXLXR { get; set; }

        /// <summary>
        /// 抵押联系人电话
        /// </summary>
        public string DYZXLXRDH { get; set; }

        /// <summary>
        /// 银行统一社会信用代码
        /// </summary>
        public string YHYTSHXYDM { get; set; }

        /// <summary>
        /// 银行统一社会信用代码权利人ID
        /// </summary>
        public string YHYTSHXYDM2 { get; set; }

        /// <summary>
        /// 银行部门编码
        /// </summary>
        public string BankDeptID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string LXR { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string LXRDH { get; set; }

        /// <summary>
        /// 承诺时间
        /// </summary>
        public DateTime CNSJ { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string SJR { get; set; }

        /// <summary>
        /// 担保范围
        /// </summary>
        public string DBFW { get; set; }

        /// <summary>
        /// 抵押权人名称
        /// </summary>
        public string DYQRMC { get; set; }

        /// <summary>
        /// 抵押权人编号
        /// </summary>
        public string DYQRMC_ID { get; set; }

        /// <summary>
        /// 抵押房屋
        /// </summary>
        public MrgeReleaseHouseVModel selectHouse { get; set; } = new MrgeReleaseHouseVModel();

        /// <summary>
        /// 抵押人信息
        /// </summary>
        public List<DyPersonVModel> selectPerson { get; set; } = new List<DyPersonVModel>();

        /// <summary>
        /// 抵押权人信息
        /// </summary>
        public List<DyPersonVModel> selectRightPerson { get; set; } = new List<DyPersonVModel>();

        /// <summary>
        /// 上传附件信息(已经上传成功文件信息)
        /// </summary>
        public List<PUB_ATT_FILE> attFiles { get; set; }

        /// <summary>
        /// OCX控件保存附件信息
        /// </summary>
        public MediasVModel mediasVModel { get; set; }
    }

    /// <summary>
    /// 抵押房产信息表
    /// </summary>
    public class MrgeReleaseHouseVModel
    {
        /// <summary>
        /// 不动产证明号
        /// </summary>
        [JsonProperty("bdczh")]
        public string BDCZH { get; set; }

        /// <summary>
        /// 不动产证明号
        /// </summary>
        [JsonProperty("bdcdyh")]
        public string BDCDYH { get; set; } = "";

        /// <summary>
        /// 受理编号
        /// </summary>
        [JsonProperty("slbh")]
        public string SLBH { get; set; } = "";

        /// <summary>
        /// 图属统一编码
        /// </summary>
        [JsonProperty("tstybm")]
        public string TSTYBM { get; set; } = "";

        /// <summary>
        /// 相关证类型（房屋抵押证明、房产证、土地证）
        /// </summary>
        [JsonProperty("zslx")]
        public string ZSLX { get; set; } = "";

        /// <summary>
        /// 权利人名称
        /// </summary>
        [JsonProperty("qlrmc")]
        public string QLRMC { get; set; } = "";

        /// <summary>
        /// 坐落
        /// </summary>
        [JsonProperty("zl")]
        public string ZL { get; set; } = "";

        /// <summary>
        /// 不动产类型：房屋或土地
        /// </summary>
        [JsonProperty("bdclx")]
        public string BDCLX { get; set; } = "";

        /// <summary>
        /// 是否有子对象
        /// </summary>
        public bool hasChildren { get; set; } = false;

        /// <summary>
        /// 子节点
        /// </summary>
        public List<MrgeReleaseHouseVModel> children { get; set; } = new List<MrgeReleaseHouseVModel>();

    }
}
