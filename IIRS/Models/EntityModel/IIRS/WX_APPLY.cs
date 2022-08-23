using System;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    /// <summary>
    /// 申请办理订单信息表
    /// </summary>
    [SugarTable("WX_APPLY", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class WX_APPLY
    {
        public WX_APPLY()
        {
        }

        /// <summary>
        /// 订单申请编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid ORDERY_ID { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string APPLY_NAME { get; set; }

        /// <summary>
        /// 申请人身份证号
        /// </summary>
        public string APPLY_ID_CARD { get; set; }

        /// <summary>
        /// 协办人姓名
        /// </summary>
        public string ORG_NAME { get; set; }

        /// <summary>
        /// 协办人身份证号
        /// </summary>
        public string ORG_ID_CARD { get; set; }

        /// <summary>
        /// 订单业务类型，1：转移登记申请
        /// </summary>
        public string BUS_TYPE { get; set; }

        /// <summary>
        /// 申请办理时间
        /// </summary>
        public DateTime APPLY_TIME { get; set; }

        /// <summary>
        /// 提交时间
        /// </summary>
        public DateTime SUBMIT_TIME { get; set; }

        /// <summary>
        /// 预留电话
        /// </summary>
        public string TEL { get; set; }

        /// <summary>
        /// 现场办理完结时间
        /// </summary>
        public DateTime FINISH_TIME { get; set; }

        /// <summary>
        /// 申请不动产证号
        /// </summary>
        public string BDCZH { get; set; }

        /// <summary>
        /// 图属统一编码
        /// </summary>
        public string TSYTBM { get; set; }

        /// <summary>
        /// 登记簿受理编号
        /// </summary>
        public string SLBH { get; set; }

        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; }

        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; }

        /// <summary>
        /// 权利人名称
        /// </summary>
        public string QLRMC { get; set; }

        /// <summary>
        /// 房屋面积
        /// </summary>
        public string FWMJ { get; set; }
    }
}
