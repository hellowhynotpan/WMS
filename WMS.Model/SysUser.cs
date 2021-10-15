/**************************************************** 
文件名称：系统用户类
功能描述: 用户实体与基本属性
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

namespace WMS.Model
{

    /// <summary>
    /// 系统用户
    /// </summary>
    [SugarTable("sys_user")]
    public class SysUser
    {
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true, ColumnDataType = "varchar(50)", ColumnDescription = "主键")]//数据库是自增才配自增 
        public string Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [SugarColumn(ColumnName = "account", ColumnDataType = "varchar(50)", ColumnDescription = "账号", IsNullable = false)]
        public string Account { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        [SugarColumn(ColumnName = "real_name", ColumnDataType = "varchar(50)", ColumnDescription = "姓名", IsNullable = false)]
        public string RealName { get; set; }

        /// <summary>
        /// 呢称
        /// </summary>
        [SugarColumn(ColumnName = "nick_name", ColumnDataType = "varchar(50)", ColumnDescription = "呢称", IsNullable = true)]
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [SugarColumn(ColumnName = "head_icon", ColumnDataType = "varchar(50)", ColumnDescription = "头像", IsNullable = true)]
        public string HeadIcon { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [SugarColumn(ColumnName = "gender", ColumnDataType = "varchar(50)", ColumnDescription = "性别", IsNullable = false)]
        public bool Gender { get; set; }

        /// <summary>
        /// 生日
        /// </summary>
        [SugarColumn(ColumnName = "birthday",  ColumnDescription = "生日", IsNullable = true)]
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 手机
        /// </summary>
        [SugarColumn(ColumnName = "mobile_phone",  ColumnDescription = "生日", IsNullable = true)]
        public DateTime MobilePhone { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [SugarColumn(ColumnName = "email", ColumnDataType = "varchar(50)", ColumnDescription = "生日", IsNullable = true)]
        public string Email { get; set; }

        /// <summary>
        /// 微信
        /// </summary>
        [SugarColumn(ColumnName = "we_chat_id", ColumnDataType = "varchar(50)", ColumnDescription = "微信", IsNullable = true)]
        public string WeChatId { get; set; }

        /// <summary>
        /// 脸部辨视
        /// </summary>
        [SugarColumn(ColumnName = "FaceId", ColumnDataType = "varchar(50)", ColumnDescription = "脸部辨视", IsNullable = true)]
        public string FaceId { get; set; }

        /// <summary>
        /// 主管主键
        /// </summary>
        [SugarColumn(ColumnName = "manager_id", ColumnDataType = "varchar(50)", ColumnDescription = "主管主键", IsNullable = true)]
        public string ManagerId { get; set; }

        /// <summary>
        /// 组织主键
        /// </summary>
        [SugarColumn(ColumnName = "organize_id", ColumnDataType = "varchar(50)", ColumnDescription = "组织主键", IsNullable = true)]
        public string OrganizeId { get; set; }

        /// <summary>
        /// 部门主键
        /// </summary>
        [SugarColumn(ColumnName = "department_id", ColumnDataType = "varchar(50)", ColumnDescription = "部门主键", IsNullable = true)]
        public string DepartmentId { get; set; }

        /// <summary>
        /// 是否管理员
        /// </summary>
        [SugarColumn(ColumnName = "is_administrator",  ColumnDescription = "是否管理员", IsNullable = false)]
        public bool IsAdministrator { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        [SugarColumn(ColumnName = "delete_mark", ColumnDescription = "删除标志", IsNullable = false)]
        public bool DeleteMark { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        [SugarColumn(ColumnName = "delete_time", ColumnDescription = "删除时间", IsNullable = true)]
        public bool DeleteTime { get; set; }

        /// <summary>
        /// 删除用户Id
        /// </summary>
        [SugarColumn(ColumnName = "delete_user_id",ColumnDescription = "删除用户 Id", IsNullable = true)]
        public bool DeleteUserId { get; set; }

        /// <summary>
        /// 有效标志
        /// </summary>
        [SugarColumn(ColumnName = "enabled_mark", ColumnDescription = "有效标志  0: 有效 1: 无效", IsNullable = false)]
        public bool EnabledMark { get; set; }

        /// <summary>
        /// 用户描述
        /// </summary>
        [SugarColumn(ColumnName = "description", ColumnDescription = "用户描述", ColumnDataType = "varchar(500)", IsNullable = true)]
        public string Description { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_time", ColumnDescription = "创建时间",  IsNullable = false)]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [SugarColumn(ColumnName = "create_owner", ColumnDescription = "创建用户", ColumnDataType = "varchar(500)", IsNullable = false)]
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
