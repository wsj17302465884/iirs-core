using System;
using System.Linq;
using System.Text;
using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DJ_DY", SysConst.DB_CON_BDC)]
    public partial class DJ_DY
    {
        public DJ_DY()
        {


        }
        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string SLBH { get; set; }

        /// <summary>
        /// 是否存在禁止或限制转让抵押不动产的约定
        /// </summary>     
        /// 
        public string QRZYQK { get; set; }

        /// <summary>
        /// Desc:登记类型
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
        /// Desc:申请登记依据
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SQDJYJ { get; set; }

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
        /// Desc:代理机构名称2
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DLJGMC2 { get; set; }

        /// <summary>
        /// Desc:代理人姓名2
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DLRXM2 { get; set; }

        /// <summary>
        /// Desc:代理人证件类型2
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DLRZJLX2 { get; set; }

        /// <summary>
        /// Desc:代理人证件号2
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DLRZJH2 { get; set; }

        /// <summary>
        /// Desc:代理人职业资格证号2
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DLRZGZH2 { get; set; }

        /// <summary>
        /// Desc:代理人电话2
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DLRDH2 { get; set; }

        /// <summary>
        /// Desc:抵押类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYLX { get; set; }

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
        /// Desc:在建建筑物坐落
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJJZWZL { get; set; }

        /// <summary>
        /// Desc:在建建筑物抵押范围
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJJZWDYFW { get; set; }

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
        /// Desc:担保范围
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DBFW { get; set; }

        /// <summary>
        /// Desc:最高债权确定事实
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZGZQQDSS { get; set; }

        /// <summary>
        /// Desc:最高债权数额
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
        /// Desc:终审人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZSR { get; set; }

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
        /// Desc:打印次数
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? DYCS { get; set; }

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
        /// Desc:核定批次号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HDPCH { get; set; }

        /// <summary>
        /// Desc:缮证批次号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SZPCH { get; set; }

        /// <summary>
        /// Desc:抵押不动产单元(0否、1是)
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? DYBDCDY { get; set; }

        /// <summary>
        /// Desc:现实\历史状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? LIFECYCLE { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string 说明 { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XGZH1 { get; set; }

        /// <summary>
        /// Desc:位置
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string WZ { get; set; }

        /// <summary>
        /// Desc:预告登记种类
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YGDJZL { get; set; }

        /// <summary>
        /// Desc:电子证书序列号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DZZSXLH { get; set; }

        /// <summary>
        /// Desc:金额币种
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JEBZ { get; set; }

        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYDJCS { get; set; }

        /// <summary>
        /// Desc:区分区域
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QFQY { get; set; }

        /// <summary>
        /// Desc:合同号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HTH { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string tstybm { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string zdtybm { get; set; }

    }
}
