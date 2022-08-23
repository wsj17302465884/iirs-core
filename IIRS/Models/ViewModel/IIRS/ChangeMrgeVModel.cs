using IIRS.Models.EntityModel.IIRS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IIRS.Models.ViewModel
{
    /// <summary>
    /// 抵押信息表
    /// </summary>
    public class ChangeMrgeVModel
    {
        /// <summary>
        /// 是否存在禁止或限制转让抵押不动产的约定
        /// </summary>     
        [JsonProperty("QRZYQK")]
        public string QRZYQK { get; set; }

        /// <summary>
        /// 流程名称
        /// </summary>
        [JsonProperty("LCMC")]
        public string LCMC { get; set; }

        /// <summary>
        /// 流程类型
        /// </summary>
        [JsonProperty("LCLX")]
        public string LCLX { get; set; }

        /// <summary>
        /// 命令类型，0:暂存，1:提交当前流程
        /// </summary>
        [JsonProperty("commandtype")]
        public int CommandType { get; set; }

        /// <summary>
        /// 保存数据DATA
        /// </summary>
        [JsonProperty("json_pk")]
        public string JSON_PK { get; set; }
        /// <summary>
        /// 流程实例
        /// </summary>
        [JsonProperty("xid")]
        public string XID { get; set; }

        /// <summary>
        /// 最高债权数额
        /// </summary>
        [JsonProperty("zgzqse")]
        public decimal? ZGZQSE { get; set; }

        /// <summary>
        /// 所在区域
        /// </summary>
        [JsonProperty("szqy")]
        public string SZQY { get; set; }

        /// <summary>
        /// 收件时间
        /// </summary>
        [JsonProperty("SJSJ")]
        public DateTime SJSJ { get; set; }

        /// <summary>
        /// 抵押类型:抵押、在建工程抵押、预告抵押
        /// </summary>
        /// 
        [JsonProperty("DYLX")]
        public string DYLX { get; set; }

        /// <summary>
        /// 抵押人类型
        /// </summary>
        /// 
        [JsonProperty("DYRLX")]
        public string DYRLX { get; set; }

        /// <summary>
        /// 受理编号
        /// </summary>
        /// 
        [JsonProperty("SLBH")]
        public string SLBH { get; set; }

        /// <summary>
        /// 不动产权证号
        /// </summary>
        /// 
        [JsonProperty("BDCQZH_NEW_NUM")]
        public int BDCQZH_NEW_NUM { get; set; }

        /// <summary>
        /// 不动产权证号
        /// </summary>
        /// 
        [JsonProperty("BDCQZH_NEW")]
        public string BDCQZH_NEW { get; set; }

        /// <summary>
        /// 抵押登记原因:债务履行期限发生变化,抵押登记,一般抵押权
        /// </summary>
        [JsonProperty("dj_djyy")]
        public string DJ_DJYY { get; set; }

        /// <summary>
        /// 登记簿登记原因:债务履行期限发生变化,抵押登记,一般抵押权
        /// </summary>
        [JsonProperty("dy_djyy")]
        public string DY_DJYY { get; set; }

        /// <summary>
        /// 登记种类
        /// </summary>
        /// 
        [JsonProperty("DJZL")]
        public string DJZL { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        /// 
        [JsonProperty("HTH")]

        public string HTH { get; set; }

        /// <summary>
        /// 坐落
        /// </summary>
        /// 
        [JsonProperty("ZL")]
        public string ZL { get; set; }

        /// <summary>
        /// 抵押面积
        /// </summary>
        /// 
        [JsonProperty("dyMJ")]
        public decimal dyMJ { get; set; }

        /// <summary>
        /// 抵押方式
        /// </summary>
        /// 
        [JsonProperty("DYFS")]
        public string DYFS { get; set; }

        /// <summary>
        /// 共有方式
        /// </summary>
        /// 
        [JsonProperty("GYFS")]
        public string GYFS { get; set; }

        /// <summary>
        /// 共有方式
        /// </summary>
        /// 
        [JsonProperty("GYFS_ZWMS")]
        public string GYFS_ZWMS { get; set; }

        /// <summary>
        /// 评估金额
        /// </summary>
        /// 
        [JsonProperty("PGJE")]
        public decimal? PGJE { get; set; }

        /// <summary>
        /// 被担保债权数额
        /// </summary>
        /// 
        [JsonProperty("BDBZQSE")]
        public int? BDBZQSE { get; set; }

        /// <summary>
        /// 履行期限
        /// </summary>
        /// 
        [JsonProperty("LXQX")]
        public int LXQX { get; set; }

        /// <summary>
        /// 抵押顺位
        /// </summary>
        /// 
        [JsonProperty("DYSW")]
        public int DYSW { get; set; }

        /// <summary>
        /// 个人授权订单流水号
        /// </summary>     
        /// 
        [JsonProperty("AUZ_ID")]
        public string AUZ_ID { get; set; }

        /// <summary>
        /// 债务履行期限-起始日期
        /// </summary>
        /// 
        [JsonProperty("ZWLXQXQSRQ")]
        public DateTime? ZWLXQXQSRQ { get; set; }

        /// <summary>
        /// 债务履行期限-截止日期
        /// </summary>
        /// 
        [JsonProperty("ZWLXQXJZRQ")]
        public DateTime? ZWLXQXJZRQ { get; set; }

        /// <summary>
        /// 抵押联系人
        /// </summary>
        /// 
        [JsonProperty("DYLXR")]
        public string DYLXR { get; set; }

        /// <summary>
        /// 抵押联系人电话
        /// </summary>
        /// 
        [JsonProperty("DYLXRDH")]
        public string DYLXRDH { get; set; }

        /// <summary>
        /// 银行统一社会信用代码权利人ID
        /// </summary>
        /// 
        [JsonProperty("YHYTSHXYDM2")]
        public string YHYTSHXYDM2 { get; set; }

        /// <summary>
        /// 银行部门编码
        /// </summary>
        /// 
        [JsonProperty("BankDeptID")]
        public string BankDeptID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        /// 
        [JsonProperty("BZ")]
        public string BZ { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        /// 
        [JsonProperty("LXR")]
        public string LXR { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        /// 
        [JsonProperty("LXRDH")]
        public string LXRDH { get; set; }

        /// <summary>
        /// 承诺时间
        /// </summary>
        /// 
        [JsonProperty("CNSJ")]
        public DateTime? CNSJ { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        /// 
        [JsonProperty("SJR")]
        public string SJR { get; set; }

        /// <summary>
        /// 担保范围
        /// </summary>
        /// 
        [JsonProperty("DBFW")]
        public string DBFW { get; set; }

        /// <summary>
        /// 抵押权人名称
        /// </summary>
        /// 
        [JsonProperty("DYQRMC")]
        public string DYQRMC { get; set; }

        /// <summary>
        /// 抵押权人编号
        /// </summary>
        /// 
        [JsonProperty("DYQRMC_ID")]
        public string DYQRMC_ID { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        /// 
        [JsonProperty("QT")]
        public string QT { get; set; }

        /// <summary>
        /// 其他（抵押填写）
        /// </summary>
        /// 
        [JsonProperty("QT_DY")]
        public string QT_DY { get; set; }

        /// <summary>
        /// 附记
        /// </summary>
        /// 
        [JsonProperty("FJ")]
        public string FJ { get; set; }

        /// <summary>
        /// 房屋及土地权利信息
        /// </summary>
        /// 
        [JsonProperty("LANDRIGHTINFO")]
        public QL_XG_INFO LandRightInfo { get; set; }

        /// <summary>
        /// 抵押房屋
        /// </summary>
        /// 

        [JsonProperty("selectHouse")]
        public ChangeMrgeHouseVModel selectHouse { get; set; }

        /// <summary>
        /// 抵押人信息
        /// </summary>
        ///
        [JsonProperty("selectDyPerson")]
        public List<ChangeMrgePersonVModel> selectDyPerson { get; set; }

        /// <summary>
        /// 义务人信息
        /// </summary>
        /// 
        [JsonProperty("selectYwPerson")]
        public List<ChangeMrgePersonVModel> selectYwPerson { get; set; }

        /// <summary>
        /// 抵押权人信息
        /// </summary>
        /// 
        [JsonProperty("selectDYQPerson")]
        public List<ChangeMrgePersonVModel> selectDyqPerson { get; set; }

        /// <summary>
        /// 上传附件信息(已经上传成功文件信息)
        /// </summary>
        /// 
        [JsonProperty("attFiles")]
        public List<PUB_ATT_FILE> attFiles { get; set; } = new List<PUB_ATT_FILE>();

        /// <summary>
        /// 上传附件信息(已经上传成功文件信息)
        /// </summary>
        public List<PUB_ATT_FILE> base64Files { get; set; } = new List<PUB_ATT_FILE>();

        /// <summary>
        /// OCX控件保存附件信息
        /// </summary>
        public MediasVModel mediasVModel { get; set; } = new MediasVModel();

        /// <summary>
        /// 权利相关信息
        /// </summary>
        /// 
        [JsonProperty("QLXG_INFO")]
        public QL_XG_INFO qlxgInfo { get; set; }

        /// <summary>
        /// 抵押收费单
        /// </summary>
        [JsonProperty("dysfd")]
        public SFD_INFO dySfd { get; set; } = new SFD_INFO();

        /// <summary>
        /// 抵押收费单明细
        /// </summary>
        [JsonProperty("dysfdlist")]
        public List<SFD_FB_INFO> dySfdList { get; set; } = new List<SFD_FB_INFO>();

        /// <summary>
        /// 登记收费单
        /// </summary>
        [JsonProperty("djsfd")]
        public SFD_INFO djSfd { get; set; } = new SFD_INFO();

        /// <summary>
        /// 登记收费单明细
        /// </summary>
        [JsonProperty("djsfdlist")]
        public List<SFD_FB_INFO> djSfdList { get; set; } = new List<SFD_FB_INFO>();

        /// <summary>
        /// 排队号
        /// </summary>
        [JsonProperty("PDH")]
        public string PDH { get; set; }

        /// <summary>
        /// 审批人
        /// </summary>
        [JsonProperty("SPR")]
        public string SPR { get; set; }

        /// <summary>
        /// 审批意见
        /// </summary>
        [JsonProperty("SPYJ")]
        public string SPYJ { get; set; }

        /// <summary>
        /// 审批备注
        /// </summary>
        [JsonProperty("SPBZ")]
        public string SPBZ { get; set; }

        /// <summary>
        /// 元数据
        /// </summary>
        [JsonProperty("metedata")]
        public Dictionary<string, string>? MeteData { get; set; }
    }

    /// <summary>
    /// 抵押房产信息表
    /// </summary>
    public class ChangeMrgeHouseVModel
    {
        /// <summary>
        /// 状态
        /// </summary>

        [JsonProperty("zt")]
        public string ZT { get; set; }

        /// <summary>
        /// 不动产证明号
        /// </summary>

        [JsonProperty("bdczh")]
        public string BDCZH { get; set; }

        /// <summary>
        /// 受理编号
        /// </summary>
        [JsonProperty("slbh")]
        public string SLBH { get; set; }

        /// <summary>
        /// 图属统一编码
        /// </summary>
        [JsonProperty("tstybm")]
        public string TSTYBM { get; set; }

        /// <summary>
        /// 证书类型（房产证、土地证）
        /// </summary>
        [JsonProperty("zslx")]
        public string ZSLX { get; set; }

        /// <summary>
        /// 权利人名称
        /// </summary>
        [JsonProperty("qlrmc")]
        public string QLRMC { get; set; }

        /// <summary>
        /// 坐落
        /// </summary>
        [JsonProperty("zl")]
        public string ZL { get; set; }

        /// <summary>
        /// 抵押顺位
        /// </summary>
        [JsonProperty("sw")]
        public int SW { get; set; }

        /// <summary>
        /// 不动产单元号
        /// </summary>
        [JsonProperty("bdcdyh")]
        public string BDCDYH { get; set; }

        /// <summary>
        /// 不动产类型：房屋或土地
        /// </summary>
        [JsonProperty("bdclx")]
        public string BDCLX { get; set; }

        /// <summary>
        /// 房屋面积
        /// </summary>
        [JsonProperty("fwmj")]
        public string FWMJ { get; set; }

        /// <summary>
        /// 土地面积
        /// </summary>
        [JsonProperty("tdmj")]
        public string TDMJ { get; set; }

        /// <summary>
        /// 登记簿附记
        /// </summary>
        [JsonProperty("fj")]
        public string FJ { get; set; }
    }

    /// <summary>
    /// 抵押人信息表
    /// </summary>
    public class ChangeMrgePersonVModel
    {
        /// <summary>
        /// 权利人名称
        /// </summary>
        [JsonProperty("qlrmc")]
        public string QLRMC { get; set; }

        /// <summary>
        /// 权利人ID
        /// </summary>
        /// 
        [JsonProperty("qlrid")]
        public string QLRID { get; set; }

        /// <summary>
        /// 证件类别代码(身份证、护照、军官证等)
        /// </summary>
        [JsonProperty("zjlb")]
        public string ZJLB { get; set; }

        /// <summary>
        /// 证件类别中文名(身份证、护照、军官证等)
        /// </summary>
        [JsonProperty("zjlb_zwm")]
        public string ZJLB_ZWM { get; set; }

        /// <summary>
        /// 证件号码（身份证、营业执照）
        /// </summary>
        /// 
        [JsonProperty("zjhm")]
        public string ZJHM { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        public decimal? SXH { get; set; }

        /// <summary>
        /// 是否权属人
        /// </summary>
        /// 
        [JsonProperty("is_owner")]
        public int IS_OWNER { get; set; }

        /// <summary>
        /// 共有方式
        /// </summary>
        /// 
        [JsonProperty("GYFS")]
        public string GYFS { get; set; } = "";

        /// <summary>
        /// 共有份额
        /// </summary>
        /// 
        [JsonProperty("gyfe")]
        public string GYFE { get; set; } = "";

        /// <summary>
        /// 电话
        /// </summary>
        /// 
        [JsonProperty("dh")]
        public string DH { get; set; } = "";

        /// <summary>
        /// 是否认证(1：已认证,0：未认证)
        /// </summary>
        /// 
        [JsonProperty("iscertified")]
        public int IsCertified { get; set; } = 0;
    }
}