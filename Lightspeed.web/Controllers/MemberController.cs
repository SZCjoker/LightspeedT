using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Lightspeed.web.Models;

namespace Lightspeed.web.Controllers
{
    public class MemberController : Controller
    { string url = Properties.Resources.apiserver_url;
        // GET: Member
        public ActionResult Index()
        {
            //string queryurl = url + "getalldata";
            //var excute = await SendPostRequest(queryurl, null);
            //var excuresult = await ApiResult<MemberDetail>(excute);

            return View();
        }




        //call api
        [HttpPost]
        public async Task<ActionResult> GetMember()
        {
            string queryurl = url + "getalldata";
            var excute = await SendPostRequest(queryurl, null);
            var excuresult = await ApiResult<MemberDetail>(excute);
            return GetResult(excuresult);
        }







        public ActionResult GetResult<T>(T data, string rc = "0000", string rm = "執行成功", string returnMsg = "執行成功")
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
            using (result) return Json(result,JsonRequestBehavior.AllowGet);
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

    //public class Jwt
    //{
    //    private IJsonSerializer serializer = new JsonNetSerializer();
    //    private IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
    //    private string secret = "asdfghjkiuytqwer"; //key

    //    public string EnCodeJwt(Dictionary<string, string> userinfo)
    //    {
    //        IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
    //        IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
    //        var token = encoder.Encode(userinfo, secret);
    //        return token;
    //    }


    //    public string DeCodeJwt(string token)
    //    {
    //        IDateTimeProvider provider = new UtcDateTimeProvider();
    //        IJwtValidator validator = new JwtValidator(serializer, provider);
    //        IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
    //        var Json = decoder.Decode(token, secret, verify: true);
    //        return Json;
    //    }





    //}



}
