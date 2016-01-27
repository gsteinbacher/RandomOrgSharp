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
            JObject json = JObject.Parse(response);

            int id = JsonHelper.JsonToInt(json.GetValue("id"));
            int code = 0;
            string message = null;

            var result = json.GetValue(JsonRpcConstants.ERROR_PARAMETER_NAME) as JObject;
            if (result != null)
            {
                code = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.CODE_PARAMETER_NAME));
                var data = result.GetValue(JsonRpcConstants.DATA_PARAMETER_NAME);

                if (!data.HasValues)
                    message = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + code);
                else
                {
                    var unformattedMessage = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + code);
                    if (!string.IsNullOrWhiteSpace(unformattedMessage))
                        message = string.Format(unformattedMessage, data.Values<object>().ToArray());
                }

                if (string.IsNullOrWhiteSpace(message))
                    message = JsonHelper.JsonToString(result.GetValue(JsonRpcConstants.MESSAGE_PARAMETER_NAME));
            }

            return new ErrorResponseInfo(id, code, message);
        }

        public bool CanHandle(IParameters parameters)
        {
            // Error Parser can always be called
            return true;
        }
    }
}