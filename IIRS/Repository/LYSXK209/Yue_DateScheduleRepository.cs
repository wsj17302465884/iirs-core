using IIRS.IRepository.Base;
using IIRS.IRepository.LYSXK209;
using IIRS.Models.EntityModel.LYSXK209;
using IIRS.Repository.Base;


namespace IIRS.Repository.LYSXK209
{
    public class Yue_DateScheduleRepository : BaseRepository<Yue_DateSchedule>, IYue_DateScheduleRepository
    {
        public Yue_DateScheduleRepository(IDBTransManagement dbTransManagement) : base(dbTransManagement)
        {
        }
       
    }
}
