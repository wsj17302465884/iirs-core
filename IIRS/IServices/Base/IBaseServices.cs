using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using IIRS.Models.ViewModel;

namespace IIRS.IServices.Base
{
    public interface IBaseServices
    {
        #region  Query
        
        /// <summary>
        /// 2个表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        Task<List<TResult>> Query<T,T1, TResult>(Expression<Func<T, T1, object[]>> joinExpression, Expression<Func<T, T1, TResult>> selectExpression, Expression<Func<T, T1, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new();

        /// <summary>
        /// 三个表联合查询
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        Task<List<TResult>> Query<T, T1,T2, TResult>(Expression<Func<T, T1,T2, object[]>> joinExpression, Expression<Func<T, T1,T2, TResult>> selectExpression, Expression<Func<T, T1,T2, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new();

        /// <summary>
        /// 四个表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        Task<List<TResult>> Query<T, T1, T2,T3, TResult>(Expression<Func<T, T1, T2, T3, object[]>> joinExpression, Expression<Func<T, T1, T2,T3, TResult>> selectExpression, Expression<Func<T, T1, T2,T3, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new();

        /// <summary>
        /// 五个表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <typeparam name="T4"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="joinExpression"></param>
        /// <param name="selectExpression"></param>
        /// <param name="whereExpression"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        Task<List<TResult>> Query<T, T1, T2, T3, T4, TResult>(Expression<Func<T, T1, T2, T3, T4, object[]>> joinExpression, Expression<Func<T, T1, T2, T3, T4, TResult>> selectExpression, Expression<Func<T, T1, T2, T3, T4, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new();
        #endregion


        #region QueryPage
        /// <summary>
        /// 一个表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="whereExpression"></param>
        /// <param name="intPageIndex"></param>
        /// <param name="intPageSize"></param>
        /// <param name="strOrderByFileds"></param>
        /// <returns></returns>
        Task<DynamicPageModel> QueryPage<T>(Expression<Func<T, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new();


        Task<DynamicPageModel> QueryPage<T, T1>(Expression<Func<T, T1, object[]>> joinExpression, Expression<Func<T, T1, dynamic>> selectExpression, Expression<Func<T, T1, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new();

        Task<DynamicPageModel> QueryPage<T, T1, T2>(Expression<Func<T, T1, T2, object[]>> joinExpression, Expression<Func<T, T1, T2, dynamic>> selectExpression, Expression<Func<T, T1, T2, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new();

        Task<DynamicPageModel> QueryPage<T, T1, T2, T3, T4, T5, T6, T7>(Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, object[]>> joinExpression, Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, dynamic>> selectExpression, Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new();
        #endregion


        /// <summary>
        /// 八个表
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
        Task<PageModel<TResult>> QueryResultList<T, T1, T2, T3, T4, T5, T6, T7, TResult>(Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, object[]>> joinExpression, Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, TResult>> selectExpression, Expression<Func<T, T1, T2, T3, T4, T5, T6, T7, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new();

        Task<PageModel<TResult>> QueryResultList<T, T1, T2, T3, T4, TResult>(Expression<Func<T, T1, T2, T3, T4, object[]>> joinExpression, Expression<Func<T, T1, T2, T3, T4, TResult>> selectExpression, Expression<Func<T, T1, T2, T3, T4, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new();

        Task<PageModel<TResult>> QueryResultList<T, T1, T2, TResult>(Expression<Func<T, T1, T2, object[]>> joinExpression, Expression<Func<T, T1, T2, TResult>> selectExpression, Expression<Func<T, T1, T2, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new();

        Task<PageModel<TResult>> QueryResultList<T, T1, TResult>(Expression<Func<T, T1, object[]>> joinExpression, Expression<Func<T, T1, TResult>> selectExpression, Expression<Func<T, T1, bool>> whereExpression, int intPageIndex = 1, int intPageSize = 20, string strOrderByFileds = null) where T : class, new();

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
        Task<List<TResult>> QueryMuch<T, T2, T3, TResult>(
            Expression<Func<T, T2, T3, object[]>> joinExpression,
            Expression<Func<T, T2, T3, TResult>> selectExpression,
            Expression<Func<T, T2, T3, bool>> whereLambda = null) where T : class, new();



    }
}
