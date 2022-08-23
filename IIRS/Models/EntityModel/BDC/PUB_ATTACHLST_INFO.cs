using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("PUB_ATTACHLST_INFO", Utilities.Common.SysConst.DB_CON_BDC)]
    public partial class PUB_ATTACHLST_INFO
    {
           public PUB_ATTACHLST_INFO(){


           }
           /// <summary>
           /// Desc:编码
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string ATTACHID {get;set;}

           /// <summary>
           /// Desc:文件编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FILEID {get;set;}

           /// <summary>
           /// Desc:父对象类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PARENTTYPE {get;set;}

           /// <summary>
           /// Desc:父对象编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PARENTNODE {get;set;}

           /// <summary>
           /// Desc:附件名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ATTACHNAME {get;set;}

           /// <summary>
           /// Desc:附件类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ATTACHTYPE {get;set;}

           /// <summary>
           /// Desc:上传人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UPLOADBY {get;set;}

           /// <summary>
           /// Desc:上传时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? UPLOADTIME {get;set;}

           /// <summary>
           /// Desc:排序序号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SORTNUM {get;set;}

    }
}
