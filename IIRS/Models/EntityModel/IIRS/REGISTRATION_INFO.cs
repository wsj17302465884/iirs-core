using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("REGISTRATION_INFO", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class REGISTRATION_INFO
    {
        public REGISTRATION_INFO()
        {


        }

        /// <summary>
        /// 主键编号
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string XID { get; set; }

        /// <summary>
        /// 业务受理编号
        /// </summary>           
        public string YWSLBH { get;set;}

        /// <summary>
        /// 合同号
        /// </summary>           
        public string HTH { get; set; }

        /// <summary>
        /// 登记种类
        /// </summary>           
        public int DJZL { get; set; }

        /// <summary>
        /// 不动产证号
        /// </summary>           
        public string BDCZH { get; set; }

        /// <summary>
        /// 登记簿受理编号
        /// </summary>           
        public string SLBH { get;set;}

        /// <summary>
        /// 坐落
        /// </summary>           
        public string ZL { get; set; }

        /// <summary>
        /// 权利人名称
        /// </summary>           
        public string QLRMC { get; set; }

        /// <summary>
        /// 权利人类型
        /// </summary>           
        public string ZJLB { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>           
        public string ZJHM { get; set; }

        /// <summary>
        /// 办理单位组织机构
        /// </summary>           
        public string ORG_ID { get; set; }

        /// <summary>
        /// 系统登录经办人
        /// </summary>           
        public string USER_ID { get; set; }

        /// <summary>
        /// 系统登录经办人联系方式
        /// </summary>           
        public string TEL { get; set; }

        /// <summary>
        /// 个人授权订单流水号
        /// </summary>           
        public string AUZ_ID { get; set; }

        /// <summary>
        /// 备注1
        /// </summary>           
        public string REMARK1 { get; set; }

        /// <summary>
        /// 备注2
        /// </summary>           
        public string REMARK2 { get; set; }

        /// <summary>
        /// SJR:收件人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SJR { get; set; }

        /// <summary>
        /// 是否当前节点确认
        /// Default:
        /// 1::已确认，0:未确认
        /// </summary>           
        public int IS_ACTION_OK { get; set; } = 1;

        /// <summary>
        /// 下一现实手XID
        /// </summary>           
        public string NEXT_XID { get; set; }

        [SugarColumn(ColumnDataType = "clob")]
        public string SaveDataJson { get; set; }
        /// <summary>
        /// 办理日期
        /// </summary>
        public DateTime SAVEDATE { get; set; }


        /// <summary>
        /// 排队号
        /// </summary>
        public string PDH { get; set; }
    }
}
