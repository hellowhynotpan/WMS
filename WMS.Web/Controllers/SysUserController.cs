using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.IService;
using WMS.WebApi.Common;
using WMS.Model;
using SqlSugar;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SysUserController : ControllerBase
    {
        private readonly ISysUserService _iSysUserService;
        private readonly ISysUserLogOnService _iSysUserLogOnService;
        public SysUserController(ISysUserService iSysUserService, ISysUserLogOnService iSysUserLogOnService)
        {
            _iSysUserService = iSysUserService;
            _iSysUserLogOnService = iSysUserLogOnService;
        }

        [HttpGet("GetAll")]
        public async Task<ApiResult> GetUser()
        {
            var data = await _iSysUserService.QueryAsync();
            return ApiResultHelper.Success(data);
        }

        [HttpGet("FindById")]
        public async Task<ApiResult> QueryUserById([FromQuery] string Id)
        {
            var data = await _iSysUserService.FindAsync(Id);
            if (data == null) return ApiResultHelper.Error("");
            return ApiResultHelper.Success(data);
        }

        [HttpGet("Edit")]
        public async Task<ApiResult> EditUser([FromBody]SysUser SysUser)
        {
            var data = await _iSysUserService.EditAsync(SysUser);
            if (data!=1) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }

        [HttpPost("Create")]
        public async Task<ApiResult> CreateUser( [FromBody]SysUser sysUser)
        {
            sysUser.Id = System.Guid.NewGuid().ToString();
            var data = await _iSysUserService.CreateAsync(sysUser);
            if (data ==null) return ApiResultHelper.Error("新增失败");

            SysUserLogOn sysUserLogOn = new SysUserLogOn();
            sysUserLogOn.Password= MD5Helper.MD5Encrypt32("123456");
            sysUserLogOn.UserId = data.Id;
            sysUserLogOn.Id = System.Guid.NewGuid().ToString();
            sysUserLogOn.FirstVisitTime = DateTime.Now;
            var data2 = await _iSysUserLogOnService.CreateAsync(sysUserLogOn);
            if (data2 == null) return ApiResultHelper.Error("新增失败");
            return ApiResultHelper.Success("新增成功");
        }

        [HttpGet("QueryPage")]
        public async Task<ApiResult> Create([FromQuery] int page, [FromQuery] int size)
        {
            //总页数 异步引用类型
            RefAsync<int> total = 0;
            var data = await _iSysUserService.QueryAsync(page, size,total);
            return ApiResultHelper.Success(data, total);
        }
    }
}
