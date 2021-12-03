using AutoMapper;
using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utils.Vaild;
using WMS.IService;
using WMS.Model.Entity;
using WMS.Model.DTO;
using WMS.WebApi.Common;

namespace WMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthoizeController : BaseController
    {
        private readonly ISysUserService _iSysUserService;
        private readonly ISysUserLogOnService _iSysUserLogOnService;
        private readonly IBaiDuFaceMService _iBaiduFaceMService;
       

        public AuthoizeController(ISysUserService iSysUserService, ISysUserLogOnService iSysUserLogOnService,  IBaiDuFaceMService iBaiDuFaceMService)
        {
            _iSysUserService = iSysUserService;
            _iSysUserLogOnService = iSysUserLogOnService;
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
            var cachedMsgCaptcha = GetMemoryCache.Get<UserDTO>(loginDto.MobilePhone);
            if (cachedMsgCaptcha == null)
            {
                return ApiResultHelper.Error("短信验证码无效，请重新获取");
            }
            if (cachedMsgCaptcha.ValidateCount >= 3)
            {
                GetMemoryCache.Remove(loginDto.MobilePhone);
                return ApiResultHelper.Error("短信验证码失效，请重新获取");
            }
            cachedMsgCaptcha.ValidateCount++;
            if (!string.Equals(cachedMsgCaptcha.SmsCode, loginDto.SmsCode, StringComparison.OrdinalIgnoreCase))
            {
                return ApiResultHelper.Error("验证码错误");
            }
            GetMemoryCache.Remove(loginDto.MobilePhone);
            var data = iMapper.Map<LoginRsDTO>(_user);
            data.Token = JwtTools.GetToken(_user);
            return ApiResultHelper.Success(data);
        }

        [HttpPost("LoginByFace")]
        public async Task<ApiResult> FaceLogin([FromServices] IMapper iMapper, [FromBody] FaceDTO face)
        {
            var user_id = _iBaiduFaceMService.SearchFace(face);
            if (string.IsNullOrEmpty(user_id)) return ApiResultHelper.Error("未找到人脸信息");
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
            var cachedMsgCaptcha = GetMemoryCache.Get<UserDTO>(mobilePhone);
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
            GetMemoryCache.Set(loginDto.MobilePhone, loginDto, TimeSpan.FromMinutes(2));
            //调用第三方SDK实际发送短信
            //.....
            return ApiResultHelper.Success("发送成功" + loginDto.SmsCode);
        }


        [HttpGet("SendSmsByResetPwd")]
        public async Task<ApiResult> SendSmsByResetPwd([FromQuery] string mobilePhone)
        {
            var _user = await _iSysUserService.FindAsync(x => x.MobilePhone == mobilePhone);
            if (_user == null) return ApiResultHelper.Error("该手机号未注册");
            var cachedMsgCaptcha = GetMemoryCache.Get<UserDTO>(mobilePhone);
            if (cachedMsgCaptcha != null)
            {
                var offsetSecionds = (DateTime.Now - cachedMsgCaptcha.CreateTime).Seconds;
                if (offsetSecionds < 60)
                {
                    return ApiResultHelper.Error("短信验证码获取太频繁，请稍后重试");
                }
            }
            UserDTO resetDTO = new UserDTO();
            resetDTO.SmsCode = ALiSMSHelper.CreateRandomNumber(6);
            resetDTO.CreateTime = DateTime.Now;
            resetDTO.ValidateCount = 0;
            resetDTO.MobilePhone = mobilePhone;
            GetMemoryCache.Set(resetDTO.MobilePhone, resetDTO, TimeSpan.FromMinutes(2));
            //调用第三方SDK实际发送短信
            //.....
            return ApiResultHelper.Success("发送成功,验证码为:" + resetDTO.SmsCode);
        }

       

        [HttpGet("SendSmsByRegister")]
        public async Task<ApiResult> SendSmsByRegister([FromQuery] string mobilePhone)
        {
            if(!Valid.IsPhoneNumber(mobilePhone)) return ApiResultHelper.Error("手机号码不合法");
            var _user = await _iSysUserService.FindAsync(x => x.MobilePhone == mobilePhone);
            if (_user != null) return ApiResultHelper.Error("该手机号已注册");
            var cachedMsgCaptcha = GetMemoryCache.Get<UserDTO>(mobilePhone);
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
            GetMemoryCache.Set(loginDto.MobilePhone, loginDto, TimeSpan.FromMinutes(2));
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
            var cachedMsgCaptcha = GetMemoryCache.Get<UserDTO>(registerDto.MobilePhone);
            if (cachedMsgCaptcha == null || cachedMsgCaptcha.ValidateCount >= 3)
            {
                GetMemoryCache.Remove(registerDto.MobilePhone);
                return ApiResultHelper.Error("短信验证码无效，请重新获取");
            }
            cachedMsgCaptcha.ValidateCount++;
            if (!string.Equals(cachedMsgCaptcha.SmsCode, registerDto.SmsCode, StringComparison.OrdinalIgnoreCase))
            {
                return ApiResultHelper.Error("验证码错误");
            }
            var sysUser = iMapper.Map<SysUser>(registerDto);
            sysUser.Id=Guid.NewGuid().ToString("N");
            sysUser.IsAdministrator = true;
            sysUser.CreateOwner = sysUser.Account;
            sysUser.CreateTime = DateTime.Now;
            SysUserLogOn sysUserLogOn = new SysUserLogOn();
            sysUserLogOn.Password = MD5Helper.MD5Encrypt32(registerDto.Password);
            sysUserLogOn.UserId = sysUser.Id;
            sysUserLogOn.Id= Guid.NewGuid().ToString("N");
            sysUserLogOn.FirstVisitTime = DateTime.Now;
            var b = await _iSysUserService.Register(sysUser,sysUserLogOn);
            if(!b) return ApiResultHelper.Error("注册失败");
            var loginRs = iMapper.Map<LoginRsDTO>(sysUser);
            loginRs.Token = JwtTools.GetToken(sysUser);
            return ApiResultHelper.Success(loginRs);
        }

        [HttpPost("ResetPwd")]
        public async Task<ApiResult> ResetPwd([FromBody] UserDTO userDTO)
        {
            var cachedMsgCaptcha = GetMemoryCache.Get<UserDTO>(userDTO.MobilePhone);
            if (cachedMsgCaptcha == null || cachedMsgCaptcha.ValidateCount >= 3)
            {
                GetMemoryCache.Remove(userDTO.MobilePhone);
                return ApiResultHelper.Error("短信验证码无效，请重新获取");
            }
            cachedMsgCaptcha.ValidateCount++;
            if (!string.Equals(cachedMsgCaptcha.SmsCode, userDTO.SmsCode, StringComparison.OrdinalIgnoreCase))
            {
                return ApiResultHelper.Error("验证码错误");
            }
            var data = await _iSysUserService.FindAsync(x => x.MobilePhone == userDTO.MobilePhone);
            var user = await _iSysUserLogOnService.FindAsync(x => x.UserId == data.Id);
            user.Password= MD5Helper.MD5Encrypt32(userDTO.Password);
            var b = await _iSysUserLogOnService.EditAsync(user);
            if(b != 1) return ApiResultHelper.Error("修改失败");
            return ApiResultHelper.Success("修改成功");
        }
    }
}
