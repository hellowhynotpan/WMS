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

namespace WMS.Model.DTO
{
    public class BasePartDTO
    {
        //主键
        public string Id { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string PartName { get; set; }
    }
}
