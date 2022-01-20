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
        /// <summary>
        /// 根据入库单主档主键查询入库单集合
        /// </summary>
        /// <param name="inbillMId">入库单主档主键</param>
        /// <returns>入库单集合</returns>
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
                WhId=cs.WhId,
                SnType=sn.SnType,
                DateCode = sn.SnDateCode,
                BatchNo = sn.BatchNo,
                PalletNo = sn.PalletNo,
                SnQty=sn.SnQty,
                CsName = cs.CsName,
                PartName = part.PartName
            })
            .ToListAsync();  
        }

        /// <summary>
        /// 根据入库单id主键集合去查询入库单集合
        /// </summary>
        /// <param name="inbillMIds">入库单主档主键集合</param>
        /// <returns>入库单集合</returns>
        public async Task<List<InbillDDTO>> QueryInBillDDTO(List<string> inbillMIds)
        {
            return await Context.Queryable<InbillD>()
            .LeftJoin<InbillDSn>((o, sn) => o.Id == sn.InbillDId)
            .LeftJoin<BaseCargospace>((o, sn, cs) => o.CsId == cs.Id)
            .LeftJoin<BasePart>((o, sn, cs, part) => o.PartId == part.Id)
            .Where(o => inbillMIds.Contains(o.Id))
            .Select((o, sn, cs, part) => new InbillDDTO
            {
                Id = o.Id,
                SnId = sn.Id,
                LineNo = o.LineNo,
                InbillMId = o.InbillMId,
                ErpCode = o.ErpCode,
                ErpCodeLine = o.ErpCodeLine,
                PartId = o.PartId,
                CsId = o.CsId,
                WhId=cs.WhId,
                InbillQty = o.InbillQty,
                SnNo = sn.SnNo,
                SnType = sn.SnType,
                DateCode = sn.SnDateCode,
                BatchNo = sn.BatchNo,
                PalletNo = sn.PalletNo,
                SnQty = sn.SnQty,
                CsName = cs.CsName,
                PartName = part.PartName
            })
            .ToListAsync();
        }
    }
}
