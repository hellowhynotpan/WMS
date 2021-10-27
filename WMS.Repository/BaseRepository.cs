/**************************************************** 
文件名称：
功能描述:
创建日期: 
作者: Light
最后修改人:
**************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using SqlSugar;
using SqlSugar.IOC;
using WMS.IRepository;
using WMS.Model;

namespace WMS.Repository
{
    public class BaseRepository<TEntity>: SimpleClient<TEntity>, IBaseRepository<TEntity> where TEntity : class, new()
    {
        public BaseRepository(ISqlSugarClient context = null) : base(context)
        {
            Context = DbScoped.SugarScope;
            Context.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql);//输出sql
                //Console.WriteLine(string.Join(",", pars?.Select(it => it.ParameterName + ":" + it.Value)));
            };
            //数据库
            /*base.Context.DbMaintenance.CreateDatabase();*/
            //初始化数据表
            /*base.Context.CodeFirst.InitTables(
                *//*typeof(SysUser),
                typeof(SysUserLogOn)*//*
                typeof(StockM),
                typeof(StockD),
                typeof(Inbill),
                typeof(InbillD),
                typeof(BaseWareHouse),
                typeof(BasePart),
                typeof(BaseCargospace)*//*
                );*/
        }

        public async Task<TEntity> CreateAsync(TEntity entity)
        {
            return await Context.Insertable(entity).ExecuteReturnEntityAsync();
        }

        public async Task<int> InsertListAsync(List<TEntity> entities)
        {
            return await Context.Insertable(entities).ExecuteCommandAsync();
        }

        public async Task<TEntity> InsertListReturnEntityAsync(List<TEntity> entities)
        {
            return await Context.Insertable(entities).ExecuteReturnEntityAsync();
        }

        public async Task<int> DeleteAsync(string id)
        {
            return await Context.Deleteable<TEntity>().In(id).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> func)
        {
            return await Context.Deleteable<TEntity>().Where(func).ExecuteCommandAsync();
        }

        public async Task<int> DeleteAsyncByList(int[] ids)
        {
            return await Context.Deleteable<TEntity>().In(ids).ExecuteCommandAsync();
        }

        public async Task<int> EditAsync(TEntity entity)
        {
            return await Context.Updateable(entity).ExecuteCommandAsync();
        }

        public async Task<int> EditAsync(Expression<Func<TEntity, bool>> func, TEntity entity)
        {
            return await Context.Updateable(entity).Where(func).ExecuteCommandAsync();
        }

        public async Task<int> EditListAsync(List<TEntity> entities)
        {
            return await Context.Updateable(entities).ExecuteCommandAsync();
        }

        public async Task<TEntity> FindAsync(int id)
        {
            return await Context.Queryable<TEntity>().InSingleAsync(id);
        }


        public async Task<List<TEntity>> QueryAsync()
        {
            return await Context.Queryable<TEntity>().ToListAsync();
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func)
        {
            return await Context.Queryable<TEntity>().Where(func).ToListAsync();
        }

        public async Task<List<TEntity>> QueryAsync(int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>().ToPageListAsync(page, size, total);
        }

        public async Task<List<TEntity>> QueryAsync(Expression<Func<TEntity, bool>> func, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>()
                .Where(func)
                .ToPageListAsync(page, size, total);
        }

        public async Task<List<TEntity>> QueryAsyncByDesc(Expression<Func<TEntity, bool>> func, Expression<Func<TEntity, object>> sortDesc, int page, int size, RefAsync<int> total, int take)
        {
            return await base.Context.Queryable<TEntity>()
              .Where(func)
              .OrderBy(sortDesc, OrderByType.Desc)
              .Take(take)
              .ToPageListAsync(page, size, total);
        }

        public async Task<List<TEntity>> QueryAsyncByDesc(Expression<Func<TEntity, bool>> func, Expression<Func<TEntity, object>> sortDesc, int page, int size, RefAsync<int> total)
        {
            return await base.Context.Queryable<TEntity>()
              .Where(func)
              .OrderBy(sortDesc, OrderByType.Desc)
              .ToPageListAsync(page, size, total);
        }

        public async Task<TEntity> FindAsync(Expression<Func<TEntity, bool>> func)
        {
            return await Context.Queryable<TEntity>().Where(func).FirstAsync();
        }
    }
}
