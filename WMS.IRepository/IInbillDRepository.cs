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
        /// <summary>
        /// 入库单主档集合
        /// </summary>
        /// <param name="inbillMId">入库单Id</param>
        /// <returns></returns>
        public Task<List<InbillDDTO>> QueryInBillDDTO(string inbillMId);

        /// <summary>
        /// 入库单主档集合
        /// </summary>
        /// <param name="inbillMIds">入库单Id集合</param>
        /// <returns></returns>
        public Task<List<InbillDDTO>> QueryInBillDDTO(List<string> inbillMIds);
    }
}
