using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp
{
    public class RandomOrgWrapper : IRandomOrgWrapper
    {

        private const string URL = "https://api.random.org/json-rpc/1/invoke";
        private const string CONTENT_TYPE = "application/json";

        public async Task<JObject> SendRequest(JObject jsonRequest)
        {
            JObject jsonResponse = null;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);

            //set HTTP properties
            request.Method = "POST";
            request.ContentType = CONTENT_TYPE;

            byte[] bytes = Encoding.UTF8.GetBytes(jsonRequest.ToString());

            try
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    await requestStream.WriteAsync(bytes, 0, bytes.Length);

                    using (WebResponse response = await request.GetResponseAsync())
                    {
                        Stream responseStream = response.GetResponseStream();
                        if (responseStream != null)
                        {
                            using (StreamReader reader = new StreamReader(responseStream))
                            {
                                string content = reader.ReadToEnd();
                                jsonResponse = JObject.Parse(content);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new RandomOrgRunTimeException("Code: 9999", e);
            }

            return jsonResponse;
        }
    }
}
