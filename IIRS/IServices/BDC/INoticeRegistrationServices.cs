using IIRS.IServices.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.IServices.BDC
{
    public interface INoticeRegistrationServices : IBaseServices
    {
        /// <summary>
        /// 获取房屋信息
        /// </summary>
        /// <param name="bdcdyh">不动产单元号</param>
        /// <param name="zl">坐落</param>
        /// <param name="intPageIndex">坐落</param>
        /// <param name="PageSize">坐落</param>
        /// <returns></returns>
        Task<PageModel<FC_H_QSDC>> NoticeSelectHouse(string bdcdyh, string zl, int intPageIndex, int PageSize);
        /// <summary>
        /// 预告登记入库
        /// </summary>
        /// <param name="StrInsertModel"></param>
        /// <param name="fileList"></param>
        /// <returns></returns>
        Task<string> InsertNoticePost(NoticeRegistrationVModel StrInsertModel, List<PUB_ATT_FILE> fileList);

        /// <summary>
        /// 获取房产户权籍调查信息
        /// </summary>
        /// <param name="bdcdyh">当前业务不动产单元号</param>
        /// <returns></returns>
        Task<List<fc_h_qsdcVmodel>> GetAdvanceByHouseInfo(string bdcdyh);

        /// <summary>
        /// 获取该合同编号下的网签数据
        /// </summary>
        /// <param name="HTBH">当前业务SLBH</param>
        /// <returns></returns>
        Task<List<V_BDCZJK_WQ_Vmodel>> GetAdvanceByInternetSignInfo(string HTBH);
        
    }
}
