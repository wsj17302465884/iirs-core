using IIRS.IServices.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface IChartServices : IBaseServices
    {
        /// <summary>
        /// 查询办件量
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="Loginregid">登陆人组织机构代码</param>
        /// <returns></returns>
        Task<ChartVModel> GetDataCounts(string date, Guid Loginregid );

    }
}
