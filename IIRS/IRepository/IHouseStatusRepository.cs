using IIRS.IRepository.Base;
using IIRS.Models.EntityModel;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IHouseStatusRepository : IBaseRepository<HouseStatusViewModel>
    {
        /// <summary>
        /// 获取房屋的所有基本属性与状态
        /// </summary>
        /// <param name="tstybm">图属统一编码</param>
        /// <returns></returns>
        Task<MessageModel<List<HouseStatusViewModel>>> GetHouseSatausList(string tstybm);

        /// <summary>
        /// 添加不动产相关信息
        /// </summary>
        /// <param name="modelList"></param>
        /// <returns></returns>
        Task<List<HouseStatusViewModel>> AddhouseList(List<HouseStatusViewModel> modelList);

        Task<int> AddviewModel(HouseStatusViewModel model);

        Task<PageModel<HouseStatusViewModel>> GetHouseSatausListToPage(string tstybm, int intPageIndex);

        Task<PageModel<HouseStatusViewModel>> GetEnterpriseHouseSatausListToPage(string tstybm, int intPageIndex);

        Task<List<HouseStatusViewModel>> GetHouseSatausList_New(string tstybm);

    }
}
