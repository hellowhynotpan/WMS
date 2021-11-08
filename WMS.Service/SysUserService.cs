/**************************************************** 
文件名称：
功能描述:
创建日期: 
作者: Light
最后修改人:
**************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.IRepository;
using WMS.IService;
using WMS.Model;

namespace WMS.Service
{
     public class SysUserService : BaseService<SysUser>, ISysUserService
    {
        private readonly ISysUserRepository _iSysUserRepository;

        public SysUserService(ISysUserRepository iSysUserRepository)
        {
            base._iBaseRepository = iSysUserRepository;
            _iSysUserRepository = iSysUserRepository;
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="user">账号</param>
        /// <param name="sysUserLog">登录</param>
        /// <returns></returns>
        public async Task<bool> Register(SysUser user,SysUserLogOn sysUserLog )
        {
            return await _iSysUserRepository.Register(user, sysUserLog);
        }
    }
}
