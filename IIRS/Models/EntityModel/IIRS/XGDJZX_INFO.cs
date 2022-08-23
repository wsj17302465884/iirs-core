using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("XGDJZX_INFO")]
    public partial class XGDJZX_INFO
    {
        public XGDJZX_INFO()
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
        /// Desc:相关证号
        /// Default:
        /// Nullable:True
        /// </summary>
        public string XGZH { get; set; }

        /// <summary>
        /// Desc:不动产最小单元号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BDCDYH { get; set; }

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
        /// Desc:相关文件
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XGWJ { get; set; }

        /// <summary>
        /// Desc:相关文号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XGWH { get; set; }

        /// <summary>
        /// Desc:申请人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SQR { get; set; }

        /// <summary>
        /// Desc:申请日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? SQRQ { get; set; }

        /// <summary>
        /// Desc:申请内容
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SQNR { get; set; }

        /// <summary>
        /// Desc:申请备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SQBZ { get; set; }

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
        /// Desc:审批人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPR { get; set; }

        /// <summary>
        /// Desc:审批单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPDW { get; set; }

        /// <summary>
        /// Desc:审批日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? SPRQ { get; set; }

        /// <summary>
        /// Desc:审批备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPBZ { get; set; }

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
        /// Desc:归档号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GDH { get; set; }

        /// <summary>
        /// Desc:注销凭证号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZXPZH { get; set; }

        /// <summary>
        /// SJR:收件人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SJR { get; set; }

        /// <summary>
        /// 登记信息主键
        /// </summary>
        public string XID { get; set; }
    }
}
