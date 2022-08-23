using IIRS.IServices.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface IAuthorizationServices : IBaseServices
    {
        Task<int> Authorization(BankAuthorize bank, REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, List<OrderHouseAssociation> ationList, DY_INFO ParcelInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos);

        Task<List<OrderHouseAssociation>> GetAuthorizationHouseList(string bid,int flowId);

        Task<List<QLRGL_INFO>> GetAuthorizationDyrList(string slbh);

        Task<List<DY_INFO>> GetDyInfoModel(string slbh);

        Task<int> DeleteAuthorizationInfo(string bid,string slbh);

        Task<int> Authorization(REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, List<OrderHouseAssociation> ationList, DY_INFO ParcelInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos);

        Task<List<PrintHouseVModel>> GetPrintHouseModels(string bdczmh);
        //Task<PageModel<BankAuthorize>> GetAuthorizationListToPage(int intPageIndex, string zjhm, string jbr, int flowId);
        Task<PageModel<BankAuthorize>> GetAuthorizationListToPage(int intPageIndex, string zjhm, string userId, int flowId);
        Task<List<IFLOW_ACTION_GROUP>> GetIflowGroupList();
        Task<List<IFLOW_ACTION>> GetFlowNameByGid(int gid);



    }
}
