using SqlSugar;

namespace IIRS.IRepository.Base
{
    public interface IDBTransManagement
    {
        SqlSugarClient GetDbClient();
        void BeginTran();
        void CommitTran();
        void RollbackTran();
    }
}
