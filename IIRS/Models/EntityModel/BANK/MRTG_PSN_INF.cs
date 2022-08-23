using IIRS.Utilities.Common;
using SqlSugar;
using System;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("MRTG_PSN_INF", SysConst.DB_CON_BANK)]
    public partial class MRTG_PSN_INF
    {
        /// <summary>
        /// 抵押人信息
        /// </summary>
        public MRTG_PSN_INF()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public Guid MID { get; set; }

        /// <summary>
        /// Desc:抵押人信息ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DYRXX_ID { get; set; }

        /// <summary>
        /// Desc:抵押人名称
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string MRTG_PSN_NM { get; set; }

        /// <summary>
        /// Desc:抵押人类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string MRTG_PSN_TP { get; set; }

        /// <summary>
        /// Desc:抵押人证件类型
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string MRTG_PSN_CRDT_TP { get; set; }

        /// <summary>
        /// Desc:抵押人证件号码
        /// Default:
        /// Nullable:False
        /// </summary>           
        public string MRTG_PSN_CRDT_NO { get; set; }

        /// <summary>
        /// Desc:抵押人联系电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_PSN_CTC_TEL { get; set; }

        /// <summary>
        /// Desc:抵押人法定代表或负责人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_PSN_LGL_TBL_OR_PNP { get; set; }

        /// <summary>
        /// Desc:抵押人地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string MRTG_PSN_ADR { get; set; }

    }
}
