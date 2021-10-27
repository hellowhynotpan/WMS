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
    public class FaceDTO
    {
        public FaceDTO()
        {
            ImageType = "BASE64";
            GroupId = "_test";
        }
        /// <summary>
        /// 
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ImageType { get; set; }

        /// <summary>
        /// 人脸组id
        /// </summary>
        public string GroupId { get; set; }

        /// <summary>
        /// user_Id
        /// </summary>
        public int UserId { get; set; }

    }
}
