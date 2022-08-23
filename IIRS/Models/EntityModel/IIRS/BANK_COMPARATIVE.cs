using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///银行名称对照表
    ///</summary>
    [SugarTable("BANK_COMPARATIVE", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class BANK_COMPARATIVE
    {
        ///<summary>
        ///银行名称对照表
        ///</summary>
        public BANK_COMPARATIVE()
        {


        }
        /// <summary>
        /// 编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string PK { get; set; }

        /// <summary>
        /// 银行编码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BANK_ID { get; set; }

        /// <summary>
        /// 对照名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string C_NAME { get; set; }
    }
}
