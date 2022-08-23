using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{ 
    ///<summary>
    ///
    ///</summary>
[SugarTable("IFLOW_ACTION_GROUP", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class IFLOW_ACTION_GROUP
    {
           public IFLOW_ACTION_GROUP(){


           }
           /// <summary>
           /// Desc:流程分组编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public int GROUP_ID {get;set;}

           /// <summary>
           /// Desc:流程分组名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string GNAME {get;set;}
        /// <summary>
        /// 是否作废（1：作废，0：使用中）
        /// </summary>
        public int IS_DETELE { get;set;}


    }
}
