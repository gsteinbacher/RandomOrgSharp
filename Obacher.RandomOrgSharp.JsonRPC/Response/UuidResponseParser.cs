using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Response
{
    /// <summary>
    /// Class which parses the responses from the UUID method call.  The UUID method call needs to have its own parser
    /// because the data returned in the responseInfo needs to be specially converted to a GUID type.
    /// </summary>
    public class UuidResponseParser : IResponseParser
    {
        /// <summary>
        /// Parse the string returned from <see cref="IRandomService"/> which called the GetUUID method.
        /// </summary>
        /// <param name="response">Response returned from <see cref="IRandomService"/> request</param>
        /// <returns>Class which contains the information from the JSON responseInfo</returns>
        public IResponseInfo Parse(string response)
        {
            JObject json = JObject.Parse(response);

            var version = JsonHelper.JsonToString(json.GetValue(JsonRpcConstants.RPC_PARAMETER_NAME));
            var completionTime = DateTime.MinValue;
            IEnumerable<Guid> data = null;
            var bitsUsed = 0;
            var bitsLeft = 0;
            var requestsLeft = 0;
            var advisoryDelay = 0;

            var result = json.GetValue(JsonRpcConstants.RESULT_PARAMETER_NAME) as JObject;
            if (result != null)
            {
                var random = result.GetValue(JsonRpcConstants.RANDOM_PARAMETER_NAME) as JObject;
                if (random != null)
                {
                    var dataArray = random.GetValue(JsonRpcConstants.DATA_PARAMETER_NAME) as JArray;
                    if (dataArray != null && dataArray.HasValues)
                        data = Array.ConvertAll(dataArray.Values<string>().ToArray(), guid => new Guid(guid));

                    completionTime = JsonHelper.JsonToDateTime(random.GetValue(JsonRpcConstants.COMPLETION_TIME_PARAMETER_NAME));
                }

                bitsUsed = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.BITS_USED_PARAMETER_NAME));
                bitsLeft = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.BITS_LEFT_PARAMETER_NAME));
                requestsLeft = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.REQUESTS_LEFT_PARAMETER_NAME));
                advisoryDelay = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.ADVISORY_DELAY_PARAMETER_NAME));
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