using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("QL_TDXG", Utilities.Common.SysConst.DB_CON_BDC)]
    public partial class QL_TDXG
    {
           public QL_TDXG(){


           }
           /// <summary>
           /// Desc:权利编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string QLBH {get;set;}

           /// <summary>
           /// Desc:受理编号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SLBH {get;set;}

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
           /// Desc:使用期限
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SYQX {get;set;}

           /// <summary>
           /// Desc:起始时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? QSRQ {get;set;}

           /// <summary>
           /// Desc:终止时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? ZZRQ {get;set;}

           /// <summary>
           /// Desc:土地用途
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDYT {get;set;}

           /// <summary>
           /// Desc:土地使用权人
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDSYQR {get;set;}

           /// <summary>
           /// Desc:共有土地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? GYTDMJ {get;set;}

           /// <summary>
           /// Desc:独用土地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? DYTDMJ {get;set;}

           /// <summary>
           /// Desc:分摊土地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? FTTDMJ {get;set;}

           /// <summary>
           /// Desc:建筑宗地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JZZDMJ {get;set;}

           /// <summary>
           /// Desc:建筑物占地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JZWZDMJ {get;set;}

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
           /// Desc:林地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? LDMJ {get;set;}

           /// <summary>
           /// Desc:牧草面积
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
           /// Desc:未利用地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? WLYDMJ {get;set;}

           /// <summary>
           /// Desc:园地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? YDMJ {get;set;}

           /// <summary>
           /// Desc:土地用途描述
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string TDYTMS {get;set;}

    }
}
