using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("FC_Z_QSDC", Utilities.Common.SysConst.DB_CON_BDC)]
    public partial class FC_Z_QSDC
    {
           public FC_Z_QSDC(){


           }
           /// <summary>
           /// Desc:图属统一编码
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
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
           /// Desc:自然幢号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZH {get;set;}

           /// <summary>
           /// Desc:逻辑幢号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LJZH {get;set;}

           /// <summary>
           /// Desc:房屋编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FWBH {get;set;}

           /// <summary>
           /// Desc:不动产单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BDCDYH {get;set;}

           /// <summary>
           /// Desc:项目名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string XMMC {get;set;}

           /// <summary>
           /// Desc:建筑物名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JZWMC {get;set;}

           /// <summary>
           /// Desc:总套数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ZTS {get;set;}

           /// <summary>
           /// Desc:楼盘类别
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LPLB {get;set;}

           /// <summary>
           /// Desc:楼幢性质
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LZXZ {get;set;}

           /// <summary>
           /// Desc:楼幢特点
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LZTD {get;set;}

           /// <summary>
           /// Desc:房屋座落
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FWZL {get;set;}

           /// <summary>
           /// Desc:权利人总数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? QLRZS {get;set;}

           /// <summary>
           /// Desc:规划用途
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GHYT {get;set;}

           /// <summary>
           /// Desc:房屋结构
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FWJG {get;set;}

           /// <summary>
           /// Desc:总层数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZCS {get;set;}

           /// <summary>
           /// Desc:预测建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? YCJZMJ {get;set;}

           /// <summary>
           /// Desc:实测建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SCJZMJ {get;set;}

           /// <summary>
           /// Desc:幢占地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ZZDMJ {get;set;}

           /// <summary>
           /// Desc:幢用地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ZYDMJ {get;set;}

           /// <summary>
           /// Desc:建筑物高度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JZWGD {get;set;}

           /// <summary>
           /// Desc:竣工日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? JGRQ {get;set;}

           /// <summary>
           /// Desc:地上层数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? DSCS {get;set;}

           /// <summary>
           /// Desc:地下层数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? DXCS {get;set;}

           /// <summary>
           /// Desc:地下深度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? DXSD {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BZ {get;set;}

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
           /// Desc:违章标示
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WZBS {get;set;}

           /// <summary>
           /// Desc:违章说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WZSM {get;set;}

           /// <summary>
           /// Desc:幢名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZMC {get;set;}

           /// <summary>
           /// Desc:门牌号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MPH {get;set;}

           /// <summary>
           /// Desc:房产自然幢编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? FCZRZBH {get;set;}

           /// <summary>
           /// Desc:变更前房屋编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OLDFWBH {get;set;}

           /// <summary>
           /// Desc:建成年份
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JCNF {get;set;}

           /// <summary>
           /// Desc:现实\历史状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? LIFECYCLE {get;set;}

           /// <summary>
           /// Desc:宗地图
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte[] ZDT {get;set;}

           /// <summary>
           /// Desc:宗地草图
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte[] ZDCT {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BFZDTYBM {get;set;}

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
           /// Desc:四至东
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SZD {get;set;}

           /// <summary>
           /// Desc:四至南
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SZN {get;set;}

           /// <summary>
           /// Desc:四到西
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SZX {get;set;}

           /// <summary>
           /// Desc:四到北
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SZB {get;set;}

           /// <summary>
           /// Desc:共享时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SHARETIME {get;set;}

           /// <summary>
           /// Desc:项目编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string XMBH {get;set;}

           /// <summary>
           /// Desc:挂地块字段
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GDK {get;set;}

           /// <summary>
           /// Desc:备份TSTYBM自建
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BFTSTYBM {get;set;}

           /// <summary>
           /// Desc:临时不动产单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LSBDCDYH {get;set;}

           /// <summary>
           /// Desc:备份FWBH自建
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BFFWBH {get;set;}

           /// <summary>
           /// Desc:已上报国土部FWBH，自建
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SBFWBH {get;set;}

           /// <summary>
           /// Desc:土地使用期限
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string C_TDSYQX {get;set;}

           /// <summary>
           /// Desc:土地起始日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? C_TDQSRQ {get;set;}

           /// <summary>
           /// Desc:土地终止日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? C_TDZZRQ {get;set;}

           /// <summary>
           /// Desc:规划用途描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GHYTMS {get;set;}

           /// <summary>
           /// Desc:原幢ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YZID {get;set;}

           /// <summary>
           /// Desc:是否核对
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SFHD {get;set;}

           /// <summary>
           /// Desc:历史问题统计
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LSWTTJ {get;set;}

           /// <summary>
           /// Desc:土地权利类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDQLLX {get;set;}

           /// <summary>
           /// Desc:土地权利性质
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDQLXZ {get;set;}

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
           /// Desc:土地起始日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? TDQSRQ {get;set;}

           /// <summary>
           /// Desc:土地使用期限
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDSYQX {get;set;}

           /// <summary>
           /// Desc:修改人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string XGR1 {get;set;}

    }
}
