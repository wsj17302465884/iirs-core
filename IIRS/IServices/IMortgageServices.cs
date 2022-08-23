using IIRS.IServices.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface IMortgageServices :IBaseServices
    {
        Task<PageModel<MortgageViewModel>> QueryMortgageList(int intPageIndex, int intPageSize, string tstybm);

        Task<MessageModel<List<MortgageViewModel>>> GetTstybmByZjhm(string zjhm);

        Task<MessageModel<List<MortgageViewModel>>> GetTstybmCountByZjhm(string zjhm);

        Task<MessageModel<List<MortgageViewModel>>> GetTstybmCountByQlrmc(string qlrmc);

        Task<List<HouseAuthorizeVModel>> GetHouseMessages(string Documentnumber);

        /// <summary>
        /// 根据身份证号和状态查询授权数据
        /// </summary>
        /// <param name="documentnumber"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        Task<List<BankAuthorize>> GetBankAuthorizes(string documentnumber, int status);

        Task<List<DJ_TSGL>> GetTstybmByBdczmh(string bdczmh);

        Task<List<IFLOW_ACTION>> GetFlowActionList(short groupId);

        #region 内网使用
        Task<List<DJ_TSGL>> GetTstybmListByZjhm(string zjhm,string bdczh,string zjlb);

        Task<List<DJ_TSGL>> GetTstybmListByEnterpriseZjhm(string zjhm, string bdczh,int queryval);

        Task<int> SaveBankauthorize(BankAuthorize bank, List<OrderHouseAssociation> orderHouseList);

        Task<string> SaveReturnBid(BankAuthorize bank, List<OrderHouseAssociation> orderHouseList,IFLOW_DO_ACTION iflowDoAction);

        Task<List<OrderHouseAssociation>> GetDoSubmit(string tstybm);
        #endregion



    }
}
