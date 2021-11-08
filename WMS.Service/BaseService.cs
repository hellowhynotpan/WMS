/**************************************************** 
文件名称：
功能描述:
创建日期: 
作者: Light
最后修改人:
**************************************************/
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WMS.IRepository;
using WMS.IService;

namespace WMS.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity : class, new()
    {
        //从子类的构造函数中传入
        protected IBaseRepository<TEntity> _iBaseRepository;

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return await _iBaseRepository.CreateAsync(entity);
        }

        public async Task<int> DeleteAsync(string id)
        {
            return await _iBaseRepository.DeleteAsync(id);
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _iBaseRepository.DeleteAsync(func);
        }

        public async Task<int> DeleteAsyncByList(int[] ids)
        {
            return await _iBaseRepository.DeleteAsyncByList(ids);
        }

        public async Task<int> EditAsync(TEntity entity)
        {
            return await _iBaseRepository.EditAsync(entity);
        }

        public async Task<int> EditAsync(Expression<Func<TEntity, bool>> func, TEntity entity)
        {
            return await _iBaseRepository.EditAsync(func, entity);
        }

        public async Task<int> EditListAsync(List<TEntity> entities)
        {
            return await _iBaseRepository.EditListAsync(entities);
        }

        public async Task<TEntity> FindAsync(int id)
        {
            return await _iBaseRepository.FindAsync(id);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _iBaseRepository.FindAsync(func);
        }

        public async Task<int> InsertListAsync(List<TEntity> entities)
        {
            return await _iBaseRepository.InsertListAsync(entities);
        }

        public async Task<TEntity> InsertListReturnEntityAsync(List<TEntity> entities)
        {
            return await _iBaseRepository.InsertListReturnEntityAsync(entities);
        }

        public async Task<List<TEntity>> QueryAsync()
        {
            return await _iBaseRepository.QueryAsync();
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await _iBaseRepository.QueryAsync(func);
        }

        public async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await _iBaseRepository.QueryAsync(page, size, total);
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await _iBaseRepository.QueryAsync(func,page, size, total);
        }

        public async Task<List<TEntity>> QueryAsyncByDesc(Expression<Func<TEntity, bool>> func, Expression<Func<TEntity, object>> sortDesc, int page, int size, RefAsync<int> total, int take)
        {
            return await _iBaseRepository.QueryAsync(func, page, size, total);
        }

        public async Task<List<TEntity>> QueryAsyncByDesc(Expression<Func<TEntity, bool>> func, Expression<Func<TEntity, object>> sortDesc, int page, int size, RefAsync<int> total)
        {
            return await _iBaseRepository.QueryAsyncByDesc(func, sortDesc, page, size, total);
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int num, Expression<Func<TEntity, object>> sortDesc) 
        {
            return await _iBaseRepository.QueryAsync(func, num, sortDesc);
        }
    }
}
