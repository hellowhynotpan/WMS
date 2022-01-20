/**************************************************** 
文件名称：
功能描述:
创建日期: 
作者: Light
最后修改人:
**************************************************/
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model.Entity
{
    /// <summary>
    /// 入库SN明细
    /// </summary>
    [SugarTable("inbill_d_sn")]
    public class InbillDSn
    {
        // <summary>
        /// 主键
        /// </summary>
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true, ColumnDataType = "varchar(50)", ColumnDescription = "主键")]
        public string Id { get; set; }

        // <summary>
        /// 入库单明细表主键
        /// </summary>
        [SugarColumn(ColumnName = "inbill_d_id", ColumnDataType = "varchar(50)", ColumnDescription = "入库单明细表主键", IsNullable = false)]
        public string InbillDId { get; set; }

        // <summary>
        /// 入库单主表主键
        /// </summary>
        [SugarColumn(ColumnName = "inbill_m_id", ColumnDataType = "varchar(50)", ColumnDescription = "入库单主表主键", IsNullable = false)]
        public string InbillMId { get; set; }

        // <summary>
        /// SN号
        /// </summary>
        [SugarColumn(ColumnName = "sn_no", ColumnDataType = "varchar(500)", ColumnDescription = "SN号")]
        public string SnNo { get; set; }

        // <summary>
        /// SN类型
        /// </summary>
        [SugarColumn(ColumnName = "sn_type",  ColumnDescription = "SN类型")]
        public int SnType {get;set;}

        // <summary>
        /// 生产日期
        /// </summary>
        [SugarColumn(ColumnName = "sn_date_code", ColumnDataType = "varchar(500)", ColumnDescription = "DateCode")]
        public string SnDateCode { get; set; }

        // <summary>
        /// 物料pk
        /// </summary>
        [SugarColumn(ColumnName = "part_id", ColumnDataType = "varchar(50)", ColumnDescription = "物料pk", IsNullable = false)]
        public string PartId { get; set; }

        // <summary>
        /// 数量
        /// </summary>
        [SugarColumn(ColumnName = "sn_qty",  ColumnDescription = "数量")]
        public int SnQty { get; set; }

        // <summary>
        /// 批次号
        /// </summary>
        [SugarColumn(ColumnName = "batch_no", ColumnDataType = "varchar(100)", ColumnDescription = "批次号", IsNullable = true)]
        public string BatchNo { get; set; }

        // <summary>
        /// 栈板号
        /// </summary>
        [SugarColumn(ColumnName = "pallet_no", ColumnDataType = "varchar(100)", ColumnDescription = "栈板号", IsNullable = true)]
        public string PalletNo { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_time", ColumnDescription = "创建时间", IsNullable = false)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [SugarColumn(ColumnName = "create_owner", ColumnDescription = "创建用户", ColumnDataType = "varchar(50)", IsNullable = false)]
        public string CreateOwner { get; set; }
    }
}
