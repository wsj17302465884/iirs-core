using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IIRS.IRepository;
using IIRS.IRepository.Base;
using IIRS.IServices;
using IIRS.IServices.Base;
using IIRS.Models.EntityModel;
using IIRS.Models.ViewModel;
using IIRS.Services.Base;
using IIRS.Utilities.SwaggerHelper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace IIRS.Controllers.V2
{
    /// <summary>
    /// 第二版测试控制器
    /// </summary>
    [ApiController]
    [CustomRoute(ApiVersions.V2)]
    [Produces("application/json")]
    public class TestController : ControllerBase
    {
        /// <summary>
        /// 事务管理
        /// </summary>
        private readonly IDBTransManagement _dbTransManagement;

        /// <summary>
        /// 数据库集合
        /// </summary>
        private readonly SqlSugarClient _sqlSugarClient;

        /// <summary>
        /// 用户信息表
        /// </summary>
        readonly IUserInfoRepository _sysUserInfoRepository;

        /// <summary>
        /// 测试表
        /// </summary>
        readonly ITestTableRepository _testTableRepository;

        readonly IMortgageServices _mortgageServices;

        readonly IBaseServices _baseServices;

        private readonly ILogger<TestController> _logger;

        readonly IHouseStatusRepository _houseStatusRepository;

        /// <summary>
        /// 初始化，依赖注入相关接口类
        /// </summary>
        /// <param name="dbTransManagement"></param>
        /// <param name="sysUserInfoRepository"></param>
        /// <param name="testTableRepository"></param>
        /// <param name="mortgageServices"></param>
        /// <param name="logger"></param>
        /// <param name="baseServices"></param>
        /// <param name="houseStatusRepository"></param>
        public TestController(IDBTransManagement dbTransManagement, IUserInfoRepository sysUserInfoRepository, ITestTableRepository testTableRepository, IMortgageServices mortgageServices, ILogger<TestController> logger,IBaseServices baseServices,IHouseStatusRepository houseStatusRepository)
        {
            //将初始化的相关依赖注入对象与私有对象相对应赋值
            _dbTransManagement = dbTransManagement;
            _sqlSugarClient = _dbTransManagement.GetDbClient();
            _sysUserInfoRepository = sysUserInfoRepository;
            _testTableRepository = testTableRepository;
            _mortgageServices = mortgageServices;
            _logger = logger;
            _baseServices = baseServices;
            _houseStatusRepository = houseStatusRepository;
        }

        /// <summary>
        /// 测试一：主数据库读取
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<PageModel<Sys_Userinfo>>> Get1()
        {
            var data = await _sysUserInfoRepository.QueryPage(a => a.ErrorCount < 3);
            return new MessageModel<PageModel<Sys_Userinfo>>()
            {
                msg = "获取成功",
                success = data.dataCount >= 0,
                response = data
            };
        }

        /// <summary>
        /// 测试二：SQL语句执行
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<MessageModel<IEnumerable<dynamic>>> Get2()
        {
            // 数据库名称一定要小写
            //_sqlSugarClient.ChangeDatabase("lysxk");
            var ttt = await _sqlSugarClient.Ado.SqlQueryAsync<dynamic>("select * from UserInfo");
            return new MessageModel<IEnumerable<dynamic>>()
            {
                msg = "获取成功",
                success = ttt.Count >= 0,
                response = ttt
            };
        }

        /// <summary>
        /// 测试三：事务测试
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<string>> Get3()
        {
            List<string> returnMsg = new List<string>() { };
            try
            {
                returnMsg.Add($"开始事务");

                _dbTransManagement.BeginTran();
                var TestData = await _testTableRepository.Query(d => d.IsEnable == false);
                returnMsg.Add($"首次查询：共有 {TestData.Count} 条记录");


                returnMsg.Add($"开始向测试表中插入数据......");
                var insertTestData = await _testTableRepository.Add(new TestTable()
                {
                    id = Guid.NewGuid(),
                    Name = Guid.NewGuid().ToString(),
                    IsEnable = false,
                    EditDate = DateTime.Now
                });


                TestData = await _testTableRepository.Query();
                returnMsg.Add($"第二次查询：共有 {TestData.Count} 条记录");

                returnMsg.Add($"生成一个错误");
                int ex = 0;
                int throwEx = 1 / ex;

                returnMsg.Add($"开始向测试表中插入数据......");
                insertTestData = await _testTableRepository.Add(new TestTable()
                {
                    id = Guid.NewGuid(),
                    Name = Guid.NewGuid().ToString(),
                    IsEnable = false,
                    EditDate = DateTime.Now
                });

                TestData = await _testTableRepository.Query();
                returnMsg.Add($"第三次查询：共有 {TestData.Count} 条记录");

                _dbTransManagement.CommitTran();
                returnMsg.Add($"事务提交");
            }
            catch (Exception)
            {
                returnMsg.Add($"事务回滚");
                _dbTransManagement.RollbackTran();
                var TestData = await _testTableRepository.Query(d => d.IsEnable == false);
                returnMsg.Add($"第四次查询：共有 {TestData.Count} 条记录");
            }

            return returnMsg;
        }

        /// <summary>
        /// 测试四：多表联查
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<DynamicPageModel> Get4()
        {
            //读取LYSXK用户
            (_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[2].ConnId);
            Expression<Func<SJD_INFO, QLRGL_INFO, object[]>> joinExpression = (a, b) => new object[] { JoinType.Inner, a.SLBH == b.SLBH };
            Expression<Func<SJD_INFO, QLRGL_INFO, dynamic>> selectExpression = (a, b) => new { slbh = a.SLBH, qlrmc = b.QLRMC };
            return await _mortgageServices.QueryPage<SJD_INFO, QLRGL_INFO>(joinExpression, selectExpression, null);
        }
        /// <summary>
        /// 测试六 三个表联合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<DynamicPageModel> Get5()
        {

            //(_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[2].ConnId);
            (_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[2].ConnId);
            Expression<Func<SJD_INFO, QLRGL_INFO, TSGL_INFO, object[]>> joinExpression = (a, b, c) => new object[]
            { JoinType.Inner, a.SLBH == b.SLBH, JoinType.Inner, a.SLBH == c.SLBH  };

            Expression<Func<SJD_INFO, QLRGL_INFO, TSGL_INFO, dynamic>> selectExpression = (a, b, c) => new { zl = a.ZL, qlrmc = b.SXH, DJYY = c.DJZL };
            
            return await _mortgageServices.QueryPage<SJD_INFO, QLRGL_INFO, TSGL_INFO>(joinExpression, selectExpression, null);
        }

        /// <summary>
        /// 测试六 多表联合分页查询
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<DynamicPageModel> Get6(int intPageIndex,int intPageSize,string tstybm)
        {            
            (_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[2].ConnId);
            //日志SQL语句
            //(_mortgageServices as BaseServices).Db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    _logger.LogDebug(sql);
            //};

            //Expression<Func<SJD_INFO, QLRGL_INFO, TSGL_INFO,XGDJGL_INFO,QLR_INFO,DY_INFO,FC_H_INFO,FC_Z_INFO, object[]>> joinExpression = (a, b, c,d,e,f,g,h) => new object[]
            //{ JoinType.Inner, a.SLBH == b.SLBH, JoinType.Inner, a.SLBH == c.SLBH ,JoinType.Inner,d.ZSLBH==a.SLBH ,JoinType.Inner,e.QLRID == b.QLRID ,JoinType.Inner,f.SLBH==a.SLBH ,JoinType.Inner , g.TSTYBM == c.TSTYBM , JoinType.Inner ,h.TSTYBM == g.LSZTYBM};

            Expression<Func<TSGL_INFO, QLRGL_INFO, DJB_INFO, SJD_INFO, FWXG_INFO, FC_H_INFO, DY_INFO, CF_INFO, object[]>> joinExpression = (a, b, c, d, e, f, g, h) => new object[]
                 { JoinType.Left, b.SLBH == a.SLBH, JoinType.Left, c.SLBH == a.SLBH ,JoinType.Left,d.SLBH == a.SLBH ,JoinType.Left,e.SLBH == a.SLBH ,JoinType.Left,f.TSTYBM == a.TSTYBM ,JoinType.Left , g.SLBH == a.SLBH , JoinType.Left ,h.SLBH == a.SLBH};

            //Expression<Func<SJD_INFO, QLRGL_INFO, TSGL_INFO, XGDJGL_INFO, QLR_INFO, DY_INFO, FC_H_INFO, FC_Z_INFO, dynamic>> selectExpression = (a, b, c, d, e, f, g, h) => new { slbh = a.SLBH, qlrlx = b.QLRLX, qlrmc = b.QLRMC, zjlb = b.ZJLB, zjhm = b.ZJHM, dh = b.DH, zl = a.ZL, xgzh = d.XGZH, xgzlx = d.XGZLX, djlx = f.DJLX, djyy = f.DJYY, djrq = f.DJRQ, bdczmh = f.BDCZMH, bdcdyh = f.BDCDYH, qllx = g.QLLX, qlxz = g.QLXZ, ghyt = g.GHYT, xmmc = h.XMMC };

            Expression<Func<TSGL_INFO, QLRGL_INFO, DJB_INFO, SJD_INFO, FWXG_INFO, FC_H_INFO, DY_INFO, CF_INFO, dynamic>> selectExpression = (a, b, c, d, e, f, g, h) => new { djzl = a.DJZL, qlrlx = b.QLRLX, qlrmc = b.QLRMC, zjlb = b.ZJLB, zjhm = b.ZJHM, zl = d.ZL, qllx = e.QLLX, qlxz = e.QLXZ, ghyt = e.GHYT, jzmj = e.JZMJ, tdsyqx = f.TDSYQX, qt = c.QT, djrq = c.DJRQ, djbfj = c.FJ, bdczmh = g.BDCZMH, dymj = g.DYMJ, dymj2 = g.DYMJ2 , bdbzzqse = g.BDBZZQSE, dyfs = g.DYFS, dyqx = g.DYQX, dyqt = g.QT, dydjrq = g.DJRQ, dyfj = g.FJ, cfwh = h.CFWH, cfjg = h.CFJG, dbr = h.DBR, cfqx = h.CFQX, cfrq = h.DJSJ, cfhj = h.FJ};

            Expression<Func<TSGL_INFO, QLRGL_INFO, DJB_INFO, SJD_INFO, FWXG_INFO, FC_H_INFO, DY_INFO, CF_INFO, bool>> whereExpression =
            (a, b, c, d, e, f, g, h) => a.TSTYBM == tstybm;



            return await _mortgageServices.QueryPage<TSGL_INFO, QLRGL_INFO, DJB_INFO, SJD_INFO, FWXG_INFO, FC_H_INFO, DY_INFO, CF_INFO>(joinExpression, selectExpression, whereExpression, intPageIndex, intPageSize ,"A.SLBH DESC");

            
        }

        [HttpPost]
        public async Task<PageModel<MortgageViewModel>> Get7(int intPageIndex, int intPageSize, string tstybm)
        {
            (_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[4].ConnId);

            //日志SQL语句
            (_mortgageServices as BaseServices).Db.Aop.OnLogExecuting = (sql, pars) =>
            {
                _logger.LogDebug(sql);
            };
            

            return await _mortgageServices.QueryMortgageList(intPageIndex, intPageSize, tstybm);

        }

        /// <summary>
        /// 根据图属统一编码获取房屋的基本信息
        /// </summary>
        /// <param name="tstybm">图属统一编码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<HouseStatusViewModel>> GetHouseSatausList(string tstybm)
        {
            return await _houseStatusRepository.GetHouseSatausList(tstybm);
        }
        /// <summary>
        /// 根据身份证号获取图属统一编码
        /// </summary>
        /// <param name="zjhm">证件号码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<MortgageViewModel>> GetTstybmByZjhm(string zjhm)
        {
            (_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[2].ConnId);
            
            return await _mortgageServices.GetTstybmByZjhm(zjhm);
        }
        /// <summary>
        /// 根据证件号码查询有多少房子
        /// </summary>
        /// <param name="zjhm">证件号码</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<MortgageViewModel>> GetTstybmCountByZjhm(string zjhm)
        {
            (_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[4].ConnId);

            return await _mortgageServices.GetTstybmCountByZjhm(zjhm);
        }
        /// <summary>
        /// 根据企业名称获得该企业名下的所有TSTYBM（企业权利人类型为：权利人）
        /// </summary>
        /// <param name="qlrmc">企业名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<MortgageViewModel>> GetTstybmCountByQlrmc(string qlrmc)
        {
            (_mortgageServices as BaseServices).ChangeDB(IIRS.Utilities.AppsettingsUtility.SiteConfig.DBS[4].ConnId);
            return await _mortgageServices.GetTstybmCountByQlrmc(qlrmc);
        }






    }
}
