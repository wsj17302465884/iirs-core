using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.IRepository.TAX;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.EntityModel.Tax;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Models.ViewModel.TAX;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RT.Comb;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 在建工程抵押土地相关信息
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    public class TaxController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        private readonly ILogger<TaxController> _logger;

        private readonly ITaxAddedBuyerRepository _ITaxAddedBuyerRepository;

        private readonly ITaxAddedHomeRepository _ITaxAddedHomeRepository;

        public TaxController(IDBTransManagement dbTransManagement, ILogger<TaxController> logger, ITaxAddedBuyerRepository iTaxAddedBuyerRepository, ITaxAddedHomeRepository iTaxAddedHomeRepository)
        {
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _ITaxAddedBuyerRepository = iTaxAddedBuyerRepository;
            _ITaxAddedHomeRepository = iTaxAddedHomeRepository;
            _logger = logger;
        }

        /// <summary>
        /// 查询办件量
        /// </summary>
        /// <param name="htbh">合同编号</param>
        /// <param name="date">时间</param>
         /// <param name="xmmc">项目名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<HouseInfoVModel>>> GetChartInfo(string htbh, DateTime date,string xmmc)
        {
            try
            {
                List<HouseInfoVModel> houselist = new List<HouseInfoVModel>();
                /* PageModel<HouseInfoVModel> houseinfolist = new PageModel<HouseInfoVModel>();*/
                List<HouseInfoVModel> houseinfolist = new List<HouseInfoVModel>();
                var data = await _ITaxAddedHomeRepository.Query();
                foreach (var item in data)
                {
                    if (item.QSQSZYDX_DM =="20103")
                    {
                        item.QSQSZYDX_DM = "保障性住房";
                    }else if (item.QSQSZYDX_DM == "20104")
                    {
                        item.QSQSZYDX_DM = "其他住房";
                    }
                    else if (item.QSQSZYDX_DM == "20105")
                    {
                        item.QSQSZYDX_DM = "非住房";
                    }
                    else if (item.QSQSZYDX_DM == "20106")
                    {
                        item.QSQSZYDX_DM = "商品住房";
                    }
                    houseinfolist.Add(new HouseInfoVModel()
                    {
                        BDCDYH = item.BDCDYH,
                        DJ = item.DJ,
                        DQYSKJE = item.DQYSKJE,
                        DQYSSKSSYF = item.DQYSSKSSYF,
                        DY = item.DY,
                        DZ = item.DZ,
                        FDC_LH = item.FDC_LH,
                        FH = item.FH,
                        FWJZMJ = item.FWJZMJ,
                        HTJE = item.HTJE,
                        HTQDSJ = item.HTQDSJ,
                        JYJG = item.JYJG,
                        LC = item.LC,
                        QSQSZYDX_DM = item.QSQSZYDX_DM,
                        QSQSZYLB_DM = item.QSQSZYLB_DM,
                        QSQSZYYT_DM = item.QSQSZYYT_DM,
                        SLBH = item.SLBH,
                        TNMJ = item.TNMJ,
                        TAX_HTBH = item.TAX_HTBH,
                        XQ_SWJG_DM = item.XQ_SWJG_DM,
                        postdata = item.POST_DATA,
                    });
                   

                }
                /* houseinfolist.dataCount = data.Count;
                 houseinfolist.page = houseinfolist.page;
                 houseinfolist.pageCount = houseinfolist.pageCount;
                 houseinfolist.PageSize = houseinfolist.PageSize;*/

                /*                return new MessageModel<PageModel<HouseInfoVModel>>()
                                {
                                    msg = "获取成功",
                                    success = true,
                                    response = houseinfolist,
                                };
                            }
                            catch (Exception ex)
                            {
                                return new MessageModel<PageModel<HouseInfoVModel>>()
                                {
                                    msg = ex.Message,
                                    success = false,
                                    response = null,
                                };
                            }*/
                return new MessageModel<List<HouseInfoVModel>>()
                {
                    msg = "获取成功",
                    success = true,
                    response = houseinfolist,
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<List<HouseInfoVModel>>()
                {
                    msg = ex.Message,
                    success = false,
                    response = null,
                };
            }
        }

      
    }
}
