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
   public class StockDTO
    {
        public string Id { get; set; }

        public string WhId { get; set; }
        public string CsId { get; set; }
        public string PartId { get; set; }

        public int PartType { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        public int StockQty { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        public string WhName { get; set; }

        /// <summary>
        /// 物料规格
        /// </summary>
        public string PartSpec { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        public string PartNo { get; set; }


        /// <summary>
        /// 物料名称
        /// </summary>
        public string PartName { get; set; }

        /// <summary>
        /// 储位编号
        /// </summary>
        public string CsNo { get; set; }

        /// <summary>
        /// 储位名称
        /// </summary>
        public string CsName { get; set; }

    }
}
