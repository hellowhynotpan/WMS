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
using System.Text;
using System.Threading.Tasks;
using WMS.Model.Entity;
using WMS.Model.DTO;

namespace WMS.IRepository
{
    public interface  IStockMRepository : IBaseRepository<StockM>
    {
        public Task<List<StockDTO>> QueryStock(string createOwner);

        /// <summary>
        /// 扣账事务 新增库存
        /// </summary>
        /// <param name="stockM"></param>
        /// <param name="stockD"></param>
        /// <returns></returns>
        public Task<bool> AddStockM(StockM stockM, StockD stockD);

        /// <summary>
        /// 扣账事务 更新库存
        /// </summary>
        /// <param name="stockM"></param>
        /// <param name="stockD"></param>
        /// <returns></returns>
        public Task<bool> UpdStockM(StockM stockM, StockD stockD);
    }
}
