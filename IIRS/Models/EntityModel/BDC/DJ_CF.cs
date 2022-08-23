using IIRS.Utilities.Common;
using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DJ_CF", SysConst.DB_CON_BDC)]
    public partial class DJ_CF
    {
           public DJ_CF(){


           }
           /// <summary>
           /// Desc:受理编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string SLBH {get;set;}

           /// <summary>
           /// Desc:查封编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CFBH {get;set;}

           /// <summary>
           /// Desc:相关不动产证
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string XGZH {get;set;}

           /// <summary>
           /// Desc:不动产单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BDCDYH {get;set;}

           /// <summary>
           /// Desc:查封顺序
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? CFSX {get;set;}

           /// <summary>
           /// Desc:查封机关
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CFJG {get;set;}

           /// <summary>
           /// Desc:查封类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CFLX {get;set;}

           /// <summary>
           /// Desc:查封文件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CFWJ {get;set;}

           /// <summary>
           /// Desc:查封文号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CFWH {get;set;}

           /// <summary>
           /// Desc:查封起始日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CFQSSJ {get;set;}

           /// <summary>
           /// Desc:查封结束日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CFJSSJ {get;set;}

           /// <summary>
           /// Desc:查封范围
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CFFW {get;set;}

           /// <summary>
           /// Desc:查封不动产单元(0否、1是)
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? CFBDCDY {get;set;}

           /// <summary>
           /// Desc:登记机构
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DJJG {get;set;}

           /// <summary>
           /// Desc:登簿人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DBR {get;set;}

           /// <summary>
           /// Desc:登记时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? DJSJ {get;set;}

           /// <summary>
           /// Desc:查封原因
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CFYY {get;set;}

           /// <summary>
           /// Desc:经办人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string JBR {get;set;}

           /// <summary>
           /// Desc:经办日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? JBRQ {get;set;}

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
           /// Desc:原证权利人关联
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QLR {get;set;}

           /// <summary>
           /// Desc:原证权利人证件编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZJBH {get;set;}

           /// <summary>
           /// Desc:来文日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? LWRQ {get;set;}

           /// <summary>
           /// Desc:轮候信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LHXX {get;set;}

           /// <summary>
           /// Desc:现实\历史状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? LIFECYCLE {get;set;}

           /// <summary>
           /// Desc:原告人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YGR {get;set;}

           /// <summary>
           /// Desc: 查封期限
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CFQX {get;set;}

           /// <summary>
           /// Desc:审批备注
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPBZ {get;set;}

           /// <summary>
           /// Desc:2017年6月6日更新前备份
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? 备份LIFECYCLE {get;set;}

           /// <summary>
           /// Desc:自建ID用于测试
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZJID {get;set;}

           /// <summary>
           /// Desc:自建判断是否新系统解封
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SFJF {get;set;}

           /// <summary>
           /// Desc:自建老系统查封
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LCF {get;set;}

           /// <summary>
           /// Desc:位置
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WZ {get;set;}

           /// <summary>
           /// Desc:购买人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GMR {get;set;}

           /// <summary>
           /// Desc:区分区域
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string QFQY {get;set;}

    }
}
