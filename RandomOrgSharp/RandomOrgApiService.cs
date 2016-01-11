using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Obacher.RandomOrgSharp.Core
{
    public class RandomOrgApiService : IRandomService
    {
        private const string Url = "https://api.random.org/json-rpc/1/invoke";
        private const string ContentType = "application/json";

        private const string HttpRequestTimeoutKey = "HttpRequestTimeout";
        private const string HttpReadwriteTimeoutKey = "HttpReadWriteTimeout";
        private const int DefaultRequestTimeout = 180000;
        private const int DefaultReadwriteTimeout = 180000;

        private readonly int _httpRequestTimeout;
        private readonly int _httpReadWriteTimeout;

        public RandomOrgApiService()
        {
            _httpRequestTimeout = SettingsManager.Instance.GetConfigurationValue(HttpRequestTimeoutKey, DefaultRequestTimeout);
            _httpReadWriteTimeout = SettingsManager.Instance.GetConfigurationValue(HttpReadwriteTimeoutKey, DefaultReadwriteTimeout);
        }

        public string SendRequest(string request)
        {
            string response;
            HttpWebRequest httpRequest = SetupHttpRequest();

            byte[] bytes = Encoding.UTF8.GetBytes(request);

            try
            {
                using (Stream requestStream = httpRequest.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);

                    using (WebResponse httpResponse = httpRequest.GetResponse())
                    {
                        response = GetResponse(httpResponse);
                    }
                }
            }
            catch (Exception e)
            {
                throw new RandomOrgRunTimeException(e.Message, e);
            }

            return response;
        }

        public async Task<string> SendRequestAsync(string request)
        {
            string response;
            HttpWebRequest httpRequest = SetupHttpRequest();

            byte[] bytes = Encoding.UTF8.GetBytes(request);

            try
            {
                using (Stream requestStream = httpRequest.GetRequestStream())
                {
                    await requestStream.WriteAsync(bytes, 0, bytes.Length);

                    using (WebResponse httpResponse = await httpRequest.GetResponseAsync())
                    {
                        response = GetResponse(httpResponse);
                    }
                }
            }
            catch (Exception e)
            {
                throw new RandomOrgRunTimeException(e.Message, e);
            }

            return response;
        }

        /// <summary>
        /// Setup the HTTP Request object
        /// </summary>
        /// <returns>IHttpRequest</returns>
        private HttpWebRequest SetupHttpRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Timeout = _httpRequestTimeout;
            request.ReadWriteTimeout = _httpReadWriteTimeout;
            request.Method = "POST";
            request.ContentType = ContentType;
            return request;
        }

        /// <summary>
        /// Retrieve the information from the HTTP ResponseInfo stream
        /// </summary>
        /// <param name="httpResponse">Reponse object from random.org</param>
        /// <returns>JSON object containing response from random.org</returns>
        private static string GetResponse(WebResponse httpResponse)
        {
            string response = null;

            Stream responseStream = httpResponse.GetResponseStream();
            if (responseStream != null)
            {
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    response = reader.ReadToEnd();

                }
            }

            return response;
        }
    }
}
