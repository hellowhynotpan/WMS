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
    /// 系统用户登录信息
    /// </summary>
    [SugarTable("sys_user_logon")]
    public class SysUserLogOn
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(ColumnName = "id", ColumnDataType = "varchar(50)", IsPrimaryKey = true,  ColumnDescription = "主键")]//数据库是自增才配自增 
        public string Id { get; set; }

        /// <summary>
        /// 用户主表主键
        /// </summary>
        [SugarColumn(ColumnName = "user_id", ColumnDataType = "varchar(50)", ColumnDescription = "账号", IsNullable = false)]
        public string UserId { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [SugarColumn(ColumnName = "password", ColumnDataType = "varchar(50)", ColumnDescription = "密码", IsNullable = false)]
        public string Password { get; set; }

        /// <summary>
        /// 允许登录开始时间
        /// </summary>
        [SugarColumn(ColumnName = "allow_start_time", ColumnDescription = "允许登录开始时间", IsNullable = true)]
        public DateTime AllowStartTime { get; set; }

        /// <summary>
        /// 允许登录结束时间
        /// </summary>
        [SugarColumn(ColumnName = "allow_end_time",  ColumnDescription = "允许登录结束时间", IsNullable = true)]
        public DateTime AllowEndTime { get; set; }

        /// <summary>
        /// 第一次访问时间
        /// </summary>
        [SugarColumn(ColumnName = "first_visit_time", ColumnDescription = "第一次访问时间", IsNullable = false)]
        public DateTime FirstVisitTime { get; set; }

        /// <summary>
        /// 上一次访问时间
        /// </summary>
        [SugarColumn(ColumnName = "previous_visit_time", ColumnDescription = "第一次访问时间", IsNullable = true)]
        public DateTime PreviousVisitTime { get; set; }

        /// <summary>
        ///最后访问时间
        /// </summary>
        [SugarColumn(ColumnName = "last_visit_time", ColumnDescription = "第一次访问时间", IsNullable = true)]
        public DateTime LastVisitTime { get; set; }

        /// <summary>
        ///最后修改密码日期
        /// </summary>
        [SugarColumn(ColumnName = "change_password_date",  ColumnDescription = "最后修改密码日期", IsNullable = true)]
        public DateTime ChangePasswordDate { get; set; }

        /// <summary>
        ///允许同时有多用户登录
        /// </summary>
        [SugarColumn(ColumnName = "multi_user_login", ColumnDataType = "bool", ColumnDescription = "最后修改密码日期", IsNullable = false)]
        public bool MultiUserLogin { get; set; } = true;

        /// <summary>
        ///登录次数
        /// </summary>
        [SugarColumn(ColumnName = "log_on_count", ColumnDescription = "登录次数", IsNullable = false)]
        public int LogOnCount { get; set; }

        /// <summary>
        ///在线状态
        /// </summary>
        [SugarColumn(ColumnName = "user_on_line", ColumnDataType = "bool", ColumnDescription = "在线状态", IsNullable = true)]
        public bool UserOnLine { get; set; }

        /// <summary>
        ///密码提示问题
        /// </summary>
        [SugarColumn(ColumnName = "question", ColumnDataType = "varchar(100)", ColumnDescription = "密码提示问题", IsNullable = true)]
        public string Question { get; set; }

        /// <summary>
        ///密码提示答案
        /// </summary>
        [SugarColumn(ColumnName = "answer_question", ColumnDataType = "varchar(100)", ColumnDescription = "密码提示答案", IsNullable = true)]
        public string AnswerQuestion { get; set; }

        /// <summary>
        ///系统语言
        /// </summary>
        [SugarColumn(ColumnName = "language", ColumnDataType = "varchar(50)", ColumnDescription = "系统语言", IsNullable = true)]
        public string Language { get; set; }

        /// <summary>
        ///系统样式
        /// </summary>
        [SugarColumn(ColumnName = "theme", ColumnDataType = "varchar(50)", ColumnDescription = "系统样式", IsNullable = true)]
        public string Theme { get; set; }
    }
}
