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
using WMS.Model;

namespace WMS.Service
{
    public class StockDService : BaseService<StockD>, IStockDService
    {
        private readonly IStockDRepository _iStockDRepository;
        public StockDService(IStockDRepository iStockDRepository)
        {
            base._iBaseRepository = iStockDRepository;
            _iStockDRepository = iStockDRepository;
        }
    }
}
