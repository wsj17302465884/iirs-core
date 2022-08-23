using IIRS.IRepository.Base;
using IIRS.IRepository.IIRS;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel.BDC.print;
using IIRS.Models.ViewModel.Print;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services.BDC
{
    public class BdcPrintServices : BaseServices, IBdcPrintServices
    {
        private readonly ILogger<BdcPrintServices> _logger;
        private readonly IDBTransManagement _dBTransManagement;
        private readonly IQlrgl_infoRepository _QlrglRepository;
        private readonly IPubAttFileRepository _pubAttFileRepository;

        public BdcPrintServices(IDBTransManagement dbTransManagement, ILogger<BdcPrintServices> logger, IQlrgl_infoRepository QlrglRepository, IPubAttFileRepository pubAttFileRepository) : base(dbTransManagement)
        {
            _logger = logger;
            _dBTransManagement = dbTransManagement;
            _QlrglRepository = QlrglRepository;
            _pubAttFileRepository = pubAttFileRepository;
        }
        /// <summary>
        /// 转移登记 - 不动产登记审批表打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        public async Task<TransferSpbPrintVModel> GetTransferSpbPrint(string xid)
        {
            var GYFE = "";
            TransferSpbPrintVModel model = new TransferSpbPrintVModel();
            string tstybm = "";
            base.ChangeDB(SysConst.DB_CON_IIRS);

            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};

            var data = await base.Db.Queryable<REGISTRATION_INFO, DJB_INFO, SJD_INFO, TSGL_INFO, QL_XG_INFO, IFLOW_ACTION, IFLOW_ACTION_GROUP>
                ((A, B, C, D, E, F, G) => A.XID == B.xid && B.xid == C.XID && C.XID == D.XID && D.XID == E.XID && A.DJZL == F.GROUP_ID && F.GROUP_ID == G.GROUP_ID)
                .Where((A, B, C, D, E, F, G) => A.XID == xid && A.NEXT_XID == null).Select((A, B, C, D, E, F, G) => new TransferSpbPrintVModel
                {
                    xid = A.XID,
                    slbh = A.YWSLBH,
                    bdcdyh = B.BDCDYH,
                    zl = C.ZL,
                    qllx = E.FW_QLLX_ZWM,
                    qlxz = E.FW_QLXZ_ZWMS,
                    bdclx = D.BDCLX,
                    jzmj = E.FW_JZMJ,
                    bdczh = B.BDCZH,
                    old_bdczh = B.XGZH,
                    syqx = E.TD_SYQX,
                    qt = B.QT,
                    spbz = B.SPBZ,
                    fj = B.FJ,
                    tstybm = D.TSTYBM
                }).Distinct().ToListAsync();

            if (data.Count > 0)
            {
                QlrInfoVModel qlrModel = GetPersonList(xid);
                if (qlrModel.qlrList != null)
                {
                    data[0].qlrmc = qlrModel.qlrList.qlrmc;
                }
                if (qlrModel.ywrList != null)
                {
                    data[0].ywrmc = qlrModel.ywrList.qlrmc;
                }

                tstybm = data[0].tstybm;
                if (tstybm != "")
                {
                    base.ChangeDB(SysConst.DB_CON_BDC);
                    var dyData = await base.Db.Queryable<DJ_TSGL, DJ_DY>((A, B) => A.SLBH == B.SLBH).Where((A, B) => A.TSTYBM == tstybm && A.DJZL == "抵押" && (A.LIFECYCLE == 0 || A.LIFECYCLE == null)).Select((A, B) => new DJ_DY()
                    {
                        BDBZZQSE = B.BDBZZQSE,
                        QLQSSJ = B.QLQSSJ,
                        QLJSSJ = B.QLJSSJ
                    }).ToListAsync();
                    if (dyData.Count > 0)
                    {
                        data[0].bdbzqse = dyData[0].BDBZZQSE;
                        data[0].lxqx = Convert.ToDateTime(dyData[0].QLQSSJ).ToString("D") + "至\r\n" + Convert.ToDateTime(dyData[0].QLJSSJ).ToString("D");
                    }
                }
                base.ChangeDB(SysConst.DB_CON_IIRS);
                var queryqlr = base.Db.Queryable<QLRGL_INFO>()
                 .Where((A) => A.XID == xid)
                    .Select((A) => new
                    {
                        gyfs = A.GYFS,
                        gyfe = A.GYFE,
                        qlrmc = A.QLRMC,
                        qlrlx = A.QLRLX,
                    }).ToList();
                foreach (var item in queryqlr)
                {
                    if (item.gyfs == "2")
                    {
                        if (item.qlrlx == "权利人")
                        {
                            GYFE += item.qlrmc + ":" + item.gyfe + "%" + ",";
                        }
                        data[0].gyfs = item.gyfs;
                    }
                   

                }
                if (GYFE!="")
                {
                    data[0].gyfe = GYFE.Substring(0, GYFE.Length - 1);
                }
                data[0].fwmj = data[0].jzmj + "m²";
                model = data[0];
                model.qlsdfs = "地表";
            }
            return model;
        }

        public QlrInfoVModel GetPersonList(string xid)
        {
            QlrInfoVModel model = new QlrInfoVModel();
            base.ChangeDB(SysConst.DB_CON_IIRS);

            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };

            var qlrData = base.Db.Queryable<QLRGL_INFO>().Where(qlr => qlr.QLRLX == "权利人" && qlr.XID == xid).Select(qlr => new
            {
                qlrmc = SqlFunc.MappingColumn(qlr.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(QLRMC))"),
                zjhm = SqlFunc.MappingColumn(qlr.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(ZJHM))"),
                zjlb_zwm = SqlFunc.MappingColumn(qlr.ZJLB, "WM_CONCAT(DISTINCT TO_CHAR(ZJLB_ZWM))")
            }).ToListAsync();

            if (qlrData.Result.Count > 0)
            {
                model.qlrList.qlrmc = qlrData.Result[0].qlrmc;
                model.qlrList.zjhm = qlrData.Result[0].zjhm;
                model.qlrList.zjlb_zwm = qlrData.Result[0].zjlb_zwm;
            }

            var ywrData = base.Db.Queryable<QLRGL_INFO>().Where(qlr => qlr.QLRLX == "义务人" && qlr.XID == xid).Select(qlr => new
            {
                qlrmc = SqlFunc.MappingColumn(qlr.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(QLRMC))"),
                zjhm = SqlFunc.MappingColumn(qlr.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(ZJHM))"),
                zjlb_zwm = SqlFunc.MappingColumn(qlr.ZJLB, "WM_CONCAT(DISTINCT TO_CHAR(ZJLB_ZWM))")
            }).ToListAsync();

            if (ywrData.Result.Count > 0)
            {
                model.ywrList.qlrmc = ywrData.Result[0].qlrmc;
                model.ywrList.zjhm = ywrData.Result[0].zjhm;
                model.ywrList.zjlb_zwm = ywrData.Result[0].zjlb_zwm;
            }

            var dyrData = base.Db.Queryable<QLRGL_INFO>().Where(qlr => qlr.QLRLX == "抵押人" && qlr.XID == xid).Select(qlr => new
            {
                qlrmc = SqlFunc.MappingColumn(qlr.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(QLRMC))"),
                zjhm = SqlFunc.MappingColumn(qlr.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(ZJHM))"),
                zjlb_zwm = SqlFunc.MappingColumn(qlr.ZJLB, "WM_CONCAT(DISTINCT TO_CHAR(ZJLB_ZWM))")
            }).ToListAsync();

            if (dyrData.Result.Count > 0)
            {
                model.dyrList.qlrmc = dyrData.Result[0].qlrmc;
                model.dyrList.zjhm = dyrData.Result[0].zjhm;
                model.dyrList.zjlb_zwm = dyrData.Result[0].zjlb_zwm;
            }

            var dyqrData = base.Db.Queryable<QLRGL_INFO>().Where(qlr => qlr.QLRLX == "抵押权人" && qlr.XID == xid).Select(qlr => new
            {
                qlrmc = SqlFunc.MappingColumn(qlr.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(QLRMC))"),
                zjhm = SqlFunc.MappingColumn(qlr.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(ZJHM))"),
                zjlb_zwm = SqlFunc.MappingColumn(qlr.ZJLB, "WM_CONCAT(DISTINCT TO_CHAR(ZJLB_ZWM))")
            }).ToListAsync();

            if (dyqrData.Result.Count > 0)
            {
                model.dyqrList.qlrmc = dyqrData.Result[0].qlrmc;
                model.dyqrList.zjhm = dyqrData.Result[0].zjhm;
                model.dyqrList.zjlb_zwm = dyqrData.Result[0].zjlb_zwm;
            }

            return model;
        }
        /// <summary>
        /// 转移登记 - 收件收据打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        public async Task<TransferSjsjPrintVModel> TransferSjsjPrint(string xid)
        {
            TransferSjsjPrintVModel model = new TransferSjsjPrintVModel();
            string strFile = "";
            base.ChangeDB(SysConst.DB_CON_IIRS);
            /* base.Db.Aop.OnLogExecuting = (sql, pars) =>
             {
                 _logger.LogDebug(sql);
             };*/
            var data = await base.Db.Queryable<SJD_INFO, DJB_INFO, QLRGL_INFO>((A, B, C) => A.XID == B.xid && B.xid == C.XID).Where((A, B, C) => A.XID == xid && C.QLRLX == "权利人").GroupBy((A, B, C) => new
            {
                slbh = A.SLBH,
                xgzh = B.XGZH,
                jjr = A.SJR,
                ywlx = A.DJDL,
                tel = A.TZRDH,
                zl = A.ZL,
                sjr = A.SJR,
                djrq = A.SJSJ,
                cnrq = A.CNSJ
            }).Select((A, B, C) => new TransferSjsjPrintVModel()
            {
                slbh = A.SLBH,
                xgzh = B.XGZH,
                sqr = SqlFunc.MappingColumn(C.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(QLRMC))"),
                jjr = A.SJR,
                ywlx = A.DJDL,
                tel = A.TZRDH,
                zl = A.ZL,
                sjr = A.SJR,
                djrq = A.SJSJ,
                cnrq = A.CNSJ
            }).ToListAsync();

            if (data.Count > 0)
            {

                var FileData = base.Db.Queryable<PUB_ATT_FILE>().Where(i => i.XID == xid).GroupBy(i => new { group_name = i.GROUP_NAME }).Select(i => new
                {
                    group_name = i.GROUP_NAME,
                    count = SqlFunc.AggregateCount(i.GROUP_NAME)
                }).ToList();

                if (FileData.Count > 0)
                {
                    for (int i = 0; i < FileData.Count; i++)
                    {
                        strFile += i + 1 + "." + FileData[i].group_name + ":" + FileData[i].count + "个" + "\r\n";
                    }
                    if (data[0].ywlx == "200")
                    {
                        data[0].ywlx = "转移登记";
                    }
                }
                data[0].fj = strFile;
                model = data[0];
            }
            return model;
        }
        /// <summary>
        /// 转移登记 - 不动产登记申请表打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        public async Task<TransferSqbPrintVModel> TransferSqbPrint(string xid)
        {
            TransferSqbPrintVModel model = new TransferSqbPrintVModel();
            string strFile = "";
            string tstybm = "";
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var data = await base.Db.Queryable<REGISTRATION_INFO, DJB_INFO, SJD_INFO, TSGL_INFO, QL_XG_INFO, IFLOW_ACTION, IFLOW_ACTION_GROUP>((A, B, C, D, E, F, G) => A.XID == B.xid && B.xid == C.XID && C.XID == D.XID && D.XID == E.XID && A.DJZL == F.GROUP_ID && F.GROUP_ID == G.GROUP_ID).Where((A, B, C, D, E, F, G) => A.XID == xid && A.NEXT_XID == null).Select((A, B, C, D, E, F, G) => new TransferSqbPrintVModel
            {
                slbh = A.YWSLBH,
                djrq = C.SJSJ,
                sjr = C.SJR,
                zl = C.ZL,
                djlx = "转移登记",
                bdcdyh = B.BDCDYH,
                bdclx = D.BDCLX,
                jzmj = E.FW_JZMJ,
                tdmj = E.TD_JZZDMJ,
                gytdmj = E.TD_GYTDMJ,
                tdlx = E.TD_QLLX_ZWM,
                ghyt = E.FW_FWGHYT_ZWM,
                tdyt = E.TD_TDYT_ZWMS,
                old_bdczh = B.XGZH,
                djyy = B.DJYY,
                tstybm = D.TSTYBM,
                djsyq = C.LCLX,
                fj = B.FJ
            }).Distinct().ToListAsync();

            if (data.Count > 0)
            {
                QlrInfoVModel qlrModel = GetPersonList(xid);
                if (qlrModel.qlrList != null)
                {
                    data[0].qlrmc = qlrModel.qlrList.qlrmc;
                    data[0].qlr_zjlb = qlrModel.qlrList.zjlb_zwm;
                    data[0].qlr_zjhm = qlrModel.qlrList.zjhm;
                }
                if (qlrModel.ywrList != null)
                {
                    data[0].ywrmc = qlrModel.ywrList.qlrmc;
                    data[0].ywr_zjlb = qlrModel.ywrList.zjlb_zwm;
                    data[0].ywr_zjhm = qlrModel.ywrList.zjhm;
                }
                var FileData = base.Db.Queryable<PUB_ATT_FILE>().Where(i => i.XID == xid).GroupBy(i => new { group_name = i.GROUP_NAME }).Select(i => new
                {
                    group_name = i.GROUP_NAME,
                    count = SqlFunc.AggregateCount(i.GROUP_NAME)
                }).ToList();

                if (FileData.Count > 0)
                {
                    for (int i = 0; i < FileData.Count; i++)
                    {
                        strFile += i + 1 + "." + FileData[i].group_name + ":" + FileData[i].count + "个" + "\r\n";
                    }
                    data[0].djyy_zmwj = strFile;
                    //model = data[0];
                }
                tstybm = data[0].tstybm;
                if (tstybm != "")
                {
                    base.ChangeDB(SysConst.DB_CON_BDC);
                    var dyData = await base.Db.Queryable<DJ_TSGL, DJ_DY>((A, B) => A.SLBH == B.SLBH).Where((A, B) => A.TSTYBM == tstybm && A.DJZL == "抵押" && (A.LIFECYCLE == 0 || A.LIFECYCLE == null)).Select((A, B) => new DJ_DY()
                    {
                        BDBZZQSE = B.BDBZZQSE,
                        QLQSSJ = B.QLQSSJ,
                        QLJSSJ = B.QLJSSJ
                    }).ToListAsync();

                    if (dyData.Count > 0)
                    {
                        data[0].bdbzqse = dyData[0].BDBZZQSE;
                        data[0].lxqx = Convert.ToDateTime(dyData[0].QLQSSJ).ToString("D") + "至\r\n" + Convert.ToDateTime(dyData[0].QLJSSJ).ToString("D");
                    }
                }

                model = data[0];
                if (model.tdmj == 0)
                {
                    model.mj = "/" + model.jzmj.ToString();
                }
                else
                {
                    if (model.tdlx == "宅基地使用权")
                    {
                        if (model.gytdmj == null)
                        {
                            model.mj = "/房屋建筑面积：" + model.jzmj.ToString() + "㎡";
                        }
                        else
                        {
                            model.mj = "土地登记面积：" + model.gytdmj.ToString() + "㎡" + "/房屋建筑面积：" + model.jzmj.ToString() + "㎡";
                        }
                    }
                    else
                    {
                        if (model.tdmj == null)
                        {
                            model.mj = "/房屋建筑面积：" + model.jzmj.ToString() + "㎡";
                        }
                        else
                        {
                            model.mj = "共有宗地面积面积：" + model.tdmj.ToString() + "㎡" + "/房屋建筑面积：" + model.jzmj.ToString() + "㎡";
                        }
                    }

                }
                if (model.bdclx == "房屋")
                {
                    model.bdclx = "房屋所有权";
                }
                if (model.tdyt == null)
                {
                    model.ghyt = "/" + model.ghyt;
                    model.bdclx = "/" + model.bdclx;
                }
                else
                {
                    model.ghyt = model.tdyt + "/" + model.ghyt;
                    model.bdclx = "土地/" + model.bdclx;
                }

            }
            return model;
        }
        /// <summary>
        /// 转移登记 - 收费单打印
        /// </summary>
        /// <param name="xid"></param>
        /// <param name="slbh"></param>
        /// <returns></returns>
        public async Task<TransferSfdPrintVModel> TransferSfdPrint(string xid, string slbh)
        {
            //xid = "3af616b9-4537-417e-a50e-0178cae61391";  //3af616b9-4537-417e-a50e-0178cae61391
            TransferSfdPrintVModel model = new TransferSfdPrintVModel();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sfdData = await base.Db.Queryable<SFD_INFO, SJD_INFO>((A, B) => A.XID == B.XID && A.SLBH == B.SLBH).Where((A, B) => A.XID == xid && A.SLBH == slbh).Select((A, B) => new TransferSfdPrintVModel
            {
                slbh = A.SLBH,
                ywlx = B.DJDL,
                xmmc = A.XMMC,
                jfbh = A.JFBH,
                jfdw = A.JFDW,
                dh = A.DH,
                zl = A.TXDZ,
                ysje = A.YSJE,
                ssje = A.SSJE,
                jbr = A.JBR,
                jbrq = A.JBRQ
            }).ToListAsync();

            if (sfdData.Count > 0)
            {
                if (sfdData[0].ywlx == "200")
                {
                    sfdData[0].ywlx = "转移登记";
                }
                var sfdfbData = await base.Db.Queryable<SFD_FB_INFO>().Where(fb => fb.XID == xid && fb.SLBH == slbh).ToListAsync();
                if (sfdfbData.Count > 0)
                {
                    model = sfdData[0];
                    model.skrq = Convert.ToDateTime(model.jbrq).ToString("D");
                    model.strYsje = "￥" + model.ysje + "元";
                    model.strSsje = "￥" + model.ssje + "元";
                    if (sfdfbData.Count == 1)
                    {
                        model.sfxm1 = sfdfbData[0].SFXM;
                        model.jldw1 = sfdfbData[0].JLDW;
                        model.sl1 = sfdfbData[0].SL;
                        model.sfbz1 = sfdfbData[0].SFBZ;
                        model.jmje1 = sfdfbData[0].JMJE;
                        model.jmyy1 = sfdfbData[0].JMYY;
                        model.hsje1 = sfdfbData[0].HSJE;
                        model.bz1 = sfdfbData[0].BZ;
                    }
                    else if (sfdfbData.Count == 4)
                    {
                        for (int i = 0; i < sfdfbData.Count; i++)
                        {
                            if (i == 0)
                            {
                                model.sfxm1 = sfdfbData[i].SFXM;
                                model.jldw1 = sfdfbData[i].JLDW;
                                model.sl1 = sfdfbData[i].SL;
                                model.sfbz1 = sfdfbData[i].SFBZ;
                                model.jmje1 = sfdfbData[i].JMJE;
                                model.jmyy1 = sfdfbData[i].JMYY;
                                model.hsje1 = sfdfbData[i].HSJE;
                                model.bz1 = sfdfbData[i].BZ;
                            }
                            else if (i == 1)
                            {
                                model.sfxm2 = sfdfbData[i].SFXM;
                                model.jldw2 = sfdfbData[i].JLDW;
                                model.sl2 = sfdfbData[i].SL;
                                model.sfbz2 = sfdfbData[i].SFBZ;
                                model.jmje2 = sfdfbData[i].JMJE;
                                model.jmyy2 = sfdfbData[i].JMYY;
                                model.hsje2 = sfdfbData[i].HSJE;
                                model.bz2 = sfdfbData[i].BZ;
                            }
                            else if (i == 2)
                            {
                                model.sfxm3 = sfdfbData[i].SFXM;
                                model.jldw3 = sfdfbData[i].JLDW;
                                model.sl3 = sfdfbData[i].SL;
                                model.sfbz3 = sfdfbData[i].SFBZ;
                                model.jmje3 = sfdfbData[i].JMJE;
                                model.jmyy3 = sfdfbData[i].JMYY;
                                model.hsje3 = sfdfbData[i].HSJE;
                                model.bz3 = sfdfbData[i].BZ;
                            }
                            else if (i == 3)
                            {
                                model.sfxm4 = sfdfbData[i].SFXM;
                                model.jldw4 = sfdfbData[i].JLDW;
                                model.sl4 = sfdfbData[i].SL;
                                model.sfbz4 = sfdfbData[i].SFBZ;
                                model.jmje4 = sfdfbData[i].JMJE;
                                model.jmyy4 = sfdfbData[i].JMYY;
                                model.hsje4 = sfdfbData[i].HSJE;
                                model.bz4 = sfdfbData[i].BZ;
                            }
                        }
                    }
                }
            }
            return model;
        }



        #region
        /// <summary>
        /// 抵押申请表打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        public async Task<DySqbPrintVModel> GeneralMrgeSqbPrint(string xid)
      {
            //xid = "ceshixid123";
            DySqbPrintVModel model = new DySqbPrintVModel();
            string strFile = "";
            base.ChangeDB(SysConst.DB_CON_IIRS);
/*            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };*/
            var DyData = await base.Db.Queryable<DY_INFO, REGISTRATION_INFO, SJD_INFO, QL_XG_INFO, TSGL_INFO>((A, B, C, D, E) => A.XID == B.XID && B.XID == C.XID && C.XID == D.XID && D.XID == E.XID).Where((A, B, C, D, E) => A.XID == xid && B.NEXT_XID == null).Select((A, B, C, D, E) => new DySqbPrintVModel
            {
                slbh = A.SLBH,
                xgzh = A.XGZH,
                bdcdyh = A.BDCDYH,
                bdbzqse = A.BDBZZQSE,
                lxqx = A.DYQX,
                qt = A.QT,
                qlqsrq = A.QLQSSJ,
                qljsrq = A.QLJSSJ,
                fj = A.FJ,
                djyy = A.DJYY,
                zl = C.ZL,
                dylx = A.DYFS,
                sjr = C.SJR,
                savedate = C.SJSJ,
                jzmj = D.FW_JZMJ,
                tdmj = D.TD_JZZDMJ,
                tddymj = D.TD_DYTDMJ,
                qllx = D.FW_QLLX_ZWM,
                qlxz = D.FW_QLXZ_ZWMS,
                tdyt = D.TD_TDYT_ZWMS,
                ghyt = D.FW_FWGHYT_ZWM,
                bdclx = E.BDCLX,
                tstybm = E.TSTYBM,
                djlx = A.DJLX,
                djrq = A.SQRQ,
                dbfw =A.DBFW,
                sfyd1 = A.QRZYQK,
                sfyd2 = A.QRZYQK,
            }).Distinct().ToListAsync();

            if (DyData.Count > 0)
            {
                QlrInfoVModel qlrModel = GetPersonList(xid);
                if (qlrModel.dyrList != null)
                {
                    DyData[0].qlrmc = qlrModel.dyrList.qlrmc;
                    DyData[0].qlr_zjlb = qlrModel.dyrList.zjlb_zwm;
                    DyData[0].qlr_zjhm = qlrModel.dyrList.zjhm;
                }
                if (qlrModel.dyqrList != null)
                {
                    DyData[0].ywrmc = qlrModel.dyqrList.qlrmc;
                    DyData[0].ywr_zjlb = qlrModel.dyqrList.zjlb_zwm;
                    DyData[0].ywr_zjhm = qlrModel.dyqrList.zjhm;
                }
                DyData[0].lxqx = Convert.ToDateTime(DyData[0].qlqsrq).ToString("D") + "至\r\n" + Convert.ToDateTime(DyData[0].qljsrq).ToString("D");
                DyData[0].djyy = DyData[0].djyy.ToString();
                var FileData = base.Db.Queryable<PUB_ATT_FILE>().Where(i => i.XID == xid).GroupBy(i => new { group_name = i.GROUP_NAME }).Select(i => new
                {
                    group_name = i.GROUP_NAME,
                    count = SqlFunc.AggregateCount(i.GROUP_NAME)
                }).ToList();

                if (FileData.Count > 0)
                {
                    for (int i = 0; i < FileData.Count; i++)
                    {
                        strFile += i + 1 + "." + FileData[i].group_name + ":" + FileData[i].count + "个" + "\r\n";
                    }
                    DyData[0].djyy_zmwj = strFile;
                    //model = data[0];
                }
                model = DyData[0];
                //model.qlsdfs = "地表";
                if (model.tdmj == 0|| model.tdmj==null)
                {
                    model.mj = "/" + model.jzmj.ToString()+"㎡";
                }
                else
                {
                    if (model.tdlx == "宅基地使用权")
                    {
                        model.mj = "土地登记面积：" + model.gytdmj.ToString() + "㎡" + "/房屋建筑面积：" + model.jzmj.ToString() + "㎡";
                    }
                    else
                    {
                        model.mj = "共有宗地面积面积：" + model.tdmj.ToString() + "㎡" + "/房屋建筑面积：" + model.jzmj.ToString() + "㎡";
                    }

                }
                if (model.bdclx == "房屋")
                {
                    model.bdclx = "房屋所有权";
                }
                if (model.tdyt == null)
                {
                    model.ghyt = "/" + model.ghyt;
                    model.bdclx = "/" + model.bdclx;
                }
                else
                {
                    model.ghyt = model.tdyt + "/" + model.ghyt;
                    model.bdclx = "土地/" + model.bdclx;
                }
                if (model.dylx == "1")
                {
                    model.dylx = "一般抵押";
                }
                else if (model.dylx == "2")
                {
                    model.dylx = "最高额抵押";
                }
                else
                {
                    model.dylx = "其它";
                }

               model.djlx = "其它";

            }
            return model;
        }
        /// <summary>
        /// 一般抵押 - 收件收据打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        public async Task<BdcGeneralMrgeSjsjPrintVModel> GeneralMrgeSjsjPrint(string xid)
        {
            BdcGeneralMrgeSjsjPrintVModel model = new BdcGeneralMrgeSjsjPrintVModel();
            string strFile = "";
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var data = await base.Db.Queryable<SJD_INFO, DY_INFO>((A, B) => A.XID == B.XID).Where((A, B) => A.XID == xid).Select((A, B) => new BdcGeneralMrgeSjsjPrintVModel()
            {
                slbh = A.SLBH,
                sjr = A.SJR,    
                zl = A.ZL,
                sjsj = B.SQRQ,
                cnrq = B.CNSJ,
                xgzh = B.XGZH,
                djlx = A.LCMC,
                dyfs = B.DYFS,
            }).ToListAsync();

            if(data.Count > 0)
            {
                QlrInfoVModel qlrModel = GetPersonList(xid);
                if (qlrModel.dyqrList != null)
                {
                    data[0].jjr = qlrModel.dyqrList.qlrmc;
                }
                if (qlrModel.dyrList != null)
                {
                    data[0].dyr = qlrModel.dyrList.qlrmc;
                }

                var FileData = base.Db.Queryable<PUB_ATT_FILE>().Where(i => i.XID == xid).GroupBy(i => new { group_name = i.GROUP_NAME }).Select(i => new
                {
                    group_name = i.GROUP_NAME,
                    count = SqlFunc.AggregateCount(i.GROUP_NAME)
                }).ToList();

                if (FileData.Count > 0)
                {
                    for (int i = 0; i < FileData.Count; i++)
                    {
                        strFile += i + 1 + "." + FileData[i].group_name + ":" + FileData[i].count + "个" + "\r\n";
                    }

                    data[0].fj = strFile;
                    
                }
                model = data[0];
            }
            return model;
        }
        /// <summary>
        /// 一般抵押 - 清单打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        public async Task<BdcGeneralMrgeQdPrintVModel> BdcGeneralMrgeQdPrint(string xid)
        {
            BdcGeneralMrgeQdPrintVModel model = null;
            BdcGeneralMrgeQd qdModel = null;
            string tdSlbh = "";
            string fwSlbh = "";
            //xid = "f712f29b-cfb2-4ac1-94a0-01783476f0d0";
            BdcGeneralMrgeQdPrintVModel ModelList = new BdcGeneralMrgeQdPrintVModel();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDic = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 1, 3, 4, 5, 6, 7, 8, 9 }).ToListAsync();
            var Data = await base.Db.Queryable<XGDJGL_INFO>().Where(i => i.XID == xid).Select(i => new
            {
                slbh = i.FSLBH,
                xgzlx = i.XGZLX,
                newSlbh = i.ZSLBH
            }).ToListAsync();

            if(Data.Count > 0)
            {
                foreach (var item in Data)
                {
                    if(item.xgzlx == "土地证")
                    {
                        tdSlbh += item.slbh + ",";
                    }
                    else
                    {
                        fwSlbh += item.slbh + ",";
                    }
                }
                if(tdSlbh != "")
                {
                    tdSlbh = tdSlbh.Substring(0, tdSlbh.Length - 1);
                    base.ChangeDB(SysConst.DB_CON_BDC);
                   /* base.Db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        _logger.LogDebug(sql);
                    };*/
                    var tdData = await base.Db.Queryable<DJ_DJB, DJ_TSGL, ZD_QSDC, QL_TDXG, DJ_SJD, DJ_QLRGL>((A, B, C, D, E, F) => new object[] { JoinType.Left, A.SLBH == B.SLBH, JoinType.Left, B.TSTYBM == C.TSTYBM, JoinType.Left, A.SLBH == D.SLBH, JoinType.Left, A.SLBH == E.SLBH, JoinType.Left, A.SLBH == F.SLBH }).Where((A, B, C, D, E, F) => tdSlbh.Contains(A.SLBH)).GroupBy((A, B, C, D, E, F) => new 
                    {
                        bdczh = A.BDCZH,
                        bdcdyh = A.BDCDYH,
                        sjdzl = E.ZL,
                        zdzl = C.ZL,
                        mj = D.GYTDMJ,
                        ftmj = D.FTTDMJ,
                        qltdyt = D.TDYT,
                        zdtdyt = C.SJTDYT,
                        tdqlxz = D.QLXZ,
                        fj = A.FJ
                    }).Select((A, B, C, D, E, F) => new
                    {
                        bdczh = A.BDCZH,
                        bdcdyh = A.BDCDYH,
                        zl = SqlFunc.IsNull(E.ZL, C.ZL),
                        mj = D.GYTDMJ,
                        ftmj = D.FTTDMJ,
                        yt = SqlFunc.IsNull(D.TDYT, C.SJTDYT),
                        tdqlxz = D.QLXZ,
                        fj = A.FJ,
                        qlrmc = SqlFunc.MappingColumn(F.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(F.QLRMC))"),
                        zjhm = SqlFunc.MappingColumn(F.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(F.ZJHM))")
                    }).ToListAsync();

                    if(tdData.Count > 0)
                    {
                        foreach (var item in tdData)
                        {
                            var Tdyt = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == item.yt).FirstOrDefault();
                            var tdQlxz = sysDic.Where(s => s.GID == 7 && s.DEFINED_CODE == item.tdqlxz).FirstOrDefault();
                            qdModel = new BdcGeneralMrgeQd();
                            qdModel.slbh = Data[0].newSlbh;
                            qdModel.bdczh = item.bdczh;
                            qdModel.bdcdyh = item.bdcdyh;
                            qdModel.zl = item.zl;
                            qdModel.mj = item.mj;
                            qdModel.ftmj = item.ftmj;
                            qdModel.qlrmc = item.qlrmc;
                            qdModel.zjhm = item.zjhm;
                            qdModel.yt_zwm = Tdyt != null ? Tdyt.DNAME : string.Empty;
                            qdModel.tdqlxz_zwm = tdQlxz != null ? tdQlxz.DNAME : string.Empty;
                            qdModel.fj = item.fj;
                            ModelList.modelList.Add(qdModel);
                        }
                    }
                }
                    
                if(fwSlbh != "")
                {
                    fwSlbh = fwSlbh.Substring(0, fwSlbh.Length - 1);
                    base.ChangeDB(SysConst.DB_CON_BDC);
                  /*  base.Db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        _logger.LogDebug(sql);
                    };*/
                    var fwData = await base.Db.Queryable<DJ_DJB, DJ_TSGL, FC_H_QSDC, QL_FWXG,QL_TDXG, QL_SLXG, DJ_SJD,DJ_QLRGL>((A, B, C, D, E, F,G,H) => new object[] { JoinType.Left, A.SLBH == B.SLBH, JoinType.Left, B.TSTYBM == C.TSTYBM, JoinType.Left, A.SLBH == D.SLBH, JoinType.Left, A.SLBH == E.SLBH, JoinType.Left, F.SLBH == A.SLBH,JoinType.Left,A.SLBH == G.SLBH,JoinType.Left,A.SLBH == H.SLBH }).Where((A, B, C, D, E, F, G,H) => fwSlbh.Contains(A.SLBH)).GroupBy((A, B, C, D, E, F, G, H) => new 
                    {
                        bdczh = A.BDCZH,
                        bdcdyh = A.BDCDYH,
                        sjdzl = G.ZL,
                        fczl = C.ZL,
                        qljzmj = D.JZMJ,
                        syqmj = F.SYQMJ,
                        gytdmj = E.GYTDMJ,
                        ftmj = E.FTTDMJ,
                        yt = C.GHYT,
                        qlxz = D.QLXZ,
                        tdqlxz = E.QLXZ,
                        fj = A.FJ
                    }).Select((A, B, C, D, E, F,G,H) => new 
                    {
                        bdczh = A.BDCZH,
                        bdcdyh = A.BDCDYH,
                        zl = SqlFunc.IsNull(G.ZL,C.ZL),
                        mj = SqlFunc.IsNull(SqlFunc.IsNull(D.JZMJ,F.SYQMJ),E.GYTDMJ),
                        ftmj = E.FTTDMJ,
                        yt = C.GHYT,
                        qlxz = D.QLXZ,
                        tdqlxz = E.QLXZ,
                        fj = A.FJ,
                        qlrmc = SqlFunc.MappingColumn(H.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(H.QLRMC))"),
                        zjhm = SqlFunc.MappingColumn(H.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(H.ZJHM))")
                    }).ToListAsync();

                    if(fwData.Count > 0)
                    {
                        foreach (var item in fwData)
                        {
                            var Fwyt = sysDic.Where(s => s.GID == 5 && s.DEFINED_CODE == item.yt).FirstOrDefault();
                            var tdQlxz = sysDic.Where(s => s.GID == 7 && s.DEFINED_CODE == item.tdqlxz).FirstOrDefault();
                            qdModel = new BdcGeneralMrgeQd();
                            qdModel.slbh = Data[0].newSlbh;
                            qdModel.bdczh = item.bdczh;
                            qdModel.bdcdyh = item.bdcdyh;
                            qdModel.zl = item.zl;
                            qdModel.mj = item.mj;
                            qdModel.ftmj = item.ftmj;
                            qdModel.qlrmc = item.qlrmc;
                            qdModel.zjhm = item.zjhm;
                            qdModel.yt_zwm = Fwyt != null ? Fwyt.DNAME : string.Empty;
                            qdModel.tdqlxz_zwm = tdQlxz != null ? tdQlxz.DNAME : string.Empty;
                            qdModel.fj = item.fj;
                            ModelList.modelList.Add(qdModel);
                        }
                    }
                }
                    

            }
            return ModelList;
        }
        /// <summary>
        /// 一般抵押 - 抵押审批表
        /// </summary>
        /// <param name="xid"></param>
        /// <param name="slbh"></param>
        /// <returns></returns>
        public async Task<DySpbPrintVModel> GeneralMrgeSpbPrint(string xid,string slbh)
        {
            DySpbPrintVModel model = new DySpbPrintVModel();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var dyData = await base.Db.Queryable<DY_INFO>().Where(dy => dy.XID == xid && dy.SLBH == dy.SLBH).ToListAsync();
            var xgdjgl =await base.Db.Queryable<XGDJGL_INFO>().Where(DJ => DJ.ZSLBH == slbh).ToListAsync();
            var FSLBH = xgdjgl[0].FSLBH;
            var data = await base.Db.Queryable<QLRGL_INFO, DY_INFO, SJD_INFO, QL_XG_INFO, TSGL_INFO>((A, B, C, D,  F) => A.XID == B.XID && B.XID == C.XID && C.XID == D.XID && D.XID == F.XID)
                .Where((A, B) => A.XID == xid&&A.SLBH ==slbh)
               .Select((A, B, C, D,  F) => new DySpbPrintVModel()
               {
                   slbh = A.SLBH,
                   qlrmc = A.QLRMC,
                   xgzh = B.BDCZMH,
                   bdcdyh = B.BDCDYH,
                   zl = C.ZL,
                   fwqllx = D.FW_QLLX_ZWM,
                   fwqlxz = D.FW_QLXZ_ZWMS,
                   bdclx = F.BDCLX,
                   fwmj = D.FW_JZMJ,
                   tdmj = D.TD_GYTDMJ,
                   ybdczh = B.XGZH,
                   ghyt = D.FW_FWGHYT_ZWM,
                   gyfs1 = A.GYFS,
                   gyfe = A.GYFE,
                   td_qllx_zwm = D.TD_QLLX_ZWM,
                   syqx = D.TD_SYQX,
                   qlrlx = A.QLRLX,
                   ywrmc = A.QLRMC,
                   zqse = B.BDBZZQSE,
                   dbfw = B.DBFW,
                   yd1 = B.QRZYQK,
                   qtqlqk = B.QT,
                   sjr = C.SJR,
                   sjsj = C.SJSJ,
                   bz = C.SJBZ,
               }).ToListAsync();
            base.ChangeDB(SysConst.DB_CON_BDC);

            var Fdata = await base.Db.Queryable<QL_TDXG, QL_FWXG, DJ_QLRGL>((A, B, C) => A.SLBH == B.SLBH && B.SLBH == C.SLBH).Where((A, B, C) => A.SLBH == FSLBH && C.QLRLX == "权利人").Select((A, B, C) => new DySpbPrintVModel()
            {
                  fwmj = B.JZMJ,
                  tdmj = A.JZZDMJ,
                  gyfs1 = C.GYFS,
                  gyfe = C.GYFE,
                  syqx = A.SYQX,
            }).ToListAsync();
            if (data.Count > 0)
            {
                QlrInfoVModel qlrModel = GetPersonList(xid);
                if (qlrModel.dyqrList != null)
                {
                    model.qlrmc = qlrModel.dyqrList.qlrmc + ",";
                }
                if (qlrModel.dyrList != null)
                {
                    model.ywrmc += qlrModel.dyrList.qlrmc + ",";

                }
                model.qlrmc = model.qlrmc.TrimEnd(',');
                model.ywrmc = model.ywrmc.TrimEnd(',');
                model.slbh = data[0].slbh;
                model.xgzh = data[0].xgzh;
                model.bdcdyh = data[0].bdcdyh;
                model.zl = data[0].zl;
                model.syqx = Fdata[0].syqx;
                model.fwqllx = data[0].fwqllx;
                model.fwqlxz = data[0].fwqlxz;
                model.bdclx = data[0].bdclx;
                if (data[0].fwmj != null)
                {
                    model.fwmj = data[0].fwmj;
                    model.jzmj = "房屋建筑面积：" + model.fwmj+"㎡";
                }
                 if(data[0].tdmj != null)
                {
                    model.tdmj = data[0].tdmj;
                    model.jzmj = "共有宗地面积：" + model.tdmj+"㎡\r\n"+ model.jzmj;
                }
                model.ybdczh = data[0].ybdczh;
                model.ghyt = data[0].ghyt;
                model.gzwlx = data[0].gzwlx;
                model.zqse = data[0].zqse;
                model.qlrlx = data[0].qlrlx;

                data[0].lxqx = Convert.ToDateTime(dyData[0].QLQSSJ).ToString("D") + "至\r\n" + Convert.ToDateTime(dyData[0].QLJSSJ).ToString("D");
                model.lxqx = data[0].lxqx;
                model.dyfw = data[0].dyfw;
                model.gyfs1 = Fdata[0].gyfs1;
                model.yd1 = data[0].yd1;
                model.qtqlqk = data[0].qtqlqk;
                model.sjr = data[0].sjr;
                model.sjsj = data[0].sjsj;
                model.bz = data[0].bz;
                model.gyfe = Fdata[0].gyfe;
            }
            return model;
        }

        /// <summary>
        /// 一般抵押 - 收费单打印
        /// </summary>
        /// <param name="xid"></param>
        /// <param name="slbh"></param>
        /// <returns></returns>
        public async Task<GeneralMrgeSfdPrintVModel> GeneralMrgeSfdPrint(string xid,string slbh)
        {
            //xid = "3af616b9-4537-417e-a50e-0178cae61391";  //3af616b9-4537-417e-a50e-0178cae61391
            GeneralMrgeSfdPrintVModel model = new GeneralMrgeSfdPrintVModel();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDic = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 1, 3, 4, 5, 6, 7, 8, 9 }).ToListAsync();
            var sfdData = await base.Db.Queryable<SFD_INFO>().Where((A) => A.XID == xid&&A.SLBH==slbh).Select((A) => new GeneralMrgeSfdPrintVModel
            {
                slbh = A.SLBH,
                xmmc = A.XMMC,
                jfbh = A.JFBH,
                jfdw = A.JFDW,
                dh = A.DH,
                zl = A.TXDZ,
                ysje = A.YSJE,
                ssje = A.SSJE,
                jbr = A.JBR,
                jbrq = A.JBRQ
            }).ToListAsync();
            var SJDData = await base.Db.Queryable<SJD_INFO>().Where(B => B.XID == xid && B.SLBH == slbh).Select((B) => new GeneralMrgeSfdPrintVModel
            {
                ywlx = B.DJDL,
            }).ToListAsync();

            if (sfdData.Count > 0)
            {
                var SFDW = await base.Db.Queryable<QLRGL_INFO>().Where(QLR => QLR.XID == xid).ToListAsync();
                var SFDW1 = "";
                var SFDW2 = "";
                var SFDW3 = "";
                for(var i = 0; i < SFDW.Count; i++)
                {
                    if (SFDW[i].QLRLX == "抵押人")
                    {
                        SFDW1 += SFDW[i].QLRMC+" ";
                    }
                    else if(SFDW[i].QLRLX == "抵押权人")
                    {
                        SFDW2 += SFDW[i].QLRMC + " ";
                    }
                }
                SFDW3 = SFDW1 + SFDW2;
                if (sfdData[0].jfdw == "1")
                {
                    sfdData[0].jfdw = SFDW1;
                }
                else if(sfdData[0].jfdw == "2")
                {
                    sfdData[0].jfdw = SFDW2;
                }
                else if (sfdData[0].jfdw == "3")
                {
                    sfdData[0].jfdw = SFDW3;
                }
                var sfdfbData = await base.Db.Queryable<SFD_FB_INFO>().Where(fb => fb.XID == xid&&fb.SLBH ==slbh).ToListAsync();
                if (sfdfbData.Count > 0)
                {
                    model = sfdData[0];
                    model.ywlx = sfdData[0].xmmc;
                    model.skrq = Convert.ToDateTime(model.jbrq).ToString("D");
                    model.strYsje = "￥" + model.ysje + "元";
                    model.strSsje = "￥" + model.ssje + "元";
                    if (sfdfbData.Count == 1)
                    {
                        model.sfxm1 = sfdfbData[0].SFXM;
                        model.jldw1 = sfdfbData[0].JLDW;
                        model.sl1 = sfdfbData[0].SL;
                        model.sfbz1 = sfdfbData[0].SFBZ;
                        model.jmje1 = sfdfbData[0].JMJE;
                        model.jmyy1 = sfdfbData[0].JMYY;
                        model.hsje1 = sfdfbData[0].HSJE;
                        model.bz1 = sfdfbData[0].BZ;
                    }
                    else if (sfdfbData.Count == 4)
                    {
                        for (int i = 0; i < sfdfbData.Count; i++)
                        {
                            if (i == 0)
                            {
                                model.sfxm1 = sfdfbData[i].SFXM;
                                model.jldw1 = sfdfbData[i].JLDW;
                                model.sl1 = sfdfbData[i].SL;
                                model.sfbz1 = sfdfbData[i].SFBZ;
                                model.jmje1 = sfdfbData[i].JMJE;
                                model.jmyy1 = sfdfbData[i].JMYY;
                                model.hsje1 = sfdfbData[i].HSJE;
                                model.bz1 = sfdfbData[i].BZ;
                            }
                            else if (i == 1)
                            {
                                model.sfxm2 = sfdfbData[i].SFXM;
                                model.jldw2 = sfdfbData[i].JLDW;
                                model.sl2 = sfdfbData[i].SL;
                                model.sfbz2 = sfdfbData[i].SFBZ;
                                model.jmje2 = sfdfbData[i].JMJE;
                                model.jmyy2 = sfdfbData[i].JMYY;
                                model.hsje2 = sfdfbData[i].HSJE;
                                model.bz2 = sfdfbData[i].BZ;
                            }
                            else if (i == 2)
                            {
                                model.sfxm3 = sfdfbData[i].SFXM;
                                model.jldw3 = sfdfbData[i].JLDW;
                                model.sl3 = sfdfbData[i].SL;
                                model.sfbz3 = sfdfbData[i].SFBZ;
                                model.jmje3 = sfdfbData[i].JMJE;
                                model.jmyy3 = sfdfbData[i].JMYY;
                                model.hsje3 = sfdfbData[i].HSJE;
                                model.bz3 = sfdfbData[i].BZ;
                            }
                            else if (i == 3)
                            {
                                model.sfxm4 = sfdfbData[i].SFXM;
                                model.jldw4 = sfdfbData[i].JLDW;
                                model.sl4 = sfdfbData[i].SL;
                                model.sfbz4 = sfdfbData[i].SFBZ;
                                model.jmje4 = sfdfbData[i].JMJE;
                                model.jmyy4 = sfdfbData[i].JMYY;
                                model.hsje4 = sfdfbData[i].HSJE;
                                model.bz4 = sfdfbData[i].BZ;
                            }
                        }
                    }
                }
            }
            return model;
        }


        #endregion

        #region 抵押注销打印
        /// <summary>
        /// 抵押注销审批表
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        public async Task<MrgeReleaseSpbVModel> GeneralMrgeZXSqbPrint(string xid)
        {
            MrgeReleaseSpbVModel model = new MrgeReleaseSpbVModel();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            //SFD_FB_INFO fbModel = null;
            var data = await base.Db.Queryable<QLRGL_INFO, DY_INFO, SJD_INFO, QL_XG_INFO, DJB_INFO, TSGL_INFO>((A, B, C, D, E, F) => A.XID == B.XID && B.XID == C.XID && C.XID == D.XID && D.XID == E.xid && E.xid == F.XID)
                .Where((A, B) => A.XID == xid)
               .Select((A, B, C, D, E, F) => new MrgeReleaseSpbVModel()
               {
                   slbh = A.SLBH,
                   qlrmc = A.QLRMC,
                   bdczmh = B.BDCZMH,
                   bdcdyh = B.BDCDYH,
                   zl = C.ZL,
                   qllx = D.FW_QLLX_ZWM,
                   qlxz = D.FW_QLXZ_ZWMS,
                   bdclx = F.BDCLX,
                   fwmj = D.FW_JZMJ,
                   tdmj = D.TD_GYTDMJ,
                   old_bdcqzh = B.XGZH,
                   ghyt = D.FW_FWGHYT_ZWM,
                   td_qllx_zwm = D.TD_QLLX_ZWM,
                   syqx = D.TD_SYQX,
                   qlrlx = A.QLRLX,
                   ywrmc = A.QLRMC,
                   bdbzqse = B.BDBZZQSE,
                   lxqx = B.QLJSSJ+"",
                   dbfw = B.DBFW,
                   yd1 = B.QRZYQK,
                   qt = B.QT,
                   sjr = C.SJR,
                   cssj = C.SJSJ,
                   bz = C.SJBZ,
               }).ToListAsync();
            if (data.Count > 0)
            {

                QlrInfoVModel qlrModel = GetPersonList(xid);
                if (qlrModel.dyqrList != null)
                {
                    model.qlrmc = qlrModel.dyqrList.qlrmc + ",";
                }
                if (qlrModel.dyrList != null)
                {
                    model.ywrmc += qlrModel.dyrList.qlrmc + ",";

                }
                model.qlrmc = model.qlrmc.TrimEnd(',');
                model.ywrmc = model.ywrmc.TrimEnd(',');
                if (data[0].fwmj != null)
                {
                    model.fwmj = data[0].fwmj;
                    model.jzmj = "房屋建筑面积：" + model.fwmj;
                }
                else
                {
                    model.tdmj = data[0].tdmj;
                    model.jzmj = "共有宗地面积：" + model.tdmj;
                }
                model.slbh = data[0].slbh;
                model.bdczmh = data[0].bdczmh;
                model.bdcdyh = data[0].bdcdyh;
                model.zl = data[0].zl;
                model.qllx = data[0].qllx;
                model.qlxz = data[0].qlxz;
                model.bdclx = data[0].bdclx;
                model.old_bdcqzh = data[0].old_bdcqzh;
                model.ghyt = data[0].ghyt;
                model.td_qllx_zwm = data[0].td_qllx_zwm;
                model.syqx = data[0].syqx;
                model.bdbzqse = data[0].bdbzqse;
                model.lxqx = data[0].lxqx;
                model.dbfw = data[0].dbfw;
                model.yd1 = data[0].yd1;
                model.qt = data[0].qt;
                model.sjr = data[0].sjr;
                model.cssj = data[0].cssj;
                model.bz = data[0].bz;
            }
            return model;
        }

        /// <summary>
        /// 抵押注销 - 清单打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        public async Task<MrgeReleaseQdPrintVmodel> MrgeReleaseQdPrint(string xid)
        {
            MrgeReleaseQdPrintVmodel model = null;
            MrgeReleaseQdPrint qdModel = null;
            string tdSlbh = "";
            string fwSlbh = "";
            //xid = "f712f29b-cfb2-4ac1-94a0-01783476f0d0";
            MrgeReleaseQdPrintVmodel ModelList = new MrgeReleaseQdPrintVmodel();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDic = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 1, 3, 4, 5, 6, 7, 8, 9 }).ToListAsync();
            var Data = await base.Db.Queryable<XGDJGL_INFO>().Where(i => i.XID == xid).Select(i => new
            {
                slbh = i.FSLBH,
                xgzlx = i.XGZLX,
                newSlbh = i.ZSLBH,
                xgzh = i.XGZH,

            }).ToListAsync();

            if (Data.Count > 0)
            {
                foreach (var item in Data)
                {
                    if (item.xgzlx == "土地证")
                    {
                        tdSlbh += item.slbh + ",";
                    }
                    else
                    {
                        fwSlbh += item.slbh + ",";
                    }
                }
                if (tdSlbh != "")
                {
                    tdSlbh = tdSlbh.Substring(0, tdSlbh.Length - 1);
                    base.ChangeDB(SysConst.DB_CON_BDC);
                    base.Db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        _logger.LogDebug(sql);
                    };
                    var tdData = await base.Db.Queryable<DJ_DJB, DJ_TSGL, ZD_QSDC, QL_TDXG, DJ_SJD, DJ_QLRGL>((A, B, C, D, E, F) => new object[] { JoinType.Left, A.SLBH == B.SLBH, JoinType.Left, B.TSTYBM == C.TSTYBM, JoinType.Left, A.SLBH == D.SLBH, JoinType.Left, A.SLBH == E.SLBH, JoinType.Left, A.SLBH == F.SLBH }).Where((A, B, C, D, E, F) => tdSlbh.Contains(A.SLBH)).GroupBy((A, B, C, D, E, F) => new
                    {
                        bdczh = A.BDCZH,
                        bdczmh = A.XGZH,
                        sjdzl = E.ZL,
                        zdzl = C.ZL,
                        mj = D.GYTDMJ,
                        ftmj = D.FTTDMJ,
                        qltdyt = D.TDYT,
                        zdtdyt = C.SJTDYT,
                        tdqlxz = D.QLXZ,
                        fj = A.FJ
                    }).Select((A, B, C, D, E, F) => new
                    {
                        bdczh = A.BDCZH,
                        bdczmh = A.XGZH,
                        zl = SqlFunc.IsNull(E.ZL, C.ZL),
                        mj = D.GYTDMJ,
                        ftmj = D.FTTDMJ,
                        yt = SqlFunc.IsNull(D.TDYT, C.SJTDYT),
                        tdqlxz = D.QLXZ,
                        fj = A.FJ,
                        qlrmc = SqlFunc.MappingColumn(F.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(F.QLRMC))"),
                        qlrlx = SqlFunc.MappingColumn(F.QLRLX, "WM_CONCAT(DISTINCT TO_CHAR(F.QLRLX))"),
                        zjhm = SqlFunc.MappingColumn(F.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(F.ZJHM))")
                    }).ToListAsync();

                    if (tdData.Count > 0)
                    {
                        String[] result = tdData[0].qlrlx.Split(",");
                        String[] qlr = tdData[0].qlrmc.Split(",");
                        foreach (var item in tdData)
                        {
                            var Tdyt = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == item.yt).FirstOrDefault();
                            var tdQlxz = sysDic.Where(s => s.GID == 7 && s.DEFINED_CODE == item.tdqlxz).FirstOrDefault();
                            qdModel = new MrgeReleaseQdPrint();
                            qdModel.slbh = Data[0].newSlbh;
                            if (Data[0].xgzh.IndexOf("证明") != -1)
                            {
                                qdModel.bdczh = Data[1].xgzh;
                                qdModel.bdczmh = Data[0].xgzh;
                                qdModel.zslx = Data[1].xgzlx;
                            }
                            else
                            {
                                qdModel.bdczh = Data[0].xgzh;
                                qdModel.bdczmh = Data[1].xgzh;
                                qdModel.zslx = Data[0].xgzlx;
                            }

                            qdModel.zl = item.zl;
                            qdModel.mj = item.mj;
                            if (result[0] == "权利人")
                            {
                                qdModel.qlrmc = qlr[0];
                            }

                            qdModel.yt = Tdyt != null ? Tdyt.DNAME : string.Empty;

                            ModelList.modelList.Add(qdModel);
                        }
                    }
                }

                if (fwSlbh != "")
                {
                    fwSlbh = fwSlbh.Substring(0, fwSlbh.Length - 1);
                    base.ChangeDB(SysConst.DB_CON_BDC);
                    base.Db.Aop.OnLogExecuting = (sql, pars) =>
                    {
                        _logger.LogDebug(sql);
                    };
                    var fwData = await base.Db.Queryable<DJ_DJB, DJ_TSGL, FC_H_QSDC, QL_FWXG, QL_TDXG, QL_SLXG, DJ_SJD, DJ_QLRGL>((A, B, C, D, E, F, G, H) => new object[] { JoinType.Left, A.SLBH == B.SLBH, JoinType.Left, B.TSTYBM == C.TSTYBM, JoinType.Left, A.SLBH == D.SLBH, JoinType.Left, A.SLBH == E.SLBH, JoinType.Left, F.SLBH == A.SLBH, JoinType.Left, A.SLBH == G.SLBH, JoinType.Left, A.SLBH == H.SLBH }).Where((A, B, C, D, E, F, G, H) => fwSlbh.Contains(A.SLBH)).GroupBy((A, B, C, D, E, F, G, H) => new
                    {
                        bdczh = A.BDCZH,
                        bdczmh = A.XGZH,
                        sjdzl = G.ZL,
                        fczl = C.ZL,
                        qljzmj = D.JZMJ,
                        syqmj = F.SYQMJ,
                        gytdmj = E.GYTDMJ,
                        ftmj = E.FTTDMJ,
                        yt = C.GHYT,
                        qlxz = D.QLXZ,
                        tdqlxz = E.QLXZ,
                        fj = A.FJ
                    }).Select((A, B, C, D, E, F, G, H) => new
                    {
                        bdczh = A.BDCZH,
                        bdczmh = A.XGZH,
                        zl = SqlFunc.IsNull(G.ZL, C.ZL),
                        mj = SqlFunc.IsNull(SqlFunc.IsNull(D.JZMJ, F.SYQMJ), E.GYTDMJ),
                        ftmj = E.FTTDMJ,
                        yt = C.GHYT,
                        qlxz = D.QLXZ,
                        tdqlxz = E.QLXZ,
                        fj = A.FJ,
                        qlrmc = SqlFunc.MappingColumn(H.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(H.QLRMC))"),
                        qlrlx = SqlFunc.MappingColumn(H.QLRLX, "WM_CONCAT(DISTINCT TO_CHAR(H.QLRLX))"),
                        zjhm = SqlFunc.MappingColumn(H.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR(H.ZJHM))")
                    }).ToListAsync();
                    String[] result = fwData[0].qlrlx.Split(",");
                    String[] qlr = fwData[0].qlrmc.Split(",");

                    if (fwData.Count > 0)
                    {
                        foreach (var item in fwData)
                        {
                            var Fwyt = sysDic.Where(s => s.GID == 5 && s.DEFINED_CODE == item.yt).FirstOrDefault();
                            var tdQlxz = sysDic.Where(s => s.GID == 7 && s.DEFINED_CODE == item.tdqlxz).FirstOrDefault();
                            qdModel = new MrgeReleaseQdPrint();
                            qdModel.slbh = Data[0].newSlbh;
                            if (Data[0].xgzh.IndexOf("证明") != -1)
                            {
                                qdModel.bdczh = Data[1].xgzh;
                                qdModel.bdczmh = Data[0].xgzh;
                                qdModel.zslx = Data[1].xgzlx;
                            }
                            else
                            {
                                qdModel.bdczh = Data[0].xgzh;
                                qdModel.bdczmh = Data[1].xgzh;
                                qdModel.zslx = Data[0].xgzlx;
                            }
                            if (result[0] == "权利人")
                            {
                                qdModel.qlrmc = qlr[0];
                            }
                            qdModel.zl = item.zl;
                            qdModel.mj = item.mj;
                            qdModel.yt = Fwyt.DNAME;

                            ModelList.modelList.Add(qdModel);
                        }
                    }
                }


            }
            return ModelList;
        }
        /// <summary>
        /// 抵押注销审批单
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        public async Task<MrgeReleaseSpbVModel> MrgeReleaseSpbPrint(string xid)
        {
            MrgeReleaseSpbVModel model = new MrgeReleaseSpbVModel();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var FSLBH = "";
            var fwslbh = "";

            var DJGLINFO = base.Db.Queryable<XGDJGL_INFO>().Where(DY => DY.XID == xid).ToList();

            var sysDic = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 1, 3, 4, 5, 6, 7, 8, 9 }).ToListAsync();
            if (DJGLINFO != null)
            {
                for (var i = 0; i < DJGLINFO.Count; i++)
                {
                    if (DJGLINFO[i].XGZLX == "房屋抵押证明")
                    {
                        FSLBH = DJGLINFO[i].FSLBH;
                        base.ChangeDB(SysConst.DB_CON_BDC);
                        var Housedata = await base.Db.Queryable<DJ_DY, DJ_TSGL>((B, C) => B.SLBH == C.SLBH)
                            .Where((B) => B.SLBH == FSLBH).Select((B, C) => new MrgeReleaseSpbVModel()
                            {
                                bdclx = C.BDCLX,
                                old_bdcqzh = B.XGZH,
                            }).ToListAsync();
                        if (Housedata.Count > 0)
                        {
                            model.bdclx = Housedata[0].bdclx;
                            model.old_bdcqzh = Housedata[0].old_bdcqzh;
                        }

                    }
                    else if (DJGLINFO[i].XGZLX == "房屋不动产证")
                    {
                        fwslbh = DJGLINFO[i].FSLBH;
                        var Housedata = await base.Db.Queryable<QL_FWXG, QL_TDXG>((D, E) => D.SLBH == E.SLBH)
                         .Where((D) => D.SLBH == fwslbh).Select((D, E) => new MrgeReleaseSpbVModel()
                         {
                             qllx = D.QLLX,
                             qlxz = D.QLXZ,
                             fwmj = D.JZMJ,
                             tdmj = E.GYTDMJ,
                             ghyt = D.GHYT,
                             syqx = E.SYQX,
                         }).ToListAsync();
                        var FWQLLX = sysDic.Where(s => s.GID == 4 && s.DEFINED_CODE == Housedata[0].qllx).FirstOrDefault();
                        var FWQLXZ = sysDic.Where(s => s.GID == 3 && s.DEFINED_CODE == Housedata[0].qlxz).FirstOrDefault();
                        /*     var yt = await base.Db.Queryable<t_dic_fwytlx>*/
                        if (Housedata[0].ghyt == "10")
                        {
                            Housedata[0].ghyt = "住宅";
                        }
                        if (Housedata.Count > 0)
                        {
                            model.qllx = FWQLLX != null ? FWQLLX.DNAME : String.Empty;
                            model.qlxz = FWQLXZ != null ? FWQLXZ.DNAME : String.Empty;
                            if (Housedata[0].fwmj != null)
                            {
                                model.fwmj = Housedata[0].fwmj;
                                model.jzmj = "房屋建筑面积：" + model.fwmj;
                            }
                            else
                            {
                                model.tdmj = Housedata[0].tdmj;
                                model.jzmj = "共有宗地面积：" + model.tdmj;
                            }
                            model.ghyt = Housedata[0].ghyt;
                            model.syqx = Housedata[0].syqx;
                        }
                    }

                }
                base.ChangeDB(SysConst.DB_CON_BDC);
                var dyInfo = base.Db.Queryable<DJ_DY>().Where((DJ) => DJ.SLBH == FSLBH).Single();

                base.ChangeDB(SysConst.DB_CON_IIRS);
                var data = await base.Db.Queryable<QLRGL_INFO, XGDJZX_INFO, SJD_INFO, TSGL_INFO>((A, B, C, D) => A.XID == B.XID && B.XID == C.XID && C.XID == D.XID)
                  .Where((A, B) => B.XID == xid)
                 .Select((A, B, C, D) => new MrgeReleaseSpbVModel()
                 {
                     slbh = A.SLBH,
                     qlrmc = A.QLRMC,
                     bdczmh = dyInfo.BDCZMH,
                     bdcdyh = B.BDCDYH,
                     zl = C.ZL,
                     qlrlx = A.QLRLX,
                     ywrmc = A.QLRMC,
                     bdbzqse = dyInfo.BDBZZQSE,
                     lxqx = dyInfo.QLJSSJ+"",
                     dbfw = dyInfo.DBFW,
                     yd1 = dyInfo.QRZYQK,
                     qt = dyInfo.QT,
                     sjr = C.SJR,
                     cssj = C.SJSJ,
                     bz = C.SJBZ,
                 }).ToListAsync();
                if (data.Count > 0)
                {
                    QlrInfoVModel qlrModel = GetPersonList(xid);
                    if (qlrModel.dyqrList != null)
                    {
                        model.qlrmc = qlrModel.dyqrList.qlrmc + ",";
                    }
                    if (qlrModel.dyrList != null)
                    {
                        model.ywrmc += qlrModel.dyrList.qlrmc + ",";

                    }
                    model.qlrmc = model.qlrmc.TrimEnd(',');
                    model.ywrmc = model.ywrmc.TrimEnd(',');
                    model.slbh = data[0].slbh;
                    model.bdczmh = data[0].bdczmh;
                    model.bdcdyh = data[0].bdcdyh;
                    model.zl = data[0].zl;
                    model.bdbzqse = data[0].bdbzqse;
                    model.lxqx = Convert.ToDateTime(dyInfo.QLQSSJ).ToString("D") + "至\r\n" + Convert.ToDateTime(dyInfo.QLJSSJ).ToString("D");
                    model.dbfw = data[0].dbfw;
                    model.yd1 = data[0].yd1;
                    model.qt = data[0].qt;
                    model.sjr = data[0].sjr;
                    model.cssj = data[0].cssj;
                    model.bz = data[0].bz;
                }
            }
            return model;
        }
        /// <summary>
        /// 抵押注销申请表打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        public async Task<DyYgSqbPrintVModel> DyYgSqbPrintPrint(string xid)
        {
            DyYgSqbPrintVModel model = new DyYgSqbPrintVModel();

            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            var strFile = "";
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var DyData = await base.Db.Queryable<DY_INFO, REGISTRATION_INFO, SJD_INFO, TSGL_INFO>((A, B, C,  E) => A.XID == B.XID && B.XID == C.XID && C.XID ==  E.XID ).Where((A, B, C,  E) => A.XID == xid && B.NEXT_XID == null).Select((A, B, C,  E) => new DyYgSqbPrintVModel
            {
                slbh = A.SLBH,
                sjr = C.SJR,
                djlx = A.DJLX,
                bdcdyh = A.BDCDYH,
                zl = C.ZL,
                //qllx = D.FW_QLLX_ZWM,
                //qlxz = D.FW_QLXZ_ZWMS,
                xgzh = A.XGZH,
                //jzmj = D.FW_JZMJ,
                //tdmj = D.TD_JZZDMJ,
                //tddymj = D.TD_DYTDMJ,
                //gytdmj = D.TD_GYTDMJ,
                bdcqlsdqx = "",//BDC 权利类型 终止时间 +“止” 
                bdbzqse = A.BDBZZQSE,
                zwlxqx  = "",
                dbfw = A.DBFW,
                djyy = A.DJYY,
                sfyd1 = A.SFCZYD,
                sfyd2= A.SFCZYD,
                qlqssj = A.QLQSSJ,
                qljssj = A.QLJSSJ,
            }).Distinct().ToListAsync();

            if (DyData.Count > 0)
            {
                QlrInfoVModel qlrModel = GetPersonList(xid);
                if (qlrModel.dyrList != null)
                {
                    DyData[0].qlrxm = qlrModel.dyrList.qlrmc;
                    DyData[0].qlr_zjzl = qlrModel.dyrList.zjlb_zwm;
                    DyData[0].qlr_zjhm = qlrModel.dyrList.zjhm;
                }
                if (qlrModel.dyqrList != null)
                {
                    DyData[0].ywrmc = qlrModel.dyqrList.qlrmc;
                    DyData[0].ywr_zjzl = qlrModel.dyqrList.zjlb_zwm;
                    DyData[0].ywr_zjhm = qlrModel.dyqrList.zjhm;
                }
                
                DyData[0].djyy = DyData[0].djyy.ToString();
                DyData[0].zwlxqx = Convert.ToDateTime(DyData[0].qlqssj).ToString("D") + "至\r\n" + Convert.ToDateTime(DyData[0].qljssj).ToString("D");
                var FileData = base.Db.Queryable<PUB_ATT_FILE>().Where(i => i.XID == xid).GroupBy(i => new { group_name = i.GROUP_NAME }).Select(i => new
                {
                    group_name = i.GROUP_NAME,
                    count = SqlFunc.AggregateCount(i.GROUP_NAME)
                }).ToList();

                if (FileData.Count > 0)
                {
                    for (int i = 0; i < FileData.Count; i++)
                    {
                        strFile += i + 1 + "." + FileData[i].group_name + ":" + FileData[i].count + "个" + "\r\n";
                    }
                    DyData[0].zmwj = strFile;
                    //model = data[0];
                }
                model = DyData[0];
                //model.qlsdfs = "地表";
                if (model.tdmj == 0 || model.tdmj == null)
                {
                    model.mj = "/" + model.jzmj.ToString() + "㎡";
                }
                else
                {
                    if (model.tdlx == "宅基地使用权")
                    {
                        model.mj = "土地登记面积：" + model.gytdmj.ToString() + "㎡" + "/房屋建筑面积：" + model.jzmj.ToString() + "㎡";
                    }
                    else
                    {
                        model.mj = "共有宗地面积面积：" + model.tdmj.ToString() + "㎡" + "/房屋建筑面积：" + model.jzmj.ToString() + "㎡";
                    }

                }
              /*  if (model.bdclx == "房屋")
                {
                    model.bdclx = "房屋所有权";
                }
                if (model.tdyt == null)
                {
                    model.ghyt = "/" + model.ghyt;
                    model.bdclx = "/" + model.bdclx;
                }
                else
                {
                    model.ghyt = model.tdyt + "/" + model.ghyt;
                    model.bdclx = "土地/" + model.bdclx;
                }*/
                if (model.djsy == "1")
                {
                    model.djsy = "一般抵押";
                }
                else if (model.djsy == "2")
                {
                    model.djsy = "最高额抵押";
                }
                else
                {
                    model.djsy = "其它";
                }

                model.djlx = "预告登记";

            }
            return model;

        }

        /// <summary>
        /// 抵押注销申请表打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        public async Task<MrgeReleaseSqbVModel> MrgeReleaseSqbPrint(string xid)
        {
            MrgeReleaseSqbVModel model = new MrgeReleaseSqbVModel();

            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};

            base.ChangeDB(SysConst.DB_CON_IIRS);
            var FSLBH = "";
            var QLslbh = "";
            var sysDic = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 1, 3, 4, 5, 6, 7, 8, 9 }).ToListAsync();
            var DJGLINFO = base.Db.Queryable<XGDJGL_INFO>().Where(DY => DY.XID == xid).ToList();

            if (DJGLINFO != null)
            {
                for (var i = 0; i < DJGLINFO.Count; i++)
                {
                    if (DJGLINFO[i].XGZLX == "房屋抵押证明")
                    {
                        FSLBH = DJGLINFO[i].FSLBH;
                    }
                    else if (DJGLINFO[i].XGZLX == "房屋不动产证")
                    {
                        QLslbh = DJGLINFO[i].FSLBH;
                    }
                }
            }
            base.ChangeDB(SysConst.DB_CON_BDC);
            var dyInfo = base.Db.Queryable<DJ_DY>().Where(DY => DY.SLBH == FSLBH).Single();
            var DYRSLBH = DJGLINFO[0].XGZLX == "房屋抵押证明" ? DJGLINFO[0].FSLBH : DJGLINFO[1].FSLBH;
            var qlrinfo = await base.Db.Queryable<DJ_QLRGL>().Where(it => it.SLBH == DYRSLBH).ToListAsync();
            if (dyInfo != null)
            {
                if (dyInfo.DYFS == "1")
                {
                    dyInfo.DYFS = "一般抵押";
                }
                else if (dyInfo.DYFS == "2")
                {
                    dyInfo.DYFS = "最高额抵押";
                }
                else if (dyInfo.DYFS == "3")
                {
                    dyInfo.DYFS = "在建工程抵押";
                }
                else
                {
                    dyInfo.DYFS = "其他";
                }
            }

            var QL_FWinfo = base.Db.Queryable<QL_FWXG>().Where(A => A.SLBH == QLslbh).Single();
            var QL_TDinfo = base.Db.Queryable<QL_TDXG>().Where(A => A.SLBH == QLslbh).Single();

            var TDQLLX = sysDic.Where(s => s.GID == 6 && s.DEFINED_CODE == QL_TDinfo.QLLX).FirstOrDefault();
            var TDQLXZ = sysDic.Where(s => s.GID == 7 && s.DEFINED_CODE == QL_TDinfo.QLXZ).FirstOrDefault();
            var FWQLLX = sysDic.Where(s => s.GID == 4 && s.DEFINED_CODE == QL_FWinfo.QLLX).FirstOrDefault();
            var FWQLXZ = sysDic.Where(s => s.GID == 3 && s.DEFINED_CODE == QL_FWinfo.QLXZ).FirstOrDefault();

            base.ChangeDB(SysConst.DB_CON_IIRS);
            string strFile = "";
            var data = await base.Db.Queryable<XGDJZX_INFO, SJD_INFO>((A, C) => A.XID == C.XID)
                .Where(A => A.XID == xid)
               .Select((A, C) => new MrgeReleaseSqbVModel()
               {
                   slbh = A.SLBH,
                   sjrq = A.SQRQ,
                   sjr = A.SJR,
                   djsy = dyInfo.DYFS,
                   bdcdyh = dyInfo.BDCDYH,
                   zl = C.ZL,
                   bdczsh = A.XGZH,
                   mj = dyInfo.DYMJ,
                   bdczmh = A.XGZH,
                   bdbzqse = dyInfo.BDBZZQSE,
                   dbfw = dyInfo.DBFW,
                   sfyd = dyInfo.QRZYQK,
                   djyy = A.DJYY,
                   bz = C.SJBZ,
               }).ToListAsync();
            if (data.Count > 0)
            {
                QlrInfoVModel qlrModel = GetPersonList(xid);

                if (qlrModel.dyqrList != null)
                {
                    data[0].dyqrmc = qlrModel.dyqrList.qlrmc;
                    data[0].dyqr_zjhm = qlrModel.dyqrList.zjhm;
                    data[0].dyqr_zjlb = qlrModel.dyqrList.zjlb_zwm;
                }
                if (qlrModel.dyrList != null)
                {
                    data[0].dyrmc = qlrModel.dyrList.qlrmc;
                    data[0].dyr_zjhm = qlrModel.dyrList.zjhm;
                    data[0].dyr_zjlb = qlrModel.dyrList.zjlb_zwm;
                }
                model.djlb = "抵押注销";
                for (int i = 0; i < data.Count; i++)
                {
                    model.slbh = data[i].slbh;
                    model.sjrq = data[i].sjrq;
                    model.sjr = data[i].sjr;
                    model.djsy = data[i].djsy;
                    model.mj = data[i].mj;
                    model.bdczmh = data[i].bdczmh;
                    model.bdcdyh = data[i].bdcdyh;
                    model.zl = data[i].zl;
                    model.bz = data[i].bz;
                    model.dbfw = data[i].dbfw;
                    model.sfyd = data[i].sfyd;
                    model.djyy = data[i].djyy;
                    model.bdbzqse = data[i].bdbzqse;
                }
                for (int i = 0; i < qlrinfo.Count; i++)
                {
                    if (qlrinfo[i].QLRLX == "抵押权人")
                    {
                        model.dyqrmc = qlrinfo[i].QLRMC + ",";
                        model.dyqr_zjhm = qlrinfo[i].ZJHM + ",";
                        model.dyqr_zjlb = qlrModel.dyqrList.zjlb_zwm;
                    }
                    else if (qlrinfo[i].QLRLX == "抵押人")
                    {
                        model.dyrmc = qlrModel.dyrList.qlrmc + ",";
                        model.dyr_zjhm = qlrModel.dyrList.zjhm + ",";
                        model.dyr_zjlb = qlrModel.dyrList.zjlb_zwm;
                    }
                }
                model.dyrmc = model.dyrmc.TrimEnd(',');
                model.dyqrmc = model.dyqrmc.TrimEnd(',');
                model.dyqr_zjhm = model.dyqr_zjhm.TrimEnd(',');
                model.dyr_zjhm = model.dyr_zjhm.TrimEnd(',');
                model.qllx = FWQLLX != null ? FWQLLX.DNAME : String.Empty;
                model.qlxz = FWQLXZ != null ? FWQLXZ.DNAME : String.Empty;
                model.zwlxqx = Convert.ToDateTime(dyInfo.QLQSSJ).ToString("D") + "至\r\n" + Convert.ToDateTime(dyInfo.QLJSSJ).ToString("D");


                var FileData = base.Db.Queryable<PUB_ATT_FILE>().Where(i => i.XID == xid).GroupBy(i => new { group_name = i.GROUP_NAME }).Select(i => new
                {
                    group_name = i.GROUP_NAME,
                    count = SqlFunc.AggregateCount(i.GROUP_NAME)
                }).ToList();

                if (FileData.Count > 0)
                {
                    for (int i = 0; i < FileData.Count; i++)
                    {
                        strFile += i + 1 + "." + FileData[i].group_name + ":" + FileData[i].count + "个" + "\r\n";
                    }
                    data[0].djyyzmwj = strFile;
                    model.djyyzmwj = data[0].djyyzmwj;
                }
            }
            return model;

        }
        /// <summary>
        /// 抵押注销 - 不动产登记收件收据打印
        /// </summary>
        /// <param name="xid"></param>
        /// <returns></returns>
        public async Task<MrgeReleaseSjPrintVModel> MrgeReleaseSjPrint(string xid)
        {
            MrgeReleaseSjPrintVModel model = new MrgeReleaseSjPrintVModel();
            string strFile = "";
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var data = await base.Db.Queryable<SJD_INFO, XGDJZX_INFO, QLRGL_INFO>((A, B, C) => A.XID == B.XID && B.XID == C.XID)
                .Where((A, B, C) => A.XID == xid)
                .GroupBy((A, B, C) => new
                {
                    slbh = A.SLBH,
                    xgzh = B.XGZH,
                    jjr = A.SJR,
                    tel = A.TZRDH,
                    zl = A.ZL,
                    sjr = A.SJR,
                    djrq = A.SJSJ,
                    cnsj = A.CNSJ
                }).Select((A, B, C) => new MrgeReleaseSjPrintVModel()
                {
                    slbh = A.SLBH,
                    xgzh = B.XGZH,
                    sqr = SqlFunc.MappingColumn(C.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(QLRMC))"),
                    jjr = A.SJR,
                    tel = A.TZRDH,
                    zl = A.ZL,
                    sjr = A.SJR,
                    djrq = A.SJSJ,
                    cnsj = A.CNSJ
                }).ToListAsync();
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var DJZX_INFO = base.Db.Queryable<XGDJZX_INFO>().Where(IN => IN.XID == xid).ToList();

            if (data.Count > 0)
            {
                data[0].ywlx = DJZX_INFO[0].DJLX + "(" + DJZX_INFO[0].DJYY + ")";
                QlrInfoVModel qlrModel = GetPersonList(xid);
                if (qlrModel.dyqrList != null)
                {
                    data[0].sqr = qlrModel.dyqrList.qlrmc;
                }
                if (qlrModel.dyrList != null)
                {

                    data[0].dyr = qlrModel.dyrList.qlrmc;

                }
                var FileData = base.Db.Queryable<PUB_ATT_FILE>().Where(i => i.XID == xid).GroupBy(i => new { group_name = i.GROUP_NAME }).Select(i => new
                {
                    group_name = i.GROUP_NAME,
                    count = SqlFunc.AggregateCount(i.GROUP_NAME)
                }).ToList();

                if (FileData.Count > 0)
                {
                    for (int i = 0; i < FileData.Count; i++)
                    {
                        strFile += i + 1 + "." + FileData[i].group_name + ":" + FileData[i].count + "个" + "\r\n";
                    }
                    data[0].fj = strFile;
                    model = data[0];
                }
                model = data[0];
            }
            return model;
        }
        #endregion

    }

}
