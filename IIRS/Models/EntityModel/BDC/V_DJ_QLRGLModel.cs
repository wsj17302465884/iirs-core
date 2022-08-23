using IIRS.Utilities.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.BDC
{
    [SugarTable("V_DJ_QLRGL", SysConst.DB_CON_BDC)]
    public class V_DJ_QLRGLModel
    {
        public V_DJ_QLRGLModel()
        {

        }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string SLBH { get; set; }
        /// <summary>
        /// 共有方式
        /// </summary>
        public string GYFS { get; set; }

        /// <summary>
        /// 权利人类型
        /// </summary>
        public string QLRLX { get; set; }


        /// <summary>
        /// 权利人名称
        /// </summary>
        public string QLRMC { get; set; }
    

        /// <summary>
        /// 证件号码
        /// </summary>
        public string ZJHM { get; set; }
   
    }
}
