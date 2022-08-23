using System;
using System.Collections.Generic;

namespace IIRS.Models.ViewModel
{
    /// <summary>
    /// 抵押信息表
    /// </summary>
    public class DYVModel
    {
        /// <summary>
        /// 当前登录用户token
        /// </summary>
        public string userToken { get; set; }


        /// <summary>
        /// 受理编号
        /// </summary>
        public string SLBH { get; set; }

        /// <summary>
        /// 抵押表抵押类型：抵押、在建工程抵押、预告抵押
        /// </summary>
        public string DYLX { get; set; }

        /// <summary>
        /// 登记大类
        /// </summary>
        public string DJDL { get; set; }

        /// <summary>
        /// 相关证明号信息
        /// </summary>
        public List<XgzhVModel> XgzhModel { get; set; }

        /// <summary>
        /// 登记小类
        /// </summary>
        public string DJXL { get; set; }

        /// <summary>
        /// 登记原因
        /// </summary>
        public string DJYY { get; set; }

        /// <summary>
        /// 承诺时间
        /// </summary>
        public DateTime CNSJ { get; set; }

        ///// <summary>
        ///// 受理科室
        ///// </summary>
        //public string SLKS { get; set; } = "辽阳市不动产登记中心";

        /// <summary>
        /// 不动产所在区域
        /// </summary>
        public string BDCSZQY { get; set; }

        /// <summary>
        /// 通知人姓名
        /// </summary>
        public string TZRXM { get; set; }

        /// <summary>
        /// 移动电话
        /// </summary>
        public string YDDH { get; set; }

        /// <summary>
        /// 电子邮寄
        /// </summary>
        public string DZYJ { get; set; }

        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; }

        /// <summary>
        /// 收件备注
        /// </summary>
        public string SZBZ { get; set; }

        /// <summary>
        /// 不动产单元坐落
        /// </summary>
        public string BDCDYZL { get; set; }

        /// <summary>
        /// 抵押面积
        /// </summary>
        public decimal DYMJ { get; set; }

        /// <summary>
        /// 抵押方式：一般抵押
        /// </summary>
        public string DYFS { get; set; }

        /// <summary>
        /// 不动产价值
        /// </summary>
        public decimal BDCJZ { get; set; }

        /// <summary>
        /// 担保范围
        /// </summary>
        public string DBFW { get; set; }

        /// <summary>
        /// 被担保主债权数额
        /// </summary>
        public decimal BDBZZQSE { get; set; }

        /// <summary>
        /// 抵押顺位
        /// </summary>
        public int DYSW { get; set; }

        /// <summary>
        /// 债权履行起始期限
        /// </summary>
        public DateTime ZQLXQX_KS { get; set; }

        /// <summary>
        /// 债权履行截止期限
        /// </summary>
        public DateTime ZQLXQX_JZ { get; set; }

        /// <summary>
        /// 最高债权确定事实
        /// </summary>
        public string ZGZQQDSS { get; set; }

        /// <summary>
        /// 申请登记备注
        /// </summary>
        public string SQDJBZ { get; set; }

        /// <summary>
        /// 附记
        /// </summary>
        public string FJ { get; set; }
    }

    public class XgzhVModel
    {
        /// <summary>
        /// 相关证号
        /// </summary>
        public string XGZH { get; set; }

        /// <summary>
        /// 相关证类型
        /// </summary>
        public string XGZLX { get; set; }

        /// <summary>
        /// 权利人名称
        /// </summary>
        public string QLRMC { get; set; }

        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; }
    }
}