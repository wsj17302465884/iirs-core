using IIRS.Utilities.Common;
using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("MRTG_REALEST_UNIT_INF", SysConst.DB_CON_BANK)]
    public partial class MRTG_REALEST_UNIT_INF
    {
           public MRTG_REALEST_UNIT_INF(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public Guid DID {get;set;}

           /// <summary>
           /// Desc:不动产所属省份代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PROVINCE_CD {get;set;}

           /// <summary>
           /// Desc:不动产所属城市代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string CITY_CD {get;set;}

           /// <summary>
           /// Desc:不动产所属区县代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DISTRICT_CD {get;set;}

           /// <summary>
           /// Desc:不动产签发权证类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REALEST_SGIS_WRNT_TP {get;set;}

           /// <summary>
           /// Desc:不动产登记证明号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REALEST_RGSCTF_NO {get;set;}

           /// <summary>
           /// Desc:不动产单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REALEST_UNIT_NO {get;set;}

           /// <summary>
           /// Desc:产权证号类型
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string OWN_CRTFNO_TP {get;set;}

           /// <summary>
           /// Desc:不动产权证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REALEST_WRNT_NO {get;set;}

           /// <summary>
           /// Desc:不动产坐落
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REALEST_LO {get;set;}

           /// <summary>
           /// Desc:不动产面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public double REALEST_AREA {get;set;}

           /// <summary>
           /// Desc:不动产价值
           /// Default:
           /// Nullable:True
           /// </summary>           
           public double REALEST_VAL {get;set;}

           /// <summary>
           /// Desc:房屋用途
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HS_USE {get;set;}

           /// <summary>
           /// Desc:用地面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LND_AREA {get;set;}

           /// <summary>
           /// Desc:土地用途
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string LAND_USE {get;set;}

           /// <summary>
           /// Desc:附记
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string HS_ATCH {get;set;}

           /// <summary>
           /// Desc:抵押人信息ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DYRXX_ID {get;set;}

           /// <summary>
           /// Desc:抵押不动产单元ID
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DYBDCDY_ID {get;set;}

           /// <summary>
           /// Desc:国有土地使用证号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string NAL_LAND_US_CRTFNO {get;set;}

           /// <summary>
           /// Desc:是否包含车库
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IF_CNTN_GRGE {get;set;}

           /// <summary>
           /// Desc:是否包含阁楼
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string IF_CNTN_LOFT {get;set;}

           /// <summary>
           /// Desc:权利份额
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string WGHT_LOT {get;set;}

           /// <summary>
           /// Desc:不动产所属区域
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string REALEST_BLNG_RGON {get;set;}

    }
}
