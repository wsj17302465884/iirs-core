using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DY_INFO", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class DY_INFO
    {
        public DY_INFO()
        {


        }

        /// <summary>
        /// 登记信息主键
        /// </summary>
        public string XID { get; set; }

        [SugarColumn(IsPrimaryKey = true)]
        public string SLBH { get; set; }

        /// <summary>
        /// 担保范围
        /// </summary>
        public string DBFW { get; set; }
        /// <summary>
        /// 是否存在禁止或限制转让抵押不动产的约定
        /// </summary>     
        /// 
        public string SFCZYD { get; set; }
        /// <summary>
        /// 是否存在禁止或限制转让抵押不动产的约定
        /// </summary>     
        /// 
        public string QRZYQK { get; set; }
        /// <summary>
        /// 抵押人类型
        /// </summary>     
        /// 
        public string DYRLX { get; set; }
        /// <summary>
        /// 业务受理编号
        /// Default:
        /// Nullable:True
        /// </summary>     
        /// 
        public string YWSLBH { get; set; }

        /// <summary>
        /// 业务受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DJLX { get; set; }

        /// <summary>
        /// Desc:登记原因
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DJYY { get; set; }

        /// <summary>
        /// Desc:抵押类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYLX { get; set; }

        /// <summary>
        /// Desc:抵押类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HTH { get; set; }

        /// <summary>
        /// Desc:相关不动产证/证明号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XGZH { get; set; }

        /// <summary>
        /// Desc:申请日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? SQRQ { get; set; }

        /// <summary>
        /// Desc:申请登记内容
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SQDJNR { get; set; }

        /// <summary>
        /// Desc:代理机构名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DLJGMC { get; set; }

        /// <summary>
        /// Desc:代理人姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DLRXM { get; set; }

        /// <summary>
        /// Desc:代理人证件类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DLRZJLX { get; set; }

        /// <summary>
        /// Desc:代理人证件号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DLRZJH { get; set; }

        /// <summary>
        /// Desc:代理人职业资格证号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DLRZGZH { get; set; }

        /// <summary>
        /// Desc:代理人电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DLRDH { get; set; }

        /// <summary>
        /// Desc:抵押顺位
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? DYSW { get; set; }

        /// <summary>
        /// Desc:债务人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZWR { get; set; }

        /// <summary>
        /// Desc:债务人证件类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZWRZJLX { get; set; }

        /// <summary>
        /// Desc:债务人证件号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZWRZJH { get; set; }

        /// <summary>
        /// Desc:抵押方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYFS { get; set; }

        /// <summary>
        /// Desc:抵押面积
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? DYMJ { get; set; }

        /// <summary>
        /// Desc:抵押面积2
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? DYMJ2 { get; set; }

        /// <summary>
        /// Desc:被担保主债权数额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? BDBZZQSE { get; set; }

        /// <summary>
        /// Desc:不动产评估价值/金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PGJE { get; set; }

        /// <summary>
        /// Desc:最高债权额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? ZGZQSE { get; set; }

        /// <summary>
        /// Desc:抵押期限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYQX { get; set; }

        /// <summary>
        /// Desc:权利起始时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? QLQSSJ { get; set; }

        /// <summary>
        /// Desc:权利结束时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? QLJSSJ { get; set; }

        /// <summary>
        /// Desc:审批单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPDW { get; set; }

        /// <summary>
        /// Desc:审批备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPBZ { get; set; }

        /// <summary>
        /// Desc:抵押不动产证明号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCZMH { get; set; }

        /// <summary>
        /// Desc:省市简称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SSJC { get; set; }

        /// <summary>
        /// Desc:机构简称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JGJC { get; set; }

        /// <summary>
        /// Desc:发证年度
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FZND { get; set; }

        /// <summary>
        /// Desc:证书号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZSH { get; set; }

        /// <summary>
        /// Desc:缮证人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SZR { get; set; }

        /// <summary>
        /// Desc:登记日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? DJRQ { get; set; }

        /// <summary>
        /// Desc:登簿人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DBR { get; set; }

        /// <summary>
        /// Desc:发证机关
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FZJG { get; set; }

        /// <summary>
        /// Desc:发证日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? FZRQ { get; set; }

        /// <summary>
        /// Desc:证书序列号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZSXLH { get; set; }

        /// <summary>
        /// Desc:归档号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GDH { get; set; }

        /// <summary>
        /// Desc:档案密级
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DAMJ { get; set; }

        /// <summary>
        /// Desc:其他
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QT { get; set; }

        /// <summary>
        /// Desc:附记
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FJ { get; set; }

        /// <summary>
        /// Desc:不动产单元号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCDYH { get; set; }

        /// <summary>
        /// Desc:现实\历史状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? LIFECYCLE { get; set; }

        /// <summary>
        /// Desc:金额币种
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JEBZ { get; set; }

        /// <summary>
        /// Desc:联系人
        /// Default:
        /// Nullable:True
        /// </summary> 
        public string LXR { get; set; }

        /// <summary>
        /// Desc:联系人电话
        /// Default:
        /// Nullable:True
        /// </summary> 
        public string LXRDH { get; set; }

        /// <summary>
        /// 承诺时间
        /// Default:
        /// Nullable:True
        /// </summary> 
        public DateTime CNSJ { get; set; }

        /// <summary>
        /// 收件人
        /// Default:
        /// Nullable:True
        /// </summary> 
        public string SJR { get; set; }

        /// <summary>
        /// 坐落（多个时加等）
        /// Default:
        /// Nullable:True
        /// </summary> 
        public string ZLXX { get; set; }

        /// <summary>
        /// 所在区域
        /// </summary>
        public string SZQY { get; set; }
    }

}
