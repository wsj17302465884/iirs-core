using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("ZD_QSDC", Utilities.Common.SysConst.DB_CON_BDC)]
    public partial class ZD_QSDC
    {
           public ZD_QSDC(){


           }
           /// <summary>
           /// Desc:图属统一编码
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string TSTYBM {get;set;}

           /// <summary>
           /// Desc:宗地类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDLX {get;set;}

           /// <summary>
           /// Desc:宗地代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDTYBM {get;set;}

           /// <summary>
           /// Desc:上级宗地编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SJZDTYBM {get;set;}

           /// <summary>
           /// Desc:不动产单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BDCDYH {get;set;}

           /// <summary>
           /// Desc:地籍号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DJH {get;set;}

           /// <summary>
           /// Desc:权属单位代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QSDWDM {get;set;}

           /// <summary>
           /// Desc:权属单位名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QSDWMC {get;set;}

           /// <summary>
           /// Desc:坐落单位代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZLDWDM {get;set;}

           /// <summary>
           /// Desc:坐落单位名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZLDWMC {get;set;}

           /// <summary>
           /// Desc:土地坐落
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDZL {get;set;}

           /// <summary>
           /// Desc:权利人名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QLRMC {get;set;}

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
           /// Desc:土地所有者
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDSYZ {get;set;}

           /// <summary>
           /// Desc:独用面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? DYMJ {get;set;}

           /// <summary>
           /// Desc:分摊面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? FTMJ {get;set;}

           /// <summary>
           /// Desc:是否共用宗
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SFGYZ {get;set;}

           /// <summary>
           /// Desc:批准土地用途
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PZTDYT {get;set;}

           /// <summary>
           /// Desc:实际土地用途
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SJTDYT {get;set;}

           /// <summary>
           /// Desc:共有使用权情况
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GYSYQQK {get;set;}

           /// <summary>
           /// Desc:实测面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SCMJ {get;set;}

           /// <summary>
           /// Desc:发证面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? FZMJ {get;set;}

           /// <summary>
           /// Desc:所在图幅号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SZTFH {get;set;}

           /// <summary>
           /// Desc:建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JZMJ {get;set;}

           /// <summary>
           /// Desc:建筑物占地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JZWZDMJ {get;set;}

           /// <summary>
           /// Desc:建筑容积率
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JZRJL {get;set;}

           /// <summary>
           /// Desc:建筑密度
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JZMD {get;set;}

           /// <summary>
           /// Desc:建筑限高
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JZXG {get;set;}

           /// <summary>
           /// Desc:取得方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QDFS {get;set;}

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
           /// Desc:四至西
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SZX {get;set;}

           /// <summary>
           /// Desc:四至北
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SZB {get;set;}

           /// <summary>
           /// Desc:说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SM {get;set;}

           /// <summary>
           /// Desc:宗地状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDZT {get;set;}

           /// <summary>
           /// Desc:不动产类型选项
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZBCLXXX {get;set;}

           /// <summary>
           /// Desc:宗地特征码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDTZM {get;set;}

           /// <summary>
           /// Desc:权利设定方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QLSDFS {get;set;}

           /// <summary>
           /// Desc:界址调查员
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JZDCY {get;set;}

           /// <summary>
           /// Desc:指界委托书
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZJWTS {get;set;}

           /// <summary>
           /// Desc:宗地草图绘制员
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDCTHZY {get;set;}

           /// <summary>
           /// Desc:宗地草图绘制日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ZDCTHZRQ {get;set;}

           /// <summary>
           /// Desc:权属调查记事
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QSDCJS {get;set;}

           /// <summary>
           /// Desc:调查员
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DCY {get;set;}

           /// <summary>
           /// Desc:调查日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? DCRQ {get;set;}

           /// <summary>
           /// Desc:界址标示
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JZBS {get;set;}

           /// <summary>
           /// Desc:地籍勘丈记事
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DJKZJS {get;set;}

           /// <summary>
           /// Desc:勘丈员
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string KZY {get;set;}

           /// <summary>
           /// Desc:勘丈日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? KZRQ {get;set;}

           /// <summary>
           /// Desc:调查审核意见
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DCSHYJ {get;set;}

           /// <summary>
           /// Desc:调查审核人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DCSHR {get;set;}

           /// <summary>
           /// Desc:调查审核日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? DCSHRQ {get;set;}

           /// <summary>
           /// Desc:土地总面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? TDZMJ {get;set;}

           /// <summary>
           /// Desc:农用地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? NYDMJ {get;set;}

           /// <summary>
           /// Desc:耕地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? GDMJ {get;set;}

           /// <summary>
           /// Desc:园地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? YDMJ {get;set;}

           /// <summary>
           /// Desc:林地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? LDMJ {get;set;}

           /// <summary>
           /// Desc:牧草地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? MCDMJ {get;set;}

           /// <summary>
           /// Desc:农用地其他面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? NYDQTMJ {get;set;}

           /// <summary>
           /// Desc:建设用地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JSYDMJ {get;set;}

           /// <summary>
           /// Desc:居民点及工矿用地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JMDMJ {get;set;}

           /// <summary>
           /// Desc:交通用地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JTYDMJ {get;set;}

           /// <summary>
           /// Desc:水域面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SYMJ {get;set;}

           /// <summary>
           /// Desc:建设用地其他面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JSYDQTMJ {get;set;}

           /// <summary>
           /// Desc:未利用地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? WLYDMJ {get;set;}

           /// <summary>
           /// Desc:国民经济行业分类代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GMJJHYFLDM {get;set;}

           /// <summary>
           /// Desc:预编宗地代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YBZDDM {get;set;}

           /// <summary>
           /// Desc:比例尺
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BLC {get;set;}

           /// <summary>
           /// Desc:界址点位说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JZDWSM {get;set;}

           /// <summary>
           /// Desc:主要权属界址走向说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZYQSJZZXSM {get;set;}

           /// <summary>
           /// Desc:建筑物类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JZWLX {get;set;}

           /// <summary>
           /// Desc:建筑物权属
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JZWQS {get;set;}

           /// <summary>
           /// Desc:使用期限
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SYQX {get;set;}

           /// <summary>
           /// Desc:起始日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? QSRQ {get;set;}

           /// <summary>
           /// Desc:终止日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ZZRQ {get;set;}

           /// <summary>
           /// Desc:宗地等级
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDDJ {get;set;}

           /// <summary>
           /// Desc:宗地价格
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ZDJG {get;set;}

           /// <summary>
           /// Desc:申报地价
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SBDJ {get;set;}

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
           /// Desc:代理人电话
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DLRDH {get;set;}

           /// <summary>
           /// Desc:批准文号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PZWH {get;set;}

           /// <summary>
           /// Desc:调查表编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DCBBH {get;set;}

           /// <summary>
           /// Desc:建筑类别
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JZLB {get;set;}

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
           /// Desc:现实\历史状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? LIFECYCLE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZL {get;set;}

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
           public string SJZDTYBM1 {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDZH {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZL1 {get;set;}

           /// <summary>
           /// Desc:调查员工作代码证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DCRGZDMZH {get;set;}

           /// <summary>
           /// Desc:勘丈员工作代码证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string KZYGZDMZH {get;set;}

           /// <summary>
           /// Desc:审核员工作代码证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DCSHRGZDMZH {get;set;}

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
           /// Desc:是否人工核实
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ISRGHS {get;set;}

           /// <summary>
           /// Desc:调查单位
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DCDW {get;set;}

           /// <summary>
           /// Desc:土地权属来源证明材料
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDQSLYZMCL {get;set;}

           /// <summary>
           /// Desc:全体业主共有土地使用权面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? QTYZGYTDSYQMJ {get;set;}

           /// <summary>
           /// Desc:空间坐标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string KJZB {get;set;}

           /// <summary>
           /// Desc:位置说明
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WZSM {get;set;}

           /// <summary>
           /// Desc:图形面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? TXMJ {get;set;}

           /// <summary>
           /// Desc:共享时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SHARETIME {get;set;}

           /// <summary>
           /// Desc:规划容积率
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GHRJL {get;set;}

           /// <summary>
           /// Desc:证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZH {get;set;}

           /// <summary>
           /// Desc:发证状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FZZT {get;set;}

           /// <summary>
           /// Desc:标注点X坐标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? LABELX {get;set;}

           /// <summary>
           /// Desc:标注点Y坐标
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? LABELY {get;set;}

           /// <summary>
           /// Desc:备注TSTYBM
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BZTSTYBM {get;set;}

           /// <summary>
           /// Desc:上报zdtybm自建
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SBZDTYBM {get;set;}

           /// <summary>
           /// Desc:备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BZ {get;set;}

           /// <summary>
           /// Desc:临时不动产单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LSBDCDYH {get;set;}

           /// <summary>
           /// Desc:土地用途描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDYTMS {get;set;}

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
           /// Desc:宗地四至
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZDSZ {get;set;}

    }
}
