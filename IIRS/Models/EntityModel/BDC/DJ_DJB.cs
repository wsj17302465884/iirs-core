using System;
using System.Linq;
using System.Text;
using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DJ_DJB", SysConst.DB_CON_BDC)]
    public partial class DJ_DJB
    {
           public DJ_DJB(){


           }
           /// <summary>
           /// Desc:受理编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string SLBH {get;set;}

           /// <summary>
           /// Desc:登记类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DJLX {get;set;}

           /// <summary>
           /// Desc:登记原因
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DJYY {get;set;}

           /// <summary>
           /// Desc:申请证书版式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SQZSBS {get;set;}

           /// <summary>
           /// Desc:申请分别持证
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SQFBCZ {get;set;}

           /// <summary>
           /// Desc:申请日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SQRQ {get;set;}

           /// <summary>
           /// Desc:申请内容
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SQNR {get;set;}

           /// <summary>
           /// Desc:申请备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SQBZ {get;set;}

           /// <summary>
           /// Desc:代理机构名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLJGMC {get;set;}

           /// <summary>
           /// Desc:代理人姓名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLRXM {get;set;}

           /// <summary>
           /// Desc:代理人证件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLRZJLX {get;set;}

           /// <summary>
           /// Desc:代理人证件号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLRZJH {get;set;}

           /// <summary>
           /// Desc:代理人职业资格证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLRZGZH {get;set;}

           /// <summary>
           /// Desc:代理人电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLRDH {get;set;}

           /// <summary>
           /// Desc:代理机构名称2
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLJGMC2 {get;set;}

           /// <summary>
           /// Desc:代理人姓名2
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLRXM2 {get;set;}

           /// <summary>
           /// Desc:代理人证件类型2
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLRZJLX2 {get;set;}

           /// <summary>
           /// Desc:代理人证件号2
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLRZJH2 {get;set;}

           /// <summary>
           /// Desc:代理人职业资格证号2
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLRZGZH2 {get;set;}

           /// <summary>
           /// Desc:代理人电话2
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLRDH2 {get;set;}

           /// <summary>
           /// Desc:审批单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPDW {get;set;}

           /// <summary>
           /// Desc:审批日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SPRQ {get;set;}

           /// <summary>
           /// Desc:审批备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPBZ {get;set;}

           /// <summary>
           /// Desc:不动产证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BDCZH {get;set;}

           /// <summary>
           /// Desc:省市简称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SSJC {get;set;}

           /// <summary>
           /// Desc:机构简称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JGJC {get;set;}

           /// <summary>
           /// Desc:发证年度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FZND {get;set;}

           /// <summary>
           /// Desc:证书号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZSH {get;set;}

           /// <summary>
           /// Desc:缮证人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SZR {get;set;}

           /// <summary>
           /// Desc:共有方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GYFS {get;set;}

           /// <summary>
           /// Desc:共有份额
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GYFE {get;set;}

           /// <summary>
           /// Desc:登记日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? DJRQ {get;set;}

           /// <summary>
           /// Desc:登簿人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DBR {get;set;}

           /// <summary>
           /// Desc:终审人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZSR {get;set;}

           /// <summary>
           /// Desc:发证机关
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FZJG {get;set;}

           /// <summary>
           /// Desc:发证日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? FZRQ {get;set;}

           /// <summary>
           /// Desc:证书类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZSLX {get;set;}

           /// <summary>
           /// Desc:证书序列号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZSXLH {get;set;}

           /// <summary>
           /// Desc:打印次数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? DYCS {get;set;}

           /// <summary>
           /// Desc:归档号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GDH {get;set;}

           /// <summary>
           /// Desc:档案密级
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DAMJ {get;set;}

           /// <summary>
           /// Desc:其他
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QT {get;set;}

           /// <summary>
           /// Desc:附记
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FJ {get;set;}

           /// <summary>
           /// Desc:相关证/证明号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string XGZH {get;set;}

           /// <summary>
           /// Desc:关联证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GLZH {get;set;}

           /// <summary>
           /// Desc:不动产单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BDCDYH {get;set;}

           /// <summary>
           /// Desc:核定批次号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HDPCH {get;set;}

           /// <summary>
           /// Desc:缮证批次号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SZPCH {get;set;}

           /// <summary>
           /// Desc:最小登记单元编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZXDJDYBH {get;set;}

           /// <summary>
           /// Desc:现实\历史状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? LIFECYCLE {get;set;}

           /// <summary>
           /// Desc:备份历史现实
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? LIFECYCLE1 {get;set;}

           /// <summary>
           /// Desc:审批登记内容（自己添加）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPDJNR {get;set;}

           /// <summary>
           /// Desc:位置
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WZ {get;set;}

           /// <summary>
           /// Desc:电子证书序列号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DZZSXLH {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string 补录说明 {get;set;}

           /// <summary>
           /// Desc:测试
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZHCL {get;set;}

           /// <summary>
           /// Desc:弓长岭证号处理数据应用
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GCLZH {get;set;}

           /// <summary>
           /// Desc:产权来源
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CQLY {get;set;}

           /// <summary>
           /// Desc:申请登记内容（自己添加）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SQDJNR {get;set;}

           /// <summary>
           /// Desc:区分区域
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QFQY {get;set;}

    }
}
