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
    [SugarTable("DJ_SJD", SysConst.DB_CON_BDC)]
    public partial class DJ_SJD
    {
           public DJ_SJD(){


           }
           /// <summary>
           /// Desc:受理编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string SLBH {get;set;}

           /// <summary>
           /// Desc:登记大类
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DJDL {get;set;}

           /// <summary>
           /// Desc:登记小类
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DJXL {get;set;}

           /// <summary>
           /// Desc:流程类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LCLX {get;set;}

           /// <summary>
           /// Desc:流程编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LCBM {get;set;}

           /// <summary>
           /// Desc:流程名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LCMC {get;set;}

           /// <summary>
           /// Desc:坐落
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZL {get;set;}

           /// <summary>
           /// Desc:收件人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SJR {get;set;}

           /// <summary>
           /// Desc:收件时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SJSJ {get;set;}

           /// <summary>
           /// Desc:承诺时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CNSJ {get;set;}

           /// <summary>
           /// Desc:区县代码/所属辖区
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QXDM {get;set;}

           /// <summary>
           /// Desc:通知人姓名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TZRXM {get;set;}

           /// <summary>
           /// Desc:通知方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TZFS {get;set;}

           /// <summary>
           /// Desc:通知人电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TZRDH {get;set;}

           /// <summary>
           /// Desc:通知人移动电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TZRYDDH {get;set;}

           /// <summary>
           /// Desc:通知人电子邮件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TZRDZYJ {get;set;}

           /// <summary>
           /// Desc:通知人通讯地址
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TZRTXDZ {get;set;}

           /// <summary>
           /// Desc:查询密码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CXMM {get;set;}

           /// <summary>
           /// Desc:收件名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SJMC {get;set;}

           /// <summary>
           /// Desc:收件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SJLX {get;set;}

           /// <summary>
           /// Desc:收件数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SJSL {get;set;}

           /// <summary>
           /// Desc:页数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? YS {get;set;}

           /// <summary>
           /// Desc:是否收缴收验
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SFSYBZ {get;set;}

           /// <summary>
           /// Desc:是否额外收件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SFEWSJ {get;set;}

           /// <summary>
           /// Desc:是否补充收件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SFBCSJ {get;set;}

           /// <summary>
           /// Desc:责任科室
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZRKS {get;set;}

           /// <summary>
           /// Desc:责任人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZRR {get;set;}

           /// <summary>
           /// Desc:优先级
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YXJ {get;set;}

           /// <summary>
           /// Desc:收件备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SJBZ {get;set;}

           /// <summary>
           /// Desc:补充说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BCSM {get;set;}

           /// <summary>
           /// Desc:房产受理号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FCSLH {get;set;}

           /// <summary>
           /// Desc:房产登记类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FCDJLX {get;set;}

           /// <summary>
           /// Desc:房产户编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FCHID {get;set;}

           /// <summary>
           /// Desc:报文ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BIZMSGID {get;set;}

           /// <summary>
           /// Desc:接入业务编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JRYWBM {get;set;}

           /// <summary>
           /// Desc:数据汇交上报状态(1上报成功;-1上报失败)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SBZT {get;set;}

           /// <summary>
           /// Desc:数据汇交下载响应报文次数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SBXZCS {get;set;}

           /// <summary>
           /// Desc:配号信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CERTINUM {get;set;}

           /// <summary>
           /// Desc:配号错误反馈信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CERTIINFO {get;set;}

           /// <summary>
           /// Desc:省级数据上报状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SBSTZT {get;set;}

           /// <summary>
           /// Desc:房产交易合同编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HTBH {get;set;}

           /// <summary>
           /// Desc:房产交易状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JHZT {get;set;}

           /// <summary>
           /// Desc:交易编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JYBH {get;set;}

           /// <summary>
           /// Desc:批次号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PCH {get;set;}

           /// <summary>
           /// Desc:案件状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string AJZT {get;set;}

           /// <summary>
           /// Desc:现场照片
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte[] XCZP {get;set;}

           /// <summary>
           /// Desc:流程实例编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PRJID {get;set;}

           /// <summary>
           /// Desc:共享密钥
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SHAREKEY {get;set;}

           /// <summary>
           /// Desc:共享时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SHARETIME {get;set;}

           /// <summary>
           /// Desc:共享状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SHARESTATE {get;set;}

           /// <summary>
           /// Desc:区县控制
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QXKZ {get;set;}

           /// <summary>
           /// Desc:成果状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CGZT {get;set;}

           /// <summary>
           /// Desc:外网申请编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WWSQBH {get;set;}

           /// <summary>
           /// Desc:外网预约编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WWYYBH {get;set;}

           /// <summary>
           /// Desc:启用限售
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QYXS {get;set;}

           /// <summary>
           /// Desc:公共项目编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GG_XMBH {get;set;}

           /// <summary>
           /// Desc:数据汇交上报次数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SBCS {get;set;}

           /// <summary>
           /// Desc:数据汇交上报时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SBSJ {get;set;}

           /// <summary>
           /// Desc:数据汇交上报结果
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SBJG {get;set;}

           /// <summary>
           /// Desc:省数据汇交上报状态(
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? HBSBZT {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SBXZSJ {get;set;}

           /// <summary>
           /// Desc:测绘编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CHBH {get;set;}

           /// <summary>
           /// Desc:注销大证
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZXDZ {get;set;}

    }
}
