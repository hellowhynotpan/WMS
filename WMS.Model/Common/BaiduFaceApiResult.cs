/**************************************************** 
文件名称：
功能描述:
创建日期: 
作者: Light
最后修改人:
**************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Model
{
    public class BaiduFaceApiResult
    {
        public JObject OriginInfo;
        public string error_code;
        public string error_msg;
        public string log_id;
        public string timestamp;
        public string cached;
        public string face_token;
        public string group_id;
        public string user_id;
        public string user_info;
        public float  score;
    }

}
