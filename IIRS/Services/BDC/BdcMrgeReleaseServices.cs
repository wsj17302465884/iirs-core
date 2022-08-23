using IIRS.IRepository.Base;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using Spire.Pdf.Widget;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class BdcMrgeReleaseServices : BaseServices, IBdcMrgeReleaseServices
    {
        private readonly ILogger<BdcMrgeReleaseServices> _logger;

        IDBTransManagement _dbTransManagement;
        public BdcMrgeReleaseServices(IDBTransManagement dbTransManagement, ILogger<BdcMrgeReleaseServices> logger) : base(dbTransManagement)
        {
            this._logger = logger;
            this._dbTransManagement = dbTransManagement;
        }

        /// <summary>
        /// 设置《不动产抵押登记申请表》
        /// </summary>
        /// <param name="pdfFile">源文件</param>
        /// <param name="xid">业务编号</param>
        public void SetPrintApplyData(PdfDocument pdfFile, string xid)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var vModel = base.Db.Queryable<SysDataRecorderModel>().Where(ZX => ZX.BUS_PK == xid).Single();
            if (vModel != null)
            {
                var sysDicList = base.Db.Queryable<SYS_DIC>().Where(SYS => (SqlFunc.ContainsArray(new int[] { 3, 4, 5, 8 }, SYS.GID))).ToList();
                BdcMrgeReleaseVModel releaseInfo = null;
                try
                {
                    releaseInfo = JsonConvert.DeserializeObject<BdcMrgeReleaseVModel>(vModel.SAVEDATAJSON);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("数据格式错误,原因:" + ex.Message);
                    //return new MessageModel<string>()
                    //{
                    //    msg = "数据保存格式错误，请与管理员联系",
                    //    success = false
                    //};
                }

                base.ChangeDB(SysConst.DB_CON_BDC);
                //base.Db.Aop.OnLogExecuting = (sql, pars) =>
                //{
                //    _logger.LogDebug(sql);
                //};
                var dyResult = base.Db.Queryable<DJ_TSGL, DJ_DY>((TS, DY) => TS.SLBH == DY.SLBH)
                    .Where((TS, DY) => DY.BDCZMH == releaseInfo.selectHouse.BDCZH)
                    .Select((TS, DY) => new { DY.DYFS, DY.DYLX, TS.BDCLX, TS.TSTYBM }).Single();

                var qlResult = base.Db.Queryable<DJ_TSGL, QL_FWXG, QL_TDXG>((TS, FW, TD) => new object[] { JoinType.Left, TS.SLBH == FW.SLBH, JoinType.Left, TS.SLBH == TD.SLBH })
                    .Where((TS, FW, TD) => TS.TSTYBM == dyResult.TSTYBM && (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null) && TS.DJZL == "权属")
                    .Select((TS, FW, TD) => new { TS.BDCLX, FW.GHYT, FW.QLLX, FW.QLXZ, FW.JZMJ, TD.JZZDMJ, TD.TDYT }).Single();
                //if (qlResult.DYLX == "在建工程抵押")
                //{

                //}
                string fw_qllx_zwm = string.Empty, fw_qlxz_zwm = string.Empty, ghyt_zwm = string.Empty;
                var dicVal = sysDicList.Where(s => s.GID == 4 && s.DEFINED_CODE == qlResult.QLLX).SingleOrDefault();
                if (dicVal != null)//房屋权利类型
                {
                    fw_qllx_zwm = dicVal.DNAME;
                }
                dicVal = sysDicList.Where(s => s.GID == 3 && s.DEFINED_CODE == qlResult.QLXZ).SingleOrDefault();
                if (dicVal != null)//房屋权利性质中文描述
                {
                    fw_qlxz_zwm += dicVal.DNAME;
                }
                String WingdingsfontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings.ttf");
                String Wingdings2fontFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Template", "Wingdings-2.ttf");
                string mj = string.Empty;
                if (dyResult.BDCLX == "房屋")
                {
                    if (qlResult.JZZDMJ != null)
                    {
                        mj += $@"共有宗地面积:{ qlResult.JZZDMJ}m²";
                    }
                    mj += @"  / ";
                    if (qlResult.JZMJ != null)
                    {
                        mj += $@"房屋建筑面积:{ qlResult.JZMJ}m²";
                    }
                    #region 房屋、土地规划用途
                    if (!string.IsNullOrEmpty(qlResult.TDYT))//如果是房屋，土地用途只能为一种用途
                    {
                        dicVal = sysDicList.Where(s => s.GID == 8 && s.DEFINED_CODE == qlResult.TDYT).SingleOrDefault();
                        if (dicVal != null)//房屋权利性质中文描述
                        {
                            ghyt_zwm += dicVal.DNAME;
                        }
                    }
                    ghyt_zwm += " / ";
                    if (!string.IsNullOrEmpty(qlResult.GHYT))
                    {
                        dicVal = sysDicList.Where(s => s.GID == 5 && s.DEFINED_CODE == qlResult.GHYT).SingleOrDefault();
                        if (dicVal != null)//房屋权利性质中文描述
                        {
                            ghyt_zwm += dicVal.DNAME;
                        }
                    }
                    #endregion
                }
                if (dyResult.BDCLX == "土地")
                {
                    mj += $@"共有宗地面积:{ qlResult.JZZDMJ}m²";

                    #region 土地规划用途,可能存在逗号分隔，即:一块土地有多个用途
                    if (!string.IsNullOrEmpty(qlResult.TDYT))//如果是房屋，土地用途只能为一种用途
                    {
                        var tmpStrArrat = string.Concat(qlResult.TDYT).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
                        if (tmpStrArrat.Length > 0)
                        {
                            string[] tmpStrNameArrat = new string[tmpStrArrat.Length];
                            int index = 0;
                            foreach (var tdyt in tmpStrArrat)
                            {
                                dicVal = sysDicList.Where(s => s.GID == 8 && s.DEFINED_CODE == tdyt).SingleOrDefault();
                                if (dicVal != null)//房屋权利性质中文描述
                                {
                                    tmpStrNameArrat[index] = dicVal.DNAME;
                                }
                                index++;
                            }
                            ghyt_zwm = string.Join(",", tmpStrNameArrat) + " / ";
                        }
                    }
                    #endregion
                }
                var formWidget = pdfFile.Form as PdfFormWidget;
                bool isElse = true;
                for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                {
                    isElse = false;
                    var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                    if (print != null)
                    {
                        switch (print.Name)
                        {
                            case "slbh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.SLBH;
                                break;
                            case "djrq":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.SJSJ.ToString("yyyy-MM-dd HH:mm:ss");
                                break;
                            case "sjr":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.SJR;
                                break;
                            case "qlrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.selectRightPerson[0].QLRMC;
                                break;
                            case "qlr_zjlb":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.selectRightPerson[0].ZJLB_ZWM;
                                break;
                            case "qlr_zjhm":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.selectRightPerson[0].ZJHM;
                                break;
                            case "ywrmc":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.Join(",", releaseInfo.selectPerson.Cast<DyPersonVModel>().Select(s => s.QLRMC).ToArray());
                                break;
                            case "ywr_zjlb":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.Join(",", releaseInfo.selectPerson.Cast<DyPersonVModel>().Select(s => s.ZJLB_ZWM).Distinct().ToArray());
                                break;
                            case "ywr_zjhm":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.Join(",", releaseInfo.selectPerson.Cast<DyPersonVModel>().Select(s => s.ZJHM).Distinct().ToArray());
                                break;

                            case "bdcdyh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                if (releaseInfo.selectHouse.children.Count == 0)
                                {
                                    throw new ApplicationException("尚未选择房屋");
                                }
                                else if (releaseInfo.selectHouse.children.Count == 1)
                                {
                                    print.Text = releaseInfo.selectHouse.children[0].BDCDYH;
                                }
                                else
                                {
                                    print.Text = releaseInfo.selectHouse.children[0].BDCDYH + "等" + releaseInfo.selectHouse.children.Count + "个";
                                }
                                break;
                            case "ZL":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.ZL;
                                break;
                            case "qllx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = fw_qllx_zwm;
                                break;
                            case "qlxz":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = fw_qlxz_zwm;
                                break;
                            case "jzmj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = mj;
                                break;
                            case "bdczh":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.selectHouse.children[0].BDCZH;
                                break;

                            case "lxqx":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = string.Concat(releaseInfo.LXQX);
                                break;
                            case "dyfw?????????":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.ZWLXQXJZRQ.ToString();
                                break;
                            case "djyy":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.BDBZQSE.ToString();
                                break;
                            case "zmwj":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.BZ;
                                break;
                            case "dylx_1":
                                if (dyResult.DYFS == "1")//一般抵押
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "dylx_2":
                                if (dyResult.DYFS == "2")//最高额抵押
                                {
                                    print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                    print.Text += "R";
                                }
                                else
                                {
                                    print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                    print.Text = "o";
                                }
                                break;
                            case "dylx_3":
                                print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                print.Text = "o";
                                break;
                            case "dylx_4":
                                print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                print.Text = "o";
                                break;
                            case "djlx_1":
                                print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                print.Text = "o";
                                break;
                            case "djlx_2":
                                print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                print.Text = "o";
                                break;
                            case "djlx_3":
                                print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                print.Text = "o";
                                break;
                            case "djlx_4":
                                print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                print.Text += "R";
                                break;
                            case "djlx_5":
                                print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                print.Text = "o";
                                break;
                            case "djlx_6":
                                print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                print.Text = "o";
                                break;
                        }
                    }
                    if (isElse)
                    {
                        if (print.Name == "" || print.Name == "")
                        {

                        }
                    }
                }
                formWidget.IsFlatten = true;
            }

        }

        /// <summary>
        /// 设置《不动产抵押登记审批表》
        /// </summary>
        /// <param name="pdfFile">源文件</param>
        /// <param name="xid">业务编号</param>
        public void SetPrintApproveData(PdfDocument pdfFile, string xid)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var vModel = base.Db.Queryable<SysDataRecorderModel>().Where(ZX => ZX.BUS_PK == xid).Single();
            if (vModel != null)
            {
                var sysDicList = base.Db.Queryable<SYS_DIC>().Where(SYS => (SqlFunc.ContainsArray(new int[] { 3, 4, 5, 8 }, SYS.GID))).ToList();
                BdcMrgeReleaseVModel releaseInfo = null;
                try
                {
                    releaseInfo = JsonConvert.DeserializeObject<BdcMrgeReleaseVModel>(vModel.SAVEDATAJSON);
                }
                catch (Exception ex)
                {
                    throw new ApplicationException("数据格式错误,原因:" + ex.Message);
                    //return new MessageModel<string>()
                    //{
                    //    msg = "数据保存格式错误，请与管理员联系",
                    //    success = false
                    //};
                }
                base.ChangeDB(SysConst.DB_CON_BDC);
                var qlResult = base.Db.Queryable<DJ_TSGL, QL_FWXG, QL_TDXG>((TS, FW, TD) => new object[] { JoinType.Left, TS.SLBH == FW.SLBH, JoinType.Left, TS.SLBH == TD.SLBH })
                    .Where((TS, FW, TD) => TS.SLBH == releaseInfo.selectHouse.children[0].SLBH)
                    .Select((TS, FW, TD) => new { TS.BDCLX, FW.GHYT, FW.QLLX, FW.QLXZ, FW.JZMJ, TD.JZZDMJ, TD.TDYT }).Single();

                string fw_qllx_zwm = string.Empty, fw_qlxz_zwm = string.Empty, ghyt_zwm = string.Empty;
                var dicVal = sysDicList.Where(s => s.GID == 4 && s.DEFINED_CODE == qlResult.QLLX).SingleOrDefault();
                if (dicVal != null)//房屋权利类型
                {
                    fw_qllx_zwm = dicVal.DNAME;
                }
                dicVal = sysDicList.Where(s => s.GID == 3 && s.DEFINED_CODE == qlResult.QLXZ).SingleOrDefault();
                if (dicVal != null)//房屋权利性质中文描述
                {
                    fw_qlxz_zwm += dicVal.DNAME;
                }
                string mj = string.Empty;
                if (qlResult.BDCLX == "房屋")
                {
                    if (qlResult.JZZDMJ != null)
                    {
                        mj += $@"共有宗地面积:{ qlResult.JZZDMJ}m²";
                    }
                    mj += @"  / ";
                    if (qlResult.JZMJ != null)
                    {
                        mj += $@"房屋建筑面积:{ qlResult.JZMJ}m²";
                    }
                    #region 房屋、土地规划用途
                    if (!string.IsNullOrEmpty(qlResult.TDYT))//如果是房屋，土地用途只能为一种用途
                    {
                        dicVal = sysDicList.Where(s => s.GID == 8 && s.DEFINED_CODE == qlResult.TDYT).SingleOrDefault();
                        if (dicVal != null)//房屋权利性质中文描述
                        {
                            ghyt_zwm += dicVal.DNAME;
                        }
                    }
                    ghyt_zwm += " / ";
                    if (!string.IsNullOrEmpty(qlResult.GHYT))
                    {
                        dicVal = sysDicList.Where(s => s.GID == 5 && s.DEFINED_CODE == qlResult.GHYT).SingleOrDefault();
                        if (dicVal != null)//房屋权利性质中文描述
                        {
                            ghyt_zwm += dicVal.DNAME;
                        }
                    }
                    #endregion
                }
                if (qlResult.BDCLX == "土地")
                {
                    mj += $@"共有宗地面积:{ qlResult.JZZDMJ}m²";

                    #region 土地规划用途,可能存在逗号分隔，即:一块土地有多个用途
                    if (!string.IsNullOrEmpty(qlResult.TDYT))//如果是房屋，土地用途只能为一种用途
                    {
                        var tmpStrArrat = string.Concat(qlResult.TDYT).Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Distinct().ToArray();
                        if (tmpStrArrat.Length > 0)
                        {
                            string[] tmpStrNameArrat = new string[tmpStrArrat.Length];
                            int index = 0;
                            foreach (var tdyt in tmpStrArrat)
                            {
                                dicVal = sysDicList.Where(s => s.GID == 8 && s.DEFINED_CODE == tdyt).SingleOrDefault();
                                if (dicVal != null)//房屋权利性质中文描述
                                {
                                    tmpStrNameArrat[index] = dicVal.DNAME;
                                }
                                index++;
                            }
                            ghyt_zwm = string.Join(",", tmpStrNameArrat) + " / ";
                        }
                    }
                    #endregion
                }
                var formWidget = pdfFile.Form as PdfFormWidget;
                for (int i = 0; i < formWidget.FieldsWidget.List.Count; i++)
                {
                    var print = formWidget.FieldsWidget.List[i] as PdfTextBoxFieldWidget;
                    if (print != null)
                    {
                        switch (print.Name)
                        {
                            case "SLBH":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.SLBH;
                                break;
                            case "BDCZMH":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.selectHouse.BDCZH;
                                break;
                            case "BDCDYH":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                if (releaseInfo.selectHouse.children.Count == 0)
                                {
                                    throw new ApplicationException("尚未选择房屋");
                                }
                                else if (releaseInfo.selectHouse.children.Count == 1)
                                {
                                    print.Text = releaseInfo.selectHouse.children[0].BDCDYH;
                                }
                                else
                                {
                                    print.Text = releaseInfo.selectHouse.children[0].BDCDYH + "等" + releaseInfo.selectHouse.children.Count + "个";
                                }
                                break;
                            case "ZL":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.ZL;
                                break;
                            case "QLRMC":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.selectRightPerson[0].QLRMC;
                                break;
                            case "QLLX":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = fw_qllx_zwm;
                                break;
                            case "QLXZ":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = fw_qlxz_zwm;
                                break;
                            case "BDCLX":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = qlResult.BDCLX;
                                break;
                            case "FWJZMJ":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = mj;
                                break;
                            case "YBDCQZSH":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.selectHouse.children[0].BDCZH;
                                break;
                            case "YT":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = ghyt_zwm;
                                break;
                            case "MJ":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = mj;
                                break;
                            case "ZWLXQX":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.ZWLXQXJZRQ.ToString();
                                break;
                            case "BDBZQSE":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.BDBZQSE.ToString();
                                break;
                            case "QTQLZK":
                                print.Font = new PdfCjkStandardFont(PdfCjkFontFamily.SinoTypeSongLight, 10f);
                                print.Text = releaseInfo.BZ;
                                break;
                                //case "dylx_1":
                                //    if (model.dylx == "一般抵押")
                                //    {
                                //        print.Font = new PdfTrueTypeFont(Wingdings2fontFile, 10f);
                                //        print.Text += "R";
                                //    }
                                //    else
                                //    {
                                //        print.Font = new PdfTrueTypeFont(WingdingsfontFile, 10f);
                                //        print.Text = "o";
                                //    }
                        }
                    }
                }
                formWidget.IsFlatten = true;
            }

        }

        /// <summary>
        /// 查询抵押项目登记信息
        /// </summary>
        /// <param name="DY_SLBH">抵押受理编号</param>
        /// <returns></returns>
        public async Task<MrgeReleaseVModel> GetMrgeCertHouseInfo(string DY_SLBH)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDicList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 1)).ToListAsync();
            base.ChangeDB(SysConst.DB_CON_BDC);
            MrgeReleaseVModel dyData = new MrgeReleaseVModel();
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            //            var resultDY = await base.Db.Queryable<DJ_DY, DJ_QLRGL, DJ_SJD, DJ_TSGL>((DY, R, S, TS)
            //     => DY.SLBH == R.SLBH && DY.SLBH == S.SLBH && DY.SLBH == TS.SLBH)
            //.Where((DY, R, S, TS) => ((DY.LIFECYCLE == null || DY.LIFECYCLE == 0) && R.QLRLX == "抵押人") && DY.SLBH == DY_SLBH)
            //.GroupBy((DY, R, S, TS) => new
            //{
            //    TS.TSTYBM,
            //    TS.BDCLX,
            //    DY.BDCZMH,
            //    DY.SLBH,
            //    S.ZL,
            //    DY.BDCDYH,
            //    DY.XGZH,
            //    DY.DYLX,
            //    DY.DYSW,
            //    DY.DYFS,
            //    DY.DYMJ,
            //    DY.PGJE,
            //    DY.HTH,
            //    DY.FJ,
            //    DY.QLQSSJ,
            //    DY.QLJSSJ,
            //    DY.DYQX,
            //    DY.QRZYQK,
            //    DY.BDBZZQSE,
            //    DY.ZGZQSE,
            //    DY.DBFW
            //})
            //.Select((DY, R, S, TS) => new
            //{
            //    TS.TSTYBM,
            //    TS.BDCLX,
            //    BDCZH = DY.BDCZMH,
            //    SLBH = DY.SLBH,
            //    ZL = S.ZL,
            //    BDCDYH = DY.BDCDYH,
            //    ZSLX = "房屋抵押证明",
            //    QLRMC = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(R.QLRMC))"),
            //    XGZH = DY.XGZH,
            //    DYLX = DY.DYLX,
            //    DYSW = DY.DYSW,
            //    DYFS = DY.DYFS,
            //    DYMJ = DY.DYMJ,
            //    PGJE = DY.PGJE,
            //    HTH = DY.HTH,
            //    FJ = DY.FJ,
            //    QLQSSJ = DY.QLQSSJ,
            //    QLJSSJ = DY.QLJSSJ,
            //    DYQX = DY.DYQX,
            //    DY.QRZYQK,
            //    DY.BDBZZQSE,
            //    DY.ZGZQSE,
            //    DY.DBFW
            //}).SingleAsync();

            var resultDY = await base.Db.Queryable<DJ_DY, DJ_QLRGL, DJ_SJD>((DY, R, S)
=> DY.SLBH == R.SLBH && DY.SLBH == S.SLBH)
.Where((DY, R, S) => ((DY.LIFECYCLE == null || DY.LIFECYCLE == 0) && R.QLRLX == "抵押人") && DY.SLBH == DY_SLBH)
.GroupBy((DY, R, S) => new
{
    DY.BDCZMH,
    DY.SLBH,
    S.ZL,
    DY.BDCDYH,
    DY.XGZH,
    DY.DYLX,
    DY.DYSW,
    DY.DYFS,
    DY.DYMJ,
    DY.PGJE,
    DY.HTH,
    DY.FJ,
    DY.QLQSSJ,
    DY.QLJSSJ,
    DY.DYQX,
    DY.QRZYQK,
    DY.BDBZZQSE,
    DY.ZGZQSE,
    DY.DBFW
})
.Select((DY, R, S) => new
{
    BDCZH = DY.BDCZMH,
    SLBH = DY.SLBH,
    ZL = S.ZL,
    BDCDYH = DY.BDCDYH,
    ZSLX = "房屋抵押证明",
    QLRMC = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(R.QLRMC))"),
    XGZH = DY.XGZH,
    DYLX = DY.DYLX,
    DYSW = DY.DYSW,
    DYFS = DY.DYFS,
    DYMJ = DY.DYMJ,
    PGJE = DY.PGJE,
    HTH = DY.HTH,
    FJ = DY.FJ,
    QLQSSJ = DY.QLQSSJ,
    QLJSSJ = DY.QLJSSJ,
    DYQX = DY.DYQX,
    DY.QRZYQK,
    DY.BDBZZQSE,
    DY.ZGZQSE,
    DY.DBFW
}).SingleAsync();
            if (resultDY != null)
            {
                #region 抵押信息
                dyData.selectHouse = new MrgeReleaseHouseVModel()
                {
                    BDCZH = resultDY.BDCZH,
                    SLBH = resultDY.SLBH,
                    BDCDYH = resultDY.BDCDYH,
                    TSTYBM = "",
                    BDCLX = "房屋抵押证明",
                    ZSLX = resultDY.ZSLX,
                    ZL = resultDY.ZL,
                    QLRMC = resultDY.QLRMC,
                    hasChildren = true
                };
                dyData.QRZYQK = resultDY.QRZYQK;
                dyData.BDBZZQSE = resultDY.BDBZZQSE;
                dyData.ZGZQSE = resultDY.ZGZQSE;
                dyData.DBFW = resultDY.DBFW;
                dyData.ZL = resultDY.ZL;
                dyData.DYLX = resultDY.DYLX;
                dyData.DYSW = Convert.ToInt32(resultDY.DYSW);
                dyData.DYFS = resultDY.DYFS;
                dyData.dyMJ = resultDY.DYMJ ?? 0;
                dyData.PGJE = resultDY.PGJE ?? 0;
                dyData.HTH = resultDY.HTH;
                dyData.BZ = resultDY.FJ;
                dyData.LXQX = resultDY.DYQX;
                #endregion
                #region 添加抵押人和抵押权人信息
                var resultPerson = await base.Db.Queryable<DJ_QLRGL, DJ_QLR>((G, R) => G.QLRID == R.QLRID)
                    .Where((G, R) => G.SLBH == DY_SLBH)
                    .Select((G, R) => new
                    {
                        DH = G.DH,
                        GYFE = G.GYFE,
                        QLRID = G.QLRID,
                        QLRMC = R.QLRMC,
                        SXH = R.SXH,
                        ZJHM = R.ZJHM,
                        ZJLB = R.ZJLB,
                        QLRLX = G.QLRLX
                    }).ToListAsync();
                foreach (var user in resultPerson)
                {
                    var zjlb_zwmObj = sysDicList.Where(s => s.DEFINED_CODE == user.ZJLB).FirstOrDefault();
                    if (user.QLRLX == "抵押人")
                    {
                        dyData.selectPerson.Add(new DyPersonVModel()
                        {
                            DH = user.DH,
                            GYFE = user.GYFE,
                            QLRID = user.QLRID,
                            QLRMC = user.QLRMC,
                            //SXH = user.SXH,
                            ZJHM = user.ZJHM,
                            ZJLB = user.ZJLB,
                            ZJLB_ZWM = zjlb_zwmObj != null ? zjlb_zwmObj.DNAME : string.Empty,
                        });
                    }
                    else if (user.QLRLX == "抵押权人")
                    {
                        dyData.selectRightPerson.Add(new DyPersonVModel()
                        {
                            DH = user.DH,
                            GYFE = user.GYFE,
                            QLRID = user.QLRID,
                            QLRMC = user.QLRMC,
                            //SXH = user.SXH,
                            ZJHM = user.ZJHM,
                            ZJLB = user.ZJLB,
                            ZJLB_ZWM = zjlb_zwmObj != null ? zjlb_zwmObj.DNAME : string.Empty,
                        });
                    }
                }
                #endregion
                #region 添加抵押房产信息
                if (resultDY.DYLX == "预告抵押")
                {
                    dyData.selectHouse.children.AddRange(await base.Db.Queryable<DJ_XGDJGL, DJ_DY, DJ_YG, DJ_SJD, DJ_TSGL>((G, DY, YG, SD, TS) => DY.SLBH == G.ZSLBH && G.FSLBH == YG.SLBH && DY.SLBH == SD.SLBH && G.FSLBH == TS.SLBH)
                    .Where((G, DY, YG, SD, TS) => (DY.LIFECYCLE == 0 || DY.LIFECYCLE == null) //&& SqlFunc.Contains(G.BGLX, "抵押变更") 
                    && DY.SLBH == DY_SLBH)
                    .Select((G, DY, YG, SD, TS) => new MrgeReleaseHouseVModel()
                    {
                        BDCZH = YG.BDCZMH,
                        BDCDYH = YG.BDCDYH,
                        TSTYBM = TS.TSTYBM,
                        BDCLX = TS.BDCLX,
                        SLBH = DY.SLBH,
                        ZL = SD.ZL,
                        ZSLX = "房屋预告证明",
                        QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == DY.SLBH && F.QLRLX == "抵押人").Select(GG => GG.QLRMC)
                        //ZT = SqlFunc.Subqueryable<DJ_TSGL>().Where(TS => TS.SLBH == YG.SLBH).Select(TS => SqlFunc.MappingColumn(TS.SLBH, "WM_CONCAT(DISTINCT TO_CHAR(DJZL))"))
                    }).ToListAsync());
                }
                else if (resultDY.DYLX == "在建工程抵押")
                {
                    dyData.selectHouse.children.AddRange(await base.Db.Queryable<DJ_TSGL, DJ_DY, DJ_SJD, FC_H_QSDC>((TS, DY, SD, FC) => DY.SLBH == TS.SLBH && DY.SLBH == SD.SLBH && TS.TSTYBM == FC.TSTYBM)
                    .Where((TS, DY, SD, FC) => (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null) && DY.SLBH == DY_SLBH)
                    .Select((TS, DY, SD, FC) => new MrgeReleaseHouseVModel()
                    {
                        BDCDYH = DY.BDCDYH,
                        BDCZH = DY.BDCZMH,
                        TSTYBM = TS.TSTYBM,
                        BDCLX = TS.BDCLX,
                        SLBH = DY.SLBH,
                        ZL = SD.ZL,
                        ZSLX = "",
                        QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == DY.SLBH && F.QLRLX == "抵押人").Select(GG => GG.QLRMC)
                        //ZT = SqlFunc.MappingColumn(TS.SLBH, "WM_CONCAT(DISTINCT TO_CHAR(DJZL))")
                    }).ToListAsync());
                }
                else
                {
                    //base.Db.Aop.OnLogExecuting = (sql, pars) =>
                    //{
                    //    _logger.LogDebug(sql);
                    //};
                    dyData.selectHouse.children.AddRange(await base.Db.Queryable<DJ_XGDJGL, DJ_DY, DJ_DJB, DJ_SJD, DJ_TSGL>((G, DY, D, SD, TS) => D.SLBH == TS.SLBH && DY.SLBH == G.ZSLBH && G.FSLBH == D.SLBH && D.SLBH == SD.SLBH)
                    .Where((G, DY, D, SD, TS) => (DY.LIFECYCLE == 0 || DY.LIFECYCLE == null) && DY.SLBH == DY_SLBH)
                    .Select((G, DY, D, SD, TS) => new MrgeReleaseHouseVModel()
                    {
                        BDCDYH = D.BDCDYH,
                        BDCZH = D.BDCZH,
                        BDCLX = TS.BDCLX,
                        TSTYBM = TS.TSTYBM,
                        SLBH = D.SLBH,
                        ZL = SD.ZL,
                        ZSLX = D.ZSLX,
                        QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == DY.SLBH && F.QLRLX == "抵押人").Select(GG => GG.QLRMC)
                        //ZT = SqlFunc.Subqueryable<DJ_TSGL>().Where(TS => TS.SLBH == YG.SLBH).Select(TS => SqlFunc.MappingColumn(TS.SLBH, "WM_CONCAT(DISTINCT TO_CHAR(DJZL))"))
                    }).ToListAsync());
                }
                #endregion
            }
            return dyData;
        }
        /// <summary>
        /// 查询抵押预告登记信息
        /// </summary>
        /// <param name="DY_SLBH">抵押受理编号</param>
        /// <returns></returns>
        public async Task<MrgeReleaseVModel> GetAdvanceHouseInfo(string DY_SLBH)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDicList = await base.Db.Queryable<SYS_DIC>().Where(SYS => (SYS.GID == 1)).ToListAsync();
            base.ChangeDB(SysConst.DB_CON_BDC);
            MrgeReleaseVModel dyData = new MrgeReleaseVModel();
            var resultDY = await base.Db.Queryable<DJ_DY, DJ_QLRGL, DJ_SJD>((DY, R, S)
=> DY.SLBH == R.SLBH && DY.SLBH == S.SLBH)
.Where((DY, R, S) => ((DY.LIFECYCLE == null || DY.LIFECYCLE == 0) && R.QLRLX == "抵押人") && DY.SLBH == DY_SLBH)
.GroupBy((DY, R, S) => new
{
    DY.BDCZMH,
    DY.SLBH,
    S.ZL,
    DY.BDCDYH,
    DY.XGZH,
    DY.DYLX,
    DY.DYSW,
    DY.DYFS,
    DY.DYMJ,
    DY.PGJE,
    DY.HTH,
    DY.FJ,
    DY.QLQSSJ,
    DY.QLJSSJ,
    DY.DYQX,
    DY.QRZYQK,
    DY.BDBZZQSE,
    DY.ZGZQSE,
    DY.DBFW
})
.Select((DY, R, S) => new
{
    BDCZH = DY.BDCZMH,
    SLBH = DY.SLBH,
    ZL = S.ZL,
    BDCDYH = DY.BDCDYH,
    ZSLX = "房屋抵押证明",
    QLRMC = SqlFunc.MappingColumn(R.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR(R.QLRMC))"),
    XGZH = DY.XGZH,
    DYLX = DY.DYLX,
    DYSW = DY.DYSW,
    DYFS = DY.DYFS,
    DYMJ = DY.DYMJ,
    PGJE = DY.PGJE,
    HTH = DY.HTH,
    FJ = DY.FJ,
    QLQSSJ = DY.QLQSSJ,
    QLJSSJ = DY.QLJSSJ,
    DYQX = DY.DYQX,
    DY.QRZYQK,
    DY.BDBZZQSE,
    DY.ZGZQSE,
    DY.DBFW
}).SingleAsync();
            return dyData;
        }
            /// <summary>
            /// 查询要抵押不动产信息
            /// </summary>
            /// <param name="BDCDYH">不动产单元号</param>
            /// <param name="BDCZMH">不动产证明号</param>
            /// <param name="QLRMC">权利人名称</param>
            /// <param name="ZL">坐落</param>
            /// <param name="DY_SLBH">抵押受理编号</param>
            /// <param name="pageIndex">分页：页码</param>
            /// <param name="pageSize">分页：每个页码数据量</param>
            /// <returns>分页结果集</returns>
            public async Task<PageStringModel> GetMrgeCertInfo(string BDCDYH, string BDCZMH, string QLRMC, string ZL, string DY_SLBH, int pageIndex = 1, int pageSize = 10)
        {
            base.ChangeDB(SysConst.DB_CON_BDC);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
            string pageDataJson = await base.Db.Queryable<DJ_TSGL, DJ_DY, DJ_SJD>((TS, DY, SD) => new object[] { JoinType.Right, DY.SLBH == TS.SLBH, JoinType.Inner, SD.SLBH == DY.SLBH })
                .Where((TS, DY, SD) => (TS.LIFECYCLE == 0 || TS.LIFECYCLE == null)
                && (DY.LIFECYCLE == 0 || DY.LIFECYCLE == null))
                .WhereIF(!string.IsNullOrEmpty(QLRMC), @"EXISTS (SELECT * FROM V_DJ_QLRGL G WHERE G.QLRLX = '抵押人' AND G.SLBH = DY.SLBH AND QLRMC LIKE '%' || '%" + QLRMC + @"%' || ' %')")
                .WhereIF(!string.IsNullOrEmpty(DY_SLBH), (TS, DY, SD) => SqlFunc.Contains(DY.SLBH, DY_SLBH))
                .WhereIF(!string.IsNullOrEmpty(BDCDYH), (TS, DY, SD) => SqlFunc.Contains(DY.BDCDYH, BDCDYH))
                .WhereIF(!string.IsNullOrEmpty(BDCZMH), (TS, DY, SD) => SqlFunc.Contains(DY.BDCZMH, BDCZMH))
                .WhereIF(!string.IsNullOrEmpty(ZL), (TS, DY, SD) => SqlFunc.Contains(SD.ZL, ZL))
                .GroupBy((TS, DY, SD) => new { TS.BDCLX, DY.ZGZQSE, DY.BDBZZQSE, DY.DBFW, DY.SLBH, DY.BDCZMH, DY.BDCDYH, DY.DYLX, SD.ZL })
                .Select((TS, DY, SD) => new
                {
                    RN = SqlFunc.MappingColumn(DY.BDCDYH, "ROW_NUMBER() OVER(ORDER BY SYSDATE)"),
                    DY.SLBH,
                    DY.BDCZMH,
                    DY.BDBZZQSE,
                    DY.ZGZQSE,
                    DY.DBFW,
                    DY.BDCDYH,
                    DY.DYLX,
                    SD.ZL,
                    BDCLX = TS.BDCLX,
                    QLRMC = SqlFunc.Subqueryable<V_DJ_QLRGLModel>().Where(F => F.SLBH == DY.SLBH && F.QLRLX == "抵押人").Select(GG => GG.QLRMC),
                    ZT = SqlFunc.MappingColumn(TS.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(TS.DJZL))")
                }).ToJsonPageAsync(pageIndex, pageSize);
            return new PageStringModel()
            {
                data = pageDataJson,
                dataCount = 0,
                PageSize = pageSize,
                page = pageIndex
            };
        }

        /// <summary>
        /// 房屋抵押注销
        /// </summary>
        /// <param name="isInsert">当前操作是否暂存</param>
        /// <param name="AuzInfo">订单表</param>
        /// <param name="DjInfo">登记信息</param>
        /// <param name="jsonData">登记信息保存暂存信息表</param>
        /// <param name="zxInfo">注销信息表</param>
        /// <param name="spInfo">审批信息表</param>
        /// <param name="flowInfo">流程信息</param>
        /// <param name="TsglInfo">图属信息</param>
        /// <param name="XgdjglInfos">相关登记关联信息</param>
        /// <param name="qlrglInfos">权利人信息</param>
        /// <param name="sjdInfo">收件单</param>
        /// <param name="qlxgInfo">权利相关信息</param>
        /// <param name="uploadFiles">附件信息</param>
        /// <param name="OldXID">历史XID（仅当退回编辑时使用）</param>
        /// <returns>多表操作影响记录数之和</returns>
        public int MortgageRelease(bool isInsert, BankAuthorize AuzInfo, REGISTRATION_INFO DjInfo, SysDataRecorderModel jsonData, XGDJZX_INFO zxInfo, SPB_INFO spInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, SJD_INFO sjdInfo, QL_XG_INFO qlxgInfo, List<PUB_ATT_FILE> uploadFiles, string OldXID)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            try
            {
                int count = 0;
                //var delData = base.Db.Queryable<REGISTRATION_INFO>().Single(s => s.XID == DjInfo.XID || s.AUZ_ID == DjInfo.AUZ_ID);
                this._dbTransManagement.BeginTran();
                if (AuzInfo != null)//说明是暂存不做任何流程操作
                {
                    var bankDb = base.Db.Queryable<BankAuthorize>().Where(W => W.BID == AuzInfo.BID).Single();
                    if (bankDb != null)//为旧件
                    {
                        count += base.Db.Updateable(AuzInfo).UpdateColumns(auz => new
                        {
                            auz.STATUS,
                            auz.PRE_STATUS
                        }).Where(b => b.BID == AuzInfo.BID).ExecuteCommand();
                    }
                    else
                    {
                        if (AuzInfo != null)
                        {
                            count += base.Db.Insertable(AuzInfo).ExecuteCommand();
                        }
                    }
                    if (flowInfo != null)
                    {
                        count = base.Db.Insertable(flowInfo).ExecuteCommand();
                    }
                }
                bool isBack = !string.IsNullOrEmpty(OldXID);//历史XID，说明为退回件
                if (isBack)//如果是退回件，将当前XID置为历史
                {
                    REGISTRATION_INFO setHistory = new REGISTRATION_INFO()
                    {
                        NEXT_XID = DjInfo.XID
                    };
                    count = base.Db.Updateable(setHistory).UpdateColumns(it => new
                    {
                        it.NEXT_XID
                    }).Where(S => S.XID == OldXID).ExecuteCommand();

                    SysDataRecorderModel historyJson = new SysDataRecorderModel()
                    {
                        IS_STOP = 1
                    };
                    count = base.Db.Updateable(historyJson).UpdateColumns(it => new
                    {
                        it.IS_STOP
                    }).Where(S => S.BUS_PK == OldXID).ExecuteCommand();

                    //更新审批表XID
                    count += base.Db.Updateable(new SPB_INFO() { XID = DjInfo.XID }).UpdateColumns(sp => new
                    {
                        sp.XID
                    }).Where(S => S.XID == OldXID).ExecuteCommand();
                    count += base.Db.Insertable(DjInfo).ExecuteCommand();
                    count += base.Db.Insertable(jsonData).ExecuteCommand();
                }
                if (isInsert)//如果为新增操作
                {
                    count += base.Db.Insertable(DjInfo).ExecuteCommand();
                    count += base.Db.Insertable(zxInfo).ExecuteCommand();
                    count += base.Db.Insertable(sjdInfo).ExecuteCommand();
                    if (spInfo != null)
                    {
                        count += base.Db.Insertable(spInfo).ExecuteCommand();
                    }
                    count += base.Db.Insertable(jsonData).ExecuteCommand();
                }
                else
                {
                    string delXidArrayKey = string.IsNullOrEmpty(OldXID) ? DjInfo.XID : OldXID;
                    count = base.Db.Updateable(sjdInfo).Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    count = base.Db.Updateable(DjInfo).UpdateColumns(it => new
                    {
                        it.XID,
                        it.IS_ACTION_OK
                    }).Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    count += base.Db.Updateable(zxInfo).UpdateColumns(zl => new
                    {
                        zl.XGZH,
                        zl.DJLX,
                        zl.DJYY,
                        zl.DLRXM,
                        zl.DLJGMC,
                        zl.SQBZ,
                        zl.SPBZ,
                        zl.SQRQ,
                        zl.SJR,
                        zl.BDCDYH,
                        zl.XID
                    }).Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    if (spInfo != null)
                    {
                        count += base.Db.Updateable(spInfo).UpdateColumns(sp => new
                        {
                            sp.SLBH,
                            sp.SPDX,
                            sp.SPYJ,
                            sp.SPR,
                            sp.SPRQ,
                            sp.SPTXR,
                            sp.XID,
                            sp.SPBZ
                        }).Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    }
                    base.Db.Updateable(jsonData).WhereColumns(json => new
                    {
                        json.IS_STOP,
                        json.BUS_PK,
                        json.SAVEDATAJSON,
                        json.REMARKS1,
                        json.REMARKS2,
                        json.REMARKS3,
                        json.REMARKS4,
                        json.REMARKS5
                    }).Where(S => S.PK == jsonData.PK);
                    //由于修改数据集合对象，所以先删除再进行插入操作,由于退换件产生新XID所以本次操作删除原XID对应的数据

                    count += base.Db.Deleteable<TSGL_INFO>().Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    count += base.Db.Deleteable<XGDJGL_INFO>().Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    count += base.Db.Deleteable<QLRGL_INFO>().Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    count = base.Db.Deleteable<PUB_ATT_FILE>().Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                    count += base.Db.Deleteable<QL_XG_INFO>().Where(S => S.XID == delXidArrayKey).ExecuteCommand();
                }
                if (qlxgInfo != null)
                {
                    count += base.Db.Insertable(qlxgInfo).ExecuteCommand();
                }
                if (TsglInfo != null && TsglInfo.Count > 0)
                {
                    count += base.Db.Insertable(TsglInfo.ToArray()).ExecuteCommand();
                }
                if (XgdjglInfos != null && XgdjglInfos.Count > 0)
                {
                    count += base.Db.Insertable(XgdjglInfos.ToArray()).ExecuteCommand();
                }
                if (qlrglInfos != null && qlrglInfos.Count > 0)
                {
                    count += base.Db.Insertable(qlrglInfos.ToArray()).ExecuteCommand();
                }
                if (uploadFiles != null && uploadFiles.Count > 0)
                {
                    count += base.Db.Insertable(uploadFiles.ToArray()).ExecuteCommand();
                }
                this._dbTransManagement.CommitTran();
                return count;
            }
            catch (Exception ex)
            {
                this._dbTransManagement.RollbackTran();
                throw ex;
            }
        }

        public async Task<int> Mortgage(REGISTRATION_INFO DjInfo, IFLOW_DO_ACTION flowInfo, List<TSGL_INFO> TsglInfo, DY_INFO DyInfo, List<XGDJGL_INFO> XgdjglInfos, List<QLRGL_INFO> qlrglInfos, List<PUB_ATT_FILE> uploadFiles, bool IsUpdate, bool IsSubmitFlow)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            try
            {
                int count = 0;
                this._dbTransManagement.BeginTran();
                //base.Db.Aop.OnLogExecuting = (sql, pars) =>
                //{
                //    _logger.LogDebug(sql);
                //};
                var delData = base.Db.Queryable<REGISTRATION_INFO>().Single(s => s.XID == DjInfo.XID);
                if (delData != null && !string.IsNullOrEmpty(delData.XID))//如果存在置为历史
                {
                    if (IsUpdate)
                    {
                        if (IsSubmitFlow)
                        {
                            count += await base.Db.Updateable<REGISTRATION_INFO>().SetColumns(it => new REGISTRATION_INFO() { IS_ACTION_OK = 1 }).Where(it => it.XID == delData.XID).ExecuteCommandAsync();
                        }
                    }
                    else
                    {
                        count += await base.Db.Updateable<REGISTRATION_INFO>().SetColumns(it => new REGISTRATION_INFO() { NEXT_XID = DjInfo.XID }).Where(it => it.XID == delData.XID).ExecuteCommandAsync();
                    }
                }
                if (IsUpdate)
                {
                    count = await base.Db.Updateable(DjInfo).UpdateColumns(it => new
                    {
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
                    }).Where(S => S.XID == DjInfo.XID).ExecuteCommandAsync();
                    count += await base.Db.Updateable(DyInfo).UpdateColumns(it => new
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
                        it.DYQX
                    }).ExecuteCommandAsync();

                    //由于修改数据集合对象，所以先删除再进行插入操作
                    count += await base.Db.Deleteable<TSGL_INFO>().Where(S => S.XID == DjInfo.XID).ExecuteCommandAsync();

                    count += await base.Db.Deleteable<XGDJGL_INFO>().Where(S => S.XID == DjInfo.XID).ExecuteCommandAsync();
                    count += await base.Db.Deleteable<QLRGL_INFO>().Where(S => S.XID == DjInfo.XID).ExecuteCommandAsync();

                    if (uploadFiles != null && uploadFiles.Count > 0)
                    {
                        count = await base.Db.Deleteable<PUB_ATT_FILE>().Where(S => S.XID == DjInfo.XID).ExecuteCommandAsync();
                    }
                }
                else
                {
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
                        fw.PRE_FLOW_ID,
                        fw.AUZ_ID,
                        fw.CDATE,
                        fw.USER_NAME
                    }).ExecuteCommandAsync();
                    int status = flowInfo.FLOW_ID;
                    await base.Db.Updateable<BankAuthorize>().SetColumns(it => new BankAuthorize() { STATUS = status })
    .Where(it => it.BID == DjInfo.AUZ_ID).ExecuteCommandAsync();
                }

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
                if (uploadFiles != null && uploadFiles.Count > 0)
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

    }
}
