using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IRepository.BDC;
using IIRS.IRepository.IIRS;
using IIRS.IServices.BDC;
using IIRS.Models.EntityModel.BDC;
using IIRS.Models.EntityModel.IIRS;
using IIRS.Models.ViewModel;
using IIRS.Models.ViewModel.BDC;
using IIRS.Services.Base;
using IIRS.Utilities.Common;
using Microsoft.Extensions.Logging;
using RT.Comb;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;
using System.Threading.Tasks;

namespace IIRS.Services.BDC
{
    public class NoticeRegistrationServices : BaseServices, INoticeRegistrationServices
    {
        private readonly ILogger<NoticeRegistrationServices> _logger;
        private readonly IDBTransManagement _dBTransManagement;


        private readonly ISysDicRepository _SysDicRepository;
        private readonly ITsgl_infoRepository _tsglRepository;
        private readonly IDJ_QLRGLRepository _dJ_QLRGLRepository;

        private readonly IQlrgl_infoRepository _qlrgl_InfoRepository;
        private readonly IQlxgRepository _qlxgRepository;
        private readonly ISjdInfoRepository _sjdInfoRepository;
        private readonly IPubAttFileRepository _fileRepository;
        private readonly IYgInfoRepository _ygInfoRepository;
        private readonly ISpbInfoRepository _spbInfoRepository;

        private readonly ISysDataRecorderRepository _sysDataRecorderRepository;
        private readonly IDO_ACTIONRepository _dO_ACTIONRepository;
        private readonly IRegistration_infoRepository _registration_InfoRepository;
        private readonly IBankAuthorizeRepository _bankAuthorizeRepository;

        public NoticeRegistrationServices(IDBTransManagement dbTransManagement, ILogger<NoticeRegistrationServices> logger, ITsgl_infoRepository tsglRepository, ISysDicRepository SysDicRepository, IQlrgl_infoRepository qlrgl_InfoRepository, IRegistration_infoRepository registration_InfoRepository,ISpbInfoRepository spbInfoRepository, IDO_ACTIONRepository dO_ACTIONRepository, ISjdInfoRepository sjdInfoRepository, IYgInfoRepository ygInfoRepository, ISysDataRecorderRepository sysDataRecorderRepository, IBankAuthorizeRepository bankAuthorizeRepository, IPubAttFileRepository fileRepository, IQlxgRepository qlxgRepository) : base(dbTransManagement)
        {
            _logger = logger;
            _dBTransManagement = dbTransManagement;
            _tsglRepository = tsglRepository;
            _SysDicRepository = SysDicRepository;
            _qlrgl_InfoRepository = qlrgl_InfoRepository;
            _registration_InfoRepository = registration_InfoRepository;
            _dO_ACTIONRepository = dO_ACTIONRepository;
            _sjdInfoRepository = sjdInfoRepository;
            _sysDataRecorderRepository = sysDataRecorderRepository;
            _bankAuthorizeRepository = bankAuthorizeRepository;
            _fileRepository = fileRepository;
            _qlxgRepository = qlxgRepository;
            _ygInfoRepository = ygInfoRepository;
            _spbInfoRepository = spbInfoRepository;
        }
        /// <summary>
        /// 预告登记入库
        /// </summary>
        /// <param name="StrInsertModel"></param>
        /// <param name="fileList"></param>
        /// <returns></returns>
        public async Task<string> InsertNoticePost(NoticeRegistrationVModel StrInsertModel, List<PUB_ATT_FILE> fileList)
        {
            string MsgInfo = "";
            string qlrmc = "";
            int isActonOK = 100;
            bool isSubmitWorkFlow = StrInsertModel.commandtype == 1;
            bool isInsert = string.IsNullOrEmpty(StrInsertModel.xid);
            #region 实例化实体类
            REGISTRATION_INFO registration = new REGISTRATION_INFO();
            SysDataRecorderModel RecorderModel = new SysDataRecorderModel();
            SJD_INFO sjdModel = new SJD_INFO();
            SPB_INFO SpbModel = new SPB_INFO();
            TSGL_INFO TsglModel = new TSGL_INFO();
            QL_XG_INFO QlModel = new QL_XG_INFO();
            BankAuthorize bankModel = new BankAuthorize();
            IFLOW_DO_ACTION do_actionModel = new IFLOW_DO_ACTION();
            QLRGL_INFO qlrModel = null;
            QLRGL_INFO ywrModel = null;
            YG_INFO ygModel = new YG_INFO();
            #endregion

            DateTime sjsj = DateTime.Now;
            if (!string.IsNullOrEmpty(StrInsertModel.sjsj))
            {
                sjsj = Convert.ToDateTime(StrInsertModel.sjsj);
            }
            #region 为实体类赋值
            string xid = Provider.Sql.Create().ToString();//主键
            #region JOSN序列化
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
            };
            var jsonstring = JsonSerializer.Serialize(StrInsertModel, options);
            #endregion
            #region BankAuthorize赋值
            bankModel.BID = Provider.Sql.Create().ToString();
            bankModel.AUTHORIZATIONDATE = sjsj;
            bankModel.STATUS = SysBdcFlowConst.FLOW_YGDJ_BANK; ;
            #endregion
            #region 预告表赋值
            ygModel.SLBH = StrInsertModel.slbh;
            ygModel.DJLX = "预告登记";
            ygModel.DJYY = "预告";
            ygModel.BDCDYH = StrInsertModel.bdcdyh;
            ygModel.SQRQ = sjsj;
            ygModel.SPDW = "辽阳市不动产登记局";
            ygModel.GYFS = StrInsertModel.gyfs;
            ygModel.SSJC = "辽";
            ygModel.JGJC = "辽阳市";
            ygModel.FZJG = "辽阳市不动产登记中心";
            ygModel.QT = StrInsertModel.qlqtqk;
            ygModel.XID = xid;
            #endregion
            #region 收件单赋值                    
            sjdModel.SLBH = StrInsertModel.slbh;
            sjdModel.DJDL = "700";
            sjdModel.DJXL = "预购商品房";
            sjdModel.SJSJ = sjsj;
            sjdModel.CNSJ = Convert.ToDateTime(StrInsertModel.cnsj);   //浩楠传过来“承诺时间”
            sjdModel.LCMC = "预告登记";
            sjdModel.LCLX = "国有建设用地使用权及房屋所有权登记";
            sjdModel.ZRKS = "辽阳市不动产登记中心";
            //sjdModel.YXJ = ""; //浩楠传过来“优先级” -- 普通
            sjdModel.HTBH = StrInsertModel.jyhtbh;//浩楠传过来“合同编号”
            //sjdModel.CGZT = ""; //成果状态怎么写？
            sjdModel.ZL = StrInsertModel.zl;
            sjdModel.SJR = StrInsertModel.slry;
            sjdModel.QXDM = StrInsertModel.szqy.Trim();
            sjdModel.TZRXM = StrInsertModel.tzrxm;
            sjdModel.TZRDH = StrInsertModel.tzrdh;
            sjdModel.JHXTID = StrInsertModel.pdh;
            sjdModel.PRJID = StrInsertModel.slbh;
            sjdModel.XID = xid;
            #endregion
            #region 审批表赋值
            SpbModel.SPBH = Provider.Sql.Create().ToString();
            SpbModel.SLBH = StrInsertModel.slbh;
            SpbModel.SPDX = "初审意见";
            SpbModel.SPYJ = StrInsertModel.csyj;
            SpbModel.SPR = StrInsertModel.csr;
            if (!string.IsNullOrEmpty(StrInsertModel.csrq))
            {
                SpbModel.SPRQ = Convert.ToDateTime(StrInsertModel.csrq);
            }
            SpbModel.SPTXR = StrInsertModel.csrq;  //  需要填写登陆人的用户名即admin
            SpbModel.XID = xid;
            #endregion
            #region 图属关联赋值
            TsglModel.GLBM = Provider.Sql.Create().ToString();
            TsglModel.TSTYBM = StrInsertModel.tstybm;
            TsglModel.SLBH = StrInsertModel.slbh;
            TsglModel.BDCLX = "房屋";
            TsglModel.BDCDYH = StrInsertModel.bdcdyh;
            TsglModel.DJZL = "预告";
            TsglModel.CSSJ = sjsj;
            TsglModel.XID = xid;
            TsglModel.LIFECYCLE = -1;
            #endregion
            #region 新权利相关表QL_XG_INFO
            QlModel.XTBH = Provider.Sql.Create().ToString();
            QlModel.SLBH = StrInsertModel.slbh;
            QlModel.FW_QLLX = StrInsertModel.fwqllx;
            QlModel.FW_QLLX_ZWM = StrInsertModel.fwqllx_zwm;
            QlModel.FW_QLXZ = StrInsertModel.fwqlxz;
            QlModel.FW_QLXZ_ZWMS = StrInsertModel.fwqlxz_zwm;
            QlModel.FW_JZMJ = StrInsertModel.jzmj;
            QlModel.FW_TNJZMJ = StrInsertModel.tnjzmj;
            QlModel.FW_FWGHYT = StrInsertModel.fwghyt;
            QlModel.FW_FWGHYT_ZWM = StrInsertModel.fwghyt_zwm;
            QlModel.TD_QLLX = StrInsertModel.tdqllx;
            QlModel.TD_QLLX_ZWM = StrInsertModel.tdqllx_zwm;
            QlModel.TD_QLXZ = StrInsertModel.tdqlxz;
            QlModel.TD_QLXZ_ZWMS = StrInsertModel.tdqlxz_zwm;
            QlModel.TD_DYTDMJ = StrInsertModel.zdmj;
            QlModel.TD_JZZDMJ = Convert.ToDecimal(StrInsertModel.fzmj);
            QlModel.TD_SYQX = StrInsertModel.syqx;
            QlModel.TD_QSRQ = StrInsertModel.qsrq;
            QlModel.TD_ZZRQ = StrInsertModel.zzrq;
            QlModel.TD_TDYT = StrInsertModel.tdyt;
            QlModel.TD_TDYT_ZWMS = StrInsertModel.tdyt_zwm;
            QlModel.XID = xid;
            #endregion
            
            #region REGISTRATION_INFO赋值
            if (StrInsertModel.qlrList.Count > 0)
            {
                foreach (var item in StrInsertModel.qlrList)
                {
                    qlrmc += item.QLRMC + ",";
                }
                qlrmc = qlrmc.Substring(0, qlrmc.Length - 1);
            }
            registration.XID = xid;
            registration.SLBH = StrInsertModel.slbh;
            registration.YWSLBH = StrInsertModel.slbh;
            registration.DJZL = 25;
            registration.AUZ_ID = bankModel.BID;
            registration.REMARK2 = "预告登记";
            registration.USER_ID = StrInsertModel.orgId;
            registration.SAVEDATE = sjsj;
            registration.ZL = StrInsertModel.zl;
            registration.QLRMC = qlrmc;
            #endregion
            #region 插入Json                    
            RecorderModel.PK = Provider.Sql.Create().ToString();
            RecorderModel.BUS_PK = xid;
            RecorderModel.USER_ID = StrInsertModel.orgId;
            RecorderModel.USER_NAME = StrInsertModel.slry;
            RecorderModel.SAVEDATAJSON = jsonstring;
            RecorderModel.CDATE = sjsj;
            RecorderModel.IS_STOP = 0;
            #endregion
            #region IFLOW_DO_ACTION赋值
            do_actionModel.PK = Provider.Sql.Create().ToString();
            do_actionModel.FLOW_ID = SysBdcFlowConst.FLOW_YGDJ_BANK;
            do_actionModel.AUZ_ID = bankModel.BID;
            do_actionModel.CDATE = sjsj;
            do_actionModel.USER_NAME = StrInsertModel.slry;
            #endregion
            #region 权利人义务人赋值
            List<QLRGL_INFO> qlrList = new List<QLRGL_INFO>();
            List<QLRGL_INFO> ywrList = new List<QLRGL_INFO>();

            foreach (var item in StrInsertModel.qlrList)
            {
                qlrModel = new QLRGL_INFO();
                qlrModel.GLBM = Provider.Sql.Create().ToString();
                qlrModel.SLBH = StrInsertModel.slbh;
                qlrModel.YWBM = StrInsertModel.slbh;
                qlrModel.QLRID = Provider.Sql.Create().ToString();
                qlrModel.GYFS = StrInsertModel.gyfs;
                //qlrModel.GYFE = item.GYFE;
                qlrModel.QLRLX = "权利人";
                qlrModel.QLRMC = item.QLRMC;
                qlrModel.ZJHM = item.ZJHM;
                qlrModel.ZJLB = item.ZJLB;
                qlrModel.SXH = item.SXH;
                qlrModel.XID = xid;
                qlrModel.ZJLB_ZWM = item.ZJLB_ZWM;
                qlrList.Add(qlrModel);
            }

            foreach (var item in StrInsertModel.ywrList)
            {
                ywrModel = new QLRGL_INFO();
                ywrModel.GLBM = Provider.Sql.Create().ToString();
                ywrModel.SLBH = StrInsertModel.slbh;
                ywrModel.YWBM = StrInsertModel.slbh;
                ywrModel.QLRID = Provider.Sql.Create().ToString();
                //ywrModel.GYFS = item.gyfs;
                //ywrModel.GYFE = item.gyfe;
                ywrModel.QLRLX = "义务人";
                ywrModel.QLRMC = item.QLRMC;
                ywrModel.ZJHM = item.ZJHM;
                ywrModel.ZJLB = item.ZJLB;
                ywrModel.SXH = item.SXH;
                ywrModel.XID = xid;
                ywrModel.ZJLB_ZWM = item.ZJLB_ZWM;
                qlrList.Add(ywrModel);
            }
            #endregion

            if (fileList != null && fileList.Count > 0)
            {
                foreach (var file in fileList)
                {
                    file.XID = xid;
                }
            }
            #endregion

            try
            {
                if (isSubmitWorkFlow)    //true时，当前任务为提交当前流程。false时，当前任务为暂存数据
                {
                    registration.IS_ACTION_OK = 1;
                    isActonOK = 1;
                }
                else
                {
                    registration.IS_ACTION_OK = 0;
                    isActonOK = 0;
                }
                if (isInsert)   //当为true时，xid为null,进行insert操作。为false时，存在xid,进行update操作
                {
                    #region
                    _dBTransManagement.BeginTran();
                    var bankCount = _bankAuthorizeRepository.Add(bankModel).Result; //
                    var RegCount = _registration_InfoRepository.Add(registration).Result;
                    var RecorderCount = _sysDataRecorderRepository.Add(RecorderModel).Result;
                    var ActionCount = _dO_ACTIONRepository.Add(do_actionModel).Result;  //
                    var ygCount = _ygInfoRepository.Add(ygModel).Result;    //
                    var spbCount = _spbInfoRepository.Add(SpbModel).Result; //
                    var sjdCount = _sjdInfoRepository.Add(sjdModel).Result;
                    var tsglCount = _tsglRepository.Add(TsglModel).Result;
                    var qlxgCount = _qlxgRepository.Add(QlModel);
                    var qlrCount = _qlrgl_InfoRepository.Add(qlrList).Result;
                    if (fileList != null && fileList.Count > 0)
                    {
                        foreach (var item in fileList)
                        {
                            item.XID = xid;
                        }
                        var fileCount = _fileRepository.Add(fileList).Result;
                        _dBTransManagement.CommitTran();
                        MsgInfo = @$"{{""XID"":""{xid}"",""AUZ_ID"":""{bankModel.BID}"",""JSON_PK"":""{xid}""}}";
                    }
                    else
                    {
                        if (isActonOK == 1)  //提交完成时，附件不能为空
                        {
                            MsgInfo = $@"{{""error"":""请上传附件信息。""}}";
                            _dBTransManagement.RollbackTran();
                        }
                        else
                        {
                            _dBTransManagement.CommitTran();
                            MsgInfo = @$"{{""XID"":""{xid}"",""AUZ_ID"":""{bankModel.BID}"",""JSON_PK"":""{xid}""}}";
                        }
                    }

                    #endregion
                }
                else     //false时，存在xid,进行update操作
                {
                    try
                    {
                        //存在XID的时候，去查询此条XID是否存在，存在的话读取出status和Bid
                        var data = await base.Db.Queryable<REGISTRATION_INFO, BankAuthorize>((A, B) => A.AUZ_ID == B.BID).Where((A, B) => A.XID == StrInsertModel.xid).Select((A, B) => new
                        {
                            bid = B.BID,
                            status = B.STATUS,
                            xid = A.XID,
                            isActionOk = A.IS_ACTION_OK
                        }).ToListAsync();
                        if (data.Count > 0)
                        {
                            if (data[0].status == 203 && data[0].isActionOk == 2)   //当状态为203并且isActionOK=2为预告登记审批退回，否则为加载数据
                            {
                                try
                                {
                                    var OldXid = data[0].xid;
                                    registration.AUZ_ID = data[0].bid;
                                    //string NewXid = Provider.Sql.Create().ToString();//主键
                                    //被退回的登记，需要先将新的Xid更新到REGISTRATION_INFO的Next_xid更新为NewXid,同时更新BankAuthorize的status为106

                                    _dBTransManagement.BeginTran();
                                    #region 执行更新操作
                                    var Regdata = await base.Db.Queryable<REGISTRATION_INFO>().Where(it => it.XID == OldXid).ToListAsync();
                                    if (Regdata.Count > 0)
                                    {
                                        Regdata[0].NEXT_XID = xid;
                                        Regdata[0].IS_ACTION_OK = isActonOK;
                                        registration.AUZ_ID = Regdata[0].AUZ_ID;
                                    }
                                    var updateReg = _registration_InfoRepository.Update(Regdata[0]).Result;

                                    var BankData = await base.Db.Queryable<BankAuthorize>().Where(it => it.BID == registration.AUZ_ID).ToListAsync();
                                    if(BankData.Count > 0)
                                    {
                                        BankData[0].STATUS = 200;
                                    }
                                    var updateBank = _bankAuthorizeRepository.Update(BankData[0]).Result;

                                    var RecorderData = await base.Db.Queryable<SysDataRecorderModel>().Where(it => it.BUS_PK == OldXid).ToListAsync();
                                    if (RecorderData.Count > 0)
                                    {
                                        RecorderData[0].BUS_PK = xid;
                                        RecorderData[0].SAVEDATAJSON = jsonstring;
                                    }
                                    var updateRecorder = _sysDataRecorderRepository.Update(RecorderData[0]).Result;

                                    var SpbData = await base.Db.Queryable<SPB_INFO>().Where(it => it.XID == OldXid).ToListAsync();
                                    if(SpbData.Count > 0)
                                    {
                                        SpbData[0].XID = xid;
                                    }
                                    var updateSpb = _spbInfoRepository.Update(SpbData[0]).Result;


                                    #endregion

                                    #region 执行删除操作
                                    var deleteSjd = base.Db.Deleteable<SJD_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();
                                    var deleteYg = base.Db.Deleteable<YG_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();
                                    var deleteTsgl = base.Db.Deleteable<TSGL_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();
                                    var deleteQlxg = base.Db.Deleteable<QL_XG_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();
                                    var deleteQlrgl = base.Db.Deleteable<QLRGL_INFO>().Where(it => it.XID == OldXid).ExecuteCommand();
                                    if (fileList != null && fileList.Count > 0)
                                    {
                                        foreach (var FileItem in fileList)
                                        {
                                            var deleteFile = base.Db.Deleteable<PUB_ATT_FILE>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                        }
                                    }
                                    #endregion

                                    #region 执行插入操作
                                    var regCount = _registration_InfoRepository.Add(registration).Result;
                                    var sjdCount = _sjdInfoRepository.Add(sjdModel).Result;
                                    var YgCount = _ygInfoRepository.Add(ygModel).Result;
                                    var tsglCount = _tsglRepository.Add(TsglModel).Result;
                                    var ActionCount = _dO_ACTIONRepository.Add(do_actionModel).Result;
                                    var qlxgCount = _qlxgRepository.Add(QlModel).Result;
                                    if (qlrList != null && qlrList.Count > 0)
                                    {
                                        foreach (var QlrItem in qlrList)
                                        {
                                            var qlrCount = _qlrgl_InfoRepository.Add(QlrItem).Result;
                                        }
                                    }
                                    
                                    if (fileList != null && fileList.Count > 0)
                                    {
                                        foreach (var item in fileList)
                                        {
                                            item.XID = xid;
                                        }
                                        var fileCount = _fileRepository.Add(fileList).Result;
                                    }
                                    _dBTransManagement.CommitTran();
                                    MsgInfo = @$"{{""XID"":""{xid}"",""AUZ_ID"":""{bankModel.BID}"",""JSON_PK"":""{xid}""}}";

                                    #endregion
                                    _dBTransManagement.CommitTran();
                                }
                                catch (Exception ex)
                                {
                                    _dBTransManagement.RollbackTran();
                                    throw ex;
                                }
                            }
                            else
                            {
                                var oldXid = data[0].xid;
                                sjdModel.XID = oldXid;
                                SpbModel.XID = oldXid;
                                TsglModel.XID = oldXid;
                                QlModel.XID = oldXid;
                                RecorderModel.BUS_PK = oldXid;
                                registration.IS_ACTION_OK = isActonOK;
                                #region 执行删除操作
                                _dBTransManagement.BeginTran();
                                var deleteQlrgl = base.Db.Deleteable<QLRGL_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteSjd = base.Db.Deleteable<SJD_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteYg = base.Db.Deleteable<YG_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteSpb = base.Db.Deleteable<SPB_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteTsgl = base.Db.Deleteable<TSGL_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteQlxg = base.Db.Deleteable<QL_XG_INFO>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                var deleteJson = base.Db.Deleteable<SysDataRecorderModel>().Where(it => it.BUS_PK == StrInsertModel.xid).ExecuteCommand();

                                if (fileList != null && fileList.Count > 0)
                                {
                                    foreach (var FileItem in fileList)
                                    {
                                        var deleteFile = base.Db.Deleteable<PUB_ATT_FILE>().Where(it => it.XID == StrInsertModel.xid).ExecuteCommand();
                                    }
                                }
                                #endregion

                                #region 执行插入操作
                                if (qlrList.Count > 0)
                                {
                                    foreach (var item in qlrList)
                                    {
                                        item.XID = oldXid;
                                        var insertQlrgl = _qlrgl_InfoRepository.Add(item).Result;
                                    }
                                }

                                var insertYg = _ygInfoRepository.Add(ygModel).Result;
                                var insertSjd = _sjdInfoRepository.Add(sjdModel).Result;
                                var insertSpb = _spbInfoRepository.Add(SpbModel).Result;
                                var insertTsgl = _tsglRepository.Add(TsglModel).Result;
                                var insertQlxg = _qlxgRepository.Add(QlModel).Result;
                                var insertJson = _sysDataRecorderRepository.Add(RecorderModel).Result;
                                if (fileList != null && fileList.Count > 0)
                                {
                                    foreach (var item in fileList)
                                    {
                                        item.XID = oldXid;
                                    }
                                    var fileCount = _fileRepository.Add(fileList).Result;
                                }

                                #endregion

                                #region 执行更新Reg主表状态为提交
                                //var updateReg = _registration_InfoRepository.Update(registration).Result;
                                var count = base.Db.Updateable(registration).UpdateColumns(it => new
                                {
                                    it.IS_ACTION_OK
                                }).Where(S => S.XID == oldXid).ExecuteCommand();
                                _dBTransManagement.CommitTran();
                                #endregion
                                MsgInfo = @$"{{""XID"":""{StrInsertModel.xid}"",""AUZ_ID"":""{data[0].bid}"",""JSON_PK"":""{StrInsertModel.xid}""}}";
                            }
                        }
                        else
                        {
                            MsgInfo = "系统错误，请与管理员联系。";
                        }
                    }
                    catch (Exception ex)
                    {
                        _dBTransManagement.RollbackTran();
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                _dBTransManagement.RollbackTran();
                throw ex;
            }


            return MsgInfo;
        }

        /// <summary>
        /// 获取房屋信息
        /// </summary>
        /// <param name="tstybm">同属统一编码</param>
        /// <param name="zl">坐落</param>
        /// <param name="intPageIndex">当前页标</param>
        /// <param name="PageSize">每页大小</param>
        /// <returns></returns>
        public async Task<PageModel<FC_H_QSDC>> NoticeSelectHouse(string tstybm, string zl, int intPageIndex, int PageSize)
        {
            intPageIndex = 1;
            PageSize = 20;
            RefAsync<int> totalCount = 0;
            string fwQllx_zwm = "";
            //string fwQlxz = "";
            string fwQlxz_zwm = "";
            //string StrfwGhyt = "";
            string fwGhyt_zwm = "";
            //string StrtdQllx = "";
            string tdQllx_zwm = "";
            string fwjg_zwm = "";
            //string StrtdQlxz = "";
            string tdQlxz_zwm = "";
            //string StrtdGhyt = "";
            string tdGhyt_zwm = "";
            //string Strfcz_tdQllx = "";
            string fcz_tdQllx_zwm = "";
            //string Strfcz_tdQlxz = "";
            string fcz_tdQlxz_zwm = "";
            //string Strfcz_tdGhyt = "";
            string fcz_tdGhyt_zwm = "";
            //string Strzd_tdQllx = "";
            string zd_tdQllx_zwm = "";
            //string Strzd_tdQlxz = "";
            string zd_tdQlxz_zwm = "";
            //string Strzd_tdGhyt = "";
            string zd_tdGhyt_zwm = "";
            //string Strzd_sjtdGhyt = "";
            string zd_sjtdGhyt_zwm = "";
            //string Strzd_pztdGhyt = "";
            string zd_pztdGhyt_zwm = "";
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDic = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 1, 3, 4, 5, 6, 7, 8, 9 }).ToListAsync();
            PageModel<FC_H_QSDC> model = new PageModel<FC_H_QSDC>();
            #region 获取房屋信息
            base.ChangeDB(SysConst.DB_CON_BDC);
            //base.Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};
           
            var houseList = await base.Db.Queryable<FC_H_QSDC, ZD_QSDC, FC_Z_QSDC>((A, B, C) => new object[] { JoinType.Left, A.ZDTYBM == B.ZDTYBM, JoinType.Left, A.LSZTYBM == C.TSTYBM })
                .WhereIF(!string.IsNullOrEmpty(tstybm), (A, B, C) => A.TSTYBM.Contains(tstybm))
                .WhereIF(!string.IsNullOrEmpty(zl), (A, B, C) => A.ZL.Contains(zl))
                .Where((A, B, C) => (A.LIFECYCLE == 0 || A.LIFECYCLE == null) && (B.LIFECYCLE == 0 || B.LIFECYCLE == null) && (C.LIFECYCLE == 0 || C.LIFECYCLE == null)&& (A.TSTYBM == tstybm) || (A.ZL == zl))
                .Select((A, B, C) => new FC_H_QSDC()
                {
                FWJG = C.FWJG,
                HZCS = C.ZCS,
                TSTYBM = A.TSTYBM,
                ZDTYBM = A.ZDTYBM,
                ZH = A.ZH,
                HH = A.HH,
                BDCDYH = A.BDCDYH,
                LSZTYBM = A.LSZTYBM,
                LSFWBH = A.LSFWBH,
                QLLX = A.QLLX,
                QLXZ = A.QLXZ,
                GHYT = A.GHYT,
                qllx_zwm = A.QLLX,
                qlxz_zwm = A.QLXZ,
                ghyt_zwm = A.GHYT,
                TDYT = A.TDYT,
                tdyt_zwm = A.TDYT,
                TDQLLX = A.TDQLLX,
                TDQLXZ = A.TDQLXZ,
                TDQLLX_zwm = A.TDQLLX,
                TDQLXZ_zwm = A.TDQLXZ,
                FCZ_ghyt = C.GHYT,
                FCZ_ghyt_zwm = C.GHYT,
                FCZ_TDQLLX = C.TDQLLX,
                FCZ_TDQLLX_zwm = C.TDQLLX,
                FCZ_TDQLXZ = C.TDQLXZ,
                FCZ_TDQLXZ_zwm = C.TDQLXZ,
                ZD_tdyt_zwm = C.TDYT,                
                ZD_qllx = B.QLLX,
                ZD_qllx_zwm = B.QLLX,
                ZD_qlxz = B.QLXZ,
                ZD_qlxz_zwm = B.QLXZ,
                ZD_pztdyt = B.PZTDYT,
                ZD_pztdyt_zwm = B.PZTDYT,
                ZD_sjtdyt = B.SJTDYT,
                ZD_sjtdyt_zwm = B.SJTDYT,
                ZL = A.ZL,
                SJC = A.SJC,
                MYC = A.MYC,
                DYH = A.DYH,
                FJH = A.FJH,
                LJZH = A.LJZH,
                QDJG = A.QDJG,
                QDFS = A.QDFS,
                YCJZMJ = A.YCJZMJ,
                YCTNJZMJ = A.YCTNJZMJ,
                YCFTJZMJ = A.YCFTJZMJ,
                JZMJ = A.JZMJ,
                TNJZMJ = A.TNJZMJ,
                FTJZMJ = A.FTJZMJ,
                TDQSRQ = A.TDQSRQ,
                TDZZRQ = A.TDZZRQ,                
                TDSYQX = A.TDSYQX,
                TDSYQR = A.TDSYQR,
                GYTDMJ = A.GYTDMJ,
                FTTDMJ = A.FTTDMJ,
                DYTDMJ = A.DYTDMJ,
                ZDMJ = A.ZDMJ,
                FZMJ = B.FZMJ,
                zd_TDZZRQ = B.ZZRQ,
                TDSYQMJ = null
                }).ToPageListAsync(intPageIndex, PageSize, totalCount);
            #endregion
            //_logger.LogDebug("开始"+ houseList.Count);
            if (houseList.Count > 0)
            {
                for (int i = 0; i < houseList.Count; i++)
                {
                   
                    #region fc_h_qsdc
                    #region fwqlxz
                    if (!string.IsNullOrEmpty(houseList[i].QLXZ))
                    {
                        string[] QLXZ_zwmArray = houseList[i].QLXZ.Split(",");
                        if (QLXZ_zwmArray.Length > 1)
                        {
                            foreach (var item in QLXZ_zwmArray)
                            {
                                var fwqlxzList = sysDic.Where(s => s.GID == 3 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if(fwqlxzList != null)
                                {
                                    fwQlxz_zwm += fwqlxzList.DNAME + ",";
                                }
                            }
                            fwQlxz_zwm = fwQlxz_zwm.Substring(0, fwQlxz_zwm.Length - 1);
                            houseList[i].qlxz_zwm = fwQlxz_zwm;
                        }
                        else
                        {
                            var fwqlxz = sysDic.Where(s => s.GID == 3 && s.DEFINED_CODE == houseList[i].QLXZ).FirstOrDefault();
                            houseList[i].qlxz_zwm = fwqlxz != null ? fwqlxz.DNAME : string.Empty;
                            fwQlxz_zwm = houseList[i].qlxz_zwm;
                        }
                    }
                    #endregion
                    #region fwjg
                    if (!string.IsNullOrEmpty(houseList[i].FWJG))
                    {
                        string[] FWJG_zwmArray = houseList[i].FWJG.Split(",");
                        if (FWJG_zwmArray.Length > 1)
                        {
                            foreach (var item in FWJG_zwmArray)
                            {
                                var fwjgList = sysDic.Where(s => s.GID == 9 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if (fwjgList != null)
                                {
                                    fwjg_zwm += fwjgList.DNAME + ",";
                                }
                            }
                            fwjg_zwm = fwjg_zwm.Substring(0, fwjg_zwm.Length - 1);
                            houseList[i].fwjg_zwm = fwjg_zwm;
                        }
                        else
                        {
                            var fwjg_zwm2 = sysDic.Where(s => s.GID == 9 && s.DEFINED_CODE == houseList[i].FWJG).FirstOrDefault();
                            houseList[i].fwjg_zwm = fwjg_zwm2 != null ? fwjg_zwm2.DNAME : string.Empty;
                            fwjg_zwm = houseList[i].fwjg_zwm;
                        }
                    }
                    #endregion
                    #region fwqllx
                    if (!string.IsNullOrEmpty(houseList[i].QLLX))
                    {
                        string[] QLLX_zwmArray = houseList[i].QLLX.Split(",");
                        if (QLLX_zwmArray.Length > 1)
                        {
                            foreach (var item in QLLX_zwmArray)
                            {
                                var fwqllxList = sysDic.Where(s => s.GID == 4 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if (fwqllxList != null)
                                    fwQllx_zwm += fwqllxList.DNAME + ",";
                            }
                            fwQllx_zwm = fwQllx_zwm.Substring(0, fwQllx_zwm.Length - 1);
                            houseList[i].qllx_zwm = fwQllx_zwm;
                        }
                        else
                        {
                            var fwqllx = sysDic.Where(s => s.GID == 4 && s.DEFINED_CODE == houseList[i].QLLX).FirstOrDefault();
                            houseList[i].qllx_zwm = fwqllx != null ? fwqllx.DNAME : string.Empty;
                            fwQllx_zwm = houseList[i].qllx_zwm;
                        }
                    }
                    #endregion

                    #region fwghyt
                    if (!string.IsNullOrEmpty(houseList[i].GHYT))
                    {
                        string[] GHYT_zwmArray = houseList[i].GHYT.Split(",");
                        if (GHYT_zwmArray.Length > 1)
                        {
                            foreach (var item in GHYT_zwmArray)
                            {
                                var fwghytList = sysDic.Where(s => s.GID == 5 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if (fwghytList != null)
                                    fwGhyt_zwm += fwghytList.DNAME + ",";
                            }
                            fwGhyt_zwm = fwGhyt_zwm.Substring(0, fwGhyt_zwm.Length - 1);
                            houseList[i].ghyt_zwm = fwGhyt_zwm;
                        }
                        else
                        {
                            var fwghyt = sysDic.Where(s => s.GID == 5 && s.DEFINED_CODE == houseList[i].GHYT).FirstOrDefault();
                            houseList[i].ghyt_zwm = fwghyt != null ? fwghyt.DNAME : string.Empty;
                            fwGhyt_zwm = houseList[i].ghyt_zwm;
                        }
                    }
                    #endregion

                    #region tdqllx
                    if (!string.IsNullOrEmpty(houseList[i].TDQLLX))
                    {
                        string[] tdqllx_zwmArray = houseList[i].TDQLLX.Split(",");
                        if (tdqllx_zwmArray.Length > 1)
                        {
                            foreach (var item in tdqllx_zwmArray)
                            {
                                var tdqllxList = sysDic.Where(s => s.GID == 6 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if (tdqllxList != null)
                                {
                                    tdQllx_zwm += tdqllxList.DNAME + ",";
                                }
                                    
                            }
                            tdQllx_zwm = tdQllx_zwm.Substring(0, tdQllx_zwm.Length - 1);
                            houseList[i].TDQLLX_zwm = tdQllx_zwm;
                        }
                        else
                        {
                            var tdQllx = sysDic.Where(s => s.GID == 6 && s.DEFINED_CODE == houseList[i].TDQLLX).FirstOrDefault();
                            houseList[i].TDQLLX_zwm = tdQllx != null ? tdQllx.DNAME : string.Empty;
                            tdQllx_zwm = houseList[i].TDQLLX_zwm;
                        }
                    }
                    #endregion

                    #region tdqlxz
                    if (!string.IsNullOrEmpty(houseList[i].TDQLXZ))
                    {

                        string[] tdqlxz_zwmArray = houseList[i].TDQLXZ.Split(",");
                        if (tdqlxz_zwmArray.Length > 1)
                        {
                            foreach (var item in tdqlxz_zwmArray)
                            {
                                var tdqlxzList = sysDic.Where(s => s.GID == 7 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if (tdqlxzList != null)
                                    tdQlxz_zwm += tdqlxzList.DNAME + ",";
                            }
                            tdQlxz_zwm = tdQlxz_zwm.Substring(0, tdQlxz_zwm.Length - 1);
                            houseList[i].TDQLXZ_zwm = tdQlxz_zwm;
                        }
                        else
                        {
                            var tdQlxz = sysDic.Where(s => s.GID == 7 && s.DEFINED_CODE == houseList[i].TDQLXZ).FirstOrDefault();
                            houseList[i].TDQLXZ_zwm = tdQlxz != null ? tdQlxz.DNAME : string.Empty;
                            tdQlxz_zwm = houseList[i].TDQLXZ_zwm;
                        }
                    }
                    #endregion

                    #region tdghyt
                    if (!string.IsNullOrEmpty(houseList[i].TDYT))
                    {
                        string[] tdyt_zwmArray = houseList[i].TDYT.Split(",");
                        if (tdyt_zwmArray.Length > 1)
                        {
                            foreach (var item in tdyt_zwmArray)
                            {
                                var tdytList = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if (tdytList != null)
                                    tdGhyt_zwm += tdytList.DNAME + ",";
                            }
                            tdGhyt_zwm = tdGhyt_zwm.Substring(0, tdGhyt_zwm.Length - 1);
                            houseList[i].tdyt_zwm = tdGhyt_zwm;
                        }
                        else
                        {
                            var tdghyt = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == houseList[i].TDYT).FirstOrDefault();
                            houseList[i].tdyt_zwm = tdghyt != null ? tdghyt.DNAME : string.Empty;
                            tdGhyt_zwm = houseList[i].tdyt_zwm;
                        }
                    }
                    #endregion
                    #endregion
                 
                    #region fc_z_qsdc
                    #region tdqllx
                    if (!string.IsNullOrEmpty(houseList[i].FCZ_TDQLLX))
                    {
                        if (string.IsNullOrEmpty(houseList[i].TDQLLX))
                        {
                            houseList[i].TDQLLX = houseList[i].FCZ_TDQLLX;
                        }
                        string[] fcz_tdqllx_zwmArray = houseList[i].FCZ_TDQLLX.Split(",");
                        if (fcz_tdqllx_zwmArray.Length > 1)
                        {
                            foreach (var item in fcz_tdqllx_zwmArray)
                            {
                                var fcz_tdqllxList = sysDic.Where(s => s.GID == 6 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if (fcz_tdqllxList != null)
                                    fcz_tdQllx_zwm += fcz_tdqllxList.DNAME + ",";
                            }
                            fcz_tdQllx_zwm = fcz_tdQllx_zwm.Substring(0, fcz_tdQllx_zwm.Length - 1);
                            houseList[i].FCZ_TDQLLX_zwm = fcz_tdQllx_zwm;
                        }
                        else
                        {
                            var fcz_tdQllx = sysDic.Where(s => s.GID == 6 && s.DEFINED_CODE == houseList[i].FCZ_TDQLLX).FirstOrDefault();
                            houseList[i].FCZ_TDQLLX_zwm = fcz_tdQllx != null ? fcz_tdQllx.DNAME : string.Empty;
                            fcz_tdQllx_zwm = houseList[i].FCZ_TDQLLX_zwm;
                        }
                    }
                    #endregion

                    #region tdqlxz
                    if (!string.IsNullOrEmpty(houseList[i].FCZ_TDQLXZ))
                    {
                        if (string.IsNullOrEmpty(houseList[i].TDQLXZ))
                        {
                            houseList[i].TDQLXZ = houseList[i].FCZ_TDQLXZ;
                        }
                        string[] fcz_tdqlxz_zwmArray = houseList[i].FCZ_TDQLXZ.Split(",");
                        if (fcz_tdqlxz_zwmArray.Length > 1)
                        {
                            foreach (var item in fcz_tdqlxz_zwmArray)
                            {
                                var fcz_tdqlxzList = sysDic.Where(s => s.GID == 7 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if (fcz_tdqlxzList != null)
                                    fcz_tdQlxz_zwm += fcz_tdqlxzList.DNAME + ",";
                            }
                            fcz_tdQlxz_zwm = fcz_tdQlxz_zwm.Substring(0, fcz_tdQlxz_zwm.Length - 1);
                            houseList[i].FCZ_TDQLXZ_zwm = fcz_tdQlxz_zwm;
                        }
                        else
                        {
                            var fcz_tdQlxz = sysDic.Where(s => s.GID == 5 && s.DEFINED_CODE == houseList[i].FCZ_TDQLXZ).FirstOrDefault();
                            houseList[i].FCZ_TDQLXZ_zwm = fcz_tdQlxz != null ? fcz_tdQlxz.DNAME : string.Empty;
                            fcz_tdQlxz_zwm = houseList[i].FCZ_TDQLXZ_zwm;
                        }
                    }
                    #endregion

                    #region tdghyt
                    if (!string.IsNullOrEmpty(houseList[i].FCZ_ghyt))
                    {
                        if (string.IsNullOrEmpty(houseList[i].TDYT))
                        {
                            houseList[i].TDYT = houseList[i].FCZ_ghyt;
                        }
                        string[] fcz_tdyt_zwmArray = houseList[i].FCZ_ghyt.Split(",");
                        if (fcz_tdyt_zwmArray.Length > 1)
                        {
                            foreach (var item in fcz_tdyt_zwmArray)
                            {
                                var fcz_tdytList = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if (fcz_tdytList != null)
                                    fcz_tdGhyt_zwm += fcz_tdytList.DNAME + ",";
                            }
                            fcz_tdGhyt_zwm = fcz_tdGhyt_zwm.Substring(0, fcz_tdGhyt_zwm.Length - 1);
                            houseList[i].FCZ_ghyt_zwm = fcz_tdGhyt_zwm;
                        }
                        else
                        {
                            var fcz_tdghyt = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == houseList[i].FCZ_ghyt).FirstOrDefault();
                            houseList[i].FCZ_ghyt_zwm = fcz_tdghyt != null ? fcz_tdghyt.DNAME : string.Empty;
                            fcz_tdGhyt_zwm = houseList[i].FCZ_ghyt_zwm;
                        }
                    }
                    #endregion
                    #endregion
                  
                    #region zd_qsdc
                    #region tdqllx
                    if (!string.IsNullOrEmpty(houseList[i].ZD_qllx))
                    {
                        if (string.IsNullOrEmpty(houseList[i].TDQLLX))
                        {
                            houseList[i].TDQLLX = houseList[i].ZD_qllx;
                        }
                        string[] zd_qllx_zwmArray = houseList[i].ZD_qllx.Split(",");
                        if (zd_qllx_zwmArray.Length > 1)
                        {
                            foreach (var item in zd_qllx_zwmArray)
                            {
                                var zd_qllxList = sysDic.Where(s => s.GID == 6 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if (zd_qllxList != null)
                                    zd_tdQllx_zwm += zd_qllxList.DNAME + ",";
                            }
                            zd_tdQllx_zwm = zd_tdQllx_zwm.Substring(0, zd_tdQllx_zwm.Length - 1);
                            houseList[i].ZD_qllx_zwm = zd_tdQllx_zwm;                            
                        }
                        else
                        {
                            var zd_Qllx = sysDic.Where(s => s.GID == 6 && s.DEFINED_CODE == houseList[i].ZD_qllx).FirstOrDefault();
                            houseList[i].ZD_qllx_zwm = zd_Qllx != null ? zd_Qllx.DNAME : string.Empty;
                            zd_tdQllx_zwm = houseList[i].ZD_qllx_zwm;
                        }
                    }
                    #endregion

                    #region tdqlxz
                    if (!string.IsNullOrEmpty(houseList[i].ZD_qlxz))
                    {
                        if (string.IsNullOrEmpty(houseList[i].TDQLXZ))
                        {
                            houseList[i].TDQLXZ = houseList[i].ZD_qlxz;
                        }
                        string[] zd_tdqlxz_zwmArray = houseList[i].ZD_qlxz.Split(",");
                        if (zd_tdqlxz_zwmArray.Length > 1)
                        {
                            foreach (var item in zd_tdqlxz_zwmArray)
                            {
                                var zd_tdqlxzList = sysDic.Where(s => s.GID == 7 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if (zd_tdqlxzList != null)
                                    zd_tdQlxz_zwm += zd_tdqlxzList.DNAME + ",";
                            }
                            zd_tdQlxz_zwm = zd_tdQlxz_zwm.Substring(0, zd_tdQlxz_zwm.Length - 1);
                            houseList[i].ZD_qlxz_zwm = zd_tdQlxz_zwm;
                        }
                        else
                        {
                            var zd_tdQlxz = sysDic.Where(s => s.GID == 5 && s.DEFINED_CODE == houseList[i].ZD_qlxz).FirstOrDefault();
                            houseList[i].ZD_qlxz_zwm = zd_tdQlxz != null ? zd_tdQlxz.DNAME : string.Empty;
                            zd_tdQlxz_zwm = houseList[i].ZD_qlxz_zwm;
                        }
                    }
                    #endregion

                    #region tdghyt
                    if (!string.IsNullOrEmpty(houseList[i].ZD_TDYT))
                    {
                        if (string.IsNullOrEmpty(houseList[i].TDYT))
                        {
                            houseList[i].TDYT = houseList[i].ZD_TDYT;
                        }
                        string[] zd_tdyt_zwmArray = houseList[i].ZD_TDYT.Split(",");
                        if (zd_tdyt_zwmArray.Length > 1)
                        {
                            foreach (var item in zd_tdyt_zwmArray)
                            {
                                var zd_tdytList = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == item.ToString()).FirstOrDefault();
                                if (zd_tdytList != null)
                                    zd_tdGhyt_zwm += zd_tdytList.DNAME + ",";
                            }
                            zd_tdGhyt_zwm = zd_tdGhyt_zwm.Substring(0, zd_tdGhyt_zwm.Length - 1);
                            houseList[i].ZD_tdyt_zwm = zd_tdGhyt_zwm;
                        }
                        else
                        {
                            var zd_tdghyt = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == houseList[i].ZD_TDYT).FirstOrDefault();
                            houseList[i].ZD_tdyt_zwm = zd_tdghyt != null ? zd_tdghyt.DNAME : string.Empty;
                            zd_tdGhyt_zwm = houseList[i].ZD_tdyt_zwm;
                        }
                    }
                    #endregion
                    #region sjtdyt
                    if (!string.IsNullOrEmpty(houseList[i].ZD_sjtdyt))
                    {
                        if (string.IsNullOrEmpty(houseList[i].TDYT))
                        {
                            houseList[i].TDYT = houseList[i].ZD_sjtdyt;
                        }
                        string[] zd_sjtdyt_zwmArray = houseList[i].ZD_sjtdyt.Split(",");
                        if (zd_sjtdyt_zwmArray.Length > 1)
                        {
                            foreach (var item in zd_sjtdyt_zwmArray)
                            {
                                //var a = item;
                                //if (item.IndexOf("0") == 0)
                                //{
                                //    a = item[1..];
                                //}
                                //_logger.LogDebug(a);
                                var zd_sjtdytList = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == item).FirstOrDefault();
                                if (zd_sjtdytList != null)
                                    zd_sjtdGhyt_zwm += zd_sjtdytList.DNAME + ",";
                            }

                            zd_sjtdGhyt_zwm = zd_sjtdGhyt_zwm.Substring(0, zd_sjtdGhyt_zwm.Length - 1);
                            houseList[i].ZD_sjtdyt_zwm = zd_sjtdGhyt_zwm;
                            _logger.LogDebug(houseList[i].ZD_sjtdyt_zwm);
                        }
                        else
                        {
                            var zd_sjtdghyt = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == houseList[i].ZD_sjtdyt).FirstOrDefault();
                            houseList[i].ZD_sjtdyt_zwm = zd_sjtdghyt != null ? zd_sjtdghyt.DNAME : string.Empty;
                            zd_sjtdGhyt_zwm = houseList[i].ZD_sjtdyt_zwm;
                        }
                    }
                    #endregion

                    #region pztdyt
                    if (!string.IsNullOrEmpty(houseList[i].ZD_pztdyt))
                    {
                        if (string.IsNullOrEmpty(houseList[i].TDYT))
                        {
                            houseList[i].TDYT = houseList[i].ZD_pztdyt;
                        }
                        string[] zd_pztdyt_zwmArray = houseList[i].ZD_pztdyt.Split(",");
                        if (zd_pztdyt_zwmArray.Length > 1)
                        {
                            foreach (var item in zd_pztdyt_zwmArray)
                            {
                                //var a = item;
                                //if (item.IndexOf("0") == 0)
                                //{
                                //    a = item[1..];
                                //}
                                var zd_pztdytList = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == item).FirstOrDefault();
                                if (zd_pztdytList != null)
                                    zd_pztdGhyt_zwm += zd_pztdytList.DNAME + ",";
                            }
                            zd_pztdGhyt_zwm = zd_pztdGhyt_zwm.Substring(0, zd_pztdGhyt_zwm.Length - 1);
                            houseList[i].ZD_pztdyt_zwm = zd_pztdGhyt_zwm;
                            _logger.LogDebug(houseList[i].ZD_pztdyt_zwm);
                        }
                        else
                        {
                            var zd_pztdghyt = sysDic.Where(s => s.GID == 8 && s.DEFINED_CODE == houseList[i].ZD_pztdyt).FirstOrDefault();
                            houseList[i].ZD_pztdyt_zwm = zd_pztdghyt != null ? zd_pztdghyt.DNAME : string.Empty;
                            zd_pztdGhyt_zwm = houseList[i].ZD_pztdyt_zwm;
                        }
                    }
                    #endregion
                    #endregion
                    //houseList[i].TDQLLX = string.IsNullOrEmpty(houseList[i].TDQLLX.Trim()) ? string.IsNullOrEmpty(houseList[i].FCZ_TDQLLX.Trim()) ? houseList[i].ZD_qllx : houseList[i].FCZ_TDQLLX : houseList[i].TDQLLX;
                    houseList[i].TDQLLX_zwm = string.IsNullOrEmpty(tdQllx_zwm.Trim()) ? string.IsNullOrEmpty(fcz_tdQllx_zwm.Trim()) ? zd_tdQllx_zwm : fcz_tdQllx_zwm : tdQllx_zwm;

                    //houseList[i].TDQLXZ = string.IsNullOrEmpty(houseList[i].TDQLXZ.Trim()) ? string.IsNullOrEmpty(houseList[i].FCZ_TDQLXZ.Trim()) ? houseList[i].ZD_qlxz : houseList[i].FCZ_TDQLXZ : houseList[i].TDQLXZ;
                    houseList[i].TDQLXZ_zwm = string.IsNullOrEmpty(tdQlxz_zwm.Trim()) ? string.IsNullOrEmpty(fcz_tdQlxz_zwm.Trim()) ? zd_tdQlxz_zwm : fcz_tdQlxz_zwm : tdQlxz_zwm;

                    //房屋用途不为null取tdGhyt_zwm，否则取fcz_tdGhyt_zwm，fcz_tdGhyt_zwm为null,取zd表的pztdyt，pztdyt为null取sjtdyt
                    //houseList[i].TDYT = string.IsNullOrEmpty(houseList[i].TDYT.Trim()) ? string.IsNullOrEmpty(houseList[i].FCZ_ghyt.Trim()) ? string.IsNullOrEmpty(houseList[i].ZD_pztdyt.Trim()) ? houseList[i].ZD_sjtdyt : houseList[i].ZD_pztdyt : houseList[i].FCZ_ghyt : houseList[i].TDYT;
                    houseList[i].tdyt_zwm = string.IsNullOrEmpty(tdGhyt_zwm.Trim()) ? string.IsNullOrEmpty(fcz_tdGhyt_zwm.Trim()) ? string.IsNullOrEmpty(zd_pztdGhyt_zwm.Trim()) ? zd_sjtdGhyt_zwm : zd_pztdGhyt_zwm : fcz_tdGhyt_zwm : tdGhyt_zwm;

                    houseList[i].GYTDMJ = !string.IsNullOrEmpty(houseList[i].GYTDMJ.ToString()) ? houseList[i].GYTDMJ : houseList[i].ZDMJ;
                    houseList[i].TDZZRQ = !string.IsNullOrEmpty(houseList[i].TDZZRQ.ToString()) ? houseList[i].TDZZRQ : houseList[i].zd_TDZZRQ;

                    houseList[i].JZMJ = !string.IsNullOrEmpty(houseList[i].JZMJ.ToString()) ? houseList[i].JZMJ : houseList[i].YCJZMJ;
                    houseList[i].TNJZMJ = !string.IsNullOrEmpty(houseList[i].TNJZMJ.ToString()) ? houseList[i].TNJZMJ : houseList[i].YCTNJZMJ;
                    houseList[i].FTJZMJ = !string.IsNullOrEmpty(houseList[i].FTJZMJ.ToString()) ? houseList[i].FTJZMJ : houseList[i].YCFTJZMJ;

                }
                int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / PageSize.ObjToDecimal()).ObjToInt();
                model.page = intPageIndex;
                model.PageSize = PageSize;
                model.dataCount = totalCount;
                model.pageCount = pageCount;
                model.data = houseList;
            }
            return model;
        }

        /// <summary>
        /// 获取房产户权籍调查信息
        /// </summary>
        /// <param name="bdcdyh">当前业务不动产单元号</param>
        /// <returns></returns>
        public async Task<List<fc_h_qsdcVmodel>> GetAdvanceByHouseInfo(string bdcdyh)
        {            
            base.ChangeDB(SysConst.DB_CON_IIRS);
            var sysDic = await base.Db.Queryable<SYS_DIC>().In(it => it.GID, new int[] { 1, 3, 4, 5, 6, 7, 8, 9 }).ToListAsync();
            base.ChangeDB(SysConst.DB_CON_BDC);
            base.Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            SYS_DIC fwyt_list = new SYS_DIC();

            var HouseData = await base.Db.Queryable<FC_H_QSDC>().Where(h => (h.BDCDYH == bdcdyh && (h.LIFECYCLE == 0 || h.LIFECYCLE == null))).
                Select((h) => new fc_h_qsdcVmodel()
                {
                    TSTYBM = h.TSTYBM,
                    BDCDYH = h.BDCDYH,
                    GHYT = h.GHYT,
                    ZL = h.ZL,
                    JZMJ = h.JZMJ != null ? h.JZMJ : h.YCJZMJ,
                    MYC = h.MYC,
                    ZT = "",
                }).ToListAsync();

            String[] TstybmArr = new String[HouseData.Count()];
            if (HouseData.Count > 0)
            {
                for (int i = 0; i < HouseData.Count; i++)
                {
                    TstybmArr[i] = HouseData[i].TSTYBM;
                }
            }

            //查询条件查询出房子的所有状态
            var djzlData = base.Db.Queryable<DJ_TSGL>().Where(i => TstybmArr.Contains(i.TSTYBM) && (i.LIFECYCLE == 0 || i.LIFECYCLE == null)).GroupBy(i => new { tstybm = i.TSTYBM }).Select(i => new
            {
                tstybm = i.TSTYBM,
                ZT = SqlFunc.MappingColumn(i.DJZL, "WM_CONCAT(DISTINCT TO_CHAR(DJZL))")
            }).ToList();

            if (HouseData.Count > 0)
            {
                for (int i = 0; i < HouseData.Count; i++)
                {
                    for (int j = 0; j < djzlData.Count; j++)
                    {
                        if (HouseData[i].TSTYBM == djzlData[j].tstybm)
                        {
                            HouseData[i].ZT = djzlData[j].ZT;
                        }
                        else
                        {
                            HouseData[i].ZT = "正常";
                        }
                    }
                    fwyt_list = sysDic.Where(s => s.GID == 5 && s.DEFINED_CODE == HouseData[i].GHYT).FirstOrDefault();
                    HouseData[i].GHYT = fwyt_list.DNAME;
                    if(djzlData.Count == 0)
                    {
                        HouseData[i].ZT = "正常";
                    }
                }
            }
            return HouseData;
        }

        /// <summary>
        /// 获取该合同编号下的网签数据
        /// </summary>
        /// <param name="HTBH">当前业务SLBH</param>
        /// <returns></returns>
        public async Task<List<V_BDCZJK_WQ_Vmodel>> GetAdvanceByInternetSignInfo(string HTBH)
        {
            //yw_slbh = "1";
            //dj_slbh = "TDTD-DNW1-001-T";

            base.ChangeDB(SysConst.DB_CON_BDC);
            List<V_BDCZJK_WQ_Vmodel> modelList = new List<V_BDCZJK_WQ_Vmodel>();
            var datalist = await base.Db.Queryable<V_BDCZJK_WQ>().Where(a => a.HTBH == HTBH).Select(b => new V_BDCZJK_WQ_Vmodel()
            {
                HTBH = b.HTBH,
                XZQHDM = b.XZQHDM,
                QX = b.QX,
                MYC = b.MYC,
                JZMJ = b.JZMJ,
                FWYT = b.FWYT,
                JYZLB = b.JYZLB,
                JYZQC = b.JYZQC,
                JYZZJHM = b.JYZZJHM,
                JYZZJMC = b.JYZZJMC,
                LXDH = b.LXDH,
                CJJE = b.CJJE,
            }).ToListAsync();


            return datalist;
        }
    }
}
