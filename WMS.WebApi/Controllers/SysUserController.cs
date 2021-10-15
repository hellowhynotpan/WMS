using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.IService;
using WMS.WebApi.Common;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SysUserController : ControllerBase
    {
        private readonly ISysUserService _iUserService;

        public SysUserController(ISysUserService iSysUserService)
        {
            _iUserService = iSysUserService;

        }

        [HttpGet("Users")]
        public async Task<ApiResult> GetUser()
        {
            var data = await _iUserService.QueryAsync();
            return ApiResultHelper.Success(data);
        }
    }
}
