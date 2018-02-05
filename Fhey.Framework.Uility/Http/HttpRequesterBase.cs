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

namespace Fhey.Framework.Uility.Network.Http
{
    public abstract class HttpRequesterBase : IHttpRequester
    {

        public IJsonSerializer JsonSerializer = new JsonSerializer();
        public virtual IHttpRequestJsonObjectResultValidator HttpRequestJsonObjectResultValidator { get; set; }

        public virtual HttpWebResponse CreateGetHttpResponse(string url, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies)
        {
            
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (string.IsNullOrEmpty(contentType))
            {
                throw new ArgumentNullException("contentType");
            }

            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Method = "GET";
            request.UserAgent = userAgent;
            request.ContentType = contentType;
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8";

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
            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendLine("");
            for(int index=0;index<url.Length+4-1;index++)
            sbOutput.Append("*");
            sbOutput.AppendLine("*");
            sbOutput.AppendLine("url:" + url);
            sbOutput.AppendLine("Method:" + request.Method);
            sbOutput.AppendLine("UserAgent:" + request.UserAgent);
            sbOutput.AppendLine("ContentType:" + request.ContentType);
            sbOutput.AppendLine("Accept:" + request.Accept);
            for (int index = 0; index < url.Length + 4 - 1; index++)
                sbOutput.Append("*");
            sbOutput.AppendLine("*");
            return request.GetResponse() as HttpWebResponse;
        }

       
        public virtual HttpWebResponse CreatePostHttpResponse(string url, string postData, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("url");
            }
            if (string.IsNullOrEmpty(contentType))
            {
                throw new ArgumentNullException("contentType");
            }

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
            request.Method = "POST";
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
            
            if (!string.IsNullOrEmpty(postData))
            {
                byte[] data = requestEncoding.GetBytes(postData);
                using (Stream stream = request.GetRequestStream())
                {
                    stream.Write(data, 0, data.Length);
                }
            }

            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendLine("");
            for (int index = 0; index < url.Length + 4 - 1; index++)
                sbOutput.Append("*");
            sbOutput.AppendLine("*");
            sbOutput.AppendLine("url:" + url);
            sbOutput.AppendLine("Method:" + request.Method);
            sbOutput.AppendLine("UserAgent:" + request.UserAgent);
            sbOutput.AppendLine("ContentType:" + request.ContentType);
            sbOutput.AppendLine("Accept:" + request.Accept);
            for (int index = 0; index < url.Length + 4 - 1; index++)
                sbOutput.Append("*");
            sbOutput.AppendLine("*");
            return request.GetResponse() as HttpWebResponse;
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

            StringBuilder sbOutput = new StringBuilder();
            sbOutput.AppendLine("");
            for (int index = 0; index < url.Length + 4 - 1; index++)
                sbOutput.Append("*");
            sbOutput.AppendLine("*");
            sbOutput.AppendLine("url:" + url);
            sbOutput.AppendLine("Method:" + request.Method);
            sbOutput.AppendLine("UserAgent:" + request.UserAgent);
            sbOutput.AppendLine("ContentType:" + request.ContentType);
            sbOutput.AppendLine("Accept:" + request.Accept);
            for (int index = 0; index < url.Length + 4 - 1; index++)
                sbOutput.Append("*");
            sbOutput.AppendLine("*");
            return request.GetResponse() as HttpWebResponse;
        }


      
        public virtual string Get(string url, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies)
        {
            string result = string.Empty;
            HttpWebResponse httpWebResponse = CreateGetHttpResponse(url, timeout, userAgent,contentType, requestEncoding, cookies);
            result=GetResponseStreamString(httpWebResponse, requestEncoding);
            return result;
        }

        string GetResponseStreamString(HttpWebResponse httpWebResponse,Encoding encoding)
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
           
            sbOutput.AppendLine("url:"+ httpWebResponse.ResponseUri.ToString());
            sbOutput.AppendLine("result:" + result);

            for (int index = 0; index < httpWebResponse.ResponseUri.AbsoluteUri.Length - 1; index++)
                sbOutput.Append("*");
            sbOutput.AppendLine("*");
            return result;
        }


        public virtual string Post(string url, string postData, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies=null)
        {
            string result = string.Empty;
            HttpWebResponse httpWebResponse = CreatePostHttpResponse(url, postData, timeout, userAgent,contentType, requestEncoding, cookies);
            result = GetResponseStreamString(httpWebResponse, requestEncoding);
            return result;
        }

  
        public virtual string PostFile(string url, IDictionary<string, string> parameters, Stream fileStream, string fileFormFieldName, string fileContentType, string filePath, string referer, Encoding requestEncoding, CookieCollection cookies)
        {
            string result = string.Empty;
            HttpWebResponse httpWebResponse = CreatePostFileHttpResponse(url, parameters,fileStream,fileFormFieldName,fileContentType,filePath,referer, requestEncoding, cookies);
            result = GetResponseStreamString(httpWebResponse, requestEncoding);
            return result;
        }

   
        public virtual string Get(string url)
        {
            return Get(url,10000, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)", "text/html;charset=UTF-8",Encoding.UTF8, null);
        }


        public virtual string Post(string url, string postData, CookieCollection cookies)
        {
            return Post(url, postData, 10000, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)", "text/html;charset=UTF-8", Encoding.UTF8, null);
        }

      
        public virtual T Get<T>(string url, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies)
        {
            string json = Get(url,timeout,userAgent,contentType,requestEncoding,cookies);
            T jsonObject = JsonSerializer.Deserialize<T>(json);

            JsonObjectValidator(jsonObject);

            return jsonObject;
        }

     
        public virtual T Post<T>(string url, string postData, int? timeout, string userAgent, string contentType, Encoding requestEncoding, CookieCollection cookies)
        {
            string json = Post(url, postData,timeout, userAgent, contentType, requestEncoding, cookies);
            T jsonObject = JsonSerializer.Deserialize<T>(json);

            JsonObjectValidator(jsonObject);

            return jsonObject;
        }


        public virtual T PostFile<T>(string url, IDictionary<string, string> parameters, Stream fileStream, string fileFormFieldName, string fileContentType, string filePath, string referer, Encoding requestEncoding, CookieCollection cookies)
        {
            string json = PostFile(url, parameters,fileStream,fileFormFieldName,fileContentType,filePath,referer, requestEncoding, cookies);
            T jsonObject = JsonSerializer.Deserialize<T>(json);

            JsonObjectValidator(jsonObject);

            return jsonObject;
        }

        public virtual T Get<T>(string url)
        {
            string json = Get(url);
            T jsonObject = JsonSerializer.Deserialize<T>(json);

            JsonObjectValidator(jsonObject);

            return jsonObject;
        }

        public virtual T Post<T>(string url, string postData, CookieCollection cookies)
        {
            string json = Post(url, postData, cookies);
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

    }
}
