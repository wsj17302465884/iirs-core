using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.IIRS;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.IRepository
{
    public interface IOrderHouseAssociationRepository : IBaseRepository<OrderHouseAssociation>
    {
        Task<List<OrderHouseAssociation>> GetOrderHouseAssociationList(string bid);

        Task<List<OrderHouseAssociation>> GetDoSubmit(string tstybm);

        Task<List<OrderHouseAssociation>> GetBIdByBdczh(string bdczh);

        Task<int> DeleteOrderHouseAssociation(string bid);
    }
}
