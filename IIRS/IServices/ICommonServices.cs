using IIRS.IServices.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface ICommonServices : IBaseServices
    {
        /// <summary>
        /// 查询附件信息
        /// </summary>
        /// <param name="XID">受理现实手编号</param>
        /// <returns></returns>
        Task<List<PUB_ATT_FILE>> UploadFileQueryByXID(string XID);

        /// <summary>
        /// 人脸识别授权通过
        /// </summary>
        /// <param name="FaceUserInfo"></param>
        /// <returns></returns>
        int FaceUserRec(FACE_RECOGNITION_AUTHORIZE FaceUserInfo);

        /// <summary>
        /// 添加JSON数据
        /// </summary>
        /// <param name="model">添加数据</param>
        /// <returns></returns>
        Task<int> BizJsonInsert(SysDataRecorderModel model);

        /// <summary>
        /// 查询当前流程相关信息
        /// </summary>
        /// <param name="XID">流程编号</param>
        /// <returns></returns>
        Task<VerifyFlowVModel> FlowInfoQuery(string XID);

        /// <summary>
        /// 查询业务表单提交过程JSON数据查询
        /// </summary>
        /// <param name="XID">流程编号</param>
        /// <returns></returns>
        Task<SysDataRecorderModel> BizJsonDataQuery(string XID);

        /// <summary>
        /// 查询业务表单提交过程JSON数据列表查询
        /// </summary>
        /// <param name="FLOW_ID">流程节点编码</param>
        /// <param name="USER_ID">用户ID</param>
        /// <param name="REMARKS1">查询条件1</param>
        /// <param name="REMARKS2">查询条件2</param>
        /// <param name="REMARKS3">查询条件3</param>
        /// <param name="REMARKS4">查询条件4</param>
        /// <param name="REMARKS5">查询条件5</param>
        /// <param name="pageIndex">查询分页页码</param>
        /// <param name="pageSize">每页显示数据数量</param>
        /// <returns></returns>
        Task<string> BizJsonDataPageQuery(decimal FLOW_ID, string USER_ID, string REMARKS1, string REMARKS2, string REMARKS3, string REMARKS4, string REMARKS5, int pageIndex, int pageSize);

        /// <summary>
        /// 追述附件查询
        /// </summary>
        /// <param name="SLBH">受理编号</param>
        /// <returns></returns>
        Task<El_CascaderNavTree> AttachmentQueryBySlbh(string SLBH);

        /// <summary>
        /// 不动产权利人查询
        /// </summary>
        /// <param name="ZJHM">查询证件号码</param>
        /// <param name="MC">查询人名称</param>
        /// <param name="pageIndex">查询分页页码</param>
        /// <param name="pageSize">查询分页每页长度</param>
        /// <returns>权利人结果集</returns>
        Task<PageModel<QLR_VModel>> BdcUserQuery(string ZJHM, string MC, int pageIndex, int pageSize);

        int SaveFile(LST_FILE FileInfo);

        /// <summary>
        /// 查询要抵押不动产信息
        /// </summary>
        /// <param name="userName">使用者名称</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns>分页结果集</returns>
        Task<PageModel<fileInfoVModel>> GetFileInfoList( string userName, int pageIndex = 1, int pageSize = 10);

        /// <summary>
        /// 获取本年度不动产证书编号计数器值
        /// </summary>
        /// <returns>不动产证书编号</returns>
        Task<string[]> GetBDCZH_NUM();

        /// <summary>
        /// 获取本年度不动产证明编号计数器值
        /// </summary>
        /// <returns>不动产证明编号</returns>
        Task<string[]> GetBDCZMH_NUM();
        /// <summary>
        /// 判断不动产证号是否存在
        /// </summary>
        /// <param name="bdczh"></param>
        /// <param name="slbh"></param>
        /// <returns></returns>
        string GetBdczhIsExist(string bdczh, string slbh);
    }
}
