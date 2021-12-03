/**************************************************** 
文件名称：仓库信息类
功能描述: 仓库信息基本属性
创建日期: 2021-10-13
作者: Light
最后修改人:Light
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
    /// 仓库信息
    /// </summary>
    [SugarTable("base_warehouse")]
    public class BaseWareHouse
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(ColumnName = "id",IsPrimaryKey = true, ColumnDataType = "varchar(50)", ColumnDescription = "主键")]//数据库是自增才配自增 
        public string Id { get; set; }

        /// <summary>
        /// 仓库编号
        /// </summary>
        [SugarColumn(ColumnName = "wh_no", ColumnDataType = "varchar(50)", ColumnDescription = "仓库编号",IsNullable =false)]//数据库是自增才配自增 
        public string WhNo { get; set; }

        /// <summary>
        /// 仓库名稱
        /// </summary>
        [SugarColumn(ColumnName = "wh_name", ColumnDataType = "varchar(100)", ColumnDescription = "仓库名稱", IsNullable = false)]//数据库是自增才配自增 
        public string WhName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "memo", ColumnDataType = "varchar(500)", ColumnDescription = "备注", IsNullable = true)]//数据库是自增才配自增 
        public string Memo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnName = "status",  ColumnDescription = "状态", IsNullable = false)]
        public int Status { get; set; }

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
        public DateTime LastUpdTime { get; set; }

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
