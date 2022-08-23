using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IIRS.IRepository;
using IIRS.IRepository.GGK;
using IIRS.Models.EntityModel;
using IIRS.Models.EntityModel.GGK;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Utilities.Filter;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IIRS.Controllers
{
    /// <summary>
    /// 系统字典表信息查询
    /// </summary>
    [ApiController]
    [CustomRoute]
    [Produces("application/json")]
    //[Authorize(Permissions.Name)]
    //[TypeFilter(typeof(ClientIdCheckFilter))]
    public class SysDicController : Controller
    {
        readonly ISysDicRepository _SysDicRepository;
        readonly ILyggk_DicMainRepository _DicMainRepository;
        readonly ILyggk_DicItemRepository _DicItemRepository;

        
        public SysDicController(ISysDicRepository sysDicRepository, ILyggk_DicMainRepository DicMainRepository, ILyggk_DicItemRepository DicItemRepository)
        {
            this._SysDicRepository = sysDicRepository;
            _DicItemRepository = DicItemRepository;
            _DicMainRepository = DicMainRepository;
        }
        /// <summary>
        /// 获取证件类别字典
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<string>> GetDicByName(string zjlbName)
        {
            string zjlb = "";
            try
            {
                if (zjlbName.Trim() == "居民身份证")
                    zjlbName = "身份证";
                var dicList = await this._SysDicRepository.Query(S => S.DNAME == zjlbName && S.IS_USED == 1 && S.GID == 1);
                if (dicList.Count > 0)
                {
                    zjlb = dicList[0].DEFINED_CODE;
                    return new MessageModel<string>()
                    {
                        msg = "数据查询成功",
                        success = true,
                        response = zjlb
                    };
                }
                else
                {
                    return new MessageModel<string>()
                    {
                        msg = "请输入正确的证件中文名",
                        success = false
                    };
                }

            }
            catch (Exception ex)
            {
                return new MessageModel<string>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="GID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> DictionaryInfoQueryByGID(int GID)
        {
            try
            {
                var dicList = await this._SysDicRepository.Query(S => S.GID == GID && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                //var dicList = await this._SysDicRepository.Query(S => S.GID == GID && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
            
        }

        /// <summary>
        /// 获取登记原因
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> GetDjyyByGID()
        {
            try
            {
                var dicList = await this._SysDicRepository.Query(S => S.GID == 12 && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                //var dicList = await this._SysDicRepository.Query(S => S.GID == GID && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }

        }

        /// <summary>
        /// 获取收费标准明细
        /// </summary>
        /// <param name="dicCode">缴费类型ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<Lyggk_DicItem>>> GetDicItemListByDicCode(string dicCode)
        {
            try
            {
                var dicList = await _DicItemRepository.Query(S => S.dicCode == dicCode);
                return new MessageModel<List<Lyggk_DicItem>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                //var dicList = await this._SysDicRepository.Query(S => S.GID == GID && S.IS_USED == 1);
                return new MessageModel<List<Lyggk_DicItem>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }
        /// <summary>
        /// 获取缴费类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<Lyggk_DicMain>>> GetDicMainList()
        {
            try
            {
                var dicList = await _DicMainRepository.Query(S => S.pid == "14a7e992f5b345fd9d2f5d5a33e1a737" && S.dicNote != null);
                return new MessageModel<List<Lyggk_DicMain>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<List<Lyggk_DicMain>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 获取乡镇街道
        /// </summary>
        /// <param name="itemNote">行政区划名字：白塔区</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> GetTownInfo(string itemNote)
        {
            try
            {
                var dicList = await this._SysDicRepository.Query(S => S.itemNote == itemNote && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }
        /// <summary>
        /// 获取行政区域信息
        /// </summary>
        /// <param name="itemVal">SW行政区划:白塔区</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> GetAdministrativeArea(string itemVal)
        {            
            var xzqh_data = await _SysDicRepository.Query(i => i.GID == 25 && i.DNAME == itemVal);
            string itemNote = "2" + xzqh_data[0].DEFINED_CODE;
            try
            {

                var dicList = await this._SysDicRepository.Query(S => S.itemNote == itemNote && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 获取税务机关信息
        /// </summary>
        /// <param name="itemNote">2+行政区划ItemVal:例如"2"+"211002"（白塔区）</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> GetTaxAuthorityInfo(string itemNote)
        {
            var xzqh_data = await _SysDicRepository.Query(i => i.GID == 25 && i.DNAME == itemNote);
            string itemVal = "2" + xzqh_data[0].DEFINED_CODE;
            try
            {
                var dicList = await this._SysDicRepository.Query(S => S.itemNote == itemVal && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }
        /// <summary>
        /// 房屋类别
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> GetHouseType()
        {
            try
            {
                var dicList = await this._SysDicRepository.Query(S => S.GID == 13 && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 权属转移对象
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> GetOwnershipInfo()
        {
            try
            {
                var dicList = await this._SysDicRepository.Query(S => S.GID == 20 && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 交易类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> GetTransactionType()
        {
            try
            {
                var dicList = await this._SysDicRepository.Query(S => S.GID == 22 && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 税务房屋结构
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> GetHouseFwjg()
        {
            try
            {
                var dicList = await this._SysDicRepository.Query(S => S.GID == 17 && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 获取房屋朝向
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> GetHouseCx()
        {
            try
            {
                var dicList = await this._SysDicRepository.Query(S => S.GID == 21 && S.DNAME != null && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 加载树形结构字典信息
        /// </summary>
        /// <param name="GID">分组编号</param>
        /// <returns>结果集</returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> DictionaryTreeQueryByGID(int GID)
        {
            try
            {
                var dicList = await this._SysDicRepository.Query(S => S.GID == GID && S.IS_USED == 1 && S.FDIC_ID == null);
                if (dicList.Count > 0)
                {
                    SetChildrenTree(dicList);
                }

                void SetChildrenTree(List<SYS_DIC> fNodes)
                {
                    var dicSunList = this._SysDicRepository.Query(S => fNodes.Cast<SYS_DIC>().Select(S => S.DIC_ID).ToArray().Contains(S.FDIC_ID) && S.IS_USED == 1).Result;
                    foreach (var sunNode in fNodes)
                    {
                        var childrenResult = dicSunList.Cast<SYS_DIC>().Where(S => S.FDIC_ID == sunNode.DIC_ID);
                        if (childrenResult.Count() > 0)
                        {
                            if (sunNode.children == null)
                            {
                                sunNode.children = new List<SYS_DIC>();
                            }
                            foreach (var node in childrenResult)
                            {
                                sunNode.children.Add(node);
                            }
                        }
                    }
                    if (dicSunList.Count > 0)
                    {
                        SetChildrenTree(dicSunList);
                    }
                }
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        }

        /// <summary>
        /// 获取抵押人类型
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<List<SYS_DIC>>> GetDyrlxType()
        {
            try
            {
                var dicList = await this._SysDicRepository.Query(S => S.GID == 29 && S.DNAME != null && S.IS_USED == 1);
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询成功",
                    success = true,
                    response = dicList
                };
            }
            catch (Exception ex)
            {
                return new MessageModel<List<SYS_DIC>>()
                {
                    msg = "数据查询失败，原因：" + ex.Message,
                    success = false
                };
            }
        } 
    }
}