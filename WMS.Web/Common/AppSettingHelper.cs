using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.WebApi.Common
{
    /// <summary>
    /// 读取AppSetting是.json
    /// </summary>
    public class AppSettingHelper
    {
        /// <summary>
        /// 读取appsetting配置
        /// </summary>
        public class AppSetting
        {
            private static IConfigurationSection _configurationSection = null;
            /// <summary>
            /// 读取配置
            /// </summary>
            /// <param name="key"></param>
            /// <returns></returns>
            public static string GetAppSetting(string key)
            {
                return _configurationSection.GetSection(key)?.Value;
            }
            /// <summary>
            /// 设置配置
            /// </summary>
            /// <param name="section"></param>
            public static void SetAppSetting(IConfigurationSection section)
            {
                _configurationSection = section;
            }
        }
    }
}
