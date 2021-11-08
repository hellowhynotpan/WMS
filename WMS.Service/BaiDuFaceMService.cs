/**************************************************** 
文件名称：
功能描述:
创建日期: 
作者: Light
最后修改人:
**************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.IService;
using WMS.Model;
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
        ///人脸注册
        /// </summary>
        /// <param name="face">人脸信息</param>
        /// <returns>face_token</returns>
        public string AddFace(FaceDTO face)
        {
            try
            {
                var APP_ID = "25057538";
                var API_KEY = "vVx9mTwjfGCm2KZ1rj2STCGZ";
                var SECRET_KEY = "98d2t0PNp07QqHWyWqo5RGLb41WmpOo0";
                var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
                client.Timeout = 60000;
                var result = client.UserAdd(face.Image, face.ImageType, face.GroupId, face.UserId);
                return result["result"]["face_token"].ToString();
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 人脸搜索
        /// </summary>
        /// <returns>user_id</returns>
        public string SearchFace(FaceDTO face)
        {
            try
            {
                var APP_ID = "25057538";
                var API_KEY = "vVx9mTwjfGCm2KZ1rj2STCGZ";
                var SECRET_KEY = "98d2t0PNp07QqHWyWqo5RGLb41WmpOo0";
                var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
                client.Timeout = 60000;
                JObject result = client.Search(face.Image, face.ImageType, face.GroupId);
                BaiduFaceApiResult apiResult = JObject2BaduApiResult(result);
                if (apiResult.error_msg.ToLower() != "success"|| apiResult.score<90)
                {
                    return null;
                }
                return apiResult.user_id;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public string UpdFace(FaceDTO face)
        {
            try
            {
                var APP_ID = "25057538";
                var API_KEY = "vVx9mTwjfGCm2KZ1rj2STCGZ";
                var SECRET_KEY = "98d2t0PNp07QqHWyWqo5RGLb41WmpOo0";
                var client = new Baidu.Aip.Face.Face(API_KEY, SECRET_KEY);
                client.Timeout = 60000;
                JObject result = client.UserUpdate(face.Image, face.ImageType, face.GroupId, face.UserId);
                return result["result"]["face_token"].ToString();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public BaiduFaceApiResult JObject2BaduApiResult(JObject result) 
        {

            BaiduFaceApiResult SearchFaceMatchInfo = new BaiduFaceApiResult();
            SearchFaceMatchInfo.log_id = result.Value<string>("log_id");//log_id
            SearchFaceMatchInfo.error_code = result.Value<string>("error_code");//log_id
            SearchFaceMatchInfo.error_msg = result.Value<string>("error_msg");//log_id
            SearchFaceMatchInfo.timestamp = result.Value<string>("timestamp");//timestamp
            SearchFaceMatchInfo.cached = result.Value<string>("cached");//cached
            SearchFaceMatchInfo.face_token = result["result"]["face_token"].ToString();//face_token
            JArray res = result["result"].Value<JArray>("user_list");
            JObject j = JObject.Parse(res[0].ToString());
            SearchFaceMatchInfo.group_id = j.Value<string>("group_id");//group_id
            SearchFaceMatchInfo.user_id = j.Value<string>("user_id");//user_id
            SearchFaceMatchInfo.user_info = j.Value<string>("user_info");//user_info
            SearchFaceMatchInfo.score = j.Value<float>("score");//scor
            return SearchFaceMatchInfo;
        }
    }
}
