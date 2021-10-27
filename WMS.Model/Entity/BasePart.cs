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
    /// 物料信息
    /// </summary>
    [SugarTable("base_part")]
    public class BasePart
    {
        // <summary>
        /// 主键
        /// </summary>
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true, ColumnDataType = "varchar(50)", ColumnDescription = "主键")]//数据库是自增才配自增 
        public string Id { get; set; }

        /// <summary>
        /// 物料编号
        /// </summary>
        [SugarColumn(ColumnName = "part_no", ColumnDataType = "varchar(100)", ColumnDescription = "物料编号", IsNullable = false)]
        public string PartNo { get; set; }

        /// <summary>
        /// 物料名稱
        /// </summary>
        [SugarColumn(ColumnName = "part_name", ColumnDataType = "varchar(300)", ColumnDescription = "物料名稱", IsNullable = false)]
        public string PartName { get; set; }

        /// <summary>
        /// 物料規格
        /// </summary>
        [SugarColumn(ColumnName = "part_spec", ColumnDataType = "varchar(300)", ColumnDescription = "物料規格", IsNullable = true)]
        public string PartSpec { get; set; }

        /// <summary>
        /// 物料图片
        /// </summary>
        [SugarColumn(ColumnName = "part_image", ColumnDataType = "varchar(300)", ColumnDescription = "物料規格", IsNullable = true)]
        public string PartImage { get; set; }

        /// <summary>
        /// 物料類型
        /// </summary>
        [SugarColumn(ColumnName = "part_type",ColumnDescription = "物料類型", IsNullable = false)]
        public bool PartType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "memo", ColumnDataType = "varchar(500)", ColumnDescription = "备注", IsNullable = false)]
        public string Memo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false)]
        public bool Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_time", ColumnDescription = "状态", IsNullable = false)]
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

        /// <summary>
        /// 作废时间
        /// </summary>
        [SugarColumn(ColumnName = "invalid_time", ColumnDescription = "作废时间", IsNullable = true)]
        public DateTime InvalidTime { get; set; }

        /// <summary>
        /// 作废用户
        /// </summary>
        [SugarColumn(ColumnName = "invalid_owner", ColumnDescription = "作废用户", IsNullable = true)]
        public string InvalidOwner { get; set; }
    }
}
