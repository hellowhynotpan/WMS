using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.IService;
using WMS.Model;
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

        [HttpGet("FindById")]
        public async Task<ApiResult> QueryUserById([FromQuery] string id)
        {
            var data = await _iBaseWareHouseService.FindAsync(id);
            if (data == null) return ApiResultHelper.Error("查询失败");
            return ApiResultHelper.Success(data);
        }

        [HttpGet("Edit")]
        public async Task<ApiResult> EditUser([FromBody] BaseWareHouse baseWareHouse)
        {
            var data = await _iBaseWareHouseService.EditAsync(baseWareHouse);
            if (data != 1) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }

        [HttpGet("Create")]
        public async Task<ApiResult> CreateUser([FromBody]BaseWareHouse baseWareHouse)
        {
            var data = await _iBaseWareHouseService.CreateAsync(baseWareHouse);
            if (data == null) return ApiResultHelper.Error("新增失败");
            return ApiResultHelper.Success("新增成功");
        }

        [HttpGet("QueryPage")]
        public async Task<ApiResult> Create([FromQuery] int page, [FromQuery] int size)
        {
            //总页数 异步引用类型
            RefAsync<int> total = 0;
            var data = await _iBaseWareHouseService.QueryAsync(page, size, total);
            return ApiResultHelper.Success(data, total);
        }
    }
}
