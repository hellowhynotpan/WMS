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
   public class InbillService : BaseService<Inbill>, IInbillService
    {
        private readonly IInbillRepository _iInbillRepository;
        public InbillService(IInbillRepository iInbillRepository)
        {
            base._iBaseRepository = iInbillRepository;
            _iInbillRepository = iInbillRepository;
        }
    }
}
