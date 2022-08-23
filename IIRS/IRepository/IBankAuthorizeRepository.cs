using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IBankAuthorizeRepository :IBaseRepository<BankAuthorize>
    {
        Task<List<BankAuthorize>> GetBankAuthorizeList(string documentnumber, int status);

        Task<List<BankAuthorize>> GetBankAuthorizeList();

        Task<PageModel<BankAuthorize>> GetAuthorizationListToPage(int intPageIndex, string zjhm, string jbr,int flowId);


    }
}
