using System;

namespace IIRS.Models.ViewModel.WQ

{

    public class MessageResult
    {        
        public string code { get; set; }
        public WQResult data { get; set; }
        public string message { get; set; }
        
    }

    public class WQResult
    {
        /// <summary>
        /// Desc:业务编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string YWBH { get; set; }

        /// <summary>
        /// Desc:业务合同编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HTBH { get; set; }
        /// <summary>
        /// Desc:理论业务类别
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LLYWLB { get; set; }
        /// <summary>
        /// Desc:实际业务类别
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SJYWLB { get; set; }

        /// <summary>
        /// Desc:房屋编码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FWBM { get; set; }
        /// <summary>
        /// Desc:房屋状态
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FWZT { get; set; }
        /// <summary>
        /// Desc:行政区划代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string XZQHDM { get; set; }
        /// <summary>
        /// Desc:区县
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string QX { get; set; }
        /// <summary>
        /// Desc:乡镇/街道办
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string XZJDB { get; set; }
        /// <summary>
        /// Desc:路街巷
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LJX { get; set; }
        /// <summary>
        /// Desc:小区
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string XQ { get; set; }
        /// <summary>
        /// Desc:楼号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LH { get; set; }
        /// <summary>
        /// Desc:所在起始层
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SZQSC { get; set; }
        /// <summary>
        /// Desc:所在终止层
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SZZZC { get; set; }
        /// <summary>
        /// Desc:名义层
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string MYC { get; set; }
        /// <summary>
        /// Desc:单元
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DY { get; set; }
        /// <summary>
        /// Desc:房号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FH { get; set; }
        /// <summary>
        /// Desc:房屋坐落
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FWZL { get; set; }
        /// <summary>
        /// Desc:户型居室
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HXJS { get; set; }
        /// <summary>
        /// Desc:户型结构
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HXJG { get; set; }
        /// <summary>
        /// Desc:房屋朝向
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FWCX { get; set; }
        /// <summary>
        /// Desc:建筑面积
        /// Default:
        /// Nullable:False
        /// </summary>           
        public decimal JZMJ { get; set; }
        /// <summary>
        /// Desc:套内建筑面积
        /// Default:
        /// Nullable:False
        /// </summary>           
        public decimal TNJZMJ { get; set; }
        /// <summary>
        /// Desc:公摊建筑面积
        /// Default:
        /// Nullable:False
        /// </summary>           
        public decimal GTJZMJ { get; set; }
        /// <summary>
        /// Desc:建筑结构
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string JZJG { get; set; }
        /// <summary>
        /// Desc:房屋用途
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FWYT { get; set; }
        /// <summary>
        /// Desc:房屋性质
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FWXZ { get; set; }
        /// <summary>
        /// Desc:房屋性质BM
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FWXZBM { get; set; }
        /// <summary>
        /// Desc:共有方式
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GYFS { get; set; }
        /// <summary>
        /// Desc:成交金额
        /// Default:
        /// Nullable:False
        /// </summary>           
        public decimal CJJE { get; set; }
        /// <summary>
        /// Desc:单价
        /// Default:
        /// Nullable:False
        /// </summary>           
        public decimal DJ { get; set; }
        /// <summary>
        /// Desc:付款类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FKLX { get; set; }
        /// <summary>
        /// Desc:贷款方式
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DKFS { get; set; }
        /// <summary>
        /// Desc:合同生效日期
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime HTSXRQ { get; set; }
        /// <summary>
        /// Desc:业务办结时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime YWBJSJ { get; set; }
        /// <summary>
        /// Desc:业务办理所在行政区划代码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string YWBLSZXZQHDM { get; set; }
        /// <summary>
        /// Desc:交易者类别
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string JYZLB { get; set; }
        /// <summary>
        /// Desc:交易者全称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string JYZQC { get; set; }
        /// <summary>
        /// Desc:交易者证件名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string JYZZJMC { get; set; }
        /// <summary>
        /// Desc:交易者证件号码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string JYZZJHM { get; set; }
        /// <summary>
        /// Desc:交易者性质
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string JYZXZ { get; set; }
        /// <summary>
        /// Desc:交易者户籍行政区划
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string JYZHJXZQH { get; set; }
        /// <summary>
        /// Desc:国籍
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string GJ { get; set; }
        /// <summary>
        /// Desc:不动产单元号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string BDCDYH { get; set; }
        /// <summary>
        /// Desc:联系电话
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string LXDH { get; set; }
        /// <summary>
        /// Desc:地址
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string DZ { get; set; }
        /// <summary>
        /// Desc:房屋类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string FWLX { get; set; }
        /// <summary>
        /// Desc:所占份额
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SZFE { get; set; }
        /// <summary>
        /// Desc:交易者户籍
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string JYZHJ { get; set; }
        /// <summary>
        /// Desc:受让方名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string BUYERMC { get; set; }
        /// <summary>
        /// Desc:受让方证件号码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string BUYZJHM { get; set; }
        /// <summary>
        /// Desc:受让方性质
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string BUYERXZ { get; set; }
        /// <summary>
        /// Desc:受让方户籍性质区号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string BUYERHJXZQH { get; set; }
        /// <summary>
        /// Desc:受让方户籍
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string BUYERHJ { get; set; }
        /// <summary>
        /// Desc:出让人名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SELLERMC { get; set; }
        /// <summary>
        /// Desc:出让人证件号码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SELLERZJHM { get; set; }
        /// <summary>
        /// Desc:出让人性质
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SELLERXZ { get; set; }
        /// <summary>
        /// Desc:出让人户籍性质区号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SELLERHJXZQH { get; set; }
        /// <summary>
        /// Desc:出让人户籍
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string SELLERHJ { get; set; }
    }

    
}
