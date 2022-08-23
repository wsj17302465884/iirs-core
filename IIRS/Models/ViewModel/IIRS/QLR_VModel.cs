using SqlSugar;
using System;

namespace IIRS.Models.ViewModel
{
    public class QLR_VModel
    {
        /// <summary>
        /// 分页编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string RN { get; set; }

        /// <summary>
        /// Desc:权利人ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string QLRID { get; set; }

        /// <summary>
        /// Desc:权利人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QLRMC { get; set; }

        /// <summary>
        /// Desc:顺序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? SXH { get; set; }

        /// <summary>
        /// Desc:证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJLB { get; set; }

        /// <summary>
        /// Desc:证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJLB_ZWM { get; set; }

        /// <summary>
        /// Desc:证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJHM { get; set; }

        /// <summary>
        /// Desc:电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DH { get; set; }

        /// <summary>
        /// 共有方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GYFS { get; set; }

        /// <summary>
        /// 共有份额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GYFE { get; set; }

        /// <summary>
        /// 是否权属人
        /// 0:否,1:是
        /// </summary>           
        public int IS_OWNER { get; set; }

        /// <summary>
        /// 是否已认证
        /// 0:未认证,1:已认证
        /// </summary>           
        public int ISCERTIFIED { get; set; }
    }
}
