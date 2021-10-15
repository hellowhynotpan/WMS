using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WMS.WebApi.Common
{
    public class ApiResult
    {
        /// <summary>
        /// 状态码
        /// </summary>
        public int Code { get; set; } = 200;

        /// <summary>
        /// 错误消息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 分页总数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public dynamic Data { get; set; }
    }
}
