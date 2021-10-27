using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.IService;
using WMS.Model.DTO;
using WMS.WebApi.Common;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaiduFaceMController : ControllerBase
    {
        private readonly IBaiDuFaceMService _iBaiduFaceMService;

        public BaiduFaceMController(IBaiDuFaceMService iBaiDuFaceMService)
        {
            _iBaiduFaceMService = iBaiDuFaceMService;
        }

        [HttpPost("AddUserFace")]
        public ApiResult AddUserFace(FaceDTO face)
        {
            var b= _iBaiduFaceMService.AddFace(face);
            if (!b) return ApiResultHelper.Success("人脸注册失败");
            return ApiResultHelper.Success("人脸注册成功");
        }

    }
}
