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
using WMS.Model;
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
            .ToListAsync();  //ViewOrder是一个新建的类，更多Select用法看下面文档
        }
    }
}
