using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("WFM_ACTINST", Utilities.Common.SysConst.DB_CON_BDC)]
    public partial class WFM_ACTINST
    {
           public WFM_ACTINST(){


           }
           /// <summary>
           /// Desc:步骤ID
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string WRKID {get;set;}

           /// <summary>
           /// Desc:受理号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PRJID {get;set;}

           /// <summary>
           /// Desc:模板类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MDLTYPE {get;set;}

           /// <summary>
           /// Desc:模板ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string MDLID {get;set;}

           /// <summary>
           /// Desc:步骤名
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string STEPNAME {get;set;}

           /// <summary>
           /// Desc:提交人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SUBMITBY {get;set;}

           /// <summary>
           /// Desc:提交时间
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime SUBMITTIME {get;set;}

           /// <summary>
           /// Desc:接收人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ACCEPTBY {get;set;}

           /// <summary>
           /// Desc:接收时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ACCEPTTIME {get;set;}

           /// <summary>
           /// Desc:最后一次保存时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SAVETIME {get;set;}

           /// <summary>
           /// Desc:提交类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SUBMITTYPE {get;set;}

           /// <summary>
           /// Desc:办理人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TRANSACTOR {get;set;}

           /// <summary>
           /// Desc:办理方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TRANSACTWAY {get;set;}

           /// <summary>
           /// Desc:完成时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? COMPLETETIME {get;set;}

           /// <summary>
           /// Desc:状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string STEPSTATE {get;set;}

           /// <summary>
           /// Desc:类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string STEPTYPE {get;set;}

           /// <summary>
           /// Desc:序号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? STEPNUM {get;set;}

           /// <summary>
           /// Desc:期限
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? STEPLIMIT {get;set;}

           /// <summary>
           /// Desc:限制时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? LIMITTIME {get;set;}

           /// <summary>
           /// Desc:路径
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ENTRYPATH {get;set;}

           /// <summary>
           /// Desc:入口参数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ENTRYPARAM {get;set;}

           /// <summary>
           /// Desc:委托人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CONSIGNOR {get;set;}

           /// <summary>
           /// Desc:剩余天数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? LEFTDAYS {get;set;}

           /// <summary>
           /// Desc:督办次数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? OVERSEE {get;set;}

           /// <summary>
           /// Desc:办件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BIZTYPE {get;set;}

           /// <summary>
           /// Desc:STEPALIAS
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string STEPALIAS {get;set;}

           /// <summary>
           /// Desc:预警状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YJZT {get;set;}

           /// <summary>
           /// Desc:预警信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YJXX {get;set;}

           /// <summary>
           /// Desc:提示风险点提醒次数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? REMINDS {get;set;}

           /// <summary>
           /// Desc:异议提交（为空或是视为同意）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UNCONSENT {get;set;}

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
           /// Desc:闸门控制状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GATESTATE {get;set;}

    }
}
