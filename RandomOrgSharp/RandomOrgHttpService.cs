using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Properties;

namespace Obacher.RandomOrgSharp
{
    public class RandomOrgApiService : IRandomOrgService
    {
        private const string URL = "https://api.random.org/json-rpc/1/invoke";
        private const string CONTENT_TYPE = "application/json";

        private const string HTTP_REQUEST_TIMEOUT_KEY = "HttpRequestTimeout";
        private const string HTTP_READWRITE_TIMEOUT_KEY = "HttpReadWriteTimeout";
        private const int DEFAULT_REQUEST_TIMEOUT = 180000;
        private const int DEFAULT_READWRITE_TIMEOUT = 180000;
        private readonly int _httpRequestTimeout;
        private readonly int _httpReadWriteTimeout;

        public RandomOrgApiService()
        {

            _httpRequestTimeout = SettingsManager.Instance.GetConfigurationValue(HTTP_REQUEST_TIMEOUT_KEY, DEFAULT_REQUEST_TIMEOUT);
            _httpReadWriteTimeout = SettingsManager.Instance.GetConfigurationValue(HTTP_READWRITE_TIMEOUT_KEY, DEFAULT_READWRITE_TIMEOUT);
        }

        public JObject SendRequest(JObject jsonRequest)
        {
            HttpWebRequest request = SetupRequest();

            byte[] bytes = Encoding.UTF8.GetBytes(jsonRequest.ToString());

            JObject jsonResponse;
            try
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    requestStream.Write(bytes, 0, bytes.Length);

                    using (WebResponse response = request.GetResponse())
                    {
                        jsonResponse = GetResponse(response);
                    }
                }
            }
            catch (Exception e)
            {
                throw new RandomOrgRunTimeException(e.Message, e);
            }

            return jsonResponse;
        }

        public async Task<JObject> SendRequestAsync(JObject jsonRequest)
        {
            HttpWebRequest request = SetupRequest();

            byte[] bytes = Encoding.UTF8.GetBytes(jsonRequest.ToString());

            JObject jsonResponse;
            try
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    await requestStream.WriteAsync(bytes, 0, bytes.Length);

                    using (WebResponse response = await request.GetResponseAsync())
                    {
                        jsonResponse = GetResponse(response);
                    }
                }
            }
            catch (Exception e)
            {
                throw new RandomOrgRunTimeException(e.Message, e);
            }

            return jsonResponse;
        }

        /// <summary>
        /// Setup the HTTP Request object
        /// </summary>
        /// <returns>IHttpRequest</returns>
        private HttpWebRequest SetupRequest()
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
            request.Timeout = _httpRequestTimeout;
            request.ReadWriteTimeout = _httpReadWriteTimeout;
            request.Method = "POST";
            request.ContentType = CONTENT_TYPE;
            return request;
        }

        /// <summary>
        /// Retrieve the information from the HTTP Response stream
        /// </summary>
        /// <param name="response">Reponse object from random.org</param>
        /// <returns>JSON object containing response from random.org</returns>
        private static JObject GetResponse(WebResponse response)
        {
            JObject jsonResponse = null;
            Stream responseStream = response.GetResponseStream();
            if (responseStream != null)
            {
                using (StreamReader reader = new StreamReader(responseStream))
                {
                    string content = reader.ReadToEnd();
                    jsonResponse = JObject.Parse(content);
                }
            }

            return jsonResponse;
        }
    }
}
