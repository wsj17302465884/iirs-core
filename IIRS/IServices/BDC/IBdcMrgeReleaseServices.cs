using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using Spire.Pdf;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices.BDC
{
    public interface IBdcMrgeReleaseServices
    {
        /// <summary>
        /// 设置《不动产抵押登记申请表》
        /// </summary>
        /// <param name="pdfFile">源文件</param>
        /// <param name="xid">业务编号</param>
        void SetPrintApplyData(PdfDocument pdfFile, string xid);

        /// <summary>
        /// 设置《不动产抵押登记审批表》
        /// </summary>
        /// <param name="pdfFile">源文件</param>
        /// <param name="xid">业务编号</param>
        void SetPrintApproveData(PdfDocument pdfFile, string xid);

        /// <summary>
        /// 查询抵押项目登记信息
        /// </summary>
        /// <param name="DY_SLBH">抵押受理编号</param>
        /// <returns></returns>
        Task<MrgeReleaseVModel> GetMrgeCertHouseInfo(string DY_SLBH);

        /// <summary>
        /// 查询要抵押不动产信息
        /// </summary>
        /// <param name="BDCDYH">不动产单元号</param>
        /// <param name="BDCZMH">不动产证明号</param>
        /// <param name="QLRMC">权利人名称</param>
        /// <param name="ZL">坐落</param>
        /// <param name="DY_SLBH">抵押受理编号</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        Task<PageStringModel> GetMrgeCertInfo(string BDCDYH, string BDCZMH, string QLRMC, string ZL, string DY_SLBH, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 房屋抵押注销
        /// </summary>
        /// <param name="isInsert">当前操作是否暂存</param>
        /// <param name="AuzInfo">订单表</param>
        /// <param name="DjInfo">登记信息</param>
        /// <param name="jsonData">登记信息保存暂存信息表</param>
        /// <param name="zxInfo">注销信息表</param>
        /// <param name="spInfo">审批信息表</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="TsglInfo">图属信息</param>
        /// <param name="XgdjglInfos">相关登记关联信息</param>
        /// <param name="qlrglInfos">权利人信息</param>
        /// <param name="sjdInfo">收件单</param>
        /// <param name="qlxgInfo">权利相关信息</param>
        /// <param name="uploadFiles">附件信息</param>
        /// <param name="OldXID">历史XID（仅当退回编辑时使用）</param>
        /// <returns>多表操作影响记录数之和</returns>
        int MortgageRelease(bool isInsert, BankAuthorize AuzInfo, REGISTRATION_INFO DjInfo, SysDataRecorderModel jsonData, XGDJZX_INFO zxInfo, SPB_INFO spInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, SJD_INFO sjdInfo, QL_XG_INFO qlxgInfo, List<PUB_ATT_FILE> uploadFiles, string OldXID);
    }
}
