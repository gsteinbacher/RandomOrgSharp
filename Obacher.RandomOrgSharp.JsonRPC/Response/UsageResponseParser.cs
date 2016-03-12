using System;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Response
{
    public class UsageResponseParser : IResponseParser
    {
        public IResponseInfo Parse(string response)
        {
            if (string.IsNullOrWhiteSpace(response))
                throw new RandomOrgRuntimeException(ResourceHelper.GetString(StringsConstants.EXCEPTION_CANNOT_BE_NULLOREMPTY, nameof(response)));

            JObject json = JObject.Parse(response);

            var version = JsonHelper.JsonToString(json.GetValue(JsonRpcConstants.RPC_PARAMETER_NAME));
            var result = json.GetValue(JsonRpcConstants.RESULT_PARAMETER_NAME) as JObject;
            StatusType status = StatusType.Unknown;
            DateTime creationTime = DateTime.MinValue;
            int bitsLeft = 0;
            int requestsLeft = 0;
            int totalBits = 0;
            int totalRequests = 0;

            if (result != null)
            {
                var statusString = JsonHelper.JsonToString(result.GetValue(JsonRpcConstants.STATUS_PARAMETER_NAME));
                switch (statusString)
                {
                    case RandomOrgConstants.STATUS_STOPPED:
                        status = StatusType.Stopped;
                        break;
                    case RandomOrgConstants.STATUS_PAUSED:
                        status = StatusType.Paused;
                        break;
                    case RandomOrgConstants.STATUS_RUNNING:
                        status = StatusType.Running;
                        break;
                    default:
                        status = StatusType.Unknown;
                        break;
                }

                creationTime = JsonHelper.JsonToDateTime(result.GetValue(JsonRpcConstants.CREATION_TIME_PARAMETER_NAME));
                bitsLeft = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.BITS_LEFT_PARAMETER_NAME));
                requestsLeft = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.REQUESTS_LEFT_PARAMETER_NAME));
                totalBits = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.TOTAL_BITS_PARAMETER_NAME));
                totalRequests = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.TOTAL_REQUESTS_PARAMETER_NAME));
            }
            var id = JsonHelper.JsonToInt(json.GetValue("id"));

            var usageResponse = new UsageResponseInfo(version, status, creationTime, bitsLeft, requestsLeft, totalBits, totalRequests, id);
            return usageResponse;
        }

        /// <summary>
        /// Identifies the types of responses that are handled by this class
        /// </summary>
        /// <param name="parameters">Parameters which are utilized by class</param>
        /// <returns>True if this class handles the method call</returns>
        public bool CanParse(IParameters parameters)
        {
            return parameters.MethodType == MethodType.Usage;
        }

    }
}