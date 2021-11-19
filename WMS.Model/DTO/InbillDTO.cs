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
   public class InbillDTO
    {
        public string InbillNo { get; set; }

        public string ErpCode { get; set; }

        public int InbillType { get; set; }

        public string Memo { get; set; }

        public int Status { get; set; }

        public string CreateOwner { get; set; }

        public List<InbillDDTO> InbillDs { get; set; }
    }
}
