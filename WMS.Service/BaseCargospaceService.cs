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
    public class BaseCargospaceService : BaseService<BaseCargospace>, IBaseCargospaceService
    {
        private readonly IBaseCargospaceRepository _iBaseCargospaceRepository;
        public BaseCargospaceService(IBaseCargospaceRepository iBaseCargospaceRepository)
        {
            base._iBaseRepository = iBaseCargospaceRepository;
            _iBaseCargospaceRepository = iBaseCargospaceRepository;
        }
    }
}
