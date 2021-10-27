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
    /// <summary>
    /// 登录DTO类
    /// </summary>
    public class UserDTO
    {
        /// <summary>
        ///登录名
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        ///短信验证码
        /// </summary>
        public string SmsCode { get; set; }

        /// <summary>
        ///手机号
        /// </summary>
        public string MobilePhone { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 验证码创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 验证码已验证次数
        /// </summary>
        public int ValidateCount { get; set; }

        /// <summary>
        ///姓名
        /// </summary>
        public string RealName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public bool Gender { get; set; }
    }
}
