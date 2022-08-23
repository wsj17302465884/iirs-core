using SqlSugar;
using System;
using System.Collections.Generic;

namespace IIRS.Models.ViewModel.IIRS
{
    public class ParcelVModel
    {
        #region
        /// <summary>
        /// 抵押类型:抵押、在建工程抵押、预告抵押
        /// </summary>
        public string DYLX { get; set; }

        public string DYSLBH { get; set; }

        public string SLBH { get; set; }

        /// <summary>
        /// 不动产证明号
        /// </summary>
        public string BDCZH { get; set; }

        public string Bdczmh { get; set; }

        /// <summary>
        /// 登记原因:债务履行期限发生变化,抵押登记,一般抵押权
        /// </summary>
        public string DJYY { get; set; }

        /// <summary>
        /// 登记种类
        /// </summary>
        public string DJZL { get; set; }

        /// <summary>
        /// 合同号
        /// </summary>
        public string HTH { get; set; }

        /// <summary>
        /// 左路
        /// </summary>
        public string ZL { get; set; }

        /// <summary>
        /// 抵押面积
        /// </summary>
        public decimal dyMJ { get; set; }

        /// <summary>
        /// 抵押方式
        /// </summary>
        public string DYFS { get; set; }

        /// <summary>
        /// 评估金额
        /// </summary>
        public decimal PGJE { get; set; }

        /// <summary>
        /// 被担保债权数额
        /// </summary>
        public int BDBZQSE { get; set; }

        /// <summary>
        /// 履行期限
        /// </summary>
        public int LXQX { get; set; }

        /// <summary>
        /// 抵押顺位
        /// </summary>
        public int DYSW { get; set; }

        /// <summary>
        /// 个人授权订单流水号
        /// </summary>           
        public string AUZ_ID { get; set; }

        /// <summary>
        /// 债务履行期限-起始日期
        /// </summary>
        public DateTime ZWLXQXQSRQ { get; set; }

        /// <summary>
        /// 债务履行期限-截止日期
        /// </summary>
        public DateTime ZWLXQXJZRQ { get; set; }

        /// <summary>
        /// 抵押联系人
        /// </summary>
        public string DYLXR { get; set; }

        /// <summary>
        /// 抵押联系人电话
        /// </summary>
        public string DYLXRDH { get; set; }

        /// <summary>
        /// 银行统一社会信用代码
        /// </summary>
        public string YHYTSHXYDM { get; set; }

        /// <summary>
        /// 银行统一社会信用代码权利人ID
        /// </summary>
        public string YHYTSHXYDM2 { get; set; }

        /// <summary>
        /// 银行部门编码
        /// </summary>
        public string BankDeptID { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string LXR { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string LXRDH { get; set; }

        /// <summary>
        /// 承诺时间
        /// </summary>
        public DateTime CNSJ { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string SJR { get; set; }

        /// <summary>
        /// 担保范围
        /// </summary>
        public string DBFW { get; set; }

        /// <summary>
        /// 抵押权人名称
        /// </summary>
        public string DYQRMC { get; set; }

        /// <summary>
        /// 抵押权人编号
        /// </summary>
        public string DYQRMC_ID { get; set; }

        
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string bdcdyh { get; set; }

        public bool Bdclx { get; set; }

        public string bid { get; set; }

        #endregion

        /// <summary>
        /// 抵押房屋
        /// </summary>
        public List<Dyfc_hVModel> selectHouseInfo { get; set; }

        /// <summary>
        /// 抵押房产信息表
        /// </summary>
        public class Dyfc_hVModel
        {
            /// <summary>
            /// 不动产证明号
            /// </summary>
            public string BDCZH { get; set; }

            public string certificatenumber { get; set; }

            /// <summary>
            /// 受理编号
            /// </summary>
            public string SLBH { get; set; }

            public string acceptancenumber { get; set; }

            /// <summary>
            /// 图属统一编码
            /// </summary>
            public string TSTYBM { get; set; }

            public string numberid { get; set; }

            /// <summary>
            /// 证书类型（房产证、土地证）
            /// </summary>
            public string ZSLX { get; set; }

            /// <summary>
            /// 权利人名称
            /// </summary>
            public string QLRMC { get; set; }

            public string rightname { get; set; }

            /// <summary>
            /// 坐落
            /// </summary>
            public string ZL { get; set; }

            public string address { get; set; }

            /// <summary>
            /// 抵押顺位
            /// </summary>
            public int SW { get; set; }

            /// <summary>
            /// 不动产单元号
            /// </summary>
            public string BDCDYH { get; set; }

            /// <summary>
            /// 不动产类型：房屋或土地
            /// </summary>
            public string BDCLX { get; set; }

            /// <summary>
            /// 预测建筑面积
            /// </summary>
            public decimal? jzmj { get; set; }
            /// <summary>
            /// 预测分摊面积
            /// </summary>
            public decimal? ftmj { get; set; }
            /// <summary>
            /// 预测套内面积
            /// </summary>
            public decimal? tnmj { get; set; }
            /// <summary>
            /// 单元号
            /// </summary>
            public string dyh { get; set; }
            /// <summary>
            /// 实际层
            /// </summary>
            public string sjc { get; set; }
            /// <summary>
            /// 名义层
            /// </summary>
            public string myc { get; set; }
            /// <summary>
            /// 幢号
            /// </summary>
            public string zh { get; set; }
            /// <summary>
            /// 房间号
            /// </summary>
            public string fjh { get; set; }
            /// <summary>
            /// 户编号
            /// </summary>
            public string hh { get; set; }

            /// <summary>
            /// 规划用途
            /// </summary>
            public string ghyt { get; set; }
            /// <summary>
            /// 权利类型
            /// </summary>
            public string qllx { get; set; }
            /// <summary>
            /// 权利性质
            /// </summary>
            public string qlxz { get; set; }
            public string bdclx { get; set; }
            public string tdqllx { get; set; }
            public string tdqlxz { get; set; }
            public decimal? tdmj { get; set; }
            public string bdcdyh { get; set; }
        }

        /// <summary>
        /// 抵押人信息
        /// </summary>
        public List<DyrVModel> selectDyPerson { get; set; }

        /// <summary>
        /// 抵押人信息表
        /// </summary>
        public class DyrVModel
        {
            /// <summary>
            /// 权利人名称
            /// </summary>
            public string QLRMC { get; set; }

            public string rightname { get; set; }

            /// <summary>
            /// 权利人ID
            /// </summary>
            public string QLRID { get; set; }

            /// <summary>
            /// 证件类别代码(身份证、护照、军官证等)
            /// </summary>
            public string ZJLB { get; set; }

            /// <summary>
            /// 证件类别中文名(身份证、护照、军官证等)
            /// </summary>
            public string ZJLB_ZWM { get; set; }

            /// <summary>
            /// 证件号码（身份证、营业执照）
            /// </summary>
            public string ZJHM { get; set; }

            

            /// <summary>
            /// 共有份额
            /// </summary>
            public string GYFE { get; set; }

            /// <summary>
            /// 电话
            /// </summary>
            public string DH { get; set; }
        }

        public List<DyZdVModel> selectZdMessage { get; set; }

        /// <summary>
        /// 抵押宗地信息
        /// </summary>
        public class DyZdVModel
        {
            /// <summary>
            /// 不动产证号
            /// </summary>
            public string BDCZH { get; set; }

            /// <summary>
            /// 受理编号
            /// </summary>
            public string SLBH { get; set; }

            /// <summary>
            /// 图属统一编码
            /// </summary>
            public string ZDTYBM { get; set; }

            /// <summary>
            /// 发证面积
            /// </summary>
            public decimal FZMJ { get; set; }

            /// <summary>
            /// 土地坐落
            /// </summary>
            public string TDZL { get; set; }

        }

        

        public class ZjdyVModel
        {
            /// <summary>
            /// 新受理编号
            /// </summary>
            public string NewSlbh { get; set; }

            /// <summary>
            /// 抵押宗地相关信息
            /// </summary>
            public List<DyZdVModel> zd { get; set; } = new List<DyZdVModel>();

            /// <summary>
            /// 抵押人相关信息
            /// </summary>
            public List<DyrVModel> person { get; set; } = new List<DyrVModel>();

            /// <summary>
            /// 抵押房屋相关信息
            /// </summary>
            public List<Dyfc_hVModel> house { get; set; } = new List<Dyfc_hVModel>();
        }
    }
}
