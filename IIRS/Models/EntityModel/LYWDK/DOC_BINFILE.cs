using System;
using System.Linq;
using System.Text;
using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.LYWDK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DOC_BINFILE", SysConst.DB_CON_LYWDK)]
    public partial class DOC_BINFILE
    {
           public DOC_BINFILE(){


           }
           /// <summary>
           /// Desc:编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string BINID {get;set;}

           /// <summary>
           /// Desc:文件编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FILEID {get;set;}

           /// <summary>
           /// Desc:文件名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FILENAME {get;set;}

           /// <summary>
           /// Desc:扩展名
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
           /// Desc:是否加密
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ISENCRYPTED {get;set;}

           /// <summary>
           /// Desc:是否压缩
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string ISCOMPRESSED {get;set;}

           /// <summary>
           /// Desc:打开方式
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OPENBY {get;set;}

           /// <summary>
           /// Desc:打开密码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OPENPWD {get;set;}

           /// <summary>
           /// Desc:来源
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string COMEFROM {get;set;}

           /// <summary>
           /// Desc:文件内容
           /// Default:
           /// Nullable:True
           /// </summary>           
           public byte[] FILECONTENT {get;set;}

           /// <summary>
           /// Desc:排序序号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? SORTNUM {get;set;}

           /// <summary>
           /// Desc:页数
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? PAGECOUNT {get;set;}

           /// <summary>
           /// Desc:FTP路径
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FTPATH {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string YWSLBH {get;set;}

    }
}
