using IIRS.Utilities.Common;
using SqlSugar;
using System;
using System.ComponentModel;

namespace IIRS.Models.EntityModel.BDC
{
    /// <summary>
    /// 不动产证明号查询实体类
    /// </summary>
    [SugarTable("V_CX_DY", SysConst.DB_CON_BDC)]
    public partial class V_CX_DyVModel
    {
        public V_CX_DyVModel()
        {


        }
        
        /// <summary>
        /// 受理编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public string SLBH { get; set; }

        /// <summary>
        /// 省市简称
        /// </summary>
        public string SSJC { get; set; }

        /// <summary>
        /// 发证年度
        /// </summary>
        public string FZND { get; set; }

        /// <summary>
        /// 机构简称
        /// </summary>
        public string JGJC { get; set; }

        /// <summary>
        /// 证书号
        /// </summary>
        public string ZSH { get; set; }

        /// <summary>
        /// 证明权利或事项
        /// </summary>
        public string LX { get; set; }

        /// <summary>
        /// 权利人名称
        /// </summary>
        public string DYQRRMC { get; set; }

        /// <summary>
        /// 义务人名称
        /// </summary>
        public string DYRMC { get; set; }

        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; }

        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; }

        /// <summary>
        /// 个数
        /// </summary>
        public int GS { get; set; }

        /// <summary>
        /// 不动产单元号总数
        /// </summary>
        public string ZSBDCDYH { get; set; }

        /// <summary>
        /// 其他
        /// </summary>
        public string QT { get; set; }

        /// <summary>
        /// 附记
        /// </summary>
        public string FJ { get; set; }

        /// <summary>
        /// 登记日期
        /// </summary>
        public DateTime DJRQ { get; set; }

        /// <summary>
        /// 证书序列号
        /// </summary>
        public string ZSXLH { get; set; }
    }
}
