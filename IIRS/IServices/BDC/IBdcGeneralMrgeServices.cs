using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.IServices.BDC
{
    public interface IBdcGeneralMrgeServices
    {
        /// <summary>
        /// 一般抵押业务
        /// </summary>
        /// <param name="DjInfo">登记信息</param>
        /// <param name="AuzInfo">订单表</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="TsglInfo">图属信息</param>
        /// <param name="jsonData">登记信息保存暂存信息表</param>
        /// <param name="DyInfo">抵押信息</param>
        /// <param name="spInfo">审批信息表</param>
        /// <param name="XgdjglInfos">相关登记关联信息</param>
        /// <param name="qlrglInfos">权利人信息</param>
        /// <param name="uploadFiles">附件信息</param>
        /// <param name="sfd">收费单</param>
        /// <param name="sfdList">收费单明细</param>
        /// <param name="sjdInfo">收件单</param>
        /// <param name="qlxgInfo">权利相关信息</param>
        /// <param name="isInsert">是否新增</param>
        /// <param name="IsSubmitFlow">是否提交完成当前流程</param>
        /// <param name="OldXID">历史XID（仅当退回编辑时使用）</param>
        /// <returns>多表操作影响记录数之和</returns>
        int Mortgage(BankAuthorize AuzInfo, REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, SysDataRecorderModel jsonData, List<TSGL_INFO> TsglInfo, DY_INFO DyInfo, SPB_INFO spInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, List<PUB_ATT_FILE> uploadFiles, SFD_INFO sfd, List<SFD_FB_INFO> sfdList, SJD_INFO sjdInfo, QL_XG_INFO qlxgInfo, bool isInsert, bool IsSubmitFlow, string OldXID);

        /// <summary>
        /// 修改该受理编号下文件信息
        /// </summary>
        /// <param name="uploadFiles">文件列表</param>
        /// <param name="xid">业务流程主键编号</param>
        /// <returns></returns>
        Task<int> UpdateFile(List<PUB_ATT_FILE> uploadFiles, string xid);

        /// <summary>
        /// 查询附件信息
        /// </summary>
        /// <param name="XID">业务信息表XID</param>
        /// <returns></returns>
        Task<List<PUB_ATT_FILE>> UploadFileQueryByXID(string XID);

        /// <summary>
        /// 查询房屋信息
        /// </summary>
        /// <param name="BDCZH">不动产证号</param>
        /// <param name="BDCLX">不动产类型(宗地、房屋,否则去除该条件)</param>
        /// <param name="QLRMC">权利人名称</param>
        /// <param name="ZL">坐落</param>
        /// <param name="DJB_SLBH">(登记簿)受理编号</param>
        /// <param name="BDCDYH">不动产单元和</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        Task<PageStringModel> GetBdcHourseInfo(string BDCZH, string BDCLX, string QLRMC, string ZL, string DJB_SLBH, string BDCDYH, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 查询不动产权利人
        /// </summary>
        /// <param name="djbSLBH">登记簿受理编号</param>
        /// <returns></returns>
        Task<List<QLR_VModel>> GetBdcQlrInfo(string[] djbSLBH);


        /// <summary>
        /// 抵押勘误查询
        /// </summary>
        /// <param name="BDCDYH">不动产单元号</param>
        /// <param name="BDCZMH">不动产证明号</param>
        /// <param name="SLBH">(登记簿)受理编号</param>
        /// <param name="BDCLX">不动产类型(宗地、房屋,否则去除该条件)</param>
        /// <param name="DYRMC">抵押人名称</param>
        /// <param name="ZL">坐落</param>
        /// <param name="YWRMC">义务人名称</param>
        /// <param name="ZSLX">证书类型</param>
        /// <param name ="LIFE"> 数据状态 </param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        Task<PageStringModel> GetBdcCorrigendumInfo(string BDCDYH, string BDCZMH, string SLBH, string BDCLX, string DYRMC, string ZL,string YWRMC,string ZSLX,string LIFE, int pageIndex = 1, int pageSize = 10);
        /// <summary>
        /// 抵押勘误收件信息
        /// </summary>
        Task<KwHouseVModel> GetMrgeCertHouseInfo(string Dy_Slbh);
    }
}
