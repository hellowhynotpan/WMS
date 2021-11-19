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
    /// 入库单
    /// </summary>
    [SugarTable("inbill")]
    public class Inbill
    {
        // <summary>
        /// 主键
        /// </summary>
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true, ColumnDataType = "varchar(50)", ColumnDescription = "主键")] 
        public string Id { get; set; }

        /// <summary>
        /// 入库单编号
        /// </summary>
        [SugarColumn(ColumnName = "Inbill_no", ColumnDataType = "varchar(50)", ColumnDescription = "入库单编号", IsNullable = false)]
        public string InbillNo { get; set; }

        /// <summary>
        ///入库通知单主表主键
        /// </summary>
        [SugarColumn(ColumnName = "inasn_id", ColumnDataType = "varchar(50)", ColumnDescription = "入库通知单主表主键", IsNullable = true)]
        public string InasnId { get; set; }

        /// <summary>
        ///ERP 单号
        /// </summary>
        [SugarColumn(ColumnName = "erp_code", ColumnDataType = "varchar(100)", ColumnDescription = "入库通知单主表主键", IsNullable = true)]
        public string ErpCode { get; set; }

        /// <summary>
        ///物料類型
        /// </summary>
        [SugarColumn(ColumnName = "inbill_type",  ColumnDescription = "物料類型0:采购人库 1:完工入库 2: 工单退料 3: 销售退回 4:其他入库", IsNullable = false)]
        public int InbillType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [SugarColumn(ColumnName = "memo", ColumnDataType = "varchar(500)", ColumnDescription = "备注", IsNullable = false)]
        public string Memo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false)]
        public int Status { get; set; }

        //入库单明细
        [SugarColumn(IsIgnore =true)]
        public List<InbillD> InbillDs { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [SugarColumn(ColumnName = "create_owner", ColumnDescription = "创建用户", ColumnDataType = "varchar(50)", IsNullable = false)]
        public string CreateOwner { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>

        [SugarColumn(ColumnName = "create_time", ColumnDescription = "创建时间", IsNullable = false)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [SugarColumn(ColumnName = "last_upd_time", ColumnDescription = "最后修改时间", IsNullable = true)]
        public DateTime LastUpdTIME { get; set; }

        /// <summary>
        /// 最后修改用户
        /// </summary>
        [SugarColumn(ColumnName = "last_upd_owner", ColumnDataType = "varchar(50)", ColumnDescription = "最后修改用户", IsNullable = true)]
        public string LastUpdOwner { get; set; }
    }
}
