using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Obacher.RandomOrgSharp.Response
{
    public class BasicMethodResponseParser<T> : IParser
    {
        public IResponse Parse(JObject json)
        {
            var version = JsonHelper.JsonToString(json.GetValue(RandomOrgConstants.JSON_RPC_PARAMETER_NAME));
            var completionTime = DateTime.MinValue;
            IEnumerable<T> data = null;
            var bitsUsed = 0;
            var bitsLeft = 0;
            var requestsLeft = 0;
            var advisoryDelay = 0;

            var result = json.GetValue(RandomOrgConstants.JSON_RESULT_PARAMETER_NAME) as JObject;
            if (result != null)
            {
                var random = result.GetValue(RandomOrgConstants.JSON_RANDOM_PARAMETER_NAME) as JObject;
                if (random != null)
                {
                    var dataArray = random.GetValue(RandomOrgConstants.JSON_DATA_PARAMETER_NAME) as JArray;
                    if (dataArray != null)
                        data = dataArray.Values<T>();

                    completionTime = JsonHelper.JsonToDateTime(random.GetValue(RandomOrgConstants.JSON_COMPLETION_TIME_PARAMETER_NAME));
                }

                bitsUsed = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_BITS_USED_PARAMETER_NAME));
                bitsLeft = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_BITS_LEFT_PARAMETER_NAME));
                requestsLeft = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_REQUESTS_LEFT_PARAMETER_NAME));
                advisoryDelay = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_ADVISORY_DELAY_PARAMETER_NAME));
            }
            var id = JsonHelper.JsonToInt(json.GetValue("id"));

            return new BasicMethodResponse<T>(version, data, completionTime, bitsUsed, bitsLeft, requestsLeft, advisoryDelay, id);
        }
    }
}