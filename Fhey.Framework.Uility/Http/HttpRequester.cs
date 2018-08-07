using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Fhey.Framework.Uility.Network.Http.Interface;
using Fhey.Framework.Uility.Serialization.Interface;
using Fhey.Framework.Uility.Serialization;
using System.Reflection;
using Fhey.framework.Enum;

namespace Fhey.Framework.Uility.Network.Http
{
    public abstract class HttpRequester : IHttpRequester
    {

        public IJsonSerializer JsonSerializer = new JsonSerializer();
        public virtual IHttpRequestJsonObjectResultValidator HttpRequestJsonObjectResultValidator { get; set; }

        public virtual HttpWebResponse CreateHttpResponse(string url, HttpRequestType httpRequestType, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (string.IsNullOrEmpty(contentType))
            {
                throw new ArgumentNullException("contentType");
            }
            if (httpRequestType == HttpRequestType.GET)
            {
                url += BuildGetParamters(parameters);
            }
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = httpRequestType.ToString();
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";
            request.ContentType = contentType;
            if (!string.IsNullOrEmpty(userAgent))
            {
                request.UserAgent = userAgent;
            }
            if (timeout.HasValue)
            {
                request.Timeout = timeout.Value;
            }
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }
            switch (httpRequestType)
            {
                case HttpRequestType.GET:

                    break;
                case HttpRequestType.POST:

                    break;
            }

            if (httpRequestType == HttpRequestType.POST && parameters != null)
            {
                var str = JsonSerializer.Serialize(parameters);

                byte[] data = requestEncoding.GetBytes(str);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }
            return request.GetResponse() as HttpWebResponse;
        }

        public virtual HttpWebResponse CreateGetHttpResponse(string url, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies)
        {
            return CreateHttpResponse(url, HttpRequestType.GET, parameters, timeout, userAgent, contentType, requestEncoding, cookies);
        }


        public virtual HttpWebResponse CreatePostHttpResponse(string url, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies)
        {
            return CreateHttpResponse(url, HttpRequestType.POST, parameters, timeout, userAgent, contentType, requestEncoding, cookies);
        }


        public virtual HttpWebResponse CreatePostFileHttpResponse(string url, IDictionary<string, string> parameters, Stream fileStream, string fileFormFieldName, string fileContentType, string filePath, string referer, Encoding requestEncoding, CookieCollection cookies)
        {
            string boundary = "----------" + DateTime.Now.Ticks.ToString("x");


            HttpWebRequest request = null;

            if (url.StartsWith("https", StringComparison.OrdinalIgnoreCase))
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
                request = WebRequest.Create(url) as HttpWebRequest;
                request.ProtocolVersion = HttpVersion.Version10;
            }
            else
            {
                request = WebRequest.Create(url) as HttpWebRequest;
            }

            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.Referer = referer;
            if (cookies != null)
            {
                request.CookieContainer = new CookieContainer();
                request.CookieContainer.Add(cookies);
            }

            StringBuilder sb = new StringBuilder();
            if (parameters != null)
            {
                foreach (string key in parameters.Keys)
                {
                    sb.Append("--" + boundary);
                    sb.Append("\r\n");
                    sb.Append("Content-Disposition: form-data; name=\"" + key + "\"");
                    sb.Append("\r\n\r\n");
                    sb.Append(parameters[key]);
                    sb.Append("\r\n");
                }
            }
            sb.Append("--" + boundary);

            sb.Append("\r\n");
            sb.Append("Content-Disposition: form-data; name=\"" + fileFormFieldName + "\";filename=\"" + filePath + "\"");
            sb.Append("\r\n");
            sb.Append("Content-Type: ");
            sb.Append(fileContentType);
            sb.Append("\r\n\r\n");
            string postHeader = sb.ToString();
            byte[] postHeaderBytes = requestEncoding.GetBytes(postHeader);

            byte[] boundaryBytes = requestEncoding.GetBytes("\r\n--" + boundary + "--\r\n");

            long length = postHeaderBytes.Length + fileStream.Length + boundaryBytes.Length;
            request.ContentLength = length;
            Stream requestStream = request.GetRequestStream();

            requestStream.Write(postHeaderBytes, 0, postHeaderBytes.Length);

            byte[] buffer = new byte[checked((uint)Math.Min(4096, (int)fileStream.Length))];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                requestStream.Write(buffer, 0, bytesRead);
            }

            requestStream.Write(boundaryBytes, 0, boundaryBytes.Length);
            return request.GetResponse() as HttpWebResponse;
        }



        public virtual string Get(string url, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies)
        {
            string result = string.Empty;
            HttpWebResponse httpWebResponse = CreateGetHttpResponse(url, parameters, timeout, userAgent, contentType, requestEncoding, cookies);
            result = GetResponseStreamString(httpWebResponse, requestEncoding);
            return result;
        }

        string GetResponseStreamString(HttpWebResponse httpWebResponse, Encoding encoding)
        {
            string result = string.Empty;
            Stream responseStream = httpWebResponse.GetResponseStream();
            StreamReader streamReader = new StreamReader(responseStream, encoding);
            result = streamReader.ReadToEnd();
            streamReader.Close();
            responseStream.Close();
            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendLine("");
            for (int index = 0; index < httpWebResponse.ResponseUri.AbsoluteUri.Length - 1; index++)
                sbOutput.Append("*");
            sbOutput.AppendLine("*");

            sbOutput.AppendLine("url:" + httpWebResponse.ResponseUri.ToString());
            sbOutput.AppendLine("result:" + result);

            for (int index = 0; index < httpWebResponse.ResponseUri.AbsoluteUri.Length - 1; index++)
                sbOutput.Append("*");
            sbOutput.AppendLine("*");
            return result;
        }


        public virtual string Post(string url, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies = null)
        {
            string result = string.Empty;
            HttpWebResponse httpWebResponse = CreatePostHttpResponse(url, parameters, timeout, userAgent, contentType, requestEncoding, cookies);
            result = GetResponseStreamString(httpWebResponse, requestEncoding);
            return result;
        }


        public virtual string PostFile(string url, IDictionary<string, string> parameters, Stream fileStream, string fileFormFieldName, string fileContentType, string filePath, string referer, Encoding requestEncoding, CookieCollection cookies)
        {
            string result = string.Empty;
            HttpWebResponse httpWebResponse = CreatePostFileHttpResponse(url, parameters, fileStream, fileFormFieldName, fileContentType, filePath, referer, requestEncoding, cookies);
            result = GetResponseStreamString(httpWebResponse, requestEncoding);
            return result;
        }


        public virtual string Get(string url, object parameters)
        {
            return Get(url, parameters, 10000, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)", "application/json", Encoding.UTF8, null);
        }


        public virtual string Post(string url, object parameters, CookieCollection cookies)
        {
            return Post(url, parameters, 10000, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)", "application/json", Encoding.UTF8, null);
        }


        public virtual T Get<T>(string url, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies)
        {
            string json = Get(url, parameters, timeout, userAgent, contentType, requestEncoding, cookies);
            T jsonObject = JsonSerializer.Deserialize<T>(json);

            JsonObjectValidator(jsonObject);

            return jsonObject;
        }


        public virtual T Post<T>(string url, object parameters, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies)
        {
            string json = Post(url, parameters, timeout, userAgent, contentType, requestEncoding, cookies);
            T jsonObject = JsonSerializer.Deserialize<T>(json);

            JsonObjectValidator(jsonObject);

            return jsonObject;
        }


        public virtual T PostFile<T>(string url, IDictionary<string, string> parameters, Stream fileStream, string fileFormFieldName, string fileContentType, string filePath, string referer, Encoding requestEncoding, CookieCollection cookies)
        {
            string json = PostFile(url, parameters, fileStream, fileFormFieldName, fileContentType, filePath, referer, requestEncoding, cookies);
            T jsonObject = JsonSerializer.Deserialize<T>(json);

            JsonObjectValidator(jsonObject);

            return jsonObject;
        }

        public virtual T Get<T>(string url, object parameters)
        {
            string json = Get(url, parameters);
            T jsonObject = JsonSerializer.Deserialize<T>(json);

            JsonObjectValidator(jsonObject);

            return jsonObject;
        }

        public virtual T Post<T>(string url, object parameters, CookieCollection cookies)
        {
            string json = Post(url, parameters, cookies);
            T jsonObject = JsonSerializer.Deserialize<T>(json);

            JsonObjectValidator(jsonObject);

            return jsonObject;
        }

        protected virtual void JsonObjectValidator<T>(T obj)
        {
            if (null != HttpRequestJsonObjectResultValidator && !HttpRequestJsonObjectResultValidator.Validate<T>(obj))
            {
            }
        }

        protected virtual bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        /// <summary>
        /// 将所有参数以Get请求的方式组装
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string BuildGetParamters(Object t)
        {
            StringBuilder result = new StringBuilder();
            Type type = t.GetType();
            PropertyInfo[] propertys = type.GetProperties();
            if (propertys != null && propertys.Length > 0)
            {
                foreach (PropertyInfo property in propertys)
                {
                    object value = property.GetValue(t, null);
                    if (value != null)
                    {
                        result.Append(property + "=" + Convert.ToString(value));
                        result.Append("&");
                    }
                }
            }
            if (result.Length > 0)
            {
                result.Insert(0, "?");
                result.Remove(result.Length - 1, 1);
            }
            return result.ToString();
        }
    }
}
