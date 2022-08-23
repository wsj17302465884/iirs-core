using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DJ_ZSGL", Utilities.Common.SysConst.DB_CON_BDC)]
    public partial class DJ_ZSGL
    {
           public DJ_ZSGL(){


           }
           /// <summary>
           /// Desc:编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string ZSBH {get;set;}

           /// <summary>
           /// Desc:证书序列号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZSXLH {get;set;}

           /// <summary>
           /// Desc:证书状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZSZT {get;set;}

           /// <summary>
           /// Desc:录入人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LRR {get;set;}

           /// <summary>
           /// Desc:录入日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? LRRQ {get;set;}

           /// <summary>
           /// Desc:分发人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FFR {get;set;}

           /// <summary>
           /// Desc:分发日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? FFRQ {get;set;}

           /// <summary>
           /// Desc:被分发人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BFFR {get;set;}

           /// <summary>
           /// Desc:被使用证书号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FZZSH {get;set;}

           /// <summary>
           /// Desc:证书类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ZSLX {get;set;}

           /// <summary>
           /// Desc:所属辖区
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SSXQ {get;set;}

           /// <summary>
           /// Desc:废弃人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FQR {get;set;}

           /// <summary>
           /// Desc:废弃日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? FQRQ {get;set;}

           /// <summary>
           /// Desc:批次编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PCBH {get;set;}

           /// <summary>
           /// Desc:受理编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SLBH {get;set;}

           /// <summary>
           /// Desc:使用人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SYR {get;set;}

           /// <summary>
           /// Desc:上手分发人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SSFFR {get;set;}

           /// <summary>
           /// Desc:上手分发日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SSFFRQ {get;set;}

           /// <summary>
           /// Desc:上手被分发人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SSBFFR {get;set;}

           /// <summary>
           /// Desc:上手证书状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SSZSZT {get;set;}

           /// <summary>
           /// Desc:使用日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SYRQ {get;set;}

    }
}
