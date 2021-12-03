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
using WMS.Repository;

namespace WMS.Service
{
    public class InbillDSnService : BaseService<InbillDSn>, IInbillDSnService
    {
        private readonly IInbillDSnRespository _ibillDSnRepository;
        public InbillDSnService(IInbillDSnRespository ibillDSnRepository)
        {
            base._iBaseRepository = ibillDSnRepository;
            _ibillDSnRepository = ibillDSnRepository;
        }
    }
}
