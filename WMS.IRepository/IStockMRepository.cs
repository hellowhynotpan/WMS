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
using WMS.Model;
using WMS.Model.DTO;

namespace WMS.IRepository
{
    public interface  IStockMRepository : IBaseRepository<StockM>
    {
        public Task<List<StockDTO>> QueryStock(string createOwner);
    }
}
