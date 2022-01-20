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
    public class StockMRepository : BaseRepository<StockM>, IStockMRepository
    {
        public async Task<List<StockDTO>> QueryStock(string createowner)
        {
            return await Context.Queryable<StockM>()
            .LeftJoin<BasePart>((o, part) => o.PartId == part.Id)
            .LeftJoin<BaseCargospace>((o, part, cs) => o.CsId == cs.Id)
            .LeftJoin<BaseWareHouse>((o, part, cs, wh) => o.WhId == wh.Id)
            .Where(o => o.CreateOwner == createowner)
            .Select((o, part, cs, wh) => new StockDTO
            {
                Id = o.Id,
                WhId = o.WhId,
                CsId = o.CsId,
                PartId = o.CsId,
                StockQty = o.StockQty,
                WhName = wh.WhName,
                PartSpec = part.PartSpec,
                PartNo = part.PartNo,
                PartType=part.PartType,
                PartName = part.PartName,
                CsNo = cs.CsNo,
                CsName = cs.CsName
            })
            .ToListAsync();
        }

        public async Task<bool> AddStockM(StockM stockM,StockD stockD)
        {
            try
            {
                Context.Ado.BeginTran();
                await Context.Insertable(stockM).ExecuteCommandAsync();
                await Context.Insertable(stockD).ExecuteCommandAsync();
                Context.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                Context.Ado.RollbackTran();
                throw ex;
            }
        }

        public async Task<bool> UpdStockM(StockM stockM,StockD stockD)
        {
            try
            {
                Context.Ado.BeginTran();
                await Context.Updateable(stockM).ExecuteCommandAsync();
                await Context.Insertable(stockD).ExecuteCommandAsync();
                Context.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                Context.Ado.RollbackTran();
                throw ex;
            }
        }
    }
}
