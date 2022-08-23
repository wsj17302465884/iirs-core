using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IIRS.IServices.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;

namespace IIRS.IServices
{
    public interface IMrgeReleaseServices : IBaseServices
    {
        /// <summary>
        /// 查询注销订单(现实手)生成json
        /// </summary>
        /// <param name="AUZ_ID"></param>
        /// <returns>json结果集</returns>
        Task<string> RegistrationJsonQuery(string AUZ_ID);

        /// <summary>
        /// 保存附件文件数据库信息
        /// </summary>
        /// <param name="files">文件信息</param>
        /// <returns></returns>
        Task<int> SaveUploadFileDB(List<PUB_ATT_FILE> files);


        /// <summary>
        /// 查询不动产抵押信息
        /// </summary>
        /// <param name="BDCZMH">不动产证明号</param>
        /// <returns></returns>
        Task<MrgeReleaseVModel> GetMortgageInfo(string BDCZMH);

        /// <summary>
        /// 解压
        /// </summary>
        /// <param name="DjInfo">登记信息</param>
        /// <param name="zxInfo">注销信息表</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="TsglInfo">图属信息</param>
        /// <param name="XgdjglInfos">相关登记关联信息</param>
        /// <param name="qlrglInfos">权利人信息</param>
        /// <param name="uploadFiles">附件信息</param>
        /// <returns>多表操作影响记录数之和</returns>
        Task<int> MortgageRelease(REGISTRATION_INFO DjInfo, XGDJZX_INFO zxInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, List<PUB_ATT_FILE> uploadFiles);

        /// <summary>
        /// 获取系统受理编号
        /// </summary>
        /// <returns>受理编号</returns>
        string GetSLBH();

        /// <summary>
        /// 初始化抵押上报文件格式信息
        /// </summary>
        /// <returns></returns>
        Task<MediasVModel> GetInitMedias();
    }
}
