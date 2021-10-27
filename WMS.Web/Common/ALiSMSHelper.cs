// This file is auto-generated, don't edit it. Thanks.

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Tea;
using Tea.Utils;
namespace Common
{
    public class ALiSMSHelper
    {
        /// <summary>
        /// 生成指定位数的随机数字码
        /// </summary>
        /// <param name="length">验证码长度</param>
        /// <returns></returns>
        public static string CreateRandomNumber(int length)
        {
            Random random = new Random();
            StringBuilder sbMsgCode = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sbMsgCode.Append(random.Next(0, 9));
            }

            return sbMsgCode.ToString();
        }
        /**
         * 使用AK&SK初始化账号Client
         * @param accessKeyId
         * @param accessKeySecret
         * @return Client
         * @throws Exception
         */
        public static AlibabaCloud.SDK.Dysmsapi20170525.Client CreateClient(string accessKeyId, string accessKeySecret)
        {
            AlibabaCloud.OpenApiClient.Models.Config config = new AlibabaCloud.OpenApiClient.Models.Config
            {
                // 您的AccessKey ID
                AccessKeyId = accessKeyId,
                // 您的AccessKey Secret
                AccessKeySecret = accessKeySecret,
            };
            // 访问的域名
            config.Endpoint = "dysmsapi.aliyuncs.com";
            return new AlibabaCloud.SDK.Dysmsapi20170525.Client(config);
        }
/*
        /// <summary>
        /// 调用示例
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            AlibabaCloud.SDK.Dysmsapi20170525.Client client = CreateClient("accessKeyId", "accessKeySecret");
            AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest sendSmsRequest = new AlibabaCloud.SDK.Dysmsapi20170525.Models.SendSmsRequest
            {
                PhoneNumbers = "17855355534",
                SignName = "aaaa",
                TemplateCode = "aaaaaa",
                TemplateParam = "{\"name\":\"张三\",\"number\":\"15038****76\"}",
            };
            // 复制代码运行请自行打印 API 的返回值
            client.SendSms(sendSmsRequest);
        }*/
    }
}
