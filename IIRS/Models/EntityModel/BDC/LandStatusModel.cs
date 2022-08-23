using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.BDC
{
    //192.168.57.128
    [SugarTable("V_LAND_STATUS_QUERY", Utilities.Common.SysConst.DB_CON_BDC)]

    //200.200.1.131
    //[SugarTable("V_LAND_STATUS_QUERY", "LYSXK")]
    public class LandStatusModel
    {
        public LandStatusModel()
        {
            
        }

        /// <summary>
        /// 户图属统一编码
        /// </summary>
        public string H_tstybm { get; set; }
        /// <summary>
        /// 宗地图属统一编码
        /// </summary>
        public string Zd_tstybm { get; set; }
        
        /// <summary>
        /// 登记种类
        /// </summary>
        public string Djzl { get; set; }
        /// <summary>
        /// 土地不动产证号
        /// </summary>
        public string Bdczh { get; set; }

        /// <summary>
        /// 土地不动产单元号
        /// </summary>
        public string Bdcdyh { get; set; }
        /// <summary>
        /// 抵押不动产证明号
        /// </summary>
        public string Dy_bdczmh { get; set; }
        /// <summary>
        /// 预告不动产证明号
        /// </summary>
        public string Yg_bdczmh { get; set; }
        /// <summary>
        /// 异议不动产证明号
        /// </summary>
        public string Yy_bdczmh { get; set; }
        /// <summary>
        /// 查封文号
        /// </summary>
        public string Cfwh { get; set; }
    }
}
