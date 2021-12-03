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

namespace WMS.Service
{
    public class BaseWareHouseService : BaseService<BaseWareHouse>, IBaseWareHouseService
    {
        private readonly IBaseWareHouseRepository _iBaseWareHouseRepository;
        public BaseWareHouseService(IBaseWareHouseRepository iBaseWareHouseRepository)
        {
            base._iBaseRepository = iBaseWareHouseRepository;
            _iBaseWareHouseRepository = iBaseWareHouseRepository;
        }

        public async Task<bool> InvalidWh(BaseWareHouse wh, List<BaseCargospace> csList)
        {
            return await _iBaseWareHouseRepository.InvalidWh(wh, csList);
        }
    }
}
