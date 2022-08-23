using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.EntityModel.Tax;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC.Condition
{
    public class InsertTransferVModel
    {
        /// <summary>
        /// 元数据
        /// </summary>
        [JsonProperty("metedata")]
        public Dictionary<string, string> MeteData { get; set; }
        /// <summary>
        /// 命令类型，0:暂存，1:提交当前流程
        /// </summary>
        public int commandtype { get; set; }
        /// <summary>
        /// 税务信息提交，1：已提交、0：没有税务信息
        /// </summary>
        public int IsCommit { get; set; }
        public string auz_id { get; set; }
        public string xid { get; set; }
        public string sfjfdw { get; set; }
        public string orgId { get; set; }
        /// <summary>
        /// 新受理编号
        /// </summary>
        public string newSlbh { get; set; }
        /// <summary>
        /// 登记大类
        /// </summary>
        public string djdl { get; set; }
        /// <summary>
        /// 登记原因
        /// </summary>
        public string djyy { get; set; }
        /// <summary>
        /// 收件时间
        /// </summary>
        public string sjsj { get; set; }
        /// <summary>
        /// 承诺时间
        /// </summary>
        public string cnsj { get; set; }
        /// <summary>
        /// 受理科室
        /// </summary>
        public string sjslks { get; set; }
        /// <summary>
        /// 受理人员
        /// </summary>
        public string sjslry { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public string sjyxj { get; set; }
        /// <summary>
        /// 所在区域
        /// </summary>
        public string sjbdcszqy { get; set; }
        /// <summary>
        /// 通知人姓名
        /// </summary>
        public string sjtzrmc { get; set; }
        /// <summary>
        /// 通知人电话
        /// </summary>
        public string sjtzrdh { get; set; }
        /// <summary>
        /// 排队号
        /// </summary>
        public string sjjhxtid { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 收件备注
        /// </summary>
        public string sjbz { get; set; }


        /// <summary>
        /// 宗地面积
        /// </summary>
        public string FWZDMJ { get; set; }
        /// <summary>
        /// 土地权利类型
        /// </summary>
        public string FWTDQLLX { get; set; }
        public string FWTDQLLX_ZWM { get; set; }
        /// <summary>
        /// 土地权利性质
        /// </summary>
        public string FWTDQLXZ { get; set; }
        public string FWTDQLXZ_ZWM { get; set; }
        /// <summary>
        /// 土地用途
        /// </summary>
        public string TDYT { get; set; }
        public string TDYT_ZWM { get; set; }
        /// <summary>
        /// 土地使用权面积
        /// </summary>
        public string FWTDSYQMJ { get; set; }
        /// <summary>
        /// 独用土地面积
        /// </summary>
        public string FWDYTDMJ { get; set; }
        /// <summary>
        /// 分摊土地面积
        /// </summary>
        public string FWFTTDMJ { get; set; }
        /// <summary>
        /// 使用期限
        /// </summary>
        public string FWSYQX { get; set; }
        /// <summary>
        /// 起始日期
        /// </summary>
        public string FWQSRQ { get; set; }
        /// <summary>
        /// 终止日期
        /// </summary>
        public string FWZZRQ { get; set; }
        /// <summary>
        /// 取得方式
        /// </summary>
        public string FWQDFS { get; set; }
        /// <summary>
        /// 评估金额
        /// </summary>
        public string FWPGJE { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public string FWJYJG { get; set; }
        /// <summary>
        /// 房间权利类型
        /// </summary>
        public string FWQLLX { get; set; }
        public string FWQLLX_ZWM { get; set; }
        /// <summary>
        /// 房间权利性质
        /// </summary>
        public string FWQLXZ { get; set; }
        public string FWQLXZ_ZWM { get; set; }
        /// <summary>
        /// 规划用途
        /// </summary>
        public string FWGHYT { get; set; }
        public string FWGHYT_ZWM { get; set; }
        /// <summary>
        /// 户建筑面积
        /// </summary>
        public string FWHJZMJ { get; set; }
        /// <summary>
        /// 户套内面积
        /// </summary>
        public string FWHTNMJ { get; set; }
        /// <summary>
        /// 户套分摊面积
        /// </summary>
        public string FWHTFTMJ { get; set; }
        /// <summary>
        /// 权利其他状况
        /// </summary>
        public string FWQLQTZK { get; set; }
        /// <summary>
        /// 附记
        /// </summary>
        public string FWFJ { get; set; }


        
        /// <summary>
        /// 收费人
        /// </summary>
        public string sfinpsfr { get; set; }
        /// <summary>
        /// 交费单位
        /// </summary>
        public string sfdwradio { get; set; }
        
        /// <summary>
        /// 缴费类型
        /// </summary>
        public string sfjflx { get; set; }
        /// <summary>
        /// 缴费形式
        /// </summary>
        public string sfinpjfxs { get; set; }
        /// <summary>
        /// 项目名称
        /// </summary>
        public string sfinpxmmc { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string sfdh { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string sftxdz { get; set; }
        /// <summary>
        /// 营业税
        /// </summary>
        public string sfinpyys { get; set; }
        /// <summary>
        /// 个人所得税
        /// </summary>
        public string sfinpgrsds { get; set; }
        /// <summary>
        /// 契税
        /// </summary>
        public string sfinpqs { get; set; }
        /// <summary>
        /// 土地增值税
        /// </summary>
        public string sfinptdzzs { get; set; }
        /// <summary>
        /// 计费人
        /// </summary>
        public string sfinpjfr { get; set; }
        /// <summary>
        /// 计费日期
        /// </summary>
        public string sfinpjfrq { get; set; }
        /// <summary>
        /// 审核人
        /// </summary>
        public string sfshr { get; set; }
        /// <summary>
        /// 审核日期
        /// </summary>
        public string sfshrq { get; set; }
        /// <summary>
        /// 应付金额
        /// </summary>
        public string sfyfje { get; set; }
        /// <summary>
        /// 实际金额
        /// </summary>
        public string sfsjje { get; set; }
        /// <summary>
        /// 收款人
        /// </summary>
        public string sfinpskr { get; set; }
        /// <summary>
        /// 收款日期
        /// </summary>
        public string sfinpskrq { get; set; }
        /// <summary>
        /// 收费备注
        /// </summary>
        public string sfbz { get; set; }


        /// <summary>
        /// 房产类型
        /// </summary>
        public string fclx { get; set; }
        /// <summary>
        /// 房产类别
        /// </summary>
        public string fclb { get; set; }
        /// <summary>
        /// 坐落地址
        /// </summary>
        public string zldz { get; set; }
        /// <summary>
        /// 行政区划
        /// </summary>
        public string bdcszqy { get; set; }
        /// <summary>
        /// 乡镇街道
        /// </summary>
        public string xzjd { get; set; }
        /// <summary>
        /// 村/社区
        /// </summary>
        public string csq { get; set; }
        /// <summary>
        /// 行政区域
        /// </summary>
        public string xzqy { get; set; }
        /// <summary>
        /// 所属税务机关
        /// </summary>
        public string ssswjg { get; set; }
        /// <summary>
        /// 房屋类别
        /// </summary>
        public string fwlb2 { get; set; }
        /// <summary>
        /// 权属转移对象
        /// </summary>
        public string qszydx { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public string jylx { get; set; }
        /// <summary>
        /// 房屋小区
        /// </summary>
        public string fwxq { get; set; }
        
        /// <summary>
        /// 单元号
        /// </summary>
        public string dyh { get; set; }
        /// <summary>
        /// 门牌号
        /// </summary>
        public string mph { get; set; }
        /// <summary>
        /// 总层数
        /// </summary>
        public string zcs { get; set; }
        /// <summary>
        /// 不动产房屋结构
        /// </summary>
        public string bdcfwjg { get; set; }
        /// <summary>
        /// 税务房屋结构
        /// </summary>
        public string hqswfwjg { get; set; }
        /// <summary>
        /// 所在基础层
        /// </summary>
        public string szjcc { get; set; }
        /// <summary>
        /// 房屋朝向
        /// </summary>
        public string fwcx { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public string jzmj { get; set; }
        /// <summary>
        /// 分摊面积
        /// </summary>
        public string ftmj { get; set; }
        /// <summary>
        /// 套内面积
        /// </summary>
        public string tnmj { get; set; }
        /// <summary>
        /// 收件时间
        /// </summary>
        public string swsjsj { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public string jyje { get; set; }
        /// <summary>
        /// 评估金额
        /// </summary>
        public string pgje { get; set; }


        /// <summary>
        /// 初审意见
        /// </summary>
        public string spcsyj { get; set; }
        /// <summary>
        /// 初审人
        /// </summary>
        public string spspr { get; set; }
        /// <summary>
        /// 初审日期
        /// </summary>
        public string spsprq { get; set; }
        /// <summary>
        /// 工作代码证号
        /// </summary>
        public string spgzdmzh { get; set; }
        /// <summary>
        /// 审批备注
        /// </summary>
        public string spspbz { get; set; }

        /// <summary>
        /// 原受理编号：选择房屋的受理编号
        /// </summary>
        public string slbh { get; set; }
        /// <summary>
        /// 不动产类型：房屋或者宗地
        /// </summary>
        public string bdclx { get; set; }
        /// <summary>
        /// 当前登录人
        /// </summary>
        public string jbr { get; set; }
        /// <summary>
        /// 相关证号
        /// </summary>
        public string xgzh { get; set; }

        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string bdcdyh { get; set; }
        /// <summary>
        /// 收费编号
        /// </summary>
        public string sfbh { get; set; }

        /// <summary>
        /// 权利人共有方式
        /// </summary>
        public string SelectGYFS { get; set; }
        /// <summary>
        /// 义务人共有方式
        /// </summary>
        public string ywrGYFS { get; set; }
        /// <summary>
        /// 证书类型
        /// </summary>
        public string zslx { get; set; }
        /// <summary>
        /// 权利人Model
        /// </summary>
        public List<qlrInfo> SelectQlrList { get; set; } = new List<qlrInfo>();
        /// <summary>
        /// 义务人Model
        /// </summary>
        public List<qlrInfo> SelectywrList { get; set; } = new List<qlrInfo>();
        
        /// <summary>
        /// 选择房子Model
        /// </summary>
        public List<selectHouse> selectHouse { get; set; } = new List<selectHouse>();
        /// <summary>
        /// 收费单副本Model
        /// </summary>
        public List<sfdfbModel> sfdfbList { get; set; } = new List<sfdfbModel>();
        public List<xgzhData> xgzhData { get; set; } = new List<xgzhData>();

    }

    public class selectHouse
    {
        /// <summary>
        /// 图属统一编码
        /// </summary>
        public string tstybm { get; set; }
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string bdcdyh { get; set; }
        /// <summary>
        /// 幢号
        /// </summary>
        public string zh { get; set; }
        /// <summary>
        /// 单元号
        /// </summary>
        public string dyh { get; set; }
        /// <summary>
        /// 实际层
        /// </summary>
        public string szc { get; set; }

        public string myc { get; set; }
        /// <summary>
        /// 户号
        /// </summary>
        public string hh { get; set; }
        /// <summary>
        /// 房间号
        /// </summary>
        public string fjh { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 建筑物类型
        /// </summary>
        public string jzwlx { get; set; }
        /// <summary>
        /// 建筑物宗地面积
        /// </summary>
        public decimal jzwzdmj { get; set; }
        /// <summary>
        /// 土地使用日期
        /// </summary>
        public string tdsyqr { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string gzwlx { get; set; }
        /// <summary>
        /// 宗地面积
        /// </summary>
        public decimal zdmj { get; set; }
        /// <summary>
        /// 土地权利类型
        /// </summary>
        public string tdlx { get; set; }
        public string tdlx_zwm { get; set; }
        /// <summary>
        /// 土地权利性质
        /// </summary>
        public string tdxz { get; set; }
        public string tdxz_zwm { get; set; }
        /// <summary>
        /// 土地用途
        /// </summary>
        public string tdyt { get; set; }
        public string tdyt_zwm { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal tdsyqmj { get; set; }
        /// <summary>
        /// 独用土地面积
        /// </summary>
        public decimal dytdmj { get; set; }
        /// <summary>
        /// 分摊土地面积
        /// </summary>
        public decimal fttdmj { get; set; }
        /// <summary>
        /// 使用期限
        /// </summary>
        public string syqx { get; set; }
        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime qsrq { get; set; }
        /// <summary>
        /// 终止日期
        /// </summary>
        public DateTime zzrq { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string qdfs { get; set; }
        /// <summary>
        /// 评估金额
        /// </summary>
        public string pgje { get; set; }
        /// <summary>
        /// 交易价格
        /// </summary>
        public string jyjg { get; set; }
        /// <summary>
        /// 房屋权利类型
        /// </summary>
        public string qllx { get; set; }

        public string qllx_zwm { get; set; }
        /// <summary>
        /// 房屋权利性质
        /// </summary>
        public string qlxz { get; set; }
        public string qlxz_zwm { get; set; }
        /// <summary>
        /// 规划用途
        /// </summary>
        public string ghyt { get; set; }
        public string ghyt_zwm { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public decimal jzmj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal htnmj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal hftmj { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string qlqtzk { get; set; }
        /// <summary>
        /// 附记
        /// </summary>
        public string fj { get; set; }
        /// <summary>
        /// 套内建筑面积
        /// </summary>
        public decimal? tnjzmj { get; set; }
        /// <summary>
        /// 分摊建筑面积
        /// </summary>
        public decimal? ftjzmj { get; set; }
    }

    public class sfdfbModel
    {
        public string slbh { get; set; }
        /// <summary>
        /// 清单序号:顺序号
        /// </summary>
        public int qdxh { get; set; }
        /// <summary>
        /// 收费项目：
        /// </summary>
        public string dname { get; set; }
        /// <summary>
        /// 计量单位：元/本
        /// </summary>
        public string jldw { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int sl { get; set; }
        /// <summary>
        /// 收费标准
        /// </summary>
        public string defined_code { get; set; }
        /// <summary>
        /// 核收金额
        /// </summary>
        public string hsje { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal is_used { get; set; }
        /// <summary>
        /// 核收金额
        /// </summary>
        public decimal mark { get; set; }
    }

    public class xgzhData
    {
        public string TSTYBM { get; set; }
        public string SLBH { get; set; }
        public string DJDL { get; set; }
        public string BDCZH { get; set; }
        public string BDCDYH { get; set; }
        public DateTime? DJRQ { get; set; }
        public string QLRMC { get; set; }
        public string ZJHM { get; set; }
        public string ZL { get; set; }
        public string BDCLX { get; set; }
        public string FWMJ { get; set; }
        public string TDMJ { get; set; }
        public string ZSLX { get; set; }
        public string ZT { get; set; }
    }

    public class qlrInfo
    {
        
        public string glbm { get; set; }

        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string slbh { get; set; }

        /// <summary>
        /// Desc:业务编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ywbm { get; set; }

        /// <summary>
        /// Desc:权利人ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string qlrid { get; set; }

        /// <summary>
        /// 是否权属人
        /// </summary>
        public int is_owner { get; set; }

        /// <summary>
        /// Desc:共有方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string gyfs { get; set; }

        /// <summary>
        /// Desc:共有份额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string gyfe { get; set; }

        /// <summary>
        /// Desc:顺序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? sxh { get; set; }

        /// <summary>
        /// Desc:权利人类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string qlrlx { get; set; }

        /// <summary>
        /// Desc:权利人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string qlrmc { get; set; }

        /// <summary>
        /// Desc:证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string zjhm { get; set; }

        /// <summary>
        /// Desc:证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string zjlb { get; set; }

        /// <summary>
        /// Desc:证件名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string zjlb_zwm { get; set; }

        /// <summary>
        /// Desc:电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string dh { get; set; }
        /// <summary>
        /// 登记信息主键
        /// </summary>
        public string xid { get; set; }

        /// <summary>
        /// 是否认证
        /// </summary>
        public string iscertified { get; set; }
    }
}
