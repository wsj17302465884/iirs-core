using IIRS.IRepository.Base;
using IIRS.IRepository.LYSXK209;
using IIRS.Models.EntityModel.LYSXK209;
using IIRS.Repository.Base;
namespace IIRS.Repository.LYSXK209
{
    public class Yue_TypeRepository : BaseRepository<Yue_Type>, IYue_TypeRepository
    {
        public Yue_TypeRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }
    }
}
