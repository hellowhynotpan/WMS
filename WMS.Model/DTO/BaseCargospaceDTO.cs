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
    /// <summary>
    /// 储位DTO
    /// </summary>
   public class BaseCargospaceDTO
    {
        public string Id { get; set; }

        public string CsNo { get; set; }

        public string CsName { get; set; }

        public string WhName { get; set; }

        public string WhId { get; set; }

        public string Memo { get; set; }

        public int Status { get; set; }

        public string LastUpdOwner { get; set; }
    }
}
