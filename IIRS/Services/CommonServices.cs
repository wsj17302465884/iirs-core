using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.EntityModel.LYGGK;
using IIRS.Models.EntityModel.LYWDK;
using IIRS.Models.ViewModel;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class CommonServices : BaseServices, ICommonServices
    {
        private readonly ILogger<ProvidentFundServices> _logger;
        private readonly IDBTransManagement _dbTransManagement;
        public CommonServices(IDBTransManagement dbTransManagement, ILogger<ProvidentFundServices> logger) : base(dbTransManagement)
        {
            _logger = logger;
            _dbTransManagement = dbTransManagement;
        }

        /// <summary>
        /// 查询附件信息
        /// </summary>
        /// <param name="XID">受理现实手编号</param>
        /// <returns></returns>
        public async Task<List<PUB_ATT_FILE>> UploadFileQueryByXID(string XID)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            return await base.Db.Queryable<PUB_ATT_FILE>().Where(it => it.XID == XID).ToListAsync();
        }

        /// <summary>
        /// 人脸识别授权通过
        /// </summary>
        /// <param name="FaceUserInfo"></param>
        /// <returns></returns>
        public int FaceUserRec(FACE_RECOGNITION_AUTHORIZE FaceUserInfo)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            return base.Db.Insertable(FaceUserInfo).ExecuteCommand();
        }

        /// <summary>
        /// 查询当前流程相关信息
        /// </summary>
        /// <param name="XID">流程编号</param>
        /// <returns></returns>
        public async Task<VerifyFlowVModel> FlowInfoQuery(string XID)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            return await base.Db.Queryable<REGISTRATION_INFO, BankAuthorize>((R, B) => R.XID == XID && R.AUZ_ID == B.BID)
.Select((R, B) => new VerifyFlowVModel()
{
    AUZ_ID = R.AUZ_ID,
    NEXT_XID = R.NEXT_XID,
    SLBH = R.SLBH,
    CURRENT_STATUS = B.STATUS,
    PRE_STATUS = B.PRE_STATUS,
    IS_ACTION_OK = R.IS_ACTION_OK
}).SingleAsync();
        }

        /// <summary>
        /// 查询业务表单提交过程JSON数据列表查询(状态：暂存或退回)
        /// </summary>
        /// <param name="FLOW_ID">流程节点编码</param>
        /// <param name="USER_ID">用户ID</param>
        /// <param name="REMARKS1">查询条件1</param>
        /// <param name="REMARKS2">查询条件2</param>
        /// <param name="REMARKS3">查询条件3</param>
        /// <param name="REMARKS4">查询条件4</param>
        /// <param name="REMARKS5">查询条件5</param>
        /// <param name="pageIndex">查询分页页码</param>
        /// <param name="pageSize">每页显示数据数量</param>
        /// <returns></returns>
        public async Task<string> BizJsonDataPageQuery(decimal FLOW_ID, string USER_ID,string REMARKS1, string REMARKS2, string REMARKS3, string REMARKS4, string REMARKS5, int pageIndex, int pageSize)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    string ee = sql;
            //};
            return await base.Db.Queryable<SysDataRecorderModel, REGISTRATION_INFO, BankAuthorize>((R, G, B) => R.BUS_PK == G.XID && G.AUZ_ID == B.BID)
                    .Where((R, G, B) => B.STATUS == FLOW_ID && G.NEXT_XID == null && G.IS_ACTION_OK == 0 && R.USER_ID == USER_ID && R.IS_STOP == 0)
                    //&& SqlFunc.ContainsArray(new string[] { "0", "2" }, G.IS_ACTION_OK)
                    .WhereIF(!string.IsNullOrEmpty(REMARKS1), (R, G, B) => R.REMARKS1.Contains(REMARKS1))
                    .WhereIF(!string.IsNullOrEmpty(REMARKS2), (R, G, B) => R.REMARKS2.Contains(REMARKS2))
                    .WhereIF(!string.IsNullOrEmpty(REMARKS3), (R, G, B) => R.REMARKS3.Contains(REMARKS3))
                    .WhereIF(!string.IsNullOrEmpty(REMARKS4), (R, G, B) => R.REMARKS4.Contains(REMARKS4))
                    .WhereIF(!string.IsNullOrEmpty(REMARKS5), (R, G, B) => R.REMARKS5.Contains(REMARKS5))
                    .Select((R, G, B) => new
                    {
                        RN = SqlFunc.MappingColumn(R.PK, "ROW_NUMBER() OVER(ORDER BY SYSDATE)"),
                        R.PK,
                        R.BUS_PK,
                        R.CDATE,
                        R.REMARKS1,
                        R.REMARKS2,
                        R.REMARKS3,
                        R.REMARKS4,
                        R.REMARKS5,
                        R.USER_ID,
                        R.USER_NAME
                    }).ToJsonPageAsync(pageIndex, pageSize);
        }

        /// <summary>
        /// 查询业务表单提交过程JSON数据查询
        /// </summary>
        /// <param name="XID">流程编号</param>
        /// <returns></returns>
        public async Task<SysDataRecorderModel> BizJsonDataQuery(string XID)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            return await base.Db.Queryable<SysDataRecorderModel>()
                    .Where(R => R.BUS_PK == XID)
                    .SingleAsync();
        }

        /// <summary>
        /// 添加JSON数据
        /// </summary>
        /// <param name="model">添加数据</param>
        /// <returns></returns>
        public async Task<int> BizJsonInsert(SysDataRecorderModel model)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                string ee = sql;
            };
            DateTime delDay = DateTime.Now.AddDays(-4).Date;
            base.Db.Deleteable<SysDataRecorderModel>().Where(it => it.REMARKS5 == "DEL_IMG_EDIT" && it.CDATE <= delDay).ExecuteCommand();
            return await base.Db.Insertable(model).ExecuteCommandAsync();
        }

        /// <summary>
        /// 不动产权利人查询
        /// </summary>
        /// <param name="ZJHM">查询证件号码</param>
        /// <param name="MC">查询人名称</param>
        /// <param name="pageIndex">查询分页页码</param>
        /// <param name="pageSize">查询分页每页长度</param>
        /// <returns>权利人结果集</returns>
        public async Task<PageModel<QLR_VModel>> BdcUserQuery(string ZJHM, string MC, int pageIndex, int pageSize)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var resultZJLB = await base.Db.Queryable<SYS_DIC>().Where(G => G.GID == 1).ToListAsync();
            var dicZjlb = resultZJLB.ToDictionary(x => x.DEFINED_CODE, x => x.DNAME);

            base.ChangeDB(SysConst.DB_CON_BDC);
            RefAsync<int> totalCount = 0;
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    string ee = sql;
            //};
            List<QLR_VModel> result = await base.Db.Queryable<DJ_QLR>()
                .WhereIF(!string.IsNullOrEmpty(ZJHM), s => ZJHM.Contains(s.ZJHM))
                .WhereIF(!string.IsNullOrEmpty(MC), s => MC.Contains(s.QLRMC))
                .Where(s => s.ZJHM != null && s.ZJLB != null && s.QLRMC != null)
                .GroupBy(s => new { s.ZJLB, s.QLRMC, s.ZJHM })
                .Select(s => new QLR_VModel
                {
                    RN = SqlFunc.MappingColumn(s.QLRID, "ROW_NUMBER() OVER(ORDER BY SYSDATE)"),
                    QLRID = SqlFunc.AggregateMax(s.QLRID),
                    ZJLB = s.ZJLB,
                    QLRMC = s.QLRMC,
                    ZJHM = s.ZJHM,
                    DH = SqlFunc.AggregateMax(s.DH)
                }).ToPageListAsync(pageIndex, pageSize, totalCount);
            foreach (var r in result)
            {
                r.ZJLB_ZWM = dicZjlb[r.ZJLB];
            }
            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / pageSize.ObjToDecimal()).ObjToInt();
            return new PageModel<QLR_VModel>()
            {
                dataCount = totalCount,
                page = pageIndex,
                PageSize = pageSize,
                pageCount = pageCount,
                data = result
            };
        }

        /// <summary>
        /// 追述附件查询
        /// </summary>
        /// <param name="SLBH">受理编号</param>
        /// <returns></returns>
        public async Task<El_CascaderNavTree> AttachmentQueryBySlbh(string SLBH)
        {
            base.ChangeDB(SysConst.DB_CON_LYGGK);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            bool exists = false;
            var patchs = new string[] { "FC", "BDC", "TDTD", "TDTX", "TD", "zt" };
            foreach (string patch in patchs)
            {
                exists = SLBH.Contains(patch);
                if (exists)
                {
                    break;
                }
            }
            if (!exists)
            {
                var result = SLBH.Split("-");
                if (result.Length > 0)
                {
                    SLBH = result[0];
                }
                //var result = SLBH.Split(patch, System.StringSplitOptions.RemoveEmptyEntries);
                //if(result.Length>0)
                //{
                //    SLBH = result[0];
                //}
            }
            var treeDic = await base.Db.Queryable<WFM_ATTACHLST>()
                .Where(s => s.PNODE == SLBH).ToListAsync();
            var pNodeList = treeDic.Cast<WFM_ATTACHLST>().Where(s => s.CKIND == "文件" && s.ISUPLOAD == 1).Select(s => s.CID).ToList();
            base.ChangeDB(SysConst.DB_CON_LYWDK);
            var treeNodes = await base.Db.Queryable<DOC_BINFILE>()
                .Where(s => pNodeList.Contains(s.BINID)).ToListAsync();
            El_CascaderNavTree treeDicRoot = null;
            foreach (WFM_ATTACHLST dicRoot in treeDic.Where(s => s.PID == null).OrderBy(s => s.CSORT).ToList())
            {
                treeDicRoot = new El_CascaderNavTree()
                {
                    label = dicRoot.CNAME,
                    value = dicRoot.CID
                };
                break;
            }
            if (treeDicRoot != null)
            {
                InitAttachmentTree(treeDicRoot, treeDic, treeNodes);
            }
            return treeDicRoot;
            
            void InitAttachmentTree(El_CascaderNavTree fTreeViewNode, List<WFM_ATTACHLST> treeDic, List<DOC_BINFILE> treeNodes)
            {
                var queryTreeNodes = treeDic.Where(S => S.PID == fTreeViewNode.value).OrderBy(s => s.CSORT).ThenBy(s => this.StrTryParse(System.Text.RegularExpressions.Regex.Replace(s.CNAME, @"[^0-9]+", "")));

                if (queryTreeNodes.Count() > 0)//查找本级目录下叶子节点并添加
                {
                    foreach (var treeNode in queryTreeNodes)
                    {
                        if (treeNode.CKIND == "文件")
                        {
                            var mediaResult = treeNodes.Where(s => s.BINID == treeNode.CID);
                            if (mediaResult.Count() > 0)
                            {
                                foreach (var result in mediaResult)//主表中类型为"文件"与子表一对一关系
                                {
                                    if (!string.IsNullOrEmpty(result.FTPATH))
                                    {
                                        fTreeViewNode.children.Add(new El_CascaderNavTree()
                                        {
                                            label = result.FILENAME,
                                            value = treeNode.CID,
                                            NavUrl = result.FTPATH
                                        });
                                    }
                                }
                            }
                        }
                        else//"文件夹"
                        {
                            var isExistsSunDic = treeDic.Where(s => s.PID == treeNode.CID);
                            if (isExistsSunDic.Count() > 0)//查询是否有子文件夹，如果没有当前空文件夹不加载
                            {
                                El_CascaderNavTree treeDicEL = new El_CascaderNavTree()
                                {
                                    label = treeNode.CNAME,
                                    value = treeNode.CID,
                                };
                                InitAttachmentTree(treeDicEL, treeDic, treeNodes);
                                fTreeViewNode.children.Add(treeDicEL);
                            }
                        }

                    }
                }

                //var queryTreeNodes = from r in treeDic
                //                     join e in treeNodes on r.CID equals e.BINID
                //                     orderby e.SORTNUM
                //                     where r.PID == fTreeViewNode.value && r.CKIND == "文件"
                //                     select new { FILE_DIC_NAME = r.CNAME, FILE_ID = r.CID, FILE_PATH = e.FTPATH };
            }
        }

        

        private decimal? StrTryParse(string strDecVal)
        {
            decimal temVal = -1;
            if (decimal.TryParse(strDecVal, out temVal))
            {
                return temVal;
            }
            else
            {
                return -1;
            }
        }

        public int SaveFile(LST_FILE FileInfo)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            try
            {
                int count = 0;
             var aaa =    base.Db.Queryable<LST_FILE>().ToList(); 
                this._dbTransManagement.BeginTran();
                if (FileInfo!=null)
                {
                    count = base.Db.Insertable(FileInfo).ExecuteCommand();
                    this._dbTransManagement.CommitTran();
                }
              
                return count;
            }
            catch (Exception ex)
            {
                this._dbTransManagement.RollbackTran();
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
            }

        public async Task<PageModel<fileInfoVModel>> GetFileInfoList(string userName, int pageIndex = 1, int pageSize = 10)
        {
            // SELECT A.USERNAME,A.TITLE,A.CDATE FROM LST_FILE A WHERE USERNAME!= '张金润' ORDER BY A.CDATE DESC
            base.ChangeDB(SysConst.DB_CON_IIRS);
            try
            {
               
                var fileListResult = base.Db.Queryable<LST_FILE>().Where(a=>a.USERNAME== userName).Select(a=>new fileInfoVModel {
                 USERNAME =a.USERNAME,
                CDATE =a.CDATE,
                TITLE = a.TITLE
                }).OrderBy(A => A.CDATE, OrderByType.Desc).ToPageList(pageIndex, pageSize);

                return new PageModel<fileInfoVModel>
                {
                    data = fileListResult,
                    dataCount = 0,
                    PageSize = pageSize,
                    page = pageIndex
                };
            }
            catch (Exception ex)
            {
                this._dbTransManagement.RollbackTran();
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }
        /// <summary>
        /// 获取本年度不动产证书编号计数器值
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> GetBDCZH_NUM()
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            string numStr = string.Concat(await base.Db.Ado.GetScalarAsync("SELECT BDCZH_SEQNUM() FROM DUAL"));
            int num = -1;
            if (int.TryParse(numStr, out num))
            {
                return new string[] { $"辽({DateTime.Now.Year})辽阳市不动产权第{numStr}号", numStr };
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 获取本年度不动产证明编号计数器值
        /// </summary>
        /// <returns></returns>
        public async Task<string[]> GetBDCZMH_NUM()
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            string numStr = string.Concat(await base.Db.Ado.GetScalarAsync("SELECT BDCZMH_SEQNUM() FROM DUAL"));
            int num = -1;
            if (int.TryParse(numStr, out num))
            {
                return new string[] { $"辽({DateTime.Now.Year})辽阳市不动产证明第{numStr}号", numStr };
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 判断不动产证号是否存在
        /// </summary>
        /// <param name="bdczh"></param>
        /// <param name="slbh"></param>
        /// <returns></returns>
        public string GetBdczhIsExist(string bdczh, string slbh)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var data = base.Db.Queryable<DJB_INFO>().Where(i => i.BDCZH == bdczh && i.SLBH != slbh).ToListAsync();
            if (data.Result.Count > 0)
            {
                return "不动产证号重复，请重新获取！";
            }
            else
            {
                return "200";
            }
        }
    }
}
