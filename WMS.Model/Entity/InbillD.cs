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

namespace WMS.Model
{
    /// <summary>
    /// 入库单明细
    /// </summary>
    [SugarTable("Inbill_d")]
    public class InbillD
    {
        // <summary>
        /// 主键
        /// </summary>
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true, ColumnDataType = "varchar(50)", ColumnDescription = "主键")]//数据库是自增才配自增 
        public string Id { get; set; }

        /// <summary>
        /// 项次编号
        /// </summary>
        [SugarColumn(ColumnName = "line_no",  ColumnDescription = "项次编号", IsNullable = false)]
        public int LineNo { get; set; }

        /// <summary>
        /// 入库单主表主键
        /// </summary>
        [SugarColumn(ColumnName = "inbill_m_id", ColumnDataType = "varchar(50)", ColumnDescription = "入库单主表主键", IsNullable = false)]
        public string InbillMId { get; set; }

        /// <summary>
        /// ERP 单号
        /// </summary>
        [SugarColumn(ColumnName = "erp_code", ColumnDataType = "varchar(100)", ColumnDescription = "ERP 单号", IsNullable = true)]
        public string ErpCode { get; set; }

        /// <summary>
        /// ERP 单号项次
        /// </summary>
        [SugarColumn(ColumnName = "erp_code_line", ColumnDataType = "varchar(100)", ColumnDescription = "ERP 单号项次", IsNullable = true)]
        public string ErpCodeLine { get; set; }

        /// <summary>
        /// 物料 pk
        /// </summary>
        [SugarColumn(ColumnName = "part_id", ColumnDataType = "varchar(100)", ColumnDescription = "物料 pk", IsNullable = false)]
        public string PartId { get; set; }

        /// <summary>
        /// 储位 pk
        /// </summary>
        [SugarColumn(ColumnName = "cs_id", ColumnDataType = "varchar(100)", ColumnDescription = "储位 pk", IsNullable = false)]
        public string CsId { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        [SugarColumn(ColumnName = "inbill_qty", ColumnDataType = "varchar(100)", ColumnDescription = "入库数量", IsNullable = false)]
        public int InbillQty { get; set; }
    }
}
