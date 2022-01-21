
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.IService;
using WMS.WebApi.Common;
using System.Linq.Expressions;
using MS.WebApi.Common;
using WMS.Model.DTO;
using WMS.Model.Entity;
using AutoMapper;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMController : ControllerBase
    {
        private readonly IStockMService _iStockMService;
        private readonly IInbillService _iInbillService;
        private readonly IInbillDService _iInbillDService;
        private readonly IInbillDSnService _iInbillDSnService;
        private readonly IBasePartService _iBasePartService;
        private readonly IBaseCargospaceService _iBaseCargospaceService;
        private readonly IStockDService _iStockDService;

        public StockMController(IStockDService iStockDService,IBasePartService iBasePartService,IBaseCargospaceService iBaseCargospaceService,IStockMService iStockMService, IInbillService iInbillService, IInbillDService iInbillDService, IInbillDSnService iInbillDSnService)
        {
            _iStockMService = iStockMService;
            _iInbillService = iInbillService;
            _iInbillDService = iInbillDService;
            _iBaseCargospaceService = iBaseCargospaceService;
            _iInbillDSnService = iInbillDSnService;
            _iBasePartService = iBasePartService;
            _iStockDService = iStockDService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResult> GetUser()
        {
            var data = await _iStockMService.QueryAsync();
            return ApiResultHelper.Success(data);
        }

        [HttpGet("QuertStockMById")]
        public async Task<ApiResult> FindById([FromQuery] string id, [FromQuery] string func)
        {
            Expression<Func<StockD, bool>> funcStock = u => true;
            funcStock = ExpressionFuncExtender.And<StockD>(funcStock, x =>x.StockMId == id);
            if (!string.IsNullOrEmpty(func))
            {
                funcStock = ExpressionFuncExtender.And<StockD>(funcStock, x=> x.SnNo.ToLower().Contains(func.ToLower())||x.PalletNo.ToLower().Contains(func.ToLower()) || x.BatchNo.ToLower().Contains(func.ToLower()));
            }
            var data = await _iStockDService.QueryAsync(funcStock);
            return ApiResultHelper.Success(data);
        }

        [HttpGet("Edit")]
        public async Task<ApiResult> EditUser([FromBody]StockM stockM)
        {
            var data = await _iStockMService.EditAsync(stockM);
            if (data != 1) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }

        /// <summary>
        /// 扣账接口
        /// </summary>
        /// <param name="iMapper"></param>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        [HttpPost("Examine")]
        public async Task<ApiResult> Examine([FromServices] IMapper iMapper ,[FromBody] ApiRequest<string> apiRequest)
        {
            var data = await _iInbillService.FindAsync(apiRequest.Data);
            if (data == null) return ApiResultHelper.Error("未找到入库单信息");
            if (data.Status != 0) return ApiResultHelper.Error("入库单状态不正确");
            var inbillsDTO = iMapper.Map<InbillDTO>(data);
            var inbillDs = await _iInbillDService.QueryAsync(x => x.InbillMId == data.Id);
            foreach (var item in inbillDs)
            {
                var c = await _iBaseCargospaceService.FindAsync(item.CsId);
                if (c.Status != 0) return ApiResultHelper.Error("明细中有储位状态异常，无法扣账");
                var p = await _iBasePartService.FindAsync(item.PartId);
                if (c.Status != 0) return ApiResultHelper.Error("明细中有物料状态异常，无法扣账");
            }
            inbillsDTO.InbillDs = await _iInbillDService.QueryInBillDDTO(inbillsDTO.Id);
            foreach (var item in inbillsDTO.InbillDs)
            {
                var stockM = await _iStockMService.FindAsync(x => x.WhId == item.WhId && x.CsId == item.CsId && x.PartId == item.PartId);
                bool flag = false;
                if (stockM == null)
                {
                    stockM = new StockM();
                    stockM.Id = Guid.NewGuid().ToString("N");
                    stockM.CsId = item.CsId;
                    stockM.WhId = item.WhId;
                    stockM.PartId = item.PartId;
                    stockM.StockQty = item.InbillQty;
                    stockM.CreateTime = DateTime.Now;
                    stockM.CreateOwner = apiRequest.User;
                }
                else
                {
                    stockM.LastUpdOwner = apiRequest.User;
                    stockM.LastUpdTIME = DateTime.Now;
                    stockM.StockQty += item.InbillQty;
                    flag = true;
                }
                var stockD = new StockD
                {
                    Id = Guid.NewGuid().ToString("N"),
                    StockMId = stockM.Id,
                    SnNo = item.SnNo,
                    SnType = item.SnType,
                    DateCode = item.DateCode,
                    BatchNo = item.BatchNo,
                    PalletNo = item.PalletNo,
                    SnQty = item.SnQty,
                };
                if (flag)
                    await _iStockMService.UpdStockM(stockM, stockD);
                else
                    await _iStockMService.AddStockM(stockM, stockD);
                data.Status = 2;
                data.LastUpdOwner = apiRequest.User;
                data.LastUpdTIME = DateTime.Now;
                await _iInbillService.EditAsync(data);
            }
            return ApiResultHelper.Success(true);
        }

        [HttpGet("QueryPage")]
        public async Task<ApiResult> QueryPage([FromQuery] int partType,[FromQuery] string whId,[FromQuery]string func, [FromQuery] int num, [FromQuery] string createOwner, [FromQuery] string csNo, [FromQuery] string partNo, [FromQuery] string partName, [FromQuery] string partSpec, bool isInAbleNone)
        {
            var data = await _iStockMService.QueryStock(createOwner);
            Expression<Func<StockDTO, bool>> funcStock = u => true;
            if (!string.IsNullOrEmpty(partNo))
            {
                funcStock = ExpressionFuncExtender.And<StockDTO>(funcStock, x => x.PartNo == partNo);
            }
            if (!string.IsNullOrEmpty(partName))
            {
                funcStock = ExpressionFuncExtender.And<StockDTO>(funcStock, x => x.PartName == partName);
            }
            if (!string.IsNullOrEmpty(partSpec))
            {
                funcStock = ExpressionFuncExtender.And<StockDTO>(funcStock, x => x.PartSpec == partSpec);
            }
            if (!string.IsNullOrEmpty(whId))
            {
                funcStock = ExpressionFuncExtender.And<StockDTO>(funcStock, x => x.WhId == whId);
            }
            if (!string.IsNullOrEmpty(csNo))
            {
                funcStock = ExpressionFuncExtender.And<StockDTO>(funcStock, x => x.CsNo == csNo);
            }
            if (!isInAbleNone)
            {
                funcStock = ExpressionFuncExtender.And<StockDTO>(funcStock, x => x.StockQty != 0);
            }
            if (partType!=4)
            {
                funcStock = ExpressionFuncExtender.And<StockDTO>(funcStock, x => x.PartType == partType);
            }
            if (!string.IsNullOrEmpty(func))
            {
                funcStock = ExpressionFuncExtender.And<StockDTO>(funcStock, x => x.WhName.ToLower().Contains(func)|| x.CsName.ToLower().Contains(func.ToLower()) || x.CsNo.ToLower().Contains(func.ToLower()) || x.PartName.ToLower().Contains(func.ToLower()) || x.PartSpec.ToLower().Contains(func.ToLower()) || x.PartNo.ToLower().Contains(func.ToLower()));
            }
           Delegate d = funcStock.Compile();
           Func<StockDTO,bool> f = (Func<StockDTO,bool>)d;
           data = data.Where(f).Take(num).ToList();
           return ApiResultHelper.Success(data);
        }
    }
}
