using IIRS.IServices.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Models.ViewModel.LAW;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface IChangeMrgeServices : IBaseServices
    {
        /// <summary>
        /// 保存转移抵押合并办理
        /// </summary>
        /// <param name="isInsert">当前操作是否暂存</param>
        /// <param name="DjInfo">收件单表信息</param>
        /// <param name="AuzInfo">订单表信息</param>
        /// <param name="jsonData">登记信息保存暂存信息表</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="spInfo">审批信息表</param>
        /// <param name="djbInfo">登记簿信息表</param>
        /// <param name="TsglInfo">图属关联</param>
        /// <param name="DyInfo">抵押信息</param>
        /// <param name="XgdjglInfos">相关登记关联信息</param>
        /// <param name="qlrglInfos">权利人关联信息</param>
        /// <param name="qlxgInfo">权利相关信息</param>
        /// <param name="uploadFiles">附件信息</param>
        /// <param name="sfdList">收费单（抵押、登记）</param>
        /// <param name="sfdDetailsList">收费单明细（抵押、登记）</param>
        /// <param name="sjdList">收件单</param>
        /// <param name="OldXID">历史XID（仅当退回编辑时使用）</param>
        /// <returns></returns>
        int ChangeMrgesSave(bool isInsert, REGISTRATION_INFO DjInfo, BankAuthorize AuzInfo, SysDataRecorderModel jsonData, IFLOW_DO_ACTION flowInfo, SPB_INFO spInfo, DJB_INFO djbInfo, List<TSGL_INFO> TsglInfo, DY_INFO DyInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, QL_XG_INFO qlxgInfo, List<PUB_ATT_FILE> uploadFiles, List<SFD_INFO> sfdList, List<SFD_FB_INFO> sfdDetailsList, List<SJD_INFO> sjdList, string OldXID);

        /// <summary>
        /// 获取本年度不动产证明编号计数器值
        /// </summary>
        /// <returns>不动产证明号+证明号编号</returns>
        Task<string[]> GetBDCZH_NUM();

        /// <summary>
        /// 查询权利人信息
        /// </summary>
        /// <param name="bdzcz">不动产证号</param>
        /// <returns></returns>
        Task<ChangeMrgeHouseVModel> GetHouseInfo(string bdzcz);

        /// <summary>
        /// 查询权利人信息
        /// </summary>
        /// <param name="bdzcz">不动产证号</param>
        /// <returns></returns>
        Task<List<ChangeMrgePersonVModel>> GetPersonInfo(string bdzcz);

        /// <summary>
        /// 获取土地房屋权利信息
        /// </summary>
        /// <param name="bdzcz">(现实手)不动产证明号</param>
        /// <returns></returns>
        Task<QL_XG_INFO> GetLandHouseRightInfo(string bdzcz);
    }
}
