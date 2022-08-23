using IIRS.IServices.Base;
using IIRS.Models.ViewModel.WQ;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices.WQ
{
    public interface IYUEServices : IBaseServices
    {
        /// <summary>
        /// 获取时间段下放号量数据
        /// </summary>
        /// <returns></returns>
       Task<List<TimeVModel>> GetTimeListAsync(string date);
        /// <summary>
        /// 修改时间段下放号量数据
        /// </summary>
        /// <returns></returns>
        Task<bool> ChangeTimeListAsync(List<TimeVModel> data);
        /// <summary>
        /// 获取一段时间内放号量数据
        /// </summary>
        /// <returns></returns>
        Task<List<DataVModelR>> GetDateListAsync(string date);
        /// <summary>
        /// 获取预约记录
        /// </summary>
        /// <returns></returns>
        Task<List<RecordVModel>> GetRecordListAsync(string startdate, string enddate);
        /// <summary>
        /// 修改时间段下放号量数据
        /// </summary>
        /// <returns></returns>
        Task<bool> ChangeTimeValue(List<PeriodVModel> data);
        
    }
}
