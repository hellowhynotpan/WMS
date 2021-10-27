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
using WMS.Model.DTO;

namespace WMS.IService
{
    public interface IBaiDuFaceMService
    {
        /// <summary>
        /// 人脸注册
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public bool AddFace(FaceDTO face);

        /// <summary>
        /// 人脸搜索
        /// </summary>
        /// <param name="face"></param>
        /// <returns></returns>
        public bool SearchFace(FaceDTO face);
    }
}
