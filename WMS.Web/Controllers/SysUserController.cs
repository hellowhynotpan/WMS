using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.IService;
using WMS.WebApi.Common;
using WMS.Model.Entity;
using SqlSugar;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using WMS.Model.DTO;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SysUserController : ControllerBase
    {
        private readonly ISysUserService _iSysUserService;
        private readonly ISysUserLogOnService _iSysUserLogOnService;
        private readonly IBaiDuFaceMService _iBaiduFaceMService;

        public SysUserController(ISysUserService iSysUserService, ISysUserLogOnService iSysUserLogOnService, IBaiDuFaceMService iBaiDuFaceMService)
        {
            _iSysUserService = iSysUserService;
            _iSysUserLogOnService = iSysUserLogOnService;
            _iBaiduFaceMService = iBaiDuFaceMService;
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
        public async Task<ApiResult> CreateUser([FromBody]SysUser sysUser)
        {
            var data = await _iSysUserService.CreateAsync(sysUser);
            if (data ==null) return ApiResultHelper.Error("新增失败");
            SysUserLogOn sysUserLogOn = new SysUserLogOn();
            sysUserLogOn.Password= MD5Helper.MD5Encrypt32("123456");
            sysUserLogOn.UserId = data.Id;
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

        [HttpPost("AddUserFace")]
        public async Task<ApiResult> AddUserFace([FromBody]FaceDTO face)
        {
            var _user = await _iSysUserService.FindAsync(face.UserId);
            if (_user == null) return ApiResultHelper.Error("人脸注册/更新失败,账号不存在");
            string faceToken;
            if (_user.FaceId != null)
            {
                //存在人脸信息则更新
                faceToken=_iBaiduFaceMService.UpdFace(face);
            }
            else
            {
                //不存在人脸信息则新增
                faceToken = _iBaiduFaceMService.AddFace(face);
            }
            if (string.IsNullOrEmpty(faceToken)) return ApiResultHelper.Success("人脸注册/更新失败");
            _user.FaceId = faceToken;
            var b =await  _iSysUserService.EditAsync(_user);
            if(b!=1) return ApiResultHelper.Error("人脸注册/更新失败");
            return ApiResultHelper.Success("人脸注册/更新成功");
        }
    }
}
