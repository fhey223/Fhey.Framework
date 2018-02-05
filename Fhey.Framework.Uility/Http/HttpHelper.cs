using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fhey.Framework.Uility.Network.Http
{
    public class HttpHelper
    {
        /// <summary>
        /// 获取Ajax请求参数
        /// </summary>
        /// <param name="Request"></param>
        /// <returns></returns>
        public Dictionary<string, string> PrepareHttpParams(HttpRequestBase Request)
        {
            var collection = HttpContext.Current.Request.RequestType == "POST"? Request.Form: Request.QueryString;
            return collection.AllKeys.ToDictionary(k => k, v => collection[v]);
        }


        /// <summary>
        /// 当前域名
        /// </summary>
        public string HostUrl
        {
            get
            {
                return CurrentUrl.IndexOf("https:") != -1
                ?
                "https://" + HttpContext.Current.Request.Url.Authority.ToLower()
                :
                "http://" + HttpContext.Current.Request.Url.Authority.ToLower();
            }
        }
        /// <summary>
        /// 当前url
        /// </summary>
        public string CurrentUrl =>
            HttpContext.Current.Request.Url.ToString().ToLower();
  
        /// <summary>
        /// 上个url
        /// </summary>
        public string BackUrl
        {
            get
            {
                try
                {
                    return HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
                }
                catch
                {
                    return CurrentUrl.ToLower();
                }
            }
        }

        /// <summary>
        /// 获取IP地址
        /// </summary>
        public string IP =>
             HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] ?? HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
    }
}
