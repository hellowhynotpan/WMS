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
   public class InbillService : BaseService<Inbill>, IInbillService
    {
        private readonly IInbillRepository _iInbillRepository;
        public InbillService(IInbillRepository iInbillRepository)
        {
            base._iBaseRepository = iInbillRepository;
            _iInbillRepository = iInbillRepository;
        }

        public async Task<bool> InBill(Inbill inbill, List<InbillD> inbillDs, List<InbillDSn> inbillDSns)
        {
            return await _iInbillRepository.InBill(inbill, inbillDs, inbillDSns);
        }

        public async Task<bool> EditInBill(Inbill inbill, List<InbillD> inbillDs, List<InbillDSn> inbillDSns, List<InbillD> nInbillDs, List<InbillDSn> nInbillDSns)
        {
            return await _iInbillRepository.EditInBill(inbill, inbillDs, inbillDSns, nInbillDs, nInbillDSns);
        }
    }
}
