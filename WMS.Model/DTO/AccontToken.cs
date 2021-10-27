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
    /// 登录Token
    /// </summary>
    public class AccontToken
    {
        /// <summary>
        /// 接收短信的手机号码 (必填项，为用户的手机号码)
        /// </summary>
        public string PhoneNumbers { get; set; }

        /// <summary>
        /// 短信签名名称 (必填项)
        /// </summary>
        public string SignName { get; set; }

        /// <summary>
        /// 短信模板变量对应的实际值
        /// </summary>
        public string TemplateParam { get; set; }

        /// <summary>
        /// 短信验证码长度
        /// </summary>
        public string Length { get; set; }

        /// <summary>
        /// 短信验证码长度
        /// </summary>
        public string SmsTime { get; set; }
    }
}
