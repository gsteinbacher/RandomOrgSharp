using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Obacher.RandomOrgSharp.Core;
using Obacher.RandomOrgSharp.Core.Parameter;
using Obacher.RandomOrgSharp.Core.Response;

namespace Obacher.RandomOrgSharp.JsonRPC.Response
{
    public class ErrorHandlerParser : IErrorHandler
    {
        public int Code { get; private set; }
        public string Message { get; private set; }

        public int Id { get; private set; }

        public bool HasError()
        {
            return Code > 0;
        }

        public void Process(string response)
        {
            JObject json = JObject.Parse(response);

            Id = JsonHelper.JsonToInt(json.GetValue("id"));

            var result = json.GetValue(JsonRpcConstants.ERROR_PARAMETER_NAME) as JObject;
            if (result != null)
            {
                Code = JsonHelper.JsonToInt(result.GetValue(JsonRpcConstants.CODE_PARAMETER_NAME));
                var data = result.GetValue(JsonRpcConstants.DATA_PARAMETER_NAME);

                if (!data.HasValues)
                    Message = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + Code);
                else
                {
                    var unformattedMessage = ResourceHelper.GetString(StringsConstants.ERROR_CODE_KEY + Code);
                    if (!string.IsNullOrWhiteSpace(unformattedMessage))
                        Message = string.Format(unformattedMessage, data.Values<object>().ToArray());
                }

                if (string.IsNullOrWhiteSpace(Message))
                    Message = JsonHelper.JsonToString(result.GetValue(JsonRpcConstants.MESSAGE_PARAMETER_NAME));
            }
        }
    }
}