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
using WMS.Model.DTO;
using WMS.Model.Entity;

namespace WMS.IRepository
{
    public interface IInbillDRepository : IBaseRepository<InbillD>
    {
        public Task<List<InbillDDTO>> QueryInBillDDTO(string inbillMId);
    }
}
