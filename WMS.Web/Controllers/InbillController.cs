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
using WMS.Model;
using WMS.Model.DTO;
using WMS.WebApi.Common;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InbillController : ControllerBase
    {
        private readonly IInbillService _iInbillService;
        private readonly IStockMService _iStockMService;
        private readonly IInbillDService _iInbillDService;
        private readonly IBaseWareHouseService _iBaseWareHouseService;
        private readonly IBaseCargospaceService _iBaseCargospaceService;
        private readonly IStockDService _iStockDService;
        private readonly IBasePartService _iBasePartService;
        public InbillController(IBaseCargospaceService iBaseCargospaceService, IBaseWareHouseService iBaseWareHouseService,IInbillService iInbillService, IInbillDService iInbillDService, IStockMService iStockMService
            ,IStockDService iStockDService, IBasePartService iBasePartService)
        {
            _iInbillService = iInbillService;
            _iInbillDService = iInbillDService;
            _iStockMService = iStockMService;
            _iBaseWareHouseService = iBaseWareHouseService;
            _iBaseCargospaceService = iBaseCargospaceService;
            _iStockDService = iStockDService;
            _iBasePartService = iBasePartService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResult> GetUser()
        {
            var data = await _iInbillService.QueryAsync();
            return ApiResultHelper.Success(data);
        }

        [HttpGet("FindById")]
        public async Task<ApiResult> QueryUserById([FromQuery] string id)
        {
            var data = await _iInbillService.FindAsync(id);
            if (data == null) return ApiResultHelper.Error("查询失败");
            return ApiResultHelper.Success(data);
        }

        [HttpGet("Edit")]
        public async Task<ApiResult> EditUser([FromBody] Inbill inbill)
        {
            var data = await _iInbillService.EditAsync(inbill);
            if (data != 1) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create([FromBody] InbillDTO inbillDto)
        {
            if (inbillDto.InbillDs.Count == 0) return ApiResultHelper.Error("入库单明细不能为空");
            foreach (var it in inbillDto.InbillDs)
            {
                if (!Valid.IsPositiveInteger(it.InbillQty.ToString()))
                {
                    return ApiResultHelper.Error("入库数量必须为正整数");
                }
            }
            Inbill inbill = new Inbill();
            inbill.Id=Guid.NewGuid().ToString("N");
            inbill.Status = 0;
            if (string.IsNullOrEmpty(inbillDto.InbillNo))
            {
                inbill.InbillNo = "Inbill" + DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            else
                inbill.InbillNo = inbillDto.InbillNo;
            inbill.CreateTime = DateTime.Now;
            inbill.CreateOwner = inbillDto.CreateOwner;
            inbill.ErpCode = inbillDto.ErpCode;
            inbill.Memo = inbillDto.Memo;
            inbill.InbillType = inbillDto.InbillType;
            var data = await _iInbillService.CreateAsync(inbill);
            if (data == null) return ApiResultHelper.Error("新增失败");
            foreach (var item in inbillDto.InbillDs)
            {
                var a = await _iStockMService.FindAsync(x => x.CsId == item.CsId && x.PartId == item.PartId&& x.CreateOwner == inbillDto.CreateOwner);
                if (a != null)
                {
                    a.StockQty = a.StockQty + item.InbillQty;
                    a.LastUpdOwner = inbill.CreateOwner;
                    a.LastUpdTIME = DateTime.Now;
                    var b1=  await _iStockMService.EditAsync(a);
                }
                else 
                {
                    StockM stockM = new StockM();
                    var b1 = await _iBaseCargospaceService.FindAsync(x=>x.Id==item.CsId);
                    stockM.Id= Guid.NewGuid().ToString("N");
                    stockM.WhId = b1.WhId;
                    stockM.CsId = item.CsId;
                    stockM.PartId = item.PartId;
                    stockM.CreateOwner = inbill.CreateOwner;
                    stockM.CreateTime = DateTime.Now;
                    stockM.StockQty = item.InbillQty;
                    await _iStockMService.CreateAsync(stockM);
                }
                StockD stockD = new StockD();
                stockD.Id = Guid.NewGuid().ToString("N");
                stockD.StockMId = a.Id;
                stockD.SnNo = item.SnNo;
                stockD.SnType = item.SnType;
                stockD.SnQty = item.SnQty;
                stockD.SnDateCode = item.SnDateCode;
                stockD.BatchNo = item.BatchNo;
                stockD.PalletNo = item.PalletNo;
                await _iStockDService.CreateAsync(stockD);
                InbillD inbillD = new InbillD();
                inbillD.Id= Guid.NewGuid().ToString("N");
                inbillD.InbillMId = inbill.Id;
                inbillD.CsId = item.CsId;
                inbillD.InbillQty = item.InbillQty;
                inbillD.PartId = item.PartId;
                inbillD.LineNo = item.LineNo;
                inbillD.ErpCode = inbill.ErpCode;
                await _iInbillDService.CreateAsync(inbillD);
            }
            //库存
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
            if (inbillType!=5)
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
