using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.WebApi.Common
{
    public class ApiRequest<TEntity>
    {
        /// <summary>
        /// 请求Ip
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 请求用户
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// 请求数据
        /// </summary>
        public TEntity Data { get; set; }
    }
}
