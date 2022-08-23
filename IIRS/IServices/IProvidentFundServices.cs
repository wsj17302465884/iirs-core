using IIRS.IServices.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel.IIRS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface IProvidentFundServices : IBaseServices
    {
        Task<List<ProvidentFundModel>> GetProvidentFundList(string slbh);

        ProvidentFundModel GetProvidentFundModel(List<ProvidentFundModel> models, string qlrmc, string zjhm);

        Task<ProvidentFundModel> FundModels(ProvidentFundVModel model);

        Task<ProvidentFundModel> FundModels1(ProvidentFundVModel model);

        Task<ProvidentFundModel> FundModels2(ProvidentFundVModel vModel);
    }
}
