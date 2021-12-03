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
using WMS.IRepository;
using WMS.IService;
using WMS.Model.Entity;
using WMS.Model.DTO;

namespace WMS.Service
{
    public class StockMService : BaseService<StockM>, IStockMService
    {
        private readonly IStockMRepository _iStockMRepository;
        public StockMService(IStockMRepository iStockMRepository)
        {
            base._iBaseRepository = iStockMRepository;
            _iStockMRepository = iStockMRepository;
        }

        public async Task<List<StockDTO>> QueryStock(string createOwner)
        {
            return await _iStockMRepository.QueryStock(createOwner);
        }
    }
}
