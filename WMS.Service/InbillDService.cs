﻿/**************************************************** 
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
using WMS.Model.DTO;
using WMS.Model.Entity;

namespace WMS.Service
{
    public class InbillDService : BaseService<InbillD>, IInbillDService
    {
        private readonly IInbillDRepository _iInbillDRepository;
        public InbillDService(IInbillDRepository iInbillDRepository)
        {
            base._iBaseRepository = iInbillDRepository;
            _iInbillDRepository = iInbillDRepository;
        }

        public async Task<List<InbillDDTO>> QueryInBillDDTO(string inbillMId)
        {
            return await _iInbillDRepository.QueryInBillDDTO(inbillMId);
        }

        public async Task<List<InbillDDTO>> QueryInBillDDTO(List<string> inbillMIds)
        {
            return await _iInbillDRepository.QueryInBillDDTO(inbillMIds);
        }

    }
}
