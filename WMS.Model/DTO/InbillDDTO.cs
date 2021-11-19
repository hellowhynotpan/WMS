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
    public class InbillDDTO
    {
        public int LineNo { get; set; }
        public string ErpCodeLine { get; set; }

        public string PartId { get; set; }

        public string CsId { get; set; }

        public int InbillQty { get; set; }

        public string SnNo { get; set; }

        public int SnType { get; set; }

        public int SnQty { get; set; }

        public DateTime SnDateCode { get; set; }

        public string BatchNo { get; set; }

        public string PalletNo { get; set; }
    }
}
