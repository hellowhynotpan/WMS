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
using WMS.IService;
using WMS.Model.DTO;

namespace WMS.Service
{
    public class BaiDuFaceMService : IBaiDuFaceMService
    {
        public BaiDuFaceMService()
        {
            // 设置APPID/AK/SK
           
        }

        /// <summary>
        /// 人脸注册
        /// </summary>
        /// <returns></returns>
        public bool AddFace(FaceDTO face)
        {
            try
            {
                var APP_ID = "25057538";
                var API_KEY = "vVx9mTwjfGCm2KZ1rj2STCGZ";
                var SECRET_KEY = "98d2t0PNp07QqHWyWqo5RGLb41WmpOo0";
                var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
                client.Timeout = 60000;  // 修改超时时间
                // 调用人脸注册，可能会抛出网络等异常，请使用try/catch捕获
                var result = client.UserAdd(face.Image, face.ImageType, face.GroupId, face.FaceToken);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 人脸搜索
        /// </summary>
        /// <returns></returns>
        public bool SearchFace(FaceDTO face)
        {
            try
            {
                var APP_ID = "25057538";
                var API_KEY = "vVx9mTwjfGCm2KZ1rj2STCGZ";
                var SECRET_KEY = "98d2t0PNp07QqHWyWqo5RGLb41WmpOo0";
                var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
                client.Timeout = 60000;  // 修改超时时间
                // 调用人脸注册，可能会抛出网络等异常，请使用try/catch捕获
                var result = client.Search(face.Image, face.ImageType, face.GroupId);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
