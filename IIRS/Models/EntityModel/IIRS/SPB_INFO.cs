using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///审批表
    ///</summary>
    [SugarTable("SPB_INFO", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class SPB_INFO
    {
        ///<summary>
        ///审批表
        ///</summary>
        public SPB_INFO()
        {


        }
        /// <summary>
        /// Desc:审批编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string SPBH { get; set; }

        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SLBH { get; set; }

        /// <summary>
        /// Desc:审批对象
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPDX { get; set; }

        /// <summary>
        /// Desc:审批序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? SPXH { get; set; }

        /// <summary>
        /// Desc:审批意见
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPYJ { get; set; }

        /// <summary>
        /// Desc:审批人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPR { get; set; }

        /// <summary>
        /// Desc:审批日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? SPRQ { get; set; }

        /// <summary>
        /// Desc:审批人资格证号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPRZGZH { get; set; }

        /// <summary>
        /// Desc:审批结果
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPJG { get; set; }

        /// <summary>
        /// Desc:审批状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPZT { get; set; }

        /// <summary>
        /// Desc:审批意见填写人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SPTXR { get; set; }
        /// <summary>
        /// 登记信息主键
        /// </summary>
        public string XID { get; set; }

        /// <summary>
        /// 审批备注
        /// 【冗余列】在各个业务表中也保存
        /// </summary>           
        public string SPBZ { get; set; }

    }
}
