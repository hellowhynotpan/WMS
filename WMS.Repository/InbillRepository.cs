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
using WMS.Model.Entity;
using WMS.Model.DTO;

namespace WMS.Repository
{
    public class InbillRepository : BaseRepository<Inbill>, IInbillRepository
    {
        /// <summary>
        /// 入库单入库事务
        /// </summary>
        /// <param name="inbillDto">入库单DTO</param>
        /// <returns></returns>
        public async Task<bool> InBill(Inbill inbill,List<InbillD> inbillDs, List<InbillDSn> inbillDSns)
        {
            try
            {
                base.Context.Ado.BeginTran();
                await Context.Insertable(inbill).ExecuteCommandAsync();
                await Context.Insertable(inbillDs).ExecuteCommandAsync();
                await Context.Insertable(inbillDSns).ExecuteCommandAsync();
                Context.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 入库单修改事务
        /// </summary>
        /// <param name="inbillDto">入库单DTO</param>
        /// <returns></returns>
        public async Task<bool> EditInBill(Inbill inbill, List<InbillD> inbillDs, List<InbillDSn> inbillDSns, List<InbillD> nInbillDs, List<InbillDSn> nInbillDSns)
        {
            try
            {
                base.Context.Ado.BeginTran();
                await Context.Updateable(inbill).ExecuteCommandAsync();
                await Context.Updateable(inbillDs).ExecuteCommandAsync();
                await Context.Updateable(inbillDSns).ExecuteCommandAsync();
                await Context.Insertable(nInbillDs).ExecuteCommandAsync();
                await Context.Insertable(nInbillDSns).ExecuteCommandAsync();
                Context.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
