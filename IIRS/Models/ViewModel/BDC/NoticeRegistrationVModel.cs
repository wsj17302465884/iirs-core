using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Models.ViewModel.BDC
{
    /// <summary>
    /// 预告登记实体类
    /// </summary>
    public class NoticeRegistrationVModel
    {
        /// <summary>
        /// 元数据
        /// </summary>
        [JsonProperty("metedata")]
        public Dictionary<string, string> MeteData { get; set; }

        /// <summary>
        /// 命令类型，0:暂存，1:完成，2:退回
        /// </summary>
        public int commandtype { get; set; }
        /// <summary>
        /// 系统主键，唯一标识
        /// </summary>
        public string xid { get; set; }
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string  bdcdyh { get; set; }
        /// <summary>
        /// 交易合同编号
        /// </summary>
        public string jyhtbh { get; set; }
        /// <summary>
        /// 图属统一编码
        /// </summary>
        public string tstybm { get; set; }
        /// <summary>
        /// 共有方式
        /// </summary>
        public string gyfs { get; set; }
        /// <summary>
        /// 受理编号
        /// </summary>
        public string slbh { get; set; }
        /// <summary>
        /// 登记大类：预告登记
        /// </summary>
        public string djdl { get; set; }
        /// <summary>
        /// 登记原因
        /// </summary>
        public string djyy { get; set; }
        /// <summary>
        /// 排队号
        /// </summary>
        public string pdh { get; set; }
        /// <summary>
        /// 收件时间
        /// </summary>
        public string sjsj { get; set; }
        /// <summary>
        /// 承诺时间
        /// </summary>
        public string cnsj { get; set; }
        /// <summary>
        /// 受理科室
        /// </summary>
        public string slks   { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public string sxj { get; set; }
        /// <summary>
        /// 经办人ID
        /// </summary>
        public string orgId { get; set; }
        /// <summary>
        /// 受理人员
        /// </summary>
        public string slry { get; set; }
        /// <summary>
        /// 所在区域
        /// </summary>
        public string szqy { get; set; }
        /// <summary>
        /// 通知人姓名
        /// </summary>
        public string tzrxm { get; set; }
        /// <summary>
        /// 通知人电话
        /// </summary>
        public string tzrdh { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string zl { get; set; }
        /// <summary>
        /// 收件备注
        /// </summary>
        public string sjbz { get; set; }

        public string SelectGYFS { get; set; }
        public string ywrGYFS { get; set; }

        /// <summary>
        /// 权利人信息
        /// </summary>
        public List<QLRGL_INFO> qlrList { get; set; } = new List<QLRGL_INFO>();
        /// <summary>
        /// 义务人信息
        /// </summary>
        public List<QLRGL_INFO> ywrList { get; set; } = new List<QLRGL_INFO>();
        /// <summary>
        /// 房屋信息
        /// </summary>
        public List<NoticeHouse> selectHouse { get; set; } = new List<NoticeHouse>();

        public class NoticeHouse
        {
            public string bdcdyh { get; set; }
            public string myc { get; set; }
            public string zl { get; set; }
            public decimal jzmj { get; set; }
            public string ghyt_zwm { get; set; }
        }

        #region  房屋登记信息
        /// <summary>
        /// 宗地面积
        /// </summary>
        public decimal? zdmj { get; set; }
        /// <summary>
        /// 发证面积
        /// </summary>
        public string fzmj { get; set; }
        /// <summary>
        /// 土地权利类型
        /// </summary>
        public string tdqllx { get; set; }
        public string tdqllx_zwm { get; set; }
        /// <summary>
        /// 土地权利性质
        /// </summary>
        public string tdqlxz { get; set; }
        public string tdqlxz_zwm { get; set; }
        /// <summary>
        /// 土地用途
        /// </summary>
        public string tdyt { get; set; }
        public string tdyt_zwm { get; set; }
        /// <summary>
        /// 土地使用权面积
        /// </summary>
        public string tdsyqmj { get; set; }
        /// <summary>
        /// 独用土地面积
        /// </summary>
        public string dytdmj { get; set; }
        /// <summary>
        /// 分摊土地面积
        /// </summary>
        public string fttdmj { get; set; }
        /// <summary>
        /// 使用期限
        /// </summary>
        public string syqx { get; set; }
        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime? qsrq { get; set; }
        /// <summary>
        /// 终止日期
        /// </summary>
        public DateTime? zzrq { get; set; }
        /// <summary>
        /// 取得方式
        /// </summary>
        public string qdfs { get; set; }
        /// <summary>
        /// 评估金额
        /// </summary>
        public string pgje { get; set; }
        /// <summary>
        /// 交易金额
        /// </summary>
        public string jyje { get; set; }
        /// <summary>
        /// 房屋权利类型
        /// </summary>
        public string fwqllx { get; set; }
        public string fwqllx_zwm { get; set; }
        /// <summary>
        /// 房屋权利性质
        /// </summary>
        public string fwqlxz { get; set; }
        public string fwqlxz_zwm { get; set; }
        /// <summary>
        /// 房屋规划用途
        /// </summary>
        public string fwghyt { get; set; }
        public string fwghyt_zwm { get; set; }
        /// <summary>
        /// 房屋建筑面积
        /// </summary>
        public decimal? jzmj { get; set; }
        /// <summary>
        /// 套内建筑面积
        /// </summary>
        public decimal? tnjzmj { get; set; }
        /// <summary>
        /// 分摊建筑面积
        /// </summary>
        public decimal? ftjzmj { get; set; }
        /// <summary>
        /// 权利其他情况
        /// </summary>
        public string qlqtqk { get; set; }
        /// <summary>
        /// 附记
        /// </summary>
        public string fj { get; set; }
        #endregion

        #region 审批
        /// <summary>
        /// 初审意见
        /// </summary>
        public string csyj { get; set; }
        /// <summary>
        /// 初审人
        /// </summary>
        public string csr { get; set; }
        /// <summary>
        /// 初审日期
        /// </summary>
        public string csrq { get; set; }
        /// <summary>
        /// 审批备注
        /// </summary>
        public string spbz { get; set; }
        #endregion
    }
}
