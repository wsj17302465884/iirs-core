using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("FC_H_QSDC_INFO", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class FC_H_QSDC_INFO
    {
           public FC_H_QSDC_INFO(){


           }
           /// <summary>
           /// Desc:图属统一编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TSTYBM {get;set;}

           /// <summary>
           /// Desc:标识码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? BSM {get;set;}

           /// <summary>
           /// Desc:宗地代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDTYBM {get;set;}

           /// <summary>
           /// Desc:幢号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZH {get;set;}

           /// <summary>
           /// Desc:户号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HH {get;set;}

           /// <summary>
           /// Desc:不动产单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BDCDYH {get;set;}

           /// <summary>
           /// Desc:隶属幢统一编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LSZTYBM {get;set;}

           /// <summary>
           /// Desc:隶属房屋编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LSFWBH {get;set;}

           /// <summary>
           /// Desc:权利类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QLLX {get;set;}

           /// <summary>
           /// Desc:权利性质
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QLXZ {get;set;}

           /// <summary>
           /// Desc:户型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HX {get;set;}

           /// <summary>
           /// Desc:户型结构
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HXJG {get;set;}

           /// <summary>
           /// Desc:建成时装修程度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZXCD {get;set;}

           /// <summary>
           /// Desc:规划用途
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GHYT {get;set;}

           /// <summary>
           /// Desc:坐落
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZL {get;set;}

           /// <summary>
           /// Desc:实际层
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SJC {get;set;}

           /// <summary>
           /// Desc:名义层
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MYC {get;set;}

           /// <summary>
           /// Desc:单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DYH {get;set;}

           /// <summary>
           /// Desc:房间号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FJH {get;set;}

           /// <summary>
           /// Desc:逻辑幢号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LJZH {get;set;}

           /// <summary>
           /// Desc:取得价格
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? QDJG {get;set;}

           /// <summary>
           /// Desc:取得方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QDFS {get;set;}

           /// <summary>
           /// Desc:室号部位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SHBW {get;set;}

           /// <summary>
           /// Desc:预测建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? YCJZMJ {get;set;}

           /// <summary>
           /// Desc:预测套内建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? YCTNJZMJ {get;set;}

           /// <summary>
           /// Desc:预测地下部分建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? YCDXBFJZMJ {get;set;}

           /// <summary>
           /// Desc:预测分摊建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? YCFTJZMJ {get;set;}

           /// <summary>
           /// Desc:预测其它建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? YCQTJZMJ {get;set;}

           /// <summary>
           /// Desc:预测分摊系数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? YCFTXS {get;set;}

           /// <summary>
           /// Desc:实测建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JZMJ {get;set;}

           /// <summary>
           /// Desc:实测套内建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? TNJZMJ {get;set;}

           /// <summary>
           /// Desc:实测分摊建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? FTJZMJ {get;set;}

           /// <summary>
           /// Desc:实测地下部分建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? DXBFJZMJ {get;set;}

           /// <summary>
           /// Desc:实测其他建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? QTJZMJ {get;set;}

           /// <summary>
           /// Desc:实测分摊系数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? FTXS {get;set;}

           /// <summary>
           /// Desc:土地终止日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? TDZZRQ {get;set;}

           /// <summary>
           /// Desc:土地用途
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDYT {get;set;}

           /// <summary>
           /// Desc:土地使用权人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDSYQR {get;set;}

           /// <summary>
           /// Desc:共有土地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? GYTDMJ {get;set;}

           /// <summary>
           /// Desc:分摊土地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? FTTDMJ {get;set;}

           /// <summary>
           /// Desc:独用土地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? DYTDMJ {get;set;}

           /// <summary>
           /// Desc:房屋类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FWLX {get;set;}

           /// <summary>
           /// Desc:房屋性质
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FWXZ {get;set;}

           /// <summary>
           /// Desc:实际层数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SJCS {get;set;}

           /// <summary>
           /// Desc:同层房间数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? TCJS {get;set;}

           /// <summary>
           /// Desc:层高
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? CG {get;set;}

           /// <summary>
           /// Desc:状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ZT {get;set;}

           /// <summary>
           /// Desc:分层分户图
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte[] FCFHT {get;set;}

           /// <summary>
           /// Desc:分层分户草图
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte[] FCFHCT {get;set;}

           /// <summary>
           /// Desc:调查者
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DCZ {get;set;}

           /// <summary>
           /// Desc:调查日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? DCSJ {get;set;}

           /// <summary>
           /// Desc:调查意见
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DCYJ {get;set;}

           /// <summary>
           /// Desc:附加说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FJSM {get;set;}

           /// <summary>
           /// Desc:实际层列号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SJCLH {get;set;}

           /// <summary>
           /// Desc:变更前不动产单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OLDBDCDYH {get;set;}

           /// <summary>
           /// Desc:现实\历史状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? LIFECYCLE {get;set;}

           /// <summary>
           /// Desc:单元名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DYMC {get;set;}

           /// <summary>
           /// Desc:土地使用期限
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDSYQX {get;set;}

           /// <summary>
           /// Desc:土地起始日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? TDQSRQ {get;set;}

           /// <summary>
           /// Desc:土地权利性质
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDQLXZ {get;set;}

           /// <summary>
           /// Desc:小于2.2米储藏间面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? XCCJMJ {get;set;}

           /// <summary>
           /// Desc:高于2.2米储藏间面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? GCCJMJ {get;set;}

           /// <summary>
           /// Desc:违章年份
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WZNF {get;set;}

           /// <summary>
           /// Desc:违章建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? WZJZMJ {get;set;}

           /// <summary>
           /// Desc:房间坐标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FJZB {get;set;}

           /// <summary>
           /// Desc:上坐标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ZBTOP {get;set;}

           /// <summary>
           /// Desc:左坐标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ZBLEFT {get;set;}

           /// <summary>
           /// Desc:右坐标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ZBRIGHT {get;set;}

           /// <summary>
           /// Desc:下坐标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ZBBOTTOM {get;set;}

           /// <summary>
           /// Desc:户类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HLX {get;set;}

           /// <summary>
           /// Desc:测绘状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CHZT {get;set;}

           /// <summary>
           /// Desc:产生标识码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CSBSM {get;set;}

           /// <summary>
           /// Desc:消亡标识码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string XWBSM {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDTYBM1 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZH1 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HH1 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LSFWBH1 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BDCDYH1 {get;set;}

           /// <summary>
           /// Desc:新加总层数，用于解决总数错乱问题
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HZCS {get;set;}

           /// <summary>
           /// Desc:原户编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YHBH {get;set;}

           /// <summary>
           /// Desc:共享状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SHARESTATE {get;set;}

           /// <summary>
           /// Desc:变更禁止信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BGJZXX {get;set;}

           /// <summary>
           /// Desc:变更提示信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BGTSXX {get;set;}

           /// <summary>
           /// Desc:是否人工核实
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ISRGHS {get;set;}

           /// <summary>
           /// Desc:人工核实人员
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGHSRY {get;set;}

           /// <summary>
           /// Desc:人工核实日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? RGHSRQ {get;set;}

           /// <summary>
           /// Desc:初审意见
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CSYJ {get;set;}

           /// <summary>
           /// Desc:初审人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CSR {get;set;}

           /// <summary>
           /// Desc:初审日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CSRQ {get;set;}

           /// <summary>
           /// Desc:复审意见
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FSYJ {get;set;}

           /// <summary>
           /// Desc:复审人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FSR {get;set;}

           /// <summary>
           /// Desc:复审日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? FSRQ {get;set;}

           /// <summary>
           /// Desc:终审意见
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZSYJ {get;set;}

           /// <summary>
           /// Desc:终审人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZSR {get;set;}

           /// <summary>
           /// Desc:终审日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ZSRQ {get;set;}

           /// <summary>
           /// Desc:墙体东
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QTD {get;set;}

           /// <summary>
           /// Desc:墙体南
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QTN {get;set;}

           /// <summary>
           /// Desc:墙体西
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QTX {get;set;}

           /// <summary>
           /// Desc:墙体北
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QTB {get;set;}

           /// <summary>
           /// Desc:构筑物类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GZWLX {get;set;}

           /// <summary>
           /// Desc:共享时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SHARETIME {get;set;}

           /// <summary>
           /// Desc:户单价
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? HDJ {get;set;}

           /// <summary>
           /// Desc:自建挂地块字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GDK {get;set;}

           /// <summary>
           /// Desc:自建LSTYBM备份
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LSTYBM备份 {get;set;}

           /// <summary>
           /// Desc:产别
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CB {get;set;}

           /// <summary>
           /// Desc:临时不动产单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LSBDCDYH {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GBTMP_LSFWBH {get;set;}

           /// <summary>
           /// Desc:已上报BDCDYH，自建
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SBBDCDYH {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CJHZH {get;set;}

           /// <summary>
           /// Desc:预告状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public short? C_BDC_YGZT {get;set;}

           /// <summary>
           /// Desc:发证状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public short? C_BDC_FZZT {get;set;}

           /// <summary>
           /// Desc:查封状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public short? C_BDC_CFZT {get;set;}

           /// <summary>
           /// Desc:抵押状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public short? C_BDC_DYZT {get;set;}

           /// <summary>
           /// Desc:异议状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public short? C_BDC_YYZT {get;set;}

           /// <summary>
           /// Desc:合同备案状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public short? C_FC_HTBAZT {get;set;}

           /// <summary>
           /// Desc:评估金额
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? C_PGJE {get;set;}

           /// <summary>
           /// Desc:评估日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? C_PGRQ {get;set;}

           /// <summary>
           /// Desc:不动产证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string C_BDCZH {get;set;}

           /// <summary>
           /// Desc:权利人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string C_QLR {get;set;}

           /// <summary>
           /// Desc:规划用途描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GHYTMS {get;set;}

           /// <summary>
           /// Desc:宗地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ZDMJ {get;set;}

           /// <summary>
           /// Desc:土地权利类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDQLLX {get;set;}

           /// <summary>
           /// Desc:登记坐落
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DJZL {get;set;}

           /// <summary>
           /// Desc:土地用途1
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDYTTT {get;set;}

           /// <summary>
           /// Desc:修改人1
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string XGR1 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BFTDYT {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? BFTDZZRQ {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BFTDQLXZ {get;set;}

    }
}
