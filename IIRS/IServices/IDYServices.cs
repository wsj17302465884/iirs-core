using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IIRS.IServices.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;

namespace IIRS.IServices
{
    public interface IDYServices : IBaseServices
    {
        /// <summary>
        /// 查询退回原因
        /// </summary>
        /// <param name="XID">流程流水号</param>
        /// <returns></returns>
        Task<IFLOW_ACTION_BACK> FlowBackReason(string XID);

        /// <summary>
        /// 查询现实手退回原因
        /// </summary>
        /// <param name="AuzID">订单主键编号</param>
        /// <param name="flowID">流程节点编号</param>
        /// <returns></returns>
        Task<IFLOW_ACTION_BACK> FlowBackReason(string AuzID, int flowID);

        /// <summary>
        /// 提交当前流程节点处理状态
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        Task<int> UpdateFinish(string slbh);

        /// <summary>
        /// 修改该受理编号下文件信息
        /// </summary>
        /// <param name="uploadFiles">文件列表</param>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        Task<int> UpdateFile(List<PUB_ATT_FILE> uploadFiles, string slbh);

        /// <summary>
        /// 查询电子证照不动产数据信息
        /// </summary>
        /// <param name="dySlbh"></param>
        /// <returns></returns>
        Task<V_CX_DyVModel> GetBdczmPdfInfo(string dySlbh);

        /// <summary>
        /// 查询附件信息
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        Task<List<PUB_ATT_FILE>> UploadFileQuery(string slbh);

        /// <summary>
        /// 保存附件文件数据库信息
        /// </summary>
        /// <param name="files">文件信息</param>
        /// <returns></returns>
        Task<int> SaveUploadFileDB(List<PUB_ATT_FILE> files);

        /// <summary>
        /// 查询不动产相关登记信息
        /// </summary>
        /// <param name="bdzcz">不动作证明号</param>
        /// <returns></returns>
        Task<DyVModel> GetHouseInfo(List<string> bdzcz);

        /// <summary>
        /// 一般抵押业务
        /// </summary>
        /// <param name="DjInfo">登记信息</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="TsglInfo">图属信息</param>
        /// <param name="DyInfo">抵押信息</param>
        /// <param name="XgdjglInfos">相关登记关联信息</param>
        /// <param name="qlrglInfos">权利人信息</param>
        /// <param name="uploadFiles">附件信息</param>
        /// <returns>多表操作影响记录数之和</returns>
        Task<int> Mortgage(REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, DY_INFO DyInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, List<PUB_ATT_FILE> uploadFiles);

        /// <summary>
        /// 获取系统受理编号
        /// </summary>
        /// <returns>受理编号</returns>
        string GetSLBH();

        /// <summary>
        /// 初始化抵押上报文件格式信息
        /// </summary>
        /// <param name="GID">分组编号</param>
        /// <returns></returns>
        Task<MediasVModel> GetInitMedias(int GID);

        /// <summary>
        /// 查询银行端抵押信息
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        Task<HouseVModel> MortgageQuery(string slbh);
    }
}
