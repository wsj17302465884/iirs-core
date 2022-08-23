using IIRS.Utilities.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.EntityModel.BDC
{
    [SugarTable("QL_SLXG", SysConst.DB_CON_BDC)]
    public class QL_SLXG
    {
        public QL_SLXG()
        {

        }
        [SugarColumn(IsPrimaryKey = true)]
        public string QLBH { get; set; }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string SLBH { get; set; }
        /// <summary>
        /// 权利类型
        /// </summary>
        public string QLLX { get; set; }
        /// <summary>
        /// 权利性质
        /// </summary>
        public string QLXZ { get; set; }
        /// <summary>
        /// 土地用途
        /// </summary>
        public string TDYT { get; set; }
        /// <summary>
        /// 发包方
        /// </summary>
        public string FBF { get; set; }
        /// <summary>
        /// 使用权（承包）面积
        /// </summary>
        public int SYQMJ { get; set; }
        /// <summary>
        /// 林地使用（承包）期限
        /// </summary>
        public string SYQX { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public DateTime? QSSJ { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? JSSJ { get; set; }
        /// <summary>
        /// 林地所有权性质
        /// </summary>
        public string LDSYQXZ { get; set; }
        /// <summary>
        /// 林木所有权人
        /// </summary>
        public string LMSYQR { get; set; }
        /// <summary>
        /// 林木使用权人
        /// </summary>
        public string LMSYQR2 { get; set; }
        /// <summary>
        /// 主要树种
        /// </summary>
        public string ZYSZ { get; set; }
        /// <summary>
        /// 株数
        /// </summary>
        public int ZS { get; set; }
        /// <summary>
        /// 林种
        /// </summary>
        public string LZ { get; set; }
        /// <summary>
        /// 起源
        /// </summary>
        public string QY { get; set; }
        /// <summary>
        /// 造林年度
        /// </summary>
        public int ZLND { get; set; }
        /// <summary>
        /// 小地名
        /// </summary>
        public string XDM { get; set; }
        /// <summary>
        /// 林班
        /// </summary>
        public string LB { get; set; }
        /// <summary>
        /// 小班
        /// </summary>
        public string XB { get; set; }
        /// <summary>
        /// 林木权利类型
        /// </summary>
        public string LMQLLX { get; set; }

    }
}
