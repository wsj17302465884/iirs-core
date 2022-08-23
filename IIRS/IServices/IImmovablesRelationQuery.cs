using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IIRS.IServices.Base;
using IIRS.Models.ViewModel;

namespace IIRS.IServices
{
    public interface IImmovablesRelationQueryServices : IBaseServices
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="BDCDYH">不动产单元号</param>
        /// <param name="ZSLX">证书类型</param>
        /// <param name="BDCZH">不动产权证书号</param>
        /// <param name="SLBH">登记受理编号</param>
        /// <param name="DY_QLR">权利人</param>
        /// <param name="ZL">坐落</param>
        /// <param name="PageIndex">分页页面</param>
        /// <param name="PageSize">每页行数</param>
        /// <param name="isComputeCount">是否计算总数据量</param>
        /// <returns></returns>
        Task<PageModel<ImmovablesRelationVModel>> ImmovablesRelationQuery(string BDCDYH, int ZSLX, string BDCZH, string SLBH, string DY_QLR, string ZL, int PageIndex, int PageSize, bool isComputeCount);
    }
}
