using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("BDCXGXX", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class BdcxgxxModel
    {
        public BdcxgxxModel()
        {


        }
        /// <summary>
        /// Desc:主键
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public Guid XID { get; set; }

        /// <summary>
        /// Desc:图属统一编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TSTYBM { get; set; }

        /// <summary>
        /// Desc:不动产证号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCZH { get; set; }

        /// <summary>
        /// Desc:登记簿受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SLBH { get; set; }

        /// <summary>
        /// Desc:坐落
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZL { get; set; }

        /// <summary>
        /// Desc:建筑面积
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? JZMJ { get; set; }

        /// <summary>
        /// Desc:共有方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GYFS { get; set; }

        /// <summary>
        /// Desc:房屋权利类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QLLX { get; set; }

        /// <summary>
        /// Desc:房屋权利性质
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QLXZ { get; set; }

        /// <summary>
        /// Desc:房屋建筑面积
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? FWXF_JZMJ { get; set; }

        /// <summary>
        /// Desc:土地权利类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TDQLLX { get; set; }

        /// <summary>
        /// Desc:土地权利性质
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TDQLXZ { get; set; }

        /// <summary>
        /// Desc:土地用途
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TDYT { get; set; }

        /// <summary>
        /// Desc:土地使用期限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? TDSYQX { get; set; }

        /// <summary>
        /// Desc:土地起始日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? TDQSRQ { get; set; }

        /// <summary>
        /// Desc:土地终止日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? TDZZRQ { get; set; }

        /// <summary>
        /// Desc:建筑宗地面积
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? JZZDMJ { get; set; }

        /// <summary>
        /// Desc:登记簿其他
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DJBQT { get; set; }

        /// <summary>
        /// Desc:登记簿登记日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? DJBDJRQ { get; set; }

        /// <summary>
        /// Desc:登记簿发证日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? DJBFZRQ { get; set; }

        /// <summary>
        /// Desc:登记簿证书序列号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DJBZSXLH { get; set; }

        /// <summary>
        /// Desc:登记簿附记
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DJBFJ { get; set; }

        /// <summary>
        /// Desc:登记簿发证机关
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DJBFZJG { get; set; }

        /// <summary>
        /// Desc:抵押不动产证明号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCZMH { get; set; }

        /// <summary>
        /// 抵押类型
        /// </summary>
        public string DYLX { get; set; }

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
        /// Desc:抵押方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYFS { get; set; }

        /// <summary>
        /// Desc:抵押期限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYQX { get; set; }

        /// <summary>
        /// Desc:抵押其他
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYQT { get; set; }

        /// <summary>
        /// Desc:抵押登记日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? DYDJRQ { get; set; }

        /// <summary>
        /// Desc:抵押起始时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? DYQSSJ { get; set; }

        /// <summary>
        /// Desc:抵押结束时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? DYJSSJ { get; set; }

        /// <summary>
        /// Desc:抵押发证日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? DYFZRQ { get; set; }

        /// <summary>
        /// Desc:抵押证书序列号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYZSXLH { get; set; }

        /// <summary>
        /// Desc:抵押发证机关
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYFZJG { get; set; }

        /// <summary>
        /// Desc:抵押合同号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYHTH { get; set; }

        /// <summary>
        /// Desc:抵押附记
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYFJ { get; set; }

        /// <summary>
        /// Desc:查封文号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CFWH { get; set; }

        /// <summary>
        /// Desc:查封机关
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CFJG { get; set; }

        /// <summary>
        /// Desc:查封期限
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CFQX { get; set; }

        /// <summary>
        /// Desc:查封日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CFRQ { get; set; }

        /// <summary>
        /// Desc:查封起始时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CFQSSJ { get; set; }

        /// <summary>
        /// Desc:查封结束时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? CFJSSJ { get; set; }

        /// <summary>
        /// Desc:查封附记
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string CFFJ { get; set; }

        /// <summary>
        /// Desc:预告申请日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? YGSQRQ { get; set; }

        /// <summary>
        /// Desc:预告审批单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YGSPDW { get; set; }

        /// <summary>
        /// Desc:预告审批日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? YGSPRQ { get; set; }

        /// <summary>
        /// Desc:预告不动产证明号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YGBDCZMH { get; set; }

        /// <summary>
        /// Desc:预告登记日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? YGDJRQ { get; set; }

        /// <summary>
        /// Desc:预告发证日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? YGFZRQ { get; set; }

        /// <summary>
        /// Desc:预告发证机关
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YGFZJG { get; set; }

        /// <summary>
        /// Desc:预告证书序列号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YGZSXLH { get; set; }

        /// <summary>
        /// Desc:预告其他
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YGQT { get; set; }

        /// <summary>
        /// Desc:权利人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QLRMC { get; set; }

        /// <summary>
        /// Desc:权利人类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJLB { get; set; }

        /// <summary>
        /// Desc:证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJHM { get; set; }

        /// <summary>
        /// Desc:义务人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YWR { get; set; }

        /// <summary>
        /// Desc:义务人证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YWR_ZJLB { get; set; }

        /// <summary>
        /// Desc:义务人证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YWR_ZJHM { get; set; }

        /// <summary>
        /// Desc:抵押人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYR { get; set; }

        /// <summary>
        /// Desc:抵押人证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYR_ZJLB { get; set; }

        /// <summary>
        /// Desc:抵押人证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYR_ZJHM { get; set; }

        /// <summary>
        /// Desc:抵押权人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYQR { get; set; }

        /// <summary>
        /// Desc:抵押权人证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYQR_ZJLB { get; set; }

        /// <summary>
        /// Desc:抵押权人证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYQR_ZJHM { get; set; }

        /// <summary>
        /// Desc:预告权利人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YG_QLRMC { get; set; }

        /// <summary>
        /// Desc:预告权利人证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YG_ZJLB { get; set; }

        /// <summary>
        /// Desc:预告权利人证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YG_ZJHM { get; set; }

        /// <summary>
        /// Desc:预告义务人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YG_YWR { get; set; }

        /// <summary>
        /// Desc:预告义务人证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YG_YWR_ZJLB { get; set; }

        /// <summary>
        /// Desc:预告义务人证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YG_YWR_ZJHM { get; set; }

        /// <summary>
        /// Desc:异议权利人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YY_QLRMC { get; set; }

        /// <summary>
        /// Desc:异议权利人证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YY_ZJLB { get; set; }

        /// <summary>
        /// Desc:异议权利人证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YY_ZJHM { get; set; }

        /// <summary>
        /// Desc:异议义务人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YY_YWR { get; set; }

        /// <summary>
        /// Desc:异议义务人证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YY_YWR_ZJLB { get; set; }

        /// <summary>
        /// Desc:异议义务人证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YY_YWR_ZJHM { get; set; }

        /// <summary>
        /// Desc:当前流程节点
        /// Default:
        /// Nullable:True
        /// </summary>           
        public short? FLOW_ID { get; set; }

        /// <summary>
        /// Desc:当前节点处理时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? FLOW_TIME { get; set; }

    }
}
