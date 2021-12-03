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
using WMS.Model.DTO;
using WMS.Model.Entity;

namespace WMS.Repository
{
    public class InbillDRepository : BaseRepository<InbillD>, IInbillDRepository
    {
        public async Task<List<InbillDDTO>> QueryInBillDDTO(string inbillMId)
        {
            return await Context.Queryable<InbillD>()
            .LeftJoin<InbillDSn>((o, sn) => o.Id == sn.InbillDId)
            .LeftJoin<BaseCargospace>((o, sn,cs) => o.CsId == cs.Id)
            .LeftJoin<BasePart>((o,sn,cs, part) => o.PartId == part.Id)
            .Where(o => o.InbillMId == inbillMId)
            .Select((o, sn,cs,part) => new InbillDDTO
            {
                Id = o.Id,
                SnId=sn.Id,
                LineNo = o.LineNo,
                InbillMId = o.InbillMId,
                ErpCode = o.ErpCode,
                ErpCodeLine = o.ErpCodeLine,
                PartId = o.PartId,
                CsId = o.CsId,
                InbillQty = o.InbillQty,
                SnNo = sn.SnNo,
                DateCode = sn.DateCode,
                BatchNo = sn.BatchNo,
                CsName= cs.CsName,
                PartName= part.PartName
            })
            .ToListAsync();  
        }
    }
}
