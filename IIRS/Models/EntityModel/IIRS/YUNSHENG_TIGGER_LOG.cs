using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("YUNSHENG_TIGGER_LOG", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class YUNSHENG_TIGGER_LOG
    {
           public YUNSHENG_TIGGER_LOG(){


           }
           /// <summary>
           /// Desc:标识列
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string Y_ID {get;set;}

           /// <summary>
           /// Desc:唯一标识
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Y_GLBM {get;set;}

           /// <summary>
           /// Desc:类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Y_TYPE {get;set;}

           /// <summary>
           /// Desc:图属统一编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Y_TSTYBM {get;set;}

           /// <summary>
           /// Desc:受理编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Y_SLBH {get;set;}

           /// <summary>
           /// Desc:日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Y_DATE {get;set;}

           /// <summary>
           /// Desc:Oracle错误ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Y_SQLCODE {get;set;}

           /// <summary>
           /// Desc:Oracle错误信息
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Y_SQLERRM {get;set;}

           /// <summary>
           /// Desc:表名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Y_NAME {get;set;}

           /// <summary>
           /// Desc:标注列
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Y_BZ {get;set;}

    }
}
