using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
{
    /// <summary>
    /// Class which parses the responses from the UUID method call.  The UUID method call needs to have its own parser
    /// because the data returned in the responseInfo needs to be specially converted to a GUID type.
    /// </summary>
    /// <typeparam name="T">Type of value being returned in the list of random values</typeparam>
    public class UuidResponseParser : IParser
    {
        /// <summary>
        /// Parse the JSON responseInfo
        /// </summary>
        /// <param name="json">JSON responseInfo</param>
        /// <returns>Class which contains the information from the JSON responseInfo</returns>
        public IResponseInfo Parse(JObject json)
        {
            var version = JsonHelper.JsonToString(json.GetValue(RandomOrgConstants.JSON_RPC_PARAMETER_NAME));
            var completionTime = DateTime.MinValue;
            IEnumerable<Guid> data = null;
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
                    if (dataArray != null && dataArray.HasValues)
                        data = Array.ConvertAll(dataArray.Values<string>().ToArray(), guid => new Guid(guid));

                    completionTime = JsonHelper.JsonToDateTime(random.GetValue(RandomOrgConstants.JSON_COMPLETION_TIME_PARAMETER_NAME));
                }

                bitsUsed = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_BITS_USED_PARAMETER_NAME));
                bitsLeft = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_BITS_LEFT_PARAMETER_NAME));
                requestsLeft = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_REQUESTS_LEFT_PARAMETER_NAME));
                advisoryDelay = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_ADVISORY_DELAY_PARAMETER_NAME));
            }
            var id = JsonHelper.JsonToInt(json.GetValue("id"));

            return new DataResponseInfo<Guid>(version, data, completionTime, bitsUsed, bitsLeft, requestsLeft, advisoryDelay, id);
        }

        /// <summary>
        /// Identifies the types of responses that are handled by this class
        /// </summary>
        /// <param name="parameters">Parameters which are utilized by class</param>
        /// <returns>True if this class handles the method call</returns>
        public bool CanHandle(IParameters parameters)
        {
            return parameters.MethodType == MethodType.Uuid;
        }
    }
}