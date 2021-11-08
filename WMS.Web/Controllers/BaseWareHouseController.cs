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
using WMS.IService;
using WMS.Model;
using WMS.Model.DTO;
using WMS.WebApi.Common;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BaseWareHouseController : ControllerBase
    {
        private readonly IBaseWareHouseService _iBaseWareHouseService;

        public BaseWareHouseController(IBaseWareHouseService iBaseWareHouseService)
        {
            _iBaseWareHouseService = iBaseWareHouseService;

        }

        [HttpGet("GetAll")]
        public async Task<ApiResult> GetUser()
        {
            var data = await _iBaseWareHouseService.QueryAsync();
            return ApiResultHelper.Success(data);
        }

        [HttpGet("Find")]
        public async Task<ApiResult> QueryUserById([FromQuery] string id)
        {
            var data = await _iBaseWareHouseService.FindAsync(x=>x.Id==id);
            if (data == null) return ApiResultHelper.Error("未查找到该记录");
            return ApiResultHelper.Success(data);
        }

        [HttpPost("Edit")]
        public async Task<ApiResult> EditUser([FromBody] BaseWareHouse baseWareHouse)
        {
            var data = await _iBaseWareHouseService.EditAsync(baseWareHouse);
            if (data != 1) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }

        [HttpPost("Create")]
        public async Task<ApiResult> Create(BaseWareHouse baseWareHouse)
        {
            var wh = await _iBaseWareHouseService.FindAsync(x => x.WhNo == baseWareHouse.WhNo);
            if(wh!=null) return ApiResultHelper.Error("仓库编号重复");
            baseWareHouse.Id = Guid.NewGuid().ToString("N");
            if (string.IsNullOrEmpty(baseWareHouse.WhNo))
            {
                baseWareHouse.WhNo= "WH"+DateTime.Now.ToString("yyyyMMddHHmmss");
            }
            baseWareHouse.Status = true;
            baseWareHouse.CreateTime = DateTime.Now;
            var data = await _iBaseWareHouseService.CreateAsync(baseWareHouse);
            if (data == null) return ApiResultHelper.Error("新增失败");
            return ApiResultHelper.Success("新增成功");
        }

        [HttpGet("QueryPage")]
        public async Task<ApiResult> Create([FromQuery] string Func, [FromQuery] int num, [FromQuery] string createOwner)
        {
            Expression<Func<BaseWareHouse, bool>> func = u => true;
            if (!string.IsNullOrEmpty(Func))
            {
                func = ExpressionFuncExtender.And<BaseWareHouse>(func, x => x.WhName.ToLower().Contains(Func.Trim().ToLower())
                ||x.WhNo.ToLower().Contains(Func.Trim().ToLower()));
            }
            func = ExpressionFuncExtender.And<BaseWareHouse>(func, x => x.CreateOwner== createOwner&&x.Status==true);
            var data = await _iBaseWareHouseService.QueryAsync(func, num, x=>x.CreateTime);
            return ApiResultHelper.Success(data);
        }

        [HttpGet("AsInvalid")]
        public async Task<ApiResult> AsInvalid([FromQuery] string id, [FromQuery] string invalidOwner)
        {
            var data = await _iBaseWareHouseService.FindAsync(x=>x.Id== id.Trim());
            if (data == null) return ApiResultHelper.Error("仓库不存在");
            data.InvalidOwner = invalidOwner;
            data.InvalidTime = DateTime.Now;
            data.Status = false;
            int num = await _iBaseWareHouseService.EditAsync(data);
            if(num!=1) return ApiResultHelper.Error("作废失败");
            return ApiResultHelper.Success(data);
        }
    }
}
