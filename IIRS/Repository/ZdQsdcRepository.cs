using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Repository.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class ZdQsdcRepository : BaseRepository<ZD_QSDC>, IZdQsdcRepository
    {
        private readonly ILogger<ZdQsdcRepository> _logger;
        public ZdQsdcRepository(IDBTransManagement dbTransManagement, ILogger<ZdQsdcRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<List<ZD_QSDC>> GetZdInfo(string zdtybm)
        {
            ZD_QSDC model = new ZD_QSDC();
            List<ZD_QSDC> modelList = new List<ZD_QSDC>();
            var sysDicTdytList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 8)).ToListAsync();
            var sysDicQllxList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 6)).ToListAsync();
            var sysDicQlxzList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 7)).ToListAsync();
            Expression<Func<ZD_QSDC, bool>> _whereExpression = a => a.ZDTYBM == zdtybm;

            //return await base.Query(a => a.bdczmh.Contains(bdczmh) && a.Dyqr.Contains(dyqr_qlrmc));

            var data = await base.Query(_whereExpression);

            if(data.Count > 0)
            {
                foreach (var item in data)
                {
                    var tdyt = sysDicTdytList.Where(s => s.DEFINED_CODE == item.SJTDYT).FirstOrDefault();
                    var qllx = sysDicQllxList.Where(s => s.DEFINED_CODE == item.QLLX).FirstOrDefault();
                    var qlxz = sysDicQlxzList.Where(s => s.DEFINED_CODE == item.QLXZ).FirstOrDefault();

                    model.ZDTYBM = item.ZDTYBM;
                    model.SJTDYT = tdyt != null ? tdyt.DNAME : string.Empty;
                    model.DYMJ = item.DYMJ;
                    model.FTMJ = item.FTMJ;
                    model.QLLX = qllx != null ? qllx.DNAME : string.Empty;
                    model.QLXZ = qlxz != null ? qlxz.DNAME : string.Empty;
                    model.QLSDFS = item.QLSDFS;
                    model.JZRJL = item.JZRJL;
                    model.JZMD = item.JZMD;
                    model.JZXG = item.JZXG;
                    model.TDZL = item.TDZL;
                    modelList.Add(model);
                }
            }
            return modelList;
        }
    }
}
