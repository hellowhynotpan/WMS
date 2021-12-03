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
using WMS.Model.Entity;

namespace WMS.Repository
{
    public class BaseWareHouseRepository : BaseRepository<BaseWareHouse>, IBaseWareHouseRepository
    {

        public async Task<bool> InvalidWh(BaseWareHouse wh,List<BaseCargospace> csList)
        {
            try
            {
                base.Context.Ado.BeginTran();
                await Context.Updateable(wh).ExecuteCommandAsync();
                await Context.Updateable(csList).ExecuteCommandAsync();
                Context.Ado.CommitTran();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
