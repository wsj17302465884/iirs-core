using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Services.Base;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class DYServices : BaseServices, IDYServices
    {
        private readonly ILogger<IDYServices> _logger;

        IDBTransManagement _dbTransManagement;
        public DYServices(IDBTransManagement dbTransManagement, ILogger<IDYServices> logger) : base(dbTransManagement)
        {
            this._logger = logger;
            this._dbTransManagement = dbTransManagement;
        }

        /// <summary>
        /// 查询现实手退回原因
        /// </summary>
        /// <param name="AuzID">订单主键编号</param>
        /// <param name="flowID">流程节点编号</param>
        /// <returns></returns>
        public async Task<IFLOW_ACTION_BACK> FlowBackReason(string AuzID, int flowID)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            return await base.Db.Queryable<IFLOW_ACTION_BACK>().Where(it => SqlFunc.Subqueryable<REGISTRATION_INFO>().Where(s => s.XID == it.XID && s.NEXT_XID == null && s.AUZ_ID == AuzID).Any()).SingleAsync(b => b.FLOW_ID == flowID);
        }

        /// <summary>
        /// 查询退回原因
        /// </summary>
        /// <param name="XID">流程流水号</param>
        /// <returns></returns>
        public async Task<IFLOW_ACTION_BACK> FlowBackReason(string XID)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            return await base.Db.Queryable<IFLOW_ACTION_BACK>().Where(it => it.XID== XID).SingleAsync();
        }

        public async Task<V_CX_DyVModel> GetBdczmPdfInfo(string dySlbh)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_BDC);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            return await base.Db.Queryable<V_CX_DyVModel>().Where(s=>s.SLBH==dySlbh).SingleAsync();
        }

        /// <summary>
        /// 保存附件文件数据库信息
        /// </summary>
        /// <param name="files">文件信息</param>
        /// <returns></returns>
        public async Task<int> SaveUploadFileDB(List<PUB_ATT_FILE> files)
        {
            if (files.Count > 0)
            {
                base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
                string slbh = files[0].BAK_PK;
                //base.Db.Aop.OnLogExecuting = (sql, pars) =>
                //{
                //    _logger.LogDebug(sql);
                //};
                try
                {
                    base.Db.Deleteable<PUB_ATT_FILE>().Where(it => it.BAK_PK == slbh).ExecuteCommand();
                    await base.Db.Insertable(files.ToArray()).ExecuteReturnIdentityAsync();
                }
                catch (Exception ex)
                {
                    string ee = ex.Message;
                }

                return 1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// 查询不动产相关登记信息
        /// </summary>
        /// <param name="bdzcz">不动作证明号</param>
        /// <returns></returns>
        public async Task<DyVModel> GetHouseInfo(List<string> bdzcz)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            var sysDicList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 1)).ToListAsync();
            //sysDicList.Cast<SYS_DIC>().Select(s=>new { DEFINED_CODE=s.DEFINED_CODE, DNAME=s.DNAME }).ToDictionary<>

            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_BDC);
            var result = await base.Db.Queryable<DJ_DJB, DJ_SJD, DJ_QLRGL, DJ_QLR>((B, S, QLRGL, QLR) => new object[] { JoinType.Inner, B.SLBH == S.SLBH, JoinType.Inner, B.SLBH == QLRGL.SLBH, JoinType.Inner, QLR.QLRID == QLRGL.QLRID })
.Where((B, S, QLRGL, QLR) => ((B.LIFECYCLE == null || B.LIFECYCLE == 0) && QLRGL.QLRLX == "权利人"
&& (QLRGL.LIFECYCLE == null || QLRGL.LIFECYCLE == 0)
&& bdzcz.Contains(B.BDCZH)))
.Select((B, S, QLRGL, QLR) => new { BDCZH = B.BDCZH, SLBH = B.SLBH, QLRMC = QLR.QLRMC, ZJLB = QLR.ZJLB, GYFE = QLRGL.GYFE, DH = QLR.DH, ZJHM = QLR.ZJHM, ZL = S.ZL, ZSLX = B.ZSLX }).ToListAsync();
            DyVModel dyData = new DyVModel();
            Dictionary<string, int> dysw = await this.GetDysw(bdzcz);
            //var houseInfo = result.Select(s => new { BDCZH = s.BDCZH, SLBH = s.SLBH, ZSLX = s.ZSLX, QLRMC = s.QLRMC, ZL = s.ZL }).Distinct();
            var houseInfo = result.Select(s => new { BDCZH = s.BDCZH, SLBH = s.SLBH, ZSLX = s.ZSLX, ZL = s.ZL }).Distinct();
            foreach (var d in houseInfo)
            {
                dyData.house.Add(new DyHouseVModel
                {
                    BDCZH = d.BDCZH,
                    SLBH = d.SLBH,
                    ZL = d.ZL,
                    ZSLX = d.ZSLX,
                    SW = dysw.Keys.Contains(d.BDCZH) ? dysw[d.BDCZH] : 0
                });
            }
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            var resultDYR = await base.Db.Queryable<DJ_QLRGL, DJ_DJB>((Q, DJB) => new object[] { JoinType.Inner, Q.SLBH == DJB.SLBH })
.Where((Q, DJB) => ((Q.LIFECYCLE == null || Q.LIFECYCLE == 0) && Q.QLRLX == "权利人" && bdzcz.Contains(DJB.BDCZH)))
.GroupBy((Q, DJB) => new { QLRMC = Q.QLRMC, ZJLB = Q.ZJLB, ZJHM = Q.ZJHM })
.Select((Q, DJB) => new { QLRMC = Q.QLRMC, ZJLB = Q.ZJLB, ZJHM = Q.ZJHM, QLRID = SqlFunc.AggregateMax(Q.QLRID), SXH = Convert.ToInt32(SqlFunc.AggregateMax(Q.SXH)), DH = SqlFunc.AggregateMax(Q.DH) }).ToListAsync();
            int sxh = 1;
            foreach (var d in resultDYR)
            {
                var zjlb_zwmObj = sysDicList.Where(s => s.DEFINED_CODE == d.ZJLB).FirstOrDefault();
                dyData.person.Add(new DyPersonVModel
                {
                    QLRMC = d.QLRMC,
                    ZJLB = d.ZJLB,
                    ZJLB_ZWM = zjlb_zwmObj != null ? zjlb_zwmObj.DNAME : string.Empty,
                    ZJHM = d.ZJHM,
                    QLRID = d.QLRID,
                    IS_OWNER = 1,//默认不动产库查询都是权属人
                    //SXH = d.SXH,
                    SXH = sxh,
                    DH = d.DH
                });
                sxh++;
            }
            return dyData;
        }

        public string GetSLBH()
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            string slbh = string.Concat(base.Db.Ado.GetScalar("SELECT SLBH_SEQNUM() FROM DUAL"));//获取系统受理编号
            return slbh;
        }

        /// <summary>
        /// 初始化抵押上报文件格式信息
        /// </summary>
        /// <returns></returns>
        public async Task<MediasVModel> GetInitMedias(int GID)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            var sysDicList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == GID)).ToListAsync();
            MediasVModel medias = new MediasVModel();
            foreach (var pathDic in sysDicList)
            {
                medias.Groups.Add(new MediasGroups()
                {
                    GroupsID = pathDic.DIC_ID,
                    GroupsName = pathDic.DNAME,
                    Items = null
                });
            }
            return medias;
        }

        /// <summary>
        /// 查询房产抵押顺位
        /// </summary>
        /// <param name="bdczh">不动产证号</param>
        /// <returns></returns>
        public async Task<Dictionary<string, int>> GetDysw(List<string> bdczh)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_BDC);
            var resultSW = await base.Db.Queryable<DJ_DY, DJ_XGDJGL, DJ_DJB>((DY, GL, DJB) => new object[] { JoinType.Left, DY.SLBH == GL.ZSLBH, JoinType.Left, DJB.SLBH == GL.FSLBH })
.Where((DY, GL, DJB) => ((DY.LIFECYCLE == null || DY.LIFECYCLE == 0) && bdczh.Contains(DJB.BDCZH) && "抵押".Contains(GL.BGLX)))
.GroupBy((DY, GL, DJB) => new { BDCZH = DJB.BDCZH })
.Select((DY, GL, DJB) => new { BDCZH = DJB.BDCZH, SW = Convert.ToInt32(SqlFunc.AggregateCount(DY.SLBH)) }).ToListAsync();
            Dictionary<string, int> sw = new Dictionary<string, int>();
            foreach (var d in resultSW)
            {
                sw.Add(d.BDCZH, d.SW);
            }
            return sw;
        }

        /// <summary>
        /// 查询附件信息
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        public async Task<List<PUB_ATT_FILE>> UploadFileQuery(string slbh)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            return await base.Db.Queryable<PUB_ATT_FILE>().Where(it => SqlFunc.Subqueryable<REGISTRATION_INFO>().Where(s => s.XID == it.XID && s.NEXT_XID == null && s.SLBH == slbh).Any()).ToListAsync();
        }

        /// <summary>
        /// 查询银行端抵押信息
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        public async Task<HouseVModel> MortgageQuery(string slbh)
        {
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            var regInfo = await base.Db.Queryable<REGISTRATION_INFO>().SingleAsync(s => s.YWSLBH == slbh && s.NEXT_XID == null);
            if (regInfo != null)
            {
                string xssXID = regInfo.XID;//现实手XID
                HouseVModel vModel = new HouseVModel();
                vModel.SLBH = regInfo.YWSLBH;
                vModel.DYLXRDH = regInfo.TEL;
                vModel.AUZ_ID = regInfo.AUZ_ID;
                vModel.HTH = regInfo.HTH;
                DY_INFO dyET = await base.Db.Queryable<DY_INFO>().SingleAsync(s => s.XID == xssXID);
                if (regInfo != null)
                {
                    vModel.DJYY = dyET.DJYY;
                    vModel.ZL = dyET.ZLXX;
                    vModel.DYLX= dyET.DYLX;
                    vModel.DYLX = dyET.DYLX;
                    if (dyET.DYSW != null)
                    {
                        vModel.DYSW = Convert.ToInt32(dyET.DYSW);
                    }
                    vModel.DYFS = dyET.DYFS;
                    if (dyET.DYMJ != null)
                    {
                        vModel.dyMJ = Convert.ToDecimal(dyET.DYMJ);
                    }
                    if (dyET.BDBZZQSE != null)
                    {
                        vModel.BDBZQSE = Convert.ToInt32(dyET.BDBZZQSE);
                    }
                    if (dyET.PGJE != null)
                    {
                        vModel.PGJE = Convert.ToDecimal(dyET.PGJE);
                    }
                    vModel.DYLXR = dyET.LXR;
                    vModel.DYLXRDH = dyET.LXRDH;
                    vModel.BZ= dyET.FJ;

                    var result = await base.Db.Queryable<TSGL_INFO, XGDJGL_INFO>((TS, GL) => new object[] { JoinType.Inner, TS.SLBH == GL.ZSLBH }).Where((TS, GL) => (TS.XID == xssXID))
.Select((TS, GL) => new { BDCLX = TS.BDCLX, TSTYBM = TS.TSTYBM, BDCDYH = TS.BDCDYH, ZSLX = GL.XGZLX, BDCZH = GL.XGZH }).ToListAsync();
                    foreach(var r in result)
                    {
                        vModel.selectDyHouse.Add(new DyHouseVModel()
                        {
                            BDCDYH = r.BDCDYH,
                            BDCLX = r.BDCLX,
                            BDCZH = r.BDCZH,
                            TSTYBM = r.TSTYBM,
                            ZSLX = r.ZSLX
                        });
                    }
                    var resultQLR = await base.Db.Queryable<QLRGL_INFO>().Where(it => it.XID == xssXID && it.QLRLX == "抵押人").OrderBy("SXH").ToListAsync();
                    int slx = 100;
                    foreach(var qlr in resultQLR)
                    {
                        if (qlr.SXH != null)
                        {
                            slx = Convert.ToInt32(qlr.SXH);
                        }
                        else
                        {
                            slx++;
                        }
                        vModel.selectDyPerson.Add(new DyPersonVModel()
                        {
                            ZJHM = qlr.ZJHM,
                            DH = qlr.DH,
                            QLRID = qlr.QLRID,
                            QLRMC = qlr.QLRMC,
                            ZJLB = qlr.ZJLB,
                            SXH = slx
                        });
                    }
                    return vModel;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 提交当前流程节点处理状态
        /// </summary>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        public async Task<int> UpdateFinish(string slbh)
        {
            int count = await base.Db.Updateable<REGISTRATION_INFO>().SetColumns(it => new REGISTRATION_INFO() { IS_ACTION_OK = 1, })
                .Where(it => it.YWSLBH == slbh && it.NEXT_XID == null).ExecuteCommandAsync();
            return count;
        }

        /// <summary>
        /// 修改该受理编号下文件信息
        /// </summary>
        /// <param name="uploadFiles">文件列表</param>
        /// <param name="slbh">受理编号</param>
        /// <returns></returns>
        public async Task<int> UpdateFile(List<PUB_ATT_FILE> uploadFiles, string slbh)
        {
            try
            {
                var regInfo = await base.Db.Queryable<REGISTRATION_INFO>().SingleAsync(E => E.YWSLBH == slbh && E.NEXT_XID == null);//取得现实手注册信息数据
                if (regInfo == null)
                {
                    throw new ApplicationException("错误(或不存在)的受理编号");
                }
                this._dbTransManagement.BeginTran();
                int count = 0;
                string xid = regInfo.XID;
                await base.Db.Deleteable<PUB_ATT_FILE>().Where(w => w.XID == xid).ExecuteCommandAsync();
                foreach (var file in uploadFiles)
                {
                    file.XID = xid;
                }
                count = await base.Db.Insertable(uploadFiles.ToArray()).ExecuteReturnIdentityAsync();
                //if (uploadFiles == null)//如果上传文件为空则客户端清空所以上传文件
                //{
                //    await base.Db.Deleteable<PUB_ATT_FILE>().Where(w => w.XID == xid).ExecuteCommandAsync();
                //}
                //else
                //{
                //    count = await base.Db.Deleteable<PUB_ATT_FILE>().In(it => it.XID, uploadFiles.Cast<PUB_ATT_FILE>().Select(s => s.BUS_PK).ToArray()).ExecuteCommandAsync();
                //    count = await base.Db.Insertable(uploadFiles.ToArray()).ExecuteReturnIdentityAsync();
                //}
                this._dbTransManagement.CommitTran();
                return count;
            }
            catch (Exception ex)
            {
                this._dbTransManagement.RollbackTran();
                throw ex;
            }
        }

        /// <summary>
        /// 一般抵押业务
        /// </summary>
        /// <param name="DjInfo">登记信息</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="TsglInfo">图属信息</param>
        /// <param name="DyInfo">抵押信息</param>
        /// <param name="XgdjglInfos">相关登记关联信息</param>
        /// <param name="qlrglInfos">权利人信息</param>
        /// <param name="uploadFiles">附件信息</param>
        /// <returns>多表操作影响记录数之和</returns>
        public async Task<int> Mortgage(REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, DY_INFO DyInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, List<PUB_ATT_FILE> uploadFiles)
        {
            
            base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
            try
            {
                int count = 0;
                this._dbTransManagement.BeginTran();
                //base.Db.Aop.OnLogExecuting = (sql, pars) =>
                //{
                //    _logger.LogDebug(sql);
                //};
                var delData = base.Db.Queryable<REGISTRATION_INFO>().Single(s => s.YWSLBH == DjInfo.YWSLBH && s.NEXT_XID == null);//取得当前受理编号现实手信息
                if (delData != null && !string.IsNullOrEmpty(delData.XID))//如果存在置为历史
                {
                    count += await base.Db.Updateable<REGISTRATION_INFO>().SetColumns(it => new REGISTRATION_INFO() { NEXT_XID = DjInfo.XID })
.Where(it => it.XID == delData.XID).ExecuteCommandAsync();
                }
                count = await base.Db.Insertable(DjInfo).InsertColumns(it => new
                {
                    it.XID,
                    it.YWSLBH,
                    it.DJZL,
                    it.BDCZH,
                    it.SLBH,
                    it.QLRMC,
                    it.ZL,
                    it.ORG_ID,
                    it.USER_ID,
                    it.TEL,
                    it.HTH,
                    it.AUZ_ID,
                    it.SaveDataJson
                }).ExecuteCommandAsync();
                count += await base.Db.Insertable(DyInfo).InsertColumns(it => new
                {
                    it.SLBH,
                    it.YWSLBH,
                    it.DJLX,
                    it.DJYY,
                    it.XGZH,
                    it.SQRQ,
                    it.DYLX,
                    it.DYSW,
                    it.DYFS,
                    it.DYMJ,
                    it.BDBZZQSE,
                    it.PGJE,
                    it.HTH,
                    it.LXR,
                    it.LXRDH,
                    it.CNSJ,
                    //it.SJR,
                    it.FJ,
                    //it.ZWR,
                    //it.ZWRZJH,
                    //it.ZWRZJLX,
                    it.DLJGMC,
                    it.QLQSSJ,
                    it.QLJSSJ,
                    it.DYQX,
                    it.XID
                }).ExecuteCommandAsync();
                count += await base.Db.Insertable(flowInfo).InsertColumns(fw => new
                {
                    fw.PK,
                    fw.FLOW_ID,
                    fw.AUZ_ID,
                    fw.CDATE,
                    fw.USER_NAME
                }).ExecuteCommandAsync();
                int status = flowInfo.FLOW_ID;
                await base.Db.Updateable<BankAuthorize>().SetColumns(it => new BankAuthorize() { STATUS = status })
.Where(it => it.BID == DjInfo.AUZ_ID).ExecuteCommandAsync();
                count += await base.Db.Insertable(TsglInfo.ToArray()).InsertColumns(ts => new
                {
                    ts.GLBM,
                    ts.SLBH,
                    ts.BDCLX,
                    ts.TSTYBM,
                    ts.BDCDYH,
                    ts.DJZL,
                    ts.CSSJ,
                    ts.XID
                }).ExecuteCommandAsync();
                count += await base.Db.Insertable(XgdjglInfos.ToArray()).InsertColumns(djgl => new
                {
                    djgl.BGBM,
                    djgl.ZSLBH,
                    djgl.FSLBH,
                    djgl.BGRQ,
                    djgl.BGLX,
                    djgl.XGZLX,
                    djgl.XGZH,
                    djgl.XID
                }).ExecuteCommandAsync();
                count += await base.Db.Insertable(qlrglInfos.ToArray()).InsertColumns(qlrgl => new
                {
                    qlrgl.GLBM,
                    qlrgl.SLBH,
                    qlrgl.YWBM,
                    qlrgl.QLRID,
                    qlrgl.QLRMC,
                    qlrgl.ZJHM,
                    qlrgl.ZJLB,
                    qlrgl.ZJLB_ZWM,
                    qlrgl.DH,
                    qlrgl.QLRLX,
                    qlrgl.SXH,
                    qlrgl.IS_OWNER,
                    qlrgl.XID
                }).ExecuteCommandAsync();
                if (uploadFiles != null)
                {
                    count = await base.Db.Insertable(uploadFiles.ToArray()).ExecuteReturnIdentityAsync();
                }
                this._dbTransManagement.CommitTran();
                return count;
            }
            catch (Exception ex)
            {
                this._dbTransManagement.RollbackTran();
                _logger.LogError(ex, ex.Message);
                throw ex;
            }
        }

        //public async Task<int> MortgageQuery(string slbh,List<TSGL_INFO> TsglInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos)
        //{
        //    base.ChangeDB(IIRS.Utilities.Common.SysConst.DB_CON_IIRS);
        //    try
        //    {
        //        REGISTRATION_INFO DjInfo = base.Db.Queryable<REGISTRATION_INFO>().Single(s => s.YWSLBH == slbh);
        //        if (DjInfo != null)
        //        {
        //            DY_INFO DjInfo = base.Db.Queryable<DY_INFO>().Single(s => s.SLBH == slbh);
        //        }

        //        #region 更新图属关联，先删除后插入数据
        //        BankAuthorize bnk = new BankAuthorize()
        //        {
        //            BID = DjInfo.AUZ_ID,
        //            STATUS = flowInfo.FLOW_ID
        //        };
        //        await base.Db.Updateable(bnk).UpdateColumns(it => new { it.STATUS }).ExecuteCommandAsync();
        //        if (TsglInfo.Count > 0)
        //        {
        //            count = await base.Db.Deleteable<TSGL_INFO>().In(it => it.SLBH, TsglInfo.Cast<TSGL_INFO>().Select(s => s.SLBH).ToArray()).ExecuteCommandAsync();
        //            count = await base.Db.Insertable(TsglInfo.ToArray()).InsertColumns(ts => new
        //            {
        //                ts.GLBM,
        //                ts.SLBH,
        //                ts.BDCLX,
        //                ts.TSTYBM,
        //                ts.BDCDYH,
        //                ts.DJZL,
        //                ts.CSSJ
        //            }).ExecuteReturnIdentityAsync();
        //        }
        //        if (XgdjglInfos.Count > 0)
        //        {
        //            count = await base.Db.Deleteable<XGDJGL_INFO>().In(it => it.ZSLBH, XgdjglInfos.Cast<XGDJGL_INFO>().Select(s => s.ZSLBH).ToArray()).ExecuteCommandAsync();
        //            count = await base.Db.Insertable(XgdjglInfos.ToArray()).InsertColumns(djgl => new
        //            {
        //                djgl.BGBM,
        //                djgl.ZSLBH,
        //                djgl.FSLBH,
        //                djgl.BGRQ,
        //                djgl.BGLX,
        //                djgl.XGZLX,
        //                djgl.XGZH
        //            }).ExecuteReturnIdentityAsync();
        //        }
        //        if (qlrglInfos.Count > 0)
        //        {
        //            count = await base.Db.Deleteable<QLRGL_INFO>().In(it => it.SLBH, qlrglInfos.Cast<QLRGL_INFO>().Select(s => s.SLBH).ToArray()).ExecuteCommandAsync();
        //            count = await base.Db.Insertable(qlrglInfos.ToArray()).InsertColumns(qlrgl => new
        //            {
        //                qlrgl.GLBM,
        //                qlrgl.SLBH,
        //                qlrgl.YWBM,
        //                qlrgl.QLRID,
        //                qlrgl.QLRMC,
        //                qlrgl.ZJHM,
        //                qlrgl.ZJLB,
        //                qlrgl.DH,
        //                qlrgl.QLRLX,
        //                qlrgl.SXH,
        //                qlrgl.IS_OWNER
        //            }).ExecuteReturnIdentityAsync();
        //        }
        //        if (uploadFiles != null)
        //        {
        //            count = await base.Db.Deleteable<PUB_ATT_FILE>().In(it => it.BUS_PK, uploadFiles.Cast<PUB_ATT_FILE>().Select(s => s.BUS_PK).ToArray()).ExecuteCommandAsync();
        //            count = await base.Db.Insertable(uploadFiles.ToArray()).ExecuteReturnIdentityAsync();
        //        }
        //        #endregion
        //        this._dbTransManagement.CommitTran();
        //        return count;
        //    }
        //    catch (Exception ex)
        //    {
        //        this._dbTransManagement.RollbackTran();
        //        _logger.LogError(ex, ex.Message);
        //        throw ex;
        //    }
        //}
    }
}
