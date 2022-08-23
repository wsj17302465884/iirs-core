using IIRS.IRepository.Base;
using IIRS.IRepository.LYSXK209;
using IIRS.Models.EntityModel.LYSXK209;
using IIRS.Repository.Base;

namespace IIRS.Repository.LYSXK209
{
    public class V_BDCZJK_WQ_LSRepository : BaseRepository<V_BDCZJK_WQ_LS>, IV_BDCZJK_WQ_LSRepository
    {
        public V_BDCZJK_WQ_LSRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }
    }
}