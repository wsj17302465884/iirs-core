using System;
using System.Collections.Generic;
using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.Tax
{
    ///<summary>
    ///完税信息表
    ///</summary>
    [SugarTable("TAX_COMPLETION", SysConst.DB_CON_TAX)]
    public partial class TAX_COMPLETION
    {
        public TAX_COMPLETION()
        {


        }

        /// <summary>
        /// Desc:交易流水号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string TAX_CPK { get; set; }

        ///// <summary>
        ///// 受理编号
        ///// Default:
        ///// Nullable:False
        ///// </summary>           
        //public string SLBH { get; set; }

        /// <summary>
        /// 业务合同编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string HTBH { get; set; }

        /// <summary>
        /// 不动产单元号
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string BDCDYH { get; set; }

        /// <summary>
        /// 最终交易价格
        /// Default:
        /// Nullable:False
        /// </summary>           
        public decimal ZZJYJG { get; set; }

        /// <summary>
        /// 完税状态
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string WSZT { get; set; }

        /// <summary>
        /// 创建时间
        /// Default:
        /// Nullable:False
        /// </summary>           
        public DateTime CDATE { get; set; } = DateTime.Now;

        [SugarColumn(IsIgnore = true)]
        public List<TAX_COMPLETION_BUYER> buyers { get; set; }

        [SugarColumn(IsIgnore = true)]
        public List<TAX_COMPLETION_SELLER> sellers { get; set; }
    }
}
