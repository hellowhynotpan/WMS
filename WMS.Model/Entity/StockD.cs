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
    /// 库存明细
    /// </summary>
    public class StockD
    {
        // <summary>
        /// 主键
        /// </summary>
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true, ColumnDataType = "varchar(50)", ColumnDescription = "主键")]
        public string Id { get; set; }

        // <summary>
        /// 库存主表主键
        /// </summary>
        [SugarColumn(ColumnName = "stock_m_id",  ColumnDataType = "varchar(50)", ColumnDescription = "库存主表主键", IsNullable = false)] 
        public string StockMId { get; set; }

        // <summary>
        /// 条码编号
        /// </summary>
        [SugarColumn(ColumnName = "sn_no", ColumnDataType = "varchar(300)", ColumnDescription = "条码编号", IsNullable = false)]
        public string SnNo { get; set; }

        // <summary>
        /// 条码类型
        /// </summary>
        [SugarColumn(ColumnName = "sn_type", ColumnDataType = "varchar(300)", ColumnDescription = "条码类型", IsNullable = false)]
        public int SnType { get; set; }

        // <summary>
        /// 条码数量
        /// </summary>
        [SugarColumn(ColumnName = "sn_qty",  ColumnDescription = "条码编号", IsNullable = false)]
        public int SnQty { get; set; }

        // <summary>
        /// 生产日期
        /// </summary>
        [SugarColumn(ColumnName = "sn_date_code",  ColumnDescription = "生产日期", IsNullable = false)]//数据库是自增才配自增 
        public DateTime SnDateCode { get; set; }

        // <summary>
        /// 批次号
        /// </summary>
        [SugarColumn(ColumnName = "batch_no", ColumnDataType = "varchar(50)", ColumnDescription = "批次号", IsNullable = true)]//数据库是自增才配自增 
        public string BatchNo { get; set; }

        // <summary>
        /// 栈板号
        /// </summary>
        [SugarColumn(ColumnName = "pallet_no", ColumnDataType = "varchar(50)", ColumnDescription = "栈板号", IsNullable = true)]//数据库是自增才配自增 
        public string PalletNo { get; set; }
    }
}
