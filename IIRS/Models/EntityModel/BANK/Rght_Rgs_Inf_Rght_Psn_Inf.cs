using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.BANK
{
    public class Rght_Rgs_Inf_Rght_Psn_Inf
    {
        /// <summary>
        /// 权利登记信息权利人信息
        /// </summary>
        public Rght_Rgs_Inf_Rght_Psn_Inf()
        {

        }
        /// <summary>
        /// 不动产权证号
        /// </summary>
        public string RealEst_Wrnt_No { get; set; }
        /// <summary>
        /// 权属状态
        /// </summary>
        public string Own_St { get; set; }
        /// <summary>
        /// 权利类型
        /// </summary>
        public string Rght_Tp { get; set; }
        /// <summary>
        /// 权利人编号
        /// </summary>
        public string Rght_Psn_No { get; set; }
        /// <summary>
        /// 权利人名称
        /// </summary>
        public string Rght_Psn_Nm { get; set; }
        /// <summary>
        /// 权利人类型
        /// </summary>
        public string Rght_Psn_Tp { get; set; }
        /// <summary>
        /// 权利人类别
        /// </summary>
        public string Rght_Psn_Class { get; set; }
        /// <summary>
        /// 单位个人
        /// </summary>
        public string Unit_Or_Person { get; set; }
        /// <summary>
        /// 权利人证件类型
        /// </summary>
        public string Rght_Psn_Crdt_Tp { get; set; }
        /// <summary>
        /// 权利人证件号码
        /// </summary>
        public string Rght_Psn_Crdt_No { get; set; }
        /// <summary>
        /// 权利人联系电话
        /// </summary>
        public string Rght_Psn_Ctc_Tel { get; set; }
        /// <summary>
        /// 权利人联系地址
        /// </summary>
        public string Rght_Psn_Adr { get; set; }
        /// <summary>
        /// 权利登记附记
        /// </summary>
        public string Rght_Rgs_Atch { get; set; }
        /// <summary>
        /// 共有方式
        /// </summary>
        public string Com_Mod { get; set; }
        /// <summary>
        /// 权利比例
        /// </summary>
        public string Rght_Pctg { get; set; }
        /// <summary>
        /// 权利人性别
        /// </summary>
        public string Rght_Psn_Gnd { get; set; }
        /// <summary>
        /// 权利人单位性质
        /// </summary>
        public string Rght_Psn_Unit_Char { get; set; }
        /// <summary>
        /// 权利人国籍
        /// </summary>
        public string Rght_Psn_Nat { get; set; }
    }
}
