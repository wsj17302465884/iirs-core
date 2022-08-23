using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.ViewModel;
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
    public class HouseStatusRepository : BaseRepository<HouseStatusViewModel>,IHouseStatusRepository
    {
        private readonly ILogger<HouseStatusRepository> _logger;
        public HouseStatusRepository(IDBTransManagement dbTransManagement, ILogger<HouseStatusRepository> logger) : base(dbTransManagement)
        {
            _logger = logger;
        }

        public async Task<List<HouseStatusViewModel>> AddhouseList(List<HouseStatusViewModel> modelList)
        {
            //throw new NotImplementedException();
            await base.Add(modelList);
            return modelList;
        }

        public async Task<int> AddviewModel(HouseStatusViewModel model)
        {
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            return await base.Add(model); 
        }

        





        /// <summary>
        /// 获取房屋的所有基本属性与状态
        /// </summary>
        /// <param name="tstybm">图属统一编码</param>
        /// <returns></returns>
        public async Task<MessageModel<List<HouseStatusViewModel>>> GetHouseSatausList(string tstybm)
        {
            //日志
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql); 
            };

            try
            {
                var data  = await base.Query(a => tstybm.Split(new char[] { ',' }).Contains(a.Tstybm));
                return new MessageModel<List<HouseStatusViewModel>>()
                {
                    msg = "获取成功",
                    success = true,
                    response = data
                };
            }
            catch (Exception ex)
            {
                string errorDynCode = Guid.NewGuid().ToString();
                _logger.LogDebug($"错误码:{errorDynCode},异常消息:{ex.Message}");
                return new MessageModel<List<HouseStatusViewModel>>()
                {
                    msg = "系统错误，请与管理员联系，错误编码：" + errorDynCode,
                    success = false,
                    response = null
                };
            }
        }

        public async Task<List<HouseStatusViewModel>> GetHouseSatausList_New(string tstybm)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            return await base.Query(a => tstybm.Split(new char[] { ',' }).Contains(a.Tstybm));
        }

        /// <summary>
        /// 获取个人抵押房屋分页
        /// </summary>
        /// <param name="tstybm"></param>
        /// <param name="intPageIndex"></param>
        /// <returns></returns>
        public async Task<PageModel<HouseStatusViewModel>> GetHouseSatausListToPage(string tstybm, int intPageIndex)
        {
            string _strOrderByFileds = "tstybm";

            Expression<Func<HouseStatusViewModel, bool>> _whereExpression = a => tstybm.Split(new char[] { ',' }).Contains(a.Tstybm);

            var data = await base.QueryPage(_whereExpression, intPageIndex, SysConst.SYS_DEFAULT_PAGE_SIZE, _strOrderByFileds);

            HouseStatusViewModel model = new HouseStatusViewModel();
            List<HouseStatusViewModel> models = new List<HouseStatusViewModel>();

            for (int i = 0; i < data.data.Count; i++)
            {
                model = new HouseStatusViewModel();
                model.Tstybm = data.data[i].Tstybm;
                model.Bdclx = data.data[i].Bdclx;
                model.Bdcdyh = data.data[i].Bdcdyh;
                model.DJBZSLX = data.data[i].DJBZSLX;
                model.Slbh = data.data[i].Slbh;
                model.Bdczh = data.data[i].Bdczh;
                model.Zl = data.data[i].Zl;
                model.Jzmj = data.data[i].Jzmj;
                model.Gyfs = data.data[i].Gyfs;
                model.Qllx = data.data[i].Qllx;
                model.Qlxz = data.data[i].Qlxz;
                model.Ghyt = data.data[i].Ghyt;
                model.Fwxg_Jzmj = data.data[i].Fwxg_Jzmj;
                model.Tdyt = data.data[i].Tdyt;
                model.Tdsyqx = data.data[i].Tdsyqx;
                model.TdQsrq = data.data[i].TdQsrq;
                model.TdZzrq = data.data[i].TdZzrq;
                model.Jzzdmj = data.data[i].Jzzdmj;
                model.Dytdmj = data.data[i].Dytdmj;
                model.Gytdmj = data.data[i].Gytdmj;
                model.DjbQt = data.data[i].DjbQt;
                model.DjbDjrq = data.data[i].DjbDjrq;
                model.DjbFzrq = data.data[i].DjbFzrq;
                model.DjbZsxlh = data.data[i].DjbZsxlh;
                model.DjbFj = data.data[i].DjbFj;
                model.DjbFzjg = data.data[i].DjbFzjg;
                model.Bdczmh = data.data[i].Bdczmh;
                model.Dymj = data.data[i].Dymj;
                model.Dymj2 = data.data[i].Dymj2;
                model.Bdbzzqse = data.data[i].Bdbzzqse;
                model.Dyfs = data.data[i].Dyfs;
                model.DyQx = data.data[i].DyQx;
                model.DyQt = data.data[i].DyQt;
                model.DyDjrq = data.data[i].DyDjrq;
                model.Dyqssj = data.data[i].Dyqssj;
                model.Dyjssj = data.data[i].Dyjssj;
                model.DyFzrq = data.data[i].DyFzrq;
                model.DyZsxlh = data.data[i].DyZsxlh;
                model.DyFzjg = data.data[i].DyFzjg;
                model.DyHth = data.data[i].DyHth;
                model.Dyfj = data.data[i].Dyfj;
                model.Cfwh = data.data[i].Cfwh;
                model.Cfjg = data.data[i].Cfjg;
                model.Cfqx = data.data[i].Cfqx;
                model.Cfrq = data.data[i].Cfrq;
                model.CfQssj = data.data[i].CfQssj;
                model.CfJssj = data.data[i].CfJssj;
                model.Cffj = data.data[i].Cffj;
                model.YG_Slbh = data.data[i].YG_Slbh;
                model.YgSqrq = data.data[i].YgSqrq;
                model.YgSpdw = data.data[i].YgSpdw;
                model.YgSprq = data.data[i].YgSprq;
                model.YgBdczmh = data.data[i].YgBdczmh;
                model.YgDjrq = data.data[i].YgDjrq;
                model.YgFzjg = data.data[i].YgFzjg;
                model.YgFzrq = data.data[i].YgFzrq;
                model.YgZsxlh = data.data[i].YgZsxlh;
                model.YgQt = data.data[i].YgQt;
                model.Djzl = data.data[i].Djzl;
                model.Dyr = data.data[i].Dyr;
                model.Dyr_Zjlb = data.data[i].Dyr_Zjlb;
                model.Dyr_Zjhm = data.data[i].Dyr_Zjhm;
                model.Dyqr = data.data[i].Dyqr;
                model.Dyqr_Zjlb = data.data[i].Dyqr_Zjlb;
                model.Dyqr_Zjhm = data.data[i].Dyqr_Zjhm;
                model.Qlrmc = data.data[i].Qlrmc;
                model.Zjlb = data.data[i].Zjlb;
                model.Zjhm = data.data[i].Zjhm;
                model.Ywr = data.data[i].Ywr;
                model.Ywr_Zjlb = data.data[i].Ywr_Zjlb;
                model.Ywr_Zjhm = data.data[i].Ywr_Zjhm;
                model.Yg_Qlrmc = data.data[i].Yg_Qlrmc;
                model.Yg_Zjlb = data.data[i].Yg_Zjlb;
                model.Yg_Zjhm = data.data[i].Yg_Zjhm;
                model.Yg_ywr = data.data[i].Yg_ywr;
                model.Yg_ywr_Zjlb = data.data[i].Yg_ywr_Zjlb;
                model.Yg_ywr_Zjhm = data.data[i].Yg_ywr_Zjhm;
                model.Yy_Qlrmc = data.data[i].Yy_Qlrmc;
                model.Yy_Zjlb = data.data[i].Yy_Zjlb;
                model.Yy_Zjhm = data.data[i].Yy_Zjhm;
                model.Yy_ywr = data.data[i].Yy_ywr;
                model.Yy_ywr_Zjlb = data.data[i].Yy_ywr_Zjlb;
                model.Yy_ywr_Zjhm = data.data[i].Yy_ywr_Zjhm;
                models.Add(model);
            }


            data.data.Clear();
            //data.data.Add(model);
            foreach (var item in models)
            {
                data.data.Add(item);
            }
            data.page = data.page;
            data.pageCount = data.pageCount;
            data.PageSize = data.PageSize;
            data.dataCount = data.dataCount;
            return data;
        }
        /// <summary>
        /// 获取企业抵押房屋分页
        /// </summary>
        /// <param name="tstybm"></param>
        /// <param name="intPageIndex"></param>
        /// <returns></returns>
        public async Task<PageModel<HouseStatusViewModel>> GetEnterpriseHouseSatausListToPage(string tstybm, int intPageIndex)
        {
            string _strOrderByFileds = "tstybm";

            Expression<Func<HouseStatusViewModel, bool>> _whereExpression = a => tstybm.Split(new char[] { ',' }).Contains(a.Tstybm);

            var data = await base.QueryPage(_whereExpression, intPageIndex, SysConst.SYS_DEFAULT_PAGE_SIZE_TEN, _strOrderByFileds);

            HouseStatusViewModel model = new HouseStatusViewModel();
            List<HouseStatusViewModel> models = new List<HouseStatusViewModel>();

            for (int i = 0; i < data.data.Count; i++)
            {
                model = new HouseStatusViewModel();
                model.Tstybm = data.data[i].Tstybm;
                model.Bdclx = data.data[i].Bdclx;
                model.Bdcdyh = data.data[i].Bdcdyh;
                model.DJBZSLX = data.data[i].DJBZSLX;
                model.Slbh = data.data[i].Slbh;
                model.Bdczh = data.data[i].Bdczh;
                model.Zl = data.data[i].Zl;
                model.Jzmj = data.data[i].Jzmj;
                model.Gyfs = data.data[i].Gyfs;
                model.Qllx = data.data[i].Qllx;
                model.Qlxz = data.data[i].Qlxz;
                model.Ghyt = data.data[i].Ghyt;
                model.Fwxg_Jzmj = data.data[i].Fwxg_Jzmj;
                model.Tdyt = data.data[i].Tdyt;
                model.Tdsyqx = data.data[i].Tdsyqx;
                model.TdQsrq = data.data[i].TdQsrq;
                model.TdZzrq = data.data[i].TdZzrq;
                model.Jzzdmj = data.data[i].Jzzdmj;
                model.Dytdmj = data.data[i].Dytdmj;
                model.Gytdmj = data.data[i].Gytdmj;
                model.DjbQt = data.data[i].DjbQt;
                model.DjbDjrq = data.data[i].DjbDjrq;
                model.DjbFzrq = data.data[i].DjbFzrq;
                model.DjbZsxlh = data.data[i].DjbZsxlh;
                model.DjbFj = data.data[i].DjbFj;
                model.DjbFzjg = data.data[i].DjbFzjg;
                model.Bdczmh = data.data[i].Bdczmh;
                model.Dymj = data.data[i].Dymj;
                model.Dymj2 = data.data[i].Dymj2;
                model.Bdbzzqse = data.data[i].Bdbzzqse;
                model.Dyfs = data.data[i].Dyfs;
                model.DyQx = data.data[i].DyQx;
                model.DyQt = data.data[i].DyQt;
                model.DyDjrq = data.data[i].DyDjrq;
                model.Dyqssj = data.data[i].Dyqssj;
                model.Dyjssj = data.data[i].Dyjssj;
                model.DyFzrq = data.data[i].DyFzrq;
                model.DyZsxlh = data.data[i].DyZsxlh;
                model.DyFzjg = data.data[i].DyFzjg;
                model.DyHth = data.data[i].DyHth;
                model.Dyfj = data.data[i].Dyfj;
                model.Cfwh = data.data[i].Cfwh;
                model.Cfjg = data.data[i].Cfjg;
                model.Cfqx = data.data[i].Cfqx;
                model.Cfrq = data.data[i].Cfrq;
                model.CfQssj = data.data[i].CfQssj;
                model.CfJssj = data.data[i].CfJssj;
                model.Cffj = data.data[i].Cffj;
                model.YG_Slbh = data.data[i].YG_Slbh;
                model.YgSqrq = data.data[i].YgSqrq;
                model.YgSpdw = data.data[i].YgSpdw;
                model.YgSprq = data.data[i].YgSprq;
                model.YgBdczmh = data.data[i].YgBdczmh;
                model.YgDjrq = data.data[i].YgDjrq;
                model.YgFzjg = data.data[i].YgFzjg;
                model.YgFzrq = data.data[i].YgFzrq;
                model.YgZsxlh = data.data[i].YgZsxlh;
                model.YgQt = data.data[i].YgQt;
                model.Djzl = data.data[i].Djzl;
                model.Dyr = data.data[i].Dyr;
                model.Dyr_Zjlb = data.data[i].Dyr_Zjlb;
                model.Dyr_Zjhm = data.data[i].Dyr_Zjhm;
                model.Dyqr = data.data[i].Dyqr;
                model.Dyqr_Zjlb = data.data[i].Dyqr_Zjlb;
                model.Dyqr_Zjhm = data.data[i].Dyqr_Zjhm;
                model.Qlrmc = data.data[i].Qlrmc;
                model.Zjlb = data.data[i].Zjlb;
                model.Zjhm = data.data[i].Zjhm;
                model.Ywr = data.data[i].Ywr;
                model.Ywr_Zjlb = data.data[i].Ywr_Zjlb;
                model.Ywr_Zjhm = data.data[i].Ywr_Zjhm;
                model.Yg_Qlrmc = data.data[i].Yg_Qlrmc;
                model.Yg_Zjlb = data.data[i].Yg_Zjlb;
                model.Yg_Zjhm = data.data[i].Yg_Zjhm;
                model.Yg_ywr = data.data[i].Yg_ywr;
                model.Yg_ywr_Zjlb = data.data[i].Yg_ywr_Zjlb;
                model.Yg_ywr_Zjhm = data.data[i].Yg_ywr_Zjhm;
                model.Yy_Qlrmc = data.data[i].Yy_Qlrmc;
                model.Yy_Zjlb = data.data[i].Yy_Zjlb;
                model.Yy_Zjhm = data.data[i].Yy_Zjhm;
                model.Yy_ywr = data.data[i].Yy_ywr;
                model.Yy_ywr_Zjlb = data.data[i].Yy_ywr_Zjlb;
                model.Yy_ywr_Zjhm = data.data[i].Yy_ywr_Zjhm;
                models.Add(model);
            }


            data.data.Clear();
            //data.data.Add(model);
            foreach (var item in models)
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
