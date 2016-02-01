using System.Linq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Response
{

    /// <summary>
    /// Parse the json response that contain error information
    /// </summary>
    public class ErrorParser : IResponseParser
    {
        public IResponseInfo Parse(string response)
        {
            if (string.IsNullOrWhiteSpace(response))
                return ErrorResponseInfo.Empty();

            JObject json = JObject.Parse(response);
            var version = JsonHelper.JsonToString(json.GetValue(JsonRpcConstants.RPC_PARAMETER_NAME));
            ErrorResponseInfo returnValue = ErrorResponseInfo.Empty(version);

            var result = json.GetValue(JsonRpcConstants.ERROR_PARAMETER_NAME) as JObject;
            if (result != null)
            {
                string message = null;
                var code = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.CODE_PARAMETER_NAME));
                var data = result.GetValue(JsonRpcConstants.DATA_PARAMETER_NAME);

                if (!data.HasValues)
                {
                    message = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + code);
                }
                else
                {
                    var unformattedMessage = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + code);
                    if (!string.IsNullOrWhiteSpace(unformattedMessage))
                        message = string.Format(unformattedMessage, data.Values<object>().ToArray());
                }

                if (string.IsNullOrWhiteSpace(message))
                {
                    message = JsonHelper.JsonToString(result.GetValue(JsonRpcConstants.MESSAGE_PARAMETER_NAME));
                    if (data.HasValues)
                        message = string.Format(message, data.Values<object>().ToArray());
                }

                int id = JsonHelper.JsonToInt(json.GetValue("id"));

                returnValue = new ErrorResponseInfo(version, id, code, message);
            }

            return returnValue;
        }

        public bool CanParse(IParameters parameters)
        {
            // Error Parser can always be called
            return true;
        }
    }
}