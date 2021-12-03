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
using WMS.Model.Entity;
using WMS.Repository;

namespace WMS.IService
{
    /// <summary>
    /// SysUserService接口
    /// </summary>
    public interface ISysUserService:IBaseService<SysUser>
    {
        public Task<bool> Register(SysUser user, SysUserLogOn sysUserLog);
    }
}
