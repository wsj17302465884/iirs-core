using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace IIRS.Models.ViewModel.LAW
{
    public class Message
    {
        public Head Head { get; set; }
        public Data Data { get; set; }
    }
    public class Head
    {
        /// <summary>
        /// 
        /// </summary>
        
        public string CREATETIME { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RESPONSECODE { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string DIGITALSIGN { get; set; }

        public string RESPONSEINFO { get; set; }
    }
    public class Data
    {
        public List<BDCCXJG> BDCFKLIST { get; set; } = new List<BDCCXJG>();
    }
    public class BDCCXJG
    {

        /// <summary>
        /// 查询请求单号
        /// </summary>
        public string CXQQDH { get; set; }
        public string RESULT { get; set; }
        public List<TDSYQ> TDSYQLIST { get; set; } = new List<TDSYQ>();
        public List<JSYDSYQ> JSYDSYQLIST { get; set; } = new List<JSYDSYQ>();
        public List<FDCQ> FDCQLIST { get; set; } = new List<FDCQ>();
        public List<GJZWSYQ> GJZWSYQLIST { get; set; } = new List<GJZWSYQ>();
        public List<DYAQ> DYAQLIST { get; set; } = new List<DYAQ>();
        public List<YGDJ> YGDJLIST { get; set; } = new List<YGDJ>();
        public List<CFDJ> CFDJLIST { get; set; } = new List<CFDJ>();
        public List<YYXX> YYXXLIST { get; set; } = new List<YYXX>();
    }
    /// <summary>
    /// 权利人信息
    /// </summary>
    public class QLRXX
    {
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; }
        /// <summary>
        /// 顺序号
        /// </summary>
        
        public string SXH { get; set; }
        /// <summary>
        /// 权利人名称
        /// </summary>
        public string QLRMC { get; set; }
        /// <summary>
        /// 不动产权证号
        /// </summary>
        public string BDCQZH { get; set; }
        /// <summary>
        /// 权证印刷序列号
        /// </summary>
        public string QZYSXLH { get; set; }
        /// <summary>
        /// 是否持证人
        /// </summary>
        public string SFCZR { get; set; }
        /// <summary>
        /// 证件种类
        /// </summary>
        public string ZJZL { get; set; }
        /// <summary>
        /// 证件号
        /// </summary>
        public string ZJH { get; set; }
        /// <summary>
        /// 发证机关
        /// </summary>
        public string FZJG { get; set; }
        /// <summary>
        /// 所属行业
        /// </summary>
        public string SSHY { get; set; }
        /// <summary>
        /// 国家/地区
        /// </summary>
        public string GJ { get; set; }
        /// <summary>
        /// 户籍所在省市
        /// </summary>
        public string HJSZSS { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string XB { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string DH { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string DZ { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string YB { get; set; }
        /// <summary>
        /// 工作单位
        /// </summary>
        public string GZDW { get; set; }
        /// <summary>
        /// 电子邮件
        /// </summary>
        public string DZYJ { get; set; }
        /// <summary>
        /// 权利人类型
        /// </summary>
        public string QLRLX { get; set; }
        /// <summary>
        /// 权利比例
        /// </summary>
        public string QLBL { get; set; }
        /// <summary>
        /// 共有方式
        /// </summary>
        public string GYFS { get; set; }
        /// <summary>
        /// 共有情况
        /// </summary>
        public string GYQK { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BZ { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string YWH { get; set; }
        /// <summary>
        /// 权属状态
        /// </summary>
        public string QSZT { get; set; }
        /// <summary>
        /// 不动产类型
        /// </summary>
        public string BDCLX { get; set; }
    }
    /// <summary>
    /// 土地所有权信息
    /// </summary>
    public class TDSYQ
    {
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; }

        /// <summary>
        /// 宗地面积
        /// </summary>
        public string ZDMJ { get; set; }
        /// <summary>
        /// 面积单位
        /// </summary>
        public string MJDW { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string YT { get; set; }
        /// <summary>
        /// 权利性质
        /// </summary>
        public string QLXZ { get; set; }
        /// <summary>
        /// 不动产权证书号
        /// </summary>
        public string BDCQZH { get; set; }
        /// <summary>
        /// 登记机构
        /// </summary>
        public string DJJG { get; set; }
        /// <summary>
        /// 份额比例
        /// </summary>
        public string FEBL { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string YWH { get; set; }
        public List<QLRXX> QLRXXLIST { get; set; } = new List<QLRXX>();
    }
    /// <summary>
    /// 建设用地使用权、宅基地使用权
    /// </summary>
    public class JSYDSYQ
    {
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; }
        /// <summary>
        /// 用途
        /// </summary>
        public string YT { get; set; }
        /// <summary>
        /// 权利性质
        /// </summary>
        public string QLXZ { get; set; }
        /// <summary>
        /// 使用期限起
        /// </summary>
        
        public string SYQQSSJ { get; set; }
        /// <summary>
        /// 使用期限止
        /// </summary>
        
        public string SYQJSSJ { get; set; }
        /// <summary>
        /// 不动产权证书号
        /// </summary>
        public string BDCQZH { get; set; }
        /// <summary>
        /// 登记机关
        /// </summary>
        public string DJJG { get; set; }
        /// <summary>
        /// 分割比列
        /// </summary>
        public string FEBL { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string YWH { get; set; }
        public List<QLRXX> QLRXXLIST { get; set; } = new List<QLRXX>();
    }
    /// <summary>
    /// 房地产权
    /// </summary>
    public class FDCQ
    {
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; }
        /// <summary>
        /// 房地坐落
        /// </summary>
        public string FDZL { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        
        public string JZMJ { get; set; }
        /// <summary>
        /// 专有建筑面积
        /// </summary>
        
        public string ZYJZMJ { get; set; }
        /// <summary>
        /// 分摊建筑面积
        /// </summary>
        
        public string FTJZMJ { get; set; }
        /// <summary>
        /// 规划用途
        /// </summary>
        public string GHYT { get; set; }
        /// <summary>
        /// 房屋性质
        /// </summary>
        public string FWXZ { get; set; }
        /// <summary>
        /// 竣工时间
        /// </summary>
        
        public string JGSJ { get; set; }
        /// <summary>
        /// 土地使用期限起
        /// </summary>
        
        public string TDSYQSSJ { get; set; }
        /// <summary>
        /// 土地使用期限止
        /// </summary>
        
        public string TDSYJSSJ { get; set; }
        /// <summary>
        /// 不动产权证书号
        /// </summary>
        public string BDCQZH { get; set; }
        /// <summary>
        /// 登记机构
        /// </summary>
        public string DJJG { get; set; }
        /// <summary>
        /// 分割比列
        /// </summary>
        public string FEBL { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string YWH { get; set; }
        public List<QLRXX> QLRXXLIST { get; set; } = new List<QLRXX>();
    }
    /// <summary>
    /// 构（建）筑物所有权
    /// </summary>
    public class GJZWSYQ
    {
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; }
        /// <summary>
        /// 构（建）筑物规划用途
        /// </summary>
        public string GJZWGHYT { get; set; }
        /// <summary>
        /// 构（建）筑物面积
        /// </summary>
        public decimal GJZWMJ { get; set; }
        /// <summary>
        /// 土地/海域使用期限起
        /// </summary>
        public DateTime TDHYSYQSSJ { get; set; }
        /// <summary>
        /// 土地/海域使用期限止
        /// </summary>
        public DateTime TDHYSYJSSJ { get; set; }
        /// <summary>
        /// 不动产权证书号
        /// </summary>
        public string BDCQZH { get; set; }
        /// <summary>
        /// 登记机关
        /// </summary>
        public string DJJG { get; set; }
        /// <summary>
        /// 分割比列
        /// </summary>
        public string FEBL { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string YWH { get; set; }
        public List<QLRXX> QLRXXLIST { get; set; }
    }
    /// <summary>
    /// 抵押权
    /// </summary>
    public class DYAQ
    {
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; }
        /// <summary>
        /// 抵押不动产类型
        /// </summary>
        public string DYBDCLX { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; }
        /// <summary>
        /// 抵押人
        /// </summary>
        public string DYR { get; set; }
        /// <summary>
        /// 抵押方式
        /// </summary>
        public string DYFS { get; set; }
        /// <summary>
        /// 债务履行期限起
        /// </summary>
        
        public string ZWLXQSSJ { get; set; }
        /// <summary>
        /// 债务履行期限止
        /// </summary>
        
        public string ZWLXJSSJ { get; set; }
        /// <summary>
        /// 被担保主债权数额
        /// </summary>
        public decimal BDBZZQSE { get; set; }
        /// <summary>
        /// 不动产登记证明号
        /// </summary>
        public string BDCDJZMH { get; set; }
        /// <summary>
        /// 登记机关
        /// </summary>
        public string DJJG { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string YWH { get; set; }
    }
    /// <summary>
    /// 预告登记
    /// </summary>
    public class YGDJ
    {
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; }
        /// <summary>
        /// 预告登记种类
        /// </summary>
        public string YGDJZL { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; }
        /// <summary>
        /// 规划用途
        /// </summary>
        public string GHYT { get; set; }
        /// <summary>
        /// 建筑面积
        /// </summary>
        public string JZMJ { get; set; }
        /// <summary>
        /// 不动产登记证明号
        /// </summary>
        public string BDCDJZMH { get; set; }
        /// <summary>
        /// 登记机构
        /// </summary>
        public string DJJG { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string YWH { get; set; }
        public List<QLRXX> QLRXXLIST { get; set; } = new List<QLRXX>();
    }
    /// <summary>
    /// 查封登记
    /// </summary>
    public class CFDJ
    {
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; }
        /// <summary>
        /// 查封机关
        /// </summary>
        public string CFJG { get; set; }
        /// <summary>
        /// 查封类型
        /// </summary>
        public string CFLX { get; set; }
        /// <summary>
        /// 查封文号
        /// </summary>
        public string CFWH { get; set; }
        /// <summary>
        /// 查封期限起
        /// </summary>
        
        public string CFQSSJ { get; set; }
        /// <summary>
        /// 查封期限止
        /// </summary>
        
        public string CFJSSJ { get; set; }
        /// <summary>
        /// 登记机构
        /// </summary>
        public string DJJG { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string CFYWH { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string YWH { get; set; }
    }
    /// <summary>
    /// 异议登记
    /// </summary>
    public class YYXX
    {
        /// <summary>
        /// 不动产单元号
        /// </summary>
        public string BDCDYH { get; set; }
        /// <summary>
        /// 坐落
        /// </summary>
        public string ZL { get; set; }
        /// <summary>
        /// 异议申请人
        /// </summary>
        public string YYSQR { get; set; }
        /// <summary>
        /// 异议事项
        /// </summary>
        public string YYSX { get; set; }
        /// <summary>
        /// 不动产登记证明号
        /// </summary>
        public string BDCDJZMH { get; set; }
        /// <summary>
        /// 登记时间
        /// </summary>
        
        public string DJSJ { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string BEIZ { get; set; }
        /// <summary>
        /// 业务号
        /// </summary>
        public string YWH { get; set; }
    }
}
