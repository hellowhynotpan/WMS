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
using WMS.Model;

namespace WMS.Repository
{
    public class SysUserRepository : BaseRepository<SysUser>, ISysUserRepository
    {
        //注册事务
        public async Task<bool>  Register(SysUser user,SysUserLogOn logOn)
        {
            try
            {
                base.Context.Ado.BeginTran();
                await Context.Insertable(user).ExecuteCommandAsync();
                await Context.Insertable(logOn).ExecuteCommandAsync();
                Context.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                Context.Ado.CommitTran();
                throw ex;
            }
        }
    }
}
