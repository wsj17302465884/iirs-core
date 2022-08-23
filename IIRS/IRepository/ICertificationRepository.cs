using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    /// <summary>
    /// 根据抵押不动产证明号查询接口
    /// </summary>
    public interface ICertificationRepository:IBaseRepository<CertificationVModel>
    {
        Task<List<CertificationVModel>> GetCertificationList(string bdczmh, string dyqr_qlrmc);

        Task<PageModel<CertificationVModel>> GetCertificationListToPage(int intPageIndex, string bdczmh, string dyr,string dyqr);

        Task<List<CertificationVModel>> GetCertificationInfo(string bdczmh, string zjhm,string dyqr);

        /// <summary>
        /// 不动产证明信息综合查询
        /// </summary>
        /// <param name="bdczmh">不动产证明号</param>
        /// <param name="bdcdyh">不动产单元号</param>
        /// <param name="dySlbh">抵押受理编号</param>
        /// <param name="dyr">抵押人姓名</param>
        /// <param name="intPageIndex">查询分页页面编码</param>
        /// <param name="pageLength">每个分页页面长度</param>
        /// <returns></returns>
        Task<PageModel<CertificationVModel>> GetCertificationListToPage(string bdczmh, string bdcdyh, string dySlbh, string dyr, int intPageIndex, int pageLength);

    }
}
