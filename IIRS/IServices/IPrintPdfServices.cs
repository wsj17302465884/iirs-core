using IIRS.IServices.Base;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel.IIRS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IServices
{
    public interface IPrintPdfServices : IBaseServices
    {
        Task<PdfVModel> GetPdfInfo(string slbh);
        Task<PdfVModel> GetMrgeReleasePdfInfo(string DySlbh, string NewSlbh);

        Task<List<OrderHouseAssociation>> GetPrintCheckList(string slbh);

        Task<List<OrderHouseAssociation>> GetPrintCheckListCount(string slbh);
    }
}
