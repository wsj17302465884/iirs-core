using IIRS.IServices.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.EntityModel.Tax;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC.Condition;
using IIRS.Models.ViewModel.TAX;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices.BDC
{
    /// <summary>
    /// 条件查询服务
    /// </summary>
    public interface IConditionQueryServices : IBaseServices
    {        
        Task<PageModel<ConditionQueryResultVModel>> GetQueryResult(string bdcdyh, string bdczh, string slbh, string qlrmc, string zl, string zslx,int intPageIndex, int PageSize);
        Task<PageModel<FC_Z_QSDC>> GetFc_zResult(string bdcdyh, string zl,string zh,string xmmc, int intPageIndex, int PageSize);
        Task<PageModel<fc_hResultVModel>> GetFc_hResult(string tstybm, int intPageIndex, int PageSize,string bdcdyh);
        Task<string> InsertTransferPost(InsertTransferVModel StrInsertModel, InsertTaxVModel StrTaxModel, List<PUB_ATT_FILE> fileList);
        Task<TaxVModel> GetTaxInfo(string tstbm, string xzqh,string zcs,string ghyt,decimal jzmj);

        Task<List<SYS_DIC>> GetPaymentType();

        Task<List<SYS_DIC>> GetPaymentTypeInfo(string itemNote);
        Task<int> InsertTaxPost(InsertTaxVModel model);
    }
}
