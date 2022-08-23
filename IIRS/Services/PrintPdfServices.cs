using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel.IIRS;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace IIRS.Services
{
    public class PrintPdfServices : BaseServices, IPrintPdfServices
    {
        private readonly ILogger<PrintPdfServices> _logger;
        private readonly IDBTransManagement _dbTransManagement;
        public PrintPdfServices(IDBTransManagement dbTransManagement, ILogger<PrintPdfServices> logger) : base(dbTransManagement)
        {
            _logger = logger;
            _dbTransManagement = dbTransManagement;
        }

        /// <summary>
        /// 获取抵押登记打印信息
        /// </summary>
        /// <param name="slbh"></param>
        /// <returns></returns>
        public async Task<PdfVModel> GetPdfInfo(string slbh)
        {
            base.ChangeDB(SysConst.DB_CON_IIRS);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            PdfVModel model = new PdfVModel();
            PUB_ATT_FILE fileModel = null;
            OrderHouseAssociation houseModel = null;

            var result = await base.Db.Queryable<DY_INFO, QLRGL_INFO, REGISTRATION_INFO, BankAuthorize, IFLOW_ACTION, IFLOW_ACTION_GROUP>((A, B, C, D, E, F) => new object[] { JoinType.Inner, A.SLBH == B.SLBH, JoinType.Inner, C.YWSLBH == A.SLBH, JoinType.Inner, D.BID == C.AUZ_ID, JoinType.Inner, E.FLOW_ID == D.STATUS, JoinType.Inner, F.GROUP_ID == E.GROUP_ID })
.Where((A, B, C, D, E, F) => (A.SLBH == slbh)).GroupBy((A, B, C, D, E, F) => new { SLBH = A.SLBH, SQRQ = A.SQRQ, DYFS = A.DYFS, BDBZZQSE = A.BDBZZQSE, QLQSSJ = A.QLQSSJ, QLJSSJ = A.QLJSSJ, BID = C.AUZ_ID, DYLX = F.GNAME })
.Select((A, B, C, D, E, F) => new { SLBH = A.SLBH, SQRQ = A.SQRQ, DYFS = A.DYFS, BDBZZQSE = A.BDBZZQSE, QLQSSJ = A.QLQSSJ, QLJSSJ = A.QLJSSJ, BID = C.AUZ_ID, DYLX = F.GNAME, 
    DYQR = SqlFunc.MappingColumn(B.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押权人', B.QLRMC,NULL)))"), 
    DYQRZJLB = SqlFunc.MappingColumn(B.ZJLB_ZWM, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押权人', B.ZJLB_ZWM,NULL)))"), 
    DYQRZJHM = SqlFunc.MappingColumn(B.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押权人', B.ZJHM,NULL)))"), 
    DYR = SqlFunc.MappingColumn(B.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押人', B.QLRMC,NULL)))"), 
    DYRZJLB = SqlFunc.MappingColumn(B.ZJLB_ZWM, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押人', B.ZJLB_ZWM,NULL)))"), 
    DYRZJHM = SqlFunc.MappingColumn(B.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押人', B.ZJHM,NULL)))")}).ToListAsync();

            foreach (var item in result)
            {
                model.slbh = item.SLBH;
                model.Strsqrq = item.SQRQ.GetValueOrDefault(new DateTime(1900, 1, 1)).ToLongDateString();
                if (item.DYFS == "1")
                    model.dyfs = "一般抵押";
                else
                    model.dyfs = "最高额抵押";
                model.bdbzzqse = item.BDBZZQSE;
                model.Strqlqssj = item.QLQSSJ.GetValueOrDefault(new DateTime(1900, 1, 1)).ToLongDateString();
                model.Strqljssj = item.QLJSSJ.GetValueOrDefault(new DateTime(1900, 1, 1)).ToLongDateString();
                model.bid = item.BID;
                model.dylx = item.DYLX;
                model.dyqr = item.DYQR;
                model.dyqrzjlb = item.DYQRZJLB;
                model.dyqrzjhm = item.DYQRZJHM;
                model.dyr = item.DYR;
                model.dyrzjlb = item.DYRZJLB;
                model.dyrzjhm = item.DYRZJHM;
            }

            var SjrResult = await base.Db.Queryable<REGISTRATION_INFO, Sys_Userinfo>((A, B) => new Object[] { JoinType.Inner, A.USER_ID == B.Id.ToString() }).Where((A, B) => A.YWSLBH == slbh).Select((A, B) =>
               new
               {
                   SJR = B.RealName
               }).ToListAsync();

            foreach (var itemSjr in SjrResult)
            {
                model.sjr = itemSjr.SJR;
            }

            var HouseList = await base.Db.Queryable<OrderHouseAssociation, TSGL_INFO>((A, B) => new object[] { JoinType.Inner, A.NUMBERID == B.TSTYBM})
.Where((A, B) => (B.SLBH == slbh)).Select((A, B) => new {
    BID = A.BID,
    CERTIFICATENUMBER = A.CERTIFICATENUMBER,
    ACCEPTANCENUMBER = A.ACCEPTANCENUMBER,
    ADDRESS = A.ADDRESS,
    QLLX = A.qllx,
    QLXZ = A.qlxz,
    JZMJ = A.jzmj,
    BDCDYH = B.BDCDYH}).ToListAsync();

            foreach (var item in HouseList)
            {
                houseModel = new OrderHouseAssociation();
                houseModel.BID = item.BID;
                houseModel.CERTIFICATENUMBER = item.CERTIFICATENUMBER;
                houseModel.ACCEPTANCENUMBER = item.ACCEPTANCENUMBER;
                houseModel.ADDRESS = item.ADDRESS;
                houseModel.qllx = item.QLLX;
                houseModel.qlxz = item.QLXZ;
                houseModel.jzmj = item.JZMJ;
                houseModel.bdcdyh = item.BDCDYH;
                model.houseList.Add(houseModel);
            }

            var fileData = await base.Db.Queryable<PUB_ATT_FILE>().Where(file => (file.BUS_PK == slbh)).GroupBy((file) => new { group_name = file.GROUP_NAME }).Select((file => new { group_name = file.GROUP_NAME, A = SqlFunc.AggregateCount(file.GROUP_NAME) })).ToListAsync();

            for (int i = 0; i < fileData.Count; i++)
            {
                fileModel = new PUB_ATT_FILE();
                fileModel.GROUP_NAME = fileData[i].group_name;
                fileModel.a = fileData[i].A;
                fileModel.xh = i+1;
                model.fileList.Add(fileModel);
            }
            return model;

        }

        /// <summary>
        /// 获取抵押注销打印信息
        /// </summary>
        /// <param name="DySlbh"></param>
        /// <param name="NewSlbh"></param>
        /// <returns></returns>
        public async Task<PdfVModel> GetMrgeReleasePdfInfo(string DySlbh, string NewSlbh)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            PdfVModel model = new PdfVModel();
            PUB_ATT_FILE fileModel = null;
            OrderHouseAssociation houseModel = null;


            base.ChangeDB(SysConst.DB_CON_BDC);
            var result = await base.Db.Queryable<DJ_DY, DJ_QLRGL, SYS_DIC>((A, B, C) => new object[] { JoinType.Inner, A.SLBH == B.SLBH, JoinType.Inner, C.DEFINED_CODE == B.ZJLB }).Where((A, B, C) => A.SLBH == DySlbh && C.GID == 1).GroupBy((A, B) => new { SLBH = A.SLBH, SQRQ = A.SQRQ, DYFS = A.DYFS, BDBZZQSE = A.BDBZZQSE, QLQSSJ = A.QLQSSJ, QLJSSJ = A.QLJSSJ }).Select((A, B, C) => new
            {
                SLBH = A.SLBH,
                SQRQ = A.SQRQ,
                DYFS = A.DYFS,
                BDBZZQSE = A.BDBZZQSE,
                QLQSSJ = A.QLQSSJ,
                QLJSSJ = A.QLJSSJ,
                DYQR = SqlFunc.MappingColumn(B.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押权人', B.QLRMC,NULL)))"),
                DYQRZJLB = SqlFunc.MappingColumn(B.ZJLB, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押权人', B.ZJLB,NULL)))"),
                DYQRZJLB_ZWM = SqlFunc.MappingColumn(C.DNAME, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押权人', C.DNAME,NULL)))"),
                DYQRZJHM = SqlFunc.MappingColumn(B.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押权人', B.ZJHM,NULL)))"),
                DYR = SqlFunc.MappingColumn(B.QLRMC, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押人', B.QLRMC,NULL)))"),
                DYRZJLB = SqlFunc.MappingColumn(B.ZJLB, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押人', B.ZJLB,NULL)))"),
                DYRZJLB_ZWM = SqlFunc.MappingColumn(C.DNAME, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押人', C.DNAME,NULL)))"),
                DYRZJHM = SqlFunc.MappingColumn(B.ZJHM, "WM_CONCAT(DISTINCT TO_CHAR( DECODE(B.QLRLX , '抵押人', B.ZJHM,NULL)))")
            }).ToListAsync();

            foreach (var item in result)
            {
                model.slbh = item.SLBH;
                model.Strsqrq = item.SQRQ.GetValueOrDefault(new DateTime(1900, 1, 1)).ToLongDateString();
                if (item.DYFS == "1")
                    model.dyfs = "一般抵押";
                else
                    model.dyfs = "最高额抵押";
                model.bdbzzqse = item.BDBZZQSE;
                model.Strqlqssj = item.QLQSSJ.GetValueOrDefault(new DateTime(1900, 1, 1)).ToLongDateString();
                model.Strqljssj = item.QLJSSJ.GetValueOrDefault(new DateTime(1900, 1, 1)).ToLongDateString();
                model.dyqr = item.DYQR;
                model.dyqrzjlb = item.DYQRZJLB_ZWM;
                model.dyqrzjhm = item.DYQRZJHM;
                model.dyr = item.DYR;
                model.dyrzjlb = item.DYRZJLB_ZWM;
                model.dyrzjhm = item.DYRZJHM;
            }
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var SjrResult = await base.Db.Queryable<REGISTRATION_INFO, Sys_Userinfo>((A, B) => new Object[] { JoinType.Inner, A.USER_ID == B.Id.ToString() }).Where((A, B) => A.YWSLBH == NewSlbh).Select((A, B) =>
               new
               {
                   SJR = B.RealName
               }).ToListAsync();

            foreach (var itemSjr in SjrResult)
            {
                model.sjr = itemSjr.SJR;
            }

            var HouseList = await base.Db.Queryable<OrderHouseAssociation, TSGL_INFO>((A, B) => new object[] { JoinType.Inner, A.NUMBERID == B.TSTYBM }).Where((A, B) => (B.SLBH == NewSlbh)).Select((A, B) => new {
                        BID = A.BID,
                        CERTIFICATENUMBER = A.CERTIFICATENUMBER,
                        ACCEPTANCENUMBER = A.ACCEPTANCENUMBER,
                        ADDRESS = A.ADDRESS,
                        QLLX = A.qllx,
                        QLXZ = A.qlxz,
                        JZMJ = A.jzmj,
                        BDCDYH = B.BDCDYH
                    }).ToListAsync();

            foreach (var item in HouseList)
            {
                houseModel = new OrderHouseAssociation();
                houseModel.BID = item.BID;
                houseModel.CERTIFICATENUMBER = item.CERTIFICATENUMBER;
                houseModel.ACCEPTANCENUMBER = item.ACCEPTANCENUMBER;
                houseModel.ADDRESS = item.ADDRESS;
                houseModel.qllx = item.QLLX;
                houseModel.qlxz = item.QLXZ;
                houseModel.jzmj = item.JZMJ;
                houseModel.bdcdyh = item.BDCDYH;
                model.houseList.Add(houseModel);
            }

            var fileData = await base.Db.Queryable<PUB_ATT_FILE>().Where(file => (file.BUS_PK == NewSlbh)).GroupBy((file) => new { group_name = file.GROUP_NAME }).Select((file => new { group_name = file.GROUP_NAME, A = SqlFunc.AggregateCount(file.GROUP_NAME) })).ToListAsync();

            for (int i = 0; i < fileData.Count; i++)
            {
                fileModel = new PUB_ATT_FILE();
                fileModel.GROUP_NAME = fileData[i].group_name;
                fileModel.a = fileData[i].A;
                fileModel.xh = i + 1;
                model.fileList.Add(fileModel);
            }
            return model;
        }

        public async Task<List<OrderHouseAssociation>> GetPrintCheckList(string slbh)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            base.ChangeDB(SysConst.DB_CON_IIRS);

            Expression<Func<OrderHouseAssociation, REGISTRATION_INFO, object[]>> _joinExpression = (a, b) => new object[]
                 { JoinType.Inner, a.BID == b.AUZ_ID};

            Expression<Func<OrderHouseAssociation, REGISTRATION_INFO, OrderHouseAssociation>> _selectExpression = (a, b) => new OrderHouseAssociation() { CERTIFICATENUMBER = a.CERTIFICATENUMBER, ACCEPTANCENUMBER = a.ACCEPTANCENUMBER ,ADDRESS = a.ADDRESS,rightname = a.rightname,qllx = a.qllx,qlxz = a.qlxz,jzmj = a.jzmj,bdclx = a.bdclx,bdcdyh = a.bdcdyh};

            Expression<Func<OrderHouseAssociation, REGISTRATION_INFO, bool>> _whereExpression =
            (a, b) => b.YWSLBH == slbh;

            return await base.Query<OrderHouseAssociation, REGISTRATION_INFO, OrderHouseAssociation>(_joinExpression, _selectExpression, _whereExpression);
        }

        public async Task<List<OrderHouseAssociation>> GetPrintCheckListCount(string slbh)
        {
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            base.ChangeDB(SysConst.DB_CON_IIRS);
            
            Expression<Func<OrderHouseAssociation, REGISTRATION_INFO, object[]>> _joinExpression = (a, b) => new object[]
                 { JoinType.Inner, a.BID == b.AUZ_ID};

            Expression<Func<OrderHouseAssociation, REGISTRATION_INFO, OrderHouseAssociation>> _selectExpression = (a, b) => new OrderHouseAssociation() { CERTIFICATENUMBER = a.CERTIFICATENUMBER, ACCEPTANCENUMBER = a.ACCEPTANCENUMBER };

            Expression<Func<OrderHouseAssociation, REGISTRATION_INFO, bool>> _whereExpression =
            (a, b) => b.YWSLBH == slbh;

            return await base.Query<OrderHouseAssociation, REGISTRATION_INFO, OrderHouseAssociation>(_joinExpression, _selectExpression, _whereExpression);
        }
    }
}
