using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.IService;
using WMS.WebApi.Common;
using WMS.Model;
using Microsoft.AspNetCore.Authorization;
using System.Linq.Expressions;
using MS.WebApi.Common;
using WMS.Model.DTO;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockMController : ControllerBase
    {
        private readonly IStockMService _iStockMService;

        public StockMController(IStockMService iStockMService)
        {
            _iStockMService = iStockMService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResult> GetUser()
        {
            var data = await _iStockMService.QueryAsync();
            return ApiResultHelper.Success(data);
        }

        [HttpGet("FindById")]
        public async Task<ApiResult> QueryUserById([FromQuery] string id)
        {
            var data = await _iStockMService.FindAsync(id);
            if (data == null) return ApiResultHelper.Error("查询失败");
            return ApiResultHelper.Success(data);
        }

        [HttpGet("Edit")]
        public async Task<ApiResult> EditUser([FromBody]StockM stockM)
        {
            var data = await _iStockMService.EditAsync(stockM);
            if (data != 1) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }

        [HttpGet("Create")]
        public async Task<ApiResult> CreateUser([FromBody]StockM stockM)
        {
            var data = await _iStockMService.CreateAsync(stockM);
            if (data == null) return ApiResultHelper.Error("新增失败");
            return ApiResultHelper.Success("新增成功");
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
