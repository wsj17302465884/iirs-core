using IIRS.Utilities.Common;
using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.Tax
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("TAX_INCREMENT_HOME", SysConst.DB_CON_TAX)]
    public partial class TAX_INCREMENT_HOME
    {
           public TAX_INCREMENT_HOME(){


           }
           /// <summary>
           /// Desc:交易流水号
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public string TAX_PK {get;set;}

           /// <summary>
           /// Desc:业务合同编号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string HTBH {get;set;}

           /// <summary>
           /// Desc:地址
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string DZ {get;set;}

           /// <summary>
           /// Desc:行政区域代码
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string XQ_SWJG_DM {get;set;}

           /// <summary>
           /// Desc:权属转移对象
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string QSQSZYDX_DM {get;set;}

           /// <summary>
           /// Desc:权属转移用途
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string QSQSZYYT_DM {get;set;}

           /// <summary>
           /// Desc:权属转移方式
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string QSQSZYLB_DM {get;set;}

           /// <summary>
           /// Desc:楼号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string FDC_LH {get;set;}

           /// <summary>
           /// Desc:单元
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DY {get;set;}

           /// <summary>
           /// Desc:楼层
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string LC {get;set;}

           /// <summary>
           /// Desc:房间号
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string FH {get;set;}

           /// <summary>
           /// Desc:结构
           /// Default:
           /// Nullable:False
           /// </summary>           
           public string JZJGLX_DM {get;set;}

           /// <summary>
           /// Desc:房屋朝向代码
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string FWCX_DM {get;set;}

           /// <summary>
           /// Desc:套内面积
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal TNMJ {get;set;}

           /// <summary>
           /// Desc:建筑面积
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? FWJZMJ {get;set;}

           /// <summary>
           /// Desc:单价
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? DJ {get;set;}

           /// <summary>
           /// Desc:交易价格
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? JYJG {get;set;}

           /// <summary>
           /// Desc:合同金额
           /// Default:
           /// Nullable:False
           /// </summary>           
           public decimal HTJE {get;set;}

           /// <summary>
           /// Desc:合同签订时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? HTQDSJ {get;set;}

           /// <summary>
           /// Desc:当期应收款金额
           /// Default:
           /// Nullable:True
           /// </summary>           
           public decimal? DQYSKJE {get;set;}

           /// <summary>
           /// Desc:当期应收税款所属月
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string DQYSSKSSYF {get;set;}

           /// <summary>
           /// Desc:不动产单元号
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string BDCDYH {get;set;}

           /// <summary>
           /// Desc:创建日期
           /// Default:
           /// Nullable:False
           /// </summary>           
           public DateTime CDATE {get;set;}

           /// <summary>
           /// Desc:发送报税时间
           /// Default:
           /// Nullable:True
           /// </summary>           
           public DateTime? TAX_TIME {get;set;}

           /// <summary>
           /// Desc:是否已报税,0,：未报税，1：已报税
           /// Default:
           /// Nullable:False
           /// </summary>           
           public short IS_TAX {get;set;}

           /// <summary>
           /// Desc:状态,0,：未发送，1：待发送，2：待确认
           /// Default:
           /// Nullable:False
           /// </summary>           
           public short STATE {get;set;}

    }
}
