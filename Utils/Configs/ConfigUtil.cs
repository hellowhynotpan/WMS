using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace Utils.Configs

{/// <summary>
 /// 读取配置文件信息
 /// </summary>
    public class ConfigExtensions
    {
        public static IConfiguration Configuration { get; set; }
        static ConfigExtensions()
        {
            Configuration = new ConfigurationBuilder()
                 .Add(new JsonConfigurationSource { Path = "appsettings.json", ReloadOnChange = true })
                 .Build();
        }
    }
}
