using IIRS.IServices;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC.TraceBack;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IIRS.Controllers
{
    /// <summary>
    /// 房产查询
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class HouseHistoryQueryController : Controller
    {
        readonly IHouseHistoryQueryServices _IHouseHistoryQueryServices;

        public HouseHistoryQueryController(IHouseHistoryQueryServices houseHistoryQueryServices)
        {
            this._IHouseHistoryQueryServices = houseHistoryQueryServices;
        }

        /// <summary>
        /// 房屋追述查询
        /// </summary>
        /// <param name="CXLX">查询类型</param>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<HouseRecountListVModel>> HouseRecount(string CXLX, string slbh)
        {
            try
            {
                var result = await this._IHouseHistoryQueryServices.HouseRecount(CXLX, slbh);
                return new MessageModel<HouseRecountListVModel>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = result
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<HouseRecountListVModel>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 房屋业务信息追述
        /// </summary>
        /// <param name="CXLX">查询类型</param>
        /// <param name="tstybm">图属统一编码</param>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<El_CascaderTree>>> HouseBusinessRecount(string CXLX, string tstybm, string slbh)
        {
            try
            {
                List<El_CascaderTree> resultQuery = await this._IHouseHistoryQueryServices.HouseBusinessRecount(CXLX, tstybm, slbh);
                return new MessageModel<List<El_CascaderTree>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = resultQuery
                };
            }
            catch (Exception ex)
            {
                //var dicList = await this._SysDicRepository.Query(S => S.GID == GID && S.IS_USED == 1);
                return new MessageModel<List<El_CascaderTree>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 追述附件查询
        /// </summary>
        /// <param name="CXLX">查询类型</param>
        /// <param name="SJLSZT">数据历史状态，1:历史，0：现实</param>
        /// <param name="SLBH">受理编号</param>
        /// <param name="BDCZH">不动产证号（证明号）</param>
        /// <param name="BDCDYH">不动产单元号</param>
        /// <param name="FZRQ">发证日期</param>
        /// <param name="QLRMC">权利人名称</param>
        /// <param name="ZJHM">证件编号</param>
        /// <param name="ZL">坐落</param>
        /// <param name="FJ">附记</param>
        /// <param name="BDCDJZT">不动产登记状态</param>
        /// <param name="pageIndex">分页：页码</param>
        /// <param name="pageSize">分页：每个页码数据量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageStringModel>> HouseHistoryQuery(string CXLX = "", string SJLSZT = "0", string SLBH = "", string BDCZH = ""
            , string BDCDYH = "", DateTime? FZRQ = null, string QLRMC = "", string ZJHM = "", string ZL = "", string FJ = ""
            , string BDCDJZT = "", int pageIndex = 1, int pageSize = 20)
        {
            try
            {
                if (string.IsNullOrEmpty(CXLX))
                {
                    return new MessageModel<PageStringModel>()
                    {
                        msg = "【查询类型】必填项",
                        success = false
                    };
                }
                ZJHM = "";
                var resultPageJson = await this._IHouseHistoryQueryServices.HouseHistoryQuery(CXLX, SJLSZT, SLBH, BDCZH, BDCDYH, FZRQ, QLRMC, ZJHM, ZL, FJ, BDCDJZT, pageIndex, pageSize);
                return new MessageModel<PageStringModel>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = resultPageJson
                };
            }
            catch (Exception ex)
            {
                //var dicList = await this._SysDicRepository.Query(S => S.GID == GID && S.IS_USED == 1);
                return new MessageModel<PageStringModel>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }



        /// <summary>
        /// 获取追溯信息
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <param name="djzl">登记种类</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<TraceBackVModel>> GetTraceBackInfo(string slbh, string djzl)
        {
            try
            {
                var resultQuery = await this._IHouseHistoryQueryServices.GetTraceBackInfo(slbh, djzl);
                return new MessageModel<TraceBackVModel>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = resultQuery
                };
            }
            catch (Exception ex)
            {
                //var dicList = await this._SysDicRepository.Query(S => S.GID == GID && S.IS_USED == 1);
                return new MessageModel<TraceBackVModel>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }
    }
}