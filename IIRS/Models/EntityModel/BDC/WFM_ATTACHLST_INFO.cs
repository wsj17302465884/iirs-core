using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("WFM_ATTACHLST_INFO", Utilities.Common.SysConst.DB_CON_BDC)]
    public partial class WFM_ATTACHLST_INFO
    {
           public WFM_ATTACHLST_INFO(){


           }
           /// <summary>
           /// Desc:主键（唯一标识）
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string CID {get;set;}

           /// <summary>
           /// Desc:父节点
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PID {get;set;}

           /// <summary>
           /// Desc:名称（文件夹、文件名称）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CNAME {get;set;}

           /// <summary>
           /// Desc:关联编号（与模板ID或实例ID关联）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PNODE {get;set;}

           /// <summary>
           /// Desc:区分流程模版和流程实例
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PTYPE {get;set;}

           /// <summary>
           /// Desc:区分必选或备选
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CTYPE {get;set;}

           /// <summary>
           /// Desc:区分文件夹和文件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CKIND {get;set;}

           /// <summary>
           /// Desc:排序编号（顺序标记）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? CSORT {get;set;}

           /// <summary>
           /// Desc:是否允许为空（扩展字段，标记文件是否允许为空）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CISEMPTY {get;set;}

           /// <summary>
           /// Desc:创建时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? CREATEDATE {get;set;}

           /// <summary>
           /// Desc:创建人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CREATEBY {get;set;}

           /// <summary>
           /// Desc:文件夹序号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? FOLDERNUM {get;set;}

           /// <summary>
           /// Desc:是否上传了附件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ISUPLOAD {get;set;}

           /// <summary>
           /// Desc:指定活动步骤隐藏当前文件
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string UNACTIDS {get;set;}

           /// <summary>
           /// Desc:文件收取状态（0或空否，1是）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? REVSTATE {get;set;}

           /// <summary>
           /// Desc:文件类型（复印件、原件）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FILETYPE {get;set;}

           /// <summary>
           /// Desc:指定目录下文件数量
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? FILENUM {get;set;}

           /// <summary>
           /// Desc:是否初始化当前节点（0或空是，1否）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? ISINIT {get;set;}

           /// <summary>
           /// Desc:指定活动步骤隐藏无附件内容文件夹
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ISNUHIDE {get;set;}

           /// <summary>
           /// Desc:是否共享（0共享，1不共享）
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? CSHARE {get;set;}

           /// <summary>
           /// Desc:FTP路径
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FTPATH {get;set;}

           /// <summary>
           /// Desc:附件内容
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte[] FILECONTENT {get;set;}

           /// <summary>
           /// Desc:后缀名
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string EXTNAME {get;set;}

           /// <summary>
           /// Desc:文件大小
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? FILESIZE {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BF {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BF1 {get;set;}

    }
}
