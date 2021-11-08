using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SqlSugar;

namespace WMS.IService
{
    public interface IBaseService<TEntity> where TEntity : class, new()
    {
        /// <summary>
        /// 新增单个
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>受影响行数</returns>
        Task<TEntity> CreateAsync(TEntity entity);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities">实体对象</param>
        /// <returns>成功或失败</returns>
        Task<int> InsertListAsync(List<TEntity> entities);

        /// <summary>
        /// 批量新增
        /// </summary>
        /// <param name="entities">实体对象</param>
        /// <returns>实体对象集合</returns>
        Task<TEntity> InsertListReturnEntityAsync(List<TEntity> entities);

        /// <summary>
        /// 根据id删除
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>成功或失败</returns>
        Task<int> DeleteAsync(string id);

        /// <summary>
        /// 根据表达式删除
        /// </summary>
        /// <param name="func">表达式</param>
        /// <returns>成功或失败</returns>
        Task<int> DeleteAsync(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// 根据id批量删除
        /// </summary>
        /// <param name="ids">id数组</param>
        /// <returns>删除数量</returns>
        Task<int> DeleteAsyncByList(int[] ids);

        /// <summary>
        /// 修改单个
        /// </summary>
        /// <param name="entity">需要修改的实体对象</param>
        /// <returns>成功或失败</returns>
        Task<int> EditAsync(TEntity entity);

        /// <summary>
        /// 修改单个
        /// </summary>
        /// <param name="func">表达式</param>
        /// <param name="entity">需要修改的实体对象</param>
        /// <returns>成功或失败</returns>
        Task<int> EditAsync(Expression<Func<TEntity, bool>> func, TEntity entity);

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="entities">实体对象集合</param>
        /// <returns>成功或失败</returns>
        Task<int> EditListAsync(List<TEntity> entities);

        /// <summary>
        /// 根据id查询
        /// </summary>
        /// <param name="id">id</param>
        /// <returns>实体对象</returns>
        Task<TEntity> FindAsync(int id);

        /// <summary>
        /// 根据表达式查询单笔
        /// </summary>
        /// <param name="func">表达式</param>
        /// <returns>实体对象</returns>
        Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func);

        /// <summary>
        /// 查询全部数据
        /// </summary>
        /// <returns>实体对象集合</returns>
        Task<List<TEntity>> QueryAsync();

        /// <summary>
        ///根据表达式查询
        /// </summary>
        /// <param name="func">表达式</param>
        /// <returns>实体对象集合</returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func);


        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="page">目标页数</param>
        /// <param name="size">每页大小</param>
        /// <param name="total">页数总计</param>
        /// <returns>实体对象集合</returns>
        Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total);

        /// <summary>
        /// 自定义条件分页查询逆序
        /// </summary>
        /// <param name="func">查询条件</param>
        /// <param name="sortDesc">排序规则</param>
        /// <param name="page">目标页数</param>
        /// <param name="size">每页大小</param>
        /// <param name="total">页数总计</param>
        /// <param name="take">查询数目</param>
        /// <returns>实体对象集合</returns>
        Task<List<TEntity>> QueryAsyncByDesc(Expression<Func<TEntity, bool>> func, Expression<Func<TEntity, object>> sortDesc, int page, int size, RefAsync<int> total, int take);

        /// <summary>
        /// 自定义条件分页查询逆序
        /// </summary>
        /// <param name="func">查询条件</param>
        /// <param name="sortDesc">排序规则</param>
        /// <param name="page">目标页数</param>
        /// <param name="size">每页大小</param>
        /// <param name="total">页数总计</param>
        /// <returns>实体对象集合</returns>
        Task<List<TEntity>> QueryAsyncByDesc(Expression<Func<TEntity, bool>> func, Expression<Func<TEntity, object>> sortDesc, int page, int size, RefAsync<int> total);


        /// <summary>
        /// 自定义条件分页查询
        /// </summary>
        /// <param name="func">查询条件</param>
        /// <param name="page">目标页数</param>
        /// <param name="size">每页大小</param>
        /// <param name="total">页数总计</param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="func"></param>
        /// <param name="num"></param>
        /// <param name="sortDesc"></param>
        /// <returns></returns>
        Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int num, Expression<Func<TEntity, object>> sortDesc);
    }
}
