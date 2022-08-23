using System;
using System.Linq;
using System.Text;
using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("REALEST_INF", SysConst.DB_CON_BANK)]
    public partial class REALEST_INF
    {
           public REALEST_INF(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string BID {get;set;}

           /// <summary>
           /// Desc:不动产单元号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string REALEST_UNIT_NO {get;set;}

           /// <summary>
           /// Desc:所属区县编码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BLNG_CNTYANDDSTC_ECD {get;set;}

           /// <summary>
           /// Desc:所属区县名称
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BLNG_CNTYANDDSTC_NM {get;set;}

           /// <summary>
           /// Desc:抵押顺位位次
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal MRTG_ORDER_PRCD {get;set;}

           /// <summary>
           /// Desc:房屋坐落
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HS_LO {get;set;}

           /// <summary>
           /// Desc:不动产权人证件号码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REALEST_WGHT_PSN_ {get;set;}

           /// <summary>
           /// Desc:房屋建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HS_CNSTRCTAREA {get;set;}

           /// <summary>
           /// Desc:房屋用途
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string HS_USE {get;set;}

           /// <summary>
           /// Desc:共有情况
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string COM_STTN {get;set;}

           /// <summary>
           /// Desc:房屋性质
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string HS_CHAR_NM {get;set;}

           /// <summary>
           /// Desc:房屋类型
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string HS_TP {get;set;}

           /// <summary>
           /// Desc:权利类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGHT_TP {get;set;}

           /// <summary>
           /// Desc:权利终止日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RGHT_TMDT {get;set;}

           /// <summary>
           /// Desc:登薄日期
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string RG_DT {get;set;}

           /// <summary>
           /// Desc:流水号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string SERIALNUMBER {get;set;}

    }
}
