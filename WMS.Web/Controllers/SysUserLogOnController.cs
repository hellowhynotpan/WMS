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
    public class SysUserLogOnController : ControllerBase
    {
        private readonly ISysUserLogOnService _iSysUserLogOnService;

        public SysUserLogOnController(ISysUserLogOnService iSysUserLogOnService)
        {
            _iSysUserLogOnService = iSysUserLogOnService;

        }

        [HttpGet("GetAll")]
        public async Task<ApiResult> GetUser()
        {
            var data = await _iSysUserLogOnService.QueryAsync();
            return ApiResultHelper.Success(data);
        }

        [HttpGet("FindById")]
        public async Task<ApiResult> QueryUserById([FromQuery] string Id)
        {
            var data = await _iSysUserLogOnService.FindAsync(Id);
            if (data == null) return ApiResultHelper.Error("");
            return ApiResultHelper.Success(data);
        }

        [HttpGet("Edit")]
        public async Task<ApiResult> EditUser([FromBody]SysUserLogOn sysUserLogOn)
        {
            var data = await _iSysUserLogOnService.EditAsync(sysUserLogOn);
            if (data != 1) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }

        [HttpGet("Create")]
        public async Task<ApiResult> CreateUser([FromBody] SysUserLogOn sysUserLogOn)
        {
            var data = await _iSysUserLogOnService.CreateAsync(sysUserLogOn);
            if (data == null) return ApiResultHelper.Error("新增失败");
            return ApiResultHelper.Success("新增成功");
        }

        [HttpGet("QueryPage")]
        public async Task<ApiResult> Create([FromQuery] int page, [FromQuery] int size)
        {
            //总页数 异步引用类型
            RefAsync<int> total = 0;
            var data = await _iSysUserLogOnService.QueryAsync(page, size, total);
            return ApiResultHelper.Success(data, total);
        }
    }
}
