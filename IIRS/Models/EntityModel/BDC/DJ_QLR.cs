using System;
using System.Linq;
using System.Text;
using IIRS.Utilities.Common;
using SqlSugar;

namespace IIRS.Models.EntityModel.BDC
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("DJ_QLR", SysConst.DB_CON_BDC)]
    public partial class DJ_QLR
    {
        public DJ_QLR()
        {


        }
        /// <summary>
        /// Desc:权利人ID
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        public string QLRID { get; set; }

        /// <summary>
        /// Desc:权利人名称
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QLRMC { get; set; }

        /// <summary>
        /// Desc:顺序号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? SXH { get; set; }

        /// <summary>
        /// Desc:证件类别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJLB { get; set; }

        /// <summary>
        /// Desc:证件号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string ZJHM { get; set; }

        /// <summary>
        /// Desc:发证机关
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FZJG { get; set; }

        /// <summary>
        /// Desc:所属行业
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SSHY { get; set; }

        /// <summary>
        /// Desc:国家/地区
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GJ { get; set; }

        /// <summary>
        /// Desc:户籍所在省市
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HJSZSS { get; set; }

        /// <summary>
        /// Desc:性别
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XB { get; set; }

        /// <summary>
        /// Desc:照片
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte[] ZP { get; set; }

        /// <summary>
        /// Desc:电话
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DH { get; set; }

        /// <summary>
        /// Desc:地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DZ { get; set; }

        /// <summary>
        /// Desc:邮编
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YB { get; set; }

        /// <summary>
        /// Desc:工作单位
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GZDW { get; set; }

        /// <summary>
        /// Desc:电子邮件
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DZYJ { get; set; }

        /// <summary>
        /// Desc:权利人性质
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QLRXZ { get; set; }

        /// <summary>
        /// Desc:权利人状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string QLRZT { get; set; }

        /// <summary>
        /// Desc:信用状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string XYZT { get; set; }

        /// <summary>
        /// Desc:信用等级
        /// Default:
        /// Nullable:True
        /// </summary>           
        public decimal? XYDJ { get; set; }

        /// <summary>
        /// Desc:父权利人
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FQLR { get; set; }

        /// <summary>
        /// Desc:关系
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string GX { get; set; }

        /// <summary>
        /// Desc:家庭人口
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string JTRK { get; set; }

        /// <summary>
        /// Desc:纳税人识别号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string NSRSBH { get; set; }

        /// <summary>
        /// Desc:法人代表姓名
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FRDBXM { get; set; }

        /// <summary>
        /// Desc:法人代表证件类型
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FRDBZJLX { get; set; }

        /// <summary>
        /// Desc:法人代表证件号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FRDBZJH { get; set; }

        /// <summary>
        /// Desc:法人代表电话号码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string FRDBDHHM { get; set; }

        /// <summary>
        /// Desc:上级主管部门
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string SJZGBM { get; set; }

        /// <summary>
        /// Desc:通讯地址
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string TXDZ { get; set; }

        /// <summary>
        /// Desc:指纹
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte[] ZW1 { get; set; }

        /// <summary>
        /// Desc:指纹特征码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte[] ZWTZM1 { get; set; }

        /// <summary>
        /// Desc:指纹
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte[] ZW2 { get; set; }

        /// <summary>
        /// Desc:指纹特征码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte[] ZWTZM2 { get; set; }

        /// <summary>
        /// Desc:指纹
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte[] ZW3 { get; set; }

        /// <summary>
        /// Desc:指纹特征码
        /// Default:
        /// Nullable:True
        /// </summary>           
        public byte[] ZWTZM3 { get; set; }

        /// <summary>
        /// Desc:有效起始日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? YXQSRQ { get; set; }

        /// <summary>
        /// Desc:有效终止日期
        /// Default:
        /// Nullable:True
        /// </summary>           
        public DateTime? YXZZRQ { get; set; }

        /// <summary>
        /// Desc:备案状态
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BAZT { get; set; }

        /// <summary>
        /// Desc:户籍备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string HJBZ { get; set; }

        /// <summary>
        /// Desc:担保范围
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string DBFW { get; set; }

        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string YWSLH { get; set; }

        /// <summary>
        /// Desc:备注
        /// Default:
        /// Nullable:True
        /// </summary>           
        public string BZ { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string qlrlx { get; set; }

    }
}
