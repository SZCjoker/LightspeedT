using LightspeedT.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace LightspeedT.Controllers
{
    public class baseController : ApiController
    {

        public class ResultDTO<T> : IDisposable
        {
            #region 建構子
            public ResultDTO()
            {

            }
            public ResultDTO(string rc, string rm, string returnMsg, T Data)
            {
                _rc = rc;
                _rm = rm;
                _returnMsg = returnMsg;
                _data = Data;
            }
            #endregion

            #region 解構子
            ~ResultDTO()
            {
                Dispose(true);
            }
            #endregion

            #region 釋放資源
            public void Dispose()
            {
                Dispose(true);
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing) GC.SuppressFinalize(this);
            }
            #endregion

            #region 欄位
            private string _rc;
            private string _rm;
            private string _returnMsg;
            private T _data;
            #endregion

            #region 屬性
            public string rc { get { return _rc; } }
            public string rm { get { return _rm; } }
            public string returnMsg { get { return _returnMsg; } }
            public T Data { get { return _data; } }
            #endregion
        }

        public IHttpActionResult GetResult<T>(T data, string rc = "0000", string rm = "執行成功", string returnMsg = "執行成功")
        {
            ResultDTO<T> result;
            try
            {
                result = new ResultDTO<T>(rc, rm, returnMsg, data);
            }
            catch (Exception ex)
            {
                result = new ResultDTO<T>("-9999", ex.Message, "執行失敗", data);
            }
            using (result) return Json(result);
        }


        /// <summary>
        /// CALL API
        /// </summary>
        /// <param name="url"></param>
        /// <param name="stringContent"></param>
        /// <returns></returns>
        internal async Task<HttpResponseMessage> SendPostRequest(string url, StringContent stringContent)
        {  
            using (HttpClient client = new HttpClient())
             return await client.PostAsync(url, stringContent);
        }
        /// <summary>
        /// PARA轉型
        /// </summary>
        /// <param name="content"></param>
        /// <param name="mediaType"></param>
        /// <returns></returns>
        internal StringContent AddParams(string content = null, string mediaType = null)
        {
            return new StringContent(content, Encoding.UTF8, mediaType);
        }
        /// <summary>
        /// 接收APIDATA
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="result"></param>
        /// <returns></returns>
        internal async Task<T> ApiResult<T>(HttpResponseMessage result)
        {
            return result.IsSuccessStatusCode ? await result.Content.ReadAsAsync<T>() : default(T);
        }


     

    }


}
