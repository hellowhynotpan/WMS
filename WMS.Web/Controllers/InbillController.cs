using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MS.WebApi.Common;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Utils.Vaild;
using WMS.IService;
using WMS.Model.Entity;
using WMS.Model.DTO;
using WMS.WebApi.Common;
using AutoMapper;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InbillController : ControllerBase
    {
        private readonly IInbillService _iInbillService;
        private readonly IInbillDService _iInbillDService;
        private readonly IInbillDSnService _iInbillDSnService;
        public InbillController(IInbillService iInbillService, IInbillDService iInbillDService,IInbillDSnService iInbillDSnService)
        {
            _iInbillService = iInbillService;
            _iInbillDService = iInbillDService;
            _iInbillDSnService= iInbillDSnService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResult> GetAll()
        {
            var data = await _iInbillService.QueryAsync();
            return ApiResultHelper.Success(data);
        }

        [HttpGet("QueryInbillById")]
        public async Task<ApiResult> QueryInbillById([FromServices] IMapper iMapper, [FromQuery] string id)
        {
            var data = await _iInbillService.FindAsync(id);
            if (data == null) return ApiResultHelper.Error("未找到入库单信息");
            var inbillDTO = iMapper.Map<InbillDTO>(data);
            inbillDTO.InbillDs= await _iInbillDService.QueryInBillDDTO(id);
            return ApiResultHelper.Success(inbillDTO);
        }

        [HttpPost("Edit")]
        public async Task<ApiResult> Edit([FromBody] InbillDTO inbillDTO)
        {
            if (inbillDTO.Status != 0) return ApiResultHelper.Error("不允许修改！");
            if (inbillDTO.InbillDs.Count == 0) return ApiResultHelper.Error("入库单明细不能为空！");
            foreach (var it in inbillDTO.InbillDs)
            {
                if (!Valid.IsPositiveInteger(it.InbillQty.ToString()))
                {
                    return ApiResultHelper.Error("入库数量必须为正整数");
                }
            }
            Inbill inbill = await _iInbillService.FindAsync(inbillDTO.Id);
            inbill.Status = inbillDTO.Status;
            if (string.IsNullOrEmpty(inbillDTO.InbillNo))
            {
                inbill.InbillNo = "Inbill" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            else
            {
                inbill.InbillNo = inbillDTO.InbillNo;
            }
            inbill.LastUpdTIME = DateTime.Now;
            inbill.LastUpdOwner = inbillDTO.LastUpdOwner;
            inbill.ErpCode = inbillDTO.ErpCode;
            inbill.Memo = inbillDTO.Memo;
            inbill.InbillType = inbillDTO.InbillType;
            List<InbillD> inbillDs = new List<InbillD>();
            List<InbillDSn> inbillDSns = new List<InbillDSn>();
            List<InbillD> nInbillDs= new List<InbillD>();
            List<InbillDSn> nInbillDSns = new List<InbillDSn>();
            foreach (var item in inbillDTO.InbillDs)
            {
                InbillD inbillD = await _iInbillDService.FindAsync(x => x.Id == item.Id && x.InbillMId == inbillDTO.Id);
                if (inbillD == null)
                {
                    inbillD = new InbillD
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        InbillMId = inbill.Id,
                        CsId = item.CsId,
                        InbillQty = item.InbillQty,
                        PartId = item.PartId,
                        LineNo = item.LineNo,
                        ErpCode = inbill.ErpCode,
                        ErpCodeLine = item.ErpCodeLine,
                        
                    };
                    nInbillDs.Add(inbillD);
                }
                else
                {
                    inbillD.CsId = item.CsId;
                    inbillD.InbillQty = item.InbillQty;
                    inbillD.PartId = item.PartId;
                    inbillD.LineNo = item.LineNo;
                    inbillD.ErpCode = inbill.ErpCode;
                    inbillD.ErpCodeLine = item.ErpCodeLine;
                    inbillDs.Add(inbillD);
                }
                InbillDSn inbillDSn = await _iInbillDSnService.FindAsync(x=>x.InbillDId==item.Id&&x.InbillMId== inbillDTO.Id);
                if (inbillDSn == null)
                {
                    inbillDSn = new InbillDSn
                    {
                        Id = Guid.NewGuid().ToString("N"),
                        InbillDId = inbillD.Id,
                        InbillMId = inbill.Id,
                        SnNo = item.SnNo,
                        SnDateCode = item.DateCode,
                        SnType= item.SnType,
                        PartId = item.PartId,
                        SnQty = item.SnQty,
                        PalletNo = item.PalletNo,
                        BatchNo = item.BatchNo,
                        CreateTime = DateTime.Now,
                        CreateOwner = inbillDTO.CreateOwner
                    };
                    nInbillDSns.Add(inbillDSn);
                }
                else
                {
                    inbillDSn.SnNo = item.SnNo;
                    inbillDSn.SnDateCode = item.DateCode;
                    inbillDSn.PartId = item.PartId;
                    inbillDSn.SnQty = item.SnQty;
                    inbillDSn.BatchNo = item.BatchNo;
                    inbillDSn.SnType = item.SnType;
                    inbillDSns.Add(inbillDSn);
                }
            }
            var data = await _iInbillService.EditInBill(inbill, inbillDs, inbillDSns, nInbillDs, nInbillDSns);
            if (!data) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create([FromBody] InbillDTO inbillDTO)
        {
            if (inbillDTO.InbillDs.Count == 0) return ApiResultHelper.Error("入库单明细不能为空");
            foreach (var it in inbillDTO.InbillDs)
            {
                if (!Valid.IsPositiveInteger(it.InbillQty.ToString()))
                {
                    return ApiResultHelper.Error("入库数量必须为正整数");
                }
            }
            Inbill inbill = new Inbill();
            inbill.Id = Guid.NewGuid().ToString("N");
            inbill.Status = 0;
            if (string.IsNullOrEmpty(inbillDTO.InbillNo))
            {
                inbill.InbillNo = "Inbill" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            else
                inbill.InbillNo = inbillDTO.InbillNo;
            inbill.CreateTime = DateTime.Now;
            inbill.CreateOwner = inbillDTO.CreateOwner;
            inbill.ErpCode = inbillDTO.ErpCode;
            inbill.Memo = inbillDTO.Memo;
            inbill.InbillType = inbillDTO.InbillType;
            List<InbillD> inbillDs = new List<InbillD>();
            List<InbillDSn> inbillDSns = new List<InbillDSn>();
            foreach (var item in inbillDTO.InbillDs)
            {
                InbillD inbillD = new InbillD
                {
                    Id = Guid.NewGuid().ToString("N"),
                    InbillMId = inbill.Id,
                    CsId = item.CsId,
                    InbillQty = item.InbillQty,
                    PartId = item.PartId,
                    LineNo = item.LineNo,
                    ErpCode = inbill.ErpCode,
                    ErpCodeLine = item.ErpCodeLine,
                };
                inbillDs.Add(inbillD);
                InbillDSn inbillDSn = new InbillDSn
                {
                    Id = Guid.NewGuid().ToString("N"),
                    InbillDId = inbillD.Id,
                    InbillMId = inbill.Id,
                    SnNo = item.SnNo,
                    SnDateCode = item.DateCode,
                    PartId = item.PartId,
                    PalletNo= item.PalletNo,
                    SnQty = item.SnQty,
                    SnType=item.SnType,
                    BatchNo = item.BatchNo,
                    CreateTime = DateTime.Now,
                    CreateOwner = inbillDTO.CreateOwner
                };
                inbillDSns.Add(inbillDSn);
            }
            var data = await _iInbillService.InBill(inbill,inbillDs,inbillDSns);
            if (!data) return ApiResultHelper.Error("新增失败");
            return ApiResultHelper.Success("新增成功");
        }

        [HttpGet("QueryPage")]
        public async Task<ApiResult> QueryPage([FromQuery] string Func, [FromQuery] int num, [FromQuery] string createOwner, [FromQuery] int inbillType, [FromQuery] int status)
        {
            Expression<Func<Inbill, bool>> func = u => true;
            if (!string.IsNullOrEmpty(Func))
            {
                func = ExpressionFuncExtender.And<Inbill>(func, x => x.InbillNo.ToLower().Contains(Func.Trim().ToLower())
                || x.ErpCode.ToLower().Contains(Func.Trim().ToLower()));
            }
            if (inbillType != 5)
            {
                func = ExpressionFuncExtender.And<Inbill>(func, x => x.InbillType == inbillType);
            }
            if (status != 4)
            {
                func = ExpressionFuncExtender.And<Inbill>(func, x => x.Status == status);
            }
            func = ExpressionFuncExtender.And<Inbill>(func, x => x.CreateOwner == createOwner);
            var data = await _iInbillService.QueryAsync(func, num, x => x.CreateTime);
            return ApiResultHelper.Success(data);
        }
    }
}
