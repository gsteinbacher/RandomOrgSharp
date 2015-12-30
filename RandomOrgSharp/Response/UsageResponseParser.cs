using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Parameter;

namespace Obacher.RandomOrgSharp.Response
{
    public class UsageResponseParser : IParser {
        public IResponse Parse(JObject json)
        {
            var version = JsonHelper.JsonToString(json.GetValue(RandomOrgConstants.JSON_RPC_VALUE));
            var result = json.GetValue(RandomOrgConstants.JSON_RESULT_PARAMETER_NAME) as JObject;
            StatusType status = StatusType.Unknown;
            DateTime creationTime = DateTime.MinValue;
            int bitsLeft = 0;
            int requestsLeft = 0;
            int totalBits = 0;
            int totalRequests = 0;

            if (result != null)
            {
                var statusString = JsonHelper.JsonToString(result.GetValue(RandomOrgConstants.JSON_STATUS_PARAMETER_NAME));
                switch (statusString)
                {
                    case RandomOrgConstants.JSON_STATUS_STOPPED:
                        status = StatusType.Stopped;
                        break;
                    case RandomOrgConstants.JSON_STATUS_PAUSED:
                        status = StatusType.Paused;
                        break;
                    case RandomOrgConstants.JSON_STATUS_RUNNING:
                        status = StatusType.Running;
                        break;
                    default:
                        status = StatusType.Unknown;
                        break;
                }

                creationTime = JsonHelper.JsonToDateTime(result.GetValue(RandomOrgConstants.JSON_CREATION_TIME_PARAMETER_NAME));
                bitsLeft = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_BITS_LEFT_PARAMETER_NAME));
                requestsLeft = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_REQUESTS_LEFT_PARAMETER_NAME));
                totalBits = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_TOTAL_BITS_PARAMETER_NAME));
                totalRequests = JsonHelper.JsonToInt(result.GetValue(RandomOrgConstants.JSON_TOTAL_REQUESTS_PARAMETER_NAME));
            }
            var id = JsonHelper.JsonToInt(json.GetValue("id"));

            var usageResponse = new UsageResponse(version, status, creationTime, bitsLeft, requestsLeft, totalBits, totalRequests, id);
            return usageResponse;
        }

        /// <summary>
        /// Identifies the types of responses that are handled by this class
        /// </summary>
        /// <param name="parameters">Parameters which are utilized by class</param>
        /// <returns>True if this class handles the method call</returns>
        public bool CanHandle(IParameters parameters)
        {
            return parameters.MethodType == MethodType.Usage;
        }

    }
}