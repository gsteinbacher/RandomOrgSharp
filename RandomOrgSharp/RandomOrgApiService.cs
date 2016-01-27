using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Obacher.Framework.Common.SystemWrapper;

namespace Obacher.RandomOrgSharp.Core
{
    public class RandomOrgApiService : IRandomService
    {
        private const string ContentType = "application/json";

        private readonly string _url;
        private readonly int _httpRequestTimeout;
        private readonly int _httpReadWriteTimeout;

        public RandomOrgApiService(ISettingsManager settingsManager = null)
        {
            if (settingsManager == null)
                settingsManager = new SettingsManager(new ConfigurationManagerWrap());

            _url = settingsManager.GetUrl();
            _httpRequestTimeout = settingsManager.GetHttpRequestTimeout();
            _httpReadWriteTimeout = settingsManager.GetHttpReadWriteTimeout();
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
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
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
