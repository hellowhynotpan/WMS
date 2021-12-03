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
    /// 库存
    /// </summary>
    public class StockM
    {
        // <summary>
        /// 主键
        /// </summary>
        [SugarColumn(ColumnName = "id",IsPrimaryKey = true, ColumnDataType = "varchar(50)", ColumnDescription = "主键")]//数据库是自增才配自增 
        public string Id { get; set; }


        /// <summary>
        /// 仓库主表主键
        /// </summary>
        [SugarColumn(ColumnName = "wh_id", ColumnDataType = "varchar(50)", ColumnDescription = "仓库主表主键", IsNullable = false)]
        public string WhId { get; set; }

        /// <summary>
        /// 储位主表主键
        /// </summary>
        [SugarColumn(ColumnName = "cs_id", ColumnDataType = "varchar(50)", ColumnDescription = "储位主表主键", IsNullable = false)]
        public string CsId { get; set; }

        /// <summary>
        /// 物料主表主键
        /// </summary>
        [SugarColumn(ColumnName = "part_id", ColumnDataType = "varchar(50)", ColumnDescription = "物料主表主键", IsNullable = false)]
        public string PartId { get; set; }

        /// <summary>
        /// 库存数量
        /// </summary>
        [SugarColumn(ColumnName = "stock_qty",  ColumnDescription = "库存数量", IsNullable = false)]
        public int StockQty { get; set; }

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

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [SugarColumn(ColumnName = "last_upd_time", ColumnDescription = "最后修改时间", IsNullable = true)]
        public DateTime LastUpdTIME { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        [SugarColumn(ColumnName = "last_upd_owner", ColumnDescription = "最后修改用户", IsNullable = true)]
        public string LastUpdOwner { get; set; }
    }
}
