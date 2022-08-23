using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DJ_SPB", Utilities.Common.SysConst.DB_CON_BDC)]
    public partial class DJ_SPB
    {
           public DJ_SPB(){


           }
           /// <summary>
           /// Desc:审批编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string SPBH {get;set;}

           /// <summary>
           /// Desc:受理编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SLBH {get;set;}

           /// <summary>
           /// Desc:审批对象
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPDX {get;set;}

           /// <summary>
           /// Desc:审批序号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SPXH {get;set;}

           /// <summary>
           /// Desc:审批意见
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPYJ {get;set;}

           /// <summary>
           /// Desc:审批人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPR {get;set;}

           /// <summary>
           /// Desc:审批日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? SPRQ {get;set;}

           /// <summary>
           /// Desc:审批人资格证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPRZGZH {get;set;}

           /// <summary>
           /// Desc:审批结果
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPJG {get;set;}

           /// <summary>
           /// Desc:审批状态
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPZT {get;set;}

           /// <summary>
           /// Desc:审批意见填写人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SPTXR {get;set;}

    }
}
