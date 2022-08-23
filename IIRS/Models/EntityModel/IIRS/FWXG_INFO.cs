using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///房屋相关权利
    ///</summary>
    [SugarTable("FWXG_INFO", Utilities.Common.SysConst.DB_CON_IIRS)]
    public partial class FWXG_INFO
    {
        ///<summary>
        ///房屋相关权利
        ///</summary>
        public FWXG_INFO()
        {


        }
        /// <summary>
        /// Desc:权利编号
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string QLBH { get; set; }

        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SLBH { get; set; }

        /// <summary>
        /// Desc:权利类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QLLX { get; set; }

        /// <summary>
        /// Desc:权利性质
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QLXZ { get; set; }

        /// <summary>
        /// Desc:建筑面积
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? JZMJ { get; set; }

        /// <summary>
        /// Desc:套内建筑面积
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? TNJZMJ { get; set; }

        /// <summary>
        /// Desc:分摊建筑面积
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? FTJZMJ { get; set; }

        /// <summary>
        /// Desc:取得方式
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QDFS { get; set; }

        /// <summary>
        /// Desc:取得价格
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? QDJG { get; set; }

        /// <summary>
        /// Desc:建筑面积描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JZMJMS { get; set; }

        /// <summary>
        /// Desc:规划用途描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GHYTMS { get; set; }

        /// <summary>
        /// Desc:其他内容描述
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QTNRMS { get; set; }

        /// <summary>
        /// Desc:小于2.2米储藏间面积
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? XCCJMJ { get; set; }

        /// <summary>
        /// Desc:高于2.2米储藏间面积
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? GCCJMJ { get; set; }

        /// <summary>
        /// Desc:违章年份
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string WZNF { get; set; }

        /// <summary>
        /// Desc:违章建筑面积
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? WZJZMJ { get; set; }

        /// <summary>
        /// Desc:规划用途
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GHYT { get; set; }

        /// <summary>
        /// Desc:评估金额
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? PGJE { get; set; }

        /// <summary>
        /// Desc:建筑物类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JZWLX { get; set; }

        /// <summary>
        /// Desc:构筑物类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GZWLX { get; set; }

        /// <summary>
        /// Desc:最后保存时间
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? SAVE { get; set; }
        /// <summary>
        /// 登记信息主键
        /// </summary>
        public string xid { get; set; }

    }
}
