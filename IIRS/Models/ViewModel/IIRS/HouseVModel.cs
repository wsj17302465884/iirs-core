using IIRS.Models.EntityModel.IIRS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace IIRS.Models.ViewModel
{
    /// <summary>
    /// 抵押信息表
    /// </summary>
    public class HouseVModel
    {
        /// <summary>
        /// 是否存在禁止或限制转让抵押不动产的约定
        /// </summary>     
        [JsonProperty("QRZYQK")]
        public string QRZYQK { get; set; }


        /// <summary>
        /// 所在区域
        /// </summary>
        [JsonProperty("SZQY")]
        public string SZQY { get; set; }

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
        public string JSON_PK { get; set; }

        /// <summary>
        /// 抵押类型:抵押、在建工程抵押、抵押预告
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
        /// 登记原因:债务履行期限发生变化,抵押登记,一般抵押权
        /// </summary>
        public string DJYY { get; set; }

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
        public decimal? PGJE { get; set; }

        /// <summary>
        /// 被担保债权数额
        /// </summary>
        public int BDBZQSE { get; set; }

        /// <summary>
        /// 履行期限
        /// </summary>
        public int LXQX { get; set; }

        /// <summary>
        /// 最高债权额
        /// </summary>
        public decimal? ZGZQSE { get; set; }

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
        /// 抵押联系人
        /// </summary>
        public string DYLXR { get; set; }

        /// <summary>
        /// 抵押联系人电话
        /// </summary>
        public string DYLXRDH { get; set; }

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
        public string LXRDH{ get; set; }

        /// <summary>
        /// 收件时间
        /// </summary>
        public DateTime SJSJ { get; set; }

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
        /// 抵押人类型
        /// </summary>
        public string DYRLX { get; set; }

        public string fc_slbh { get; set; }
        public string fc_tstybm { get; set; }

        /// <summary>
        /// 抵押房屋
        /// </summary>
        public List<DyHouseVModel> selectDyHouse { get; set; }

        /// <summary>
        /// 预告房屋不动产证XGDJGL
        /// </summary>
        public List<YgXgdjglModel> ygXgdjglList { get; set; } = new List<YgXgdjglModel>();

        /// <summary>
        /// 抵押人信息
        /// </summary>
        public List<DyPersonVModel> selectDyPerson { get; set; }

        [JsonProperty("selectrightperson")]
        public List<DyPersonVModel> selectRightPerson { get; set; } = new List<DyPersonVModel>();

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

        /// <summary>
        /// 流程实例
        /// </summary>
        public string XID { get; set; }

        /// <summary>
        /// 其他（抵押填写）
        /// </summary>
        /// 
        [JsonProperty("QT_DY")]
        public string QT_DY { get; set; }


        /// <summary>
        /// 收费单
        /// </summary>
        public SFD_INFO sfd { get; set; } = new SFD_INFO();

        /// <summary>
        /// 收费单明细
        /// </summary>
        public List<SFD_FB_INFO> sfdList { get; set; } = new List<SFD_FB_INFO>();

        

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
    }

    public class Base64FilesVModel
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string label { get; set; }

        /// <summary>
        /// Base64图片
        /// </summary>
        public string IMG { get; set; }

        /// <summary>
        /// 是否为Base64格式,1:是,0:否
        /// </summary>
        public int IsBase64 { get; set; } = 0;

        /// <summary>
        /// 当前节点子节点个数
        /// </summary>
        public int MaxPageNo { get; set; } = 0;

        /// <summary>
        /// 级别
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<Base64FilesVModel> children { get; set; } = new List<Base64FilesVModel>();
    }

    public class DyVModel
    {
        /// <summary>
        /// 新受理编号
        /// </summary>
        public string NewSlbh { get; set; }

        /// <summary>
        /// 抵押相关证信息
        /// </summary>
        public List<DyHouseVModel> house { get; set; } = new List<DyHouseVModel>();
                
        /// <summary>
        /// 抵押相关证信息
        /// </summary>
        public List<DyPersonVModel> person { get; set; } = new List<DyPersonVModel>();

        /// <summary>
        /// 上传附件初始化树形结构
        /// </summary>
        public Base64FilesVModel attFiles { get; set; } = new Base64FilesVModel();
    }

    /// <summary>
    /// 抵押房产信息表
    /// </summary>
    public class DyHouseVModel
    {
        /// <summary>
        /// 不动产证明号
        /// </summary>
        [JsonProperty("BDCZH")]
        public string BDCZH { get; set; }

        /// <summary>
        /// 受理编号
        /// </summary>
        [JsonProperty("SLBH")]
        public string SLBH { get; set; }

        /// <summary>
        /// 图属统一编码
        /// </summary>
        [JsonProperty("TSTYBM")]
        public string TSTYBM { get; set; }

        /// <summary>
        /// 证书类型（房产证、土地证）
        /// </summary>
        [JsonProperty("ZSLX")]
        public string ZSLX { get; set; }

        /// <summary>
        /// 房屋面积
        /// </summary>
        [JsonProperty("FWMJ")]
        public decimal? FWMJ { get; set; }

        /// <summary>
        /// 权利人名称
        /// </summary>
        [JsonProperty("QLRMC")]
        public string QLRMC { get; set; }

        /// <summary>
        /// 坐落
        /// </summary>
        [JsonProperty("ZL")]
        public string ZL { get; set; }

        /// <summary>
        /// 抵押顺位
        /// </summary>
        [JsonProperty("SW")]
        public int SW { get; set; }

        /// <summary>
        /// 不动产单元号
        /// </summary>
        [JsonProperty("BDCDYH")]
        public string BDCDYH { get; set; }
        
        /// <summary>
        /// 不动产类型：房屋或土地
        /// </summary>
        [JsonProperty("BDCLX")]
        public string BDCLX { get; set; }
    }

    /// <summary>
    /// 抵押人信息表
    /// </summary>
    public class DyPersonVModel
    {
        /// <summary>
        /// 权利人名称
        /// </summary>
        [JsonProperty("qlrmc")]
        public string QLRMC { get; set; }

        /// <summary>
        /// 权利人ID
        /// </summary>
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
        [JsonProperty("zjhm")]
        public string ZJHM { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        [JsonProperty("sxh")]
        public int SXH { get; set; }

        /// <summary>
        /// 是否权属人
        /// </summary>
        [JsonProperty("is_owner")]
        public int IS_OWNER { get; set; }

        /// <summary>
        /// 共有份额
        /// </summary>
        [JsonProperty("gyfe")]
        public string GYFE { get; set; } = "";

        /// <summary>
        /// 电话
        /// </summary>
        [JsonProperty("dh")]
        public string DH { get; set; } = "";

        /// <summary>
        /// 是否认证(1：已认证,0：未认证)
        /// </summary>
        [JsonProperty("iscertified")]
        public int IsCertified { get; set; } = 0;
    }

    /// <summary>
    /// 预告房屋不动产证XGDJGL
    /// </summary>
    public class YgXgdjglModel
    {
        public string BGBM { get; set; }

        /// <summary>
        /// Desc:子受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZSLBH { get; set; }

        /// <summary>
        /// Desc:父受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FSLBH { get; set; }

        /// <summary>
        /// Desc:变更日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? BGRQ { get; set; }

        /// <summary>
        /// Desc:变更类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BGLX { get; set; }

        /// <summary>
        /// Desc:相关证号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XGZH { get; set; }

        /// <summary>
        /// Desc:相关证类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XGZLX { get; set; }

        /// <summary>
        /// Desc:记录树形关系
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string PID { get; set; }
        public string XID { get; set; }
    }
}