﻿/**************************************************** 
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
using WMS.Model.DTO;

namespace WMS.IService
{
    public interface IStockMService : IBaseService<StockM>
    {
        public Task<List<StockDTO>> QueryStock(string createOwner);
    }
}
