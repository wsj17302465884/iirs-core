using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.IIRS
{    ///<summary>
     ///
     ///</summary>
    [SugarTable("FACE_RECOGNITION_AUTHORIZE", Utilities.Common.SysConst.DB_CON_IIRS)]
    public class FACE_RECOGNITION_AUTHORIZE
    {
        public FACE_RECOGNITION_AUTHORIZE()
        {

        }
        /// <summary>
        /// 登记信息主键
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public Guid PK { get; set; }
        /// <summary>
        /// Desc:授权人编号
        /// Default:
        /// Nullable:True
        /// </summary>      
        public Guid AUTHORIZE_ID { get; set; }
        /// <summary>
        /// Desc:授权说明
        /// Default:
        /// Nullable:True
        /// </summary>      
        public string AUTHORIZE_REMARK { get; set; }
        /// <summary>
        /// Desc:授权时间
        /// Default:
        /// Nullable:True
        /// </summary>      
        public DateTime AUTHORIZE_TIME { get; set; }
        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:True
        /// </summary>      
        public string SLBH { get; set; }
        /// <summary>
        /// Desc:被授权交易人名称
        /// Default:
        /// Nullable:True
        /// </summary>      
        public string AUTHORIZE_USER { get; set; }
        /// <summary>
        /// Desc:交易人证件号码
        /// Default:
        /// Nullable:True
        /// </summary>      
        public string AUTHORIZE_USER_ID { get; set; }


    }
}
