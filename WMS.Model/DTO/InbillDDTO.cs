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
    //入库单明细DTO
    public class InbillDDTO
    {
        public string Id { get; set; }


        public string SnId { get; set; }

        public string InbillMId { get; set; }

        public int LineNo { get; set; }

        /// <summary>
        /// 项次编号
        /// </summary>
        public string ErpCode { get; set; }

        /// <summary>
        /// 项次编号
        /// </summary>
        public string ErpCodeLine { get; set; }

        /// <summary>
        /// 物料pk
        /// </summary>
        public string PartId { get; set; }

        /// <summary>
        /// 储位PK
        /// </summary>
        public string CsId { get; set; }

        /// <summary>
        /// 物料名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 储位名称
        /// </summary>
        public string CsName { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int InbillQty { get; set; }

        /// <summary>
        /// SN号
        /// </summary>
        public string SnNo { get; set; }

        /// <summary>
        /// date
        /// </summary>
        public string DateCode { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        public string BatchNo { get; set; }

    }
}
