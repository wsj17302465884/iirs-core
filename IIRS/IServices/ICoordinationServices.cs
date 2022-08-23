using IIRS.IServices.Base;
using IIRS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface ICoordinationServices : IBaseServices
    {
        /// <summary>
        /// 开发企业
        /// </summary>
        /// <param name="qlrmc"></param>
        /// <param name="zjhm"></param>
        /// <returns></returns>
        Task<List<CoordinationVModel>> GetEnterpriseList(string qlrmc, string zjhm);
        /// <summary>
        /// 社区教育
        /// </summary>
        /// <param name="qlrmc"></param>
        /// <param name="zjhm"></param>
        /// <returns></returns>
        Task<List<CoordinationVModel>> GetCommunityList(string qlrmc, string zjhm);

        /// <summary>
        /// 社区人口
        /// </summary>
        /// <param name="zl"></param>
        /// <returns></returns>
        Task<List<CoordinationVModel>> GetCommunityPeopleList(string zl);

        /// <summary>
        /// 中介
        /// </summary>
        /// <param name="qlrmc"></param>
        /// <param name="zjhm"></param>
        /// <returns></returns>
        Task<List<CoordinationVModel>> GetIntermediaryList(string qlrmc, string zjhm);

        /// <summary>
        /// 水电气
        /// </summary>
        /// <param name="qlrmc"></param>
        /// <param name="zjhm"></param>
        /// <returns></returns>
        Task<List<CoordinationVModel>> GetPaymentTransferList(string qlrmc, string zjhm);
        

    }
}
