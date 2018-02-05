using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Fhey.Framework.Uility.Network.Http.Interface
{
    public interface IHttpRequester
    {
        HttpWebResponse CreateGetHttpResponse(string url, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        HttpWebResponse CreatePostHttpResponse(string url, string postData, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        HttpWebResponse CreatePostFileHttpResponse(string url, IDictionary<string, string> parameters, Stream fileStream, string fileFormFieldName, string fileContentType, string filePath, string referer, Encoding encoding, CookieCollection cookies);

        string Get(string url, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        string Post(string url, string postData, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        string PostFile(string url, IDictionary<string, string> parameters, Stream fileStream, string fileFormFieldName, string fileContentType, string filePath, string referer, Encoding requestEncoding,CookieCollection cookies);

        string Get(string url);
        string Post(string url, string postData,CookieCollection cookies);

        T Get<T>(string url, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        T Post<T>(string url, string postData, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies);
        T PostFile<T>(string url, IDictionary<string, string> parameters, Stream fileStream, string fileFormFieldName, string fileContentType, string filePath, string referer, Encoding requestEncoding, CookieCollection cookies);

        T Get<T>(string url);
        T Post<T>(string url, string postData, CookieCollection cookies);
    }
}
