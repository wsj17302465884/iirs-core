using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Repository.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Repository
{
    public class Fc_h_QsdcRepository : BaseRepository<FC_H_QSDC>, IFc_h_QsdcRepository
    {
        private readonly ILogger<Fc_h_QsdcRepository> _logger;
        public Fc_h_QsdcRepository(IDBTransManagement dbTransManagement, ILogger<Fc_h_QsdcRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<PageModel<FC_H_QSDC>> GetFCHList(string lsfwbh, int intPageIndex)
        {
            int j = 0;
            string _strOrderByFileds = "dyh,fjh";
            List<FC_H_QSDC> modelList = new List<FC_H_QSDC>();
            FC_H_QSDC model;
            var sysDicGhytList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 5)).ToListAsync();
            var sysDicQllxList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 4)).ToListAsync();
            var sysDicQlxzList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 3)).ToListAsync();
            

            Expression<Func<FC_H_QSDC, bool>> _whereExpression =
            (a) => (a.LIFECYCLE == 0 || a.LIFECYCLE == null) && a.LSFWBH == lsfwbh;

            

            var data = await base.QueryPage(_whereExpression, intPageIndex, SysConst.SYS_DEFAULT_PAGE_SIZE, _strOrderByFileds);

            if(intPageIndex == 1)
            {
                j = 0;
            }
            else if(intPageIndex == 2)
            {
                j = intPageIndex * 10;
            }
            else
            {
                j = data.PageSize * intPageIndex;
            }


            for (int i = 0; i < data.data.Count; i++)
            {
                model = new FC_H_QSDC();
                var ghyt = sysDicGhytList.Where(s => s.DEFINED_CODE == data.data[i].GHYT).FirstOrDefault();
                var qllx = sysDicQllxList.Where(s => s.DEFINED_CODE == data.data[i].QLLX).FirstOrDefault();
                var qlxz = sysDicQlxzList.Where(s => s.DEFINED_CODE == data.data[i].QLXZ).FirstOrDefault();

                model.xh = j + i + 1;
                model.TSTYBM = data.data[i].TSTYBM;
                model.DYH = data.data[i].DYH;
                model.MYC = data.data[i].MYC;
                model.SJC = data.data[i].SJC;
                model.FJH = data.data[i].FJH;
                model.ZH = data.data[i].ZH;
                model.HH = data.data[i].HH;
                model.BDCDYH = data.data[i].BDCDYH;
                model.ZL = data.data[i].ZL;
                if (data.data[i].YCJZMJ == null)
                {
                    model.YCJZMJ = data.data[i].JZMJ;
                }
                else
                {
                    model.YCJZMJ = data.data[i].YCJZMJ;
                }

                if (data.data[i].YCTNJZMJ == null)
                {
                    model.YCTNJZMJ = data.data[i].TNJZMJ;
                }
                else
                {
                    model.YCTNJZMJ = data.data[i].YCTNJZMJ;
                }

                if (data.data[i].YCFTJZMJ == null)
                {
                    model.YCFTJZMJ = data.data[i].FTJZMJ;
                }
                else
                {
                    model.YCFTJZMJ = data.data[i].YCFTJZMJ;
                }
                model.QLLX = qllx != null ? qllx.DNAME : string.Empty;
                model.QLXZ = qlxz != null ? qlxz.DNAME : string.Empty;
                model.GHYT = ghyt != null ? ghyt.DNAME : string.Empty;
                modelList.Add(model);
            }

            data.data.Clear();
            //data.data.Add(model);
            foreach (var item in modelList)
            {
                data.data.Add(item);
            }
            data.page = data.page;
            data.pageCount = data.pageCount;
            data.PageSize = data.PageSize;
            data.dataCount = data.dataCount;
            return data;
        }
    }
}
