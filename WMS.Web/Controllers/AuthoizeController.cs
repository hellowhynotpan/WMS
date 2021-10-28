using AutoMapper;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WMS.IService;
using WMS.Model;
using WMS.Model.DTO;
using WMS.WebApi.Common;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : ControllerBase
    {
        private readonly ISysUserService _iSysUserService;
        private readonly ISysUserLogOnService _iSysUserLogOnService;
        private readonly IBaiDuFaceMService _iBaiduFaceMService;
        private IMemoryCache _cache;

        public AuthoizeController(ISysUserService iSysUserService, ISysUserLogOnService iSysUserLogOnService, IMemoryCache cache, IBaiDuFaceMService iBaiDuFaceMService)
        {
            _iSysUserService = iSysUserService;
            _iSysUserLogOnService = iSysUserLogOnService;
            _cache = cache;
            _iBaiduFaceMService = iBaiDuFaceMService;
        }

        [HttpPost("LoginByPwd")]
        public async Task<ApiResult> LoginByPwd([FromServices] IMapper iMapper, [FromBody] UserDTO loginDto)
        {
            var _user = await _iSysUserService.FindAsync(x => x.Account == loginDto.Account || x.Email == loginDto.Account || x.MobilePhone == loginDto.Account);
            if (_user == null) return ApiResultHelper.Error("账号不存在");
            if (_user.EnabledMark == false) return ApiResultHelper.Error("账号已注销,请联系管理员");
            var userLogOn = await _iSysUserLogOnService.FindAsync(x => x.UserId == _user.Id);
            string pwd = MD5Helper.MD5Encrypt32(loginDto.Password);
            if (userLogOn.Password != pwd) return ApiResultHelper.Error("账号或密码不正确");
            var data = iMapper.Map<LoginRsDTO>(_user);
            data.Token = JwtTools.GetToken(_user);
            return ApiResultHelper.Success(data);
        }

        [HttpPost("LoginBySms")]
        public async Task<ApiResult> LoginBySms([FromServices] IMapper iMapper, [FromBody] UserDTO loginDto)
        {
            var _user = await _iSysUserService.FindAsync(x => x.MobilePhone == loginDto.MobilePhone);
            if (_user == null) return ApiResultHelper.Error("该手机号未注册");
            var cachedMsgCaptcha = _cache.Get<UserDTO>(loginDto.MobilePhone);
            if (cachedMsgCaptcha == null)
            {
                return ApiResultHelper.Error("短信验证码无效，请重新获取");
            }
            if (cachedMsgCaptcha.ValidateCount >= 3)
            {
                _cache.Remove(loginDto.MobilePhone);
                return ApiResultHelper.Error("短信验证码失效，请重新获取");
            }
            cachedMsgCaptcha.ValidateCount++;
            if (!string.Equals(cachedMsgCaptcha.SmsCode, loginDto.SmsCode, StringComparison.OrdinalIgnoreCase))
            {
                return ApiResultHelper.Error("验证码错误");
            }
            _cache.Remove(loginDto.MobilePhone);
            var data = iMapper.Map<LoginRsDTO>(_user);
            data.Token = JwtTools.GetToken(_user);
            return ApiResultHelper.Success(data);
        }

        [HttpPost("LoginByFace")]
        public async Task<ApiResult> FaceLogin([FromServices] IMapper iMapper, [FromBody] FaceDTO face)
        {
            var user_id = _iBaiduFaceMService.SearchFace(face);
            if (user_id == -1) return ApiResultHelper.Error("未找到人脸信息");
            var _user = await _iSysUserService.FindAsync(x => x.Id == user_id);
            if (_user == null) return ApiResultHelper.Error("该用户已删除");
            var data = iMapper.Map<LoginRsDTO>(_user);
            data.Token = JwtTools.GetToken(_user);
            return ApiResultHelper.Success(data);
        }

        [HttpGet("SendSmsByLogin")]
        public async Task<ApiResult> SendSmsByLogin([FromQuery] string mobilePhone)
        {
            var _user = await _iSysUserService.FindAsync(x => x.MobilePhone == mobilePhone);
            if (_user == null) return ApiResultHelper.Error("该手机号未注册");
            var cachedMsgCaptcha = _cache.Get<UserDTO>(mobilePhone);
            if (cachedMsgCaptcha != null)
            {
                var offsetSecionds = (DateTime.Now - cachedMsgCaptcha.CreateTime).Seconds;
                if (offsetSecionds < 60)
                {
                    return ApiResultHelper.Error("短信验证码获取太频繁，请稍后重试");
                }
            }
            UserDTO loginDto = new UserDTO();
            loginDto.SmsCode = ALiSMSHelper.CreateRandomNumber(6);
            loginDto.CreateTime = DateTime.Now;
            loginDto.ValidateCount = 0;
            loginDto.MobilePhone = mobilePhone;
            _cache.Set(loginDto.MobilePhone, loginDto, TimeSpan.FromMinutes(2));
            //调用第三方SDK实际发送短信
            //.....
            return ApiResultHelper.Success("发送成功" + loginDto.SmsCode);
        }


        [HttpGet("SendSmsByRegister")]
        public async Task<ApiResult> SendSmsByRegister([FromQuery] string mobilePhone)
        {
            var _user = await _iSysUserService.FindAsync(x => x.MobilePhone == mobilePhone);
            if (_user != null) return ApiResultHelper.Error("该手机号已注册");
            var cachedMsgCaptcha = _cache.Get<UserDTO>(mobilePhone);
            if (cachedMsgCaptcha != null)
            {
                var offsetSecionds = (DateTime.Now - cachedMsgCaptcha.CreateTime).Seconds;
                if (offsetSecionds < 60)
                {
                    return ApiResultHelper.Error("短信验证码获取太频繁，请稍后重试");
                }
            }
            UserDTO loginDto = new UserDTO();
            loginDto.SmsCode = ALiSMSHelper.CreateRandomNumber(6);
            loginDto.CreateTime = DateTime.Now;
            loginDto.ValidateCount = 0;
            loginDto.MobilePhone = mobilePhone;
            _cache.Set(loginDto.MobilePhone, loginDto, TimeSpan.FromMinutes(2));
            //调用第三方SDK实际发送短信
            //.....
            return ApiResultHelper.Success("发送成功" + loginDto.SmsCode);
        }

        [HttpPost("Register")]
        public async Task<ApiResult> Register([FromServices] IMapper iMapper, [FromBody] UserDTO registerDto)
        {
            var _user = await _iSysUserService.FindAsync(x => x.MobilePhone == registerDto.MobilePhone);
            if (_user != null) return ApiResultHelper.Error("该手机号已注册,请更换手机号");
            _user = await _iSysUserService.FindAsync(x => x.Account == registerDto.Account);
            if (_user != null) return ApiResultHelper.Error("该用户名已注册");
            var cachedMsgCaptcha = _cache.Get<UserDTO>(registerDto.MobilePhone);
            if (cachedMsgCaptcha == null || cachedMsgCaptcha.ValidateCount >= 3)
            {
                _cache.Remove(registerDto.MobilePhone);
                return ApiResultHelper.Error("短信验证码无效，请重新获取");
            }
            cachedMsgCaptcha.ValidateCount++;
            if (!string.Equals(cachedMsgCaptcha.SmsCode, registerDto.SmsCode, StringComparison.OrdinalIgnoreCase))
            {
                return ApiResultHelper.Error("验证码错误");
            }
            var sysUser = iMapper.Map<SysUser>(registerDto);
            sysUser.IsAdministrator = true;
            sysUser.CreateOwner = sysUser.Account;
            sysUser.CreateTime = DateTime.Now;
            var data = await _iSysUserService.CreateAsync(sysUser);
            if (data == null) return ApiResultHelper.Error("新增失败");
            SysUserLogOn sysUserLogOn = new SysUserLogOn();
            sysUserLogOn.Password = MD5Helper.MD5Encrypt32(registerDto.Password);
            sysUserLogOn.UserId = data.Id;
            sysUserLogOn.FirstVisitTime = DateTime.Now;
            var data2 = await _iSysUserLogOnService.CreateAsync(sysUserLogOn);
            if (data2 == null) return ApiResultHelper.Error("新增失败");
            var loginRs = iMapper.Map<LoginRsDTO>(data);
            loginRs.Token = JwtTools.GetToken(data);
            return ApiResultHelper.Success(loginRs);
        }

        [HttpGet("Test")]
        public async Task<ApiResult> Test()
        {

        }
    }
}
