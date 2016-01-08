using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core.Parameter;

namespace Obacher.RandomOrgSharp.Core.Response
{
    /// <summary>
    /// Class which parses the responses from Blob, Decimal, Guassian, Integer and String method calls.  
    /// All these methods return the same information except for the type so the same method is used for all of them.
    /// </summary>
    /// <typeparam name="T">Type of value being returned in the list of random values</typeparam>
    public class DataResponseParser<T> : IParser
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
                    if (dataArray != null && dataArray.HasValues)
                        data = dataArray.Values<T>();

                    completionTime = JsonHelper.JsonToDateTime(random.GetValue(RandomOrgConstants.JSON_COMPLETION_TIME_PARAMETER_NAME));
                }

                bitsUsed = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_BITS_USED_PARAMETER_NAME));
                bitsLeft = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_BITS_LEFT_PARAMETER_NAME));
                requestsLeft = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_REQUESTS_LEFT_PARAMETER_NAME));
                advisoryDelay = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_ADVISORY_DELAY_PARAMETER_NAME));
            }
            var id = JsonHelper.JsonToInt(json.GetValue("id"));

            return new DataResponseInfo<T>(version, data, completionTime, bitsUsed, bitsLeft, requestsLeft, advisoryDelay, id);
        }

        /// <summary>
        /// Identifies the types of responses that are handled by this class
        /// </summary>
        /// <param name="parameters">Parameters which are utilized by class</param>
        /// <returns>True if this class handles the method call</returns>
        public bool CanHandle(IParameters parameters)
        {
            return
                parameters.MethodType == MethodType.Blob ||
                parameters.MethodType == MethodType.Decimal ||
                parameters.MethodType == MethodType.Gaussian ||
                parameters.MethodType == MethodType.Integer ||
                parameters.MethodType == MethodType.String;
        }
    }
}