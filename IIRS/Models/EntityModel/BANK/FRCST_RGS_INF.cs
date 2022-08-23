using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BANK
{
    ///<summary>
    ///预告登记信息
    ///</summary>
    [SugarTable("FRCST_RGS_INF", SysConst.DB_CON_BANK)]
    public partial class FRCST_RGS_INF
    {
        /// <summary>
        /// 预告登记信息
        /// </summary>
        public FRCST_RGS_INF()
        {


        }
        /// <summary>
        /// Desc:
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string YID { get; set; }

        /// <summary>
        /// Desc:预告登记信息ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YGDJXX_ID { get; set; }

        /// <summary>
        /// Desc:预告不动产登记关联证明号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FRCST_REALEST_RGS_RLTV_CTFN_NO { get; set; }

        /// <summary>
        /// Desc:义务人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DTY_PSN_NM { get; set; }

        /// <summary>
        /// Desc:预告登记记载日期时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FRCST_RGS_DT_TM { get; set; }

        /// <summary>
        /// Desc:预告登记权利人信息ID
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YGDJQLRXX_ID { get; set; }

    }
}
