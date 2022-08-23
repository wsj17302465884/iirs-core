using System;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using SqlSugar;

namespace IIRS.Models.EntityModel.IIRS
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("QL_XG_INFO")]
    public partial class QL_XG_INFO
    {
        public QL_XG_INFO()
        {


        }
        /// <summary>
        /// Desc:关联编码
        /// Default:
        /// Nullable:False
        /// </summary>           
        [SugarColumn(IsPrimaryKey = true)]
        [JsonProperty("xtbh")]
        public string XTBH { get; set; } = string.Empty;

        /// <summary>
        /// Desc:受理编号
        /// Default:
        /// Nullable:False
        /// </summary>
        [JsonProperty("slbh")]
        public string SLBH { get; set; } = string.Empty;

        /// <summary>
        /// 房屋权利类型
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_qllx")]
        public string FW_QLLX { get; set; } = string.Empty;

        /// <summary>
        /// 房屋权利类型中文名
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_qllx_zwm")]
        public string FW_QLLX_ZWM { get; set; } = string.Empty;

        /// <summary>
        /// 房屋权利性质
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_qlxz")]
        public string FW_QLXZ { get; set; } = string.Empty;

        /// <summary>
        /// 房屋权利性质中文描述
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_qlxz_zwms")]
        public string FW_QLXZ_ZWMS { get; set; } = string.Empty;

        /// <summary>
        /// 建筑面积
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_jzmj")]
        public decimal? FW_JZMJ { get; set; }

        /// <summary>
        /// 产权来源(预留)
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_cqly")]
        public decimal? FW_CQLY { get; set; }

        /// <summary>
        /// 套内建筑面积
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_tnjzmj")]
        public decimal? FW_TNJZMJ { get; set; }

        /// <summary>
        /// 分摊建筑面积
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_ftjzmj")]
        public decimal? FW_FTJZMJ { get; set; }

        /// <summary>
        /// 预测建筑面积
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_yc_jzmj")]
        public decimal? FW_YC_JZMJ { get; set; }

        /// <summary>
        /// 预测套内建筑面积
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_yc_tnjzmj")]
        public decimal? FW_YC_TNJZMJ { get; set; }

        /// <summary>
        /// 预测分摊建筑面积
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_yc_ftjzmj")]
        public decimal? FW_YC_FTJZMJ { get; set; }

        /// <summary>
        /// 房屋规划用途
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_fwghyt")]
        public string FW_FWGHYT { get; set; } = string.Empty;

        /// <summary>
        /// 房屋规划用途中文名
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_fwghyt_zwm")]
        public string FW_FWGHYT_ZWM { get; set; } = string.Empty;

        /// <summary>
        /// 取得方式
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("qdfs")]
        public string QDFS { get; set; } = string.Empty;

        /// <summary>
        /// 取得价格
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("qdjg")]
        public decimal? QDJG { get; set; }

        /// <summary>
        /// 评估金额
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("pgje")]
        public decimal? PGJE { get; set; }

        /// <summary>
        /// 土地权利类型
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_qllx")]
        public string TD_QLLX { get; set; } = string.Empty;

        /// <summary>
        /// 土地权利类型中文名
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_qllx_zwm")]
        public string TD_QLLX_ZWM { get; set; } = string.Empty;

        /// <summary>
        /// 土地权利性质(多个性质逗号分割)
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_qlxz")]
        public string TD_QLXZ { get; set; } = string.Empty;

        /// <summary>
        /// 土地权利性质中文描述
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_qlxz_zwms")]
        public string TD_QLXZ_ZWMS { get; set; } = string.Empty;

        /// <summary>
        /// 土地共有土地面积
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_gytdmj")]
        public decimal? TD_GYTDMJ { get; set; }

        /// <summary>
        /// 土地独用土地面积
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_dytdmj")]
        public decimal? TD_DYTDMJ { get; set; }

        /// <summary>
        /// 土地分摊土地面积
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_fttdmj")]
        public decimal? TD_FTTDMJ { get; set; }

        /// <summary>
        /// 土地建筑宗地面积
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_jzzdmj")]
        public decimal? TD_JZZDMJ { get; set; }

        /// <summary>
        /// 土地使用期限
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_syqx")]
        public string TD_SYQX { get; set; } = string.Empty;

        /// <summary>
        /// 土地使用期限起始时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_qsrq")]
        public DateTime? TD_QSRQ { get; set; }

        /// <summary>
        /// Desc:土地使用期限终止时间
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_zzrq")]
        public DateTime? TD_ZZRQ { get; set; }

        /// <summary>
        /// Desc:土地用途
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_tdyt")]
        public string TD_TDYT { get; set; } = string.Empty;

        /// <summary>
        /// 土地用途中文描述
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_tdyt_zwms")]
        public string TD_TDYT_ZWMS { get; set; } = string.Empty;

        /// <summary>
        /// 土地用途中文描述
        /// Default:
        /// Nullable:True
        /// </summary>
        [JsonProperty("td_tdyt_full_path")]
        [SugarColumn(IsIgnore = true)]
        public string TD_TDYT_FULL_PATH { get; set; }
        

        /// <summary>
        /// 登记信息主键
        /// </summary>
        [JsonProperty("xid")]
        public string XID { get; set; } = string.Empty;

        /// <summary>
        /// 房屋结构
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_fwjg")]
        [SugarColumn(IsIgnore = true)]
        public string FW_FWJG { get; set; } = string.Empty;

        /// <summary>
        /// 房屋结构中文描述
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_fwjg_zwms")]
        [SugarColumn(IsIgnore = true)]
        public string FW_FWJG_ZWMS { get; set; } = string.Empty;

        /// <summary>
        /// 房屋总层数
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_fwzcs")]
        [SugarColumn(IsIgnore = true)]
        public string FW_FWZCS { get; set; } = string.Empty;

        /// <summary>
        /// 房屋所在层数
        /// Nullable:True
        /// </summary>
        [JsonProperty("fw_szcs")]
        [SugarColumn(IsIgnore = true)]
        public string FW_SZCS { get; set; } = string.Empty;

    }
}
