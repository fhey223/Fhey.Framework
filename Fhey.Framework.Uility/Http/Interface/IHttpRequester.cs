using Fhey.framework.Enum;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Fhey.Framework.Uility.Network.Http.Interface
{
    public interface IHttpRequester
    {
        HttpWebResponse CreateHttpResponse(string url, HttpRequestType httpRequestType, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        HttpWebResponse CreateGetHttpResponse(string url, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        HttpWebResponse CreatePostHttpResponse(string url, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        HttpWebResponse CreatePostFileHttpResponse(string url, IDictionary<string, string> parameters, Stream fileStream, string fileFormFieldName, string fileContentType, string filePath, string referer, Encoding encoding, CookieCollection cookies);

        string Get(string url, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        string Post(string url, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        string PostFile(string url, IDictionary<string, string> parameters, Stream fileStream, string fileFormFieldName, string fileContentType, string filePath, string referer, Encoding requestEncoding, CookieCollection cookies);

        string Get(string url, object parameters);
        string Post(string url, object parameters, CookieCollection cookies);

        T Get<T>(string url, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        T Post<T>(string url, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        T PostFile<T>(string url, IDictionary<string, string> parameters, Stream fileStream, string fileFormFieldName, string fileContentType, string filePath, string referer, Encoding requestEncoding, CookieCollection cookies);

        T Get<T>(string url, object parameters);
        T Post<T>(string url, object parameters, CookieCollection cookies);
    }
}
    
