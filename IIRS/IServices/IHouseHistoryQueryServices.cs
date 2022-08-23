using IIRS.IServices.Base;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC.TraceBack;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface IHouseHistoryQueryServices : IBaseServices
    {
        /// <summary>
        /// 追述附件查询
        /// </summary>
        /// <param name="CXLX">查询类型</param>
        /// <param name="SJLSZT">数据历史状态，1:历史，0：现实</param>
        /// <param name="SLBH">受理编号</param>
        /// <param name="BDCZH">不动产证号（证明号）</param>
        /// <param name="BDCDYH">不动产单元号</param>
        /// <param name="FZRQ">发证日期</param>
        /// <param name="QLRMC">权利人名称</param>
        /// <param name="ZJHM">证件编号</param>
        /// <param name="ZL">坐落</param>
        /// <param name="FJ">附记</param>
        /// <param name="BDCDJZT">不动产登记状态</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns></returns>
        Task<PageStringModel> HouseHistoryQuery(string CXLX, string SJLSZT, string SLBH, string BDCZH
            , string BDCDYH, DateTime? FZRQ, string QLRMC, string ZJHM, string ZL, string FJ
            , string BDCDJZT, int pageIndex, int pageSize);

        /// <summary>
        /// 房屋追述查询
        /// </summary>
        /// <param name="CXLX">查询类型</param>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        Task<HouseRecountListVModel> HouseRecount(string CXLX, string slbh);

        /// <summary>
        /// 房屋业务信息追述
        /// </summary>
        /// <param name="CXLX">查询类型</param>
        /// <param name="tstybm">图属统一编码</param>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        Task<List<El_CascaderTree>> HouseBusinessRecount(string CXLX, string tstybm, string slbh);

        /// <summary>
        /// 获取追溯信息
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <param name="djzl">登记种类</param>
        /// <returns></returns>
        Task<TraceBackVModel> GetTraceBackInfo(string slbh, string djzl);
    }
}
