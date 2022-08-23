using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IIRS.IRepository.Base;
using IIRS.IServices.Base;
using IIRS.Models.ViewModel;
using SqlSugar;

namespace IIRS.Services.Base
{
    public class BaseServices : IBaseServices
    {
        #region 内部变量
        private readonly IDBTransManagement _dbTransManagement;
        private string _dbName = IIRS.Utilities.AppsettingsUtility.SiteConfig.MainDB.ToLower();
        /// <summary>
        /// 数据库对象集合
        /// </summary>
        internal readonly SqlSugarClient dbBase;

        /// <summary>
        /// 
        /// </summary>
        private ISqlSugarClient _db
        {
            get
            {
                dbBase.ChangeDatabase(_dbName);
                return dbBase;
            }
        }

        /// <summary>
        /// 默认连接数据库对象
        /// </summary>
        internal ISqlSugarClient Db
        {
            get { return _db; }
        }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="dbTransManagement"></param>
        public BaseServices(IDBTransManagement dbTransManagement)
        {
            _dbTransManagement = dbTransManagement;
            dbBase = _dbTransManagement.GetDbClient();
        }

        public void ChangeDB(string DBName)
        {
            _dbName = DBName.ToLower();
        }

        #region Query
        //2个表
        public async Task<List<TResult>> Query<T, T1, TResult>(Expression<Func<T, T1, object[]>> joinExpression, Expression<Func<T, T1, TResult>> selectExpression, Expression<Func<T, T1, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            return await _db.Queryable<T, T1>(joinExpression)
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .Select(selectExpression).ToListAsync();
        }

        //3个表
        public async Task<List<TResult>> Query<T, T1, T2, TResult>(Expression<Func<T, T1, T2, object[]>> joinExpression, Expression<Func<T, T1, T2, TResult>> selectExpression, Expression<Func<T, T1, T2, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            return await _db.Queryable<T, T1, T2>(joinExpression)
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .Select(selectExpression).ToListAsync();
        }

        //4个表
        public async Task<List<TResult>> Query<T, T1, T2, T3, TResult>(Expression<Func<T, T1, T2, T3, object[]>> joinExpression, Expression<Func<T, T1, T2, T3, TResult>> selectExpression, Expression<Func<T, T1, T2, T3, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            return await _db.Queryable<T, T1, T2, T3>(joinExpression)
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .Select(selectExpression).ToListAsync();
        }

        public async Task<List<TResult>> Query<T, T1, T2, T3, T4, TResult>(Expression<Func<T, T1, T2, T3, T4, object[]>> joinExpression, Expression<Func<T, T1, T2, T3, T4, TResult>> selectExpression, Expression<Func<T, T1, T2, T3, T4, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            return await _db.Queryable<T, T1, T2, T3, T4>(joinExpression)
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .Select(selectExpression).ToListAsync();
        }
        #endregion

        #region QueryPage
        public async Task<DynamicPageModel> QueryPage<T>(Expression<Func<T, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            var list = await _db.Queryable<T>()
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .ToPageListAsync(intPageIndex, intPageSize, totalCount);

            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal()).ObjToInt();
            return new DynamicPageModel() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list.ToList<dynamic>() };
        }

        public async Task<DynamicPageModel> QueryPage<T, T1>(Expression<Func<T, T1, object[]>> joinExpression, Expression<Func<T, T1, dynamic>> selectExpression, Expression<Func<T, T1, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            var list = await _db.Queryable<T, T1>(joinExpression)
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .Select(selectExpression)
             .ToPageListAsync(intPageIndex, intPageSize, totalCount);

            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal()).ObjToInt();
            return new DynamicPageModel() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list };
        }

        public async Task<DynamicPageModel> QueryPage<T, T1, T2>(Expression<Func<T, T1, T2, object[]>> joinExpression, Expression<Func<T, T1, T2, dynamic>> selectExpression, Expression<Func<T, T1, T2, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            var list = await _db.Queryable<T, T1, T2>(joinExpression)
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .Select(selectExpression)
             .ToPageListAsync(intPageIndex, intPageSize, totalCount);

            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal()).ObjToInt();
            return new DynamicPageModel() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list };
        }

        public async Task<DynamicPageModel> QueryPage<T, T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, object[]>> joinExpression, Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, dynamic>> selectExpression, Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            var list = await _db.Queryable<T, T1, T2, T3, T4, T5, T6, T7>(joinExpression)
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .Select(selectExpression)
             .ToPageListAsync(intPageIndex, intPageSize, totalCount);

            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal()).ObjToInt();
            return new DynamicPageModel() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list };
        }
        #endregion

        /// <summary>
        /// 八张表分页查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="T5"></typeparam>
        /// <typeparam name="T6"></typeparam>
        /// <typeparam name="T7"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        public async Task<PageModel<TResult>> QueryResultList<T, T1, T2, T3, T4, T5, T6, T7, TResult>(System.Linq.Expressions.Expression<System.Func<T, T1, T2, T3, T4, T5, T6, T7, object[]>> joinExpression, System.Linq.Expressions.Expression<System.Func<T, T1, T2, T3, T4, T5, T6, T7, TResult>> selectExpression, System.Linq.Expressions.Expression<System.Func<T, T1, T2, T3, T4, T5, T6, T7, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            var list = await _db.Queryable<T, T1, T2, T3, T4, T5, T6, T7>(joinExpression)
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .Select(selectExpression)
             .ToPageListAsync(intPageIndex, intPageSize, totalCount);

            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal()).ObjToInt();
            return new PageModel<TResult>() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list };
        }

        public async Task<PageModel<TResult>> QueryResultList<T, T1, T2, T3, T4, TResult>(Expression<Func<T, T1, T2, T3, T4, object[]>> joinExpression, Expression<Func<T, T1, T2, T3, T4, TResult>> selectExpression, Expression<Func<T, T1, T2, T3, T4, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            var list = await _db.Queryable<T, T1, T2, T3, T4>(joinExpression)
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .Select(selectExpression)
             .ToPageListAsync(intPageIndex, intPageSize, totalCount);

            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal()).ObjToInt();
            return new PageModel<TResult>() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list };
        }

        public async Task<PageModel<TResult>> QueryResultList<T, T1, T2,TResult>(Expression<Func<T, T1, T2,object[]>> joinExpression, Expression<Func<T, T1, T2,TResult>> selectExpression, Expression<Func<T, T1, T2,bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            var list = await _db.Queryable<T, T1, T2>(joinExpression)
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .Select(selectExpression)
             .ToPageListAsync(intPageIndex, intPageSize, totalCount);

            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal()).ObjToInt();
            return new PageModel<TResult>() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list };
        }

        public async Task<PageModel<TResult>> QueryResultList<T, T1, TResult>(Expression<Func<T, T1, object[]>> joinExpression, Expression<Func<T, T1, TResult>> selectExpression, Expression<Func<T, T1, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new()
        {
            RefAsync<int> totalCount = 0;
            var list = await _db.Queryable<T, T1>(joinExpression)
             .OrderByIF(!string.IsNullOrEmpty(strOrderByFileds), strOrderByFileds)
             .WhereIF(whereExpression != null, whereExpression)
             .Select(selectExpression)
             .ToPageListAsync(intPageIndex, intPageSize, totalCount);

            int pageCount = Math.Ceiling(totalCount.ObjToDecimal() / intPageSize.ObjToDecimal()).ObjToInt();
            return new PageModel<TResult>() { dataCount = totalCount, pageCount = pageCount, page = intPageIndex, PageSize = intPageSize, data = list };
        }

        /// <summary>
        /// 查询-多表查询
        /// </summary> 
        /// <typeparam name="T">实体1</typeparam> 
        /// <typeparam name="T2">实体2</typeparam> 
        /// <typeparam name="T3">实体3</typeparam>
        /// <typeparam name="TResult">返回对象</typeparam>
        /// <param name="joinExpression">关联表达式 (join1,join2) => new object[] {JoinType.Left,join1.UserNo==join2.UserNo}</param> 
        /// <param name="selectExpression">返回表达式 (s1, s2) => new { Id =s1.UserNo, Id1 = s2.UserNo}</param>
        /// <param name="whereLambda">查询表达式 (w1, w2) =>w1.UserNo == "")</param> 
        /// <returns>值</returns>
        public async Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(
            Expression<Func<T, T2, T3, object[]>> joinExpression,
            Expression<Func<T, T2, T3, TResult>> selectExpression,
            Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new()
        {
            if (whereLambda == null)
            {
                return await _db.Queryable(joinExpression).Select(selectExpression).ToListAsync();
            }
            return await _db.Queryable(joinExpression).Where(whereLambda).Select(selectExpression).ToListAsync();
        }
    }
}
